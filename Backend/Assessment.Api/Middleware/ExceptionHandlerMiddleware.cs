using System.Net;
using System.Text.Json;
using Assessment.Shared.Models;
using Serilog;

namespace Assessment.Api.Middleware;


/// <summary>
/// Global middleware to correctly handle <see cref="FluentValidation.ValidationException"/>
/// thrown by <see cref="FluentValidation"/>.
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Invoked by the pipeline.
    /// </summary>
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    /// <summary>
    /// Attempt to deserialize any errors from <see cref="FluentValidation"/> if available,
    /// else format a sensible 500 error message.
    /// </summary>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (FluentValidation.ValidationException e)
        {
            httpContext.Response.ContentType = "application/json";
            var responseObject = UnitResult.Error(e.Errors.Select(x => x.ErrorMessage));
            
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(responseObject, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }));
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Global error. See inner exception.");
            
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var responseObject = UnitResult.Error("Something went wrong.");
            
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(responseObject, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }));
        }
    }
}
