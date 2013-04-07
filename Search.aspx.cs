using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the query string
        string query = Request.QueryString["q"];

        //display the query string
        searchedQuery.Text = "You have searched for: <b>" + query + "</b><br><br>Fuddle found you these.";

        //if nothing is submitted clear the area, display message
        if (query == "" || query == null)
        {
            //searchresults.Controls.Clear();
            searchedQuery.Text = "Use the box in the header to search for cute elephants.";
            return;
        }
    }
}