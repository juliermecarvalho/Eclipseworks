using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;

namespace Eclipseworks.Api.AutoMapper;

public class TarefaMapper : Profile
{
    public TarefaMapper()
    {
        CreateMap<Tarefa, TarefaModel>()
            .ReverseMap();
    }
}