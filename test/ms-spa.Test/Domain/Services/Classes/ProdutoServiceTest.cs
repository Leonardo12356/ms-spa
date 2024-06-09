using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using ms_spa.Api.Contract.Produto;
using ms_spa.Api.Contract.Usuario;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Classes;


namespace ms_spa.Test.Domain.Services.Classes
{
    public class ProdutoServiceTest
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTest()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _mapperMock = new Mock<IMapper>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task Adicionar_DeveAdicionarUsuarioCorretamente()
        {
            // Arrange
            var request = new ProdutoRequestContract
            {
                Nome = "Produto ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto ",

            };
            var produtoModel = new Produto
            {
                Nome = "Produto ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto ",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };
            var produtoResponse = new ProdutoResponseContract
            {
                Id = 1,
                Nome = "Produto ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto",
                DataCadastro = produtoModel.DataCadastro,
                ClienteId = 1
            };

            _mapperMock.Setup(m => m.Map<Produto>(request)).Returns(produtoModel);
            _produtoRepositoryMock.Setup(r => r.Adicionar(produtoModel)).ReturnsAsync(produtoModel);
            _mapperMock.Setup(m => m.Map<ProdutoResponseContract>(produtoModel)).Returns(produtoResponse);


            // Act
            var result = await _produtoService.Adicionar(request, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoResponse, result);
            _produtoRepositoryMock.Verify(r => r.Adicionar(produtoModel), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var request = new ProdutoRequestContract
            {
                Nome = "Produto",
                QuantidadeEstoque = 20,
                ValorCusto = 55.0,
                ValorVenda = 80.0,
                Observacao = "Produto",

            };
            var produtoModel = new Produto
            {
                Id = id,
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };
            var produtoResponse = new ProdutoResponseContract
            {
                Id = id,
                Nome = "Produto",
                QuantidadeEstoque = 20,
                ValorCusto = 55.0,
                ValorVenda = 80.0,
                Observacao = "Produto",
                DataCadastro = produtoModel.DataCadastro,
                ClienteId = 1
            };

            _mapperMock.Setup(m => m.Map<Produto>(request)).Returns(produtoModel);
            _produtoRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoModel);
            _produtoRepositoryMock.Setup(r => r.Atualizar(produtoModel)).ReturnsAsync(produtoModel);
            _mapperMock.Setup(m => m.Map<ProdutoResponseContract>(produtoModel)).Returns(produtoResponse);

            // Act
            var result = await _produtoService.Atualizar(id, request, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoResponse, result);
            _produtoRepositoryMock.Verify(r => r.Atualizar(produtoModel), Times.Once);
        }

        [Fact]
        public async Task Inativar_DeveInativarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var produtoModel = new Produto
            {
                Id = id,
                Nome = "Produto ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };

            _produtoRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoModel);

            // Act
            await _produtoService.Inativar(id, 1);

            // Assert
            _produtoRepositoryMock.Verify(r => r.Deletar(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var produtoModel = new Produto
            {
                Id = id,
                Nome = "Produto ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };
            var produtoResponse = new ProdutoResponseContract
            {
                Id = id,
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "Produto",
                DataCadastro = produtoModel.DataCadastro,
                ClienteId = 1
            };

            _produtoRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoModel);
            _mapperMock.Setup(m => m.Map<ProdutoResponseContract>(produtoModel)).Returns(produtoResponse);

            // Act
            var result = await _produtoService.ObterPorId(id, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoResponse, result);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarUsuariosCorretamente()
        {
            // Arrange
            var produtoModels = new List<Produto>
            {
                new() {
                    Id = 1,
                    Nome = "Produto 1",
                    QuantidadeEstoque = 10,
                    ValorCusto = 50.0,
                    ValorVenda = 75.0,
                    Observacao = "Produto",
                    DataCadastro = DateTime.Now,
                    ClienteId = 1
                },
                new() {
                    Id = 2,
                    Nome = "Produto",
                    QuantidadeEstoque = 5,
                    ValorCusto = 30.0,
                    ValorVenda = 50.0,
                    Observacao = "Produto 2",
                    DataCadastro = DateTime.Now,
                    ClienteId = 2
                }
            };
            var produtoResponses = new List<ProdutoResponseContract>
            {
                new() {
                    Id = 1,
                    Nome = "Produto 1",
                    QuantidadeEstoque = 10,
                    ValorCusto = 50.0,
                    ValorVenda = 75.0,
                    Observacao = "Produto 1",
                    DataCadastro = produtoModels[0].DataCadastro,
                    ClienteId = 1
                },
                new() {
                    Id = 2,
                    Nome = "Produto 2",
                    QuantidadeEstoque = 5,
                    ValorCusto = 30.0,
                    ValorVenda = 50.0,
                    Observacao = "Produto 2",
                    DataCadastro = produtoModels[1].DataCadastro,
                    ClienteId = 2
                }
            };

            _produtoRepositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(produtoModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutoResponseContract>>(It.IsAny<IEnumerable<Produto>>())).Returns(produtoResponses);

            // Act
            var result = await _produtoService.ObterTodos(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoResponses.Count, result.Count());
        }
    }
}
