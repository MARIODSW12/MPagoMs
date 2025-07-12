using dotenv.net;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MPago.Application.Commands;
using MPago.Application.Commands.CommandHandlers;
using MPago.Application.Validations;
using MPago.Domain.Events;
using MPago.Domain.Events.EventHandlers;
using MPago.Domain.Interfaces;
using MPago.Infrastructure.Configurations;
using MPago.Infrastructure.Consumers;
using MPago.Infrastructure.Interfaces;
using MPago.Infrastructure.Persistences.Repositories.MongoRead;
using MPago.Infrastructure.Persistences.Repositories.MongoWrite;
using MPago.Infrastructure.Queries;
using MPago.Infrastructure.Queries.QueryHandlers;
using MPago.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar configuración de MongoDB
builder.Services.AddSingleton<MPagoWriteDbConfig>();
builder.Services.AddSingleton<MPagoReadDbConfig>();

// REGISTRA EL REPOSITORIO ANTES DE MediatR
builder.Services.AddScoped<IMPagoRepository, MPagoWriteRepository>();
builder.Services.AddScoped<IMPagoReadRepository, MPagoReadRepository>();
builder.Services.AddScoped<IStripeService, StripeService>();

// REGISTRA MediatR PARA TODOS LOS HANDLERS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMPagoPorIdQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMPagoPorIdPostorQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTodosMPagoQueryHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AgregarMPagoCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EliminarMPagoCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MPagoPredeterminadoCommandHandler).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MPagoAgregadoEventHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MPagoEliminadoEventHandler).Assembly));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MPagoPredeterminadoEventHandler).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<AgregarMPagoDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MPagoDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<AgregarMPagoConsumer>();
    busConfigurator.AddConsumer<EliminarMPagoConsumer>();
    busConfigurator.AddConsumer<MPagoPredeterminadoConsumer>();

    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(Environment.GetEnvironmentVariable("RABBIT_URL")), h =>
        {
            h.Username(Environment.GetEnvironmentVariable("RABBIT_USERNAME"));
            h.Password(Environment.GetEnvironmentVariable("RABBIT_PASSWORD"));
        });

        configurator.ReceiveEndpoint(Environment.GetEnvironmentVariable("RABBIT_QUEUE_AgregarMPago"), e => {
            e.ConfigureConsumer<AgregarMPagoConsumer>(context);
        });

        configurator.ReceiveEndpoint(Environment.GetEnvironmentVariable("RABBIT_QUEUE_EliminarMPago"), e => {
            e.ConfigureConsumer<EliminarMPagoConsumer>(context);
        });

        configurator.ReceiveEndpoint(Environment.GetEnvironmentVariable("RABBIT_QUEUE_ActualizarPredeterminadoMPago"), e => {
            e.ConfigureConsumer<MPagoPredeterminadoConsumer>(context);
        });

        configurator.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        configurator.ConfigureEndpoints(context);
    });
});
EndpointConvention.Map<MPagoAgregadoEvent>(new Uri("queue:" + Environment.GetEnvironmentVariable("RABBIT_QUEUE_AgregarMPago")));
EndpointConvention.Map<MPagoEliminadoEvent>(new Uri("queue:" + Environment.GetEnvironmentVariable("RABBIT_QUEUE_EliminarMPago")));
EndpointConvention.Map<MPagoPredeterminadoEvent>(new Uri("queue:" + Environment.GetEnvironmentVariable("RABBIT_QUEUE_ActualizarPredeterminadoMPago")));


// Configuración CORS permisiva
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier dominio
            .AllowAnyMethod()  // GET, POST, PUT, DELETE, etc.
            .AllowAnyHeader(); // Cualquier cabecera
    });
});

var app = builder.Build();

// Habilitar CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
