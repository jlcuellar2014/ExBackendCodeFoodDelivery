using FoodDeliveryAPI.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FoodDeliveryAPI.Publishers
{
    public class RabbitMqPublisher : IMsgPublisher
    {
        private readonly IConfiguration configuration;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void PublishDeliveryVehicheMovedEvent(DeliveryVehicleMovedDto vehicleMovedDto)
        {
            var urlCloudAMQP = configuration.GetConnectionString("CloudAMQP") ?? string.Empty;

            // conection to RabbitMQ
            var factory = new ConnectionFactory() { Uri = new Uri(urlCloudAMQP) };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "delivery-vehicle-moved", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Building the message
                var message = JsonSerializer.Serialize(vehicleMovedDto);
                var body = Encoding.UTF8.GetBytes(message);

                // Posting the message to the queue
                channel.BasicPublish(exchange: "", routingKey: "delivery-vehicle-moved", basicProperties: null, body: body);
            }
        }
    }
}
