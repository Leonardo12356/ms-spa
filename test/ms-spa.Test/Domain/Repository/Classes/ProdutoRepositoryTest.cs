using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Classes;

namespace ms_spa.Test.Domain.Repository.Classes
{
    public class ProdutoRepositoryTest
    {
        [Fact]
        public async Task Adicionar_DeveAdicionarProdutoComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "spa-db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ProdutoRepository(context);
            var produto = new Produto
            {
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "sabão 5kg",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };

            // Act
            var result = await repository.Adicionar(produto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Nome, result.Nome);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarProdutoComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "atualizar_Produto_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ProdutoRepository(context);
            var produto = new Produto
            {
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "sabão 5kg",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };

            await repository.Adicionar(produto);
            produto.QuantidadeEstoque = 20;

            // Act
            var result = await repository.Atualizar(produto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(20, result.QuantidadeEstoque);
        }

        [Fact]
        public async Task Deletar_DeveDeletarProdutoComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "deletar_Produto_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ProdutoRepository(context);
            var produto = new Produto
            {
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "sabão 5kg",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };

            await repository.Adicionar(produto);

            // Act
            await repository.Deletar(produto);

            // Assert
            var result = await context.Produtos.FindAsync(produto.Id);
            Assert.Null(result);
        }


        [Fact]
        public async Task ObterPorId_DeveRetornarProdutoCorreto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_Produto_id_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ProdutoRepository(context);
            var produto = new Produto
            {
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "sabão 5kg",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            };

            await repository.Adicionar(produto);

            // Act
            var result = await repository.ObterPorId(produto.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Id, result.Id);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarTodosProdutos()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_todos_Produtos_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ProdutoRepository(context);
            await repository.Adicionar(new Produto
            {
                Nome = "Produto",
                QuantidadeEstoque = 10,
                ValorCusto = 50.0,
                ValorVenda = 75.0,
                Observacao = "sabão 5kg",
                DataCadastro = DateTime.Now,
                ClienteId = 1
            });
            await repository.Adicionar(new Produto
            {
                Nome = "Guitarra",
                QuantidadeEstoque = 5,
                ValorCusto = 450,
                ValorVenda = 400,
                Observacao = "Guitarra Esp LTD",
                DataCadastro = DateTime.Now,
                ClienteId = 2
            });

            // Act
            var result = await repository.ObterTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}