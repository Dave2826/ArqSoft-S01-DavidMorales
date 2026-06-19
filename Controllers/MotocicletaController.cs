using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
{
    public class MotocicletaController : Controller
    {
        private readonly MotocicletaService _motocicletaService;
        private readonly ConfiguracionMantenimientoService _configuracionService;
        private readonly MantenimientoService _mantenimientoService;

        public MotocicletaController(
            MotocicletaService motocicletaService,
            ConfiguracionMantenimientoService configuracionService,
            MantenimientoService mantenimientoService)
        {
            _motocicletaService = motocicletaService;
            _configuracionService = configuracionService;
            _mantenimientoService = mantenimientoService;
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
        public IActionResult Crear(Motocicleta motocicleta,
            int? kmAceite, int? kmCadena, int? kmBalatas,
            int? kmLlantas, int? kmFiltro, int? kmValvulas)
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

            RegistrarHistorialCompra(motocicleta.Id, kmAceite, kmCadena, kmBalatas, kmLlantas, kmFiltro, kmValvulas);
            
            return RedirectToAction(nameof(Index));
        }

        private void RegistrarHistorialCompra(Guid motoId,
            int? kmAceite, int? kmCadena, int? kmBalatas,
            int? kmLlantas, int? kmFiltro, int? kmValvulas)
        {
            if (kmAceite.HasValue)
                _mantenimientoService.Agregar(new Mantenimiento
                {
                    MotocicletaId = motoId, Tipo = "Cambio de aceite",
                    KilometrajeServicio = kmAceite.Value, Costo = 0,
                    Taller = "Historial de compra",
                    Descripcion = "Registrado al dar de alta como usada"
                });
            if (kmCadena.HasValue)
                _mantenimientoService.Agregar(new Mantenimiento
                {
                    MotocicletaId = motoId, Tipo = "Cadena",
                    KilometrajeServicio = kmCadena.Value, Costo = 0,
                    Taller = "Historial de compra",
                    Descripcion = "Registrado al dar de alta como usada"
                });
            if (kmBalatas.HasValue)
                _mantenimientoService.Agregar(new Mantenimiento
                {
                    MotocicletaId = motoId, Tipo = "Balatas",
                    KilometrajeServicio = kmBalatas.Value, Costo = 0,
                    Taller = "Historial de compra",
                    Descripcion = "Registrado al dar de alta como usada"
                });
            if (kmLlantas.HasValue)
                _mantenimientoService.Agregar(new Mantenimiento
                {
                    MotocicletaId = motoId, Tipo = "Llantas",
                    KilometrajeServicio = kmLlantas.Value, Costo = 0,
                    Taller = "Historial de compra",
                    Descripcion = "Registrado al dar de alta como usada"
                });
            if (kmFiltro.HasValue)
                _mantenimientoService.Agregar(new Mantenimiento
                {
                    MotocicletaId = motoId, Tipo = "Filtro de aire",
                    KilometrajeServicio = kmFiltro.Value, Costo = 0,
                    Taller = "Historial de compra",
                    Descripcion = "Registrado al dar de alta como usada"
                });
            if (kmValvulas.HasValue)
                _mantenimientoService.Agregar(new Mantenimiento
                {
                    MotocicletaId = motoId, Tipo = "Válvulas",
                    KilometrajeServicio = kmValvulas.Value, Costo = 0,
                    Taller = "Historial de compra",
                    Descripcion = "Registrado al dar de alta como usada"
                });
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
