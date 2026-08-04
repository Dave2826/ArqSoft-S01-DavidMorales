using MotoTrack.Application.Services;
using MotoTrack.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MotoTrack.Controllers.Api
{
    [Route("api/motocicletas")]
    [ApiController]
    public class MotocicletasApiController : ControllerBase
    {
        private readonly MotocicletaService _motocicletaService;

        public MotocicletasApiController(MotocicletaService motocicletaService)
        {
            _motocicletaService = motocicletaService;
        }

        [HttpGet]
        public ActionResult<List<Motocicleta>> GetAll()
        {
            return _motocicletaService.ObtenerTodas();
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Motocicleta> GetById(Guid id)
        {
            var moto = _motocicletaService.ObtenerPorId(id);
            if (moto == null)
                return NotFound();
            return moto;
        }

        [HttpPost]
        public ActionResult<Motocicleta> Create([FromBody] Motocicleta motocicleta)
        {
            if (motocicleta.Id == Guid.Empty)
                motocicleta.Id = Guid.NewGuid();

            motocicleta.FechaRegistro = DateTime.Now;

            _motocicletaService.Agregar(motocicleta);
            return CreatedAtAction(nameof(GetById), new { id = motocicleta.Id }, motocicleta);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Motocicleta motocicleta)
        {
            if (id != motocicleta.Id)
                return BadRequest("El ID de la ruta no coincide con el ID del objeto.");

            var existing = _motocicletaService.ObtenerPorId(id);
            if (existing == null)
                return NotFound();

            _motocicletaService.Actualizar(motocicleta);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var existing = _motocicletaService.ObtenerPorId(id);
            if (existing == null)
                return NotFound();

            _motocicletaService.Eliminar(id);
            return NoContent();
        }
    }
}
