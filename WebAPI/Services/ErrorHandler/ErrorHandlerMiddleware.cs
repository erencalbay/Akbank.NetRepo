using System.Net;
using System.Text.Json;
using WebAPI.Models.Exceptions;

namespace WebAPI.Services.ErrorHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {

                    case NotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case BadRequestException e:
                        //badrequest
                        response.StatusCode= (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.LogError(error.Message);
                context.Response.StatusCode = response.StatusCode;
                var resultMessage = JsonSerializer.Serialize(new { message = error?.Message });
                await context.Response.WriteAsync(resultMessage);

            }
        }
    }
}
