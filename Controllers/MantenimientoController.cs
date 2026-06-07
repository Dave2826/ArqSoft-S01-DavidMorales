using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly MantenimientoService
            _mantenimientoService;

        private readonly MotocicletaService
            _motocicletaService;

        public MantenimientoController(
            MantenimientoService mantenimientoService,
            MotocicletaService motocicletaService)
        {
            _mantenimientoService = mantenimientoService;
            _motocicletaService = motocicletaService;
        }

        public IActionResult Index(Guid motocicletaId)
        {
            var mantenimientos = _mantenimientoService
                .ObtenerPorMotocicleta(motocicletaId);
            
            var motocicleta = _motocicletaService
                .ObtenerPorId(motocicletaId);
                
            ViewData["Motocicleta"] = motocicleta;
                
            return View(mantenimientos);
        }

        public IActionResult Crear(Guid motocicletaId)
        {
            var motocicleta = _motocicletaService
                .ObtenerPorId(motocicletaId);

            if (motocicleta == null)
            {
                return NotFound();
            }

            var mantenimiento = new Mantenimiento
            {
                MotocicletaId = motocicletaId,
                KilometrajeServicio = motocicleta.KilometrajeActual
            };

            return View(mantenimiento);
        }

        [HttpPost]
        public IActionResult Crear(Mantenimiento mantenimiento)
        {
            if (!ModelState.IsValid)
            {
                return View(mantenimiento);
            }

            var motocicleta = _motocicletaService
                .ObtenerPorId(mantenimiento.MotocicletaId);

            if (motocicleta == null)
            {
                return NotFound();
            }

            _mantenimientoService
                .Agregar(mantenimiento);

            return RedirectToAction(
                "Index",
                "Motocicleta");
        }
    }
}
