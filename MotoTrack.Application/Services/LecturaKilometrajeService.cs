using MotoTrack.Domain.Interfaces;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.Services
{
    public class LecturaKilometrajeService
    {
        private readonly ILecturaKilometrajeRepository
            _lecturaRepository;

        public LecturaKilometrajeService(
            ILecturaKilometrajeRepository lecturaRepository)
        {
            _lecturaRepository = lecturaRepository;
        }

        public List<LecturaKilometraje>
            ObtenerPorMotocicleta(Guid motocicletaId)
        {
            return _lecturaRepository
                .ObtenerPorMotocicleta(motocicletaId);
        }

        public LecturaKilometraje?
            ObtenerUltimaLectura(Guid motocicletaId)
        {
            return _lecturaRepository
                .ObtenerUltimaLectura(motocicletaId);
        }

        public void Agregar(
            LecturaKilometraje lectura)
        {
            _lecturaRepository
                .Agregar(lectura);
        }
    }
}
