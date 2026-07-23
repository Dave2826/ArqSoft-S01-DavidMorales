using System.Text.Json;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Repositories
{
    public class MantenimientoRepository
        : IMantenimientoRepository
    {
        private readonly string _rutaArchivo =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "mantenimientos.json");

        public void Agregar(Mantenimiento mantenimiento)
        {
            var mantenimientos = ObtenerTodos();

            mantenimientos.Add(mantenimiento);

            var json = JsonSerializer.Serialize(
                mantenimientos,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_rutaArchivo, json);
        }

        public List<Mantenimiento> ObtenerPorMotocicleta(
            Guid motocicletaId)
        {
            return ObtenerTodos()
                .Where(m => m.MotocicletaId == motocicletaId)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public Mantenimiento? ObtenerPorId(Guid id)
        {
            return ObtenerTodos()
                .FirstOrDefault(m => m.Id == id);
        }

        private List<Mantenimiento> ObtenerTodos()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Mantenimiento>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<Mantenimiento>>(json)
                   ?? new List<Mantenimiento>();
        }
    }
}
