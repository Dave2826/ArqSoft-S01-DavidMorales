using Catalogo.Application.Services;
using Catalogo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class MotocicletaController : Controller
    {
        private readonly MotocicletaService _motocicletaService;
        private readonly ConfiguracionMantenimientoService _configuracionService;

        public MotocicletaController(
            MotocicletaService motocicletaService,
            ConfiguracionMantenimientoService configuracionService)
        {
            _motocicletaService = motocicletaService;
            _configuracionService = configuracionService;
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

        public IActionResult Editar(Guid id)
        {
            var motocicleta = _motocicletaService.ObtenerPorId(id);
            if (motocicleta == null)
            {
                return NotFound();
            }
            return View(motocicleta);
        }

        [HttpPost]
        public IActionResult Editar(Motocicleta motocicleta)
        {
            if (!ModelState.IsValid)
            {
                return View(motocicleta);
            }

            _motocicletaService.Actualizar(motocicleta);
            return RedirectToAction(nameof(Index));
        }
    }
}