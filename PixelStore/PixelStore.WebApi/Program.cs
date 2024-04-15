using System.Reflection;
using PixelStore.Infrastructure;
using PixelStore.Infrastructure.IoC;
using PixelStore.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints(assembly: Assembly.GetExecutingAssembly());
builder.Services.AddInfrastructure(configuration: builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync<ApplicationDbContext>();
    await app.SeedData();
}

app.MapDynamicEndpoints(assembly: Assembly.GetExecutingAssembly());
app.Run();