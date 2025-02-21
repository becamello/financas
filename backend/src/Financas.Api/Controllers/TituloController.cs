
using Financas.Api.Contract.Titulos;
using Financas.Api.Domain.Services.Interfaces;
using Financas.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financas.Api.Controllers
{
    [ApiController]
    [Route("titulos")]
    public class TituloController : BaseController
    {
        private readonly IService<TituloRequestContract, TituloResponseContract, long> _titulosService;

        private long _idUsuario;

        public TituloController(IService<TituloRequestContract, TituloResponseContract, long> titulosService)
        {
            _titulosService = titulosService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(TituloRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();

                var resultado = await _titulosService.Adicionar(contrato, _idUsuario);

                return Created("", resultado);
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

                var titulos = await _titulosService.Obter(_idUsuario);

                return Ok(titulos);
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

                var resultado = await _titulosService.Obter(id, _idUsuario);

                return Ok(resultado);
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (UnauthorizedException ex)  
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, TituloRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();

                var resultado = await _titulosService.Atualizar(id, contrato, _idUsuario);

                return Ok(resultado);
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

                await _titulosService.Inativar(id, _idUsuario);

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