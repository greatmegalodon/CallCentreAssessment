using Assessment.Api.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Assessment.Application;
using Assessment.Data;
using Assessment.Infrastructure.Services;
using Assessment.Infrastructure.Services.Interfaces;
using Assessment.Shared.Configuration;
using Serilog;

namespace Assessment.Api;

/// <summary>
/// Startup configuration for the application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configure <see cref="ConfigurationManager"/>.
    /// </summary>
    public static void ConfigureConfiguration(ConfigurationManager config)
    {
        config.AddEnvironmentVariables();
    }

    /// <summary>
    /// Configure <see cref="ConfigureHostBuilder"/>.
    /// </summary>
    public static void ConfigureHost(ConfigureHostBuilder host, ConfigurationManager config)
    {
        var seqConfig = config.GetSection(nameof(Seq));

        if (seqConfig.GetValue<bool>(nameof(Seq.Enabled)))
        {
            host.UseSerilog((_, conf) => conf
                .Enrich.WithProperty("AppSource", seqConfig.GetValue<string>(nameof(Seq.ApplicationName)))
                .WriteTo.Async(x => x.Console())
                .WriteTo.Async(x =>
                    x.Seq(seqConfig.GetValue<string>(nameof(Seq.Uri))!,
                            apiKey: seqConfig.GetValue<string>(nameof(Seq.ApiKey)))
                        .MinimumLevel.Debug()));
        }
        else
        {
            host.UseSerilog((_, conf) => conf
                .MinimumLevel.Debug()
                .WriteTo.Async(x => x.Console()));
        }
    }

    /// <summary>
    /// Configure <see cref="IServiceCollection"/>.
    /// </summary>
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddOpenTelemetry(config);
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatRAnchor).Assembly));
        services.AddValidatorsFromAssembly(typeof(MediatRAnchor).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>)); 

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")!));

        services.AddTransient<ICsvService, CsvService>();
    }
    
    /// <summary>
    /// Register OpenTelemetry services for distributed tracing.
    /// </summary>
    private static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        IConfiguration config)
    {
        var oTelConfig = config.GetSection(nameof(Shared.Configuration.OpenTelemetry));

        if (oTelConfig.Exists() == false)
        {
            Log.Warning("OpenTelemetry configuration section not found.");
            return services;
        }

        var oTelTracingEndpoint = oTelConfig.GetValue<string>(nameof(Shared.Configuration.OpenTelemetry.TracingEndpoint));

        if (string.IsNullOrWhiteSpace(oTelTracingEndpoint)
            || Uri.TryCreate(oTelTracingEndpoint, UriKind.Absolute, out _) == false)
        {
            Log.Warning("OpenTelemetry TracingEndpoint not found or invalid.");
            return services;
        }
        
        var oTelServiceName = oTelConfig.GetValue<string>(nameof(Shared.Configuration.OpenTelemetry.ServiceName));
        
        if (string.IsNullOrWhiteSpace(oTelServiceName))
        {
            Log.Warning("OpenTelemetry ServiceName not found or invalid.");
            return services;
        }

        var oTelBuilder = services.AddOpenTelemetry();
        oTelBuilder.ConfigureResource(r => r.AddService(serviceName: oTelServiceName));

        oTelBuilder.WithTracing(t =>
        {
            t.AddAspNetCoreInstrumentation();
            t.AddHttpClientInstrumentation();
            t.AddRedisInstrumentation();
            t.AddEntityFrameworkCoreInstrumentation();
            t.AddSqlClientInstrumentation();
            t.AddOtlpExporter(x => x.Endpoint = new Uri(oTelTracingEndpoint));
        });
        
        Log.Information("OpenTelemetry configured.");

        return services;
    }
}