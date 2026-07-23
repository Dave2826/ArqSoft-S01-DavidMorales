using System.Text.Json;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Repositories
{
    public class ConfiguracionMantenimientoRepository
        : IConfiguracionMantenimientoRepository
    {
        private readonly string _rutaArchivo =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "configuracionesMantenimiento.json");

        private List<ConfiguracionMantenimiento>
            ObtenerTodas()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<ConfiguracionMantenimiento>();
            }

            var json =
                File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<
                List<ConfiguracionMantenimiento>>(json)
                ?? new List<ConfiguracionMantenimiento>();
        }

        public ConfiguracionMantenimiento?
            ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return ObtenerTodas()
                .FirstOrDefault(c =>
                    c.MotocicletaId == motocicletaId);
        }

        public void Guardar(
            ConfiguracionMantenimiento configuracion)
        {
            var configuraciones =
                ObtenerTodas();

            var existente =
                configuraciones.FirstOrDefault(
                    c => c.MotocicletaId ==
                         configuracion.MotocicletaId);

            if (existente != null)
            {
                configuraciones.Remove(existente);
            }

            configuraciones.Add(configuracion);

            var json =
                JsonSerializer.Serialize(
                    configuraciones,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(
                _rutaArchivo,
                json);
        }
    }
}
