using CadastroDocumentosBankLine.Domain.IServices;
using CadastroDocumentosBankLine.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CadastroDocumentosBankLine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly ICadastroDocumentosService _cadastroDocumentosService;
        private readonly IDocumentosFilaEnviadorService _documentoFilaEnviadorService;

        public DocumentosController(ICadastroDocumentosService cadastroDocumentosService, IDocumentosFilaEnviadorService documentoFilaEnviadorService)
        {
            _cadastroDocumentosService = cadastroDocumentosService;
            _documentoFilaEnviadorService = documentoFilaEnviadorService;
        }

        /// <summary>
        /// Cadastra documentos no formato "Base64String"
        /// </summary>
        /// <param name="cadastroDocumentosRequest"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CadastrarDocumentos))]
        [ApiVersion("1.0")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CadastrarDocumentos(CadastroDocumentosRequest cadastroDocumentosRequest)
        {
            try
            {
                var result = await _cadastroDocumentosService.CadastrarDocumentos(cadastroDocumentosRequest);

                if (result.IsFailure)
                    return BadRequest(result.Error);

                var documentosEnviados = await _documentoFilaEnviadorService.EnviarDocumentosParaFila(cadastroDocumentosRequest.ListaDocumentos);

                if (!documentosEnviados)
                    return BadRequest("Documentos não enviados para fila!");

                var model = result;

                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
