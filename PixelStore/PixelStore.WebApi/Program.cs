using Microsoft.EntityFrameworkCore;
using PixelStore.Infrastructure;
using PixelStore.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(configuration: builder.Configuration);

var app = builder.Build();

// TODO: REMOVE INTO A SEPARATE CLASS OR METHOD
using var scope = app.Services.CreateScope();
using DbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.MigrateAsync();
// TODO: REMOVE INTO A SEPARATE CLASS OR METHOD

app.Run();