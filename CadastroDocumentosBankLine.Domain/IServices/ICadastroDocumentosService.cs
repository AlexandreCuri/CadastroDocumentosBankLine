using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Domain.Requests;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Domain.IServices
{
    public interface ICadastroDocumentosService
    {
        Task<Result> CadastrarDocumentos(CadastroDocumentosRequest cadastroDocumentosRequest);
    }
}
