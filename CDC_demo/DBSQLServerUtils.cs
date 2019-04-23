using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CDC_demo
{
    class DBSQLServerUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = "Data Source=VIKTORIYAKA5408;Initial Catalog=CDC_demo;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            
            try
            {
                Console.WriteLine("Openning Connection ...");
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
