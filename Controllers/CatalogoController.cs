using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Controllers
{
    public class CatalogoController : Controller
    {
        private static List<Item> _items = new()
        {
            new Item { Id = 1, Nombre = "Kawasaki Ninja 400", Marca = "Kawasaki", Ano = 2018, Tipo = "Deportiva", Descripcion = "Ligera y ágil." },
            new Item { Id = 2, Nombre = "Yamaha R3", Marca = "Yamaha", Ano = 2020, Tipo = "Deportiva", Descripcion = "Buen balance potencia-control." },
            new Item { Id = 3, Nombre = "Honda CBR600RR", Marca = "Honda", Ano = 2022, Tipo = "Deportiva", Descripcion = "Rendimiento en pista." },
            new Item { Id = 4, Nombre = "Italika DM200", Marca = "Italika", Ano = 2023, Tipo = "Doble propósito", Descripcion = "Uso diario y caminos mixtos." },
            new Item { Id = 5, Nombre = "BMW GS 1250", Marca = "BMW", Ano = 2022, Tipo = "Adventure", Descripcion = "Viajes largos y off-road." }
        };

        public IActionResult Index(string? marca)
        {
            var resultado = string.IsNullOrEmpty(marca)
                ? _items
                : _items.Where(i => i.Marca == marca).ToList();

            ViewBag.Marcas = _items.Select(i => i.Marca).Distinct().ToList();
            ViewBag.MarcaActual = marca;

            return View(resultado);
        }

        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            if (!ModelState.IsValid)
                return View(item);

            item.Id = _items.Count + 1;
            _items.Add(item);

            return RedirectToAction("Index");
        }
    }
}