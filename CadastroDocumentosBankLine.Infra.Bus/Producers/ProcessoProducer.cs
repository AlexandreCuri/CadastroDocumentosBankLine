using CadastroDocumentosBankLine.Domain.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Infra.Bus.Producers
{
    public class ProcessoProducer : IProcessoProducer
    {
        private readonly IConfiguration _configuration;

        public ProcessoProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result> Publish(ProcessoDocumento processoDocumentos)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetSection("RabbitMQConfigurations").GetSection("RabbitMqUri").Value),
                UserName = _configuration.GetSection("RabbitMQConfigurations").GetSection("UserNameMq").Value,
                Password = _configuration.GetSection("RabbitMQConfigurations").GetSection("PasswordMq").Value
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _configuration.GetSection("RabbitMQConfigurations").GetSection("DocumentosQueue").Value,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(processoDocumentos.Documentos));

                    channel.BasicPublish(exchange: _configuration.GetSection("RabbitMQConfigurations").GetSection("ExchangeName").Value,
                                         routingKey: "",
                                         basicProperties: null,
                                         body: body);


                    return Result.Ok();
                }
            }
        }
    }
}
