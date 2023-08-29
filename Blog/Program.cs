using Blog.Data;
using Blog.Data.Models;
using Blog.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetDefaultConnection() ?? builder.Configuration.ThrowExceptionForNotFoundDefaultConnection();

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder
    .Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddUserManager<UserManager<User>>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<BlogDbContext>();

builder.Services.AddServices();
builder.Services.AddMvcWithValidation();

var app = builder.Build();

app.UseErrorPage(app.Environment);
await app.SeedDataAsync();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureEndpoints();

app.Run();
