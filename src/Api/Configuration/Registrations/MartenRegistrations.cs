using HP.Common.Configuration;
using Marten;
using Marten.NodaTimePlugin;
using Marten.Schema;
using Marten.Storage;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Weasel.Core;
using Wolverine.Http.Marten;
using Wolverine.Marten;

namespace HP.Data.Infrastructure.Marten.Registrations
{
    //ToDo: I am not sure if this will live here. 
    public static class MartenRegistrations
    {
        public static IServiceCollection AddMartenModules(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo<IMartenModule>())
                .AsSelfWithInterfaces()
                .WithLifetime(ServiceLifetime.Scoped)
            );

            return services;
        }

        public static IServiceCollection AddMartenModulesFromType(this IServiceCollection services, List<Type> types)
        {
            services.Scan(scan => scan
                .FromTypes(types.ToArray())
                .AddClasses(classes => classes.AssignableTo<IMartenModule>().Where(c => !c.IsAbstract))
                .AsImplementedInterfaces()
                .WithLifetime(ServiceLifetime.Scoped)
            );

            return services;
        }

        /// <summary>
        /// Register individual instance of an <see cref="IMartenModule"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMartenModule<T>(this IServiceCollection services) where T : class, IMartenModule
        {
            services.AddScoped<IMartenModule,T>();

            return services;
        }

        public static IServiceCollection AddMartenDb(this IServiceCollection services, IAppConfiguration config, StoreOptions? options = default)
        {
            var seedData = new List<IInitialData>();

            if (config.IsDefaultConfig || config.CurrentEnvironment.ToLower().Contains("local"))
            {
                Log.Information($"Seeding Marten DB");

                seedData.Add(new SeedData(PracticeData.AllByConfig(config)));
            }

            services
                .AddMarten(sp =>
                {
                    var martenModules = sp.GetServices<IMartenModule>().ToList();

                    var storeOptions = SetStoreOptions(sp, config, options);

                    foreach (var martenModule in martenModules)
                    {
                        martenModule.SetStoreOptions(storeOptions);
                    }

                    return storeOptions;
                })
                .IntegrateWithWolverine()
                .InitializeWith(seedData.ToArray());

            services.AddMartenTenancyDetection(o =>
            {
                o.IsRequestHeaderValue("tenant-id");
            });

            return services;
        }

        private static StoreOptions SetStoreOptions(IServiceProvider serviceProvider, IAppConfiguration config,
            StoreOptions? options = null)
        {
            var opts = options;

            opts ??= new StoreOptions();

            // Other Configuration
            opts.Connection(config.Settings.Marten.ConnectionString);
            opts.AutoCreateSchemaObjects = AutoCreate.All;
            opts.Events.TenancyStyle = TenancyStyle.Conjoined;
            opts.UseNewtonsoftForSerialization(enumStorage: EnumStorage.AsString);
            opts.UseNodaTime();
            opts.DisableNpgsqlLogging = true;
            return opts;
        }
    }
}