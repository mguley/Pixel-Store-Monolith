using PixelStore.Infrastructure;
using PixelStore.Infrastructure.IoC;
using PixelStore.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(configuration: builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync<ApplicationDbContext>();
    await app.SeedData();
}

app.Run();