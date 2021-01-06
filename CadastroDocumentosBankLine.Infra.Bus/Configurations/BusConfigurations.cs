using CadastroDocumentosBankLine.Infra.Bus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDocumentosBankLine.Infra.Bus.Configurations
{
    public class BusConfiguration : IBusConfiguration
    {
        private readonly IRabbitMqSettings _settings;

        public BusConfiguration(IRabbitMqSettings settings)
        {
            _settings = settings;
        }

        public IBusControl CreateBus()
        {
            var bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                //cfg.Host(new Uri(_settings.RabbitMqUri), hst =>
                //{
                //    hst.Username(_settings.UserName);
                //    hst.Password(_settings.Password);
                //});

                cfg.Host(new Uri(_settings.RabbitMqUri));
                cfg.PrefetchCount = 16;
                cfg.Durable = true;
                cfg.UseJsonSerializer();

                cfg.Publish<ObtidoProcesso>(x => x.BindQueue($"{typeof(ObtidoProcesso).Namespace}:{nameof(ObtidoProcesso)}",
                                                                       _settings.EnviarDocumentosServiceQueue));
            });

            return bus;
        }
    }
}
