using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private static List<Item> _items = new List<Item>
        {
            new Item
            {
                Id = 1,
                Nombre = "Kawasaki Ninja 400",
                Marca = "Kawasaki",
                Tipo = "Deportiva",
                Ano = 2018,
                Descripcion = "Motocicleta deportiva ligera y rápida."
            },

            new Item
            {
                Id = 2,
                Nombre = "Yamaha R3",
                Marca = "Yamaha",
                Tipo = "Deportiva",
                Ano = 2020,
                Descripcion = "Excelente moto para ciudad y carretera."
            },

            new Item
            {
                Id = 3,
                Nombre = "Honda CBR600RR",
                Marca = "Honda",
                Tipo = "Deportiva",
                Ano = 2022,
                Descripcion = "Motocicleta supersport de alto rendimiento."
            },

            new Item
            {
                Id = 4,
                Nombre = "Italika DM200",
                Marca = "Italika",
                Tipo = "Doble propósito",
                Ano = 2023,
                Descripcion = "Ideal para ciudad y caminos irregulares."
            },

            new Item
            {
                Id = 5,
                Nombre = "BMW S1000RR",
                Marca = "BMW",
                Tipo = "Superbike",
                Ano = 2021,
                Descripcion = "Moto premium con gran potencia."
            }
        };

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