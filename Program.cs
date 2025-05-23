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
using PetPals_BackEnd_Group_9.Validators;
using PetPals_BackEnd_Group_9.Command;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "PetPals API", Version = "v1" });

    // Tambahkan server IP kamu di Swagger UI
    c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer
    {
        Url = "http://192.168.100.8:5249"
    });
});


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
builder.Services.AddScoped<GetServiceListQueryValidator>();
builder.Services.AddScoped<SearchAdoptionListValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddScoped<IForumPostRepository, ForumPostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<PetPalsDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IForumCommentRepository, ForumCommentRepository>();
builder.Services.AddScoped<IForumPostRepository, ForumPostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IForumCategoryRepository, ForumCategoryRepository>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5249); // HTTP
    // options.ListenAnyIP(7249, listenOptions => { listenOptions.UseHttps(); }); // kalau kamu mau HTTPS
});


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
