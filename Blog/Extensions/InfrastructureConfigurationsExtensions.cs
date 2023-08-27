namespace Blog.Extensions
{
    using Services.Data.Common;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public static class InfrastructureConfigurationsExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
            => services
                .AddDbContext<BlogDbContext>(
                    options => options.UseSqlServer(
                        connectionString,
                        b => b.MigrationsAssembly(typeof(BlogDbContext).Assembly.FullName)));

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var serviceType = typeof(IService);
            var scopedServiceType = typeof(IScoped);
            var singletonServiceType = typeof(ISingleton);

            var serviceTypes = serviceType
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t,
                    Interface = t.GetInterface($"I{t.Name}")
                })
                .Where(t => t.Interface != null)
                .ToList();

            foreach (var type in serviceTypes)
            {
                if (serviceType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Interface, type.Service);
                }
                else if (scopedServiceType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Interface, type.Service);
                }
                else if(singletonServiceType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Interface, type.Service);
                }
            }

            return services;
        }
    }
}
