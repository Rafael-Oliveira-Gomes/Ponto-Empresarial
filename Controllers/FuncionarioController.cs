using ForPonto.Model;
using ForPonto.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForPonto.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class FuncionarioController : ControllerBase 
    {
        private readonly ILogger<FuncionarioController> _logger;
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController(ILogger<FuncionarioController> logger, FuncionarioService funcionarioService)
        {
            _logger = logger;
            _funcionarioService = funcionarioService;
        }

        [HttpPost(template: "AdicionaFuncionario")]
        public async Task<IActionResult> AdicionaFuncionario([FromBody] FuncionarioDto funcionarioDto)
        {

            var result = await _funcionarioService.AdicionaFuncionario(funcionarioDto);
            return Ok(result);
        }

        [HttpPost(template: "Remover")]
        public async Task<IActionResult> RemoverFuncionario([FromBody] int funcionarioId)
        {
            if (funcionarioId <= 0) { return BadRequest("Id do Funcionario não deve ser menor que 1"); }

            var result = await _funcionarioService.RemoverFuncionario(funcionarioId);
            return Ok(result);
        }

        [HttpGet(template: "ProcurarId")]
        public async Task<IActionResult> GetFuncionario([FromQuery] int funcionarioId)
        {
            if (funcionarioId <= 0) { return BadRequest("Id do funcionário não deve ser menor que 1"); }

            try
            {
                var result = await _funcionarioService.GetFuncionario(funcionarioId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarFuncionarios()
        {
            try
            {
                var result = await _funcionarioService.ListarFuncionario();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet(template: "Horarios")]
        public async Task<IActionResult> GetHorarios([FromQuery] int funcionarioId)
        {
            if (funcionarioId <= 0) { return BadRequest("Id do funcionário não deve ser menor que 1"); }

            try
            {
                var result = await _funcionarioService.GetFuncionario(funcionarioId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
