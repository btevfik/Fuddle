function previewImage(input) {
    var browserName = navigator.appName;
    if (browserName == "Microsoft Internet Explorer") {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image')
                    .attr('src', e.target.result)
                    .width(200)
                    .height(200)
            };

            reader.readAsDataURL(input.files[0]);
        }
    }
    else {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image')
                    .attr('src', e.target.result)
                    .width(input.file.width)
                    .height(input.file.height)
            };

            reader.readAsDataURL(input.files[0]);
        }
    }
}