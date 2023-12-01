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
public class ProjetoControllerTests
{
    private Mock<IRepositoryProjeto> _mockRepositoryProjeto;
    private Mock<IMapper> _mapper;
    private ProjetoController _projetoController;

    [SetUp]
    public void SetUp()
    {
        _mockRepositoryProjeto = new Mock<IRepositoryProjeto>();
        _mapper = new Mock<IMapper>();

        _projetoController = new ProjetoController(
            _mockRepositoryProjeto.Object,
            _mapper.Object);
    }

    [Test]
    public async Task List_DeveRetornarListaDeProjetos()
    {
        // Arrange
        var entidades = new List<Projeto> { new Projeto { } };
        var models = new List<ProjetoModel> { new ProjetoModel { } };
        var usuarioId = 1;
        Expression<Func<Projeto, bool>> filtro = p => p.CriadorDoProjetoId == usuarioId;

        _mapper.Setup(m => m.Map<List<Projeto>>(models)).Returns(entidades);
        _mapper.Setup(m => m.Map<List<ProjetoModel>>(entidades)).Returns(models);
        _mockRepositoryProjeto.Setup(r => r.ListAsync(
            It.IsAny<Expression<Func<Projeto, bool>>>(), // Filtro
            It.IsAny<Func<IQueryable<Projeto>, IOrderedQueryable<Projeto>>>(), // Ordenação
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Projeto, object>>[]>() // Includes
        )).ReturnsAsync(entidades);

        // Act
        var resultado = await _projetoController.List(usuarioId);

        // Assert
        Assert.IsInstanceOf<ActionResult<IList<ProjetoModel>>>(resultado);
        Assert.IsInstanceOf<List<ProjetoModel>>(resultado.Value);
        Assert.AreEqual(entidades.Count, resultado.Value.Count);
        _mockRepositoryProjeto.Verify(r => r.ListAsync(
            It.IsAny<Expression<Func<Projeto, bool>>>(), // Filtro
            It.IsAny<Func<IQueryable<Projeto>, IOrderedQueryable<Projeto>>>(), // Ordenação
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Projeto, object>>[]>() // Includes
        ), Times.Once);

    }

      

    [Test]
    public async Task Get_DeveRetornarProjetoPorId()
    {
        // Arrange
        const int id = 1;
        var entidade = new Projeto { Id = 1 };
        var model = new ProjetoModel { Id = 1, };
        _mapper.Setup(m => m.Map<ProjetoModel>(entidade)).Returns(model);
        _mockRepositoryProjeto.Setup(r => r.GetAsync(id,
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Projeto, object>>[]>() // Includes
        )).ReturnsAsync(entidade);

        // Act
        var resultado = await _projetoController.Get(id);

        // Assert
        Assert.IsInstanceOf<ActionResult<ProjetoModel>>(resultado);
        Assert.IsInstanceOf<ProjetoModel>(resultado.Value);
        Assert.AreEqual(entidade.Id, resultado.Value.Id);
        _mockRepositoryProjeto.Verify(r => r.GetAsync(id,
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Projeto, object>>[]>() // Includes
        ), Times.Once);

    }

    [Test]
    public async Task Post_ComModelValido_DeveSalvarProjetoEretornarProjetoModel()
    {
        // Arrange
        var model = new ProjetoModel { Id = 1, };
        var entidade = new Projeto { Id = 1, };
        _mapper.Setup(m => m.Map<Projeto>(model)).Returns(entidade);
        _mapper.Setup(m => m.Map<ProjetoModel>(entidade)).Returns(model);
        _mockRepositoryProjeto.Setup(r => r.SaveAsync(entidade)).Returns(Task.FromResult(entidade));

        // Act
        var resultado = await _projetoController.Post(model);

        // Assert            
        Assert.IsInstanceOf<ActionResult<ProjetoModel>>(resultado);
        Assert.IsInstanceOf<ProjetoModel>(resultado.Value);
        Assert.AreEqual(entidade.Id, resultado.Value.Id);
        _mockRepositoryProjeto.Verify(r => r.SaveAsync(entidade), Times.Once);
        _mockRepositoryProjeto.Verify(r => r.CommitAsync(0), Times.Once);

    }


    [Test]
    public async Task Delete_DeveExcluirProjetoECommitarAlteracoes()
    {
        // Arrange
        var id = 1;
        var entidade = new Projeto { Id = 1, Tarefas = new List<Tarefa>() {new Tarefa {Status = Status.Concluida}}};
        var model = new ProjetoModel { Id = 1, };
        _mapper.Setup(m => m.Map<ProjetoModel>(entidade)).Returns(model);
        _mockRepositoryProjeto.Setup(r => r.GetAsync(id,
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Projeto, object>>[]>() // Includes
        )).ReturnsAsync(entidade);


        // Act
        var resultado = await _projetoController.Delete(id);

        // Assert
        Assert.IsInstanceOf<OkResult>(resultado);
        _mockRepositoryProjeto.Verify(r => r.DeleteAsync(id), Times.Once);
        _mockRepositoryProjeto.Verify(r => r.CommitAsync(It.IsAny<long>()), Times.Once);
    }

    [Test]
    public async Task Delete_DeveRetornarBadRequestQuandoTarefasPendentesExistem()
    {
        var projetoId = 1;
        var projetoComTarefasPendentes = new Projeto
        {
            Id = projetoId,
            Tarefas = new List<Tarefa> { new Tarefa { Status = Status.Pendente } }
        };

        _mockRepositoryProjeto.Setup(repo => repo.GetAsync(projetoId, It.IsAny<bool>(), It.IsAny<Expression<Func<Projeto, object>>>()))
            .ReturnsAsync(projetoComTarefasPendentes);

        // Act
        var result = await _projetoController.Delete(projetoId);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual("Não é possível excluir um projeto que possui tarefas pendentes. Primeiro conclua todas as tarefas.", badRequestResult.Value);
        _mockRepositoryProjeto.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        _mockRepositoryProjeto.Verify(repo => repo.CommitAsync(It.IsAny<int>()), Times.Never);
    }
}