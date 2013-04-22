using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public class SSImage
{
    public int id; //id of img
    public int width; //width of the SSImage
    public int height; //height of the SSImage
    public string title; //title of the img
}

public class User
{
    public string name; //username
}

/// <summary>
/// SearchService is used to retrieve images, albums and users from the database with a given query
/// </summary>
[WebService(Namespace = "http://fuddle.apphb.com")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

[System.Web.Script.Services.ScriptService]
public class SearchService : System.Web.Services.WebService {

    //images list
    List<SSImage> Images = new List<SSImage>();
    //users list
    List<User> Users = new List<User>();

    //sql connection
    SqlConnection conn = new SqlConnection();
    //sql command
    SqlCommand cmd = new SqlCommand();
    //data reader
    SqlDataReader rdr = null;

    public SearchService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    ///return images with the given search query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
   [WebMethod]
    public List<SSImage> GetImages(string query)
    {
        //clear images list
        Images.Clear();

        if (query == "")
        {
            return null;
        } 

        //search images in database
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);

            cmd = new SqlCommand("SELECT Image_id,Image_thumbHeight,Image_thumbWidth,Image_title FROM [Image_table] WHERE Image_desc like '%" + query + "%' OR Image_title like '%" + query + "%' OR Image_filename like '%" + query + "%'", conn);
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
               //get the id
               int id = ((int)rdr["Image_id"]);
                //get the width of thumb
               int width = ((int)rdr["Image_thumbWidth"]);
                //get the height of thumb
               int height = ((int)rdr["Image_thumbHeight"]);
               //get the title 
               string title = ((string)rdr["Image_title"]);
               //add to images list
               SSImage newImage = new SSImage { id = id, width = width, height = height, title=title };
               Images.Add(newImage);
                
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
        return Images;
    }

   /// <summary>
   ///return users with the given search query
   /// </summary>
   /// <param name="query"></param>
   /// <returns></returns>
   [WebMethod]
   public List<User> GetUsers(string query)
   {
       //clear user list
       Users.Clear();

       if (query == "")
       {
           return null;
       }

       //search users in database
       try
       {
           string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
           conn = new SqlConnection(connStr);

           cmd = new SqlCommand("SELECT UserName FROM [aspnet_Users] WHERE UserName like '%" + query + "%'", conn);
           conn.Open();
           rdr = cmd.ExecuteReader();
           while (rdr.Read())
           {
               //get the name
               string name = ((string)rdr["UserName"]);
               //add to users list 
               User newUser = new User{name = name};
               Users.Add(newUser);

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
       return Users;
   }

    public List<SSImage> GetUserUploads(string query)
    {
        //clear images list
        Images.Clear();

        if (query == "")
        {
            return null;
        }

        //search images in database
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);
            MembershipUser u = Membership.GetUser(query);
            Guid user_id = (Guid)u.ProviderUserKey;
            cmd = new SqlCommand("SELECT Image_id,Image_width,Image_height FROM [Image_table] WHERE User_Id = @user_id ORDER BY Image_id DESC", conn);
            cmd.Parameters.Add("@user_id", SqlDbType.UniqueIdentifier).Value = user_id;
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //get the id
                int id = ((int)rdr["Image_id"]);
                //get the height
                int width = ((int)rdr["Image_width"]);
                //get the width
                int height = ((int)rdr["Image_height"]);
                //add to images list 
                SSImage newImage = new SSImage { id = id, width = width, height = height };
                Images.Add(newImage);

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
        return Images;
    }


}
