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

                UltimasBujias = resultado?.UltimasBujias ?? "Sin registro",

                UltimasValvulas = resultado?.UltimasValvulas ?? "Sin registro",

                UltimaBateria = resultado?.UltimaBateria ?? "Sin registro",

                UltimaSuspension = resultado?.UltimaSuspension ?? "Sin registro",

                UltimoLiquidoFrenos = resultado?.UltimoLiquidoFrenos ?? "Sin registro",

                UltimoAnticongelante = resultado?.UltimoAnticongelante ?? "Sin registro",

                ProximoAceite = resultado?.ProximoAceite ?? "Sin registro",

                ProximaCadena = resultado?.ProximaCadena ?? "Sin registro",

                ProximasBalatas = resultado?.ProximasBalatas ?? "Sin registro",

                ProximasLlantas = resultado?.ProximasLlantas ?? "Sin registro",

                ProximoFiltroAire = resultado?.ProximoFiltroAire ?? "Sin registro",

                ProximasBujias = resultado?.ProximasBujias ?? "Sin registro",

                ProximasValvulas = resultado?.ProximasValvulas ?? "Sin registro",

                ProximaBateria = resultado?.ProximaBateria ?? "Sin registro",

                ProximaSuspension = resultado?.ProximaSuspension ?? "Sin registro",

                ProximoLiquidoFrenos = resultado?.ProximoLiquidoFrenos ?? "Sin registro",

                ProximoAnticongelante = resultado?.ProximoAnticongelante ?? "Sin registro",

                EstadoAceite = resultado?.EstadoAceite ?? "Sin registro",

                EstadoCadena = resultado?.EstadoCadena ?? "Sin registro",

                EstadoBalatas = resultado?.EstadoBalatas ?? "Sin registro",

                EstadoLlantas = resultado?.EstadoLlantas ?? "Sin registro",

                EstadoFiltroAire = resultado?.EstadoFiltroAire ?? "Sin registro",

                EstadoBujias = resultado?.EstadoBujias ?? "Sin registro",

                EstadoValvulas = resultado?.EstadoValvulas ?? "Sin registro",

                EstadoBateria = resultado?.EstadoBateria ?? "Sin registro",

                EstadoSuspension = resultado?.EstadoSuspension ?? "Sin registro",

                EstadoLiquidoFrenos = resultado?.EstadoLiquidoFrenos ?? "Sin registro",

                EstadoAnticongelante = resultado?.EstadoAnticongelante ?? "Sin registro",

                Motocicletas = motocicletas,

                MotocicletaSeleccionadaId =
                    motocicletaSeleccionada?.Id,

                AceiteEsEstimado = resultado?.AceiteEsEstimado ?? false,
                CadenaEsEstimado = resultado?.CadenaEsEstimado ?? false,
                BalatasEsEstimado = resultado?.BalatasEsEstimado ?? false,
                LlantasEsEstimado = resultado?.LlantasEsEstimado ?? false,
                FiltroAireEsEstimado = resultado?.FiltroAireEsEstimado ?? false,
                BujiasEsEstimado = resultado?.BujiasEsEstimado ?? false,
                ValvulasEsEstimado = resultado?.ValvulasEsEstimado ?? false,
                BateriaEsEstimado = resultado?.BateriaEsEstimado ?? false,
                SuspensionEsEstimado = resultado?.SuspensionEsEstimado ?? false,
                LiquidoFrenosEsEstimado = resultado?.LiquidoFrenosEsEstimado ?? false,
                AnticongelanteEsEstimado = resultado?.AnticongelanteEsEstimado ?? false,
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

        public IActionResult Presentation()
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

            if (r.EstadoBujias is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Bujías", Estado = r.EstadoBujias, EsEstimado = r.BujiasEsEstimado });

            if (r.EstadoValvulas is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Válvulas", Estado = r.EstadoValvulas, EsEstimado = r.ValvulasEsEstimado });

            if (r.EstadoBateria is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Batería", Estado = r.EstadoBateria, EsEstimado = r.BateriaEsEstimado });

            if (r.EstadoSuspension is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Suspensión", Estado = r.EstadoSuspension, EsEstimado = r.SuspensionEsEstimado });

            if (r.EstadoLiquidoFrenos is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Líquido de frenos", Estado = r.EstadoLiquidoFrenos, EsEstimado = r.LiquidoFrenosEsEstimado });

            if (r.EstadoAnticongelante is "VENCIDO" or "PRÓXIMO")
                items.Add(new DashboardViewModel.AlertaItem { Tipo = "Anticongelante", Estado = r.EstadoAnticongelante, EsEstimado = r.AnticongelanteEsEstimado });

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

        public string UltimasBujias { get; set; } = "Sin registro";

        public string UltimasValvulas { get; set; } = "Sin registro";

        public string UltimaBateria { get; set; } = "Sin registro";

        public string UltimaSuspension { get; set; } = "Sin registro";

        public string UltimoLiquidoFrenos { get; set; } = "Sin registro";

        public string UltimoAnticongelante { get; set; } = "Sin registro";

        public string ProximoAceite { get; set; } = "Sin registro";

        public string ProximaCadena { get; set; } = "Sin registro";

        public string ProximasBalatas { get; set; } = "Sin registro";

        public string ProximasLlantas { get; set; } = "Sin registro";

        public string ProximoFiltroAire { get; set; } = "Sin registro";

        public string ProximasBujias { get; set; } = "Sin registro";

        public string ProximasValvulas { get; set; } = "Sin registro";

        public string ProximaBateria { get; set; } = "Sin registro";

        public string ProximaSuspension { get; set; } = "Sin registro";

        public string ProximoLiquidoFrenos { get; set; } = "Sin registro";

        public string ProximoAnticongelante { get; set; } = "Sin registro";

        public string EstadoAceite { get; set; } = "Sin registro";

        public string EstadoCadena { get; set; } = "Sin registro";

        public string EstadoBalatas { get; set; } = "Sin registro";

        public string EstadoLlantas { get; set; } = "Sin registro";

        public string EstadoFiltroAire { get; set; } = "Sin registro";

        public string EstadoBujias { get; set; } = "Sin registro";

        public string EstadoValvulas { get; set; } = "Sin registro";

        public string EstadoBateria { get; set; } = "Sin registro";

        public string EstadoSuspension { get; set; } = "Sin registro";

        public string EstadoLiquidoFrenos { get; set; } = "Sin registro";

        public string EstadoAnticongelante { get; set; } = "Sin registro";

        public List<Motocicleta> Motocicletas { get; set; }
            = new();

        public Guid? MotocicletaSeleccionadaId { get; set; }

        public bool TieneEstimados { get; set; }

        public bool AceiteEsEstimado { get; set; }

        public bool CadenaEsEstimado { get; set; }

        public bool BalatasEsEstimado { get; set; }

        public bool LlantasEsEstimado { get; set; }

        public bool FiltroAireEsEstimado { get; set; }

        public bool BujiasEsEstimado { get; set; }

        public bool ValvulasEsEstimado { get; set; }

        public bool BateriaEsEstimado { get; set; }

        public bool SuspensionEsEstimado { get; set; }

        public bool LiquidoFrenosEsEstimado { get; set; }

        public bool AnticongelanteEsEstimado { get; set; }

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