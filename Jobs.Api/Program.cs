using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Jobs.Api.Data;
using Jobs.Api.Entities;
using Jobs.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<JobsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<JobsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthenticationJwtBearer(s =>
    s.SigningKey = builder.Configuration["Jwt:SigningKey"]!);

builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "Jobs API";
            s.Version = "v1";
            s.Description = "Jobs API";
        };
    });

builder.Services.AddScoped<IFileStorageService, FileStorageService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();