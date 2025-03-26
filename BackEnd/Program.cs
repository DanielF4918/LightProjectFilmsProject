using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("https://localhost:5101")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

#region Dependency Injection (DI)

builder.Services.AddDbContext<RentalSystem>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentalSystemDB")));

builder.Services.AddScoped<IClientDAL, ClientDAL>();
builder.Services.AddScoped<IEmployeeDAL, EmployeeDAL>();
builder.Services.AddScoped<IEventoDAL, EventoDAL>();
builder.Services.AddScoped<IEquipmentDAL, EquipmentDAL>();
builder.Services.AddScoped<IPaymentDAL, PaymentDAL>();
builder.Services.AddScoped<IRentalDAL, RentalDAL>();
builder.Services.AddScoped<IRentalDetailDAL, RentalDetailDAL>();

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
