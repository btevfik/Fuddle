function getImages(parameter) {
    $.ajax({
        type: "POST",
        url: "SearchService.asmx/GetImages",
        data: "{'query':'" + parameter + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var images = response.d;
            var count=0;
            $('#searchresults').empty();
            $.each(images, function (index, image) {
                count++;
                $('#searchresults').append('<img class="search-img" data-width="' + image.width + '" ' + 'data-height="' + image.height + '" ' + 'src="' + image.link + '"/>');
                runLayout(205);
            });
            $('#numresult').html(count+" images found.");
        },
        failure: function (msg) {
            $('#searchresults').text(msg);
        }
    });
}

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
            $('#searchresults').empty();
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
            $('#searchresults').empty();
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