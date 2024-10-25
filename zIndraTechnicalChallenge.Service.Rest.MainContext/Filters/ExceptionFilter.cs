using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using zIndraTechnicalChallenge.Service.Rest.Dto.SeedWork;

namespace zIndraTechnicalChallenge.Service.Rest.MainContext.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void OnException(ExceptionContext context)
        {
            ResultDto result;
            var exception = context.Exception;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (exception is ApplicationException)
            {
                ApplicationException ex = exception as ApplicationException;
                result = new ResultDto { TraceId = context.HttpContext.TraceIdentifier, Title = ex.Message, Status = StatusCodes.Status400BadRequest };
            }
            else
            {
                result = new ResultDto { TraceId = context.HttpContext.TraceIdentifier, Title = exception.ToString(), Status = StatusCodes.Status500InternalServerError };
                _logger.LogError($"Message => {result.Title}", result);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new BadRequestObjectResult(result);
        }
    }
}
