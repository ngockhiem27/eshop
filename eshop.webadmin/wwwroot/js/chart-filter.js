
function filterarrow() {
    //Change icon when filter-dropdown show/hide
    $("#filter-form").on("hide.bs.collapse", function () {
        //$('#filter-btn span').toggleClass('ion-ios-arrow-up ion-ios-arrow-down');
        $('#filter-btn span').removeClass('ion-ios-arrow-up').addClass('ion-ios-arrow-down');
    });

    $("#filter-form").on("show.bs.collapse", function () {
        //$('#filter-btn span').toggleClass('ion-ios-arrow-up ion-ios-arrow-down');
        $('#filter-btn span').removeClass('ion-ios-arrow-down').addClass('ion-ios-arrow-up');
    });
};

$(document).ready(function () {
    $("#btnApply").click(function (e) {
        e.preventDefault();
    });
});