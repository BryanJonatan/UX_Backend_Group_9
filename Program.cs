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
