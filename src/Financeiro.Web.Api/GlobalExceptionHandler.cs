namespace Financeiro.Web.Api;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;

        switch (exception)
        {
            // case Meeting.Hub.Application.Commands.Behaviors.ApplicationException applicationException:
            // {
            //     problemDetails.Title = "one or more validation errors occurred.";
            //     httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            //     var validationErrors = applicationException.ValidationResults.Select(error => error.ErrorMessage).ToList();
            //     problemDetails.Extensions.Add("errors", validationErrors);
            //     break;
            // }
            case ValidationException validationException:
            {
                problemDetails.Title = "one or more validation errors occurred.";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationErrors = validationException.Errors.Select(error => error.ErrorMessage).ToList();
                problemDetails.Extensions.Add("errors", validationErrors);
                break;
            }
            default:
                problemDetails.Title = exception.Message;
                break;
        }

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}