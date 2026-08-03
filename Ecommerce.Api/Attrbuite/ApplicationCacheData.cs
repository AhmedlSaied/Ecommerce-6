using Ecommerce.Application.contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Ecommerce.Api.Attrbuite
{
    public class ApplicationCacheData : ActionFilterAttribute
    {
        private readonly int _deurationTime;

        public ApplicationCacheData(int deurationTime=60)
        {
           _deurationTime = deurationTime;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cache = context.HttpContext.RequestServices
                    .GetRequiredService<ICachedDataService>();
            var cacheKey = GetKey(context.HttpContext.Request);
            var values=await cache.GetCachedData(cacheKey);
            if (!string.IsNullOrEmpty(values))
            {
                context.Result = new ContentResult
                {
                    Content = values,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                return;
            }

           var connectionext=await next.Invoke();
            if(connectionext.Result is OkObjectResult { Value : not null} ok)
            {
                await cache.SetCachedData(cacheKey, ok.Value, TimeSpan.FromSeconds(_deurationTime));
            }
            //return base.OnActionExecutionAsync(context, next);
        }
        private string GetKey(HttpRequest reqest)
        {
            var key = new StringBuilder();
            key.Append(reqest.Path);
            if (reqest.Query.Any())
            {
                key.Append("?");
                foreach (var(parameterName, parameterValue) in reqest.Query.OrderBy(p=>p.Key))
                {
                    key.Append(parameterName).Append("=").Append(parameterValue).Append("&");
                }

            }
            return key.ToString();
        }
    }
}
