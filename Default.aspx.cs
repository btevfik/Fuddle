using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ImageVoteList> topImages = FuddleImage.getTopImages();
        foreach(ImageVoteList topImage in topImages){
        Literal image = new Literal();
        image.Text = "<a href='/Image.aspx?id=" + topImage.img_id + "'><figure><img src='/ShowThumbnail.ashx?imgid=" + topImage.img_id + "' width='200'/><figcaption>" + topImage.votes + " Upvotes </figcaption></figure></a>";
        container.Controls.Add(image);
        }

    }
}