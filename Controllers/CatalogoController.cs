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
                Descripcion = "La Ninja 400 es pura adrenalina contenida en un chasis ligero. Perfecta para quienes buscan una deportiva ágil con alma de pistera y estilo agresivo que no pasa desapercibido en ninguna curva.",
                Cilindrada = "399 cc",
                Potencia = "45 HP @ 10,000 rpm",
                VelocidadMax = "190 km/h",
                Peso = "168 kg",
                Transmision = "6 velocidades",
                CapacidadTanque = "14 L",
                TipoMotor = "Bicilíndrico en paralelo",
                ImagenUrl = "/images/Kawasaki Ninja 400.png"
            },

            new Item
            {
                Id = 2,
                Nombre = "Yamaha R3",
                Marca = "Yamaha",
                Tipo = "Deportiva",
                Ano = 2020,
                Descripcion = "La R3 es la dosis justa de potencia y control. Diseñada para dominar el asfalto con precisión quirúrgica, su respuesta inmediata y postura deportiva te meten de lleno en la experiencia supersport desde el primer puño.",
                Cilindrada = "321 cc",
                Potencia = "42 HP @ 10,750 rpm",
                VelocidadMax = "180 km/h",
                Peso = "169 kg",
                Transmision = "6 velocidades",
                CapacidadTanque = "14 L",
                TipoMotor = "Bicilíndrico en paralelo",
                ImagenUrl = "/images/Yamaha R3.png"
            },

            new Item
            {
                Id = 3,
                Nombre = "Honda CBR600RR",
                Marca = "Honda",
                Tipo = "Deportiva",
                Ano = 2022,
                Descripcion = "La CBR600RR no pide permiso: acelera, tracciona y dobla como una máquina de carreras afinada al milímetro. Su ADN de competición se siente en cada recta —si buscas emociones fuertes, esta es tu máquina.",
                Cilindrada = "599 cc",
                Potencia = "118 HP @ 13,500 rpm",
                VelocidadMax = "260 km/h",
                Peso = "194 kg",
                Transmision = "6 velocidades",
                CapacidadTanque = "18 L",
                TipoMotor = "Tetracilíndrico en línea",
                ImagenUrl = "/images/Honda CBR600RR.png"
            },

            new Item
            {
                Id = 4,
                Nombre = "Italika DM200",
                Marca = "Italika",
                Tipo = "Doble propósito",
                Ano = 2023,
                Descripcion = "La DM200 te saca del asfalto sin excusas. Robusta, sencilla y rendidora, está hecha para el rider que quiere llegar a donde otros no pueden —del tráfico urbano a la terracería en un mismo viaje.",
                Cilindrada = "200 cc",
                Potencia = "16 HP @ 8,000 rpm",
                VelocidadMax = "110 km/h",
                Peso = "131 kg",
                Transmision = "5 velocidades",
                CapacidadTanque = "11 L",
                TipoMotor = "Monocilíndrico 4T",
                ImagenUrl = "/images/Italika DM200.png"
            },

            new Item
            {
                Id = 5,
                Nombre = "BMW S1000RR",
                Marca = "BMW",
                Tipo = "Superbike",
                Ano = 2021,
                Descripcion = "La S1000RR es ingeniería alemana al servicio de la velocidad. Con un rugido que eriza la piel y tecnología de punta que te mantiene pegado al suelo, cada salida se convierte en una experiencia de otro nivel.",
                Cilindrada = "999 cc",
                Potencia = "205 HP @ 13,500 rpm",
                VelocidadMax = "305 km/h",
                Peso = "197 kg",
                Transmision = "6 velocidades",
                CapacidadTanque = "16.5 L",
                TipoMotor = "Tetracilíndrico en línea",
                ImagenUrl = "/images/BMW S1000RR.png"
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
            item.ImagenUrl = item.ImagenUrl?.Trim() ?? "";

            _items.Add(item);

            return RedirectToAction("Index");
        }

        // ================= EDITAR GET =================

        public IActionResult Editar(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);

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

            var existente = _items.FirstOrDefault(i => i.Id == item.Id);

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

            return RedirectToAction("Index");
        }
    }
}
