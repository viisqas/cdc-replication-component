using System;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CDC_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = DBSQLServerUtils.GetDBConnection();
            var result_tables = CDCData.GetTableList();
            
            foreach (string item in result_tables)
            {
                CDCData.SerializeSchema(item, conn);
                CDCData.SerializeTable(item, conn);
            }
            
            Console.Read();
        }
    }
}
