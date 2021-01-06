using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Domain.IServices;
using CadastroDocumentosBankLine.Domain.Requests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.ApplicationService
{
    public class CadastroDocumentosService : ICadastroDocumentosService
    {
        private readonly IConfiguration _configuration;

        public CadastroDocumentosService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result> CadastarDocumentos(CadastroDocumentosRequest cadastroDocumentosRequest)
        {
            var path = Path.Combine(_configuration.GetSection(WebHostDefaults.ContentRootKey).Value , _configuration.GetSection("DiretorioArquivoDocumentos").Value.ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                foreach (var documento in cadastroDocumentosRequest.ListaDocumentos)
                {

                    byte[] arquivo = Convert.FromBase64String(documento);

                    using (MemoryStream memoryStream = new MemoryStream(arquivo))
                    {
                        var imagem = Image.FromStream(memoryStream);
                        
                        imagem.Save(Path.Combine(path, $"{Guid.NewGuid()}.jpg"));
                    }

                }

                return Result.Ok("Arquivos gravados com sucesso!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
