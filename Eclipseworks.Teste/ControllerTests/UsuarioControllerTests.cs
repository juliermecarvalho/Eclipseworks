using AutoMapper;
using Eclipseworks.Api.Controllers.V1;
using Eclipseworks.Api.Models;
using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace Eclipseworks.Teste.ControllerTests
{
    [TestFixture]
    public class UsuarioControllerTests
    {
        private Mock<IRepositoryUsuario> _mockRepositoryoUsuario;
        private Mock<IMapper> _mapper;
        private UsuarioController _usuarioController;

        [SetUp]
        public void SetUp()
        {
            _mockRepositoryoUsuario = new Mock<IRepositoryUsuario>();
            _mapper = new Mock<IMapper>();

            _usuarioController = new UsuarioController(
                _mockRepositoryoUsuario.Object,
                _mapper.Object);
        }

        [Test]
        public async Task List_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var entidades = new List<Usuario> { new Usuario { } };
            var models = new List<UsuarioModel> { new UsuarioModel { } };

            _mapper.Setup(m => m.Map<List<Usuario>>(models)).Returns(entidades);
            _mapper.Setup(m => m.Map<List<UsuarioModel>>(entidades)).Returns(models);
            //_mockRepositoryoUsuario.Setup(r => r.ListAsync(null, null, true)).ReturnsAsync(entidades);
            _mockRepositoryoUsuario.Setup(repo => repo.ListAsync(
                It.IsAny<Expression<Func<Usuario, bool>>>(), // Filtro
                It.IsAny<Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>>>(), // Ordenação
                It.IsAny<bool>(), // AsNoTracking
                It.IsAny<Expression<Func<Usuario, object>>[]>() // Includes
            )).ReturnsAsync(entidades);

            // Act
            var resultado = await _usuarioController.List();

            // Assert
            Assert.IsInstanceOf<ActionResult<IList<UsuarioModel>>>(resultado);
            Assert.IsInstanceOf<List<UsuarioModel>>(resultado.Value);
            Assert.AreEqual(entidades.Count, resultado.Value.Count);
            _mockRepositoryoUsuario.Verify(r => r.ListAsync(
                It.IsAny<Expression<Func<Usuario, bool>>>(), // Filtro
                It.IsAny<Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>>>(), // Ordenação
                It.IsAny<bool>(), // AsNoTracking
                It.IsAny<Expression<Func<Usuario, object>>[]>() // Includes
                ), Times.Once);

        }

    

        [Test]
        public async Task Get_DeveRetornarUsuarioPorId()
        {
            // Arrange
            const int id = 1;
            var entidade = new Usuario { Id = 1 };
            var model = new UsuarioModel { Id = 1, };
            _mapper.Setup(m => m.Map<UsuarioModel>(entidade)).Returns(model);
            _mockRepositoryoUsuario.Setup(r => r.GetAsync(id, It.IsAny<bool>(), // AsNoTracking
                It.IsAny<Expression<Func<Usuario, object>>[]>() // Includes
                                                                )).ReturnsAsync(entidade);

            // Act
            var resultado = await _usuarioController.Get(id);

            // Assert
            Assert.IsInstanceOf<ActionResult<UsuarioModel>>(resultado);
            Assert.IsInstanceOf<UsuarioModel>(resultado.Value);
            Assert.AreEqual(entidade.Id, resultado.Value.Id);
            _mockRepositoryoUsuario.Verify(r => r.GetAsync(id, It.IsAny<bool>(), It.IsAny<Expression<Func<Usuario, object>>[]>()), Times.Once);

        }

        [Test]
        public async Task Post_ComModelValido_DeveSalvarUsuarioEretornarUsuarioModel()
        {
            // Arrange
            var model = new UsuarioModel { Id = 1, };
            var entidade = new Usuario { Id = 1, };
            _mapper.Setup(m => m.Map<Usuario>(model)).Returns(entidade);
            _mapper.Setup(m => m.Map<UsuarioModel>(entidade)).Returns(model);
            _mockRepositoryoUsuario.Setup(r => r.SaveAsync(entidade)).Returns(Task.FromResult(entidade));

            // Act
            var resultado = await _usuarioController.Post(model);

            // Assert            
            Assert.IsInstanceOf<ActionResult<UsuarioModel>>(resultado);
            Assert.IsInstanceOf<UsuarioModel>(resultado.Value);
            Assert.AreEqual(entidade.Id, resultado.Value.Id);
            _mockRepositoryoUsuario.Verify(r => r.SaveAsync(entidade), Times.Once);
            _mockRepositoryoUsuario.Verify(r => r.CommitAsync(), Times.Once);

        }


        [Test]
        public async Task Delete_DeveExcluirUsuarioECommitarAlteracoes()
        {
            // Arrange
            var id = 1;

            // Act
            var resultado = await _usuarioController.Delete(id);

            // Assert
            Assert.IsInstanceOf<OkResult>(resultado);
            _mockRepositoryoUsuario.Verify(r => r.DeleteAsync(id), Times.Once);
            _mockRepositoryoUsuario.Verify(r => r.CommitAsync(), Times.Once);
        }
    }
}
