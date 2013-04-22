using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Globalization;


//THIS USER PROFILE PAGE IS PUBLIC PROFILE
//EVERYTHING SHOULD BE READ-ONLY
//LOGGED IN USERS PROFILE IS AT /member/MyProfile.aspx
//WHERE HE CAN EDIT HIS PROFILE
public partial class UserProfile : System.Web.UI.Page
{
    private int upload_index;
    private List<SSImage> images;

    protected void Page_Load(object sender, EventArgs e)
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
            AvatarImage.ImageUrl = "getavatar.ashx?user="+u.UserName+"&size=200";

            //get user uploads
            SearchService ss = new SearchService();
            this.images = ss.GetUserUploads(user);
            //Displays user uploads
            for (upload_index = 0; upload_index < 5; upload_index++)
            {
                try
                {
                    SSImage img = images[upload_index];
                    HyperLink imglink = new HyperLink();
                    imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                    imglink.ImageUrl = "/ShowImage.ashx?imgid=" + img.id;
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
        //if not found, direct to 404
        else
        {
            Response.Redirect("/Oops.aspx?e=404");
        }
    }


    protected void loaduploads_Click(object sender, EventArgs e)
    {
        //Displays User Uploads
        for (int i = 0; i < 5; i++, upload_index++)
        {
            try
            {
                SSImage img = images[upload_index];
                HyperLink imglink = new HyperLink();
                imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                imglink.ImageUrl = "/ShowImage.ashx?imgid=" + img.id;
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
        TableRow row1 = new TableRow();
        TableRow row2 = new TableRow();
        int i;
        for (i = 0; i < 5; i++)
        {
            try
            {
                SSImage img = images[i];
                HyperLink imglink = new HyperLink();
                imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                imglink.ImageUrl = "/ShowImage.ashx?imgid=" + img.id;
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
        for (; i < 10; i++)
        {
            try
            {
                SSImage img = images[i];
                HyperLink imglink = new HyperLink();
                imglink.NavigateUrl = "/Image.aspx?id=" + img.id;
                imglink.ImageUrl = "/ShowImage.ashx?imgid=" + img.id;
                imglink.CssClass = "imgtab";
                TableCell c = new TableCell();
                c.Controls.Add(imglink);
                row2.Cells.Add(c);
            }
            catch (ArgumentOutOfRangeException)
            {
                break;
            }
        }
        if (row2.Cells.Count > 0)
        {
            Table1.Rows.Add(row2);
        }
    }
}
