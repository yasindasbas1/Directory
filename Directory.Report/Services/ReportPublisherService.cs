using RabbitMQ.Client;
using Serilog;
using System.Text;
using Directory.Core;
using Directory.Data;

namespace Directory.Report.Services
{
    public class ReportPublisherService
    {
        private readonly ReportContextDb _db;
        public int _reportId = 0;

        public ReportPublisherService(ReportContextDb db)
        {
            _db = db;
        }

        public async Task<Result> CreateReport()
        {
            try
            {
                var vAddRequestReportResult = await AddReport();

                if (vAddRequestReportResult.Failed)
                    return Result.PrepareFailure(vAddRequestReportResult.Message);

                var vAddToQueueResult = await AddToQueue();

                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService CreateReport error");
                return Result.PrepareFailure("Rapor oluşturma işlemi başarısız");
            }
        }

        private async Task<Result> AddToQueue()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = RabbitMqConsts.RabbitMqHostName,
                    UserName = RabbitMqConsts.UserName,
                    Password = RabbitMqConsts.Password
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: RabbitMqConsts.RabbitMqQueue,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        );

                        string message = _reportId.ToString();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: RabbitMqConsts.RabbitMqExchangeName,
                            routingKey: RabbitMqConsts.RabbitMqRoutingKey,
                            basicProperties: null,
                            body: body);

                        channel.Close();
                        connection.Close();
                    }

                }

                return Result.PrepareSuccess();
            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService AddToQueue error");
                return Result.PrepareFailure("İstek kuyruğa eklenemedi");
            }
        }

        private async Task<Result> AddReport()
        {
            try
            {
                var newReport = new Data.Entities.Report
                {
                    RequestTime = DateTime.Now,
                    Status = ReportStatuses.Waiting
                };

                _db.Reports.Add(newReport);

                await _db.SaveChangesAsync();

                _reportId = newReport.Id;

                return Result.PrepareSuccess();

            }
            catch (Exception vEx)
            {
                Log.Error(vEx, "ReportService AddReport error");
                return Result.PrepareFailure("Rapor talebi oluşturulamadı");
            }
        }
    }
}
