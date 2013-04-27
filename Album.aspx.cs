using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Album : System.Web.UI.Page
{
    //loggedin user
    string loggedUser;
    //album id
    int albumId;
    //user who ows this album
    string user;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            albumId = Int32.Parse(Request.QueryString["id"]);
            string title = FuddleAlbum.getTitle(albumId);
            user = FuddleAlbum.getUser(albumId);
            if (title == "" || user == "")
            {
                throw new Exception();
            }
            AlbumTitle.Text = title;
            AlbumUser.Text = user;
            AlbumUser.NavigateUrl = "/user/" + user;
            //set the title of the page
            Page.Header.Title = "Fuddle | " + title;

        }
        catch
        {
            Response.Redirect("/Oops.aspx?e=404");
        }

        //get logged in user name
        MembershipUser u = Membership.GetUser();
        if (u != null)
            loggedUser = u.UserName;

        //load images of this album
        loadImages();

        //show update title button
        if (loggedUser == user)
        {
            UpdateTitleButton.Visible = true;
        }
    }

    protected void loadImages()
    {
        //List<int> imgs = FuddleAlbum.getImages(albumId);
        //int rows = imgs.Count/5;
        int rows = 10;
        for (int k = 0; k < rows; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/resources/gravatar.jpg";
                    imglink.ImageUrl = "/resources/gravatar.jpg";
                    imglink.CssClass = "album-img";
                    TableCell c = new TableCell();
                    c.Controls.Add(imglink);
                    if (loggedUser == user)
                    {
                        CheckBox removeBox = new CheckBox();
                        removeBox.ID = i + k*rows + "";
                        removeBox.CssClass = "remove-box";
                        c.Controls.Add(removeBox);
                    }
                    row1.Cells.Add(c);
                }
                catch
                {
                    break;
                }
                if (row1.Cells.Count > 0)
                {
                    ImageTable.Rows.Add(row1);
                }
            }
        }
    }

    protected void UpdateTitleButton_Click(object sender, EventArgs e)
    {
        UpdateTitleButton.Visible = false;
        SaveTitleButton.Visible = true;
        NewAlbumTitle.Visible = true;
    }
    protected void SaveTitleButton_Click(object sender, EventArgs e)
    {
        try
        {
            FuddleAlbum.updateTitle(NewAlbumTitle.Text, albumId);
            AlbumTitle.Text = NewAlbumTitle.Text;
            UpdateTitleButton.Visible = true;
            SaveTitleButton.Visible = false;
            NewAlbumTitle.Visible = false;
        }
        catch
        {
            //something bad ???
        }
    }
    protected void deleteSelectedButton_Click(object sender, EventArgs e)
    {
        var boxes =  Page.GetAllControlsOfType<CheckBox>();
        foreach (CheckBox checkbox in boxes)
        {
            //if checked remove from album
            if (checkbox.Checked)
            {
                System.Diagnostics.Debug.WriteLine(checkbox.ID);
            }
            //reset boxes
            checkbox.Checked = false;
        }
    }
}

/// <summary>
///is used to get all the checkboxes in the page
/// </summary>
public static class ControlExtensions
{
    public static IEnumerable<T> GetAllControlsOfType<T>(this Control parent) where T : Control
    {
        var result = new List<T>();
        foreach (Control control in parent.Controls)
        {
            if (control is T)
            {
                result.Add((T)control);
            }
            if (control.HasControls())
            {
                result.AddRange(control.GetAllControlsOfType<T>());
            }
        }
        return result;
    }
}
