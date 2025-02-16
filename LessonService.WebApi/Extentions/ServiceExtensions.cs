using System.Reflection;
using FluentValidation;
using LessonService.Application.Services;
using LessonService.Application.Services.Mapping;
using LessonService.Application.Services.MessageProducer;
using LessonService.Commands.Commands;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using LessonService.WebApi.Exception;
using MediatR;
using Microsoft.OpenApi.Models;

namespace LessonService.WebApi.Extentions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(builder.Configuration);
        // Adding the database context
        var dbConnection = Environment.GetEnvironmentVariable("LessonService_ConnectionStrings__DbConnection");
        if (string.IsNullOrWhiteSpace(dbConnection))
            dbConnection = builder.Configuration.GetConnectionString("sqlConnection");
        ArgumentNullException.ThrowIfNull(dbConnection);
        builder.Services.AddNpgsql<AppDbContext>(dbConnection, options =>
        {
            options.MigrationsAssembly("LessonService.Infrastructure.EF");
        });
        builder.Services.AddLogging();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateLessonCommandHandler).Assembly));        
        builder.Services.AddScoped<IRequestHandler<RemoveTrainerCommand, ApiResponse<LessonModel>>, RemoveTrainerCommandHandler>();
        builder.Services.AddSingleton<IMessageProducer, RabbitMessageProducer>();
        builder.Services.AddAutoMapper(typeof(Program), typeof(LessonMapping));
        
        // Adding validators from the current assembly
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
        builder.Services.AddScoped<ILessonServiceApp, LessonServiceApp>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen( c=>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LessonServiceApi", Version = "v1", Description = "SnowPro LessonService API" });
        });
    }
}