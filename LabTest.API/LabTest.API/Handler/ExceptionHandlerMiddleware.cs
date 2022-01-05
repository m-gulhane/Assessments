using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LabTest.API.Handler
{
    public class ExceptionHandlerMiddleware
    {
        #region Global Variables
        /// <summary>
        /// private variable of RequestDelegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// private variable of Ilogger
        /// </summary>
        private readonly ILogger _logger; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        /// <param name="logger">ILogger<ExceptionHandlerMiddleware></param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>void</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex, _logger).ConfigureAwait(false);
            }
        } 
        #endregion

        #region Private Method
        /// <summary>
        /// Handle exception with details
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="exception">Exception</param>
        /// <param name="logger">ILogger</param>
        /// <returns>void</returns>
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            logger.LogError($"ExceptionHandlerMiddleware : {result}");
            return context.Response.WriteAsync(result);
        } 
        #endregion

    }
}
