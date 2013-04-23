$(document).ready(function(){
// Check the File API support
if (window.File && window.FileReader && window.FileList && window.Blob) {
    // in process
} else {
    //remove preview image
    $("#right-half").css("display", "none");
    $("#left-half").css({
        "width" : "300px", 
        "margin": "0 auto",
        "float": "none"
        });
}
});

function previewImage(input) {
    $("#uploadStatus").css("display", "none");
    var browserName = navigator.appName;
    if (browserName == "Microsoft Internet Explorer") {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            var ext = input.value.split('.').pop(); // get the file's extension
            var extStr = ext.toString();
            ext = extStr.toLowerCase();

            if (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "gif" || ext == "bmp") {
                reader.onload = function (e) {
                    $('#image')
                        .attr('src', e.target.result)
                        .width(350)
                };
            }
            // if the file is not an image, show the non_image png
            else {
                reader.onload = function (e) {
                    $('#image')
                    .attr('src', "/resources/non_image.png")
                    .width(350)
                };
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    else {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            var ext = input.value.split('.').pop(); // get the file's extension
            var extStr = ext.toString();
            ext = extStr.toLowerCase();

            if (ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "gif" || ext == "bmp") {
                reader.onload = function (e) {
                    $('#image')
                        .attr('src', e.target.result)
                        .width(input.file.width)
                        .height(input.file.height)
                };
            }
            // if the file is not an image, show the non_image png
            else {
                reader.onload = function (e) {
                    $('#image')
                    .attr('src', "/resources/non_image.png")
                    .width(350)
                };
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
}