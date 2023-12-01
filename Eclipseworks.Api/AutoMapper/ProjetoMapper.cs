using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;

namespace Eclipseworks.Api.AutoMapper;

public class ProjetoMapper : Profile
{
    public ProjetoMapper()
    {
        CreateMap<Projeto, ProjetoModel>()
            .ReverseMap();
    }
}