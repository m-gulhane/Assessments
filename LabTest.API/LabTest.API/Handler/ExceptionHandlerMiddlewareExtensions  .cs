using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace LabTest.API.Handler
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Static extention method
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
