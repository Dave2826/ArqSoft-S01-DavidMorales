using Microsoft.AspNetCore.Http;
using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
{
    public class PerfilController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public PerfilController(
            UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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

            var model = new PerfilViewModel
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                FechaRegistro = usuario.FechaRegistro,

                TotalMotocicletas = 0,
                ServiciosRealizados = 0,
                GastoAcumulado = 0
            };

            return View(model);
        }
    }
}
