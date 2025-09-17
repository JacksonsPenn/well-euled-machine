using System;
using Api.Models;
using Api.Models.Enums;

namespace Api.Events;

public interface ICalculationEvent
{
    Guid CalculationId { get; }
    CalculationType Type { get; }
    DateTime OccurredAt { get; }
}