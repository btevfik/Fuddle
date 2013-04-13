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
            Gravatar1.Email = u.Email;
        }
        //if not found, direct to 404
        else
        {
            Response.Redirect("/Oops.aspx?e=404");
        }
    }
}
