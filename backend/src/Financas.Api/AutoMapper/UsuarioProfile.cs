

using AutoMapper;
using Financas.Api.Contract.Usuario;
using Financas.Api.Domain.Models;

namespace Financas.Api.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRequestContract>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseContract>().ReverseMap();
        }
    }
}