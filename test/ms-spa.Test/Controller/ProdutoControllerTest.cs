using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ms_spa.Api.Contract;
using ms_spa.Api.Contract.Produto;
using ms_spa.Api.Controllers;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;


namespace ms_spa.Test.Controller
{
    public class ProdutoControllerTest
    {
        private readonly Mock<IProdutoService> _produtoServiceMock = new();
        private readonly ProdutoController _controller;

        public ProdutoControllerTest()
        {
            _controller = new ProdutoController(_produtoServiceMock.Object);

            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, "1")
            };
            var identity = new ClaimsIdentity(userClaims, "TestAuthType");
            var ProdutoPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = ProdutoPrincipal }
            };
        }

        [Fact]
        public async Task Adicionar_DeveRetornarCreatedResult()
        {
            // Arrange
            _produtoServiceMock.Setup(service => service.Adicionar(It.IsAny<ProdutoRequestContract>(), It.IsAny<int>()))
                .ReturnsAsync(new ProdutoResponseContract());

            // Act
            var result = await _controller.Adicionar(new ProdutoRequestContract());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarBadRequestQuandoFalha()
        {
            // Arrange
            var mensagemDeErro = "Mensagem de erro";
            _produtoServiceMock.Setup(service => service.Adicionar(It.IsAny<ProdutoRequestContract>(), It.IsAny<int>()))
                               .ThrowsAsync(new BadRequestException(mensagemDeErro));

            // Act
            var result = await _controller.Adicionar(new ProdutoRequestContract());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal(400, badRequestResult?.StatusCode);
            var modelError = badRequestResult?.Value as ModelErrorContract;
            Assert.NotNull(modelError);
            Assert.Equal("Bad Request", modelError.Title);
            Assert.Equal(mensagemDeErro, modelError.Message);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarOkResult()
        {
            // Arrange
            _produtoServiceMock.Setup(service => service.Atualizar(It.IsAny<int>(), It.IsAny<ProdutoRequestContract>(), It.IsAny<int>()))
                               .ReturnsAsync(new ProdutoResponseContract());

            // Act
            var result = await _controller.Atualizar(1, new ProdutoRequestContract());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFoundQuandoProdutoNaoEncontrado()
        {
            // Arrange
            var ProdutoId = 1;
            var mensagemDeErro = "Produto não encontrado";
            _produtoServiceMock.Setup(service => service.Atualizar(ProdutoId, It.IsAny<ProdutoRequestContract>(), It.IsAny<int>()))
                               .ThrowsAsync(new NotFoundException(mensagemDeErro));

            // Act
            var result = await _controller.Atualizar(ProdutoId, new ProdutoRequestContract());

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var modelError = Assert.IsType<ModelErrorContract>(notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Not Found", modelError.Title);
            Assert.Equal(mensagemDeErro, modelError.Message);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOkResult()
        {
            // Arrange
            var ProdutoId = 1;
            var ProdutoResponse = new ProdutoResponseContract();
            _produtoServiceMock.Setup(service => service.ObterPorId(ProdutoId, It.IsAny<int>()))
                               .ReturnsAsync(ProdutoResponse);

            // Act
            var result = await _controller.ObterPorId(ProdutoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<ProdutoResponseContract>(okResult.Value);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFoundQuandoProdutoNaoEncontrado()
        {
            // Arrange
            var ProdutoId = 1;
            var mensagemDeErro = "Produto não encontrado";
            _produtoServiceMock.Setup(service => service.ObterPorId(ProdutoId, It.IsAny<int>()))
                               .ThrowsAsync(new NotFoundException(mensagemDeErro));

            // Act
            var result = await _controller.ObterPorId(ProdutoId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var modelError = Assert.IsType<ModelErrorContract>(notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Not Found", modelError.Title);
            Assert.Equal(mensagemDeErro, modelError.Message);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkResult()
        {
            // Arrange
            var ProdutosResponse = new List<ProdutoResponseContract>();
            _produtoServiceMock.Setup(service => service.ObterTodos(It.IsAny<int>()))
                               .ReturnsAsync(ProdutosResponse);

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            var responseBody = Assert.IsType<List<ProdutoResponseContract>>(okResult.Value);
            Assert.Empty(responseBody);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarProblemaQuandoFalha()
        {
            // Arrange
            _produtoServiceMock.Setup(service => service.ObterTodos(It.IsAny<int>()))
                               .ThrowsAsync(new Exception("Erro ao obter Produtos"));

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNoContent()
        {
            // Arrange
            var ProdutoId = 1;
            _produtoServiceMock.Setup(service => service.Inativar(ProdutoId, It.IsAny<int>()))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Deletar(ProdutoId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNotFoundQuandoProdutoNaoEncontrado()
        {
            // Arrange
            var ProdutoId = 1;
            var mensagemDeErro = "Produto não encontrado";
            _produtoServiceMock.Setup(service => service.Inativar(ProdutoId, It.IsAny<int>()))
                               .ThrowsAsync(new NotFoundException(mensagemDeErro));

            // Act
            var result = await _controller.Deletar(ProdutoId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var modelError = Assert.IsType<ModelErrorContract>(notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Not Found", modelError.Title);
            Assert.Equal(mensagemDeErro, modelError.Message);
        }
    }
}
