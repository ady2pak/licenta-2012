using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace xoxoChat
{
    class fileTransferProtocol
    {
        int partSize = 1024; // * 50;//1024;
        int counter = 0;
        NetworkServices netServ;

        public fileTransferProtocol(NetworkServices netServ)
        {
            this.netServ = netServ;
        }

        public void SplitUp(string filename)
        {
            if (filename.Length < 1) {
                MessageBox.Show("Invalid filename.");
                return;
            }

            counter = 0;
            
            byte []buffer=new byte [partSize];
            string curFileName;

            BinaryReader br=new BinaryReader(File.Open(filename, FileMode.Open));
            string fileEnd = ".0";
            //Check if slice size is grater than file size
            if (br.BaseStream.Length < partSize)
            {
                partSize = (int)br.BaseStream.Length;
                fileEnd = ".O.E";
            }

            //Slicing work starts here
            while (br.BaseStream.Length > partSize * counter)
            {
                if (br.BaseStream.Length > partSize * (counter + 1))
                {
                    br.BaseStream.Read(buffer, 0, partSize);
                    curFileName = filename + "." + counter.ToString();
                }
                else
                {
                    int remainLen = (int)br.BaseStream.Length - partSize * counter;
                    buffer = new byte[remainLen];
                    br.BaseStream.Read(buffer, 0, remainLen);
                    curFileName = filename + "." + counter.ToString() + ".E";
                }
                
                if (File.Exists(curFileName))
                    File.Delete(curFileName);

                File.WriteAllBytes(curFileName, buffer);
                counter++;

            }
            br.Close();
            MessageBox.Show("File spilt successful.");

            sendNextPart(filename + fileEnd, 0);
        }

        public void sendNextPart(string firstFileName, int partNo)
        {
            if (firstFileName.Length < 1)
            {
                MessageBox.Show("Invalid Filename");
                return;
            }

            string endPart = firstFileName;
            string orgFile = "";

            orgFile = endPart.Substring(0, endPart.LastIndexOf("."));
            endPart = endPart.Substring(endPart.LastIndexOf(".") + 1);


            if (File.Exists(orgFile + "." + partNo.ToString() + ".E")) sendPart(orgFile + "." + partNo.ToString(), true);
            else sendPart(orgFile + "." + partNo.ToString(), false);

        }

        void sendPart(string nextFileName, bool isLast)
        {
            //MessageBox.Show(nextFileName);

            byte[] fileNameByte = Encoding.ASCII.GetBytes(nextFileName);
            byte[] fileData;
            if (isLast) fileData = File.ReadAllBytes(nextFileName + ".E");
            else fileData = File.ReadAllBytes(nextFileName);
            byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
            byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

            fileNameLen.CopyTo(clientData, 0);
            fileNameByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fileNameByte.Length);

            dataFile fileToSend = new dataFile();
            fileToSend.setFilename(nextFileName);
            fileToSend.setData(clientData);

            dataTypes objToSend = new dataTypes();
            objToSend.setType(typeof(dataFile).ToString());
            objToSend.setObject(fileToSend);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            formatter.Serialize(stream, objToSend);

            byte[] buffer = ((MemoryStream)stream).ToArray();

            netServ.m_clientSocket.Send(buffer, buffer.Length, 0);

            stream.Close();
        }

        public void RebuildFile(string firstFileName)
        {
            if (firstFileName.Length < 1) {
                MessageBox.Show("Invalid Filename");
                return;
            }

            string endPart = firstFileName;
            string orgFile = "";

            orgFile = endPart.Substring(0, endPart.LastIndexOf("."));
            endPart = endPart.Substring(endPart.LastIndexOf(".") + 1);

            if (endPart == "E") //If only one slice is there
            {
                orgFile = orgFile.Substring(0, orgFile.LastIndexOf("."));
                endPart = "0";
            }

            if (File.Exists(orgFile))
            {
                if (MessageBox.Show(orgFile + " already exists, do you want to delete it", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    File.Delete(orgFile);
                else
                {
                    MessageBox.Show("File not assembled. Operation cancelled by user.");
                    return;
                }
            }
 
            //Assembling starts from here
            BinaryWriter bw = new BinaryWriter(File.Open(orgFile, FileMode.Append));
            string nextFileName = "";
            byte []buffer=new byte [bw.BaseStream.Length];

            
            int counter=int.Parse(endPart);
            while(true)
            {
                nextFileName = orgFile + "." + counter.ToString();
                if (File.Exists(nextFileName + ".E"))
                {
                    //Last slice
                    buffer = File.ReadAllBytes(nextFileName + ".E");
                    bw.Write(buffer);
                    break;
                }
                else
                {
                    buffer = File.ReadAllBytes(nextFileName);
                    bw.Write(buffer);
                }
                counter++;
            } 
            bw.Close();
            MessageBox.Show("File assebled successfully");
        }
       
    }    
}
