using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Vehicle.Common;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    public ExceptionHandler(IProblemDetailsService programDetailsService, ILoggerFactory logger)
    {
        _logger = logger.CreateLogger<ExceptionHandler>();
        _problemDetailsService = programDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error");

        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails()
            {
                Type = exception.GetType().Name,
                Title = "Unhandeled exception occured",
                Detail = exception.Message
            }
        });
    }
}
 