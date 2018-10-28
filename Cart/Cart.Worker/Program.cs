namespace Cart.Worker
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using StackExchange.Redis;
    using System;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            string rabbitmqUrl = Environment.GetEnvironmentVariable("RABBITMQ_CLOUD_URL");
            factory.Uri = new Uri(rabbitmqUrl);

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_CACHE_CONNECTION_STRING"));

            IDatabase _database = redis.GetDatabase();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "order.procesed", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var buyerId = Encoding.UTF8.GetString(body);

                    _database.KeyDeleteAsync(buyerId);

                    Console.WriteLine(" [x] Received {0} and deleted cache!", buyerId);
                };
                channel.BasicConsume(queue: "order.procesed", autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
