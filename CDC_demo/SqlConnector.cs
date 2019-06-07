using System;
using System.Data.SqlClient;

namespace CDC_demo
{
    class SqlConnector
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = "Data Source=VIKTORIYAKA5408;Initial Catalog=service_order;Integrated Security=True";

            SqlConnection conn = new SqlConnection(connString);
            
            try
            {
                Console.WriteLine("Openning Connection...");
                conn.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return conn;
        }
    }
}
