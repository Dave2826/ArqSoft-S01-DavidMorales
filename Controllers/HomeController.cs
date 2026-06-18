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
        private readonly ConfiguracionMantenimientoService _configuracionService;

        public HomeController(
            MotocicletaService motocicletaService,
            MantenimientoService mantenimientoService,
            ConfiguracionMantenimientoService configuracionService)
        {
            _motocicletaService = motocicletaService;
            _mantenimientoService = mantenimientoService;
            _configuracionService = configuracionService;
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
                mantenimientos
                    .Where(m => m.Tipo == "Cambio de aceite")
                    .OrderByDescending(m => m.KilometrajeServicio)
                    .FirstOrDefault();

            var ultimaCadena =
                mantenimientos
                    .Where(m => m.Tipo == "Cadena")
                    .OrderByDescending(m => m.KilometrajeServicio)
                    .FirstOrDefault();

            var ultimasBalatas =
                mantenimientos
                    .Where(m => m.Tipo == "Balatas")
                    .OrderByDescending(m => m.KilometrajeServicio)
                    .FirstOrDefault();

            var ultimasLlantas =
                mantenimientos
                    .Where(m => m.Tipo == "Llantas")
                    .OrderByDescending(m => m.KilometrajeServicio)
                    .FirstOrDefault();

            var configuracion =
                motocicletaSeleccionada != null
                    ? _configuracionService
                        .ObtenerPorMotocicleta(
                            motocicletaSeleccionada.Id)
                    : null;

            var proximoAceiteKm = 0;

            if (ultimoAceite != null &&
                configuracion != null)
            {
                proximoAceiteKm =
                    ultimoAceite.KilometrajeServicio +
                    configuracion.CambioAceiteKm;
            }

            var proximoAceite =
                proximoAceiteKm > 0
                    ? $"{proximoAceiteKm} km"
                    : "Sin registro";

            var proximaCadena =
                ultimaCadena != null && configuracion != null
                    ? $"{ultimaCadena.KilometrajeServicio + configuracion.RevisionCadenaKm} km"
                    : "Sin registro";

            var proximasBalatas =
                ultimasBalatas != null && configuracion != null
                    ? $"{ultimasBalatas.KilometrajeServicio + configuracion.RevisionBalatasKm} km"
                    : "Sin registro";

            var proximasLlantas =
                ultimasLlantas != null && configuracion != null
                    ? $"{ultimasLlantas.KilometrajeServicio + configuracion.RevisionLlantasKm} km"
                    : "Sin registro";

            string estadoAceite = "Sin registro";

            if (motocicletaSeleccionada != null &&
                proximoAceiteKm > 0)
            {
                var faltan =
                    proximoAceiteKm -
                    motocicletaSeleccionada.KilometrajeActual;

                if (faltan < 0)
                {
                    estadoAceite = "VENCIDO";
                }
                else if (faltan <= 500)
                {
                    estadoAceite = "PRÓXIMO";
                }
                else
                {
                    estadoAceite = "AL DÍA";
                }
            }

            string estadoCadena = "Sin registro";

            if (motocicletaSeleccionada != null &&
                ultimaCadena != null &&
                configuracion != null)
            {
                var proximoKm =
                    ultimaCadena.KilometrajeServicio +
                    configuracion.RevisionCadenaKm;

                var faltan =
                    proximoKm -
                    motocicletaSeleccionada.KilometrajeActual;

                if (faltan < 0)
                {
                    estadoCadena = "VENCIDO";
                }
                else if (faltan <= 500)
                {
                    estadoCadena = "PRÓXIMO";
                }
                else
                {
                    estadoCadena = "AL DÍA";
                }
            }

            string estadoBalatas = "Sin registro";

            if (motocicletaSeleccionada != null &&
                ultimasBalatas != null &&
                configuracion != null)
            {
                var proximoKm =
                    ultimasBalatas.KilometrajeServicio +
                    configuracion.RevisionBalatasKm;

                var faltan =
                    proximoKm -
                    motocicletaSeleccionada.KilometrajeActual;

                if (faltan < 0)
                {
                    estadoBalatas = "VENCIDO";
                }
                else if (faltan <= 500)
                {
                    estadoBalatas = "PRÓXIMO";
                }
                else
                {
                    estadoBalatas = "AL DÍA";
                }
            }

            string estadoLlantas = "Sin registro";

            if (motocicletaSeleccionada != null &&
                ultimasLlantas != null &&
                configuracion != null)
            {
                var proximoKm =
                    ultimasLlantas.KilometrajeServicio +
                    configuracion.RevisionLlantasKm;

                var faltan =
                    proximoKm -
                    motocicletaSeleccionada.KilometrajeActual;

                if (faltan < 0)
                {
                    estadoLlantas = "VENCIDO";
                }
                else if (faltan <= 500)
                {
                    estadoLlantas = "PRÓXIMO";
                }
                else
                {
                    estadoLlantas = "AL DÍA";
                }
            }

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

                ProximoAceite = proximoAceite,

                ProximaCadena = proximaCadena,

                ProximasBalatas = proximasBalatas,

                ProximasLlantas = proximasLlantas,

                EstadoAceite = estadoAceite,

                EstadoCadena = estadoCadena,

                EstadoBalatas = estadoBalatas,

                EstadoLlantas = estadoLlantas,

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

        public string ProximoAceite { get; set; } = "Sin registro";

        public string ProximaCadena { get; set; } = "Sin registro";

        public string ProximasBalatas { get; set; } = "Sin registro";

        public string ProximasLlantas { get; set; } = "Sin registro";

        public string EstadoAceite { get; set; } = "Sin registro";

        public string EstadoCadena { get; set; } = "Sin registro";

        public string EstadoBalatas { get; set; } = "Sin registro";

        public string EstadoLlantas { get; set; } = "Sin registro";

        public List<Motocicleta> Motocicletas { get; set; }
            = new();

        public Guid? MotocicletaSeleccionadaId { get; set; }
    }
}