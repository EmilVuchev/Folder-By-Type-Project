namespace Blog.Extensions
{
    using static GlobalConstants.Configurations;

    public static class ConfigurationExtensions
    {
        public static string? GetDefaultConnection(this IConfiguration configuration) 
            => configuration.GetConnectionString(DefaultConnection);

        public static string ThrowExceptionForNotFoundDefaultConnection(this IConfiguration configuration)
            => throw new InvalidOperationException(DefaultConnectionError);

    }
}
