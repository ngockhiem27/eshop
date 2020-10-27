using eshop.infrastructure.KafkaLog.Logger;
using eshop.infrastructure.KafkaLog.LogModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace eshop.infrastructure.KafkaLog
{
    public class KafkaLogMidleware
    {
        private readonly RequestDelegate _next;
        private readonly IKafkaLogger _kafkaLogger;

        public KafkaLogMidleware(RequestDelegate next, IKafkaLogger kafkaLogger)
        {
            _kafkaLogger = kafkaLogger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var routeData = context.GetRouteData();
            if (routeData.Values["action"] != null)
            {
                ControllerMessage msg = new ControllerMessage
                (
                    dateTime: DateTime.Now,
                    ip: context.Connection.RemoteIpAddress.ToString(),
                    service: Assembly.GetExecutingAssembly().GetName().Name,
                    route: context.Request.Path.ToString(),
                    controller: routeData.Values["controller"].ToString(),
                    action: routeData.Values["action"].ToString(),
                    identity: context.User.Identity.Name ?? "Unidentified",
                    status: context.Response.StatusCode
                );
                await _kafkaLogger.WriteLogAsync();
            }
        }
    }
}
