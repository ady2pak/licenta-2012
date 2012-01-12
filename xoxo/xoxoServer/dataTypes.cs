using System;
using System.Collections.Generic;

namespace xoxoChat
{
    [Serializable()]
    class dataTypes
    {
        public String objectType;
        public object myObject;

        public dataTypes() { }

        public void setType(String objectType)
        {
            this.objectType = objectType;
        }

        public void setObject(object myObject)
        {
            this.myObject = myObject;
        }
    }

    [Serializable()]
    class loginInfo
    {
        public string username;
        public string password;

        public void setUsername(String username)
        {
            this.username = username;
        }

        public void setPassword(String password)
        {
            this.password = password;
        }
    }

    [Serializable()]
    class messageToEveryone
    {
        public string whoAmI;
        public string message;

        public void setMe(string username)
        {
            this.whoAmI = username;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }
    }

    [Serializable()]
    class userList
    {
        public List<string> users = new List<string>();
    }

    [Serializable()]
    class dataFile
    {
        public string filename;
        public byte[] buffer;

        public void setFilename(string filename)
        {
            this.filename = filename;
        }

        public void setData(byte[] buffer)
        {
            this.buffer = buffer;
        }
    }

    [Serializable()]
    class datafileReceived
    {
        public string filename;
        public int partNo;
        public int totalPartNo;

        public void setFilename(string filename)
        {
            this.filename = filename;
        }

        public void setPartNo(int partNo)
        {
            this.partNo = partNo;
        }
        public void setTotalPartNo(int totalPartNo)
        {
            this.totalPartNo = totalPartNo;
        }
    }

    [Serializable()]
    class startPrivate
    {
        public string whoStarts;
        public string withWho;

        public void setWhoStarts(string whoStarts) { this.whoStarts = whoStarts; }
        public void setWithWho(string withWho) { this.withWho = withWho; }
    }

    [Serializable()]
    class iQuit
    {
        public string username;

        public void setUsername(string username)
        {
            this.username = username;
        }
    }
}
