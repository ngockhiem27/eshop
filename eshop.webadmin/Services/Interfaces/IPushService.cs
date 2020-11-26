using eshop.core.PushNotificationModels;
using eshop.infrastructure.WebPush.Models;
using System.Collections.Generic;

namespace eshop.webadmin.Services
{
    public interface IPushService
    {
        Dictionary<string, PushSubscriptionModel> Subscriptions { get; }
        public void Subscribe(PushSubscriptionModel subscription);
        public void UnSubscribe(PushSubscriptionModel subscription);
        public void Notify(string endpoint, NotificationModel msg);
        public void Broadcast(NotificationModel msg);
    }
}