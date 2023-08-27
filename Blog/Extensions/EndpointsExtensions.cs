namespace Blog.Extensions
{
    public static class EndpointsExtensions
    {
        public static IEndpointRouteBuilder ConfigureEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "custom",
                pattern: "custom/{controller}/{action}");

            app.MapRazorPages();

            return app;
        }
    }
}
