using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly HttpExceptionHandler _exceptionHandler;
        private readonly RequestDelegate _requestDelegate;
        private readonly LoggerServiceBase _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public ExceptionMiddleware(RequestDelegate requestDelegate, LoggerServiceBase logger, IHttpContextAccessor contextAccessor)
        {
            _exceptionHandler = new HttpExceptionHandler();
            _requestDelegate = requestDelegate;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                await LogException(context, exception);
                await HandleExceptionAsync(context.Response, exception);
            }
        }

        private Task LogException(HttpContext context, Exception exception)
        {
            List<LogParameter> logParameters = new()
            {
                new(){Type = context.GetType().Name, Value = exception.ToString()}
            };

            LogDetailWithException logDetail = new() 
            { 
                ExceptionMessage = exception.Message,
                LogParameters = logParameters,
                MethodName = _requestDelegate.Method.Name,
                User = _contextAccessor.HttpContext?.User.Identity.Name ?? "?" 
            };

            _logger.Error(JsonSerializer.Serialize(logDetail));
            return Task.CompletedTask;
        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _exceptionHandler.Response = response;
            return _exceptionHandler.HandleExceptionAsync(exception);
        }
    }
}
