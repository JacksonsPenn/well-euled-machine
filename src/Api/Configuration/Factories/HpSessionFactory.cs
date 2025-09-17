using Api.Configuration;
using Api.Extensions;
using Marten;

// ReSharper disable ClassNeverInstantiated.Global

namespace HP.Data.Infrastructure.Marten.Factories
{
    public class HpSessionFactory(
        IDocumentStore store,
        IServiceProvider serviceProvider,
        IAppConfiguration appConfiguration)
        : ISessionFactory
    {
        private readonly string? _tenantId = serviceProvider.TenantId(appConfiguration);

        // This is important! You will need to use the
        // IDocumentStore to open sessions

        public IQuerySession QuerySession()
        {
            return !string.IsNullOrEmpty(_tenantId) ? store.QuerySession(_tenantId!) : store.QuerySession();
        }

        public IDocumentSession OpenSession()
        {
            return !string.IsNullOrEmpty(_tenantId) ? store.LightweightSession(_tenantId!) : store.LightweightSession();
        }
    }
}
