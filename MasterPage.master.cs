using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if user is logged in display avatar
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {

            System.Web.UI.WebControls.Image image1 = (System.Web.UI.WebControls.Image)LoginView1.FindControl("profileImage");
            image1.ImageUrl = "/GetAvatar.ashx?user="+Membership.GetUser().UserName;
            image1.CssClass = "profile-img";
        }
    }

    //on search submit
    public void searchInput(object sender, EventArgs e)
    {
      //get input
      string input = HttpUtility.HtmlEncode(searchBox.Text);
      //replace space with plus
      string query = input.Replace(" ", "+");
      //redirect to search page
      Response.Redirect("~/Search.aspx?q=" + query);
    }

    //after logout redirect to default
    protected void OnLogout(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

}
