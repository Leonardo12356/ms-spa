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
        private readonly IClienteService _clienteService = clienteService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(ClienteRequestContract contrato)
        {
            try
            {

                return Created("", await _clienteService.Adicionar(contrato));
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
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                return Ok(await _clienteService.ObterTodos());
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

                return Ok(await _clienteService.ObterPorId(id));
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
                return Ok(await _clienteService.Atualizar(id, contrato));
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
                await _clienteService.Inativar(id);
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

        [HttpGet]
        [Route("clientes/quantidade-total")]
        [Authorize]
        public async Task<IActionResult> ObterQuantidadeTotalDeClientes()
        {
            try
            {
                return Ok(await _clienteService.ObterQuantidadeTotalDeClientes());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}