using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CDC_demo
{
    public static class CDC_Data
    {
        public static string filedate = DateTime.Now.ToString("MM-dd-yyyy HH.mm.ss");

        public static void GetCapturedColumnsData(SqlConnection connection)
        {
            XDocument xdoc = new XDocument();
            string table_name = "captured_columns";
            string get_data_from_captured_column = $"select * from cdc.{table_name}";
            SqlCommand cmd = new SqlCommand(get_data_from_captured_column, connection);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                XElement captured_columns = new XElement("captured_columns");
                //Console.WriteLine(dr.FieldCount);
                //Console.WriteLine($"{dr.GetName(0)}\t{dr.GetName(1)}\t{dr.GetName(2)}\t{dr.GetName(3)}\t{dr.GetName(4)}\t{dr.GetName(5)}\t");

                while (dr.Read())
                {
                    XElement column_element = new XElement("row");

                    XAttribute column = new XAttribute("object_id", $"{dr[0].ToString()}");
                    XElement column_name = new XElement($"{dr.GetName(1)}", $"{dr[1].ToString()}");
                    XElement column_type = new XElement($"{dr.GetName(2)}", $"{dr[2].ToString()}");
                    XElement column_ordinal = new XElement($"{dr.GetName(3)}", $"{dr[3].ToString()}");
                    XElement is_computed = new XElement($"{dr.GetName(4)}", $"{dr[4].ToString()}");
                    XElement masking_function = new XElement($"{dr.GetName(5)}", $"{dr[5].ToString()}");

                    column_element.Add(column);
                    column_element.Add(column_name);
                    column_element.Add(column_type);
                    column_element.Add(column_ordinal);
                    column_element.Add(is_computed);
                    column_element.Add(masking_function);

                    captured_columns.Add(column_element);

                    //Console.WriteLine($"{dr[0].ToString()}\t{dr[1].ToString()}\t{dr[2].ToString()}\t{dr[3].ToString()}\t{dr[4].ToString()}\t{dr[5].ToString()}\t");
                }
                xdoc.Add(captured_columns);
                xdoc.Save($"{table_name}.{filedate}.xml");
                Console.WriteLine($"{table_name}.xml saved");
            }
        }

        public static void GetChangeTables(SqlConnection connection)
        {
            string table_name = "change_tables";
            string get_data_from_change_tables = $"select * from cdc.{table_name}";
            SqlCommand cmd = new SqlCommand(get_data_from_change_tables, connection);
            XDocument xdoc = new XDocument();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                XElement change_tables = new XElement($"{table_name}");

                while (dr.Read())
                {
                    XElement column_element = new XElement("row");

                    XAttribute column = new XAttribute("object_id", $"{dr[0].ToString()}");
                    XElement version = new XElement($"{dr.GetName(1)}", $"{dr[1].ToString()}");
                    XElement source_object_id = new XElement($"{dr.GetName(2)}", $"{dr[2].ToString()}");
                    XElement capture_instance = new XElement($"{dr.GetName(3)}", $"{dr[3].ToString()}");
                    XElement start_lsn = new XElement($"{dr.GetName(4)}", $"{dr[4].ToString()}");
                    XElement end_lsn = new XElement($"{dr.GetName(5)}", $"{dr[5].ToString()}");
                    XElement supports_net_changes = new XElement($"{dr.GetName(6)}", $"{dr[6].ToString()}");
                    XElement has_drop_pending = new XElement($"{dr.GetName(7)}", $"{dr[7].ToString()}");
                    XElement role_name = new XElement($"{dr.GetName(8)}", $"{dr[8].ToString()}");
                    XElement index_name = new XElement($"{dr.GetName(9)}", $"{dr[9].ToString()}");
                    XElement filegroup_name = new XElement($"{dr.GetName(10)}", $"{dr[10].ToString()}");
                    XElement create_date = new XElement($"{dr.GetName(11)}", $"{dr[11].ToString()}");
                    XElement partition_switch = new XElement($"{dr.GetName(12)}", $"{dr[12].ToString()}");
                    
                    column_element.Add(column);
                    column_element.Add(version);
                    column_element.Add(source_object_id);
                    column_element.Add(capture_instance);
                    column_element.Add(start_lsn);
                    column_element.Add(end_lsn);
                    column_element.Add(supports_net_changes);
                    column_element.Add(has_drop_pending);
                    column_element.Add(role_name);
                    column_element.Add(index_name);
                    column_element.Add(filegroup_name);
                    column_element.Add(create_date);
                    column_element.Add(partition_switch);

                    change_tables.Add(column_element);
                }
                xdoc.Add(change_tables);
                xdoc.Save($"{table_name}.{filedate}.xml");
                Console.WriteLine($"{table_name}.xml saved");
            }
        }

        public static void GetAllDataFromCTTables(SqlConnection connection)
        {
            var dblist = SqlServerContext.GetLocalTableNames(connection);
            string tablename;
            SqlCommand cmd; ;
            XDocument xdoc = new XDocument();

            foreach(var item in dblist)
            {
                tablename = item;
                string query = $"select * from cdc.{tablename}";
                cmd = new SqlCommand(query, connection);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    XElement change_tables = new XElement($"{tablename}");
                    
                    while (dr.Read())
                    {
                        XElement column_element = new XElement("row");

                        XAttribute column = new XAttribute("start_lsn", $"{dr[0].ToString()}");
                        XElement version = new XElement("end_lsn", $"{dr[1].ToString()}");
                        XElement source_object_id = new XElement("seqval", $"{dr[2].ToString()}");
                        XElement capture_instance = new XElement("operation", $"{dr[3].ToString()}");
                        XElement start_lsn = new XElement("update_mask", $"{dr[4].ToString()}");
                        XElement end_lsn = new XElement("id", $"{dr[5].ToString()}");

                        column_element.Add(column);
                        column_element.Add(version);
                        column_element.Add(source_object_id);
                        column_element.Add(capture_instance);
                        column_element.Add(start_lsn);
                        column_element.Add(end_lsn);

                        change_tables.Add(column_element);
                    }
                    xdoc.Add(change_tables);
                    xdoc.Save($"{tablename}.{filedate}.xml");
                    Console.WriteLine($"{tablename}.xml saved");
                }
            }
        }
    }
}
