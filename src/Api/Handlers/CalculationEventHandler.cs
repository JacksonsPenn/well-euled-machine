using Api.Events;
using Api.Models;
using Api.Services;
using Api.Services.Abstractions;
using Marten;
using System.Threading.Tasks;
using Wolverine;

namespace Api.Handlers;
public class CalculationEventHandler
{
    private readonly ICalculationService _calculationService;

    public CalculationEventHandler(ICalculationService calculationService)
    {
        _calculationService = calculationService;
    }

    // Wolverine handler for CalculationRequested event
    public async Task Handle(CalculationRequested evt, IDocumentSession session)
    {
        var output = await _calculationService.EvaluateAsync(evt.Type, evt.Input);

        var completedEvent = new CalculationCompleted(evt.CalculationId, evt.Type, evt.Input, output);

        // Store result as document
        var calc = new AlgebraCalculation
        {
            Id = evt.CalculationId,
            Type = evt.Type,
            CreatedAt = evt.OccurredAt,
            Input = evt.Input,
            Output = output
        };

        session.Store(calc);
        await session.SaveChangesAsync();

        // Publish completed event (if you want to chain further actions)
        // await context.PublishAsync(completedEvent); // Uncomment if using Wolverine context

        // Optionally log or handle errors
    }
}