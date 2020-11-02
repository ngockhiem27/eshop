// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getCookieValue(a) {
    var b = document.cookie.match('(^|;)\\s*' + a + '\\s*=\\s*([^;]+)');
    return b ? b.pop() : '';
}

var emptyDataSource = new kendo.data.DataSource({
    data: []
});

var productDataSource = new kendo.data.DataSource({
    data: [],
    pageSize: 8,
    schema: {
        model: {
            id: "Id",
                fields: {
                Id: { type: "number" },
                Name: { type: "string" },
                RegularPrice: { type: "number" },
                DiscountPrice: { type: "number" },
                CreatedAt: { type: "DateTime" },
                UpdatedAt: { type: "DateTime" },
                Categories: { type: "Categories" },
            }
        }
    }
});

var cartCookie = decodeURIComponent(getCookieValue("cart"));
var cartData = cartCookie === "" ? { Items: [] } : JSON.parse(cartCookie);

var cartModel = kendo.observable({
    contents: cartData.Items,
    cleared: false,
    contentsCount: function () {
        return this.get("contents").length;
    },
    add: function (item) {
        console.log("add", item.Id, productDataSource.get(item.Id));
        var found = false;

        this.set("cleared", false);

        for (var i = 0; i < this.contents.length; i++) {
            var current = this.contents[i];
            if (current.ProductId === item.Id) {
                current.set("Quantity", current.get("Quantity") + 1);
                found = true;
                break;
            }
        }

        if (!found) {
            this.contents.push({ ProductId: item.Id, Quantity: 1 });
        }
    },
    remove: function (item) {
        for (var i = 0; i < this.contents.length; i++) {
            var current = this.contents[i];
            if (current === item) {
                this.contents.splice(i, 1);
                break;
            }
        }
    },
});

var productsGridModel = kendo.observable({
    items: productDataSource,
    addToCart: function (e) {
        $.ajax({
            type: "POST",
            url: "/cart/add/" + e.data.Id,
            success: function (response) {
                cartModel.add(e.data);
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
});

var productHubNotify = $("#product-hub-notify").kendoNotification().data("kendoNotification");