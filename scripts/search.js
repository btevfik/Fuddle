var arrayOfImages = new Array();
var heights = new Array();
var widths = new Array();
var end = 20;
var length = 0;

function getImages(parameter) {
    //initially don't display load more
    $("#loadMore").hide();
    //display loading gif
    $("#loading").html("<img src='/resources/loader.gif'/>");
    $.ajax({
        type: "POST",
        async:true,
        url: "SearchService.asmx/GetImages",
        data: "{'query':'" + parameter + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var images = response.d;
            $.each(images, function (index, image) {
                arrayOfImages[index] = image.link;
                heights[index] = image.height;
                widths[index] = image.width;
                //$("#searchresults").append('<img class="search-img" data-width="' + image.width + '" ' + 'data-height="' + image.height + '" ' + 'src="' + image.link + '"/>');
            });
            length=arrayOfImages.length;
            preloadImages(arrayOfImages, widths, heights, 0, end);
            $('#numresult').html(length + " images found.");
        },
        failure: function (msg) {
            $('#searchresults').text(msg);
        }
    });
};

/*
function getAlbums(parameter) {
    $.ajax({
        type: "POST",
        url: "SearchService.asmx/GetAlbums",
        data: "{'query':'" + parameter + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var albums = response.d;
            var count = 0;
            $.each(albums, function (index, album) {
                $('#searchresults').append('<p>' + album.name + '</p>');
                count++;
            });
            $('#numresult').html(count + " albums found.");
        },
        failure: function (msg) {
            $('#searchresults').text(msg);
        }
    });
}

function getUsers(parameter) {
    $.ajax({
        type: "POST",
        url: "SearchService.asmx/GetUsers",
        data: "{'query':'" + parameter + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var users = response.d;
            var count = 0;
            $.each(users, function (index, user) {
                $('#searchresults').append('<p>' + user.name + '</p>');
                count++;
            });
            $('#numresult').html(count + " users found.");
        },
        failure: function (msg) {
            $('#searchresults').text(msg);
        }
    });
}
*/

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function loadMore(type, parameter)
{
    if (end == length) {
        $("#loadMore").text("No More");
        return;
    }
    //initially don't display load more
    $("#loadMore").hide();
    //display loading gif
    $("#loading").html("<img src='/resources/loader.gif'/>");
    preloadImages(arrayOfImages, widths, heights, end, end + 20);
    end = end + 20;
}

function preloadImages(srcs, widths, heights, start, end) {
    var img;
    var imgs = [];
    var remaining = end;
    for (var i = start; i < end; i++) {
        img = new Image();
        img.onload = function () {
            --remaining;
            if (remaining <= start) {
                $("#searchresults").append(imgs);
                runLayout(205);
                $("#loading").empty();
                $("#loadMore").show();
            }
        };
        img.src = srcs[i];
        img.className = "search-img";
        img.setAttribute("data-width", widths[i]);
        img.setAttribute("data-height", heights[i]);
        imgs.push(img);
    }
}
