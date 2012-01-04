using System;
using System.Data.SqlClient;

namespace xoxoServer
{
    class DatabaseServices
    {
        NetworkServices netServ;
        SqlConnection myConnection;

        public DatabaseServices(NetworkServices netServ)
        {
            this.netServ = netServ;

            string connectionString = GetConnectionString();

            myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
        }

        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code, 
            // you can retrieve it from a configuration file.
            return "Data Source=|DataDirectory|/database.sdf";
        }

        public void executeQuery() 
        {
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from table",
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
        
    }
}
