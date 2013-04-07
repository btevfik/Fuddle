using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
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
