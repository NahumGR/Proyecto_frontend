using BibliotecaMVC.Repositories;
using BibliotecaMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<NumericRoundabortConnectionInterceptor>();
builder.Services.AddDbContext<BibliotecaContext>((serviceProvider, options) =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BibliotecaDB"));

    options.AddInterceptors(
        serviceProvider.GetRequiredService<NumericRoundabortConnectionInterceptor>());
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<BibliotecaContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddSingleton<IRepositorioLibro, RepositorioMemoria>();


var app = builder.Build();


// ===== ACTIVIDAD 1 (CAMBIADO): inicialización de roles y asignación a usuarios =====
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    string[] roles = { "Admin", "Bibliotecario", "Usuario" };
    foreach (var rol in roles)
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            var resultadoRol = await roleManager.CreateAsync(new IdentityRole(rol));
            if (!resultadoRol.Succeeded)
                logger.LogError("No se pudo crear el rol {Rol}: {Errores}", rol,
                    string.Join(", ", resultadoRol.Errors.Select(e => e.Description)));
        }
    }

    var asignaciones = new Dictionary<string, string>
    {
        { "admin", "Admin" },
        { "bibliotecario", "Bibliotecario" }
    };

    foreach (var (nombre, rol) in asignaciones)
    {
        var u = await userManager.FindByNameAsync(nombre);
        if (u != null && !await userManager.IsInRoleAsync(u, rol))
        {
            var resultadoAsignacion = await userManager.AddToRoleAsync(u, rol);
            if (!resultadoAsignacion.Succeeded)
                logger.LogError("No se pudo asignar el rol {Rol} a {Usuario}: {Errores}", rol, nombre,
                    string.Join(", ", resultadoAsignacion.Errors.Select(e => e.Description)));
        }
    }
    foreach (var u in userManager.Users.ToList())
    {
        if ((await userManager.GetRolesAsync(u)).Count == 0)
        {
            var resultadoUsuario = await userManager.AddToRoleAsync(u, "Usuario");
            if (!resultadoUsuario.Succeeded)
                logger.LogError("No se pudo asignar el rol Usuario a {Usuario}", u.UserName);
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/CodigoEstado", "?codigo={0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();






