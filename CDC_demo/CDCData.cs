using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace CDC_demo
{
    public static class CDCData
    {
        public static void SerializeTable(string tablename, SqlConnection connection)
        {
            //SqlConnection conn = DBSQLServerUtils.GetDBConnection();
            string sqlExpression = $"Select * from cdc.{tablename}";
            XmlDocument doc = new XmlDocument();
            string filedate = DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss");
            string filename = $"{tablename}.{filedate}.xml";

            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
            DataSet ds = new DataSet($"{tablename}");
            adapter.Fill(ds);
            //DataTable dt = ds.Tables[0];
            ds.WriteXml(filename);
            Console.WriteLine($"table {tablename} is serialized");
            connection.Close();
        }

        public static void SerializeSchema(string tablename, SqlConnection connection)
        {
            //SqlConnection conn = DBSQLServerUtils.GetDBConnection();
            string schema_file = $"{tablename}.schema.xml";
            string sqlExpression = $"Select * from cdc.{tablename}";
            XmlDocument doc = new XmlDocument();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
            DataSet ds = new DataSet($"{tablename}");
            adapter.Fill(ds);
            ds.WriteXmlSchema(schema_file);
            Console.WriteLine($"schema {tablename} is serialized");
            connection.Close();
        }

        public static List<string> GetTableList()
        {
            SqlConnection conn = DBSQLServerUtils.GetDBConnection();
            DataTable tables = conn.GetSchema("tables");
            conn.Close();
            List<string> tablelist = new List<string>();

            for (int i = 0; i < tables.Rows.Count-3; i++)
                tablelist.Add(tables.Rows[i+3][2].ToString());

            foreach (var item in tablelist)
                Console.WriteLine(item);

            //string result_tables_list = String.Join(", ", tablelist.ToArray());

            return tablelist;
        }
    }
}
