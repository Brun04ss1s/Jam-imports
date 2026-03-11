using Confluent.Kafka;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace JamImports.Api.Services
{
    public class KafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendAsyncMessage(string topic, object message)
        {
            var messageJson = JsonSerializer.Serialize(message);

            var kafkaMessage = new Message<Null, string> { Value = messageJson };

            await _producer.ProduceAsync(topic, kafkaMessage);

            Console.WriteLine($"Mensagem enviada para o Kafka no tópico {topic}: {messageJson}");
        }
    }
}