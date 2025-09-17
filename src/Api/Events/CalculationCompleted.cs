using System;
using Api.Models;
using Api.Models.Abstractions;
using Api.Models.Enums;

namespace Api.Events;

public class CalculationCompleted : ICalculationEvent
{
    public Guid CalculationId { get; }
    public CalculationType Type { get; }
    public DateTime OccurredAt { get; }
    public CalculationOutput Output { get; }

    public CalculationCompleted(Guid calculationId, CalculationType type, CalculationInput input, CalculationOutput output)
    {
        CalculationId = calculationId;
        Type = type;
        Output = output;
        OccurredAt = DateTime.UtcNow;
    }
}