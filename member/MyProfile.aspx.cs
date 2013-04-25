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
    protected int cuddle_index
    {
        get { return (int)Session["cuddle_index"]; }
        set { Session["cuddle_index"] = value; }
    }

    protected int upload_index
    {
        get { return (int)Session["upload_index"]; }
        set { Session["upload_index"] = value; }
    }

    protected List<SSImage> images
    {
        get { return (List<SSImage>)Session["images"]; }
        set { Session["images"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MembershipUser u = Membership.GetUser();
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
        string bio = account.getBio();
        //if (!bio.Equals(""))
            aboutmeLabel.Text = bio;

        //get user uploads
        SearchService ss = new SearchService();
        this.images = ss.GetUserUploads(u.UserName);
        upload_index = 5;
        //Displays user uploads
        for (int i = 0; i < upload_index; i++)
        {
            try
            {
                SSImage img = images[i];
                HyperLink imglink = new HyperLink();
                imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                imglink.ImageUrl = "/ShowImage.ashx?imgid=" + img.id;
                imglink.ToolTip = img.title;
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
        //Display Album
        cuddle_index = 2;
        for (int k = 0; k < cuddle_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //Add new Hyperlink
                    SSImage img = images[i + k * 5];
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                    imglink.ImageUrl = "/ShowThumbnail.ashx?imgid=" + img.id;
                    imglink.ToolTip = img.title;
                    imglink.CssClass = "imgtab";
                    
                    //Add new Panel
                    Panel dimglink = new Panel();
                    dimglink.CssClass = "imgpanel";
                    dimglink.Controls.Add(imglink);

                    //Add delete button
                    Button deleteButton = new Button();
                    deleteButton.Text = "Delete";
                    deleteButton.ID = "deleteButton" + img.id.ToString();
                    deleteButton.CssClass = "deleteButton";
                    deleteButton.Click += new EventHandler(deleteButton_Click);
                    deleteButton.CommandArgument = img.id.ToString();
                    dimglink.Controls.Add(deleteButton);
                    deleteButton.OnClientClick = "return confirm('Are you sure want to delete from Cuddles?');";


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
        if (cuddle_index * 5 < images.Count)
        {
            loadrows.Visible = true;
        }
        else
        {
            loadrows.Visible = false;
        }
        cuddle_index += 2;

    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        Button clickedbutton = (Button)sender;
        FuddleImage.deleteImage(Convert.ToInt32(clickedbutton.CommandArgument));
        UpdatePanel1.Update();

    }


    protected void loaduploads_Click(object sender, EventArgs e)
    {
        //Displays User Uploads
        int start = RecentUpload.ContentTemplateContainer.Controls.Count - 3;
        upload_index += 5;
        for (int i = start; i < upload_index; i++)
        {
            try
            {
                SSImage img = images[i];
                HyperLink imglink = new HyperLink();
                imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                imglink.ImageUrl = "/ShowImage.ashx?imgid=" + img.id;
                imglink.ToolTip = img.title;
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
    }
    protected void albumLink_Click(object sender, EventArgs e)
    {

    }
    protected void cuddleLink_Click(object sender, EventArgs e)
    {
        Table1.Rows.Clear();
        cuddle_index = 2;
        for (int k = 0; k < cuddle_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    SSImage img = images[i + k * 5];
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                    imglink.ImageUrl = "/ShowThumbnail.ashx?imgid=" + img.id;
                    imglink.ToolTip = img.title;
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
        if (cuddle_index * 5 < images.Count)
        {
            loadrows.Visible = true;
        }
        else
        {
            loadrows.Visible = false;
        }
        cuddle_index += 2;
    }
    protected void loadrows_Click(object sender, EventArgs e)
    {
        for (int k = 0; k < cuddle_index; k++)
        {
            TableRow row1 = new TableRow();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    SSImage img = images[i + k * 5];
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                    imglink.ImageUrl = "/ShowThumbnail.ashx?imgid=" + img.id;
                    imglink.ToolTip = img.title;
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
        if (cuddle_index * 5 < images.Count)
        {
            loadrows.Visible = true;
        }
        else
        {
            loadrows.Visible = false;
        }
        cuddle_index += 2;
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
}
