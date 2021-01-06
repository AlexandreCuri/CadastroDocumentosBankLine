using CadastroDocumentosBankLine.Domain.Entities;
using CadastroDocumentosBankLine.Domain.IServices;
using CadastroDocumentosBankLine.Domain.Requests;
using CadastroDocumentosBankLine.Infra.Bus.Producers;
using MassTransit;
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
        private readonly IProcessoProducer _processoProducer;


        public CadastroDocumentosService(IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _configuration = configuration;
            _processoProducer = new ProcessoProducer(publishEndpoint);
        }

        public async Task<Result> CadastrarDocumentos(CadastroDocumentosRequest cadastroDocumentosRequest)
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

                var documentoFilaEnviador = new DocumentosFilaEnviadorService(_processoProducer);
                var documentoEnviado = documentoFilaEnviador.EnviarDocumentosParaFila(cadastroDocumentosRequest.ListaDocumentos);

                return Result.Ok("Arquivos gravados com sucesso!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
