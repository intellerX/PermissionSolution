using Confluent.Kafka;
using Newtonsoft.Json;
using Permission.Domain.Entities;
using Permission.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Permission.Infrastructure.Adapters
{
    public class PermissionKafkaRepository : IPermissionKafkaRepository
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;
        private readonly ILogger<PermissionKafkaRepository> _logger;

        public PermissionKafkaRepository(IConfiguration configuration, ILogger<PermissionKafkaRepository> logger)
        {
            _logger = logger;
            var bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];

            if (string.IsNullOrEmpty(bootstrapServers) || string.IsNullOrEmpty(_topic))
            {
                _logger.LogError("Kafka configuration is missing.");
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

                _logger.LogInformation("Message delivered to {TopicPartitionOffset}", deliveryReport.TopicPartitionOffset);
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, "Kafka produce error: {ErrorReason}", ex.Error.Reason);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while producing message: {ErrorMessage}", ex.Message);
            }
        }
    }
}
