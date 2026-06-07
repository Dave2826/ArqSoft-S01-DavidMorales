using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _itemService;

        public CatalogoController(ItemService itemService)
        {
            _itemService = itemService;
        }

        // ================= INDEX =================

        public IActionResult Index(string? marca)
        {
            var items = _itemService.ObtenerTodos();

            var resultado = string.IsNullOrWhiteSpace(marca)
                ? items
                : items.Where(i =>
                    i.Marca.Equals(marca,
                    StringComparison.OrdinalIgnoreCase))
                    .ToList();

            ViewBag.Marcas = items
                .Select(i => i.Marca.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(m => m)
                .ToList();

            ViewBag.MarcaActual = marca;

            return View(resultado);
        }

        // ================= DETALLE =================

        public IActionResult Detalle(int id)
        {
            var item = _itemService.ObtenerPorId(id);

            return item == null
                ? NotFound()
                : View(item);
        }

        // ================= AGREGAR GET =================

        public IActionResult Agregar()
        {
            return View();
        }

        // ================= AGREGAR POST =================

        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            item.Id = _itemService.ObtenerTodos().Count + 1;

            item.Nombre = item.Nombre?.Trim() ?? "";
            item.Marca = item.Marca?.Trim() ?? "";
            item.Tipo = item.Tipo?.Trim() ?? "";
            item.Descripcion = item.Descripcion?.Trim() ?? "";
            item.ImagenUrl = item.ImagenUrl?.Trim() ?? "";

            _itemService.Agregar(item);

            return RedirectToAction("Index");
        }

        // ================= EDITAR GET =================

        public IActionResult Editar(int id)
        {
            var item = _itemService.ObtenerPorId(id);

            return item == null
                ? NotFound()
                : View(item);
        }

        // ================= EDITAR POST =================

        [HttpPost]
        public IActionResult Editar(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            var existente = _itemService.ObtenerPorId(item.Id);

            if (existente == null)
            {
                return NotFound();
            }

            existente.Nombre = item.Nombre?.Trim() ?? "";
            existente.Marca = item.Marca?.Trim() ?? "";
            existente.Tipo = item.Tipo?.Trim() ?? "";
            existente.Ano = item.Ano;
            existente.Descripcion = item.Descripcion?.Trim() ?? "";
            existente.Cilindrada = item.Cilindrada?.Trim() ?? "";
            existente.Potencia = item.Potencia?.Trim() ?? "";
            existente.VelocidadMax = item.VelocidadMax?.Trim() ?? "";
            existente.Peso = item.Peso?.Trim() ?? "";
            existente.Transmision = item.Transmision?.Trim() ?? "";
            existente.CapacidadTanque = item.CapacidadTanque?.Trim() ?? "";
            existente.TipoMotor = item.TipoMotor?.Trim() ?? "";
            existente.ImagenUrl = item.ImagenUrl?.Trim() ?? "";

            _itemService.Editar(existente);

            return RedirectToAction("Index");
        }
    }
}
