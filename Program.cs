using MotoTrack.Application.Services;
using MotoTrack.Domain.Interfaces;
using MotoTrack.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
// Catálogo
// ======================

builder.Services.AddSingleton<IItemRepository, ItemRepository>();
builder.Services.AddSingleton<ItemService>();

// ======================
// Usuarios
// ======================

builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddSingleton<UsuarioService>();

// ======================
// Motocicletas
// ======================

builder.Services.AddSingleton<IMotocicletaRepository, MotocicletaRepository>();
builder.Services.AddSingleton<MotocicletaService>();

// ======================
// Lecturas de Kilometraje
// ======================

builder.Services.AddSingleton<
    ILecturaKilometrajeRepository,
    LecturaKilometrajeRepository>();

builder.Services.AddSingleton<
    LecturaKilometrajeService>();

builder.Services.AddSingleton<
    IConfiguracionMantenimientoRepository,
    ConfiguracionMantenimientoRepository>();

builder.Services.AddSingleton<
    ConfiguracionMantenimientoService>();

builder.Services.AddSingleton<
    IMantenimientoRepository,
    MantenimientoRepository>();

builder.Services.AddSingleton<
    MantenimientoService>();

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

app.Run();
