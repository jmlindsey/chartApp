$(function () {
    $('#saveChanges').on("click", function () {
        $("#loaderContainer").show();
        $("#main").hide();
        setTimeout(show, 2000);
    });
});

// highligts each changed vital / intervention
$(function () {
    $("input").change(function () {
        $(this).css({ 'font-weight': 900 });
        $(this).animate({
            backgroundColor: "#ffff00"
            //color: "white",
        },
            1000);

    });
});

function show() {
    //$("#loaderContainer").hide();
    //$("#main").show();
    $("#submitButton").click();
}