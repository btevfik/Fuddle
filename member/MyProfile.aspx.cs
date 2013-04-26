using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

//THIS USER PROFILE PAGE IS PRIVATE
//ONLY LOGGED IN USERS CAN VIEW IT
//WHICH SHOWS THE CURRENT LOGGED IN USER'S PROFILE
//HERE USERS SHOULD BE ABLE TO EDIT THEIR PROFILE
//PUBLIC PROFILE PAGE IS AT ~/UserProfile.aspx
public partial class member_MyProfile : System.Web.UI.Page
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

    protected MembershipUser u;

    protected void Page_Load(object sender, EventArgs e)
    {
        u = Membership.GetUser();
        
        //display username
        userLabel.Text = u.UserName;
        
        //display email
        userEmail.Text = u.Email;
        
        //display public profile link
        string url = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
        publicLink.NavigateUrl = url+ "/user/" + u.UserName;
        publicLink.Text = url+ "/user/" + u.UserName;
        
        //set gravatar
        AvatarImage.ImageUrl = "/getavatar.ashx?user=" + u.UserName + "&size=200";

        //set user bio
        FuddleUser account = new FuddleUser(u.UserName);
        aboutmeLabel.Text = account.getBio();

        //get user uploads
        uploads = account.getuploads();

        //Displays user uploads
        upload_index = 5;
        loadUploads();

        //Display Album
        albums = FuddleAlbum.getAllAlbums((Guid)u.ProviderUserKey);
        album_index = 2;
        loadAlbums();

        //get user cuddles
        cuddles = FuddleVote.getCuddles((Guid)u.ProviderUserKey);
        cuddle_index = 2;
        loadCuddles();

        table_state = 0;
    }

    protected void changeBio_Click(object sender, EventArgs e)
    {
        saveBio.Visible = true;
        cancelBio.Visible = true;
        changeBio.Visible = false;
        aboutmeLabel.Visible = false;
        aboutmeText.Visible = true;
        aboutmeText.Text = aboutmeLabel.Text;

    }
    protected void saveBio_Click(object sender, EventArgs e)
    {
        FuddleUser u = new FuddleUser(Membership.GetUser().UserName);
        if (u.changeBio(aboutmeText.Text))
        {
            aboutmeLabel.Text = aboutmeText.Text;
        }
        saveBio.Visible = false;
        cancelBio.Visible = false;
        changeBio.Visible = true;
        aboutmeLabel.Visible = true;
        aboutmeText.Visible = false;

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
        //clear table
        Table1.Controls.Clear();

        //add css
        albumListItem.Attributes.Add("class", "activated");
        //remove css
        cuddleListItem.Attributes.Remove("class");

        for (int k = 0; k < album_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    HyperLink imglink = new HyperLink();
                    //imglink.NavigateUrl = "/Image.aspx?id=" + albums[i];
                    //imglink.ImageUrl = "/ShowThumbnail.ashx?imgid=" + albums[i];
                    imglink.ToolTip = FuddleAlbum.getTitle(albums[i]);
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
        //clear table
        Table1.Controls.Clear();

        //add css
        cuddleListItem.Attributes.Add("class", "activated");
        //remove css
        albumListItem.Attributes.Remove("class");

        for (int k = 0; k < cuddle_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //Add new Hyperlink
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Image.aspx?id=" + cuddles[i + k * 5];
                    imglink.ImageUrl = "/ShowThumbnail.ashx?imgid=" + cuddles[i + k * 5];
                    imglink.ToolTip = FuddleImage.getTitle(cuddles[i + k * 5]);
                    imglink.CssClass = "imgtab";

                    //Add new Panel
                    Panel dimglink = new Panel();
                    dimglink.CssClass = "imgpanel";
                    dimglink.Controls.Add(imglink);

                    //Add delete button
                    Button deleteCuddle = new Button();
                    deleteCuddle.Text = "Delete";
                    deleteCuddle.ID = "deleteButton" + cuddles[i + k * 5].ToString();
                    deleteCuddle.CssClass = "deleteButton";
                    deleteCuddle.Click += new EventHandler(deleteCuddle_Click);
                    deleteCuddle.CausesValidation = false;
                    deleteCuddle.CommandArgument = cuddles[i + k * 5].ToString();
                    dimglink.Controls.Add(deleteCuddle);
                    deleteCuddle.OnClientClick = "return confirm('Are you sure want to delete from Cuddles?');";

                    //Add new Cell
                    TableCell c = new TableCell();
                    c.Controls.Add(dimglink);
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
        if (cuddle_index * 5 < uploads.Count)
        {
            loadrows.Visible = true;
        }
        else
        {
            loadrows.Visible = false;
        }
        cuddle_index += 2;
    }

    protected void deleteCuddle_Click(object sender, EventArgs e)
    {
        Button clickedbutton = (Button)sender;
        //remove cuddle from database
        FuddleVote.removeCuddle((Guid)u.ProviderUserKey, Convert.ToInt32(clickedbutton.CommandArgument));
        //update index
        cuddle_index -= 2;
        //update cuddles list
        cuddles = FuddleVote.getCuddles((Guid)u.ProviderUserKey);
        //load cuddles
        loadCuddles();
    }

}
