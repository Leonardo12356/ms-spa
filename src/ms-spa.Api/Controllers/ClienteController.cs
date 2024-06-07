using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_spa.Api.Contract.Cliente;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController(IClienteService clienteService) : BaseController
    {
        private readonly IService<ClienteRequestContract, ClienteResponseContract, int> _clienteService = clienteService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(ClienteRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _clienteService.Adicionar(contrato, _idUsuario));
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
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _clienteService.ObterTodos(_idUsuario));
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _clienteService.ObterPorId(id, _idUsuario));
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
        public async Task<IActionResult> Atualizar(int id, ClienteRequestContract contrato)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _clienteService.Atualizar(id, contrato, _idUsuario));
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
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                await _clienteService.Inativar(id, _idUsuario);
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