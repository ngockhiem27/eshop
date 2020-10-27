using System;

namespace eshop.infrastructure.KafkaLog.LogModel
{
    public class ControllerMessage : BaseLogMessage
    {
        public string Service { get; set; }

        public string Route { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public int Status { get; set; }

        public string Identity { get; set; }

        public ControllerMessage(DateTime dateTime, string ip, string service, string route, string controller, string action, string identity, int status)
        {
            IP = ip;
            Service = service;
            Controller = controller;
            Route = route;
            Action = action;
            Status = status;
            Identity = identity;
            DateTime = dateTime;
        }
    }
}
