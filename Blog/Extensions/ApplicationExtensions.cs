namespace Blog.Extensions
{
    using Blog.Data;
    using Blog.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static GlobalConstants.Environments;

    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseErrorPage(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler(ErrorPagePath);
                app.UseHsts();
            }

            return app;
        }

        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetService<BlogDbContext>();
                await dbContext.Database.MigrateAsync();

                var roleManager = services.GetService<RoleManager<IdentityRole>>();
                var adminRole = await roleManager.FindByNameAsync("Admin");

                if (adminRole != null)
                    return app;

                var identityRole = new IdentityRole("Admin");

                await roleManager.CreateAsync(identityRole);

                var adminUser = new User
                {
                    UserName = "admin@blog.com",
                    Email = "admin@blog.com",
                    SecurityStamp = "Random",
                };

                var passwordHasher = new PasswordHasher<User>();
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin*123");

                var userManager = services.GetService<UserManager<User>>();
                await userManager.CreateAsync(adminUser);

                await userManager.AddToRoleAsync(adminUser, "Admin");

                await dbContext.SaveChangesAsync();
            }

            return app;
        }
    }
}
