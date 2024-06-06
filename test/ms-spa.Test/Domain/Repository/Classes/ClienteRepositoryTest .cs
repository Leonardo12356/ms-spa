using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Classes;

namespace ms_spa.Test.Domain.Repository.Classes
{
    public class ClienteRepositoryTest
    {
        [Fact]
        public async Task Adicionar_DeveAdicionarClienteComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "spa-db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ClienteRepository(context);
            var cliente = new Cliente
            {
                Nome = "Cliente1",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            // Act
            var result = await repository.Adicionar(cliente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Nome, result.Nome);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarClienteComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "atualizar_Cliente_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ClienteRepository(context);
            var cliente = new Cliente
            {
                Nome = "Cliente1",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            await repository.Adicionar(cliente);
            cliente.Telefone = "2742";

            // Act
            var result = await repository.Atualizar(cliente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Telefone, result.Telefone);
        }

        [Fact]
        public async Task Deletar_DeveDeletarClienteComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "deletar_Cliente_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ClienteRepository(context);
            var cliente = new Cliente
            {
                Nome = "Cliente1",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            await repository.Adicionar(cliente);

            // Act
            await repository.Deletar(cliente);

            // Assert
            var result = await context.Clientes.FindAsync(cliente.Id);
            Assert.Null(result);
        }


        [Fact]
        public async Task ObterPorId_DeveRetornarClienteCorreto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_Cliente_id_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ClienteRepository(context);
            var cliente = new Cliente
            {
                Nome = "Cliente1",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            await repository.Adicionar(cliente);

            // Act
            var result = await repository.ObterPorId(cliente.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cliente.Id, result.Id);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarTodosClientes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_todos_Clientes_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new ClienteRepository(context);
            await repository.Adicionar(new Cliente
            {
                Nome = "Cliente1",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            });
            await repository.Adicionar(new Cliente
            {
                Nome = "Cliente1",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            });

            // Act
            var result = await repository.ObterTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}