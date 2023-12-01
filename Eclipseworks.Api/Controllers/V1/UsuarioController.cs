using AutoMapper;
using Eclipseworks.Api.Models;
using Eclipseworks.Api.Validation.ValidateAttribute;
using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Eclipseworks.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IRepositoryUsuario _repositoryUsuario;
    private readonly IMapper _mapper;

    public UsuarioController(
        IRepositoryUsuario repositoryUsuario,
        IMapper mapper)
    {
        _repositoryUsuario = repositoryUsuario;
        _mapper = mapper;
    }

    [HttpGet("listar")]
    public async Task<ActionResult<IList<UsuarioModel>>> List()
    {
        var entidades = await _repositoryUsuario.ListAsync();
        return _mapper.Map<List<UsuarioModel>>(entidades);
    }

       

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioModel>> Get([FromRoute] int id)
    {
        var entidade = await _repositoryUsuario.GetAsync(id);
        return _mapper.Map<UsuarioModel>(entidade);
    }

    [HttpPost]
    [ValidateModel(typeof(UsuarioModel))]
    public async Task<ActionResult<UsuarioModel>> Post([FromBody] UsuarioModel model)
    {
        var entidade = _mapper.Map<Usuario>(model);
        await _repositoryUsuario.SaveAsync(entidade);
        await _repositoryUsuario.CommitAsync();
        return _mapper.Map<UsuarioModel>(entidade);
    }


   
}