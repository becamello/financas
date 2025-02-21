
using AutoMapper;
using Financas.Api.Contract.Titulos;
using Financas.Api.Domain.Models;

namespace Financas.Api.AutoMapper
{
    public class TituloProfile : Profile
    {
        public TituloProfile()
        {
            CreateMap<Titulo, TituloRequestContract>().ReverseMap();
            CreateMap<Titulo, TituloResponseContract>().ReverseMap();
        }
    }
}