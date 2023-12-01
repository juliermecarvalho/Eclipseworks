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
public class HistoricoControllerTests
{
    private Mock<IRepositoryHistorico> _mockRepositorioHistorico;
    private Mock<IMapper> _mapper;
    private HistoricoController _historicoController;

    [SetUp]
    public void SetUp()
    {
        _mockRepositorioHistorico = new Mock<IRepositoryHistorico>();
        _mapper = new Mock<IMapper>();

        _historicoController = new HistoricoController(
            _mockRepositorioHistorico.Object,
            _mapper.Object);
    }

    [Test]
    public async Task List_DeveRetornarListaDeHistoricos()
    {
        // Arrange
        var entidades = new List<Historico> { new Historico { } };
        var models = new List<HistoricoModel> { new HistoricoModel { } };

        _mapper.Setup(m => m.Map<List<Historico>>(models)).Returns(entidades);
        _mapper.Setup(m => m.Map<List<HistoricoModel>>(entidades)).Returns(models);
        _mockRepositorioHistorico.Setup(r => r.ListAsync(

            It.IsAny<Expression<Func<Historico, bool>>>(), // Filtro
            It.IsAny<Func<IQueryable<Historico>, IOrderedQueryable<Historico>>>(), // Ordenação
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Historico, object>>[]>() // Includes
            )).ReturnsAsync(entidades);

        // Act
        var resultado = await _historicoController.List(5);

        // Assert
        Assert.IsInstanceOf<ActionResult<IList<HistoricoModel>>>(resultado);
        Assert.IsInstanceOf<List<HistoricoModel>>(resultado.Value);
        Assert.AreEqual(entidades.Count, resultado.Value.Count);
        _mockRepositorioHistorico.Verify(r => r.ListAsync(
            It.IsAny<Expression<Func<Historico, bool>>>(), // Filtro
            It.IsAny<Func<IQueryable<Historico>, IOrderedQueryable<Historico>>>(), // Ordenação
            It.IsAny<bool>(), // AsNoTracking
            It.IsAny<Expression<Func<Historico, object>>[]>() // Includes
            ), Times.Once);

    }

}