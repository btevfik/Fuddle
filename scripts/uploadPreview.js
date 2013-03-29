function previewImage(input)
{
    if (input.files && input.files[0])
    {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#image')
                .attr('src', e.target.result)
                .width(input.file.width / 2)
                .height(input.file.height / 2);
        };

        reader.readAsDataURL(input.files[0]);
    }
}