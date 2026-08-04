using MotoTrack.Domain.Models;
using MotoTrack.Domain.Interfaces;

namespace MotoTrack.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public List<Item> ObtenerTodos()
        {
            return _itemRepository.ObtenerTodos();
        }

        public Item? ObtenerPorId(int id)
        {
            return _itemRepository.ObtenerPorId(id);
        }

        public void Agregar(Item item)
        {
            _itemRepository.Agregar(item);
        }

        public void Editar(Item item)
        {
            _itemRepository.Editar(item);
        }
    }
}
