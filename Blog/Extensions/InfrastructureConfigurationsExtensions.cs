namespace Blog.Extensions
{
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
    }
}
