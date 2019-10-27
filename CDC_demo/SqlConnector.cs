using System;
using System.Data.SqlClient;

namespace CDC_demo
{
    class SqlConnector
    {
        private readonly string connString = "Data Source=VIKTORIYAKA5408;Initial Catalog=service_order;Integrated Security=True";
        
        public static SqlConnection GetDBConnection()
        {
            SqlConnection connection = new SqlConnection(connString);
            
            try
            {
                Console.WriteLine("Openning Connection...");
                connection.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return connection;
        }
    }
}
