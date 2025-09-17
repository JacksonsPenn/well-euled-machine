using HP.Services.Common;
using Marten;

namespace HP.Data.Infrastructure.Marten
{
    public interface IMartenModule: IModule
    {
        void SetStoreOptions(StoreOptions options);
    }
}
