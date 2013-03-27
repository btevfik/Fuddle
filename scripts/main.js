$(document).ready(function () {
    //Search Box -----------------------------------------------------------------------
    //focus in
    $("#searchBox").focus(function () {
        $(this).val("");
        $(this).animate({
            width: '200px'
        },
            "slow")
    });

    //focus out (blur)
    $("#searchBox").blur(function () {
        $(this).animate({
            width: '100px'
        },
            "slow")
        $(this).val("     Search...");
    });
    //-----------------------------------------------------------------------------------
});