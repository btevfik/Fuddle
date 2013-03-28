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

    //on login submit
    protected void login(object sender, EventArgs e)
    {
      System.Diagnostics.Debug.WriteLine("Email: " + username.Text);
      System.Diagnostics.Debug.WriteLine("Password: " + password.Text);
      if (username.Text == "" || password.Text == "")
      {
          loginError.Text = "Not successful. Try again.";     
      }
    }

    //on search submit
    protected void searchInput(object sender, EventArgs e)
    {
      //get input
      string input = HttpUtility.HtmlEncode(searchBox.Text);
      //replace space with plus
      string query = input.Replace(" ", "+");
      //redirect to search page
      Response.Redirect("/Search.aspx?q=" + query);
    }

}
