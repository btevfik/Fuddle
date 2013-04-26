using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;



/// <summary>
/// FuddleUser is used to update/get info of the membership user
/// </summary>
public class FuddleUser
{
    MembershipUser user;
    Guid id;
    protected string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;

	public FuddleUser(string username)
	{
        SqlConnection conn = new SqlConnection(connString);
        this.user = Membership.GetUser(username);
        this.id = (Guid)user.ProviderUserKey;
	}


    //change the type (gravatar || upload)
    public bool setAvatarType(string type){
        if (type == "gravatar")
        {
            //connection
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                // Insert the image into the database
                string insertQuery = "UPDATE [User_info] SET Use_gravatar = 1 WHERE User_id = @userId";
                SqlCommand cmd = new SqlCommand(insertQuery);
                cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

                // Execute the sql command                
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                //something right?
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return true;
        }
        else if (type == "upload")
        {
            //connection
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                // Insert the image into the database
                string insertQuery = "UPDATE [User_info] SET Use_gravatar = 0 WHERE User_id = @userId";
                SqlCommand cmd = new SqlCommand(insertQuery);
                cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

                // Execute the sql command                
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
                //something right?
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return true;
        }
        else
        {
            //bad request
            return false;
        }
    }


    //upload a new image for avatar
    public bool changeUploadedAvatar(byte[] image)
    {
        //connection
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Insert the image into the database
            string insertQuery = "UPDATE [User_info] SET User_avatar = @newData, Use_gravatar = @useGravatar WHERE User_id = @userId";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@newData", SqlDbType.Binary).Value = image;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;
            cmd.Parameters.Add("@useGravatar", SqlDbType.Bit).Value = 0;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            //something right?
            return false;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return true;
    }

    //change bio of this user
    public bool changeBio(string bioMessage)
    {
        //connection
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // change the bio message
            string insertQuery = "UPDATE [User_info] SET User_bio = @newBio WHERE User_id = @userId";
            SqlCommand cmd = new SqlCommand(insertQuery);
            cmd.Parameters.Add("@newBio", SqlDbType.VarChar).Value = bioMessage;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ee)
        {
            //something right?
            return false;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return true;
    }

    //get bio of this user
    public string getBio()
    {
        //connection
        SqlConnection conn = new SqlConnection(connString);
        //reader
        SqlDataReader rdr = null;
        //bio
        string bio = "";
        try
        {
            // change the bio message
            string selectQuery = "SELECT User_bio FROM [User_info] WHERE User_id = @userId";
            SqlCommand cmd = new SqlCommand(selectQuery);
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = id;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            rdr = cmd.ExecuteReader();

            // Retreive image title
            while (rdr.Read())
            {
                if (System.DBNull.Value.Equals(rdr["User_bio"]))
                    bio = "";
                else
                    bio = (string)rdr["User_bio"];
            }
            if (rdr != null)
            {
                rdr.Close();
            }
        }
        catch (IndexOutOfRangeException)
        {
            //something right?
            return "";
        }
        catch (Exception)
        {
            return "N/A";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return bio;
    }

    public List<int> getuploads()
    {
        List<int> img_ids = new List<int>();
        //connection
        SqlConnection conn = new SqlConnection(connString);
        //reader
        SqlDataReader rdr = null;
        try
        {
            // change the bio message
            string selectQuery = "SELECT Image_id FROM [Image_table] WHERE User_Id = @user_id ORDER BY Image_id DESC";
            SqlCommand cmd = new SqlCommand(selectQuery);
            cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier).Value = id;

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //get the id
                int img_id = ((int)rdr["Image_id"]);
                img_ids.Add(img_id);
            }

            if (rdr != null)
                rdr.Close();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        //return the json data
        return img_ids;
    }
}