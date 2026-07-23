using Microsoft.EntityFrameworkCore;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepositoryEF : IUsuarioRepository
    {
        private readonly MotoTrackDbContext _context;

        public UsuarioRepositoryEF(MotoTrackDbContext context)
        {
            _context = context;
        }

        public List<Usuario> ObtenerTodos()
        {
            return _context.Usuarios.AsNoTracking().ToList();
        }

        public Usuario? ObtenerPorCorreo(string correo)
        {
            return _context.Usuarios.AsNoTracking()
                .FirstOrDefault(u => u.Correo == correo);
        }

        public Usuario? ObtenerPorId(Guid id)
        {
            return _context.Usuarios.AsNoTracking()
                .FirstOrDefault(u => u.Id == id);
        }

        public void Agregar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
