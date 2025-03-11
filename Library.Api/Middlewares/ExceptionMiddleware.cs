using FluentValidation;
using Library.Core.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Net;

namespace Library.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleException(context, ex);
            }
            catch(ArgumentNullException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleException(context, ex);
            }
            catch(ArgumentException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleException(context, ex);
            }
            catch(ObjectAlreadyExistsException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                await HandleException(context, ex);
            }
            catch(ObjectNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await HandleException(context, ex);
            }
            catch(UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HandleException(context, ex);
            }
            catch(SecurityTokenException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await HandleException(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleException(context, ex);
            }
            
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            logger.LogError($"Error: {ex.ToString()}");

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { 
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Details = ex.ToString()
            });
        }
    }
}
