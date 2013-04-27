using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int albumQuery = Int32.Parse(Request.QueryString["id"]);
            string title = FuddleAlbum.getTitle(albumQuery);
            string user = FuddleAlbum.getUser(albumQuery);
            if (title == "" || user == "")
                {
                    throw new Exception();
                }
            AlbumTitle.Text = title;
            AlbumUser.Text = user;
            AlbumUser.NavigateUrl = "/user/" + user;
        }
        catch
        {
            Response.Redirect("/Oops.aspx?e=404");
        }
    }
}