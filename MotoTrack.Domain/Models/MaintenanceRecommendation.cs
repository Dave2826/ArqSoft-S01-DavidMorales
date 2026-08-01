using MotoTrack.Domain.Enums;

namespace MotoTrack.Domain.Models;

public record MaintenanceRecommendation(
    MaintenanceType Type,
    int RecommendedIntervalKm
);
