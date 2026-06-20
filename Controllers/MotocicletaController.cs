using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using MotoTrack.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
{
    public class MotocicletaController : Controller
    {
        private readonly MotocicletaService _motocicletaService;
        private readonly ConfiguracionMantenimientoService _configuracionService;
        private readonly MantenimientoService _mantenimientoService;
        private readonly IWebHostEnvironment _env;

        public MotocicletaController(
            MotocicletaService motocicletaService,
            ConfiguracionMantenimientoService configuracionService,
            MantenimientoService mantenimientoService,
            IWebHostEnvironment env)
        {
            _motocicletaService = motocicletaService;
            _configuracionService = configuracionService;
            _mantenimientoService = mantenimientoService;
            _env = env;
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

            var estados = new Dictionary<Guid, EstadoMantenimientoResult>();

            foreach (var moto in motos)
            {
                var mantenimientos = _mantenimientoService.ObtenerPorMotocicleta(moto.Id);
                var config = _configuracionService.ObtenerPorMotocicleta(moto.Id);
                estados[moto.Id] = CalculadorEstadoMantenimiento.Calcular(moto, mantenimientos, config);
            }

            ViewData["Estados"] = estados;

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
        public IActionResult Crear(Motocicleta motocicleta, IFormFile? foto,
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

            if (foto != null && foto.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(foto.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError("foto", "Formato no válido (jpg, jpeg, png, webp).");
                    return View(motocicleta);
                }
                if (foto.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("foto", "La imagen no debe superar 5 MB.");
                    return View(motocicleta);
                }

                var fileName = $"{motocicleta.Id}{ext}";
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "motos");
                Directory.CreateDirectory(uploadsDir);

                using (var stream = new FileStream(Path.Combine(uploadsDir, fileName), FileMode.Create))
                {
                    foto.CopyTo(stream);
                }

                motocicleta.FotoUrl = $"/uploads/motos/{fileName}";
            }

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
        public IActionResult Editar(Motocicleta motocicleta, IFormFile? foto, bool? eliminarFoto)
        {
            if (!ModelState.IsValid)
            {
                return View(motocicleta);
            }

            var motoActual = _motocicletaService.ObtenerPorId(motocicleta.Id);

            if (eliminarFoto == true)
            {
                if (!string.IsNullOrEmpty(motoActual?.FotoUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "uploads", "motos", Path.GetFileName(motoActual.FotoUrl));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                motocicleta.FotoUrl = null;
            }
            else if (foto != null && foto.Length > 0)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(foto.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError("foto", "Formato no válido (jpg, jpeg, png, webp).");
                    motocicleta.FotoUrl = motoActual?.FotoUrl;
                    return View(motocicleta);
                }
                if (foto.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("foto", "La imagen no debe superar 5 MB.");
                    motocicleta.FotoUrl = motoActual?.FotoUrl;
                    return View(motocicleta);
                }

                var oldUrl = motoActual?.FotoUrl;
                if (!string.IsNullOrEmpty(oldUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "uploads", "motos", Path.GetFileName(oldUrl));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = $"{motocicleta.Id}{ext}";
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "motos");
                Directory.CreateDirectory(uploadsDir);

                using (var stream = new FileStream(Path.Combine(uploadsDir, fileName), FileMode.Create))
                {
                    foto.CopyTo(stream);
                }

                motocicleta.FotoUrl = $"/uploads/motos/{fileName}";
            }
            else
            {
                motocicleta.FotoUrl = motoActual?.FotoUrl;
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

            if (!string.IsNullOrEmpty(motocicleta.FotoUrl))
            {
                var filePath = Path.Combine(_env.WebRootPath, "uploads", "motos", Path.GetFileName(motocicleta.FotoUrl));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            _motocicletaService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
