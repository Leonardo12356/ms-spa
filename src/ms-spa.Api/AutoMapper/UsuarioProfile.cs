using AutoMapper;
using ms_spa.Api.Contract.Usuario;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioLoginRequestContract>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginResponseContract>().ReverseMap();
        }
    }
}