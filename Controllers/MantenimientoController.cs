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

        public IActionResult Gastos(Guid motocicletaId)
        {
            var mantenimientos = _mantenimientoService
                .ObtenerPorMotocicleta(motocicletaId);

            var motocicleta = _motocicletaService
                .ObtenerPorId(motocicletaId);

            ViewData["Motocicleta"] = motocicleta;

            ViewData["TotalGastado"] =
                mantenimientos.Sum(m => m.Costo);

            ViewData["Aceite"] =
                mantenimientos
                    .Where(m => m.Tipo == "Cambio de aceite")
                    .Sum(m => m.Costo);

            ViewData["Balatas"] =
                mantenimientos
                    .Where(m => m.Tipo == "Balatas")
                    .Sum(m => m.Costo);

            ViewData["Cadena"] =
                mantenimientos
                    .Where(m => m.Tipo == "Cadena")
                    .Sum(m => m.Costo);

            ViewData["Llantas"] =
                mantenimientos
                    .Where(m => m.Tipo == "Llantas")
                    .Sum(m => m.Costo);

            return View();
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

            // Actualizar kilometraje de la motocicleta
            if (mantenimiento.KilometrajeServicio >
                motocicleta.KilometrajeActual)
            {
                motocicleta.KilometrajeActual =
                    mantenimiento.KilometrajeServicio;

                _motocicletaService
                    .Actualizar(motocicleta);
            }

            return RedirectToAction(
                "Index",
                "Motocicleta");
        }
    }
}
