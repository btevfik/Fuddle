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
    //id of image
    int id;
    //current user
    MembershipUser u;
    //get the vote on this img by logged in user
    string vote;


    protected void Page_Load(object sender, EventArgs e)
    {
        //get the requested img
        string reqId = Request.QueryString["id"];
        //get currently logged in user
        u = Membership.GetUser();

        if (reqId != null && reqId != "")
        {
            id = Int32.Parse(reqId);
        }

        if (!IsPostBack)
        {
            //if image not found redirect to 404
            if (FuddleImage.getUser(id) == "")
            {
                Response.Redirect("/Oops.aspx?e=404");
            }
        }

        //set img
        string url = "/ShowImage.ashx?imgid=" + id;
        Image1.ImageUrl = url;

        //set the voting counts        
        upCount.Text = FuddleVote.getUpCount(id).ToString();
        downCount.Text = FuddleVote.getDownCount(id).ToString();
        cuddleCount.Text = FuddleVote.getCuddleCount(id).ToString();


        //set widths of counts
        upCount.Width = upCount.Text.Length * 8;
        downCount.Width = downCount.Text.Length * 8;
        cuddleCount.Width = cuddleCount.Text.Length * 8;

        //set the title of the page
        Page.Header.Title = "Fuddle | " + FuddleImage.getTitle(id);

        //set title and description of image
        imageTitle.Text = FuddleImage.getTitle(id);
        imageDescription.Text = FuddleImage.getDescription(id);

        //set the user who uploded img
        imageUser.Text = "by <b><a href='/user/" + FuddleImage.getUser(id) + "' target='_blank'>" + FuddleImage.getUser(id) + "</a></b>";

        //load comments from database
        loadComments();

        //for simulating button click from javascript when enter is pressed
        ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(this.commentButton);

        //add this button (email) script
        ScriptManager.RegisterClientScriptInclude(this, UpdatePanel1.GetType(), "Test", "http://s7.addthis.com/js/300/addthis_widget.js#pubid=undefined");

        //Show deletebutton if logged inuser is the one who uploaded this picture
        string uploadedUser = FuddleImage.getUser(id); //returns the user who uploaded that picture        

        if (u != null)
        {
            vote = FuddleVote.checkIfVoted(id, (Guid)u.ProviderUserKey);
            if (vote == "up")
            {
                upButton.Attributes.Add("style", "background-color:#C4C4C4");
            }
            else if (vote == "down")
            {
                downButton.Attributes.Add("style", "background-color:#C4C4C4");
            }
            else
            {
                downButton.Attributes.Remove("style");
                upButton.Attributes.Remove("style");
            }
            if (uploadedUser == u.UserName)
            {
                deleteButton.Visible = true;
                updateButton.Visible = true;
            }
        }

        if (!IsPostBack)
        {
            // Spam stuff -------
            // Setting a threshold ratio of 1/10
            double threshold = 0.1;

            // Get the upvote and downvote counts and calculate the spam ratio
            double upvoteCount = FuddleVote.getUpCount(id);
            double downvoteCount = FuddleVote.getDownCount(id);
            downvoteCount *= -1;
            double spamRatio = upvoteCount / downvoteCount;

            // If the spam ratio is lower than the threshold, then flag the image as spam
            if (spamRatio <= threshold && upvoteCount != 0 && downvoteCount != 0)
            {
                error.Text = "This image has been flagged as spam!";
                lightbox.Visible = true;
                return;
            }
            else if (upvoteCount == 0 && downvoteCount >= 10)
            {
                error.Text = "This image has been flagged as spam!";
                lightbox.Visible = true;
                return;
            }
        }
    }

    //upvoting this image
    protected void upButton_Click(object sender, EventArgs e)
    {
        if (u == null)
        {
            error.Text = "Please login to vote.";
            lightbox.Visible = true;
            return;
        }
        vote = FuddleVote.checkIfVoted(id, (Guid)u.ProviderUserKey);
        if (vote == "non")
        {
            try
            {
                u = Membership.GetUser();
                if (u == null) throw new Exception();

                FuddleVote.addToUpCount(id);
                upCount.Text = FuddleVote.getUpCount(id).ToString();
                upCount.Width = upCount.Text.Length * 8;
                upButton.Attributes.Add("style", "background-color:#C4C4C4");

                // Clean up the table
                FuddleVote.dbGarbageCollector();
            }
            catch
            {
                error.Text = "Error voting.";
                lightbox.Visible = true;
            }
        }
        //if already upvoted remove
        else if (vote == "up")
        {
            FuddleVote.removeFromUpCount(id);
            upCount.Text = FuddleVote.getUpCount(id).ToString();
            upCount.Width = upCount.Text.Length * 8;
            upButton.Attributes.Remove("style");
        }
    }

    //down voting this image
    protected void downButton_Click(object sender, EventArgs e)
    {
        if (u == null)
        {
            error.Text = "Please login to vote.";
            lightbox.Visible = true;
            return;
        }
        vote = FuddleVote.checkIfVoted(id, (Guid)u.ProviderUserKey);
        if (vote == "non")
        {
            try
            {
                u = Membership.GetUser();
                if (u == null) throw new Exception();

                FuddleVote.addToDownCount(id);
                downCount.Text = FuddleVote.getDownCount(id).ToString();
                downCount.Width = downCount.Text.Length * 8;
                downButton.Attributes.Add("style", "background-color:#C4C4C4");

                // Clean up the table
                FuddleVote.dbGarbageCollector();
            }
            catch
            {
                error.Text = "Error voting.";
                lightbox.Visible = true;
            }
        }
        //if already downvoted remove
        else if (vote == "down")
        {
            FuddleVote.removeFromDownCount(id);
            downCount.Text = FuddleVote.getDownCount(id).ToString();
            downCount.Width = downCount.Text.Length * 8;
            downButton.Attributes.Remove("style");
        }
    }

    //cuddling this image
    protected void cuddleButton_Click(object sender, EventArgs e)
    {
        if (u == null)
        {
            error.Text = "Please login to cuddle.";
            lightbox.Visible = true;
            return;
        }
        try
        {
            u = Membership.GetUser();
            if (u == null) throw new Exception();

            FuddleVote.cuddleIt((Guid)u.ProviderUserKey, id).ToString();
            cuddleCount.Text = FuddleVote.getCuddleCount(id).ToString();
            cuddleCount.Width = cuddleCount.Text.Length * 8;
        }
        catch
        {
            error.Text = "Error cuddling.";
            lightbox.Visible = true;
        }
    }

    //adding a comment for this image 
    //errors don't display due to the fact that jquery keypress registers twice if commentbutton async trigger added both on updatepanel5 and 6.
    //panelsUpdated[i].id === "UpdatePanel5" needs to be checked for panel 6 to get the error working
    //this will cause twice register though.
    protected void commentButton_Click(object sender, EventArgs e)
    {
        u = Membership.GetUser();
        //get comment box
        TextBox commentBox = LoginView1.FindControl("AddCommentBox") as TextBox;

        //if nothing is entered in commentbox
        if (commentBox.Text.Length <= 0)
        {
            error.Text = "Please enter a comment.";
            lightbox.Visible = true;
            return;
        }

        //add comment to database
        int commId = FuddleImage.addComment(commentBox.Text, id);

        //there is an error adding comment
        if (commId == -1)
        {
            System.Diagnostics.Debug.WriteLine("Error Commenting.");
            error.Text = "Error commenting.";
            lightbox.Visible = true;
        }
        //when comments are added clear the commentbox
        else
        {
            //create a comment literal        
            Literal myComment = new Literal();
            myComment.Text = "<div id='comment" + commId + "' class='comment'><div class='pro-image'><img src='/GetAvatar.ashx?user=" + u.UserName + "'/></div><div class='comm-cont'><span class='commenter'><a href='/user/" + u.UserName + "'target='_blank'>" + u.UserName + "</a></span><span class='message'>" + commentBox.Text + "</span><span class='date'> just now &nbsp;<button type='button' class='deleteCommentButton submitButton' value='" + commId + "'>Delete</button></span></div></div>";
            //add to comment panel        
            commentPanel.Controls.AddAt(0, myComment);
            //clear comment box
            commentBox.Text = "";
            //hide no comment
            nocomment.Visible = false;
        }
    }

    //load all the comments for this image
    protected void loadComments()
    {
        List<Comment_Info> comments = FuddleImage.getComments(id);  //this should return all the comments for that image
        //display no comments if non found
        if (comments.Count == 0)
        {
            nocomment.Visible = true;
            return;
        }
        else
        {
            nocomment.Visible = false;
        }
        //load comments below
        foreach (Comment_Info comment in comments)
        {
            Literal myComment = new Literal();
            if (u != null)
            {
                if (comment.username == u.UserName)
                {
                    myComment.Text = "<div id='comment" + comment.id + "' class='comment'><div class='pro-image'><img src='/GetAvatar.ashx?user=" + comment.username + "'/></div><div class='comm-cont'><span class='commenter'><a href='/user/" + comment.username + "'target='_blank'>" + comment.username + "</a></span><span class='message'>" + comment.comment + "</span><span class='date'>" + findTimeDiff(comment.date) + "&nbsp;<button type='button' class='deleteCommentButton submitButton' value='" + comment.id + "'>Delete</button></span></div></div>";
                }
                else
                {
                    myComment.Text = "<div class='comment'><div class='pro-image'><img src='/GetAvatar.ashx?user=" + comment.username + "'/></div><div class='comm-cont'><span class='commenter'><a href='/user/" + comment.username + "'target='_blank'>" + comment.username + "</a></span><span class='message'>" + comment.comment + "</span><span class='date'>" + findTimeDiff(comment.date) + "</span></div></div>";
                }
            }
            else
            {
                myComment.Text = "<div class='comment'><div class='pro-image'><img src='/GetAvatar.ashx?user=" + comment.username + "'/></div><div class='comm-cont'><span class='commenter'><a href='/user/" + comment.username + "'target='_blank'>" + comment.username + "</a></span><span class='message'>" + comment.comment + "</span><span class='date'>" + findTimeDiff(comment.date) + "</span></div></div>";
            }
            //add to comment panel
            commentPanel.Controls.AddAt(0, myComment);
        }
    }

    //delete button is clicked
    protected void delete_Click(object sender, EventArgs e)
    {
        try
        {
            //delete this image
            FuddleImage.deleteImage(id);
            //delete from albums as well, if it exists in any
            FuddleAlbum.deleteImage(id);
            //display deleted message
            error.Text = "Image deleted.";
            lightbox.Visible = true;
            Session["deleted"] = true;
        }
        catch
        {
            error.Text = "Error on deletion.";
            lightbox.Visible = true;
            Session["deleted"] = false;
        }
    }

    protected string findTimeDiff(DateTime then)
    {
        var ts = new TimeSpan(DateTime.UtcNow.Ticks - then.Ticks);
        double delta = Math.Abs(ts.TotalSeconds);

        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        if (delta < 0)
        {
            return "not yet";
        }
        if (delta < 1 * MINUTE)
        {
            return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
        }
        if (delta < 2 * MINUTE)
        {
            return "a minute ago";
        }
        if (delta < 45 * MINUTE)
        {
            return ts.Minutes + " minutes ago";
        }
        if (delta < 90 * MINUTE)
        {
            return "an hour ago";
        }
        if (delta < 24 * HOUR)
        {
            return ts.Hours + " hours ago";
        }
        if (delta < 48 * HOUR)
        {
            return "yesterday";
        }
        if (delta < 30 * DAY)
        {
            return ts.Days + " days ago";
        }
        if (delta < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            return months <= 1 ? "one month ago" : months + " months ago";
        }
        else
        {
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        //change desc to textbox
        updateDesc.Text = imageDescription.Text;
        updateDesc.Visible = true;
        imageDescription.Visible = false;

        //change title to textbox
        updateTitle.Text = imageTitle.Text;
        updateTitle.Visible = true;
        imageTitle.Visible = false;

        //show save button
        saveButton.Visible = true;
        //hide update button
        updateButton.Visible = false;

        //clear error
        error.Text = "";
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        try
        {
            //get new info
            string newTitle = updateTitle.Text;
            string newDesc = updateDesc.Text;
            //if empty throw exception
            if (newTitle == "" || newDesc == "") throw new Exception();
            //update to info
            FuddleImage.updateTitle(newTitle, id);
            FuddleImage.updateDescription(newDesc, id);
            //disable save button, and textboxes
            saveButton.Visible = false;
            updateTitle.Visible = false;
            updateDesc.Visible = false;
            //copy over text
            imageTitle.Text = updateTitle.Text;
            imageDescription.Text = updateDesc.Text;
            //make labels visible
            imageDescription.Visible = true;
            imageTitle.Visible = true;
        }
        catch
        {
            //disable save button, and textboxes
            saveButton.Visible = false;
            updateTitle.Visible = false;
            updateDesc.Visible = false;
            //make labels visible
            imageDescription.Visible = true;
            imageTitle.Visible = true;
            error.Text = "Error updating image info.";
            lightbox.Visible = true;
        }
    }

    protected void closeError_Click(object sender, EventArgs e)
    {
        lightbox.Visible = false;
        try
        {
            bool test = (bool)Session["deleted"];
            if (test == true)
            {
                Session.Remove("deleted");
                Response.Redirect("/member/MyProfile.aspx");
            }
        }
        catch
        {
            //who cares?
        }
    }

    //delete a comment with given id
    [WebMethod]
    public static bool deleteComment(int id)
    {
        bool result = FuddleImage.deleteComment(id);
        return result;
    }
}