using System;
using Confluent.Kafka;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace CDC_demo
{
    public static class KafkaProducer
    {
        public static void XmlProduce(XDocument xdoc, string bootstrapServers)
        {
            var conf = new ProducerConfig { BootstrapServers = bootstrapServers };

            Action<DeliveryReport<Null, string>> handler = r =>
                Console.WriteLine(!r.Error.IsError
                    ? $"Delivered message to {r.TopicPartitionOffset}"
                    : $"Delivery Error: {r.Error.Reason}");

            var stream = xdoc;
            using (var p = new ProducerBuilder<Null, string>(conf).Build())
            {
                p.Produce("topic_new", new Message<Null, string> { Value = stream.ToString() }, handler);
                p.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
