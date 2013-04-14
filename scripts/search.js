var arrayOfImages = new Array();
var heights = new Array();
var widths = new Array();
var end = 0;
var length = 0;

function getImages(parameter) {
    //reset end
    end = 25;
    //reset Load More on getImages call again
    $("#loadMore").text("Load More");
    //initially don't display load more
    $("#loadMore").hide();
    //display loading gif
    $("#loading").html("<img src='/resources/loader.gif'/>");
    $.ajax({
        type: "POST",
        async: true,
        url: "SearchService.asmx/GetImages",
        data: "{'query':'" + parameter + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var images = response.d;
            $.each(images, function (index, image) {
                arrayOfImages[index] = image.id;
                heights[index] = image.height;
                widths[index] = image.width;
            });
            //number of results
            length = arrayOfImages.length;
            //display number of results
            $('#numresult').html(length + " images found.");
            //if non return
            if (length === 0) {
                $("#loading").empty();
                return;
            }
            preloadImages(arrayOfImages, widths, heights, 0, end);
        },
        failure: function (msg) {
            $('#searchresults').text(msg);
        }
    });
};


function getUsers(parameter) {
    //hide load more for users.
    $("#loadMore").hide();
    //display loading gif
    $("#loading").html("<img src='/resources/loader.gif'/>");
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
                count++;
                $('#user-results').append('<div class="user-result"><a href="/user/' + user.name + '" target="_blank"><img src="/GetAvatar.ashx?user=' + user.name + '"/></a><a href="/user/' + user.name + '"target="_blank">' + user.name + '</a></div>');
                if (count % 4 == 0) {
                    $('#user-results').append('<div style="clear: both; margin-bottom: 40px"></div>');
                }
            });
            $('#numresult').html(count + " users found.");
            //hide loading gif when images are loaded
            $("#loading").empty();
        },
        failure: function (msg) {
            $('#searchresults').text(msg);
        }
    });
}


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

function loadMore(type) {
    //if end of results
    if (end >= length) {
        $("#loadMore").text("No More");
        return;
    }
    //initially don't display load more
    $("#loadMore").hide();
    //display loading gif
    $("#loading").html("<img src='/resources/loader.gif'/>");
    setTimeout(function () {
        preloadImages(arrayOfImages, widths, heights, end, end + 25);
        end = end + 25;
    }, 1000)
}

function preloadImages(srcs, widths, heights, start, end) {
    var hasRun = false;
    if (end >= length) {
        end = length;
    }
    var img;
    var anchor;
    var imgs = [];
    var ancs = [];
    var remaining = end;
    for (var i = start; i < end; i++) {
        img = new Image();
        anchor = document.createElement("a");
        img.onload = function () {
            --remaining;
            if (remaining <= start) {
                //append images
                $("#searchresults").append(imgs);
                //hide loading gif when images are loaded
                $("#loading").empty();
                //show load more now
                $("#loadMore").show();
                if (hasRun == false) {
                    runLayout(205);
                    hasRun = true;
                }
            }
        };
        //set link
        anchor.setAttribute("href", "/Image.aspx?id=" + srcs[i]);
        anchor.setAttribute("target", "_blank");
        //set src
        img.src = "/ShowImage.ashx?imgid=" + srcs[i];
        img.className = "search-img";
        img.setAttribute("data-width", widths[i]);
        img.setAttribute("data-height", heights[i]);
        anchor.appendChild(img);
        imgs.push(anchor);
    }
}
