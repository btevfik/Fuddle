<%@ WebHandler Language="C#" Class="GetAvatar" %>
using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;

public class GetAvatar : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        SqlDataReader rdr = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        bool use_gravatar = true;

        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
            conn = new SqlConnection(connStr);

            String username = context.Request.QueryString["user"];
            String size = context.Request.QueryString["size"];
            MembershipUser u = Membership.GetUser(username);
            Guid id = (Guid)u.ProviderUserKey;
            cmd = new SqlCommand("SELECT Use_gravatar FROM [User_info] WHERE User_id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier, 16).Value = id;
            conn.Open();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
              use_gravatar = (bool)rdr["Use_gravatar"];
            }

            //if we want to use gravatar
            if (use_gravatar == true)
            {
                //Compute the hashstring 
                string hash = HashEmailForGravatar(u.Email);
                //set default size if not specified
                if (size == null || size == "")
                {
                    size = "80";
                }
                //  Assemble the url
                string defaultImg = HttpContext.Current.Server.UrlEncode("http://fuddle.apphb.com/resources/gravatar.jpg");
                string URL = string.Format("http://www.gravatar.com/avatar/{0}?r=pg&s={1}&d={2}",hash,size,defaultImg);
                // Make request to gravatar
                makeAvatarRequest(URL);
            }

            //if we don't want to use gravatar
            else if (use_gravatar == false)
            {
                cmd = new SqlCommand("SELECT User_avatar FROM [User_info] WHERE User_id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier, 16).Value = id;
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    context.Response.ContentType = "image/jpg";
                    context.Response.BinaryWrite((byte[])rdr["User_avatar"]);
                }

            }
            if (rdr != null)
                rdr.Close();
        }
        catch (Exception ex)
        {
            context.Response.Write("Error retrieving user avatar!");
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }      
    }
 
    public bool IsReusable 
    {
        get {
            return false;
        }
    }

    // Hashes an email with MD5.  Suitable for use with Gravatar profile
    public static string HashEmailForGravatar(string email)
    {    
    // Create a new instance of the MD5CryptoServiceProvider object.      
    MD5 md5Hasher = MD5.Create();      
    // Convert the input string to a byte array and compute the hash.      
    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));      
    // Create a new Stringbuilder to collect the bytes    
    // and create a string.      
    StringBuilder sBuilder = new StringBuilder();      
    // Loop through each byte of the hashed data      
    // and format each one as a hexadecimal string.      
    for(int i = 0; i < data.Length; i++)    
    {        
        sBuilder.Append(data[i].ToString("x2"));    
    }      
    return sBuilder.ToString();  
    // Return the hexadecimal string. 
    }

    // Attempt a request for avatar
    private bool makeAvatarRequest(string URL)
    {
        try
        {
            WebRequest request = WebRequest.Create(URL);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    displayImage(responseStream);
                    return true;
                }
            }
        }
        catch (WebException ex)
        {
            return false;
        }
    }

    // Display the image from stream
    private void displayImage(Stream stream)
    {
        HttpContext.Current.Response.ContentType = "image/png";
        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
        MemoryStream temp = new MemoryStream();
        img.Save(temp, ImageFormat.Png);
        byte[] buffer = temp.GetBuffer();
        HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);

        img.Dispose();
        temp.Dispose();
    }

}