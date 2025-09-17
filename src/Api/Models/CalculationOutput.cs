using System.Collections.Generic;

namespace Api.Models;

public class CalculationOutput
{
    public Dictionary<string, object> Fields { get; set; } = new();
    public List<string>? Steps { get; set; }
    public string? GraphUrl { get; set; }
    public string? ErrorMessage { get; set; }
}