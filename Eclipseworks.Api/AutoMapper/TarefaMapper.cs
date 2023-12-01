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

public class ComentarioMapper : Profile
{
    public ComentarioMapper()
    {
        CreateMap<Comentario, ComentarioModel>()
            .ReverseMap();
    }
}