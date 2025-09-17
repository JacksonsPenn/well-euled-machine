using System;
using Api.Models;
using Api.Models.Abstractions;
using Api.Models.Enums;

namespace Api.Events;

public class CalculationRequested : ICalculationEvent
{
    public Guid CalculationId { get; }
    public CalculationType Type { get; }
    public DateTime OccurredAt { get; }
    public CalculationInput Input { get; }

    public CalculationRequested(Guid calculationId, CalculationType type, CalculationInput input)
    {
        CalculationId = calculationId;
        Type = type;
        Input = input;
        OccurredAt = DateTime.UtcNow;
    }
}