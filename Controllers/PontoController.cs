using ForPonto.Model;
using ForPonto.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForPonto.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PontoController : ControllerBase
    {
        private readonly ILogger<PontoController> _logger;
        private readonly PontoService _pontoService;

        public PontoController(ILogger<PontoController> logger, PontoService pontoService)
        {
            _logger = logger;
            _pontoService = pontoService;
        }
        

        [HttpPost(template: "AdicionarPonto")]
        public async Task<IActionResult> AdicionaPonto([FromBody] PontoDto pontoDto)
        {

            var result = await _pontoService.AdicionaPonto(pontoDto);
            return Ok(result);
        }
    }
}
