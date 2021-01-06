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

        public DocumentosController(ICadastroDocumentosService cadastroDocumentosService)
        {
            _cadastroDocumentosService = cadastroDocumentosService;
        }

        [HttpPost(Name = nameof(CadastrarDocumentos))]
        [ApiVersion("1.0")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CadastrarDocumentos(CadastroDocumentosRequest cadastroDocumentosRequest)
        {
            var result = await _cadastroDocumentosService.CadastarDocumentos(cadastroDocumentosRequest);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var model = result;

            return Ok(result);
        }
    }
}
