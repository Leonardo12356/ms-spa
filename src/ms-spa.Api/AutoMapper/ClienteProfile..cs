using AutoMapper;
using ms_spa.Api.Contract.Cliente;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.AutoMapper
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteRequestContract>().ReverseMap();
            CreateMap<Cliente, ClienteResponseContract>().ReverseMap();
        }
    }
}