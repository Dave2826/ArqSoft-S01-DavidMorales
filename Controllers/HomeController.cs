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

        public IActionResult Index()
        {
            var usuarioIdString = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioIdString))
            {
                return RedirectToAction("Login", "Auth");
            }

            var usuarioId = Guid.Parse(usuarioIdString);
            var motocicletas = _motocicletaService.ObtenerPorUsuario(usuarioId);
            
            var model = new DashboardViewModel
            {
                TotalMotocicletas = motocicletas.Count,
                PrimeraMotocicleta = motocicletas.FirstOrDefault(),
                UltimoMantenimiento = motocicletas.Any() 
                    ? _mantenimientoService.ObtenerPorMotocicleta(motocicletas.First().Id)
                        .OrderByDescending(m => m.Fecha)
                        .FirstOrDefault()
                    : null,
                KilometrajeStatus = "Lectura disponible próximamente"
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class DashboardViewModel
    {
        public int TotalMotocicletas { get; set; }
        public Motocicleta? PrimeraMotocicleta { get; set; }
        public Mantenimiento? UltimoMantenimiento { get; set; }
        public string KilometrajeStatus { get; set; } = "";
    }
}
