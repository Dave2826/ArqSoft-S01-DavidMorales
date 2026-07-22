using MotoTrack.Application.Strategies;

namespace MotoTrack.Tests
{
    public class DefaultEstadoStrategyTests
    {
        private readonly DefaultEstadoStrategy _strategy = new();

        [Theory]
        [InlineData(1000, 500, 500, "VENCIDO")]
        [InlineData(1000, 999, 500, "VENCIDO")]
        [InlineData(1000, 1000, 500, "PRÓXIMO")]
        [InlineData(500, 1000, 500, "PRÓXIMO")]
        [InlineData(501, 1000, 500, "PRÓXIMO")]
        [InlineData(400, 1000, 500, "AL DÍA")]
        [InlineData(0, 1000, 500, "AL DÍA")]
        [InlineData(1000, 1000, 0, "PRÓXIMO")]
        [InlineData(999, 1000, 0, "AL DÍA")]
        [InlineData(0, 0, 500, "PRÓXIMO")]
        [InlineData(100, 0, 500, "VENCIDO")]
        public void DeterminarEstado_DadosKilometrajes_RetornaEstadoEsperado(
            int kilometrajeActual, int kilometrajeProximo, int warningThresholdKm, string esperado)
        {
            // Act
            var resultado = _strategy.DeterminarEstado(
                kilometrajeActual,
                kilometrajeProximo,
                warningThresholdKm);

            // Assert
            Assert.Equal(esperado, resultado);
        }
    }
}
