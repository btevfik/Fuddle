using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if user is logged in redirect to default page.
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("/Default.aspx");
        }
    }
}