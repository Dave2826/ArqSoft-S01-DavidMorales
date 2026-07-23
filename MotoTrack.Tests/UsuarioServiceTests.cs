using MotoTrack.Application.Services;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;
using Moq;

namespace MotoTrack.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_repoMock.Object);
        }

        [Fact]
        public void ObtenerTodos_RetornaListaDelRepositorio()
        {
            var esperados = new List<Usuario> { new() { Correo = "a@a.com" } };
            _repoMock.Setup(r => r.ObtenerTodos()).Returns(esperados);

            var resultado = _service.ObtenerTodos();

            Assert.Equal(esperados, resultado);
        }

        [Fact]
        public void ObtenerPorCorreo_CuandoExiste_RetornaUsuario()
        {
            var usuario = new Usuario { Correo = "test@test.com" };
            _repoMock.Setup(r => r.ObtenerPorCorreo("test@test.com")).Returns(usuario);

            var resultado = _service.ObtenerPorCorreo("test@test.com");

            Assert.Equal(usuario, resultado);
        }

        [Fact]
        public void ObtenerPorCorreo_CuandoNoExiste_RetornaNull()
        {
            _repoMock.Setup(r => r.ObtenerPorCorreo("no@existe.com")).Returns((Usuario?)null);

            var resultado = _service.ObtenerPorCorreo("no@existe.com");

            Assert.Null(resultado);
        }

        [Fact]
        public void ObtenerPorId_CuandoExiste_RetornaUsuario()
        {
            var id = Guid.NewGuid();
            var usuario = new Usuario { Id = id };
            _repoMock.Setup(r => r.ObtenerPorId(id)).Returns(usuario);

            var resultado = _service.ObtenerPorId(id);

            Assert.Equal(usuario, resultado);
        }

        [Fact]
        public void ObtenerPorId_CuandoNoExiste_RetornaNull()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.ObtenerPorId(id)).Returns((Usuario?)null);

            var resultado = _service.ObtenerPorId(id);

            Assert.Null(resultado);
        }

        [Fact]
        public void RegistrarUsuario_CuandoCorreoNoExiste_AgregaYRetornaTrue()
        {
            var usuario = new Usuario { Correo = "nuevo@test.com", PasswordHash = "hash123" };
            _repoMock.Setup(r => r.ObtenerPorCorreo("nuevo@test.com")).Returns((Usuario?)null);

            var resultado = _service.RegistrarUsuario(usuario);

            Assert.True(resultado);
            _repoMock.Verify(r => r.Agregar(usuario), Times.Once);
        }

        [Fact]
        public void RegistrarUsuario_CuandoCorreoYaExiste_NoAgregaYRetornaFalse()
        {
            var existente = new Usuario { Correo = "dup@test.com" };
            var nuevo = new Usuario { Correo = "dup@test.com" };
            _repoMock.Setup(r => r.ObtenerPorCorreo("dup@test.com")).Returns(existente);

            var resultado = _service.RegistrarUsuario(nuevo);

            Assert.False(resultado);
            _repoMock.Verify(r => r.Agregar(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void ValidarLogin_CuandoCredencialesSonCorrectas_RetornaUsuario()
        {
            var usuario = new Usuario { Correo = "user@test.com", PasswordHash = "pass123" };
            _repoMock.Setup(r => r.ObtenerPorCorreo("user@test.com")).Returns(usuario);

            var resultado = _service.ValidarLogin("user@test.com", "pass123");

            Assert.Equal(usuario, resultado);
        }

        [Fact]
        public void ValidarLogin_CuandoCorreoNoExiste_RetornaNull()
        {
            _repoMock.Setup(r => r.ObtenerPorCorreo("ghost@test.com")).Returns((Usuario?)null);

            var resultado = _service.ValidarLogin("ghost@test.com", "cualquier");

            Assert.Null(resultado);
        }

        [Fact]
        public void ValidarLogin_CuandoPasswordEsIncorrecto_RetornaNull()
        {
            var usuario = new Usuario { Correo = "user@test.com", PasswordHash = "passCorrecto" };
            _repoMock.Setup(r => r.ObtenerPorCorreo("user@test.com")).Returns(usuario);

            var resultado = _service.ValidarLogin("user@test.com", "passIncorrecto");

            Assert.Null(resultado);
        }
    }
}
