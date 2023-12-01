using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;

namespace Eclipseworks.Api.AutoMapper;

public class HistoricoMapper : Profile
{
    public HistoricoMapper()
    {
        CreateMap<Historico, HistoricoModel>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(o => o.Usuario.Nome))
            .ForMember(dest => dest.TituloDaTarefa, opt => opt.MapFrom(o => o.Tarefa.Titulo));

    }
}