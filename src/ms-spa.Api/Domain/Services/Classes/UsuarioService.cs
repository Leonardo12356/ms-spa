using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ms_spa.Api.Contract.Usuario;
using ms_spa.Api.Domain.Models;
using ms_spa.Api.Domain.Repository.Interfaces;
using ms_spa.Api.Domain.Services.Interfaces;
using ms_spa.Api.Exceptions;

namespace ms_spa.Api.Domain.Services.Classes
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, TokenService tokenService) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IMapper _mapper = mapper;
        private readonly TokenService _tokenService = tokenService;

        public async Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest)
        {
            UsuarioResponseContract usuario = await ObterEmail(usuarioLoginRequest.Email);
            var hashSenha = GerarHashSenha(usuarioLoginRequest.Senha);
            if (usuario is null || usuario.Senha != hashSenha)
            {
                throw new AuthenticationException($"Usuario ou senha iválida.");
            }

            return new UsuarioLoginResponseContract
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenService.GerarToken(_mapper.Map<Usuario>(usuario))
            };
        }

        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract entidade, int idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);

        }

        public async Task<UsuarioResponseContract> Atualizar(int id, UsuarioRequestContract entidade, int idUsuario)
        {
            _ = await ObterTodos(id) ?? throw new NotFoundException("Usuário não encontrado para atualização.");

            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.Id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);

            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);

        }

        public async Task Inativar(int id, int idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(id) ?? throw new NotFoundException("Usuário não encontrado para inativação.");
            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));
        }

        public async Task<IEnumerable<UsuarioResponseContract>> ObterTodos(int idUsuario)
        {
            var usuarios = await _usuarioRepository.ObterTodos();
            return usuarios.Select(_mapper.Map<UsuarioResponseContract>);
        }

        public async Task<UsuarioResponseContract> ObterPorId(int id, int idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task<UsuarioResponseContract> ObterEmail(string email)
        {
            var usuario = await _usuarioRepository.Obter(email);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        private static string GerarHashSenha(string senha)
        {
            string hashSenha;
            byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
            byte[] byteHashSenha = SHA256.HashData(bytesSenha);
            hashSenha = BitConverter.ToString(byteHashSenha).Replace("-", "").Replace("-", "").ToLower();

            return hashSenha;
        }
    }
}