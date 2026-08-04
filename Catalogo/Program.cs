using MotoTrack.Application.Services;
using MotoTrack.Application.Strategies;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Infrastructure.Decorators;
using MotoTrack.Infrastructure.Persistence;
using MotoTrack.Infrastructure.Persistence.Repositories;
using MotoTrack.Infrastructure.Repositories;
using MotoTrack.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// === Swagger ===
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoTrack API",
        Version = "v1",
        Description = "API REST para la gestión de motocicletas."
    });
});

// ======================
// Sesiones
// ======================

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ======================
// Entity Framework Core
// ======================

builder.Services.AddDbContext<MotoTrackDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ======================
// Catálogo
// ======================

builder.Services.AddSingleton<IItemRepository, ItemRepository>();
builder.Services.AddSingleton<ItemService>();

// ======================
// Strategy + Decorator
// ======================

builder.Services.AddSingleton<IEstadoMantenimientoStrategy, DefaultEstadoStrategy>();
builder.Services.AddSingleton<CalculadorEstadoMantenimiento>();

var persistenceProvider = builder.Configuration.GetValue<string>("Persistence:Provider");

switch (persistenceProvider)
{
    case "EntityFramework":
        builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepositoryEF>();

        builder.Services.AddSingleton<MotocicletaRepositoryEF>();
        builder.Services.AddSingleton<IMotocicletaRepository>(sp =>
            new LoggingMotocicletaRepository(
                sp.GetRequiredService<MotocicletaRepositoryEF>()));

        builder.Services.AddSingleton<ILecturaKilometrajeRepository, LecturaKilometrajeRepositoryEF>();
        builder.Services.AddSingleton<IConfiguracionMantenimientoRepository, ConfiguracionMantenimientoRepositoryEF>();
        builder.Services.AddSingleton<IMantenimientoRepository, MantenimientoRepositoryEF>();
        builder.Services.AddSingleton<IGastoRepository, GastoRepositoryEF>();
        break;
    default:
        builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

        builder.Services.AddSingleton<MotocicletaRepository>();
        builder.Services.AddSingleton<IMotocicletaRepository>(sp =>
            new LoggingMotocicletaRepository(
                sp.GetRequiredService<MotocicletaRepository>()));

        builder.Services.AddSingleton<ILecturaKilometrajeRepository, LecturaKilometrajeRepository>();
        builder.Services.AddSingleton<IConfiguracionMantenimientoRepository, ConfiguracionMantenimientoRepository>();
        builder.Services.AddSingleton<IMantenimientoRepository, MantenimientoRepository>();
        builder.Services.AddSingleton<IGastoRepository, GastoRepository>();
        break;
}

builder.Services.AddSingleton<UsuarioService>();
builder.Services.AddSingleton<MotocicletaService>();
builder.Services.AddSingleton<LecturaKilometrajeService>();
builder.Services.AddSingleton<ConfiguracionMantenimientoService>();
builder.Services.AddSingleton<MantenimientoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
