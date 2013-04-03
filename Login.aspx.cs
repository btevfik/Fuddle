using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if user is logged in redirect to default page.
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    //after login redirect to default
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

}