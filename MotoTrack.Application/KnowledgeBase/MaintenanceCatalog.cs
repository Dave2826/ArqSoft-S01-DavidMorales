using MotoTrack.Domain.Enums;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.KnowledgeBase;

public static class MaintenanceCatalog
{
    private static readonly IReadOnlyList<MaintenanceKnowledgeEntry> _entries =
    [
        new(
            MaintenanceType.Aceite,
            3000, 500,
            "Recomendación inicial utilizada por MotoTrack para motocicletas de uso urbano.",
            "2500–5000 km",
            "Valor conservador ampliamente recomendado por fabricantes y talleres especializados.",
            "Puede variar según tipo de aceite y condiciones de uso.",
            "Manuales de servicio: Honda, Yamaha, Suzuki — intervalos 3000–5000 km."
        ),
        new(
            MaintenanceType.Cadena,
            20000, 1000,
            "Recomendación inicial para mantenimiento de cadena de transmisión.",
            "15000–25000 km",
            "Intervalo estándar para cadenas selladas en motocicletas de media cilindrada.",
            "La lubricación frecuente puede extender la vida útil de la cadena.",
            "Manuales de servicio: Kawasaki, Yamaha — intervalos 18000–25000 km."
        ),
        new(
            MaintenanceType.Balatas,
            15000, 1000,
            "Recomendación inicial para reemplazo de balatas de freno.",
            "10000–20000 km",
            "Punto medio del rango típico de desgaste para conducción mixta.",
            "El desgaste real depende del estilo de conducción y tipo de freno.",
            "Manuales de servicio: Honda, Suzuki — intervalos 10000–20000 km."
        ),
        new(
            MaintenanceType.Llantas,
            25000, 2000,
            "Recomendación inicial para reemplazo de neumáticos.",
            "20000–30000 km",
            "Valor seguro para conducción mixta en carretera y ciudad.",
            "La presión adecuada y el tipo de superficie afectan la duración.",
            "Manuales de servicio: Michelin, Pirelli — rangos 20000–30000 km."
        ),
        new(
            MaintenanceType.FiltroAire,
            12000, 1000,
            "Recomendación inicial para reemplazo de filtro de aire.",
            "10000–15000 km",
            "Intervalo que coincide con cada tercer cambio de aceite en condiciones normales.",
            "Zonas con alto polvo pueden requerir reemplazos más frecuentes.",
            "Manuales de servicio: KTM, Yamaha — intervalos 10000–15000 km."
        ),
        new(
            MaintenanceType.Bujias,
            20000, 1000,
            "Recomendación inicial para reemplazo de bujías.",
            "15000–24000 km",
            "Valor estándar para bujías de iridio en motores de 4 tiempos.",
            "Las bujías de cobre tienen una vida útil menor.",
            "Manuales de servicio: NGK, Denso — rangos 15000–24000 km."
        ),
        new(
            MaintenanceType.Valvulas,
            24000, 2000,
            "Recomendación inicial para ajuste de válvulas.",
            "20000–26000 km",
            "Intervalo común en motores de fabricantes japoneses de 4 tiempos.",
            "El ajuste debe realizarse con el motor frío y siguiendo las especificaciones del fabricante.",
            "Manuales de servicio: Honda, Yamaha, Suzuki — intervalos 20000–26000 km."
        ),
        new(
            MaintenanceType.Bateria,
            24000, 2000,
            "Recomendación inicial para reemplazo de batería.",
            "20000–30000 km",
            "Vida útil típica de una batería de plomo-ácido en condiciones normales de uso.",
            "Las baterías de litio pueden durar más; el uso de cargadores inteligentes prolonga la vida útil.",
            "Manuales de servicio: Yuasa, GS — intervalos 20000–30000 km."
        ),
        new(
            MaintenanceType.Suspension,
            30000, 3000,
            "Recomendación inicial para servicio de suspensión delantera y trasera.",
            "25000–35000 km",
            "Intervalo estándar para cambio de aceite de horquilla y revisión de suspensiones.",
            "El uso en terrenos irregulares puede requerir servicio más frecuente.",
            "Manuales de servicio: Showa, KYB — intervalos 25000–35000 km."
        ),
        new(
            MaintenanceType.LiquidoFrenos,
            20000, 2000,
            "Recomendación inicial para reemplazo de líquido de frenos.",
            "15000–25000 km",
            "El líquido de frenos absorbe humedad con el tiempo, reduciendo su punto de ebullición.",
            "Se recomienda cambiar cada 2 años independientemente del kilometraje.",
            "Estándares DOT 4 — intervalos 15000–25000 km."
        ),
        new(
            MaintenanceType.Anticongelante,
            40000, 3000,
            "Recomendación inicial para reemplazo de anticongelante.",
            "30000–50000 km",
            "El anticongelante pierde sus propiedades protectoras con el tiempo y el uso.",
            "También recomendado cada 2–3 años independientemente del kilometraje.",
            "Manuales de servicio: Honda, Yamaha — intervalos 30000–50000 km."
        )
    ];

    public static IReadOnlyList<MaintenanceKnowledgeEntry> GetAll() => _entries;

    public static bool TryGet(MaintenanceType type, out MaintenanceKnowledgeEntry? entry)
    {
        foreach (var e in _entries)
        {
            if (e.Type == type)
            {
                entry = e;
                return true;
            }
        }

        entry = null;
        return false;
    }
}
