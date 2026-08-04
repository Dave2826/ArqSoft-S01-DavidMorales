using MotoTrack.Application.KnowledgeBase;
using MotoTrack.Domain.Enums;
using MotoTrack.Domain.Models;

namespace MotoTrack.Tests
{
    public class DefaultMaintenancePolicyTests
    {
        private static int Intervalo(
            IReadOnlyList<MaintenanceRecommendation> recomendaciones,
            MaintenanceType tipo)
        {
            return recomendaciones.First(r => r.Type == tipo).RecommendedIntervalKm;
        }

        [Theory]
        [InlineData(125)]
        [InlineData(200)]
        public void ObtenerRecomendaciones_CilindradaHasta200_RetornaPlantillaA(
            int cilindrada)
        {
            var recomendaciones = DefaultMaintenancePolicy.ObtenerRecomendaciones(cilindrada);

            Assert.Equal(6, recomendaciones.Count);
            Assert.Equal(3000, Intervalo(recomendaciones, MaintenanceType.Aceite));
            Assert.Equal(1000, Intervalo(recomendaciones, MaintenanceType.Cadena));
            Assert.Equal(5000, Intervalo(recomendaciones, MaintenanceType.FiltroAire));
            Assert.Equal(8000, Intervalo(recomendaciones, MaintenanceType.Bujias));
            Assert.Equal(8000, Intervalo(recomendaciones, MaintenanceType.Balatas));
            Assert.Equal(12000, Intervalo(recomendaciones, MaintenanceType.Llantas));
        }

        [Theory]
        [InlineData(201)]
        [InlineData(250)]
        [InlineData(400)]
        public void ObtenerRecomendaciones_CilindradaEntre201y400_RetornaPlantillaB(
            int cilindrada)
        {
            var recomendaciones = DefaultMaintenancePolicy.ObtenerRecomendaciones(cilindrada);

            Assert.Equal(6, recomendaciones.Count);
            Assert.Equal(4000, Intervalo(recomendaciones, MaintenanceType.Aceite));
            Assert.Equal(1000, Intervalo(recomendaciones, MaintenanceType.Cadena));
            Assert.Equal(6000, Intervalo(recomendaciones, MaintenanceType.FiltroAire));
            Assert.Equal(10000, Intervalo(recomendaciones, MaintenanceType.Bujias));
            Assert.Equal(10000, Intervalo(recomendaciones, MaintenanceType.Balatas));
            Assert.Equal(15000, Intervalo(recomendaciones, MaintenanceType.Llantas));
        }

        [Theory]
        [InlineData(401)]
        [InlineData(600)]
        [InlineData(900)]
        public void ObtenerRecomendaciones_CilindradaEntre401y900_RetornaPlantillaC(
            int cilindrada)
        {
            var recomendaciones = DefaultMaintenancePolicy.ObtenerRecomendaciones(cilindrada);

            Assert.Equal(6, recomendaciones.Count);
            Assert.Equal(5000, Intervalo(recomendaciones, MaintenanceType.Aceite));
            Assert.Equal(1000, Intervalo(recomendaciones, MaintenanceType.Cadena));
            Assert.Equal(8000, Intervalo(recomendaciones, MaintenanceType.FiltroAire));
            Assert.Equal(12000, Intervalo(recomendaciones, MaintenanceType.Bujias));
            Assert.Equal(12000, Intervalo(recomendaciones, MaintenanceType.Balatas));
            Assert.Equal(18000, Intervalo(recomendaciones, MaintenanceType.Llantas));
        }

        [Theory]
        [InlineData(901)]
        [InlineData(1000)]
        public void ObtenerRecomendaciones_CilindradaMayorA900_RetornaPlantillaD(
            int cilindrada)
        {
            var recomendaciones = DefaultMaintenancePolicy.ObtenerRecomendaciones(cilindrada);

            Assert.Equal(6, recomendaciones.Count);
            Assert.Equal(6000, Intervalo(recomendaciones, MaintenanceType.Aceite));
            Assert.Equal(1000, Intervalo(recomendaciones, MaintenanceType.Cadena));
            Assert.Equal(10000, Intervalo(recomendaciones, MaintenanceType.FiltroAire));
            Assert.Equal(12000, Intervalo(recomendaciones, MaintenanceType.Bujias));
            Assert.Equal(12000, Intervalo(recomendaciones, MaintenanceType.Balatas));
            Assert.Equal(20000, Intervalo(recomendaciones, MaintenanceType.Llantas));
        }

        [Fact]
        public void ObtenerRecomendaciones_RetornaExactamenteLosSeisTiposEsperados()
        {
            var esperados = new[]
            {
                MaintenanceType.Aceite,
                MaintenanceType.Cadena,
                MaintenanceType.FiltroAire,
                MaintenanceType.Bujias,
                MaintenanceType.Balatas,
                MaintenanceType.Llantas
            };

            foreach (var cilindrada in new[] { 125, 250, 600, 1000 })
            {
                var recomendaciones = DefaultMaintenancePolicy.ObtenerRecomendaciones(cilindrada);
                var tipos = recomendaciones.Select(r => r.Type).OrderBy(t => t).ToArray();
                Assert.Equal(esperados.OrderBy(t => t).ToArray(), tipos);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-500)]
        public void ObtenerRecomendaciones_CilindradaInvalida_LanzaExcepcion(
            int cilindrada)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                DefaultMaintenancePolicy.ObtenerRecomendaciones(cilindrada));
        }
    }
}
