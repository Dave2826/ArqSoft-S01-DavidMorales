using Catalogo.Application.Services;
using Catalogo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class LecturaKilometrajeController : Controller
    {
        private readonly LecturaKilometrajeService
            _lecturaService;

        private readonly MotocicletaService
            _motocicletaService;

        public LecturaKilometrajeController(
            LecturaKilometrajeService lecturaService,
            MotocicletaService motocicletaService)
        {
            _lecturaService = lecturaService;
            _motocicletaService = motocicletaService;
        }

        // =====================
        // CREAR GET
        // =====================

        public IActionResult Crear(Guid motocicletaId)
        {
            var model =
                new RegistrarLecturaViewModel
                {
                    MotocicletaId = motocicletaId
                };

            return View(model);
        }

        // =====================
        // CREAR POST
        // =====================

        [HttpPost]
        public IActionResult Crear(
            RegistrarLecturaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var motocicleta =
                _motocicletaService
                    .ObtenerPorId(model.MotocicletaId);

            if (motocicleta == null)
            {
                return NotFound();
            }

            if (model.Kilometraje
                < motocicleta.KilometrajeActual)
            {
                ModelState.AddModelError(
                    "",
                    "El kilometraje no puede ser menor al actual.");

                return View(model);
            }

            var lectura =
                new LecturaKilometraje
                {
                    MotocicletaId = model.MotocicletaId,
                    Kilometraje = model.Kilometraje,
                    Observaciones = model.Observaciones ?? ""
                };

            _lecturaService.Agregar(lectura);

            motocicleta.KilometrajeActual =
                model.Kilometraje;

            _motocicletaService
                .Actualizar(motocicleta);

            return RedirectToAction(
                "Index",
                "Motocicleta");
        }
    }
}
