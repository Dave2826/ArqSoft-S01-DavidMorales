using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Helpers
{
    public class CalculadorEstadoMantenimiento
    {
        private readonly IEstadoMantenimientoStrategy _strategy;

        public CalculadorEstadoMantenimiento(IEstadoMantenimientoStrategy strategy)
        {
            _strategy = strategy;
        }

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

        private (string estado, string proximo, string ultimo, bool esEstimado, EstadoMantenimientoResult.EstadoPrioridad prioridad) CalcularTipo(
            List<Mantenimiento> mantenimientos,
            ConfiguracionMantenimiento? config,
            Motocicleta moto,
            string tipoFiltro,
            Func<ConfiguracionMantenimiento, int> selectorIntervalo)
        {
            var ultimo = mantenimientos
                .Where(m => m.Tipo == tipoFiltro)
                .OrderByDescending(m => m.KilometrajeServicio)
                .FirstOrDefault();

            string estado = "Sin registro";
            string proximo = "Sin registro";
            string ultimoStr = ultimo != null ? $"{ultimo.KilometrajeServicio} km" : "Sin registro";
            bool esEstimado = false;

            if (ultimo != null && config != null)
            {
                var intervalo = selectorIntervalo(config);
                var proxKm = ultimo.KilometrajeServicio + intervalo;
                proximo = $"{proxKm} km";
                estado = _strategy.DeterminarEstado(moto.KilometrajeActual, proxKm);
            }
            else if (ultimo == null && moto.KilometrajeCompra.HasValue && config != null)
            {
                var intervalo = selectorIntervalo(config);
                var estKm = moto.KilometrajeCompra.Value + intervalo;
                proximo = $"{estKm} km";
                estado = _strategy.DeterminarEstado(moto.KilometrajeActual, estKm);
                esEstimado = true;
            }

            return (estado, proximo, ultimoStr, esEstimado, EstadoAPrioridad(estado));
        }

        public EstadoMantenimientoResult Calcular(
            Motocicleta moto,
            List<Mantenimiento> mantenimientos,
            ConfiguracionMantenimiento? config)
        {
            var r = new EstadoMantenimientoResult();
            bool tieneEstimados = false;

            var (estA, proxA, ultA, estAEs, priA) = CalcularTipo(mantenimientos, config, moto, "Cambio de aceite", c => c.CambioAceiteKm);
            r.EstadoAceite = estA; r.ProximoAceite = proxA; r.UltimoAceite = ultA;
            if (estAEs) { r.AceiteEsEstimado = true; tieneEstimados = true; }

            var (estC, proxC, ultC, estCEs, priC) = CalcularTipo(mantenimientos, config, moto, "Cadena", c => c.RevisionCadenaKm);
            r.EstadoCadena = estC; r.ProximaCadena = proxC; r.UltimaCadena = ultC;
            if (estCEs) { r.CadenaEsEstimado = true; tieneEstimados = true; }

            var (estB, proxB, ultB, estBEs, priB) = CalcularTipo(mantenimientos, config, moto, "Balatas", c => c.RevisionBalatasKm);
            r.EstadoBalatas = estB; r.ProximasBalatas = proxB; r.UltimasBalatas = ultB;
            if (estBEs) { r.BalatasEsEstimado = true; tieneEstimados = true; }

            var (estL, proxL, ultL, estLEs, priL) = CalcularTipo(mantenimientos, config, moto, "Llantas", c => c.RevisionLlantasKm);
            r.EstadoLlantas = estL; r.ProximasLlantas = proxL; r.UltimasLlantas = ultL;
            if (estLEs) { r.LlantasEsEstimado = true; tieneEstimados = true; }

            var (estF, proxF, ultF, estFEs, priF) = CalcularTipo(mantenimientos, config, moto, "Filtro de aire", c => c.RevisionFiltroAireKm);
            r.EstadoFiltroAire = estF; r.ProximoFiltroAire = proxF; r.UltimoFiltroAire = ultF;
            if (estFEs) { r.FiltroAireEsEstimado = true; tieneEstimados = true; }

            var (estV, proxV, ultV, estVEs, priV) = CalcularTipo(mantenimientos, config, moto, "Válvulas", c => c.AjusteValvulasKm);
            r.EstadoValvulas = estV; r.ProximasValvulas = proxV; r.UltimasValvulas = ultV;
            if (estVEs) { r.ValvulasEsEstimado = true; tieneEstimados = true; }

            r.TieneEstimados = tieneEstimados;

            var prioridades = new[] { priA, priC, priB, priL, priF, priV };
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
