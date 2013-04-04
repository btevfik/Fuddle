using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class Recover : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //send email for password recovery
    protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
    {
        MailMessage mm = new MailMessage();

        mm.From = e.Message.From;

        mm.Subject = e.Message.Subject.ToString();

        mm.To.Add(e.Message.To[0]);

        mm.Body = e.Message.Body;
        SmtpClient smtp = new SmtpClient();
        smtp.EnableSsl = true;

        smtp.Send(mm);
        e.Cancel = true;
    }
}