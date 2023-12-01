using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Api.Validation.ValidateAttribute;
using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Eclipseworks.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/projeto")]
public class ProjetoController : ControllerBase
{
    private readonly IRepositoryProjeto _repositoryProjeto;
    private readonly IMapper _mapper;

    public ProjetoController(
        IRepositoryProjeto repositoryProjeto,
        IMapper mapper)
    {
        _repositoryProjeto = repositoryProjeto;
        _mapper = mapper;
    }

    /// <summary>
    /// Listagem de Projetos - listar todos os projetos do usuário
    /// </summary>
    /// <returns></returns>
    [HttpGet("listar/{usuarioId:long}")]
    public async Task<ActionResult<IList<ProjetoModel>>> List([FromRoute] long usuarioId)
    {
        var entidades = await _repositoryProjeto.ListAsync(filter: p => p.CriadorDoProjetoId == usuarioId );
        return _mapper.Map<List<ProjetoModel>>(entidades);
    }
    

    [HttpGet("{id:int}")]
    [IdMaiorQueZero]
    public async Task<ActionResult<ProjetoModel>> Get([FromRoute] int id)
    {
        var entidade = await _repositoryProjeto.GetAsync(id);
        return _mapper.Map<ProjetoModel>(entidade);
    }

    /// <summary>
    /// Criação de Projetos - criar um novo projeto
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateModel(typeof(ProjetoModel))]
    public async Task<ActionResult<ProjetoModel>> Post([FromBody] ProjetoModel model)
    {
        var entidade = _mapper.Map<Projeto>(model);
        await _repositoryProjeto.SaveAsync(entidade);
        await _repositoryProjeto.CommitAsync();
        return _mapper.Map<ProjetoModel>(entidade);
    }

    /// <summary>
    /// Um projeto não pode ser removido se ainda houver tarefas pendentes associadas a ele.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [IdMaiorQueZero]
    public async Task<ActionResult> Delete(int id)
    {
        var entidade = await _repositoryProjeto.GetAsync(id, includes: p => p.Tarefas );
        if (entidade.Tarefas.Any(t => t.Status == Status.Pendente))
        {
            return BadRequest("Não é possível excluir um projeto que possui tarefas pendentes. Primeiro conclua todas as tarefas.");
        }
        await _repositoryProjeto.DeleteAsync(id);
        await _repositoryProjeto.CommitAsync();
        return Ok();
    }
}