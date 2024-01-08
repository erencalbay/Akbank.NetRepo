using Microsoft.EntityFrameworkCore;
using NLog;
using WebAPI.Repositories;
using WebAPI.Services;
using WebAPI.Services.Contracts;
using WebAPI.Services.ErrorHandler;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models.GameOperations.CreateOneGame;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

builder.Services.AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddSingleton<ILoggerService, LoggerManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
