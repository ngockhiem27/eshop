const connection = new signalR.HubConnectionBuilder()
    .withUrl("/producthub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(start);

// Start the connection.
start();

connection.on("ProductUpdate", (msgContent) => {
    var productData = JSON.parse(msgContent)
    var existProduct = productDataSource.get(productData.Id);
    if (existProduct === undefined) {
        productDataSource.insert(productData);
        productHubNotify.show(`${productData.Name} just added`, "success");
    } else {
        productData.Images = existProduct.Images;
        productDataSource.pushUpdate(productData);
        productDataSource.sync();
        productHubNotify.show(`${productData.Name} just updated`, "success");
    }
});



