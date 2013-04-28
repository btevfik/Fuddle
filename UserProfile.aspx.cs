using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Globalization;
using System.Web.UI.HtmlControls;


//THIS USER PROFILE PAGE IS PUBLIC PROFILE
//EVERYTHING SHOULD BE READ-ONLY
//LOGGED IN USERS PROFILE IS AT /member/MyProfile.aspx
//WHERE HE CAN EDIT HIS PROFILE
public partial class UserProfile : System.Web.UI.Page
{
    protected int table_state
    {
        get { return (int)Session["table_state"]; }
        set { Session["table_state"] = value; }
    }
    
    protected int album_index
    {
        get { return (int)Session["album_index"]; }
        set { Session["album_index"] = value; }
    }

    protected List<int> albums
    {
        get { return (List<int>)Session["albums"]; }
        set { Session["albums"] = value; }
    }

    protected int cuddle_index
    {
        get { return (int)Session["cuddle_index"]; }
        set { Session["cuddle_index"] = value; }
    }

    protected List<int> cuddles
    {
        get { return (List<int>)Session["cuddles"]; }
        set { Session["cuddles"] = value; }
    }

    protected int upload_index
    {
        get { return (int)Session["upload_index"]; }
        set { Session["upload_index"] = value; }
    }

    protected List<int> uploads
    {
        get { return (List<int>)Session["images"]; }
        set { Session["images"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //get the requested user
            string user = Request.QueryString["user"];

            //member user
            MembershipUser u = null;

            //if no parameter is given redirect to 404
            if (user == null || user == "")
            {
                Response.Redirect("/Oops.aspx?e=404");
            }

            //if a user is specified in the url with "user=username"
            //find the specific user in database
            else
            {
                u = Membership.GetUser(user);
            }

            //if found, fill the page
            if (u != null)
            {
                //display username
                userLabel.Text = u.UserName;

                //set the title of the page
                Page.Header.Title = "Fuddle | " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(u.UserName.ToString().ToLower());

                //set gravatar
                AvatarImage.ImageUrl = "getavatar.ashx?user=" + u.UserName + "&size=200";

                //set user bio
                FuddleUser account = new FuddleUser(u.UserName);
                aboutmeLabel.Text = account.getBio();

                //get user uploads
                uploads = account.getuploads();

                //get user cuddles
                cuddles = FuddleVote.getCuddles((Guid)u.ProviderUserKey);
                
                //Displays user uploads
                upload_index = 5;
                loadUploads();

                //Display Album
                albums = FuddleAlbum.getAllAlbums((Guid) u.ProviderUserKey);
                album_index = 2;
                loadAlbums();

            }
            //if not found, direct to 404
            else
            {
                Response.Redirect("/Oops.aspx?e=404");
            }
        }
    }


    protected void loaduploads_Click(object sender, EventArgs e)
    {
        loadUploads();
    }
    protected void albumLink_Click(object sender, EventArgs e)
    {
        table_state = 0; //Display Albums
        album_index = 2;
        loadAlbums();

    }
    protected void cuddleLink_Click(object sender, EventArgs e)
    {
        table_state = 1; //Display Cuddles
        cuddle_index = 2;
        loadCuddles();
    }
    protected void loadrows_Click(object sender, EventArgs e)
    {
        if (table_state == 0)
        {
            loadAlbums();
        }
        else if (table_state == 1)
        {
            loadCuddles();
        }
    }


    protected void loadUploads()
    {
        //Displays User Uploads
        for (int i = 0; i < upload_index; i++)
        {
            try
            {
                HyperLink imglink = new HyperLink();
                imglink.NavigateUrl = "/Image.aspx?id=" + uploads[i];
                imglink.ImageUrl = "/ShowImage.ashx?imgid=" + uploads[i];
                imglink.ToolTip = FuddleImage.getTitle(uploads[i]);
                imglink.CssClass = "imgupload";
                Control contentpanel = RecentUpload.ContentTemplateContainer;
                contentpanel.Controls.AddAt(contentpanel.Controls.Count - 2, imglink);
            }
            catch (ArgumentOutOfRangeException)
            {
                loaduploads.Visible = false;
                break;
            }
        }
        upload_index += 5;
    }

    protected void loadAlbums()
    {
        for (int k = 0; k < album_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    var figure = new HtmlGenericControl("figure");
                    var figcaption = new HtmlGenericControl("figcaption");
                    figcaption.InnerText = FuddleAlbum.getTitle(albums[i + k * 5]);
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Album.aspx?id=" + albums[i + k * 5];
                    int albumCoverId = FuddleAlbum.getAlbumCover(albums[i + k * 5]);
                    System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                    //if no cover show default
                    if (albumCoverId == -1)
                    {
                        img.ImageUrl = "/resources/gravatar.jpg";
                    }
                    //if cover found
                    else
                    {
                        img.ImageUrl = "/ShowThumbnail.ashx?imgid=" + albumCoverId;
                    }
                    figure.Controls.Add(img);
                    figure.Controls.Add(figcaption);
                    imglink.ToolTip = FuddleAlbum.getTitle(albums[i + k * 5]);
                    imglink.CssClass = "imgtab";
                    imglink.Controls.Add(figure);
                    TableCell c = new TableCell();
                    c.Controls.Add(imglink);
                    row1.Cells.Add(c);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            if (row1.Cells.Count > 0)
            {
                Table1.Rows.Add(row1);
            }
        }
        if (album_index * 5 < albums.Count)
        {
            loadrows.Visible = true;
        }
        else
        {
            loadrows.Visible = false;
        }
        album_index += 2;
    }

    protected void loadCuddles()
    {
        for (int k = 0; k < cuddle_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Image.aspx?id=" + cuddles[i + k * 5];
                    imglink.ImageUrl = "/ShowThumbnail.ashx?imgid=" + cuddles[i + k * 5];
                    imglink.ToolTip = FuddleImage.getTitle(cuddles[i + k * 5]);
                    imglink.CssClass = "imgtab";
                    TableCell c = new TableCell();
                    c.Controls.Add(imglink);
                    row1.Cells.Add(c);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            if (row1.Cells.Count > 0)
            {
                Table1.Rows.Add(row1);
            }
        }
        if (cuddle_index * 5 < cuddles.Count)
        {
            loadrows.Visible = true;
        }
        else
        {
            loadrows.Visible = false;
        }
        cuddle_index += 2;
    }
}
