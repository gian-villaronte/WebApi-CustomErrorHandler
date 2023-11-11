using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using EventsScheduler.Common.Exceptions;


namespace EventScheduler.Problems
{
    public static class BuildInExceptionHandler
    {
        public static void AddErrorHandler(this IApplicationBuilder app, ILogger<IEventSchedulerException> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is IEventSchedulerException ex)
                        { 
                            if(ex.HttpStatusCode.HasValue) 
                            { 
                                context.Response.StatusCode = (int)ex.HttpStatusCode.Value;
                            }
                            await context.Response.WriteAsync(ex.ToJson());
                            logger?.LogError(ex.EventId, contextFeature.Error, ex.Message);
                        }
                        else 
                        {
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "something went wrong"
                            }));
                            logger?.LogError(new EventId(0, "UnknownError"), contextFeature.Error, contextFeature.Error.Message);
                        }

                    }
                });
            });
        }
    }
}
