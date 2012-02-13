using System;
using System.Collections.Generic;

namespace StudentConnections
{
    [Serializable()]
    public class dataTypes
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
    class startPrivate
    {
        public string whoStarts;
        public string withWho;

        public void setWhoStarts(string whoStarts) { this.whoStarts = whoStarts; }
        public void setWithWho(string withWho) { this.withWho = withWho; }
    }

    [Serializable()]
    class privateMessage
    {
        public string toWho;
        public string whoSent;
        public string message;

        public void setToWho(string toWho)
        {
            this.toWho = toWho;
        }

        public void setWhoSent(string whoSent)
        {
            this.whoSent = whoSent;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }
    }

    [Serializable()]
    class closePrivate
    {
        public string toWho;
        public string whoSent;        

        public void setToWho(string toWho)
        {
            this.toWho = toWho;
        }

        public void setWhoSent(string whoSent)
        {
            this.whoSent = whoSent;
        }
    }

    /*[Serializable()]
    class iQuit
    {
        public string username;

        public void setUsername(string username)
        {
            this.username = username;
        }
    }
    */
}
