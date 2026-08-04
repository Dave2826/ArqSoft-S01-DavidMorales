using MotoTrack.Application.KnowledgeBase;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class ConfiguracionMantenimientoService
    {
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
                configuracion.AsignarIntervalo(
                    recomendacion.Type,
                    recomendacion.RecommendedIntervalKm);
            }

            _repository.Guardar(configuracion);

            return configuracion;
        }
    }
}
