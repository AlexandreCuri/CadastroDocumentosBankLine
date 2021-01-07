using CadastroDocumentosBankLine.Domain.Entities;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Infra.Bus.Producers
{
    public interface IProcessoProducer
    {
        Task<Result> Publish(ProcessoDocumento processoDocumentos);
    }
}
