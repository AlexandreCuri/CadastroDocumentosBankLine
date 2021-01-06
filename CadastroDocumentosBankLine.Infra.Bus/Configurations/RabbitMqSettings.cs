using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDocumentosBankLine.Infra.Bus.Configurations
{
    public class RabbitMqSettings : IRabbitMqSettings
    {
        public string RabbitMqUri { get; }
        public string UserName { get; }
        public string Password { get; }
        public string EnviarDocumentosServiceQueue { get; }

        private readonly IConfiguration _configuration;

        public RabbitMqSettings(IConfiguration configuration)
        {
            configuration = _configuration;
            
            RabbitMqUri = configuration.GetSection("RabbitMQConfigurations").GetSection("RabbitMqUri").Value;
            //UserName = configuration.GetSection("RabbitMQConfigurations").GetSection("UserNameMq").Value;
            //Password = configuration.GetSection("RabbitMQConfigurations").GetSection("PasswordMq").Value;
            EnviarDocumentosServiceQueue = configuration.GetSection("RabbitMQConfigurations").GetSection("DocumentosQueue").Value;
        }
    }
}
