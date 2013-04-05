using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

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

    public static void sendLink(MembershipUser user)
    {
        //return if null or already approved
        if (user == null || user.IsApproved==true)
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
}