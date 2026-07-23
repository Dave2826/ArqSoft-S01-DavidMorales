using MotoTrack.Domain.Enums;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;
using MotoTrack.Application.KnowledgeBase;

namespace MotoTrack.Helpers
{
    public class CalculadorEstadoMantenimiento
    {
        private readonly IEstadoMantenimientoStrategy _strategy;

        public CalculadorEstadoMantenimiento(IEstadoMantenimientoStrategy strategy)
        {
            _strategy = strategy;
        }

        private static readonly Dictionary<MaintenanceType, string> FiltroTipo = new()
        {
            [MaintenanceType.Aceite] = "Cambio de aceite",
            [MaintenanceType.Cadena] = "Cadena",
            [MaintenanceType.Balatas] = "Balatas",
            [MaintenanceType.Llantas] = "Llantas",
            [MaintenanceType.FiltroAire] = "Filtro de aire",
            [MaintenanceType.Bujias] = "Bujías",
            [MaintenanceType.Valvulas] = "Válvulas",
            [MaintenanceType.Bateria] = "Batería",
            [MaintenanceType.Suspension] = "Suspensión",
            [MaintenanceType.LiquidoFrenos] = "Líquido de frenos",
            [MaintenanceType.Anticongelante] = "Anticongelante"
        };

        private static string ObtenerNombreTipo(MaintenanceType tipo) => tipo switch
        {
            MaintenanceType.Aceite => "Aceite",
            MaintenanceType.Cadena => "Cadena",
            MaintenanceType.Balatas => "Balatas",
            MaintenanceType.Llantas => "Llantas",
            MaintenanceType.FiltroAire => "Filtro de aire",
            MaintenanceType.Bujias => "Bujías",
            MaintenanceType.Valvulas => "Válvulas",
            MaintenanceType.Bateria => "Batería",
            MaintenanceType.Suspension => "Suspensión",
            MaintenanceType.LiquidoFrenos => "Líquido de frenos",
            MaintenanceType.Anticongelante => "Anticongelante",
            _ => tipo.ToString()
        };

        private static EstadoMantenimientoResult.EstadoPrioridad EstadoAPrioridad(string estado)
        {
            return estado switch
            {
                "VENCIDO" => EstadoMantenimientoResult.EstadoPrioridad.Vencido,
                "PRÓXIMO" => EstadoMantenimientoResult.EstadoPrioridad.Proximo,
                "AL DÍA" => EstadoMantenimientoResult.EstadoPrioridad.AlDia,
                _ => EstadoMantenimientoResult.EstadoPrioridad.SinRegistro
            };
        }

        private static string ObtenerUltimoStr(List<Mantenimiento> mantenimientos, string tipoFiltro)
        {
            var ultimo = mantenimientos
                .Where(m => m.Tipo == tipoFiltro)
                .OrderByDescending(m => m.KilometrajeServicio)
                .FirstOrDefault();

            return ultimo != null ? $"{ultimo.KilometrajeServicio} km" : "Sin registro";
        }

        private MaintenanceStatusResult CalcularEstadoIndividual(
            List<Mantenimiento> mantenimientos,
            Motocicleta moto,
            MaintenanceType tipo)
        {
            var result = new MaintenanceStatusResult();

            if (!MaintenanceCatalog.TryGet(tipo, out var entry) || entry is null)
            {
                result.Estado = "Sin registro";
                return result;
            }

            var tipoFiltro = FiltroTipo[tipo];

            var ultimo = mantenimientos
                .Where(m => m.Tipo == tipoFiltro)
                .OrderByDescending(m => m.KilometrajeServicio)
                .FirstOrDefault();

            int proxKm;

            if (ultimo != null)
            {
                proxKm = ultimo.KilometrajeServicio + entry.RecommendedIntervalKm;
            }
            else if (moto.KilometrajeCompra.HasValue)
            {
                proxKm = moto.KilometrajeCompra.Value + entry.RecommendedIntervalKm;
            }
            else
            {
                result.Estado = "Sin registro";
                return result;
            }

            var faltan = proxKm - moto.KilometrajeActual;
            var kmRestantes = faltan > 0 ? faltan : 0;

            var estado = _strategy.DeterminarEstado(moto.KilometrajeActual, proxKm, entry.WarningThresholdKm);

            result.Estado = estado;
            result.Color = estado switch
            {
                "VENCIDO" => "red",
                "PRÓXIMO" => "orange",
                "AL DÍA" => "green",
                _ => "gray"
            };
            result.KilometrosRestantes = kmRestantes;
            result.ProximoServicio = proxKm;

            var nombre = ObtenerNombreTipo(tipo);

            result.Mensaje = estado switch
            {
                "VENCIDO" => $"{nombre} superó el kilometraje recomendado. Se recomienda inspección o reemplazo.",
                "PRÓXIMO" => $"Próximo mantenimiento recomendado a los {proxKm} km. Actualmente faltan aproximadamente {kmRestantes} km.",
                "AL DÍA" => $"{nombre} dentro del intervalo recomendado. Próximo servicio a los {proxKm} km.",
                _ => "Sin información disponible."
            };

            return result;
        }

