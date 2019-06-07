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

            //var stream = XmlSerializer.ConvertDataToXml(connection);
            //KafkaProducer.XmlProduce(stream);
            XmlSerializer.ConvertDataToXml(connection);
            connection.Close();
            Console.Read();
        }
    }
}
