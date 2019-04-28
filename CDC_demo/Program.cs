using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Data;
using System.Xml;

namespace CDC_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializeTable("captured_columns");
            SerializeTable("change_tables");

            Console.Read();
        }

        public static void SerializeTable(string table)
        {
            SqlConnection conn = DBSQLServerUtils.GetDBConnection();
            
            string sqlExpression = $"Select * from cdc.{table}";
            XmlDocument doc = new XmlDocument();
            string filedate = DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss");
            string filename = $"{table}.{filedate}.xml";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, conn);

            DataSet ds = new DataSet($"{table}");
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            ds.WriteXml(filename);
            Console.WriteLine("table is serialized");
            conn.Close();
        }
    }
}
