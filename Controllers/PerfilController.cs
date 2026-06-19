using Microsoft.AspNetCore.Http;
using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
{
    public class PerfilController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly MotocicletaService _motocicletaService;
        private readonly MantenimientoService _mantenimientoService;

        public PerfilController(
            UsuarioService usuarioService,
            MotocicletaService motocicletaService,
            MantenimientoService mantenimientoService)
        {
            _usuarioService = usuarioService;
            _motocicletaService = motocicletaService;
            _mantenimientoService = mantenimientoService;
        }

        public IActionResult Index()
        {
            var usuarioIdString =
                HttpContext.Session.GetString("UsuarioId");

            if (string.IsNullOrEmpty(usuarioIdString))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var usuarioId =
                Guid.Parse(usuarioIdString);

            var usuario =
                _usuarioService.ObtenerPorId(usuarioId);

            if (usuario == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            var motos =
                _motocicletaService.ObtenerPorUsuario(usuarioId);

            var todosMantenimientos = motos
                .SelectMany(m =>
                    _mantenimientoService.ObtenerPorMotocicleta(m.Id))
                .ToList();

            var model = new PerfilViewModel
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FechaRegistro = usuario.FechaRegistro,

                TotalMotocicletas = motos.Count,
                ServiciosRealizados = todosMantenimientos.Count,
                GastoAcumulado = todosMantenimientos.Sum(m => m.Costo)
            };

            return View(model);
        }
    }
}
