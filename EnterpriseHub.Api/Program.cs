using EnterpriseHub.Api.Data;
using EnterpriseHub.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var provider = builder.Configuration["DatabaseProvider"] ?? "InMemory";
var connectionString = builder.Configuration.GetConnectionString("EnterpriseHubDb");

builder.Services.AddDbContext<ContextoEnterpriseHub>(options =>
{
    if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase) &&
        !string.IsNullOrWhiteSpace(connectionString))
    {
        options.UseSqlServer(connectionString);
        return;
    }

    options.UseInMemoryDatabase("EnterpriseHubDb");
});

builder.Services.AddScoped<IServicioDepartamentos, ServicioDepartamentos>();
builder.Services.AddScoped<IServicioCargos, ServicioCargos>();
builder.Services.AddScoped<IServicioEmpleados, ServicioEmpleados>();
builder.Services.AddScoped<IServicioSolicitudesPermiso, ServicioSolicitudesPermiso>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ContextoEnterpriseHub>();

    if (context.Database.IsRelational())
    {
        context.Database.EnsureCreated();
    }

    DatosIniciales.Initialize(context);
}

app.Run();

