using System;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace xoxoChat
{
    class DatabaseServices
    {
        NetworkServices netServ;
        SqlCeConnection myConnection;

        public DatabaseServices(NetworkServices netServ)
        {
            this.netServ = netServ;

            myConnection = new SqlCeConnection(@"Data Source = |DataDirectory|\database.sdf");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
        }

        public void executeQuery() 
        {
            try
            {
                SqlCeDataReader myReader = null;
                SqlCeCommand myCommand = new SqlCeCommand("select * from Users",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Console.WriteLine(myReader["id"].ToString());
                    Console.WriteLine(myReader["username"].ToString());
                    Console.WriteLine(myReader["password"].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        internal bool isClientAuthorized(loginInfo loginInfo)
        {
            return true;
        }
    }
}
