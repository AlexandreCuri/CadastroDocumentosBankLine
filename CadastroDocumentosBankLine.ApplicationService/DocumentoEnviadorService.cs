using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Infra.Bus.Producers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.ApplicationService
{
    public class DocumentosFilaEnviadorService
    {
        private readonly IProcessoProducer _processoProducer;

        public DocumentosFilaEnviadorService(IProcessoProducer processoProducer)
        {
            _processoProducer = processoProducer;
        }

        public bool EnviarDocumentosParaFila(List<string> documentos)
        {
            Task<Result> result = _processoProducer.Publish(new ProcessoDocumento()
            {
                Documentos = documentos
            });

            if (result.Result.IsFailure)
            {
                return false;
            }

            return true;
        }
    }
}
