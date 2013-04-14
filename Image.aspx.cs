using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get the requested img
        string id = Request.QueryString["id"];
        //set img
        string url = "/ShowImage.ashx?imgid=" + id;
        Image1.ImageUrl = url;
        //set width of comment box
        if (Page.User.Identity.IsAuthenticated)
        {
            TextBox commentBox = LoginView1.FindControl("AddCommentBox") as TextBox;
            commentBox.Width = getWidth(id);
        }
        //set widths of counts
        upCount.Width = upCount.Text.Length * 8;
        downCount.Width = downCount.Text.Length * 8;
        cuddleCount.Width = cuddleCount.Text.Length * 8;
    }


    //can be moved to a separete image class for reuse
     protected int getWidth(string id){
     SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        int width=300;
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);

            cmd = new SqlCommand("SELECT Image_width FROM [Image_table] WHERE Image_id = " + id, conn);
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                width = (int )rdr["Image_width"];
                if (width > 800)
                {
                    width = 800;
                }
            }

            if (rdr != null)
                rdr.Close();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
         return width+10;
    }
}