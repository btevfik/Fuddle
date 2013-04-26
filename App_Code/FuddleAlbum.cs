using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

/// <summary>
/// this class is used for CRUD album
/// </summary>
public class FuddleAlbum
{
    static string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
    public FuddleAlbum()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void createAlbum(string title)
    {
        SqlConnection conn = new SqlConnection(connString);

        //Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;
        try
        {
            // Insert new album into database
            string createNewAlbum = "INSERT INTO [Album_table] (Album_title, User_id)" + "values (@newTitle, @userId)";
            SqlCommand cmd = new SqlCommand(createNewAlbum);
            cmd.Parameters.Add("@newTitle", SqlDbType.VarChar).Value = title;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

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

    public static void addImage(int album_id, string album_title, int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);

        //Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;
        try
        {
            // Insert the image into the album table
            string insertQuery = "INSERT INTO [Album_table] ( User_Id, Album_id, Image_id, Album_title)"
                + "values ( @userId, @albumId, @imageId, @albumTitle)";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@newTitle", SqlDbType.VarChar).Value = album_title;
            cmd.Parameters.Add("@imageId", SqlDbType.Int).Value = image_id;
            cmd.Parameters.Add("@albumId", SqlDbType.Int).Value = album_id;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

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

    public static void updateTitle(string new_album_title, int album_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Update the album title
            SqlCommand cmd = new SqlCommand("UPDATE [Album_table] SET Album_title = '" + new_album_title + "'" + " WHERE Album_id = '" + album_id.ToString() + "'", conn);

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

    public static string getTitle(int album_id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        string album_title = "";

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT Album_title FROM [Album_table] WHERE Album_id = '" + album_id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Retreive album title
            while (rdr.Read())
            {
                album_title = (string)rdr["Album_title"];
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
        return album_title;
    }

    public static List<int> getImages(int album_id) //returns a list of Image Ids in the album
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        List<int> images = new List<int>();
        int imgID = -1;

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT Image_id FROM [Album_table] WHERE Album_id = '" + album_id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Add the image id to the list
            while (rdr.Read())
            {
                imgID = (int)rdr["Image_id"];
                images.Add(imgID);
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
        return images;
    }

    // Must use this method to delete all the specified user's albums when deleting the user 
    public static void deleteAllUsersAlbums(Guid id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Delete all the albums of the user
            SqlCommand cmd = new SqlCommand("Delete FROM [Album_table] WHERE User_id = '" + id.ToString() + "'", conn);

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

    public static void deleteAlbum(int album_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Delete the album 
            SqlCommand cmd = new SqlCommand("Delete FROM [Album_table] WHERE Album_id = '" + album_id.ToString() + "'", conn);

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

    public static void deleteImage(int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Delete the image 
            SqlCommand cmd = new SqlCommand("Delete FROM [Album_table] WHERE Image_id = '" + image_id.ToString() + "'", conn);

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

    public static List<int> getAllAlbums(Guid id)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        List<int> user_albums = new List<int>();
        int albumID = -1;

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT DISTINCT Album_id FROM [Album_table] WHERE User_id = '" + id.ToString() + "'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Add the album id to the list
            while (rdr.Read())
            {
                albumID = (int)rdr["Album_id"];
                user_albums.Add(albumID);
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
        return user_albums;
    }
}