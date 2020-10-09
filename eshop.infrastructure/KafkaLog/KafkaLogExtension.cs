using Confluent.Kafka;
using eshop.infrastructure.KafkaLog.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eshop.infrastructure.KafkaLog
{
    public static class KafkaLogExtension
    {
        public static void AddKafkaLog(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(new ProducerConfig { BootstrapServers = Configuration.GetConnectionString("Kafka") });
            services.AddSingleton<IKafkaLogger, KafkaLogger>();
        }
    }
}
