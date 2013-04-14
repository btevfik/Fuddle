using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Activation class that is used to send activation links
/// </summary>
public class Activation
{
	public Activation()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void sendLink(string username)
    {
        MembershipUser user = Membership.GetUser(username);
        //return if null or already approved
        if (user == null || user.IsApproved == true)
        {
            return;
        }
        //get user id
        Guid userId = (Guid)user.ProviderUserKey;
        //contruct email
        MailMessage mm = new MailMessage();
        mm.Subject = "Verify your account at Fuddle";
        mm.IsBodyHtml = true;
        string url = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
        string link = url + "/Activate.aspx?ID=" + userId.ToString();
        mm.Body = "Thanks for registering with Fuddle!<br /><br />Your activation link : <a href='" + link + "'>" + link + "</a>";
        mm.To.Add(user.Email);
        SmtpClient smtp = new SmtpClient();
        smtp.EnableSsl = true;
        //send email
        smtp.Send(mm);
    }

    public static void addToUserInfoTable(string username)
    {
        MembershipUser user = Membership.GetUser(username);
        //return if null
        if (user == null)
        {
            return;
        }
        //Get the UserId of the just-added user     
        Guid newUserId = (Guid)user.ProviderUserKey;
        //Insert a new record into user_info      
        string connectionString = ConfigurationManager.ConnectionStrings["fuddleConnectionString"].ConnectionString;
        string insertSql = "INSERT INTO User_info(User_id) VALUES(@UserId)";
        using (SqlConnection myConnection = new SqlConnection(connectionString))
        {
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(insertSql, myConnection);
            myCommand.Parameters.AddWithValue("@UserId", newUserId);
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}