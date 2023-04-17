using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using PartsInfo.Common.Configuration;
using PartsInfo.Models;
using PartsInfo.Repo.Interface;
using Q101.ServiceCollectionExtensions.ServiceCollectionExtensions;
using Serilog;
using System.Text.Json.Serialization;

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
AppConfigs.LoadAll(config);
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -- add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// -- add dbcontext
builder.Services.AddDbContext<PartsDbContext>(options =>
    options.UseMySql(AppConfigs.MySqlConnection, ServerVersion.AutoDetect(AppConfigs.MySqlConnection)));
//--register Api Service
builder.Services.RegisterAssemblyTypesByName(typeof(IPartsInfoRepository).Assembly,
     name => name.EndsWith("Repository"))
     .AsScoped()
.AsImplementedInterfaces()
     .Bind();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
