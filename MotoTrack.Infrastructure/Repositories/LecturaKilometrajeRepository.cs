using System.Text.Json;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Repositories
{
    public class LecturaKilometrajeRepository
        : ILecturaKilometrajeRepository
    {
        private readonly string _rutaArchivo =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "lecturasKilometraje.json");

        public List<LecturaKilometraje> ObtenerTodas()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<LecturaKilometraje>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<LecturaKilometraje>>(json)
                   ?? new List<LecturaKilometraje>();
        }

        public List<LecturaKilometraje> ObtenerPorMotocicleta(
            Guid motocicletaId)
        {
            return ObtenerTodas()
                .Where(l => l.MotocicletaId == motocicletaId)
                .OrderBy(l => l.Fecha)
                .ToList();
        }

        public LecturaKilometraje? ObtenerUltimaLectura(
            Guid motocicletaId)
        {
            return ObtenerPorMotocicleta(motocicletaId)
                .OrderByDescending(l => l.Fecha)
                .FirstOrDefault();
        }

        public void Agregar(
            LecturaKilometraje lectura)
        {
            var lecturas = ObtenerTodas();

            lecturas.Add(lectura);

            var json = JsonSerializer.Serialize(
                lecturas,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_rutaArchivo, json);
        }
    }
}
