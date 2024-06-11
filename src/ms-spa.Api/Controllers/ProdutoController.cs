using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_spa.Api.Contract.Produto;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Controllers
{
    [ApiController]
    [Route("produto")]
    public class ProdutoController(IProdutoService produtoService) : BaseController
    {
        private readonly IProdutoService _produtoService = produtoService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(ProdutoRequestContract contrato)
        {
            try
            {
                return Created("", await _produtoService.Adicionar(contrato));
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
                return Ok(await _produtoService.ObterTodos());
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
                return Ok(await _produtoService.ObterPorId(id));
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
        public async Task<IActionResult> Atualizar(int id, ProdutoRequestContract contrato)
        {
            try
            {
                return Ok(await _produtoService.Atualizar(id, contrato));
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
                await _produtoService.Inativar(id);
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
        [Route("produtos/maior-estoque")]
        [Authorize]
        public async Task<IActionResult> ObterProdutosComMaiorEstoque(int quantidade = 10)
        {
            try
            {
                return Ok(await _produtoService.ObterProdutosComMaiorEstoque(quantidade));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("produtos/estoque-zerado-negativo")]
        [Authorize]
        public async Task<IActionResult> ObterProdutosComEstoqueZeradoOuNegativo()
        {
            try
            {
                return Ok(await _produtoService.ObterProdutosComEstoqueZeradoOuNegativo());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("produtos/quantidade-total")]
        [Authorize]
        public async Task<IActionResult> ObterQuantidadeTotalDeProdutos()
        {
            try
            {
                return Ok(await _produtoService.ObterQuantidadeTotalDeProdutos());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}