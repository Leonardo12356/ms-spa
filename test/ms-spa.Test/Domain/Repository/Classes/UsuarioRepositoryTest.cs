using Microsoft.EntityFrameworkCore;
using ms_spa.Api.Data;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Classes;

namespace ms_spa.Test.Domain.Repository.Classes
{
    public class UsuarioRepositoryTest
    {
        [Fact]
        public async Task Adicionar_DeveAdicionarUsuarioComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "spa-db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new UsuarioRepository(context);
            var usuario = new Usuario { Email = "leoaguiar.dsn.pack", Senha = "senha123", Perfil = "Admin" };

            // Act
            var result = await repository.Adicionar(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuario.Email, result.Email);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarUsuarioComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "atualizar_usuario_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new UsuarioRepository(context);
            var usuario = new Usuario { Email = "leoaguiar.dsn.pack", Senha = "senha123", Perfil = "Admin" };

            await repository.Adicionar(usuario);
            usuario.Perfil = "User";

            // Act
            var result = await repository.Atualizar(usuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User", result.Perfil);
        }

        [Fact]
        public async Task Deletar_DeveDeletarUsuarioComSucesso()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "deletar_usuario_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new UsuarioRepository(context);
            var usuario = new Usuario { Email = "leoaguiar.dsn.pack", Senha = "senha123", Perfil = "Admin" };

            await repository.Adicionar(usuario);

            // Act
            await repository.Deletar(usuario);

            // Assert
            var result = await context.Usuarios.FindAsync(usuario.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task Obter_DeveRetornarUsuarioPorEmail()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_usuario_email_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new UsuarioRepository(context);
            var usuario = new Usuario { Email = "leoaguiar.dsn.pack", Senha = "senha123", Perfil = "Admin" };

            await repository.Adicionar(usuario);

            // Act
            var result = await repository.Obter("leoaguiar.dsn.pack");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuario.Email, result.Email);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarUsuarioCorreto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_usuario_id_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new UsuarioRepository(context);
            var usuario = new Usuario { Email = "leoaguiar.dsn.pack", Senha = "senha123", Perfil = "Admin" };

            await repository.Adicionar(usuario);

            // Act
            var result = await repository.ObterPorId(usuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuario.Id, result.Id);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarTodosUsuarios()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "obter_todos_usuarios_db")
                .Options;

            var context = new AppDbContext(options);
            var repository = new UsuarioRepository(context);
            await repository.Adicionar(new Usuario { Email = "leoaguiar.dsn.pack", Senha = "senha123", Perfil = "Admin" });
            await repository.Adicionar(new Usuario { Email = "leo.dsn.pack", Senha = "senha123", Perfil = "User" });

            // Act
            var result = await repository.ObterTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}