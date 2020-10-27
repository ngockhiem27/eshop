using Confluent.Kafka;
using System;
using System.IO;

namespace LogParser.Fetcher
{
    public class Fetcher : IDisposable
    {
        private ConsumerConfig _config;
        private IConsumer<Null, string> _consumer;
        private const string BASE_DIR = "_LogData";
        public Fetcher()
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "LogParser",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(_config).Build();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Fetch(string topic, int timeout = 5000)
        {
            _consumer.Subscribe(topic);
            var stop = false;
            while (!stop)
            {
                var consumeResult = _consumer.Consume(timeout);
                if (consumeResult == null) stop = true;
                else
                {
                    Console.WriteLine(consumeResult.Message.Value);
                    SaveLogToFile(consumeResult.Message.Value);
                }
            }
            _consumer.Close();
        }

        private void SaveLogToFile(string log)
        {
            var fields = log.Split('\t');
            var logType = fields[0];
            var logTime = fields[1];
            var logName = DateTime.ParseExact(logTime, "HH:mm:ss dd-MM-yyyy", null).ToString("dd-MM-yyyy");
            var dirPath = Path.Combine(BASE_DIR, logType);
            var filePath = Path.Combine(dirPath, logName + ".txt");

            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            StreamWriter writer;
            if (!File.Exists(filePath))
            {
                var w = File.Create(filePath);
                writer = new StreamWriter(w);
            }
            else
            {
                writer = File.AppendText(filePath);
            }
            writer.WriteLine(log);
            writer.Close();
        }
    }
}
