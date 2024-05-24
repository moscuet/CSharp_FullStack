using Microsoft.EntityFrameworkCore;
using System.Net;
using Eshop.Core.src.Common;

namespace Eshop.WebApi.src.middleware
{
   public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError( @"Unauthorized access error occurred: {Message}", ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = @"Access denied:{ex.Message}",
                });
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    context.Response.StatusCode,
                    Message = $"A database error occurred. Please check your input or try again later.",
                    Error = dbEx.InnerException?.Message ?? dbEx.Message 
                });
            }
            catch (AppException appEx)
            {
                _logger.LogError(appEx.ErrorMessage, "Application-specific error occurred.");

                context.Response.StatusCode = (int)appEx.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    context.Response.StatusCode,
                    Message = appEx.ErrorMessage,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    context.Response.StatusCode,
                    Message = "An unexpected error occurred: " + ex.Message,
                    Error = "For more details refer to the server logs." 
                });
            }
        }
    }}