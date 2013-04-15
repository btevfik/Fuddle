using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.Services;

public partial class Image : System.Web.UI.Page
{
    //if of image
    string id;
    //current user
    MembershipUser u;
    //comments in the session
    List<Literal> persistControls = new List<Literal>();

    protected void Page_Load(object sender, EventArgs e)
    {
        //get the requested img
        id = Request.QueryString["id"];
        //set img
        string url = "/ShowImage.ashx?imgid=" + id;
        Image1.ImageUrl = url;
        //set width of comment box
        if (Page.User.Identity.IsAuthenticated)
        {
            TextBox commentBox = LoginView1.FindControl("AddCommentBox") as TextBox;
            commentBox.Width = getWidth(id); //later something like.. FuddleImage.getWidth(id);
        }
        //set widths of counts
        upCount.Width = upCount.Text.Length * 8;
        downCount.Width = downCount.Text.Length * 8;
        cuddleCount.Width = cuddleCount.Text.Length * 8;

        //set the voting counts
        /*
         upCount.Text  = FuddleImage.getUpCount(id);
         downCount.Text = FuddleImage.getDownCount(id);
         cuddleCount.Text = FuddleImage.getCuddleCount(id);
         */

         //set the title of the page
        /*
            Page.Header.Title = "Fuddle | " + FuddleImage.getTitle(id);
         */

        //set title and description of image
        /*
        imageTitle.Text = FuddleImage.getTitle(id);
        imageDescription.Text = FuddleImage.getDesc(id);
         */

        //load comments from database
        loadComments();

        //load session comments (we wont need when database is implemented)
        // if you already have some literal populated
        if (Session["persistControls"] != null)
        {
            // pull them out of the session
            persistControls = (List<Literal>)Session["persistControls"];
            foreach (Literal ltrls in persistControls)
                commentPanel.Controls.AddAt(0, ltrls); // and push them back into the page
        }

        //for simulating button click from javascript when enter is pressed
        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.commentButton);

    }


    //can be moved to a separete image class for reuse
    protected int getWidth(string id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        int width = 300;
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);

            cmd = new SqlCommand("SELECT Image_width FROM [Image_table] WHERE Image_id = @id", conn);
            if (id == null)
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@id", SqlDbType.Int, 16).Value = id;
            }
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                width = (int)rdr["Image_width"];
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
        return width + 10;
    }

    //upvoting this image
    protected void upButton_Click(object sender, EventArgs e)
    {
        try
        {
            u = Membership.GetUser();
            if (u == null) throw new Exception();
            /*
            bool result = FuddleImage.upVote(id,u.UserName);
            if(result == false){
                error.Text = "Error voting."
            }
            else{
             */
            upCount.Text = Int32.Parse(upCount.Text) + 1 + "";
            // }
        }
        catch
        {
            error.Text = "You are not logged in.";
        }
    }

    //down voting this image
    protected void downButton_Click(object sender, EventArgs e)
    {
        try
        {
            u = Membership.GetUser();
            if (u == null) throw new Exception();
            /*
            bool result = FuddleImage.downVote(id,u.UserName);
            if(result == false){
                error.Text = "Error voting."
            }
            else{
             */
             downCount.Text = Int32.Parse(downCount.Text) - 1 + ""; 
             // }
        }
        catch
        {
            error.Text = "You are not logged in.";
        }
    }

    //cuddling this image
    protected void cuddleButton_Click(object sender, EventArgs e)
    {
        try
        {
            u = Membership.GetUser();
            if (u == null) throw new Exception();
            /*
            bool result = FuddleUser.cuddleImage(id);
            if(result == false){
                error.Text = "Error cuddling."
            }
            else{
             */
            cuddleCount.Text = Int32.Parse(cuddleCount.Text) + 1 + "";
            //}
        }
        catch
        {
            error.Text = "You are not logged in.";
        }
    }

    //adding a comment for this image 
    protected void commentButton_Click(object sender, EventArgs e)
    {
        u = Membership.GetUser();
        //get comment box
        TextBox commentBox = LoginView1.FindControl("AddCommentBox") as TextBox;
        //create a comment literal
        Literal myComment = new Literal();
        myComment.Text = "<div class='comment' style='max-width:"+getWidth(id)+"px'><span class='commenter'><img src='/GetAvatar.ashx?user=" + u.UserName + "'/>&nbsp;<a href='/user/" + u.UserName + "'target='_blank'>" + u.UserName + "</a></span>" + commentBox.Text + "</div>";
        //add to comment panel
        commentPanel.Controls.AddAt(0, myComment);
        // add it to the list
        persistControls.Add(myComment);
        // put it in the session
        Session["persistControls"] = persistControls;
        //clear comment box
        commentBox.Text = "";

        //add comment to database
        /*
        bool result = FuddleImage.comment(id, commentBox.Text);
        if(result == false){
            error.Text = "Error commenting."
        }
        //when comments are added to database remove from session.
        else{
             persistControls.Clear();    
         }
         */
    }

    protected void loadComments()
    {
        //FuddleImage.getComments(id);  //this should return all the comments for that image
        //load comments below
    }
}