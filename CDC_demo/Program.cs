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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
 
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            
			var connection = SqlConnector.GetDBConnection(config["ConnectionString"]);
            
            string db = connection.Database;
            string server = connection.DataSource;
            Console.WriteLine($"db: {db}\ndatasource: {server}");

            var stream = XmlSerializer.ConvertDataToXml(connection);
            KafkaProducer.XmlProduce(stream, config["BootstrapServers"]);
            
            connection.Close();
            Console.Read();
        }
    }
}
