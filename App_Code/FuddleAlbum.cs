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

    static void createAlbum(string title)
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

    static void addImage(string title, string description, string contentType, Byte[] bytes, string fileName, double fileWidth, double fileHeight, double fileWidthThumb, double fileHeightThumb, Byte[] bytes_thumb, int album_id)
    {
        SqlConnection conn = new SqlConnection(connString);

        //Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;
        try
        {
            // Insert the image and its description and title into the database
            string insertQuery = "INSERT INTO [Image_table] (Image_title, Image_desc, Image_content_type, Image_data, Image_filename, Image_width, Image_height, "
                + "Image_thumbWidth, Image_thumbHeight, Image_thumbnail, User_Id, Album_id)"
                + "values (@newTitle, @newDesc, @newContentType, @newData, @newFilename, @newWidth, @newHeight, @newThumbWidth, @newThumbHeight, @newThumbnail, @userId, @albumId)";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@newTitle", SqlDbType.VarChar).Value = title;
            cmd.Parameters.Add("@newDesc", SqlDbType.VarChar).Value = description;
            cmd.Parameters.Add("@newContentType", SqlDbType.VarChar).Value = contentType;
            cmd.Parameters.Add("@newData", SqlDbType.Binary).Value = bytes;
            cmd.Parameters.Add("@newFilename", SqlDbType.VarChar).Value = fileName;
            cmd.Parameters.Add("@newWidth", SqlDbType.Int).Value = fileWidth;
            cmd.Parameters.Add("@newHeight", SqlDbType.Int).Value = fileHeight;
            cmd.Parameters.Add("@newThumbWidth", SqlDbType.Int).Value = fileWidthThumb;
            cmd.Parameters.Add("@newThumbHeight", SqlDbType.Int).Value = fileHeightThumb;
            cmd.Parameters.Add("@newThumbnail", SqlDbType.Binary).Value = bytes_thumb;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;
            cmd.Parameters.Add("@albumId", SqlDbType.Int).Value = album_id;

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

    static void updateTitle(string new_album_title, int album_id)
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
    
    static string getTitle(int album_id)
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
    
    static List<int> getImages(int album_id) //returns a list of Image Ids in the album
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        
        List<int> images = new List<int>();
        int imgID = -1;

        try
        {
            conn = new SqlConnection(connString);
            cmd = new SqlCommand("SELECT Image_id FROM [Image_table] WHERE Album_id = '" + album_id.ToString() + "'", conn);
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
    
}