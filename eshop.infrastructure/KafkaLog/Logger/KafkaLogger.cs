using Confluent.Kafka;
using eshop.infrastructure.KafkaLog.LogModel;
using System;
using System.Reflection;
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

        public async Task LogAsync(BaseLogMessage msg)
        {
            var result = await _producer.Value.ProduceAsync(Assembly.GetExecutingAssembly().GetName().Name, new Message<Null, string> { Value = msg.Serialize() });
            Console.WriteLine(result.Message.Value);
        }
    }
}
