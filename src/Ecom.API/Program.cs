using Ecom.API.Errors;
using Ecom.API.Extensions;
using Ecom.API.Middleware;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Extension method to register services
builder.Services.AddApiRegistration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Auth Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };
    s.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
    s.AddSecurityRequirement(securityRequirement);
});

// Configures infrastructure services, including repositories, Unit of Work, and database context.
builder.Services.InfrastructureConfiguration(builder.Configuration);

// Configure redius
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration["Redis:ConnectionString"], true);
    return ConnectionMultiplexer.Connect(configuration);
});

// Configure Order Services
builder.Services.AddScoped<IOrderService, OrderService>();

//configure payment gateway
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add ExceptionMiddleware
app.UseMiddleware<ExceptionMiddleware>();

// This middleware to redirect any error inside the app to ErrorsController
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

// Configures the application to serve static files to clients.
app.UseStaticFiles();

//// Use CORS policy to allow requests from the Angular app.
//app.UseCors("CorsPolicy");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// This middleware is used to seed the database with initial data.
InfrastructureRegistration.InfrastructureConfigMiddleware(app);

app.Run();
