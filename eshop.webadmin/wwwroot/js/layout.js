$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        console.log("dsad");
        $('#sidebar, #content').toggleClass('active');
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });
});