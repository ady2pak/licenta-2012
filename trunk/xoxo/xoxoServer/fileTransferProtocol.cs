using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace xoxoChat
{
    class fileTransferProtocol
    {
        int partSize = 1024 * 1024 * 2;
        int counter = 0;

        public void SplitUp(string filename,int fileSizeInMB)
        {
            if (filename.Length < 1) {
                MessageBox.Show("Invalid filename.");
                return;
            }

            counter = 0;

            
            byte []buffer=new byte [partSize];
            string curFileName;

            BinaryReader br=new BinaryReader(File.Open(filename, FileMode.Open));
            
            //Check if slice size is grater than file size
            if (br.BaseStream.Length < partSize)
                partSize = (int)br.BaseStream.Length;

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
        }

        public void RebuildFile(string firstFileName)
        {
            if (firstFileName.Length < 1) {
                MessageBox.Show("Invalid Filname");
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
