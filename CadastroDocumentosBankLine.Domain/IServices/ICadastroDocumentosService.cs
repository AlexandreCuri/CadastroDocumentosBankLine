using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.Domain.IServices
{
    public interface ICadastroDocumentosService
    {
        Task<Result> CadastrarDocumentos(CadastroDocumentosRequest cadastroDocumentosRequest);
    }
}
