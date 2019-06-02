using System;
using Confluent.Kafka;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CDC_producer
{
    public static class KafkaProducer
    {
        public static void KafkaProduce()
        {
            var conf = new ProducerConfig { BootstrapServers = "localhost:9092" };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                    ? $"Delivered message to {r.TopicPartitionOffset}"
                    : $"Delivery Error: {r.Error.Reason}");

            //string FILE_PATH = "source\repos";
            var f = @"data/captured_columns.05-31-2019 14.10.00.xml";
            StreamReader objxml = new StreamReader(f);
            var file = objxml.ReadToEnd();
            
            Console.WriteLine(file);

            using (var p = new ProducerBuilder<Null, string>(conf).Build())
            {
                p.Produce("topic_new", new Message<Null, string> { Value = file.ToString() }, handler);

                // wait for up to 10 seconds for any inflight messages to be delivered.
                p.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
