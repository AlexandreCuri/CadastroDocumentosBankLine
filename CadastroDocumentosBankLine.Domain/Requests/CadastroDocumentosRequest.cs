using CadastroDocumentosBankLine.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDocumentosBankLine.Domain.Requests
{
    public class CadastroDocumentosRequest
    {
        public List<string> ListaDocumentos { get; set; }
    }
}
