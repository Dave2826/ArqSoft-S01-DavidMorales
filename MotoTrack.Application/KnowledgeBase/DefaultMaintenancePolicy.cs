using MotoTrack.Domain.Enums;
using MotoTrack.Domain.Models;

namespace MotoTrack.Application.KnowledgeBase;

public static class DefaultMaintenancePolicy
{
    private sealed record DisplacementRange(
        int MinCilindrada,
        int? MaxCilindrada,
        IReadOnlyList<MaintenanceRecommendation> Recommendations);

    private static readonly IReadOnlyList<DisplacementRange> _ranges =
    [
        new(
            1, 200,
            [
                new(MaintenanceType.Aceite, 3000),
                new(MaintenanceType.Cadena, 1000),
                new(MaintenanceType.FiltroAire, 5000),
                new(MaintenanceType.Bujias, 8000),
                new(MaintenanceType.Balatas, 8000),
                new(MaintenanceType.Llantas, 12000)
            ]
        ),
        new(
            201, 400,
            [
                new(MaintenanceType.Aceite, 4000),
                new(MaintenanceType.Cadena, 1000),
                new(MaintenanceType.FiltroAire, 6000),
                new(MaintenanceType.Bujias, 10000),
                new(MaintenanceType.Balatas, 10000),
                new(MaintenanceType.Llantas, 15000)
            ]
        ),
        new(
            401, 900,
            [
                new(MaintenanceType.Aceite, 5000),
                new(MaintenanceType.Cadena, 1000),
                new(MaintenanceType.FiltroAire, 8000),
                new(MaintenanceType.Bujias, 12000),
                new(MaintenanceType.Balatas, 12000),
                new(MaintenanceType.Llantas, 18000)
            ]
        ),
        new(
            901, null,
            [
                new(MaintenanceType.Aceite, 6000),
                new(MaintenanceType.Cadena, 1000),
                new(MaintenanceType.FiltroAire, 10000),
                new(MaintenanceType.Bujias, 12000),
                new(MaintenanceType.Balatas, 12000),
                new(MaintenanceType.Llantas, 20000)
            ]
        )
    ];

    public static IReadOnlyList<MaintenanceRecommendation> ObtenerRecomendaciones(int cilindrada)
    {
        if (cilindrada <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cilindrada),
                "La cilindrada debe ser un valor positivo.");
        }

        foreach (var rango in _ranges)
        {
            if (rango.MinCilindrada <= cilindrada
                && (rango.MaxCilindrada is null || cilindrada <= rango.MaxCilindrada))
            {
                return rango.Recommendations;
            }
        }

        throw new InvalidOperationException("No se encontró una plantilla para la cilindrada indicada.");
    }
}
