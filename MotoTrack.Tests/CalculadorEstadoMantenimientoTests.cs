using MotoTrack.Application.Strategies;
using MotoTrack.Domain.Models;
using MotoTrack.Helpers;

namespace MotoTrack.Tests
{
    public class CalculadorEstadoMantenimientoTests
    {
        private static CalculadorEstadoMantenimiento CrearCalculador()
        {
            return new CalculadorEstadoMantenimiento(new DefaultEstadoStrategy());
        }

        private static Motocicleta CrearMoto(
            int kilometrajeActual,
            int? kilometrajeCompra = null)
        {
            return new Motocicleta
            {
                Marca = "Yamaha",
                Modelo = "Xtz 250",
                Ano = 2025,
                Cilindrada = 250,
                KilometrajeActual = kilometrajeActual,
                KilometrajeCompra = kilometrajeCompra
            };
        }

        private static ConfiguracionMantenimiento CrearConfig(
            int aceite,
            int cadena,
            int balatas,
            int llantas,
            int filtro,
            int valvulas = 0)
        {
            return new ConfiguracionMantenimiento
            {
                CambioAceiteKm = aceite,
                RevisionCadenaKm = cadena,
                RevisionBalatasKm = balatas,
                RevisionLlantasKm = llantas,
                RevisionFiltroAireKm = filtro,
                AjusteValvulasKm = valvulas
            };
        }

        [Fact]
        public void Calcular_MotoNuevaSinHistorial_UsoConfigParaProximosServicios()
        {
            // Arrange
            var moto = CrearMoto(0);
            var config = CrearConfig(4000, 1000, 10000, 15000, 6000);

            // Act
            var resultado = CrearCalculador().Calcular(moto, new List<Mantenimiento>(), config);

            // Assert
            Assert.Equal("AL DÍA", resultado.EstadoAceite);
            Assert.Equal("4000 km", resultado.ProximoAceite);
            Assert.Equal("AL DÍA", resultado.EstadoBalatas);
            Assert.Equal("10000 km", resultado.ProximasBalatas);
            Assert.Equal("AL DÍA", resultado.EstadoLlantas);
            Assert.Equal("15000 km", resultado.ProximasLlantas);
            Assert.Equal("AL DÍA", resultado.EstadoFiltroAire);
            Assert.Equal("6000 km", resultado.ProximoFiltroAire);
            Assert.Equal(0, resultado.TotalVencidos);
        }

        [Fact]
        public void Calcular_MotoNuevaSinHistorial_ValvulasSinConfiguracion_NoVencido()
        {
            // Arrange
            var moto = CrearMoto(0);
            var config = CrearConfig(4000, 1000, 10000, 15000, 6000);

            // Act
            var resultado = CrearCalculador().Calcular(moto, new List<Mantenimiento>(), config);

            // Assert
            Assert.Equal("Sin registro", resultado.EstadoValvulas);
            Assert.Equal("Sin registro", resultado.ProximasValvulas);
            Assert.Equal("Sin registro", resultado.EstadoBujias);
            Assert.Equal("Sin registro", resultado.ProximasBujias);
        }

        [Fact]
        public void Calcular_MotoNuevaSinHistorial_CadenaConIntervaloCompletoDisponible_PresentaAlDia()
        {
            // Arrange
            var moto = CrearMoto(0);
            var config = CrearConfig(4000, 1000, 10000, 15000, 6000);

            // Act
            var resultado = CrearCalculador().Calcular(moto, new List<Mantenimiento>(), config);

            // Assert
            Assert.Equal("1000 km", resultado.ProximaCadena);
            Assert.Equal("AL DÍA", resultado.EstadoCadena);
        }

        [Fact]
        public void Calcular_ConHistorialReal_ProximoDesdeUltimoServicio()
        {
            // Arrange
            var moto = CrearMoto(6000, 0);
            var config = CrearConfig(4000, 1000, 10000, 15000, 6000);
            var mantenimientos = new List<Mantenimiento>
            {
                new() { Tipo = "Cambio de aceite", KilometrajeServicio = 4000 }
            };

            // Act
            var resultado = CrearCalculador().Calcular(moto, mantenimientos, config);

            // Assert
            Assert.Equal("AL DÍA", resultado.EstadoAceite);
            Assert.Equal("8000 km", resultado.ProximoAceite);
            Assert.Equal("4000 km", resultado.UltimoAceite);
            Assert.False(resultado.AceiteEsEstimado);
        }

