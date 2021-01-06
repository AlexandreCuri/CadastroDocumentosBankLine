using CadastroDocumentosBankLine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Infra.Bus.Producers
{
    public interface IProcessoProducer
    {
        Task<Result> Publish(ProcessoDocumento processoDocumentos);
    }
}
