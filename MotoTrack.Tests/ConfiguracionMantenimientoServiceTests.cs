using MotoTrack.Application.Services;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;
using Moq;

namespace MotoTrack.Tests
{
    public class ConfiguracionMantenimientoServiceTests
    {
        private readonly Mock<IConfiguracionMantenimientoRepository> _repoMock;
        private readonly ConfiguracionMantenimientoService _service;

        public ConfiguracionMantenimientoServiceTests()
        {
            _repoMock = new Mock<IConfiguracionMantenimientoRepository>();
            _service = new ConfiguracionMantenimientoService(_repoMock.Object);
        }

        private ConfiguracionMantenimiento CrearConfiguracionRegistrada(int cilindrada)
        {
            ConfiguracionMantenimiento? capturada = null;
            _repoMock
                .Setup(r => r.Guardar(It.IsAny<ConfiguracionMantenimiento>()))
                .Callback<ConfiguracionMantenimiento>(c => capturada = c);

            _service.CrearConfiguracionInicial(new Motocicleta { Cilindrada = cilindrada });

            return capturada!;
        }

        [Theory]
        [InlineData(125, 3000, 1000, 5000, 8000, 12000)]
        [InlineData(250, 4000, 1000, 6000, 10000, 15000)]
        [InlineData(600, 5000, 1000, 8000, 12000, 18000)]
        [InlineData(1000, 6000, 1000, 10000, 12000, 20000)]
        public void CrearConfiguracionInicial_UsaLaPlantillaSegunCilindrada(
            int cilindrada, int aceite, int cadena, int filtroAire, int balatas, int llantas)
        {
            var configuracion = CrearConfiguracionRegistrada(cilindrada);

            Assert.Equal(aceite, configuracion.CambioAceiteKm);
            Assert.Equal(cadena, configuracion.RevisionCadenaKm);
            Assert.Equal(filtroAire, configuracion.RevisionFiltroAireKm);
            Assert.Equal(balatas, configuracion.RevisionBalatasKm);
            Assert.Equal(llantas, configuracion.RevisionLlantasKm);
            Assert.Equal(0, configuracion.AjusteValvulasKm);
        }

        [Fact]
        public void CrearConfiguracionInicial_Cilindrada250_NoUtilizaValoresHardcodeadosAnteriores()
        {
            var configuracion = CrearConfiguracionRegistrada(250);

            Assert.NotEqual(2000, configuracion.CambioAceiteKm);
            Assert.NotEqual(3000, configuracion.RevisionCadenaKm);
            Assert.NotEqual(5000, configuracion.RevisionBalatasKm);
            Assert.NotEqual(7500, configuracion.RevisionLlantasKm);
            Assert.NotEqual(10000, configuracion.RevisionFiltroAireKm);
        }

        [Fact]
        public void CrearConfiguracionInicial_AsignaMotocicletaIdYGuardaEnRepositorio()
        {
            var motocicleta = new Motocicleta { Id = Guid.NewGuid(), Cilindrada = 250 };
            ConfiguracionMantenimiento? capturada = null;
            _repoMock
                .Setup(r => r.Guardar(It.IsAny<ConfiguracionMantenimiento>()))
                .Callback<ConfiguracionMantenimiento>(c => capturada = c);

            var resultado = _service.CrearConfiguracionInicial(motocicleta);

            Assert.Same(resultado, capturada);
            Assert.Equal(motocicleta.Id, resultado.MotocicletaId);
            _repoMock.Verify(r => r.Guardar(It.IsAny<ConfiguracionMantenimiento>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-250)]
        public void CrearConfiguracionInicial_CilindradaInvalida_LanzaExcepcion(
            int cilindrada)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _service.CrearConfiguracionInicial(new Motocicleta { Cilindrada = cilindrada }));
        }
    }
}
