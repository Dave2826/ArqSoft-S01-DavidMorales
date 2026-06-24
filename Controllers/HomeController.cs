using MotoTrack.Models;
using MotoTrack.Helpers;
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
        private readonly CalculadorEstadoMantenimiento _calculadorEstado;

        public HomeController(
            MotocicletaService motocicletaService,
            MantenimientoService mantenimientoService,
            ConfiguracionMantenimientoService configuracionService,
            CalculadorEstadoMantenimiento calculadorEstado)
        {
            _motocicletaService = motocicletaService;
            _mantenimientoService = mantenimientoService;
            _configuracionService = configuracionService;
            _calculadorEstado = calculadorEstado;
        }

        public IActionResult Index(Guid? motocicletaId)
        {
            var usuarioIdString = HttpContext.Session.GetString("UsuarioId");

            if (string.IsNullOrEmpty(usuarioIdString))
            {
                return View("Landing");
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

            var configuracion =
                motocicletaSeleccionada != null
                    ? _configuracionService
                        .ObtenerPorMotocicleta(
                            motocicletaSeleccionada.Id)
                    : null;

            EstadoMantenimientoResult? resultado = null;

            if (motocicletaSeleccionada != null)
            {
                resultado = _calculadorEstado.Calcular(
                    motocicletaSeleccionada,
                    mantenimientos,
                    configuracion);
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

                UltimoAceite = resultado?.UltimoAceite ?? "Sin registro",

                UltimaCadena = resultado?.UltimaCadena ?? "Sin registro",

                UltimasBalatas = resultado?.UltimasBalatas ?? "Sin registro",

                UltimasLlantas = resultado?.UltimasLlantas ?? "Sin registro",

                UltimoFiltroAire = resultado?.UltimoFiltroAire ?? "Sin registro",

                UltimasValvulas = resultado?.UltimasValvulas ?? "Sin registro",

                ProximoAceite = resultado?.ProximoAceite ?? "Sin registro",

                ProximaCadena = resultado?.ProximaCadena ?? "Sin registro",

                ProximasBalatas = resultado?.ProximasBalatas ?? "Sin registro",

                ProximasLlantas = resultado?.ProximasLlantas ?? "Sin registro",

                ProximoFiltroAire = resultado?.ProximoFiltroAire ?? "Sin registro",

                ProximasValvulas = resultado?.ProximasValvulas ?? "Sin registro",

                EstadoAceite = resultado?.EstadoAceite ?? "Sin registro",

                EstadoCadena = resultado?.EstadoCadena ?? "Sin registro",

                EstadoBalatas = resultado?.EstadoBalatas ?? "Sin registro",

                EstadoLlantas = resultado?.EstadoLlantas ?? "Sin registro",

                EstadoFiltroAire = resultado?.EstadoFiltroAire ?? "Sin registro",

                EstadoValvulas = resultado?.EstadoValvulas ?? "Sin registro",

                Motocicletas = motocicletas,

                MotocicletaSeleccionadaId =
                    motocicletaSeleccionada?.Id,

                AceiteEsEstimado = resultado?.AceiteEsEstimado ?? false,
                CadenaEsEstimado = resultado?.CadenaEsEstimado ?? false,
                BalatasEsEstimado = resultado?.BalatasEsEstimado ?? false,
                LlantasEsEstimado = resultado?.LlantasEsEstimado ?? false,
                FiltroAireEsEstimado = resultado?.FiltroAireEsEstimado ?? false,
                ValvulasEsEstimado = resultado?.ValvulasEsEstimado ?? false,
                TieneEstimados = resultado?.TieneEstimados ?? false,
                TotalVencidos = resultado?.TotalVencidos ?? 0,
                TotalProximos = resultado?.TotalProximos ?? 0,
                Alertas = ConstruirAlertas(resultado)
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

        private List<DashboardViewModel.AlertaItem> ConstruirAlertas(EstadoMantenimientoResult? r)
        {
            var items = new List<DashboardViewModel.AlertaItem>();

            if (r == null) return items;

            if (r.EstadoAceite is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Aceite", Estado = r.EstadoAceite, EsEstimado = r.AceiteEsEstimado });

            if (r.EstadoCadena is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Cadena", Estado = r.EstadoCadena, EsEstimado = r.CadenaEsEstimado });

            if (r.EstadoBalatas is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Balatas", Estado = r.EstadoBalatas, EsEstimado = r.BalatasEsEstimado });

            if (r.EstadoLlantas is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Llantas", Estado = r.EstadoLlantas, EsEstimado = r.LlantasEsEstimado });

            if (r.EstadoFiltroAire is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Filtro de aire", Estado = r.EstadoFiltroAire, EsEstimado = r.FiltroAireEsEstimado });

            if (r.EstadoValvulas is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Válvulas", Estado = r.EstadoValvulas, EsEstimado = r.ValvulasEsEstimado });

            return items;
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

        public string UltimoFiltroAire { get; set; } = "Sin registro";

        public string UltimasValvulas { get; set; } = "Sin registro";

        public string ProximoAceite { get; set; } = "Sin registro";

        public string ProximaCadena { get; set; } = "Sin registro";

        public string ProximasBalatas { get; set; } = "Sin registro";

        public string ProximasLlantas { get; set; } = "Sin registro";

        public string ProximoFiltroAire { get; set; } = "Sin registro";

        public string ProximasValvulas { get; set; } = "Sin registro";

        public string EstadoAceite { get; set; } = "Sin registro";

        public string EstadoCadena { get; set; } = "Sin registro";

        public string EstadoBalatas { get; set; } = "Sin registro";

        public string EstadoLlantas { get; set; } = "Sin registro";

        public string EstadoFiltroAire { get; set; } = "Sin registro";

        public string EstadoValvulas { get; set; } = "Sin registro";

        public List<Motocicleta> Motocicletas { get; set; }
            = new();

        public Guid? MotocicletaSeleccionadaId { get; set; }

        public bool TieneEstimados { get; set; }

        public bool AceiteEsEstimado { get; set; }

        public bool CadenaEsEstimado { get; set; }

        public bool BalatasEsEstimado { get; set; }

        public bool LlantasEsEstimado { get; set; }

        public bool FiltroAireEsEstimado { get; set; }

        public bool ValvulasEsEstimado { get; set; }

        public int TotalVencidos { get; set; }

        public int TotalProximos { get; set; }

        public List<AlertaItem> Alertas { get; set; } = new();

        public class AlertaItem
        {
            public string Tipo { get; set; } = "";
            public string Estado { get; set; } = "";
            public bool EsEstimado { get; set; }
        }
    }
}