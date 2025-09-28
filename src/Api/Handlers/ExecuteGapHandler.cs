using Microsoft.AspNetCore.SignalR.Protocol;
using Api.Services.Abstractions;
using Api.Models;

namespace Api.Handlers;



public record ExecuteGapCommand(string Code);


public class ExecuteGapHandler
{
    private readonly IGapKernelClient _gapClient;

    public ExecuteGapHandler(IGapKernelClient gapClient)
    {
        _gapClient = gapClient;
    }

    public async Task<GapResult> Handle(ExecuteGapCommand command)
    {
        var result = await _gapClient.ExecuteAsync(command.Code);
        return new GapResult(result);
    }
}
