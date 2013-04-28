using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

/// <summary>
/// this class is used for CRUD image
/// </summary>

//comment class that holds info about a comment
public class Comment_Info
{
    public string username;
    public string comment;
    public DateTime date;
    public int id;

    public Comment_Info(int id, string username, string comment, DateTime date)
    {
        this.id = id;
        this.username = username;
        this.comment = comment;
        this.date = date;
    }
}

public class ImageVoteList
{
    public int img_id;
    public int votes;
    public int rank;

    public ImageVoteList(int img_id, int votes, int rank)
    {
        this.img_id = img_id;
        this.votes = votes;
        this.rank = rank;
    }
}

public class FuddleImage
{
    static string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
    public FuddleImage()
    {
        //
        // TODO: No constructor here. Move away.
        //
    }

    public static int addComment(string comment, int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);

        //just inserted id.
        int idOut=-1;

        //Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        string usrname = (string)user.UserName;

        //Grabs the datetime of creation for the comment
        DateTime dateOfCreation = DateTime.UtcNow;

        try
        {
            // Insert new comment into database
            SqlCommand cmd = new SqlCommand("INSERT INTO [Comments_table] (Image_id, Comment, Creation_date_time, Username)" + "VALUES(@imageId, @comment, @dateofcreation, @username);SELECT @id = SCOPE_IDENTITY()", conn);
            cmd.Parameters.Add("@imageId", SqlDbType.Int).Value = image_id.ToString();
            cmd.Parameters.Add("@comment", SqlDbType.VarChar).Value = comment;
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = usrname;
            cmd.Parameters.Add("@dateofcreation", SqlDbType.DateTime2).Value = dateOfCreation;
            cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;    
            
            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            idOut = (int)cmd.Parameters["@id"].Value;
            System.Diagnostics.Debug.WriteLine("Inserted id: "+idOut);
        }
        catch
        {
            return -1;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return idOut;
    }

    //update description of this image
    public static void updateDescription(string s, int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Update the image description
            SqlCommand cmd = new SqlCommand("UPDATE [Image_table] SET Image_desc = '" + s + "'" + " WHERE Image_id = '" + image_id.ToString() + "'", conn);

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    //update title of this image
    public static void updateTitle(string s, int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Update the image title
            SqlCommand cmd = new SqlCommand("UPDATE [Image_table] SET Image_title = '" + s + "'" + " WHERE Image_id = '" + image_id.ToString() + "'", conn);

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    //delete this image 
    public static void deleteImage(int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Delete the image 
            SqlCommand cmd = new SqlCommand("Delete FROM [Image_table] WHERE Image_id = '" + image_id.ToString() + "'", conn);

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    //get the title of this image
    public static string getTitle(int image_id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        string image_title = "";

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT Image_title FROM [Image_table] WHERE Image_id = '" + image_id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Retreive image title
            while (rdr.Read())
            {
                image_title = (string)rdr["Image_title"];
            }

            if (rdr != null)
            {
                rdr.Close();
            }
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return image_title;
    }

    //get the description of this image
    public static string getDescription(int image_id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        string image_desc = "";

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT Image_desc FROM [Image_table] WHERE Image_id = '" + image_id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Retreive image title
            while (rdr.Read())
            {
                image_desc = (string)rdr["Image_desc"];
            }

            if (rdr != null)
            {
                rdr.Close();
            }
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return image_desc;
    }

    //get all the comments for this image
    public static List<Comment_Info> getComments(int image_id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        List<Comment_Info> comments = new List<Comment_Info>();

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT cid, Comment, Creation_date_time, Username FROM [Comments_table] WHERE Image_id = '" + image_id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Retreive image comments in a list
            while (rdr.Read())
            {
                string comment = (string)rdr["Comment"];
                DateTime date = (DateTime)rdr["Creation_date_time"];
                string username = (string)rdr["Username"];
                int id = (int)rdr["cid"];
                Comment_Info com = new Comment_Info(id , username, comment, date);
                comments.Add(com);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return comments;
    }

    //delete comment with this id
    public static bool deleteComment(int id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Delete the image 
            SqlCommand cmd = new SqlCommand("Delete FROM [Comments_table] WHERE cid = '" + id + "'", conn);

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            return false;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return true;
    }

    //get the user who uploaded this image
    //return username
    public static string getUser(int image_id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        Guid user_id;
        MembershipUser u;
        string name = "";

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT User_id FROM [Image_table] WHERE Image_id = '" + image_id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Retreive user uploaded image
            while (rdr.Read())
            {
                user_id = (Guid)rdr["User_id"];
                u = Membership.GetUser(user_id);
                name = u.UserName;
            }

            if (rdr != null)
            {
                rdr.Close();
            }
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return name;
    }

    //get width of an image
    public static int getWidth(int id)
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

    // Get the top 20 images with the highest ranking (upvotes + downvotes because downvotes are negative)
    public static List<ImageVoteList> getTopImages()
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        // List of all images and their upvotes and rank
        List<ImageVoteList> ImageVotes = new List<ImageVoteList>();

        // List of top 20 images according to rank
        List<ImageVoteList> TopImages = new List<ImageVoteList>();

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT DISTINCT Image_id FROM [Vote_table]", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();


            // Retreive user uploaded image
            while (rdr.Read())
            {
                int img_id = (int)rdr["Image_id"];
                int votes = FuddleVote.getUpCount(img_id);
                int rank = votes + FuddleVote.getDownCount(img_id);
                ImageVoteList ivl = new ImageVoteList(img_id, votes, rank);
                ImageVotes.Add(ivl);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

        // Function to sort the list of images by rank
        ImageVotes.Sort((x, y) => x.rank.CompareTo(y.rank));

        // Reverse the order of the list to make it in descending order
        ImageVotes.Reverse();

        // Put the top 20 in the list TopImages
        for (int i = 0; i < 20; i++)
        {
            try
            {
                TopImages.Add(ImageVotes[i]);
            }
            catch
            {
                break;
            }
        }
        return TopImages;
    }
}