using System.ComponentModel.DataAnnotations;

namespace MotoTrack.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del modelo")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "La marca es obligatoria")]
        [Display(Name = "Marca")]
        public string Marca { get; set; } = "";

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = "";

        [Required(ErrorMessage = "El año es obligatorio")]
        [Display(Name = "Año")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = "";

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [Display(Name = "URL de imagen")]
        public string ImagenUrl { get; set; } = "";


        // --- Ficha técnica ---

        [Required(ErrorMessage = "La cilindrada es obligatoria")]
        [Display(Name = "Cilindrada")]
        public string Cilindrada { get; set; } = "";

        [Required(ErrorMessage = "La potencia es obligatoria")]
        [Display(Name = "Potencia")]
        public string Potencia { get; set; } = "";

        [Required(ErrorMessage = "La velocidad máxima es obligatoria")]
        [Display(Name = "Velocidad máxima")]
        public string VelocidadMax { get; set; } = "";

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Display(Name = "Peso")]
        public string Peso { get; set; } = "";

        [Required(ErrorMessage = "La transmisión es obligatoria")]
        [Display(Name = "Transmisión")]
        public string Transmision { get; set; } = "";

        [Required(ErrorMessage = "La capacidad del tanque es obligatoria")]
        [Display(Name = "Capacidad del tanque")]
        public string CapacidadTanque { get; set; } = "";

        [Required(ErrorMessage = "El tipo de motor es obligatorio")]
        [Display(Name = "Tipo de motor")]
        public string TipoMotor { get; set; } = "";
    }
}
