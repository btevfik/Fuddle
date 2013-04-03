using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the requested user
        string user = Request.QueryString["user"];

        //member user
        MembershipUser u = null;

        //if no parameter is given retrieve the logged in user
        if (user == null || user == "")
        {
            //get current user
            u = Membership.GetUser();
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
            Label1.Text = u.Email;
        }
        //if not found, direct to 404
        else
        {
            Response.Redirect("/Oops.aspx?e=404");
        }
    }
}