using StrawberryShake;
using System.Threading.Tasks;

namespace BlazorApp.Client.GraphQL.Mutations;

public class Mutations
{
    public async Task<IOperationResult<IExecuteGapResult>> ExecuteGap(IWellEuledClient wellEuledClient,string code)
    {
        return await wellEuledClient.ExecuteGap.ExecuteAsync(new ExecuteGapInput()
        {
            Code = code
        });
    }
}

