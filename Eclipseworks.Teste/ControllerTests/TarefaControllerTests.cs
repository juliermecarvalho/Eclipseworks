using System.Linq.Expressions;
using AutoMapper;
using Eclipseworks.Api.Controllers.V1;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Eclipseworks.Teste.ControllerTests;

[TestFixture]
public class TarefaControllerTests
{
    private Mock<IRepositoryTarefa> _mockRepositoryTarefa;
    private Mock<IMapper> _mapper;
    private TarefaController _tarefaController;

    [SetUp]
    public void SetUp()
    {
        _mockRepositoryTarefa = new Mock<IRepositoryTarefa>();
        _mapper = new Mock<IMapper>();

        _tarefaController = new TarefaController(
            _mockRepositoryTarefa.Object,
            _mapper.Object);
    }

    [Test]
    public async Task List_DeveRetornarListaDeTarefas()
    {
        // Arrange
        var entidades = new List<Tarefa> { new Tarefa { } };
        var models = new List<TarefaModel> { new TarefaModel { } };
        var projetoId = 1;
        Expression<Func<Tarefa, bool>> filtro = p => p.ProjetoId == projetoId;
        _mapper.Setup(m => m.Map<List<Tarefa>>(models)).Returns(entidades);
        _mapper.Setup(m => m.Map<List<TarefaModel>>(entidades)).Returns(models);
        _mockRepositoryTarefa.Setup(r => r.ListAsync(
            It.IsAny<Expression<Func<Tarefa, bool>>>(), // Filtro
            It.IsAny<Func<IQueryable<Tarefa>, IOrderedQueryable<Tarefa>>>(), // Ordenação
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Tarefa, object>>[]>() // Includes
            )).ReturnsAsync(entidades);

        // Act
        var resultado = await _tarefaController.ListTarefasDoProjeto(projetoId);

        // Assert
        Assert.IsInstanceOf<ActionResult<IList<TarefaModel>>>(resultado);
        Assert.IsInstanceOf<List<TarefaModel>>(resultado.Value);
        Assert.AreEqual(entidades.Count, resultado.Value.Count);
        _mockRepositoryTarefa.Verify(r => r.ListAsync(
            It.IsAny<Expression<Func<Tarefa, bool>>>(), // Filtro
            It.IsAny<Func<IQueryable<Tarefa>, IOrderedQueryable<Tarefa>>>(), // Ordenação
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Tarefa, object>>[]>() // Includes
            ), Times.Once);

    }


    [Test]
    public async Task Get_DeveRetornarTarefaPorId()
    {
        // Arrange
        const int id = 1;
        var entidade = new Tarefa { Id = 1 };
        var model = new TarefaModel { Id = 1, };
        _mapper.Setup(m => m.Map<TarefaModel>(entidade)).Returns(model);
        _mockRepositoryTarefa.Setup(r => r.GetAsync(id,
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Tarefa, object>>[]>() // Includes
            )).ReturnsAsync(entidade);

        // Act
        var resultado = await _tarefaController.Get(id);

        // Assert
        Assert.IsInstanceOf<ActionResult<TarefaModel>>(resultado);
        Assert.IsInstanceOf<TarefaModel>(resultado.Value);
        Assert.AreEqual(entidade.Id, resultado.Value.Id);
        _mockRepositoryTarefa.Verify(r => r.GetAsync(id,
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Tarefa, object>>[]>() // Includes
            ), Times.Once);

    }

    [Test]
    public async Task Post_ComModelValido_DeveSalvarTarefaEretornarTarefaModel()
    {
        // Arrange
        var model = new TarefaModel { Id = 1, };
        var entidade = new Tarefa { Id = 1, };
        _mapper.Setup(m => m.Map<Tarefa>(model)).Returns(entidade);
        _mapper.Setup(m => m.Map<TarefaModel>(entidade)).Returns(model);
        _mockRepositoryTarefa.Setup(r => r.SaveAsync(entidade)).Returns(Task.FromResult(entidade));

        // Act
        var resultado = await _tarefaController.Post(model);

        // Assert            
        Assert.IsInstanceOf<ActionResult<TarefaModel>>(resultado);
        Assert.IsInstanceOf<TarefaModel>(resultado.Value);
        Assert.AreEqual(entidade.Id, resultado.Value.Id);
        _mockRepositoryTarefa.Verify(r => r.SaveAsync(entidade), Times.Once);
        _mockRepositoryTarefa.Verify(r => r.CommitAsync(0), Times.Once);

    }

    [Test]
    public async Task AtualizarStatus_DeveRetornarTarefaModelAtualizada()
    {
        // Arrange
        var tarefaId = 1;
        var tarefaEntidade = new Tarefa
        {
            Id = tarefaId,
            Titulo = "Título Antigo",
            Descricao = "Descrição Antiga",

            Status = Status.Pendente
        };

        var tarefaUpdateModel = new TarefaUpdateModel
        {
            Titulo = "Novo Título",
            Descricao = "Nova Descrição",
            Status = (int)Status.Concluida,
            UsuarioId = 1
            
        };

        var tarefaModelAtualizada = new TarefaModel
        {
            Id = tarefaId,
            Titulo = tarefaUpdateModel.Titulo,
            Descricao = tarefaUpdateModel.Descricao,
            Status = tarefaUpdateModel.Status
        };

        _mockRepositoryTarefa.Setup(repo => repo.GetAsync(tarefaId,
                It.IsAny<bool>(), // AsNoTracking
                It.IsAny<Expression<Func<Tarefa, object>>[]>() // Includes
                ))
                             .ReturnsAsync(tarefaEntidade);

        _mapper.Setup(mapper => mapper.Map<TarefaModel>(It.IsAny<Tarefa>()))
                   .Returns(tarefaModelAtualizada);

        // Act
        var result = await _tarefaController.AtualizarStatus(tarefaId, tarefaUpdateModel);

        // Assert
        
        Assert.IsInstanceOf<ActionResult<TarefaModel>>(result);
        var tarefaModelResult = result.Value;
        Assert.IsNotNull(tarefaModelResult);
        Assert.AreEqual(tarefaModelAtualizada.Id, tarefaModelResult.Id);
        Assert.AreEqual(tarefaModelAtualizada.Titulo, tarefaModelResult.Titulo);
        Assert.AreEqual(tarefaModelAtualizada.Descricao, tarefaModelResult.Descricao);
        Assert.AreEqual(tarefaModelAtualizada.Status, tarefaModelResult.Status);
        _mockRepositoryTarefa.Verify(repo => repo.SaveAsync(It.IsAny<Tarefa>()), Times.Once);
        _mockRepositoryTarefa.Verify(repo => repo.CommitAsync(tarefaUpdateModel.UsuarioId), Times.Once);
    }


    [Test]
    public async Task Delete_DeveExcluirTarefaECommitarAlteracoes()
    {
        // Arrange
        var id = 1;
        var usuarioId = 1;

        // Act
        var resultado = await _tarefaController.Delete(id, usuarioId);

        // Assert
        Assert.IsInstanceOf<OkResult>(resultado);
        _mockRepositoryTarefa.Verify(r => r.DeleteAsync(id), Times.Once);
        _mockRepositoryTarefa.Verify(r => r.CommitAsync(usuarioId), Times.Once);
    }
}