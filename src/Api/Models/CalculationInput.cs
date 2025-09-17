using System.Collections.Generic;

namespace Api.Models;

public class CalculationInput
{
    public Dictionary<string, object> Fields { get; set; } = new();
}