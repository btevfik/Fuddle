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
        //find the user in database

        //if found, fill the page
        Label1.Text = user;
        
        //if not found direct to not found
        //this is just a test for redirection
        if (user == "nouser" || user == null)
        {
            Response.Redirect("/Oops.aspx?e=404");
        }
    }
}