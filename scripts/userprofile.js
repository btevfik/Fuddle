var arrayOfImages = new Array();
var heights = new Array();
var widths = new Array();
var end = 0;
var length = 0;
var loadnumber = 10;

function getUserUploads(parameter) {
    //reset end
    end = loadnumber;
    //reset Load More on getImages call again
    $("#loadMore").text("Load More");
    //initially don't display load more
    $("#loadMore").hide();
    //display loading gif
    $("#loading").html("<img src='/resources/loader.gif'/>");
    $.ajax({
        type: "POST",
        async: true,
        url: "SearchService.asmx/GetUserUploads",
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
        preloadImages(arrayOfImages, widths, heights, end, end + loadnumber);
        end = end + loadnumber;
    }, 1000)
}

function getParameter(name) {
    var regexS = "(^=" + name + ".)";
    console.log(regexS);
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    console.log(results[1]);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}

function preloadImages(srcs, widths, heights, start, end) {
    if (end >= length) {
        end = length;
    }
    var img;
    var anchor;
    var figure;
    var caption;
    var imgs = [];
    var ancs = [];
    var remaining = end;
    for (var i = start; i < end; i++) {
        img = new Image();
        anchor = document.createElement("a");
        figure = document.createElement("figure");
        caption = document.createElement("figcaption");
        img.onload = function () {
            --remaining;
            if (remaining == start) {
                //append images
                $("#searchresults").append(imgs);
                //append images
                $("#searchresults").append(imgs);
                //hide loading gif when images are loaded
                $("#loading").empty();
                //show load more now
                $("#loadMore").show();
                //fix the layout
                //runLayout(205);
            }
        };
        //set figure and caption
        caption.innerHTML = "test caption"; //we can add the title here.
        figure.appendChild(img);
        figure.appendChild(caption);
        //set link
        anchor.setAttribute("href", "/Image.aspx?id=" + srcs[i]);
        anchor.setAttribute("target", "_blank");
        //set src
        img.src = "/ShowThumbnail.ashx?imgid=" + srcs[i];
        img.className = "search-img";
        img.setAttribute("data-width", widths[i]);
        img.setAttribute("data-height", heights[i]);
        anchor.appendChild(figure);
        imgs.push(anchor);
    }
}
