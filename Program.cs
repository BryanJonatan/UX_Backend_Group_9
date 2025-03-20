using Serilog;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using PetPals_BackEnd_Group_9;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using PetPals_BackEnd_Group_9.Handlers;
using System.Reflection;
using PetPals_BackEnd_Group_9.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Logging.AddSerilog(new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File($"logs/Log-{DateTime.Now:yyyyMMdd}.txt")
    .CreateLogger());


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PetPalsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PetPalsDbContext>();
    
    // Add roles if they don't exist
    if (!context.Roles.Any())
    {
        context.Roles.AddRange(
            new Role { Name = "Adopter" },
            new Role { Name = "Owner" },
            new Role { Name = "Provider" }
        );
        context.SaveChanges();
    }

    // Add species if they don't exist
    if (!context.Species.Any())
    {
        context.Species.AddRange(
            new Species { Name = "Dog", Description = "A domesticated mammal of the family Canidae." },
            new Species { Name = "Cat", Description = "A small, typically furry, omnivorous mammal." }
        );
        context.SaveChanges();
    }

    // Add a test user if they don't exist
    if (!context.Users.Any())
    {
        var ownerRole = context.Roles.First(r => r.Name == "Owner");
        context.Users.Add(new User
        {
            Name = "Test Owner",
            Email = "owner@test.com",
            Password = "password123",
            Phone = "1234567890",
            Address = "123 Test St",
            RoleId = ownerRole.RoleId,
            Role = ownerRole
        });
        context.SaveChanges();
    }

    // Add test pets if they don't exist
    if (!context.Pets.Any())
    {
        var owner = context.Users.First();
        var dog = context.Species.First(s => s.Name == "Dog");
        var cat = context.Species.First(s => s.Name == "Cat");

        context.Pets.AddRange(
            new Pet
            {
                Name = "Max",
                Breed = "Golden Retriever",
                Age = 2,
                SpeciesId = dog.SpeciesId,
                Species = dog,
                Price = 1000.00m,
                Status = "available",
                OwnerId = owner.UserId,
                Owner = owner
            },
            new Pet
            {
                Name = "Luna",
                Breed = "Persian",
                Age = 1,
                SpeciesId = cat.SpeciesId,
                Species = cat,
                Price = 800.00m,
                Status = "available",
                OwnerId = owner.UserId,
                Owner = owner
            }
        );
        context.SaveChanges();
    }
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/problem+json";

        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (errorFeature != null)
        {
            var problemDetails = new
            {
                type = "https://tools.ietf.org/html/rfc7807",
                title = "An unexpected error occurred!",
                status = context.Response.StatusCode,
                detail = errorFeature.Error.Message,
                instance = context.Request.Path
            };

            var errorJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(errorJson);
        }
    });
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.UseAuthorization();

app.MapControllers();

app.Run();
