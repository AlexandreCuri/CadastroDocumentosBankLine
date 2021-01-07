using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Domain.IServices
{
    public interface IDocumentosFilaEnviadorService
    {
        Task<bool> EnviarDocumentosParaFila(List<string> documentos);
    }
}
