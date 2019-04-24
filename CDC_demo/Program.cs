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
            //DB connect
            SqlConnection conn = DBSQLServerUtils.GetDBConnection();
            //Get data
            string sqlExpression = "Select * from cdc.dbo_PersonalInfo_CT";
            XmlDocument doc = new XmlDocument();
            string filename = "XmlTest.xml";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, conn);

            DataSet ds = new DataSet("PersonalInfo");
            adapter.Fill(ds);

            DataTable dt = ds.Tables[0];
            
            conn.Close();

            foreach (DataColumn column in dt.Columns)
                Console.Write("\t{0}", column.ColumnName);
            Console.WriteLine();

            foreach(DataRow row in dt.Rows)
            {
                var cells = row.ItemArray;
                foreach (object cell in cells)
                    Console.Write("\t{0}", cell);
                    
                Console.WriteLine();
            }

            ds.WriteXml(filename);
           

            Console.Read();
        }
    }
}
