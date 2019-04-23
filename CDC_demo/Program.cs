using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

namespace CDC_demo
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            //DB connect
            SqlConnection conn = DBSQLServerUtils.GetDBConnection();

            //Get data
            string sqlExpression = "Select * from cdc.dbo_PersonalInfo_CT";

            SqlCommand command = new SqlCommand(sqlExpression, conn);
            SqlDataReader reader = command.ExecuteReader();
      
            if (reader.HasRows)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetName(0), reader.GetName(3), reader.GetName(5), reader.GetName(6));

                while (reader.Read())
                {
                    byte[] start_lsn = (byte[])reader.GetValue(0);
                    int operation = reader.GetInt32(3);
                    int ClientId = reader.GetInt32(5);
                    string FirstName = reader.GetString(6);
                    
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", start_lsn, operation, ClientId, FirstName);
                }
            }

            reader.Close();
            Console.Read();
        }
    }
}
