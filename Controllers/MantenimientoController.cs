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
                mantenimientos.Sum(m => m.Costo ?? 0);

            ViewData["Aceite"] =
                mantenimientos
                    .Where(m => m.Tipo == "Cambio de aceite")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Balatas"] =
                mantenimientos
                    .Where(m => m.Tipo == "Balatas")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Cadena"] =
                mantenimientos
                    .Where(m => m.Tipo == "Cadena")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Llantas"] =
                mantenimientos
                    .Where(m => m.Tipo == "Llantas")
                    .Sum(m => m.Costo ?? 0);

            ViewData["FiltroAire"] =
                mantenimientos
                    .Where(m => m.Tipo == "Filtro de aire")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Bujias"] =
                mantenimientos
                    .Where(m => m.Tipo == "Bujías")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Valvulas"] =
                mantenimientos
                    .Where(m => m.Tipo == "Válvulas")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Bateria"] =
                mantenimientos
                    .Where(m => m.Tipo == "Batería")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Suspension"] =
                mantenimientos
                    .Where(m => m.Tipo == "Suspensión")
                    .Sum(m => m.Costo ?? 0);

            ViewData["LiquidoFrenos"] =
                mantenimientos
                    .Where(m => m.Tipo == "Líquido de frenos")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Anticongelante"] =
                mantenimientos
                    .Where(m => m.Tipo == "Anticongelante")
                    .Sum(m => m.Costo ?? 0);

            ViewData["Otros"] =
                mantenimientos
                    .Where(m => m.Tipo != "Cambio de aceite"
                             && m.Tipo != "Balatas"
                             && m.Tipo != "Cadena"
                             && m.Tipo != "Llantas"
                             && m.Tipo != "Filtro de aire"
                             && m.Tipo != "Bujías"
                             && m.Tipo != "Válvulas"
                             && m.Tipo != "Batería"
                             && m.Tipo != "Suspensión"
                             && m.Tipo != "Líquido de frenos"
                             && m.Tipo != "Anticongelante")
                    .Sum(m => m.Costo ?? 0);

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
