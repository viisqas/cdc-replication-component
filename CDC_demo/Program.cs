using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CDC_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = SqlConnector.GetDBConnection();
            string db = connection.Database;
            string server = connection.DataSource;
            Console.WriteLine($"db: {db}\ndatasource: {server}");


            CDC_Data.GetAllDataFromCTTables(connection);
            //CDC_Data.GetCapturedColumnsData(connection);

            //CDCData.GetCapturedColumnsData(connection);
            //CDCData.GetChangeTables(connection);

            //string get_sys_tables = "select name from sys.tables";

            //CDC_producer.KafkaProducer.KafkaProduce();

            connection.Close();
            Console.Read();
        }
    }
}
