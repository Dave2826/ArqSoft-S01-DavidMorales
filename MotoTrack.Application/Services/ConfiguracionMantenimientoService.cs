using MotoTrack.Application.KnowledgeBase;
using MotoTrack.Domain.Enums;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class ConfiguracionMantenimientoService
    {
        private static readonly Dictionary<MaintenanceType, Action<ConfiguracionMantenimiento, int>> _aplicadores =
            new()
            {
                [MaintenanceType.Aceite] = (c, km) => c.CambioAceiteKm = km,
                [MaintenanceType.Cadena] = (c, km) => c.RevisionCadenaKm = km,
                [MaintenanceType.Balatas] = (c, km) => c.RevisionBalatasKm = km,
                [MaintenanceType.Llantas] = (c, km) => c.RevisionLlantasKm = km,
                [MaintenanceType.FiltroAire] = (c, km) => c.RevisionFiltroAireKm = km
            };

        private readonly
            IConfiguracionMantenimientoRepository
            _repository;

        public ConfiguracionMantenimientoService(
            IConfiguracionMantenimientoRepository repository)
        {
            _repository = repository;
        }

        public ConfiguracionMantenimiento?
            ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _repository
                .ObtenerPorMotocicleta(motocicletaId);
        }

        public void Guardar(
            ConfiguracionMantenimiento configuracion)
        {
            _repository.Guardar(configuracion);
        }

        public ConfiguracionMantenimiento
            CrearConfiguracionInicial(
                Motocicleta motocicleta)
        {
            var configuracion =
                new ConfiguracionMantenimiento
                {
                    MotocicletaId = motocicleta.Id
                };

            var recomendaciones =
                DefaultMaintenancePolicy
                    .ObtenerRecomendaciones(
                        motocicleta.Cilindrada);

            foreach (var recomendacion in recomendaciones)
            {
                if (_aplicadores.TryGetValue(
                        recomendacion.Type,
                        out var aplicar))
                {
                    aplicar(
                        configuracion,
                        recomendacion
                            .RecommendedIntervalKm);
                }
            }

            _repository.Guardar(configuracion);

            return configuracion;
        }
    }
}
