using Api.Models;
using Api.Models.Enums;
using Api.Services.Abstractions;
using System.Threading.Tasks;

namespace Api.Services;

public class CalculationService : ICalculationService
{
    public async Task<CalculationOutput> EvaluateAsync(CalculationType type, CalculationInput input)
    {
        // TODO: Use calculationType to select proper engine, parse input, compute result
        // Example for algebra:
        if (type == CalculationType.Algebra && input.Fields.TryGetValue("expression", out var exprObj))
        {
            var expression = exprObj?.ToString() ?? "";
            // Placeholder: Implement Math.NET or AngouriMath integration here
            // For now, just echo back as result
            return new CalculationOutput
            {
                Fields = new Dictionary<string, object> { { "result", $"Evaluated: {expression}" } },
                Steps = new List<string> { "Received expression", $"Evaluated: {expression}" }
            };
        }
        // Add additional calculation types/engines here
        return new CalculationOutput { ErrorMessage = "Unsupported calculation type or missing input." };
    }
}