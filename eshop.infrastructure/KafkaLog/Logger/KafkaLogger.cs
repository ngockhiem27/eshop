using Confluent.Kafka;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eshop.infrastructure.KafkaLog.Logger
{
    public class KafkaLogger : IDisposable, IKafkaLogger
    {
        private readonly Lazy<IProducer<Null, string>> _producer;

        public KafkaLogger(ProducerConfig config)
        {
            _producer = new Lazy<IProducer<Null, string>>(() => new ProducerBuilder<Null, string>(config).Build());
        }

        public void Dispose()
        {
            if (_producer.IsValueCreated) _producer.Value.Dispose();
        }

        public async Task WriteLogAsync(params string[] fields)
        {
            var msg = new StringBuilder();
            for (int i = 0; i < fields.Length; i++)
            {
                msg.Append(fields[i]);
                if (i != fields.Length - 1)
                {
                    msg.Append('\t');
                }
            }
            var topic = Assembly.GetEntryAssembly().GetName().Name;
            var result = await _producer.Value.ProduceAsync(topic, new Message<Null, string> { Value = msg.ToString() });
        }
    }
}
