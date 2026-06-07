using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
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

        [HttpPost]
        public IActionResult Crear(Motocicleta motocicleta)
        {
            if (!ModelState.IsValid)
            {
                return View(motocicleta);
            }

            var usuarioIdString = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioIdString))
            {
                return RedirectToAction("Login", "Auth");
            }

            motocicleta.UsuarioId = Guid.Parse(usuarioIdString);
            _motocicletaService.Agregar(motocicleta);
            
            var configuracion = new ConfiguracionMantenimiento()
            {
                MotocicletaId = motocicleta.Id,
                CambioAceiteKm = 2000,
                RevisionCadenaKm = 3000,
                RevisionBalatasKm = 5000,
                RevisionLlantasKm = 7500,
                RevisionFiltroAireKm = 10000,
                AjusteValvulasKm = 12000
            };
            _configuracionService.Guardar(configuracion);
            
            return RedirectToAction(nameof(Index));
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
        
        public IActionResult Eliminar(Guid id)
        {
            var motocicleta = _motocicletaService.ObtenerPorId(id);
            if (motocicleta == null)
            {
                return NotFound();
            }
            
            _motocicletaService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
