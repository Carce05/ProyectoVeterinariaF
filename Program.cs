using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinaria.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSingleton<ICartero, Cartero>();
builder.Services.Configure<ConfiguracionSmtp>(builder.Configuration.GetSection("ConfiguracionSmtp"));

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
 option =>
 {
     option.LoginPath = "/login/Login";

     option.ExpireTimeSpan = TimeSpan.FromMinutes(40);
     option.AccessDeniedPath = "/Home/Privacy"; //si no tiene acceso se va para aca
 }
    );

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
name: "default",
pattern: "{controller=login}/{action=Login}/{id?}");

app.Run();
