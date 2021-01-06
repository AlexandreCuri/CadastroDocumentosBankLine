using CadastroDocumentosBankLine.ApplicationService;
using CadastroDocumentosBankLine.Domain.IServices;
using CadastroDocumentosBankLine.Domain.Requests;
using CadastroDocumentosBankLine.Infra.Bus.Producers;
using MassTransit;
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
        
        public DocumentosController(ICadastroDocumentosService cadastroDocumentosService)
        {
            _cadastroDocumentosService = cadastroDocumentosService;            
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
            var result = await _cadastroDocumentosService.CadastrarDocumentos(cadastroDocumentosRequest);           

            if (result.IsFailure)
                return BadRequest(result.Error);

            var model = result;

            return Ok(result);
        }
    }
}
