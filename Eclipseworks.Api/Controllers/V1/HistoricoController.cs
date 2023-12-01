using AutoMapper;
using Eclipseworks.Api.Models;
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

    [HttpGet("listar")]
    public async Task<ActionResult<IList<HistoricoModel>>> List()
    {
        var entidades = await _repositoryHistorico.ListAsync();
        return _mapper.Map<List<HistoricoModel>>(entidades);
    }

   
}