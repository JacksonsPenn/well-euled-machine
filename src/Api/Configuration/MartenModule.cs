using HP.Services.Common;
using Marten;
using Serilog;

namespace HP.Data.Infrastructure.Marten;

public class MartenModule : Module, IMartenModule
{
    public virtual void SetStoreOptions(StoreOptions options)
    {
        Log.Information($"{nameof(this.GetType)} has no Marten Store Options configured");  
    }
}