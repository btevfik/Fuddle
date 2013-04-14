using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if user is logged in redirect to default page.
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        //Send email to user for verifying account        
        CreateUserWizard cuw = (CreateUserWizard)sender;
        MembershipUser user = Membership.GetUser(cuw.UserName);
        //set to unapproved user
        user.IsApproved = false;
        Membership.UpdateUser(user);
        //send link
        Activation.sendLink(user.UserName);
        //additionally add userID to user_info table
        Activation.addToUserInfoTable(user.UserName);
    }
}