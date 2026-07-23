using MotoTrack.Application.Strategies;

namespace MotoTrack.Tests
{
    public class ConservadoraEstadoStrategyTests
    {
        private readonly ConservadoraEstadoStrategy _strategy = new();

        [Theory]
        [InlineData(1000, 500, 500, "VENCIDO")]
        [InlineData(1000, 999, 500, "VENCIDO")]
        [InlineData(1000, 1000, 500, "PRÓXIMO")]
        [InlineData(600, 1000, 500, "PRÓXIMO")]
        [InlineData(500, 1000, 500, "PRÓXIMO")]
        [InlineData(200, 1000, 500, "PRÓXIMO")]
        [InlineData(0, 1000, 500, "PRÓXIMO")]
        [InlineData(1000, 1000, 0, "PRÓXIMO")]
        [InlineData(800, 1000, 0, "AL DÍA")]
        [InlineData(0, 0, 500, "PRÓXIMO")]
        [InlineData(100, 0, 500, "VENCIDO")]
        public void DeterminarEstado_DadosKilometrajes_RetornaEstadoEsperado(
            int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm, string esperado)
        {
            var resultado = _strategy.DeterminarEstado(kilometrajeActual, kilometrajeProximo, warningThresholdKm);
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(499, 1000, 500, "PRÓXIMO", "AL DÍA")]
        [InlineData(400, 1000, 500, "PRÓXIMO", "AL DÍA")]
        [InlineData(250, 1000, 500, "PRÓXIMO", "AL DÍA")]
        [InlineData(0, 1000, 500, "PRÓXIMO", "AL DÍA")]
        [InlineData(500, 1500, 500, "PRÓXIMO", "AL DÍA")]
        [InlineData(600, 1500, 500, "PRÓXIMO", "AL DÍA")]
        public void DeterminarEstado_ComparadoConDefaultStrategy_MuestraComportamientoMasConservador(
            int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm,
            string esperadoConservadora, string esperadoDefault)
        {
            var conservadora = new ConservadoraEstadoStrategy();
            var defaultStrategy = new DefaultEstadoStrategy();

            var resultadoConservadora = conservadora.DeterminarEstado(kilometrajeActual, kilometrajeProximo, warningThresholdKm);
            var resultadoDefault = defaultStrategy.DeterminarEstado(kilometrajeActual, kilometrajeProximo, warningThresholdKm);

            Assert.Equal(esperadoConservadora, resultadoConservadora);
            Assert.Equal(esperadoDefault, resultadoDefault);
        }

        [Theory]
        [InlineData(2499, 2500, 1)]
        [InlineData(2498, 2500, 1)]
        [InlineData(2497, 2500, 2)]
        [InlineData(2496, 2500, 2)]
        public void DeterminarEstado_UmbralWarningThresholdMultiplicado_CubreRangoEsperado(
            int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm)
        {
            var esperado = "PRÓXIMO";
            var resultado = _strategy.DeterminarEstado(kilometrajeActual, kilometrajeProximo, warningThresholdKm);
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(int.MinValue, 0, 500)]
        [InlineData(int.MaxValue, int.MaxValue, 500)]
        [InlineData(0, int.MaxValue, int.MaxValue)]
        public void DeterminarEstado_ValoresExtremos_NoLanzaExcepcion(
            int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm)
        {
            var excepcion = Record.Exception(() =>
                _strategy.DeterminarEstado(kilometrajeActual, kilometrajeProximo, warningThresholdKm));
            Assert.Null(excepcion);
        }
    }
}
