using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Models;
using Catalogo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly IItemRepository _repository;

        public CatalogoController()
        {
            _repository = new ItemRepository();
        }

        // ================= INDEX =================

        public IActionResult Index(string? marca)
        {
            var items = _repository.ObtenerTodos();

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
            var item = _repository.ObtenerPorId(id);

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

            item.Id = _repository.ObtenerTodos().Count + 1;

            item.Nombre = item.Nombre?.Trim() ?? "";
            item.Marca = item.Marca?.Trim() ?? "";
            item.Tipo = item.Tipo?.Trim() ?? "";
            item.Descripcion = item.Descripcion?.Trim() ?? "";
            item.ImagenUrl = item.ImagenUrl?.Trim() ?? "";

            _repository.Agregar(item);

            return RedirectToAction("Index");
        }

        // ================= EDITAR GET =================

        public IActionResult Editar(int id)
        {
            var item = _repository.ObtenerPorId(id);

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

            var existente = _repository.ObtenerPorId(item.Id);

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

            _repository.Editar(existente);

            return RedirectToAction("Index");
        }
    }
}