using System;
using System.Data.SqlClient;

namespace CDC_demo
{
    class SqlConnector
    {
        public static SqlConnection GetDBConnection(connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            
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
