using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace StudentConnections
{
    public class fileTransferProtocol
    {
        string splitter = "'\\'";
        string fName;
        string[] split = null;
        byte[] clientData;
        ClientMW clientMW;
        string filename;

        public fileTransferProtocol(ClientMW clientMW)
        {
            this.clientMW = clientMW;
        }

        public void OpenFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            char[] delimiter = splitter.ToCharArray();
            openFileDialog1.ShowDialog();
            filename = openFileDialog1.FileName;            
            split = filename.Split(delimiter);
            int limit = split.Length;
            fName = split[limit - 1].ToString();
            clientMW.setFileName(fName);

        }

        public void SendFile()
        {
            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            byte[] fileName = Encoding.UTF8.GetBytes(fName); //file name
            byte[] fileData = File.ReadAllBytes(filename); //file
            byte[] fileNameLen = BitConverter.GetBytes(fileName.Length); //lenght of file name
            clientData = new byte[4 + fileName.Length + fileData.Length];

            fileNameLen.CopyTo(clientData, 0);
            fileName.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileName.Length);


            clientSock.Connect("127.0.0.1", 9050); //target machine's ip address and the port number
            clientSock.Send(clientData);
            clientSock.Close();
        }
    }  
}
