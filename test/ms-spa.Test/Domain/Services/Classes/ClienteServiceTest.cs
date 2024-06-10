using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using ms_spa.Api.Contract.Cliente;
using ms_spa.Api.Contract.Usuario;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Classes;


namespace ms_spa.Test.Domain.Services.Classes
{
    public class ClienteServiceTest
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ClienteService _clienteService;

        public ClienteServiceTest()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _mapperMock = new Mock<IMapper>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object, _mapperMock.Object);
        }


        [Fact]
        public async Task Adicionar_DeveAdicionarUsuarioCorretamente()
        {
            // Arrange
            var request = new ClienteRequestContract
            {
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",

            };
            var clienteModel = new Cliente
            {
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };
            var ClienteResponse = new ClienteResponseContract
            {

                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            _mapperMock.Setup(m => m.Map<Cliente>(request)).Returns(clienteModel);
            _clienteRepositoryMock.Setup(r => r.Adicionar(clienteModel)).ReturnsAsync(clienteModel);
            _mapperMock.Setup(m => m.Map<ClienteResponseContract>(clienteModel)).Returns(ClienteResponse);


            // Act
            var result = await _clienteService.Adicionar(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClienteResponse, result);
            _clienteRepositoryMock.Verify(r => r.Adicionar(clienteModel), Times.Once);
        }

        [Fact]
        public async Task Atualizar_DeveAtualizarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var request = new ClienteRequestContract
            {
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
            };
            var ClienteModel = new Cliente
            {
                Id = id,
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };
            var clienteResponse = new ClienteResponseContract
            {
                Id = id,
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            _mapperMock.Setup(m => m.Map<Cliente>(request)).Returns(ClienteModel);
            _clienteRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(ClienteModel);
            _clienteRepositoryMock.Setup(r => r.Atualizar(ClienteModel)).ReturnsAsync(ClienteModel);
            _mapperMock.Setup(m => m.Map<ClienteResponseContract>(ClienteModel)).Returns(clienteResponse);

            // Act
            var result = await _clienteService.Atualizar(id, request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clienteResponse, result);
            _clienteRepositoryMock.Verify(r => r.Atualizar(ClienteModel), Times.Once);
        }

        [Fact]
        public async Task Inativar_DeveInativarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var ClienteModel = new Cliente
            {
                Id = id,
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            _clienteRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(ClienteModel);

            // Act
            await _clienteService.Inativar(id);

            // Assert
            _clienteRepositoryMock.Verify(r => r.Deletar(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarUsuarioCorretamente()
        {
            // Arrange
            var id = 1;
            var ClienteModel = new Cliente
            {
                Id = id,
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };
            var ClienteResponse = new ClienteResponseContract
            {
                Id = id,
                Nome = "Cliente",
                Email = "leoaguiar.dsn.pack",
                Cpf = "12345",
                Telefone = "12345",
                Endereco = "rua catablau",
                Observacao = "Cliente",
                UsuarioId = 1
            };

            _clienteRepositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(ClienteModel);
            _mapperMock.Setup(m => m.Map<ClienteResponseContract>(ClienteModel)).Returns(ClienteResponse);

            // Act
            var result = await _clienteService.ObterPorId(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClienteResponse, result);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarUsuariosCorretamente()
        {
            // Arrange
            var clienteModels = new List<Cliente>
            {
                new() {
                    Id = 1,
                    Nome = "Cliente1",
                    Email = "leoaguiar.dsn.pack",
                    Cpf = "12345",
                    Telefone = "12345",
                    Endereco = "rua catablau",
                    Observacao = "Cliente",
                    UsuarioId = 1
                },
                new() {
                     Id = 2,
                    Nome = "Cliente",
                    Email = "leoaguiar.dsn.pack",
                    Cpf = "12345",
                    Telefone = "12345",
                    Endereco = "rua catablau",
                    Observacao = "Cliente",
                    UsuarioId = 1
                }
            };
            var clienteResponses = new List<ClienteResponseContract>
            {
                new() {
                    Id = 3,
                    Nome = "Cliente2",
                    Email = "leoaguiar.dsn.pack",
                    Cpf = "12345",
                    Telefone = "12345",
                    Endereco = "rua catablau",
                    Observacao = "Cliente",
                    UsuarioId = 1
                },
                new() {
                    Id = 4,
                    Nome = "Cliente3",
                    Email = "leoaguiar.dsn.pack",
                    Cpf = "12345",
                    Telefone = "12345",
                    Endereco = "rua catablau",
                    Observacao = "Cliente",
                    UsuarioId = 1
                }
            };

            _clienteRepositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(clienteModels);
            _mapperMock.Setup(m => m.Map<IEnumerable<ClienteResponseContract>>(It.IsAny<IEnumerable<Produto>>())).Returns(clienteResponses);

            // Act
            var result = await _clienteService.ObterTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clienteResponses.Count, result.Count());
        }

        [Fact]
        public async Task ObterQuantidadeTotalDeClientes_DeveRetornarQuantidadeTotalDeClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
        {
            new() { Id = 1, Nome = "Eu" },
            new() { Id = 2, Nome = "Leo" },
            new() { Id = 3, Nome = "Thais" },
        };
            _clienteRepositoryMock.Setup(repo => repo.ObterTodos()).ReturnsAsync(clientes);

            // Act
            var result = await _clienteService.ObterQuantidadeTotalDeClientes();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task ObterQuantidadeTotalDeClientes_DeveRetornarZeroQuandoNaoHaClientes()
        {
            // Arrange
            var clientes = new List<Cliente>();
            _clienteRepositoryMock.Setup(repo => repo.ObterTodos()).ReturnsAsync(clientes);

            // Act
            var result = await _clienteService.ObterQuantidadeTotalDeClientes();

            // Assert
            Assert.Equal(0, result);
        }
    }
}

