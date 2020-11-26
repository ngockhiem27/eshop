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

connection.on("NewProduct", (msgContent) => {
    var productData = JSON.parse(msgContent);
    var existProduct = productDataSource.get(productData.Id);
    if (existProduct === undefined) {
        productDataSource.insert(productData);
        productHubNotify.hide();
        productHubNotify.show(`${productData.Name} just added`, "success");
    }
});

connection.on("UpdateProduct", (msgContent) => {
    var productData = JSON.parse(msgContent);
    var existProduct = productDataSource.get(productData.Id);
    if (existProduct !== undefined) {
        productData.Images = existProduct.Images;
        productDataSource.pushUpdate(productData);
        productDataSource.sync();
        productHubNotify.hide();
        productHubNotify.show(`${productData.Name} just updated`, "info");
    }
});

connection.on("RemoveProduct", (id) => {
    var existProduct = productDataSource.get(id);
    if (existProduct !== undefined) {
        productDataSource.remove(existProduct);
        productHubNotify.hide();
        productHubNotify.show(`${existProduct.Name} just got removed`, "error");
    }
});

connection.on("NewProductImage", (msgContent) => {
    var imgData = JSON.parse(msgContent);
    var existProduct = productDataSource.get(imgData.ProductId);
    console.log(existProduct);
    if (existProduct !== undefined) {
        if (existProduct.Images === null) existProduct.Images = [];
        existProduct.Images.push(imgData);
        productDataSource.pushUpdate(existProduct);
        productDataSource.sync();
        productHubNotify.hide();
        productHubNotify.show(`${existProduct.Name} just updated`, "info");
        console.log("dd", existProduct);
    }
});

