using System.ComponentModel.DataAnnotations;

namespace Catalogo.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = "";

        [Required]
        public string Marca { get; set; } = "";

        [Required]
        public string Tipo { get; set; } = "";

        public int Ano { get; set; }

        [Required]
        public string Descripcion { get; set; } = "";
    }
}