using System.Threading.Tasks;
using Api.Models;
using Api.Models.Enums;

namespace Api.Services.Abstractions;

public interface ICalculationService
{
    Task<CalculationOutput> EvaluateAsync(CalculationType type, CalculationInput input);
}