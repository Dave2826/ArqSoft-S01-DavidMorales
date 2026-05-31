using Catalogo.Application.Services;
using Catalogo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class MotocicletaController : Controller
    {
        private readonly MotocicletaService _motocicletaService;

        public MotocicletaController(
            MotocicletaService motocicletaService)
        {
            _motocicletaService = motocicletaService;
        }

        // =====================
        // MIS MOTOCICLETAS
        // =====================

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

            var motos =
                _motocicletaService
                    .ObtenerPorUsuario(usuarioId);

            return View(motos);
        }

        // =====================
        // CREAR GET
        // =====================

        public IActionResult Crear()
        {
            return View(new Motocicleta());
        }

        // =====================
        // CREAR POST
        // =====================

        [HttpPost]
        public IActionResult Crear(Motocicleta motocicleta)
        {
            if (!ModelState.IsValid)
            {
                return View(motocicleta);
            }

            var usuarioIdString =
                HttpContext.Session.GetString("UsuarioId");

            if (string.IsNullOrEmpty(usuarioIdString))
            {
                return RedirectToAction(
                    "Login",
                    "Auth");
            }

            motocicleta.UsuarioId =
                Guid.Parse(usuarioIdString);

            _motocicletaService.Agregar(motocicleta);

            return RedirectToAction(nameof(Index));
        }
    }
}