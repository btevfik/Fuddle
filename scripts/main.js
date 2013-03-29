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

    /*Login-Dropdown-----------------------------------------------------------------------*/
    $('#login-trigger').click(function () {
        $(this).next('#login-content').slideToggle();
        $(this).toggleClass('active');

        if ($(this).hasClass('active')) $(this).find('span').html('&#x25B2;')
        else $(this).find('span').html('&#x25BC;')
    })
});