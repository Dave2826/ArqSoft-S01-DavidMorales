using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _usuarioRepository.ObtenerTodos();
        }

        public Usuario? ObtenerPorCorreo(string correo)
        {
            return _usuarioRepository.ObtenerPorCorreo(correo);
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            var existente =
                _usuarioRepository.ObtenerPorCorreo(usuario.Correo);

            if (existente != null)
            {
                return false;
            }

            _usuarioRepository.Agregar(usuario);

            return true;
        }
        public Usuario? ValidarLogin(
            string correo,
            string password)
        {
            var usuario =
                _usuarioRepository.ObtenerPorCorreo(correo);

            if (usuario == null)
            {
                return null;
            }

            if (usuario.PasswordHash != password)
            {
                return null;
            }

            return usuario;
        }
        public Usuario? ObtenerPorId(Guid id)
        {
            return _usuarioRepository.ObtenerPorId(id);
        }
    }
}
