using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Planner.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add entity framework
builder.Services.AddDbContext<PlannerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]), ServiceLifetime.Singleton);
    
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSwaggerGen(c =>
c.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "Dit is de Blâuwe OAPI",
        Version = "v1",
        Description = "Interface rondom de reserveringen van de Blâuwe Oap"
    })
);
var app = builder.Build();
DbInitializer.CreateDbIfNotExists(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();