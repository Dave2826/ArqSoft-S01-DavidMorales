using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface ILecturaKilometrajeRepository
    {
        List<LecturaKilometraje> ObtenerTodas();

        List<LecturaKilometraje> ObtenerPorMotocicleta(Guid motocicletaId);

        LecturaKilometraje? ObtenerUltimaLectura(Guid motocicletaId);

        void Agregar(LecturaKilometraje lectura);
    }
}
