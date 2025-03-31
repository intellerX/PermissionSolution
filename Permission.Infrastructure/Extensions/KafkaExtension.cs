using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Permission.Infrastructure.Extensions
{
    public static class KafkaExtension
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            var bootstrapServers = configuration["Kafka:BootstrapServers"];
            var topic = configuration["Kafka:Topic"];

            if (string.IsNullOrEmpty(bootstrapServers) || string.IsNullOrEmpty(topic))
            {
                throw new InvalidOperationException("Kafka configuration is missing.");
            }

            logger.LogInformation($"Configuring Kafka with BootstrapServers: {bootstrapServers}, Topic: {topic}");

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                Acks = Acks.All
            };

            services.AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(producerConfig).Build());

            return services;
        }
    }
}
