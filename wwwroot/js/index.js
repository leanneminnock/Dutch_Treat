
$(document).ready(function () {
    var theForm = $("#theForm");
    theForm.hide();

    var button = $("#buyButton");
    button.on("click", function () { console.log("Buying Item"); });

    var productInfo = $(".product-properties li");
    productInfo.on("click", function () { console.log("You clicked on " + $(this).text()); });

    var $loginToggel = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggel.on("click", function () {
        $popupForm.slideToggle(500);
    });
});


