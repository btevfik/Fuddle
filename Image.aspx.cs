using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the requested img
        string id = Request.QueryString["id"];
        Image1.ImageUrl = "/ShowImage.ashx?imgid=" + id;
    }
}