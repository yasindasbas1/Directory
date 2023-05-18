using System.Text;
using Directory.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Directory.Report.Services
{
    public class ReportConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = RabbitMqConsts.RabbitMqHostName,
                };

                using var connection = factory.CreateConnection();
                {
                    using var channel = connection.CreateModel();
                    {
                        channel.QueueDeclare(
                            queue: RabbitMqConsts.RabbitMqQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );
                        var vConsumer = new EventingBasicConsumer(channel);

                        vConsumer.Received += (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            //Kuyrukta bir istek varsa yapılacak işlem
                        };

                        channel.BasicConsume(
                            queue: RabbitMqConsts.RabbitMqQueue,
                            autoAck: true,
                            exclusive: false,
                            consumer: vConsumer
                        );
                        channel.BasicQos(0, 1, false);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
