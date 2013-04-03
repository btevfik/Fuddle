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
            Response.Redirect("/Default.aspx");
        }
        Login1.FailureText = "Ah ah ah! You didn't say the magic word.";
    }
}