namespace MotoTrack.Domain.Models;

public class MaintenanceStatusResult
{
    public string Estado { get; set; } = "Sin registro";
    public string Color { get; set; } = "gray";
    public string Mensaje { get; set; } = "";
    public int KilometrosRestantes { get; set; }
    public int ProximoServicio { get; set; }
}
