//Startinstallationen:
//dotnet add package Microsoft.EntityFrameworkCore.Design
//dotnet add package Pomelo.EntityFrameworkCore.MySql

// Create Database:
// PM console go to project folder (under .sol)
// dotnet new tool-manifest
// dotnet tool install --local dotnet-ef
// dotnet ef migrations add InitialCreate
// dotnet ef database update

//Swagger:
//dotnet add package Swashbuckle.AspNetCore


using Assignment1;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = "datasource=localhost;port=3307;database=logbook;userid=root;password=" + Environment.GetEnvironmentVariable("dbpassword")

//string? connectionString = builder.Configuration.GetConnectionString("Logbookdatabase");

builder.Services.AddDbContext<LogbookContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
