using Microsoft.AspNetCore.Mvc;
using Moq;
using ms_spa.Api.Contract;
using ms_spa.Api.Contract.Usuario;
using ms_spa.Api.Controllers;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Test.Controller
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock = new();
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _controller = new UsuarioController(_usuarioServiceMock.Object);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarCreatedResult()
        {
            // Arrange
            _usuarioServiceMock.Setup(service => service.Adicionar(It.IsAny<UsuarioRequestContract>()))
                .ReturnsAsync(new UsuarioResponseContract());

            // Act
            var result = await _controller.Adicionar(new UsuarioRequestContract());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarBadRequestQuandoFalha()
        {
            // Arrange
            var mensagemDeErro = "Mensagem de erro";
            _usuarioServiceMock.Setup(service => service.Adicionar(It.IsAny<UsuarioRequestContract>()))
                               .ThrowsAsync(new BadRequestException(mensagemDeErro));

            // Act
            var result = await _controller.Adicionar(new UsuarioRequestContract());

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
            _usuarioServiceMock.Setup(service => service.Atualizar(It.IsAny<int>(), It.IsAny<UsuarioRequestContract>()))
                               .ReturnsAsync(new UsuarioResponseContract());

            // Act
            var result = await _controller.Atualizar(1, new UsuarioRequestContract());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarNotFoundQuandoDespesaNaoEncontrada()
        {
            // Arrange
            var despesaId = 1;
            var mensagemDeErro = "Despesa não encontrada";
            _usuarioServiceMock.Setup(service => service.Atualizar(despesaId, It.IsAny<UsuarioRequestContract>()))
                               .ThrowsAsync(new NotFoundException(mensagemDeErro));

            // Act
            var result = await _controller.Atualizar(despesaId, new UsuarioRequestContract());

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
            var despesaId = 1;
            var despesaResponse = new UsuarioResponseContract();
            _usuarioServiceMock.Setup(service => service.ObterPorId(despesaId))
                               .ReturnsAsync(despesaResponse);

            // Act
            var result = await _controller.ObterPorId(despesaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<UsuarioResponseContract>(okResult.Value);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFoundQuandoDespesaNaoEncontrada()
        {
            // Arrange
            var despesaId = 1;
            var mensagemDeErro = "Despesa não encontrada";
            _usuarioServiceMock.Setup(service => service.ObterPorId(despesaId))
                               .ThrowsAsync(new NotFoundException(mensagemDeErro));

            // Act
            var result = await _controller.ObterPorId(despesaId);

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
            var usuariosResponse = new List<UsuarioResponseContract>();
            _usuarioServiceMock.Setup(service => service.ObterTodos())
                               .ReturnsAsync(usuariosResponse);

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            var responseBody = Assert.IsType<List<UsuarioResponseContract>>(okResult.Value);
            Assert.Empty(responseBody);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarProblemaQuandoFalha()
        {
            // Arrange
            _usuarioServiceMock.Setup(service => service.ObterTodos())
                               .ThrowsAsync(new Exception("Erro ao obter usuários"));

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
            var usuarioId = 1;
            _usuarioServiceMock.Setup(service => service.Inativar(usuarioId))
                               .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Deletar(usuarioId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Deletar_DeveRetornarNotFoundQuandoUsuarioNaoEncontrado()
        {
            // Arrange
            var usuarioId = 1;
            var mensagemDeErro = "Usuário não encontrado";
            _usuarioServiceMock.Setup(service => service.Inativar(usuarioId))
                               .ThrowsAsync(new NotFoundException(mensagemDeErro));

            // Act
            var result = await _controller.Deletar(usuarioId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var modelError = Assert.IsType<ModelErrorContract>(notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Not Found", modelError.Title);
            Assert.Equal(mensagemDeErro, modelError.Message);
        }


    }
}