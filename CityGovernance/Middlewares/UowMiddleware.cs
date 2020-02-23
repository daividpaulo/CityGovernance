using CityGovernance.infra.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGovernance.Middlewares
{
    public class UowMiddleware
    {
        private readonly RequestDelegate next;

        public UowMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await next(context);
            var uow = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
            await uow.CommitAsync();
        }
    }

    public static class UowMiddlewareExtensions
    {
        public static IApplicationBuilder UseUow(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UowMiddleware>();
        }
    }
}
