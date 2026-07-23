using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();

        Usuario? ObtenerPorCorreo(string correo);

        Usuario? ObtenerPorId(Guid id);

        void Agregar(Usuario usuario);
    }
}
