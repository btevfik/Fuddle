using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;

/// <summary>
/// This class handles all up votes and down votes for images.
/// </summary>
/// 
public class FuddleVote
{
    static string connString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;

	public FuddleVote()
	{
		//
		// TODO: No default constructor.
		//
	}

    public static int getUpCount(int image_id)
    {
        SqlConnection conn = new SqlConnection();
        int upCount = 0;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Vote_table] WHERE Image_id = " + image_id.ToString()
            + " AND UpVote = @true", conn);
            cmd.Parameters.Add("@true", System.Data.SqlDbType.Bit).Value = true;
            conn.Open();
            upCount = Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch
        {
            // If a -1 is returned, something went wrong
            return -1;
        }
        finally 
        {
            conn.Close();
            conn.Dispose();
        }

        return upCount;
    }

    public static int getDownCount(int image_id)
    {
        SqlConnection conn = new SqlConnection();
        int downCount = 0;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Vote_table] WHERE Image_id = " + image_id.ToString()
            + " AND DownVote = @true", conn);
            cmd.Parameters.Add("@true", System.Data.SqlDbType.Bit).Value = true;
            conn.Open();
            downCount = Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch
        {
            // If a -1 is returned, something went wrong
            return -1;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

        downCount *= -1;
        return downCount;
    }

    public static void addToUpCount(int image_id)
    {
        // Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;

        SqlConnection conn = new SqlConnection(connString);
        SqlDataReader rdr = null;

        bool upVote = false;
        bool downVote = false;

        try
        {
            SqlCommand sel_cmd = new SqlCommand("SELECT UpVote, DownVote FROM [Vote_table] WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            sel_cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            sel_cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            conn.Open();
            rdr = sel_cmd.ExecuteReader();
            while (rdr.Read())
            {
                upVote = (bool)rdr["UpVote"];
                downVote = (bool)rdr["DownVote"];
            }

            if (rdr != null)
                rdr.Close();

            conn.Close();

            // If the user has not voted on this particular image...
            if (!upVote && !downVote)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [Vote_table] (Image_id, UpVote, DownVote, User_id) "
                    + "VALUES (@newImageid, @newUpVote, @newDownVote, @newUserid)", conn);
                cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
                cmd.Parameters.Add("@newUpVote", System.Data.SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@newDownVote", System.Data.SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch
        {
            // It will never fail! :D
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public static void addToDownCount(int image_id)
    {
        // Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;

        SqlConnection conn = new SqlConnection(connString);
        SqlDataReader rdr = null;

        bool upVote = false;
        bool downVote = false;

        try
        {
            SqlCommand sel_cmd = new SqlCommand("SELECT UpVote, DownVote FROM [Vote_table] WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            sel_cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            sel_cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            conn.Open();
            rdr = sel_cmd.ExecuteReader();
            while (rdr.Read())
            {
                upVote = (bool)rdr["UpVote"];
                downVote = (bool)rdr["DownVote"];
            }

            if (rdr != null)
                rdr.Close();

            conn.Close();

            // If the user has not voted on this particular image...
            if (!upVote && !downVote)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [Vote_table] (Image_id, UpVote, DownVote, User_id) "
                    + "VALUES (@newImageid, @newUpVote, @newDownVote, @newUserid)", conn);
                cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
                cmd.Parameters.Add("@newUpVote", System.Data.SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@newDownVote", System.Data.SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch
        {
            // It will never fail! :D
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public static void removeFromUpCount(int image_id)
    {
        // Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;

        SqlConnection conn = new SqlConnection(connString);
        SqlDataReader rdr = null;

        bool upVote = false;

        try
        {
            SqlCommand cmd = new SqlCommand("SELECT UpVote FROM [Vote_table] WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                upVote = (bool)rdr["UpVote"];
            }

            if (upVote)
                upVote = false;
            else
                throw new Exception();

            conn.Close();

            SqlCommand update_cmd = new SqlCommand("UPDATE [Vote_table] SET UpVote = @newUpVote WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            update_cmd.Parameters.Add("@newUpVote", System.Data.SqlDbType.Bit).Value = upVote;
            update_cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            update_cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            conn.Open();
            update_cmd.ExecuteNonQuery();

        }
        catch
        {
            // Never fails!
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public static void removeFromDownCount(int image_id)
    {
        // Grabs the ID of the logged in user
        MembershipUser user = Membership.GetUser();
        Guid id = (Guid)user.ProviderUserKey;

        SqlConnection conn = new SqlConnection(connString);
        SqlDataReader rdr = null;

        bool downVote = false;

        try
        {
            SqlCommand cmd = new SqlCommand("SELECT DownVote FROM [Vote_table] WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                downVote = (bool)rdr["DownVote"];
            }

            if (downVote)
                downVote = false;
            else
                throw new Exception();

            conn.Close();

            SqlCommand update_cmd = new SqlCommand("UPDATE [Vote_table] SET DownVote = @newDownVote WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            update_cmd.Parameters.Add("@newDownVote", System.Data.SqlDbType.Bit).Value = downVote;
            update_cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            update_cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = id;
            conn.Open();
            update_cmd.ExecuteNonQuery();
        }
        catch
        {
            // Never fails!
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public static string checkIfVoted(int image_id, Guid user_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        SqlDataReader rdr = null;

        bool upVote = false;
        bool downVote = false;

        try
        {
            SqlCommand cmd = new SqlCommand("SELECT UpVote, DownVote FROM [Vote_table] WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = user_id;
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                upVote = (bool)rdr["UpVote"];
                downVote = (bool)rdr["DownVote"];
            }

            if (upVote)
                return "up";
            else if(downVote)
                return "down";
            else
                return "non";
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public static List<int> getCuddles(Guid user_id)
    {
        List<int> cuddles = new List<int>();
        SqlConnection conn = new SqlConnection();
        try
        {
            conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT Image_id FROM [Cuddle_table] WHERE User_id = '" + user_id.ToString() + "'", conn);
            SqlDataReader rdr = null;

            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int cuddle = (int)rdr["Image_id"];
                cuddles.Add(cuddle);
            }

            if (rdr != null)
                rdr.Close();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

        return cuddles;
    }

    public static bool alreadyCuddled(Guid user_id, int image_id)
    {
        SqlConnection conn = new SqlConnection();
        int cuddleCount = 0;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Cuddle_table] WHERE Image_id = @newImageid AND User_id = @newUserid", conn);
            cmd.Parameters.Add("@newImageid", System.Data.SqlDbType.Int).Value = image_id;
            cmd.Parameters.Add("@newUserid", System.Data.SqlDbType.UniqueIdentifier).Value = user_id;
            conn.Open();
            cuddleCount = Convert.ToInt32(cmd.ExecuteScalar());
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

        if (cuddleCount == 0)
        {
            return false;
        }
        else return true;
    }

    public static int cuddleIt(Guid user_id, int img_id)
    {
        int cuddles = getCuddleCount(img_id);
        
        if (alreadyCuddled(user_id, img_id) == false)
        {
            cuddles++;
            SqlConnection conn = new SqlConnection();
            try
            {
                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Cuddle_table] ( Image_id, User_id) values (@imageId, @userId)", conn);
                cmd.Parameters.Add("@imageId", System.Data.SqlDbType.Int).Value = img_id;
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.UniqueIdentifier).Value = user_id;
                conn.Open();
                int count = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        return cuddles;
    }

    public static int getCuddleCount(int image_id)
    {
        SqlConnection conn = new SqlConnection();
        int count = 0;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT Image_id FROM [Cuddle_table] WHERE Image_id = " + image_id.ToString(), conn);
            SqlDataReader rdr = null;

            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int img_id = (int)rdr["Image_id"];
                count++;
            }

            if (rdr != null)
                rdr.Close();
        }
        catch
        {
            // If a -1 is returned, something went wrong
            return -1;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

        return count;
    }

    public static void removeCuddle(Guid user_id, int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);
        try
        {
            // Delete the image 
            SqlCommand cmd = new SqlCommand("Delete FROM [Cuddle_table] WHERE Image_id = '" + image_id.ToString() + "' AND User_id = '" + user_id.ToString() + "'", conn);

            // Execute the sql command                
            cmd.Connection = conn;
            conn.Open();
            int count = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
}