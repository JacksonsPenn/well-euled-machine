using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Marten;
using Api.Models;
using Api.Events;
using Wolverine;
using Api.Models.Enums;

namespace Api.GraphQL.Mutations;

public class CalculationMutation
{
    public async Task<AlgebraCalculation> SubmitCalculation(
        string expression,
        [Service] IMessageBus bus)
    {
        var calculationId = Guid.NewGuid();
        var input = new CalculationInput();
        input.Fields.Add("expression", expression);

        var evt = new CalculationRequested(calculationId, CalculationType.Algebra, input);

        await bus.PublishAsync(evt);

        // Return a placeholder, actual result will be available after handler runs
        return new AlgebraCalculation
        {
            Id = calculationId,
            Type = CalculationType.Algebra,
            CreatedAt = DateTime.UtcNow,
            Input = input
        };
    }
}
