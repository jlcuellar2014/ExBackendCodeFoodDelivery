using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SubscriberDeliveryVehicle.Model;
using System.Text;
using System.Text.Json;

namespace SubscriberDeliveryVehicle.Subscripter
{
    internal class RabbitMqSubscripter : IMsgSubscripter
    {
        private static readonly string url = "amqps://kvlgfauz:X99FTgiZSQlr6Hf441gv4eRouTwjOYY4@shrimp.rmq.cloudamqp.com/kvlgfauz";
        private static readonly string queue = "delivery-vehicle-moved";
        private bool disposed = false;

        public void Dispose()
        {
            disposed = false;
        }

        public void ReciveDeliveryVehicheMovedEvent()
        {
            var factory = new ConnectionFactory() { Uri = new Uri(url) };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                while (!disposed)
                {
                    Console.Write(".");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, args) =>
                    {
                        var body = args.Body.ToArray();
                        var messageString = Encoding.UTF8.GetString(body);

                        DeliveryVehicleMoved? message = JsonSerializer.Deserialize<DeliveryVehicleMoved>(messageString);

                        if (message != null)
                            Console.WriteLine($"\n{message.PlainFormat()}");
                    };

                    channel.BasicConsume(
                            queue: "delivery-vehicle-moved",
                            autoAck: true,
                            consumer: consumer
                        );

                    Thread.Sleep(10000);
                }
            }
        }
    }
}
