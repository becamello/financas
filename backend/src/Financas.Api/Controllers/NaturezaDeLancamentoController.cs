using Financas.Api.Contract.NaturezaDeLancamento;
using Financas.Api.Domain.Services.Interfaces;
using Financas.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Api.Controllers
{
    [ApiController]
    [Route("naturezasdelancamento")]
    public class NaturezaDeLancamentoController : BaseController
    {
        private readonly IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long> _naturezaDeLancamentoService;

        private long _idUsuario;

        public NaturezaDeLancamentoController(IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long> naturezaDeLancamentoService)
        {
            _naturezaDeLancamentoService = naturezaDeLancamentoService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(NaturezaDeLancamentoRequestContract contrato)
        {
            try
            {
                long idUsuarioLogado = ObterIdUsuarioLogado();
                return Created("", await _naturezaDeLancamentoService.Adicionar(contrato, idUsuarioLogado));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Obter(_idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Obter(id, _idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, NaturezaDeLancamentoRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Atualizar(id, contrato, _idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _naturezaDeLancamentoService.Inativar(id, _idUsuario);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}