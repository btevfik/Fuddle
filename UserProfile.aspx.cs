﻿using System;
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
                string bio = account.getBio();
                //if (!bio.Equals(""))
                    aboutmeLabel.Text = bio;

                //get user uploads
                SearchService ss = new SearchService();
                this.images = ss.GetUserUploads(user);
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
            //if not found, direct to 404
            else
            {
                Response.Redirect("/Oops.aspx?e=404");
            }
        }
    }


    protected void loaduploads_Click(object sender, EventArgs e)
    {
        //Displays User Uploads
        upload_index += 5;
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
    }
    protected void albumLink_Click(object sender, EventArgs e)
    {

    }
    protected void cuddleLink_Click(object sender, EventArgs e)
    {
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
}
