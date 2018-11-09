// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#myCarousel').carousel({
        interval: 10000
    })
});

$(document).ready(function () {
    debugger;
    $('#example').DataTable();
});

$('.flip').hover(function () {
    $(this).find('.card').toggleClass('flipped');
});