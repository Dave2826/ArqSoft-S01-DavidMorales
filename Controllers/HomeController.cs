using MotoTrack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;

namespace MotoTrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly MotocicletaService _motocicletaService;
        private readonly MantenimientoService _mantenimientoService;

        public HomeController(
            MotocicletaService motocicletaService,
            MantenimientoService mantenimientoService)
        {
            _motocicletaService = motocicletaService;
            _mantenimientoService = mantenimientoService;
        }

        public IActionResult Index(Guid? motocicletaId)
        {
            var usuarioIdString = HttpContext.Session.GetString("UsuarioId");

            if (string.IsNullOrEmpty(usuarioIdString))
            {
                return RedirectToAction("Login", "Auth");
            }

            var usuarioId = Guid.Parse(usuarioIdString);

            var motocicletas =
                _motocicletaService.ObtenerPorUsuario(usuarioId);

            var motocicletaSeleccionada =
                motocicletaId.HasValue
                    ? motocicletas.FirstOrDefault(
                        m => m.Id == motocicletaId.Value)
                    : motocicletas.FirstOrDefault();

            var mantenimientos =
                motocicletaSeleccionada != null
                    ? _mantenimientoService
                        .ObtenerPorMotocicleta(
                            motocicletaSeleccionada.Id)
                    : new List<Mantenimiento>();

            var ultimoMantenimiento =
                mantenimientos.FirstOrDefault();

            var ultimoAceite =
                mantenimientos.FirstOrDefault(
                    m => m.Tipo == "Cambio de aceite");

            var ultimaCadena =
                mantenimientos.FirstOrDefault(
                    m => m.Tipo == "Cadena");

            var ultimasBalatas =
                mantenimientos.FirstOrDefault(
                    m => m.Tipo == "Balatas");

            var ultimasLlantas =
                mantenimientos.FirstOrDefault(
                    m => m.Tipo == "Llantas");

            var model = new DashboardViewModel
            {
                TotalMotocicletas = motocicletas.Count,

                PrimeraMotocicleta = motocicletaSeleccionada,

                UltimoMantenimiento = ultimoMantenimiento,

                KilometrajeStatus =
                    motocicletaSeleccionada != null
                        ? $"{motocicletaSeleccionada.KilometrajeActual} km"
                        : "Sin motocicletas",

                UltimoAceite =
                    ultimoAceite != null
                        ? $"{ultimoAceite.KilometrajeServicio} km"
                        : "Sin registro",

                UltimaCadena =
                    ultimaCadena != null
                        ? $"{ultimaCadena.KilometrajeServicio} km"
                        : "Sin registro",

                UltimasBalatas =
                    ultimasBalatas != null
                        ? $"{ultimasBalatas.KilometrajeServicio} km"
                        : "Sin registro",

                UltimasLlantas =
                    ultimasLlantas != null
                        ? $"{ultimasLlantas.KilometrajeServicio} km"
                        : "Sin registro",

                Motocicletas = motocicletas,

                MotocicletaSeleccionadaId =
                    motocicletaSeleccionada?.Id
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id
                        ?? HttpContext.TraceIdentifier
                });
        }
    }

    public class DashboardViewModel
    {
        public int TotalMotocicletas { get; set; }

        public Motocicleta? PrimeraMotocicleta { get; set; }

        public Mantenimiento? UltimoMantenimiento { get; set; }

        public string KilometrajeStatus { get; set; } = "";

        public string UltimoAceite { get; set; } = "Sin registro";

        public string UltimaCadena { get; set; } = "Sin registro";

        public string UltimasBalatas { get; set; } = "Sin registro";

        public string UltimasLlantas { get; set; } = "Sin registro";

        public List<Motocicleta> Motocicletas { get; set; }
            = new();

        public Guid? MotocicletaSeleccionadaId { get; set; }
    }
}