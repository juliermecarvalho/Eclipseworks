using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;

namespace Eclipseworks.Api.AutoMapper
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<Usuario, UsuarioModel>()
                .ReverseMap();
        }
    }
}
