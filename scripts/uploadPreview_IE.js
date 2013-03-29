function previewImage_IE(input)
{
    var newPreview = document.getElementById("preview_IE");
    newPreview.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = input.value;
    newPreview.style.width = "400px";
    newPreview.style.height = "400px";
}