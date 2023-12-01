using System.Linq.Expressions;
using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Api.Validation.ValidateAttribute;
using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Eclipseworks.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/tarefa")]
public class TarefaController : ControllerBase
{
    private readonly IRepositoryTarefa _repositoryTarefa;
    private readonly IMapper _mapper;

    public TarefaController(
        IRepositoryTarefa repositoryTarefa,
        IMapper mapper)
    {
        _repositoryTarefa = repositoryTarefa;
        _mapper = mapper;
    }

    /// <summary>
    /// Visualiza��o de Tarefas - visualizar todas as tarefas de um projeto espec�fico
    /// </summary>
    /// <param name="projetoId"></param>
    /// <returns></returns>
    [HttpGet("listar/tarefas-do-projeto/{projetoId:long}")]
    public async Task<ActionResult<IList<TarefaModel>>> ListTarefasDoProjeto([FromRoute] long projetoId)
    {
        var entidades = await _repositoryTarefa.ListAsync(filter: t => t.ProjetoId == projetoId, includes: t => t.Comentarios);
        return _mapper.Map<List<TarefaModel>>(entidades);
    }


    /// <summary>
    /// A API deve fornecer endpoints para gerar relat�rios de desempenho, como o n�mero m�dio de tarefas conclu�das por usu�rio nos �ltimos 30 dias.
    /// </summary>
    /// <param name="projetoId"></param>
    /// <returns></returns>
    [HttpGet("listar/relatorios-de-desempenho/{usuarioId:long}")]
    public async Task<ActionResult<IList<TarefaModel>>> GerarRelat�riosDeDesempenho([FromRoute] long usuarioId)
    {
        var dt = DateTime.Now.AddDays(-30);
        var entidades = await _repositoryTarefa.ListAsync(filter: t => t.UsuarioId == usuarioId && t.Status == Status.Concluida && t.DataVencimento > dt);
        return _mapper.Map<List<TarefaModel>>(entidades);
    }




    [HttpGet("{id:long}")]
    [IdMaiorQueZero]
    public async Task<ActionResult<TarefaModel>> Get([FromRoute] int id)
    {
        var entidade = await _repositoryTarefa.GetAsync(id, includes: t => t.Comentarios);
        return _mapper.Map<TarefaModel>(entidade);
    }



    /// <summary>
    /// Cria��o de Tarefas - adicionar uma nova tarefa a um projeto
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateModel(typeof(TarefaModel))]
    public async Task<ActionResult<TarefaModel>> Post([FromBody] TarefaModel model)
    {
        var projetoCount = _repositoryTarefa.ListAsync(filter: t => t.ProjetoId == model.ProjetoId).Result.Count();

        if (projetoCount > 20)
        {
            return BadRequest("Limite m�ximo de 20 tarefas por projeto");
        }

        
        var entidade = _mapper.Map<Tarefa>(model);
        entidade.SetPrioridade((Prioridade)model.Prioridade);
        await _repositoryTarefa.SaveAsync(entidade);
        await _repositoryTarefa.CommitAsync(model.UsuarioId);
        return _mapper.Map<TarefaModel>(entidade);
    }


    /// <summary>
    /// Atualiza��o de Tarefas - atualizar o status ou detalhes de uma tarefa
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id:long}/atualizar-status ")]
    [ValidateModel(typeof(TarefaModel))]
    [IdMaiorQueZero]
    public async Task<ActionResult<TarefaModel>> AtualizarStatus([FromRoute] long id, [FromBody] TarefaUpdateModel model)
    {
        var entidade = await _repositoryTarefa.GetAsync(id);

        if (!string.IsNullOrWhiteSpace(model.Titulo))
        {
            entidade.Titulo = model.Titulo;
        }

        if (!string.IsNullOrWhiteSpace(model.Descricao))
        {
            entidade.Descricao = model.Descricao;
        }
      
        entidade.SetStatus((Status)model.Status);

        await _repositoryTarefa.SaveAsync(entidade);
        await _repositoryTarefa.CommitAsync(model.UsuarioId);
        return _mapper.Map<TarefaModel>(entidade);
    }

    /// <summary>
    /// Remo��o de Tarefas - remover uma tarefa de um projeto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:long}")]
    [IdMaiorQueZero]
    public async Task<ActionResult> Delete([FromRoute]long id, [FromQuery] long usuarioId)
    {

    
        await _repositoryTarefa.DeleteAsync(id);
        await _repositoryTarefa.CommitAsync(usuarioId);
        return Ok();
    }

    /// <summary>
    /// Coment�rios nas Tarefas:
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("{id:long}")]
    [IdMaiorQueZero]
    public async Task<ActionResult<TarefaModel>> Post([FromRoute] long id, [FromQuery] long usuarioId, [FromQuery]string texto)
    {
        var entidade = await _repositoryTarefa.GetAsync(id);
        if (entidade == null)
        {
            return BadRequest("TituloDaTarefa n�o localizada.");
        }

        entidade.Comentarios.Add(new Comentario { Texto = texto });
        await _repositoryTarefa.SaveAsync(entidade);
        await _repositoryTarefa.CommitAsync(usuarioId);
        return _mapper.Map<TarefaModel>(entidade);
    }
}