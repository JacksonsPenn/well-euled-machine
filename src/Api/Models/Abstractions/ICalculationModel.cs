using System;
using Api.Models.Enums;

namespace Api.Models.Abstractions;

public interface ICalculationModel
{
    Guid Id { get; set; }
    CalculationType Type { get; set; }
    DateTime CreatedAt { get; set; }
    CalculationInput Input { get; set; }
    CalculationOutput? Output { get; set; }
}