function receivePushNotification(event) {
    console.log("[Service Worker] Push Received.");
    const data = event.data.json();
    console.log(data);
    let title = "";
    let options = {};
    

    title = "Received Notification";
    options.body = data.Message;
    options.actions = [{ action: "Detail", title: "View" }]

    event.waitUntil(self.registration.showNotification(title, options));
}

function openPushNotification(event) {
    console.log("[Service Worker] Notification click Received.", event.notification.data);
    event.waitUntil(clients.openWindow(event.notification.data));
    event.notification.close();
}

self.addEventListener("push", receivePushNotification);
self.addEventListener("notificationclick", openPushNotification);