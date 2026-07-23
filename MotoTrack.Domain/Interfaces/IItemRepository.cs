using MotoTrack.Domain.Models;

namespace MotoTrack.Domain.Interfaces
{
    public interface IItemRepository
    {
        List<Item> ObtenerTodos();
        Item? ObtenerPorId(int id);
        void Agregar(Item item);
        void Editar(Item item);
    }
}
