using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Infra.Bus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Infra.Bus.Producers
{
    public class ProcessoProducer : IProcessoProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProcessoProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result> Publish(ProcessoDocumento processoDocumentos)
        {
            var documento = new ObtidoProcesso()
            {
                Documentos = processoDocumentos.Documentos
            };

            _publishEndpoint.Publish(documento);

            return Result.Ok();
        }
    }
}
