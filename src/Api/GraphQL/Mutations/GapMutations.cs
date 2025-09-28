using Api.Handlers;
using Wolverine;
using Api.Models;

namespace Api.GraphQL.Mutations;

[MutationType]
public class GapMutations
{
    private readonly IMessageBus _bus;

    public GapMutations(IMessageBus bus)
    {
        _bus = bus;
    }

    public async Task<GapResult> ExecuteGap(ExecuteGapInput input)
    {
        // Use your Wolverine command pipeline
        var result = await _bus.InvokeAsync<GapResult>(new ExecuteGapCommand(input.Code));
        return result;
    }
}
