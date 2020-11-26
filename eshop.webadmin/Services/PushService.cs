using eshop.core.PushNotificationModels;
using eshop.infrastructure.WebPush;
using eshop.infrastructure.WebPush.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace eshop.webadmin.Services
{
    public class PushService : IPushService
    {
        private static readonly Dictionary<string, PushSubscriptionModel> _subscriptions = new Dictionary<string, PushSubscriptionModel>();
        private readonly VapidDetails _vapid;
        private readonly WebPushClient _webpush;

        public Dictionary<string, PushSubscriptionModel> Subscriptions => _subscriptions;

        public PushService(HttpClient httpClient, VapidDetails vapid)
        {
            this._vapid = vapid;
            this._webpush = new WebPushClient(httpClient);
        }

        public void Subscribe(PushSubscriptionModel subscription)
        {
            _subscriptions.TryAdd(subscription.Subscription.Endpoint, subscription);
        }

        public void UnSubscribe(PushSubscriptionModel subscription)
        {
            _subscriptions.Remove(subscription.Subscription.Endpoint);
        }

        public void Broadcast(NotificationModel data)
        {
            foreach (var sub in _subscriptions)
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options["vapidDetails"] = _vapid;
                try
                {
                    _webpush.SendNotification(sub.Value.Subscription, JsonSerializer.Serialize(data), options);
                } catch (Exception ex)
                {
                    _subscriptions.Remove(sub.Value.Subscription.Endpoint);
                }
            }
        }

        public void Notify(string endpoint, NotificationModel data)
        {
            if (_subscriptions.TryGetValue(endpoint, out var subscription))
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options["vapidDetails"] = _vapid;
                try
                {
                    _webpush.SendNotification(subscription.Subscription, JsonSerializer.Serialize(data), options);
                }
                catch (Exception ex)
                {
                    _subscriptions.Remove(subscription.Subscription.Endpoint);
                }
            }
        }
    }
}
