using Api.Models.Abstractions;
using Api.Models.Enums;
using System;

namespace Api.Models;

public class AlgebraCalculation : ICalculationModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public CalculationType Type { get; set; } = CalculationType.Algebra;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public CalculationInput Input { get; set; } = new();
    public CalculationOutput? Output { get; set; }
}