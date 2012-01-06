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
    class iQuit
    {
        public string username;

        public void setUsername(string username)
        {
            this.username = username;
        }
    }
}
