using Microsoft.EntityFrameworkCore;
using Assessment.Api;
using Assessment.Api.Middleware;
using Assessment.Data;
using Assessment.Shared.Configuration;

var builder = WebApplication.CreateBuilder(args);

DependencyInjection.ConfigureConfiguration(builder.Configuration);
DependencyInjection.ConfigureHost(builder.Host, builder.Configuration);
DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddCors(o =>
    o.AddPolicy(name: "DefaultCorsPolicy",
        b => b.WithOrigins(builder.Configuration.GetSection(nameof(Cors))
                .GetSection(nameof(Cors.AllowedOrigins))
                .Get<string[]>()!
                .Select(_ => _.Trim().TrimEnd('/'))
                .ToArray())
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod()
            .WithExposedHeaders("Content-Disposition")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope
        .ServiceProvider
        .GetRequiredService<ApplicationDbContext>()
        .Database
        .MigrateAsync()
        .GetAwaiter()
        .GetResult();
}

app.Run();