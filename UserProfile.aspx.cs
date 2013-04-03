using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the requested user
        string user = Request.QueryString["user"];

        //if nothing is given then by default display currently logged in users page
        if (user == null || user == "")
        {
            //get current user
            

            //if no user logged in, redirect to not found
            Response.Redirect("/Oops.aspx?e=404");
        }

        //if a user is specified in the url
        //find the user in database

        //if found, fill the page
        Label1.Text = user;
        
        //if not found direct to not found
        //this is just a test for redirection
        if (user == "nouser")
        {
            Response.Redirect("/Oops.aspx?e=404");
        }
    }
}