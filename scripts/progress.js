function loadProgress()
{
    ProgressImg = document.getElementById("progressImg");
    document.getElementById("progress").style.display = "block";
    setTimeout("ProgressImg.src = ProgressImg.src", 200);
    return true;
}