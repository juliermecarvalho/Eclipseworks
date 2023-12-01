using System.Linq.Expressions;
using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Eclipseworks.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/historico")]
public class HistoricoController : ControllerBase
{
    private readonly IRepositoryHistorico _repositoryHistorico;
    private readonly IMapper _mapper;

    public HistoricoController(
        IRepositoryHistorico repositoryHistorico,
        IMapper mapper)
    {
        _repositoryHistorico = repositoryHistorico;
        _mapper = mapper;
    }

    [HttpGet("listar/{tarefaId:long}")]
    public async Task<ActionResult<IList<HistoricoModel>>> List([FromRoute] long tarefaId)
    {
        var entidades = await _repositoryHistorico.ListAsync(filter: h => h.TarefaId == tarefaId);
        return _mapper.Map<List<HistoricoModel>>(entidades);
    }

   
}