$(document).ready(function () {

    //jquery placeholder plug-in for IE9
    $('input, textarea').placeholder();

    /*Search Box -----------------------------------------------------------------------*/
    //focus in
    $("#searchBox").focus(function () {
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
    });

});