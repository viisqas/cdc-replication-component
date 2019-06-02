using System;
using System.Data.SqlClient;

namespace CDC_demo
{
    class SqlConnector
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = "Data Source=VIKTORIYAKA5408;Initial Catalog=cdc_test;Integrated Security=True";
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
            //Console.WriteLine("Original: " + builder.ConnectionString);
            //string server = builder.DataSource;
            //string database = builder.InitialCatalog;

            //Console.WriteLine($"server: {server}, database: {database}");
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
