using MotoTrack.Domain.Enums;

namespace MotoTrack.Domain.Models;

public record MaintenanceKnowledgeEntry(
    MaintenanceType Type,
    int RecommendedIntervalKm,
    int WarningThresholdKm,
    string Description,
    string RangeFound,
    string Justification,
    string Observations,
    string? SourceReference
);
