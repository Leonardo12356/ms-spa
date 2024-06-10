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
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTest()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _mapperMock.Object, _clienteRepositoryMock.Object);
        }


        [Fact]
        public async Task Adicionar_DeveAdicionarUsuarioCorretamente()
        {
            // Arrange
            var request = new ProdutoRequestContract
            {
                Nome = "Guitarra ",
                QuantidadeEstoque = 10,
                ValorCusto = 5.000,
                ValorVenda = 7.000,
                Observacao = "ESP LTD Gary-Holt",

            };
            var produtoModel = new Produto
            {
                Nome = "Guitarra ",
                QuantidadeEstoque = 10,
                ValorCusto = 5.000,
                ValorVenda = 7.000,
                Observacao = "ESP LTD Gary-Holt",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };
            var produtoResponse = new ProdutoResponseContract
            {
                Id = 1,
                Nome = "Guitarra ",
                QuantidadeEstoque = 10,
                ValorCusto = 5.000,
                ValorVenda = 7.000,
                Observacao = "ESP LTD Gary-Holt",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };

            _mapperMock.Setup(m => m.Map<Produto>(request)).Returns(produtoModel);
            _produtoRepositoryMock.Setup(r => r.Adicionar(produtoModel)).ReturnsAsync(produtoModel);
            _clienteRepositoryMock.Setup(c => c.ObterPorId(1)).ReturnsAsync(new Cliente { Id = 1 });
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
                Nome = "Guitarra ",
                QuantidadeEstoque = 10,
                ValorCusto = 5.000,
                ValorVenda = 7.000,
                Observacao = "ESP LTD Gary-Holt",

            };
            var produtoModel = new Produto
            {
                Nome = "Guitarra ",
                QuantidadeEstoque = 10,
                ValorCusto = 5.000,
                ValorVenda = 7.000,
                Observacao = "ESP LTD Gary-Holt",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };
            var produtoResponse = new ProdutoResponseContract
            {
                Id = 1,
                Nome = "Guitarra ",
                QuantidadeEstoque = 10,
                ValorCusto = 5.000,
                ValorVenda = 7.000,
                Observacao = "ESP LTD Gary-Holt",
                DataCadastro = DateTime.Now,
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
                Nome = "Shampo ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "anti-caspa ",
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
                Nome = "Shampo ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "anti-caspa ",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };
            var produtoResponse = new ProdutoResponseContract
            {
                Id = id,
                Nome = "Shampo ",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "anti-caspa ",
                DataCadastro = DateTime.Now,
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
                    Nome = "P55",
                    QuantidadeEstoque = 10,
                    ValorCusto = 4.000,
                    ValorVenda = 5.000,
                    Observacao = "PS5 Slim",
                    DataCadastro = DateTime.Now,
                    ClienteId = 1
                },
                new() {
                    Id = 2,
                    Nome = "PS5",
                    QuantidadeEstoque = 5,
                    ValorCusto = 4.000,
                    ValorVenda = 5.000,
                    Observacao = "PS5 Fat",
                    DataCadastro = DateTime.Now,
                    ClienteId = 2
                }
            };
            var produtoResponses = new List<ProdutoResponseContract>
            {
                new() {
                    Id = 1,
                    Nome = "P55",
                    QuantidadeEstoque = 10,
                    ValorCusto = 4.000,
                    ValorVenda = 5.000,
                    Observacao = "PS5 Slim",
                    DataCadastro = DateTime.Now,
                    ClienteId = 1
                },
                new() {
                    Id = 2,
                    Nome = "PS5",
                    QuantidadeEstoque = 5,
                    ValorCusto = 4.000,
                    ValorVenda = 5.000,
                    Observacao = "PS5 Fat",
                    DataCadastro = DateTime.Now,
                    ClienteId = 2
                }
            };

            _produtoRepositoryMock.Setup(r => r.ObeterPeloIdUsuario(1)).ReturnsAsync(produtoModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutoResponseContract>>(It.IsAny<IEnumerable<Produto>>())).Returns(produtoResponses);

            // Act
            var result = await _produtoService.ObterTodos(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoResponses.Count, result.Count());
        }
    }
}
