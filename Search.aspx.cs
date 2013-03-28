using System;
using System.Collections.Generic;
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
        //display the string
        searchedQuery.Text = query;
    }
}