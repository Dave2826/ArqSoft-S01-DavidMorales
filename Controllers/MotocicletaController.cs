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

            // =====================
            // Configuración inicial
            // =====================

            var configuracion =
                new ConfiguracionMantenimiento
                {
                    MotocicletaId = motocicleta.Id,

                    CambioAceiteKm = 3000,

                    RevisionCadenaKm = 1000,

                    RevisionBalatasKm = 5000,

                    RevisionLlantasKm = 5000,

                    RevisionFiltroAireKm = 10000,

                    AjusteValvulasKm = 12000
                };

            _configuracionService
                .Guardar(configuracion);

            return RedirectToAction(nameof(Index));
        }
    }
}