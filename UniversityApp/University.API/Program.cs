using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using University.Api.Middlewares;
using University.Service.Interfaces;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Implementations;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using UniversityApi.Models;
using Microsoft.AspNetCore.Mvc;
using UniversityApp.Service.Exceptions;
using University.API.Middlewares;
using Serilog;
using UniversityApp.Data.Repositories.Interfaces;
using UniversityApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(x => x.Value.Errors.Count > 0)
        .Select(x => new RestExceptionError(x.Key, x.Value.Errors.First().ErrorMessage)).ToList();
        return new BadRequestObjectResult(new { message = "", errors });
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<UniversityDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<GroupCreateDtoValidator>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

builder.Services.AddFluentValidationRulesToSwagger();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();


//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

app.Run();

 