        [Fact]
        public void Calcular_ConfigPersonalizadoSinHistorial_ProximoDesdeIntervaloPersonalizado()
        {
            // Arrange
            var moto = CrearMoto(0);
            var config = CrearConfig(5000, 1000, 10000, 15000, 6000);

            // Act
            var resultado = CrearCalculador().Calcular(moto, new List<Mantenimiento>(), config);

            // Assert
            Assert.Equal("AL DÍA", resultado.EstadoAceite);
            Assert.Equal("5000 km", resultado.ProximoAceite);
        }

        [Fact]
        public void Calcular_ConfigPersonalizadoConHistorial_ProximoDesdeUltimoMasIntervaloPersonalizado()
        {
            // Arrange
            var moto = CrearMoto(6000, 0);
            var config = CrearConfig(5000, 1000, 10000, 15000, 6000);
            var mantenimientos = new List<Mantenimiento>
            {
                new() { Tipo = "Cambio de aceite", KilometrajeServicio = 5000 }
            };

            // Act
            var resultado = CrearCalculador().Calcular(moto, mantenimientos, config);

            // Assert
            Assert.Equal("AL DÍA", resultado.EstadoAceite);
            Assert.Equal("10000 km", resultado.ProximoAceite);
            Assert.False(resultado.AceiteEsEstimado);
        }

        [Fact]
        public void Calcular_IntervaloCero_ExcluyeTipoSinVencidoFalso()
        {
            // Arrange
            var moto = CrearMoto(9000);
            var config = CrearConfig(0, 1000, 10000, 15000, 6000);

            // Act
            var resultado = CrearCalculador().Calcular(moto, new List<Mantenimiento>(), config);

            // Assert
            Assert.Equal("Sin registro", resultado.EstadoAceite);
            Assert.Equal("Sin registro", resultado.ProximoAceite);
            Assert.Equal("VENCIDO", resultado.EstadoCadena);
            Assert.True(resultado.TotalVencidos >= 1);
        }

        [Fact]
        public void Calcular_SuperadoIntervalo_PresentaVencido()
        {
            // Arrange
            var moto = CrearMoto(9000, 0);
            var config = CrearConfig(4000, 1000, 10000, 15000, 6000);
            var mantenimientos = new List<Mantenimiento>
            {
                new() { Tipo = "Cambio de aceite", KilometrajeServicio = 4000 }
            };

            // Act
            var resultado = CrearCalculador().Calcular(moto, mantenimientos, config);

            // Assert
            Assert.Equal("VENCIDO", resultado.EstadoAceite);
            Assert.Equal("8000 km", resultado.ProximoAceite);
            Assert.Equal("VENCIDO", resultado.Resumen);
            Assert.True(resultado.TotalVencidos >= 1);
        }

        [Fact]
        public void Calcular_CambioDeConfig_RecalculaProximoServicio()
        {
            // Arrange
            var moto = CrearMoto(0);

            // Act
            var resultadoA = CrearCalculador().Calcular(
                moto,
                new List<Mantenimiento>(),
                CrearConfig(3000, 1000, 10000, 15000, 6000));
            var resultadoB = CrearCalculador().Calcular(
                moto,
                new List<Mantenimiento>(),
                CrearConfig(8000, 1000, 10000, 15000, 6000));

            // Assert
            Assert.Equal("3000 km", resultadoA.ProximoAceite);
            Assert.Equal("8000 km", resultadoB.ProximoAceite);
            Assert.NotEqual(resultadoA.ProximoAceite, resultadoB.ProximoAceite);
        }

        [Fact]
        public void Calcular_ConfigEsFuenteDeIntervalo_NoCatalogo()
        {
            // Arrange
            var moto = CrearMoto(0);
            var config = CrearConfig(8000, 1000, 10000, 15000, 6000);

            // Act
            var resultado = CrearCalculador().Calcular(moto, new List<Mantenimiento>(), config);

            // Assert
            Assert.Equal("8000 km", resultado.ProximoAceite);
        }
    }
}
