using Microsoft.Extensions.DependencyInjection;

namespace HP.Common.Configuration.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IAppConfiguration config)
        {
            services.Configure<Settings>(!config.IsDefaultConfig
                ? config.Configuration.GetSection($"{config.CurrentEnvironment}:backend")
                : config.Configuration.GetSection("ConfigurationSettings"));

            return services;
        }
    }
}
