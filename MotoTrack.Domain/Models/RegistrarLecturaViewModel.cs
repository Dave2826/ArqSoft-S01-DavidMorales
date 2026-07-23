using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class RegistrarLecturaViewModel
    {
        public Guid MotocicletaId { get; set; }

        [Required]
        [Display(Name = "Kilometraje Actual")]
        public int Kilometraje { get; set; }

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }
    }
}
