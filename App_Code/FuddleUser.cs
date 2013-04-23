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
            cmd.Parameters.Add("@newData", SqlDbType.VarChar).Value = bioMessage;
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
            cmd.ExecuteNonQuery();

            // Retreive image title
            while (rdr.Read())
            {
                bio = (string)rdr["User_bio"];
            }

            if (rdr != null)
            {
                rdr.Close();
            }
        }
        catch (Exception ee)
        {
            //something right?
            return "N/A";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return bio;
    }
}