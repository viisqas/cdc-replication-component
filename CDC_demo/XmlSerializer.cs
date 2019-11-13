using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CDC_demo
{
    public static class XmlSerializer
    {
        private static readonly string filedate = DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss");
        private static XDocument xdoc = new XDocument();

        public static XDocument ConvertDataToXml(SqlConnection connection)
        {
            List<string> dblist = SqlServerContext.GetLocalTableNames(connection);

            foreach (string tablename in dblist)
            {
                List<string> columns = SqlServerContext.GetColumnNames(connection, tablename);
                private readonly string query = $"select sys.fn_cdc_map_lsn_to_time(__$start_lsn), sys.fn_cdc_map_lsn_to_time(__$end_lsn), __$operation, {columns[0]}, {columns[1]}, {columns[2]}, {columns[3]}, {columns[4]}, {columns[5]}, {columns[6]} from cdc.{tablename}";
                SqlCommand cmd = new SqlCommand(query, connection);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    XElement table = new XElement($"cdc.{tablename}");
                    while (dr.Read())
                    {
                        XElement column_el = new XElement("row");

                        XAttribute start_lsn = new XAttribute("start_lsn", $"{dr[0].ToString()}");
                        column_el.Add(start_lsn);
                        XElement end_lsn = new XElement("end_lsn", $"{dr[1].ToString()}");
                        column_el.Add(end_lsn);
                        XElement operation = new XElement("operation", $"{GetOperation((int)dr[2]).ToString()}");
                        column_el.Add(operation);
                        
                        for (int i = 0; i < columns.Count(); i++)
                        {
                            XElement el = new XElement($"{columns[i]}", $"{dr[i+3].ToString()}");
                            column_el.Add(el);
                        }
                        table.Add(column_el);
                    }
                    xdoc.Add(table);
                    xdoc.Save($"{tablename}.{filedate}.xml");
                    Console.WriteLine($"{tablename}.xml created");
                }
            }
            return xdoc;
        }

        public static string GetOperation(int caseSwitch)
        {
            string op = null;
            
			switch (caseSwitch)
            {
                case 1:
                    return op = "delete";
                case 2:
                    return op = "insert";
                case 3:
                    return op = "update_old";
                case 4:
                    return op = "update_new";
            }
            return op;
        }
    }
}
