﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

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
            SqlCommand cmd = new SqlCommand("SELECT UpVote FROM [Vote_table] WHERE Image_id = " + image_id.ToString(), conn);
            SqlDataReader rdr = null;            

            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                upCount = (int)rdr["UpVote"];
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

        return upCount;
    }

    public static int getDownCount(int image_id)
    {
        SqlConnection conn = new SqlConnection();
        int downCount = 0;

        try
        {
            conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SELECT DownVote FROM [Vote_table] WHERE Image_id = " + image_id.ToString(), conn);
            SqlDataReader rdr = null;

            conn.Open();           
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                downCount = (int)rdr["DownVote"];
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

        return downCount;
    }

    public static int addToUpCount(int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);

        // Get the current upVote count
        int upCount = getUpCount(image_id);

        // Increment it by one
        // For later: check User_id to see if this user has already cast their vote
        upCount++;

        try
        {
            SqlCommand cmd = new SqlCommand("UPDATE [Vote_table] SET UpVote = @newUpVote WHERE Image_id = " + image_id.ToString(), conn);
            cmd.Parameters.Add("@newUpVote", System.Data.SqlDbType.Int).Value = upCount;

            conn.Open();
            cmd.ExecuteNonQuery();
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

        return upCount;
    }

    public static int addToDownCount(int image_id)
    {
        SqlConnection conn = new SqlConnection(connString);

        // Get the current downVote count
        int downCount = getDownCount(image_id);

        // Decrement it by one
        // For later: check User_id to see if this user has already cast their vote
        downCount--;

        try
        {
            SqlCommand cmd = new SqlCommand("UPDATE [Vote_table] SET DownVote = @newDownVote WHERE Image_id = " + image_id.ToString(), conn);
            cmd.Parameters.Add("@newDownVote", System.Data.SqlDbType.Int).Value = downCount;

            conn.Open();
            cmd.ExecuteNonQuery();
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

        return downCount;
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

    public static int cuddleIt(Guid user_id, int img_id)
    {
        int cuddles = getCuddleCount(img_id);
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