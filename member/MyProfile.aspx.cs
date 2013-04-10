using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

//THIS USER PROFILE PAGE IS PRIVATE
//ONLY LOGGED IN USERS CAN VIEW IT
//WHICH SHOWS THE CURRENT LOGGED IN USER'S PROFILE
//HERE USERS SHOULD BE ABLE TO EDIT THEIR PROFILE
//PUBLIC PROFILE PAGE IS AT ~/UserProfile.aspx
public partial class member_MyProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MembershipUser u = Membership.GetUser();
        //display username
        userLabel.Text = u.UserName;
        //display email
        userEmail.Text = u.Email;
        //display public profile link
        string url = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host + (HttpContext.Current.Request.Url.IsDefaultPort ? "" : ":" + HttpContext.Current.Request.Url.Port);
        publicLink.NavigateUrl = url+ "/user/" + u.UserName;
        publicLink.Text = url+ "/user/" + u.UserName;
    }
}