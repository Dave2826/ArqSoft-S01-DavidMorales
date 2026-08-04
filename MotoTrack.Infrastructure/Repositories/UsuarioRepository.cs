using System.Text.Json;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _rutaArchivo =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "usuarios.json");

        public List<Usuario> ObtenerTodos()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Usuario>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<Usuario>>(json)
                   ?? new List<Usuario>();
        }

        public Usuario? ObtenerPorCorreo(string correo)
        {
            return ObtenerTodos()
                .FirstOrDefault(u =>
                    u.Correo.Equals(
                        correo,
                        StringComparison.OrdinalIgnoreCase));
        }

        public Usuario? ObtenerPorId(Guid id)
        {
            return ObtenerTodos()
                .FirstOrDefault(u => u.Id == id);
        }

        public void Agregar(Usuario usuario)
        {
            var usuarios = ObtenerTodos();

            usuarios.Add(usuario);

            var json = JsonSerializer.Serialize(
                usuarios,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_rutaArchivo, json);
        }
    }
}
