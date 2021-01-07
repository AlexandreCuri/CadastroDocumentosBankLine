using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Domain.IServices;
using CadastroDocumentosBankLine.Infra.Bus.Producers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.ApplicationService
{
    public class DocumentosFilaEnviadorService : IDocumentosFilaEnviadorService
    {
        private readonly IProcessoProducer _processoProducer;

        public DocumentosFilaEnviadorService(IProcessoProducer processoProducer)
        {
            _processoProducer = processoProducer;
        }

        public async Task<bool> EnviarDocumentosParaFila(List<string> documentos)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
