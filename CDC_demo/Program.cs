using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace CDC_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //DB connect
            SqlConnection conn = DBSQLServerUtils.GetDBConnection();

            //Get data
            string sql = "Select * from PersonalInfo";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int clientIdIndex = reader.GetOrdinal("ClientId");
                    long clientId = Convert.ToInt64(reader.GetValue(0));

                    string clientNo = reader.GetString(1);
                    int clientNameIndex = reader.GetOrdinal("FirstName");
                    string clientName = reader.GetString(clientNameIndex);

                    Console.WriteLine(clientName);
                    Console.WriteLine(sql);
                }
            }

            Console.Read();
        }
    }
}
