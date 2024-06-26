using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using ms_spa.Api.Contract.Usuario;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Classes;


namespace ms_spa.Test.Domain.Services.Classes
{
    public class UsuarioServiceTest
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTest()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            var configuration = new Mock<IConfiguration>();
            var tokenService = new TokenService(configuration.Object);
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mapperMock.Object, tokenService);
        }


        [Fact]
        public async Task Adicionar_DeveAdicionarUsuarioCorretamente()
        {
            // Arrange
            var request = new UsuarioRequestContract
            {
                Email = "leoaguiar@dsn.pack",
                Senha = "123",
                Perfil = "Admin"
            };
            var usuarioModel = new Usuario
            {
                Email = "leoaguiar@dsn.pack",
                Senha = GenerateHash("123"),
                Perfil = "Admin",
                DataCadastro = DateTime.Now
            };
            var usuarioResponse = new UsuarioResponseContract
            {
                Id = 1,
                Perfil = "Admin",
                DataCadastro = usuarioModel.DataCadastro
            };

            _mapperMock.Setup(m => m.Map<Usuario>(request)).Returns(usuarioModel);
            _usuarioRepositoryMock.Setup(r => r.Adicionar(usuarioModel)).ReturnsAsync(usuarioModel);
            _mapperMock.Setup(m => m.Map<UsuarioResponseContract>(usuarioModel)).Returns(usuarioResponse);

            // Act
            var result = await _usuarioService.Adicionar(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioResponse, result);
            _usuarioRepositoryMock.Verify(r => r.Adicionar(usuarioModel), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var request = new UsuarioRequestContract
            {
                Email = "leoaguiar.dsn.pack",
                Senha = "123",
                Perfil = "Admin"
            };
            var usuarioModel = new Usuario
            {
                Id = id,
                Email = "leoaguiar.dsn.pack",
                Senha = GenerateHash("123"),
                Perfil = "Admin",
                DataCadastro = DateTime.Now
            };
            var usuarioResponse = new UsuarioResponseContract
            {
                Id = id,
                Perfil = "Admin",
                DataCadastro = usuarioModel.DataCadastro
            };

            _mapperMock.Setup(m => m.Map<Usuario>(request)).Returns(usuarioModel);
            _usuarioRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(usuarioModel);
            _usuarioRepositoryMock.Setup(r => r.Atualizar(usuarioModel)).ReturnsAsync(usuarioModel);
            _mapperMock.Setup(m => m.Map<UsuarioResponseContract>(usuarioModel)).Returns(usuarioResponse);

            // Act
            var result = await _usuarioService.Atualizar(id, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioResponse, result);
            _usuarioRepositoryMock.Verify(r => r.Atualizar(usuarioModel), Times.Once);
        }

        [Fact]
        public async Task Inativar_DeveInativarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var usuarioModel = new Usuario { Id = id, Email = "leoaguiar.dsn.pack", Senha = "123", Perfil = "Admin", DataCadastro = DateTime.Now };

            _usuarioRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(usuarioModel);

            // Act
            await _usuarioService.Inativar(id);

            // Assert
            _usuarioRepositoryMock.Verify(r => r.Deletar(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var usuarioModel = new Usuario
            {
                Id = id,
                Email = "leoaguiar.dsn.pack",
                Perfil = "Admin",
                DataCadastro = DateTime.Now
            };
            var usuarioResponse = new UsuarioResponseContract
            {
                Id = id,
                Perfil = "Admin",
                DataCadastro = usuarioModel.DataCadastro
            };

            _usuarioRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(usuarioModel);
            _mapperMock.Setup(m => m.Map<UsuarioResponseContract>(usuarioModel)).Returns(usuarioResponse);

            // Act
            var result = await _usuarioService.ObterPorId(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioResponse, result);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarUsuariosCorretamente()
        {
            // Arrange
            var usuarioModels = new List<Usuario>
            {
                new() {
                    Id = 1, Email = "leo@teste",
                    Perfil = "Admin",
                    DataCadastro = DateTime.Now
                },
                new() {
                    Id = 2,
                    Email = "leoaguiar.dsn.pack",
                    Perfil = "User",
                    DataCadastro = DateTime.Now
                }
            };
            var usuarioResponses = new List<UsuarioResponseContract>
            {
                new() {
                    Id = 1, Perfil = "Admin",
                    DataCadastro = usuarioModels[0].DataCadastro
                },
                    new() {
                    Id = 2, Perfil = "User",
                    DataCadastro = usuarioModels[1].DataCadastro
                }
            };

            _usuarioRepositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(usuarioModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<UsuarioResponseContract>>(It.IsAny<IEnumerable<Usuario>>())).Returns(usuarioResponses);

            // Act
            var result = await _usuarioService.ObterTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioResponses.Count, result.Count());
        }



        private static string GenerateHash(string senha)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha));
            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}