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
            private readonly string CDC_LOCAL_TABLE_NAMES = "SELECT SCHEMA_NAME(5) as schema_name, name as table_name from sys.tables where name like('dbo%')";
            
			SqlCommand cmd = new SqlCommand(CDC_LOCAL_TABLE_NAMES, connection);
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
        }

        public static List<string> GetColumnNames(SqlConnection connection, string tablename)
        {
            private readonly string get_column_from_table = $"select * from cdc.{tablename}";
            
			List<string> Column_names = new List<string>();
            SqlCommand cmd = new SqlCommand(get_column_from_table, connection);
            
            using (var dr = cmd.ExecuteReader())
            {
                for (int i = 5; i < dr.FieldCount-1; i++)
                {
                    var temp = $"{dr.GetName(i).ToString()}";
                    Column_names.Add(temp);
                }
            }
            return Column_names;
        }
    }
}
