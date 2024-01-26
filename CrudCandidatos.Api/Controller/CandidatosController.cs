using CrudCandidatos.Application.Interfaces;
using CrudCandidatos.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;

namespace CrudCandidatosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatosController : ControllerBase
    {
        private readonly ICandidatoService _CandidatoService;

        public CandidatosController(ICandidatoService CandidatoService)
        {
            _CandidatoService = CandidatoService ?? throw new ArgumentNullException(nameof(CandidatoService));
        }

        [HttpGet("ListarCandidatos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Candidato>))]
        public async Task<IActionResult> ListarCandidatos()
        {
            

            var Candidatos = await _CandidatoService.ListarCandidatosAsync();
            return Ok(Candidatos);
        }


        [HttpGet("ObterCandidatoPorId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Candidato))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterCandidatoPorId(string id)
        {
            var Candidato = await _CandidatoService.ObterCandidatoPorIdAsync(id);

            if (Candidato == null)
            {
                return NotFound("Candidato não encontrado.");
            }

            return Ok(Candidato);
        }
        [HttpGet("verificacpfexiste/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> VerificarCpfExiste(string cpf)
        {
            var cpfExiste = await _CandidatoService.VerificarCpfExisteAsync(cpf);

            return Ok(cpfExiste);
        }



        [HttpPost("CriarCandidato")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Candidato))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarCandidato([FromBody] Candidato Candidato)
        {
            try
            {
                var novoCandidatoId = await _CandidatoService.CriarCandidatoAsync(Candidato);
                return CreatedAtAction(nameof(ObterCandidatoPorId), new { id = novoCandidatoId }, Candidato);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("AtualizarCandidato/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarCandidato(string id, [FromBody] Candidato Candidato)
        {
            try
            {
                var candidatoatualizado = await _CandidatoService.AtualizarCandidatoAsync(id, Candidato);
                return Ok(candidatoatualizado); 
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("BuscarCandidatosPorNome")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Candidato>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarCandidatosPorNome(string nome)
        {
            var Candidatos = await _CandidatoService.BuscarCandidatosPorNomeAsync(nome);

            if (Candidatos == null || !Candidatos.Any())
            {
                return NotFound("Nenhum Candidato encontrado com o nome especificado.");
            }

            return Ok(Candidatos);
        }

        [HttpDelete("DeletarCandidato/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarCandidato(string id)
        {
            try
            {
                await _CandidatoService.DeletarCandidatoAsync(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
