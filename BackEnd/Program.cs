using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL;
using DAL.Auth;
using DAL.Implementations;
using DAL.Interfaces;
using Domain.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración del ApplicationDbContext (autenticación)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración del contexto de RentalSystem (dominio de negocio)
builder.Services.AddDbContext<RentalSystem>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentalSystemDB")));

// Configurar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configurar rutas de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/auth/login";
    options.AccessDeniedPath = "/api/auth/denied";
});

// Inyecciones DAL
builder.Services.AddScoped<IClientDAL, ClientDAL>();
builder.Services.AddScoped<IEmployeeDAL, EmployeeDAL>();
builder.Services.AddScoped<IEventoDAL, EventoDAL>();
builder.Services.AddScoped<IEquipmentDAL, EquipmentDAL>();
builder.Services.AddScoped<IPaymentDAL, PaymentDAL>();
builder.Services.AddScoped<IRentalDAL, RentalDAL>();
builder.Services.AddScoped<IRentalDetailDAL, RentalDetailsDAL>();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

// Inyecciones de servicios
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Add Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();