        public EstadoMantenimientoResult Calcular(
            Motocicleta moto,
            List<Mantenimiento> mantenimientos,
            ConfiguracionMantenimiento? config)
        {
            _ = config;

            var r = new EstadoMantenimientoResult();
            bool tieneEstimados = false;

            var rA = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Aceite);
            r.EstadoAceite = rA.Estado;
            r.ProximoAceite = rA.Estado != "Sin registro" ? $"{rA.ProximoServicio} km" : "Sin registro";
            r.UltimoAceite = ObtenerUltimoStr(mantenimientos, "Cambio de aceite");
            if (rA.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Cambio de aceite") && moto.KilometrajeCompra.HasValue)
            { r.AceiteEsEstimado = true; tieneEstimados = true; }

            var rC = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Cadena);
            r.EstadoCadena = rC.Estado;
            r.ProximaCadena = rC.Estado != "Sin registro" ? $"{rC.ProximoServicio} km" : "Sin registro";
            r.UltimaCadena = ObtenerUltimoStr(mantenimientos, "Cadena");
            if (rC.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Cadena") && moto.KilometrajeCompra.HasValue)
            { r.CadenaEsEstimado = true; tieneEstimados = true; }

            var rB = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Balatas);
            r.EstadoBalatas = rB.Estado;
            r.ProximasBalatas = rB.Estado != "Sin registro" ? $"{rB.ProximoServicio} km" : "Sin registro";
            r.UltimasBalatas = ObtenerUltimoStr(mantenimientos, "Balatas");
            if (rB.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Balatas") && moto.KilometrajeCompra.HasValue)
            { r.BalatasEsEstimado = true; tieneEstimados = true; }

            var rL = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Llantas);
            r.EstadoLlantas = rL.Estado;
            r.ProximasLlantas = rL.Estado != "Sin registro" ? $"{rL.ProximoServicio} km" : "Sin registro";
            r.UltimasLlantas = ObtenerUltimoStr(mantenimientos, "Llantas");
            if (rL.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Llantas") && moto.KilometrajeCompra.HasValue)
            { r.LlantasEsEstimado = true; tieneEstimados = true; }

            var rF = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.FiltroAire);
            r.EstadoFiltroAire = rF.Estado;
            r.ProximoFiltroAire = rF.Estado != "Sin registro" ? $"{rF.ProximoServicio} km" : "Sin registro";
            r.UltimoFiltroAire = ObtenerUltimoStr(mantenimientos, "Filtro de aire");
            if (rF.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Filtro de aire") && moto.KilometrajeCompra.HasValue)
            { r.FiltroAireEsEstimado = true; tieneEstimados = true; }

            var rU = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Bujias);
            r.EstadoBujias = rU.Estado;
            r.ProximasBujias = rU.Estado != "Sin registro" ? $"{rU.ProximoServicio} km" : "Sin registro";
            r.UltimasBujias = ObtenerUltimoStr(mantenimientos, "Bujías");
            if (rU.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Bujías") && moto.KilometrajeCompra.HasValue)
            { r.BujiasEsEstimado = true; tieneEstimados = true; }

            var rV = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Valvulas);
            r.EstadoValvulas = rV.Estado;
            r.ProximasValvulas = rV.Estado != "Sin registro" ? $"{rV.ProximoServicio} km" : "Sin registro";
            r.UltimasValvulas = ObtenerUltimoStr(mantenimientos, "Válvulas");
            if (rV.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Válvulas") && moto.KilometrajeCompra.HasValue)
            { r.ValvulasEsEstimado = true; tieneEstimados = true; }

            var rE = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Bateria);
            r.EstadoBateria = rE.Estado;
            r.ProximaBateria = rE.Estado != "Sin registro" ? $"{rE.ProximoServicio} km" : "Sin registro";
            r.UltimaBateria = ObtenerUltimoStr(mantenimientos, "Batería");
            if (rE.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Batería") && moto.KilometrajeCompra.HasValue)
            { r.BateriaEsEstimado = true; tieneEstimados = true; }

            var rS = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Suspension);
            r.EstadoSuspension = rS.Estado;
            r.ProximaSuspension = rS.Estado != "Sin registro" ? $"{rS.ProximoServicio} km" : "Sin registro";
            r.UltimaSuspension = ObtenerUltimoStr(mantenimientos, "Suspensión");
            if (rS.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Suspensión") && moto.KilometrajeCompra.HasValue)
            { r.SuspensionEsEstimado = true; tieneEstimados = true; }

            var rH = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.LiquidoFrenos);
            r.EstadoLiquidoFrenos = rH.Estado;
            r.ProximoLiquidoFrenos = rH.Estado != "Sin registro" ? $"{rH.ProximoServicio} km" : "Sin registro";
            r.UltimoLiquidoFrenos = ObtenerUltimoStr(mantenimientos, "Líquido de frenos");
            if (rH.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Líquido de frenos") && moto.KilometrajeCompra.HasValue)
            { r.LiquidoFrenosEsEstimado = true; tieneEstimados = true; }

            var rN = CalcularEstadoIndividual(mantenimientos, moto, MaintenanceType.Anticongelante);
            r.EstadoAnticongelante = rN.Estado;
            r.ProximoAnticongelante = rN.Estado != "Sin registro" ? $"{rN.ProximoServicio} km" : "Sin registro";
            r.UltimoAnticongelante = ObtenerUltimoStr(mantenimientos, "Anticongelante");
            if (rN.Estado != "Sin registro" && !mantenimientos.Any(m => m.Tipo == "Anticongelante") && moto.KilometrajeCompra.HasValue)
            { r.AnticongelanteEsEstimado = true; tieneEstimados = true; }

            r.TieneEstimados = tieneEstimados;

            var prioridades = new[]
            {
                EstadoAPrioridad(rA.Estado), EstadoAPrioridad(rC.Estado), EstadoAPrioridad(rB.Estado),
                EstadoAPrioridad(rL.Estado), EstadoAPrioridad(rF.Estado), EstadoAPrioridad(rU.Estado),
                EstadoAPrioridad(rV.Estado), EstadoAPrioridad(rE.Estado), EstadoAPrioridad(rS.Estado),
                EstadoAPrioridad(rH.Estado), EstadoAPrioridad(rN.Estado)
            };

            var maxPri = prioridades.Max();
            r.PrioridadGeneral = maxPri;
            r.Resumen = maxPri switch
            {
                EstadoMantenimientoResult.EstadoPrioridad.Vencido => "VENCIDO",
                EstadoMantenimientoResult.EstadoPrioridad.Proximo => "PRÓXIMO SERVICIO",
                EstadoMantenimientoResult.EstadoPrioridad.AlDia => "AL DÍA",
                _ => "Sin registro"
            };

            r.TotalVencidos = prioridades.Count(p => p == EstadoMantenimientoResult.EstadoPrioridad.Vencido);
            r.TotalProximos = prioridades.Count(p => p == EstadoMantenimientoResult.EstadoPrioridad.Proximo);

            return r;
        }
    }
}
