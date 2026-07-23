using System.Text.Json;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Repositories
{
    public class MotocicletaRepository : IMotocicletaRepository
    {
        private readonly string _rutaArchivo =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "motocicletas.json");

        public List<Motocicleta> ObtenerTodas()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Motocicleta>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<Motocicleta>>(json)
                   ?? new List<Motocicleta>();
        }

        public List<Motocicleta> ObtenerPorUsuario(Guid usuarioId)
        {
            return ObtenerTodas()
                .Where(m => m.UsuarioId == usuarioId)
                .ToList();
        }

        public Motocicleta? ObtenerPorId(Guid id)
        {
            return ObtenerTodas()
                .FirstOrDefault(m => m.Id == id);
        }

        public void Agregar(Motocicleta motocicleta)
        {
            var motos = ObtenerTodas();

            motos.Add(motocicleta);

            var json = JsonSerializer.Serialize(
                motos,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_rutaArchivo, json);
        }

        public void Actualizar(
    Motocicleta motocicleta)
        {
            var motos = ObtenerTodas();

            var indice =
                motos.FindIndex(
                    m => m.Id == motocicleta.Id);

            if (indice >= 0)
            {
                motos[indice] = motocicleta;

                var json = JsonSerializer.Serialize(
                    motos,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                File.WriteAllText(
                    _rutaArchivo,
                    json);
            }
        }
        
        public void Eliminar(Guid id)
        {
            var motos = ObtenerTodas();
            motos.RemoveAll(m => m.Id == id);
            
            var json = JsonSerializer.Serialize(
                motos,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                
            File.WriteAllText(_rutaArchivo, json);
        }
    }
}
