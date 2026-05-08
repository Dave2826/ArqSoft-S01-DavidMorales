using Catalogo.Models;
using Catalogo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IRepository<Item> _repository;

        public CatalogoController(IRepository<Item> repository)
        {
            _repository = repository;
        }

        // ================= INDEX =================

        public IActionResult Index(string? marca)
        {
            var resultado = string.IsNullOrWhiteSpace(marca)
                ? _items
                : _items.Where(i =>
                    i.Marca.Equals(marca,
                    StringComparison.OrdinalIgnoreCase))
                    .ToList();

            ViewBag.Marcas = _items
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
            var item = _items.FirstOrDefault(i => i.Id == id);

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

            item.Id = _items.Count + 1;

            item.Nombre = item.Nombre?.Trim() ?? "";
            item.Marca = item.Marca?.Trim() ?? "";
            item.Tipo = item.Tipo?.Trim() ?? "";
            item.Descripcion = item.Descripcion?.Trim() ?? "";

            _items.Add(item);

            return RedirectToAction("Index");
        }
    }
}