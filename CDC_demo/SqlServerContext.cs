using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CDC_demo
{
    public static class SqlServerContext
    {
        public static List<string> GetLocalTableNames(SqlConnection connection)
        {
            string get_sys_object = "SELECT SCHEMA_NAME(5) as schema_name, name as table_name from sys.tables where name like('dbo%')";
            SqlCommand cmd = new SqlCommand(get_sys_object, connection);
            List<string> dbo_tables = new List<string>();
            string row = null;

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    row = dr[1].ToString();
                    dbo_tables.Add(row);
                }
            }
            return dbo_tables;

            //foreach (string item in dbo_tables)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
