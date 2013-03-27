using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Oops : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the error
        string error = Request.QueryString["error"];
        //set page title
        Page.Title = error;
        //set the image and text
        if (error == "404")
        {
            errImage.ImageUrl = "/resources/shy.jpg";
            errMessage.Text = "This page is being shy.";
        }
        else
        {
            errImage.ImageUrl = "/resources/wrong.jpg";
            errMessage.Text = "Oops! Something's gone wrong.";
        }
    }
}