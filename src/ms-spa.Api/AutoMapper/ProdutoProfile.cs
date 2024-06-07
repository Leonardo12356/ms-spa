using AutoMapper;
using ms_spa.Api.Contract.Produto;
using ms_spa.Api.Domain.Models;

namespace ms_spa.Api.AutoMapper
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoRequestContract>().ReverseMap();
            CreateMap<Produto, ProdutoResponseContract>().ReverseMap();
        }
    }
}