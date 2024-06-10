using AutoMapper;
using Moq;
using ms_spa.Api.Contract.Produto;
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

            };

            _mapperMock.Setup(m => m.Map<Produto>(request)).Returns(produtoModel);
            _produtoRepositoryMock.Setup(r => r.Adicionar(produtoModel)).ReturnsAsync(produtoModel);
            _mapperMock.Setup(m => m.Map<ProdutoResponseContract>(produtoModel)).Returns(produtoResponse);


            // Act
            var result = await _produtoService.Adicionar(request);

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

            };

            _mapperMock.Setup(m => m.Map<Produto>(request)).Returns(produtoModel);
            _produtoRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoModel);
            _produtoRepositoryMock.Setup(r => r.Atualizar(produtoModel)).ReturnsAsync(produtoModel);
            _mapperMock.Setup(m => m.Map<ProdutoResponseContract>(produtoModel)).Returns(produtoResponse);

            // Act
            var result = await _produtoService.Atualizar(id, request);

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
            };

            _produtoRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoModel);

            // Act
            await _produtoService.Inativar(id);

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
            };

            _produtoRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoModel);
            _mapperMock.Setup(m => m.Map<ProdutoResponseContract>(produtoModel)).Returns(produtoResponse);

            // Act
            var result = await _produtoService.ObterPorId(id);

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

                },
                new() {
                    Id = 2,
                    Nome = "PS5",
                    QuantidadeEstoque = 5,
                    ValorCusto = 4.000,
                    ValorVenda = 5.000,
                    Observacao = "PS5 Fat",
                    DataCadastro = DateTime.Now,
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

                },
                new() {
                    Id = 2,
                    Nome = "PS5",
                    QuantidadeEstoque = 5,
                    ValorCusto = 4.000,
                    ValorVenda = 5.000,
                    Observacao = "PS5 Fat",
                    DataCadastro = DateTime.Now,
                }
            };

            _produtoRepositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(produtoModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutoResponseContract>>(It.IsAny<IEnumerable<Produto>>())).Returns(produtoResponses);

            // Act
            var result = await _produtoService.ObterTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produtoResponses.Count, result.Count());
        }

        [Fact]
        public async Task ObterProdutosComMaiorEstoque_DeveRetornarProdutosOrdenadosPorQuantidade()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new() { Id = 1, Nome = "Guitarra", QuantidadeEstoque = 50 },
                new() { Id = 2, Nome = "PS5", QuantidadeEstoque = 100 },
                new() { Id = 3, Nome = "PS5 Slim", QuantidadeEstoque = 75 },
            };
            _produtoRepositoryMock.Setup(repo => repo.ObterTodos()).ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.ObterProdutosComMaiorEstoque(2);

            // Assert
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal(2, resultList[0].Id);
            Assert.Equal(3, resultList[1].Id);
        }

        [Fact]
        public async Task ObterProdutosComEstoqueZeradoOuNegativo_DeveRetornarProdutosComEstoqueZeroOuNegativo()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new() { Id = 1, Nome = "Guitarra", QuantidadeEstoque = 0 },
                new() { Id = 2, Nome = "PS5", QuantidadeEstoque = -10 },
                new() { Id = 3, Nome = "PS5 Slim", QuantidadeEstoque = 5 },
            };
            _produtoRepositoryMock.Setup(repo => repo.ObterTodos()).ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.ObterProdutosComEstoqueZeradoOuNegativo();

            // Assert
            Assert.NotNull(result);
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Contains(resultList, p => p.Id == 1);
            Assert.Contains(resultList, p => p.Id == 2);
        }

        [Fact]
        public async Task ObterQuantidadeTotalDeProdutos_DeveRetornarQuantidadeTotalDeProdutos()
        {
            // Arrange
            var produtos = new List<Produto>

            {
                new() { Id = 1, Nome = "Guitarra", QuantidadeEstoque = 50 },
                new() { Id = 2, Nome = "PS5", QuantidadeEstoque = 100 },
                new() { Id = 3, Nome = "PS5 Slim", QuantidadeEstoque = 75 },
            };

            _produtoRepositoryMock.Setup(repo => repo.ObterTodos()).ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.ObterQuantidadeTotalDeProdutos();

            // Assert
            Assert.Equal(3, result);
        }
    }
}

