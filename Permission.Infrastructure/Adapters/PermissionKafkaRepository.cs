using Confluent.Kafka;
using Newtonsoft.Json;
using Permission.Domain.Entities;
using Permission.Domain.Ports;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Permission.Infrastructure.Adapters
{
    public class PermissionKafkaRepository : IPermissionKafkaRepository
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public PermissionKafkaRepository(IConfiguration configuration)
        {
            var bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];

            if (string.IsNullOrEmpty(bootstrapServers) || string.IsNullOrEmpty(_topic))
            {
                throw new InvalidOperationException("Kafka configuration is missing.");
            }

            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task PublishOperationAsync(PermissionOperationMessageEntity message)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(message);
                var deliveryReport = await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = jsonMessage });

                Console.WriteLine($"Message delivered to {deliveryReport.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine($"Kafka produce error: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while producing message: {ex.Message}");
            }
        }
    }
}
