﻿namespace eshop.infrastructure.WebPush.Models
{
    public class PushSubscription
    {
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }

        public PushSubscription()
        {

        }

        public PushSubscription(string endpoint, string p256dh, string auth)
        {
            Endpoint = endpoint;
            P256DH = p256dh;
            Auth = auth;
        }
    }
}
