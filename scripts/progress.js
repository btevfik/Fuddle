function loadProgress()
{
    ProgressImg = document.getElementById("progressImg");
    document.getElementById("progress").style.visibility = "visible";
    setTimeout("ProgressImg.src = ProgressImg.src", 200);
    return true;
}