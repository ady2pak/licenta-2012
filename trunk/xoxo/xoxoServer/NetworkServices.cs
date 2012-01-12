using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace xoxoChat
{
    class NetworkServices
    {
        public ServerMW serverMW;

        public List<Client> clients = new List<Client>();        
        public int clientsIndex = 0;

        public Socket m_socListener;
        public Socket[] m_socWorker = new Socket[10];
        int m_clientCount = 0;
        
        Encoding encoding = Encoding.UTF8;
        
        ServerToClientBridge STCB;
        DatabaseServices DS;

        public AsyncCallback pfnWorkerCallback;
        
        public NetworkServices(ServerMW windowForm)
        {
            this.serverMW = windowForm;
            startListening();
            STCB = new ServerToClientBridge(this);
            DS = new DatabaseServices(this);

            DS.executeQuery();

        }
        
        public void startListening()
        {
            try
            {
                m_socListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, 8221);
                m_socListener.Bind(ipLocal);
                m_socListener.Listen(4);
                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (SocketException se)
            {
                serverMW.appendDebugOutput(se.Message);
            }
        }


        public void OnClientConnect(IAsyncResult asyn)
        {
            string userName = "placeholder"; // to be edited
            try
            {
                m_socWorker[m_clientCount] = m_socListener.EndAccept(asyn);

                Client _newUser = new Client(m_socWorker[m_clientCount], userName);
                clients.Add(_newUser);

                WaitForData(m_socWorker[m_clientCount]);
                ++m_clientCount;

                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                serverMW.appendDebugOutput(se.Message + se.StackTrace);
            }
            catch (Exception ex)
            {
                serverMW.appendDebugOutput(ex.Message + ex.StackTrace);
            }
        }

        public class SocketPacket
        {
            public Socket m_currentSocket;
            public byte[] dataBuffer = new byte[(1024 * 1024 * 4) + 100];
        }

        public void WaitForData(Socket m_socWorker)
        {
            try
            {
                if (pfnWorkerCallback == null)
                {
                    pfnWorkerCallback = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSokPkt = new SocketPacket();
                theSokPkt.m_currentSocket = m_socWorker;
                m_socWorker.BeginReceive(theSokPkt.dataBuffer, 0,
                    theSokPkt.dataBuffer.Length,
                    SocketFlags.None,
                    pfnWorkerCallback,
                    theSokPkt);
            }
            catch (SocketException se)
            {
                serverMW.appendDebugOutput(se.Message);
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int bytesReceived = 0;
                bytesReceived = socketData.m_currentSocket.EndReceive(asyn);
                byte[] buffer = new byte[bytesReceived + 1];

                buffer = socketData.dataBuffer;

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();

                stream.Write(buffer, 0, buffer.Length);
                stream.Seek(0, 0);

                dataTypes objReceived = new dataTypes();

                objReceived = (dataTypes)formatter.Deserialize(stream);

                parseObject(objReceived, socketData.m_currentSocket);

                stream.Close();
                
                WaitForData(socketData.m_currentSocket);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                if (se.ErrorCode.ToString().Equals("10054"))
                {

                    SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                    serverMW.appendDebugOutput("Client disconnected : ");

                    for (int i = 0; i < m_clientCount; i++)
                    {
                        if (m_socWorker[i] == socketData.m_currentSocket)
                            for (int j = i; j < m_clientCount - 1; j++)
                            {
                                m_socWorker[j] = m_socWorker[j + 1];
                            }
                        m_clientCount--;
                    }

                    for (int i = 0; i < clients.Count; i++)
                        if (clients[i].getSocket() == socketData.m_currentSocket)
                        {
                            serverMW.appendDebugOutput(clients[i].getUserName());
                            clients.Remove(clients[i]);

                        }


                    socketData.m_currentSocket.Close();
                    socketData.m_currentSocket.Dispose();

                    userList connectedUsers = new userList();
                    
                    for (int index = 0; index < clients.Count; index++)
                    {
                        connectedUsers.users.Add(clients[index].getUserName());
                    }

                    STCB.sendUserlistToClients(connectedUsers);
                }
            }
            catch (Exception ex)
            {
                serverMW.appendDebugOutput(ex.Message);
            }
        }

        private void parseObject(dataTypes objReceived, Socket m_socWorker)
        {
            if (objReceived.objectType.Equals(typeof(loginInfo).ToString())) 
            {
                loginInfo clientInfo = (loginInfo)objReceived.myObject;
                bool result = DS.isClientAuthorized((loginInfo)objReceived.myObject);
                if (result) serverMW.appendDebugOutput("New client connecterd : " + clientInfo.username);
                
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].getSocket() == m_socWorker)
                        clients[i].setName(clientInfo.username);
                }

                userList connectedUsers = new userList();
                for (int index = 0; index < clients.Count; index++)
                {
                    connectedUsers.users.Add(clients[index].getUserName());
                }

                STCB.sendUserlistToClients(connectedUsers);
            }
            if (objReceived.objectType.Equals(typeof(messageToEveryone).ToString()))
            {
                messageToEveryone msg = (messageToEveryone)objReceived.myObject;
                STCB.sendMsgToAllClients(msg);
            }
            if (objReceived.objectType.Equals(typeof(iQuit).ToString()))
            {

            }
            if (objReceived.objectType.Equals(typeof(dataFile).ToString()))
            {
                #region old filetransfer

                /*string receivedPath = "C:/";
                dataFile fileReceived = (dataFile)objReceived.myObject;
                int fileNameLen = BitConverter.ToInt32(fileReceived.buffer, 0);
                string fileName = Encoding.ASCII.GetString(fileReceived.buffer, 4, fileNameLen);
                fileName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);

                
                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + fileName, FileMode.Append));                
                bWrite.Write(fileReceived.buffer, 4 + fileNameLen, fileReceived.buffer.Length - 4 - fileNameLen);

                bWrite.Close();

                serverMW.appendDebugOutput(receivedPath + fileName);

                datafileReceived dfR = new datafileReceived();
                dfR.setFilename(Encoding.ASCII.GetString(fileReceived.buffer, 4, fileNameLen));
                dfR.setPartNo(Int32.Parse(fileName.Substring(fileName.LastIndexOf(".") + 1)));
                dfR.setTotalPartNo(100);
                
                dataTypes wasReceived = new dataTypes();
                wasReceived.setType(typeof(datafileReceived).ToString());
                wasReceived.setObject(dfR);

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream();

                formatter.Serialize(stream, wasReceived);

                byte[] buffer = ((MemoryStream)stream).ToArray();
                m_socWorker.Send(buffer, buffer.Length, 0);*/
                #endregion
            }
            if (objReceived.objectType.Equals(typeof(startPrivate).ToString()))
            {
                startPrivate startPRV = (startPrivate)objReceived.myObject;
                STCB.sendMsgToClient(startPRV.withWho, objReceived);
            }
                
        }
        
        public Socket getUserSocketByName(string username)
        {
            for (int index = 0 ; index < clients.Count ; index++)
            {
                if (clients[index].getUserName().Equals(username))
                {
                    return clients[index].getSocket();
                }
            }
            return null;
        }
    }


}
