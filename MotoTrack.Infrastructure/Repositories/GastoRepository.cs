using System.Text.Json;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Repositories
{
    public class GastoRepository : IGastoRepository
    {
        private readonly string _rutaArchivo =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "gastos.json");

        public void Agregar(Gasto gasto)
        {
            var gastos = ObtenerTodos();

            gastos.Add(gasto);

            var json = JsonSerializer.Serialize(
                gastos,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_rutaArchivo, json);
        }

        public List<Gasto> ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return ObtenerTodos()
                .Where(g => g.MotocicletaId == motocicletaId)
                .OrderByDescending(g => g.Fecha)
                .ToList();
        }

        public Gasto? ObtenerPorId(Guid id)
        {
            return ObtenerTodos()
                .FirstOrDefault(g => g.Id == id);
        }

        private List<Gasto> ObtenerTodos()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Gasto>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<Gasto>>(json)
                   ?? new List<Gasto>();
        }
    }
}