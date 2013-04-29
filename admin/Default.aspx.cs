using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DeleteImagesButton_Click(object sender, EventArgs e)
    {
        try
        {
            string input = DeleteImagesText.Text;
            string[] ids = input.Split(',');
            foreach (string id in ids)
            {
                FuddleImage.deleteImage(Int32.Parse(id));
            }
            result.Text = "Images deleted.";
        }
        catch
        {
            result.Text = "Error deleting images.";
        }
    }
    protected void DeleteAlbumsButton_Click(object sender, EventArgs e)
    {
        try
        {
            string input = DeleteAlbumsText.Text;
            string[] ids = input.Split(',');
            foreach (string id in ids)
            {
                FuddleAlbum.deleteAlbum(Int32.Parse(id));
            }
            result.Text = "Albums deleted.";
        }
        catch
        {
            result.Text = "Error deleting albums.";
        }
    }
}
