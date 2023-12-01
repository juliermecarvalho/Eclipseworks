using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;

namespace Eclipseworks.Api.AutoMapper;

public class HistoricoMapper : Profile
{
    public HistoricoMapper()
    {
        CreateMap<Historico, HistoricoModel>();

    }
}