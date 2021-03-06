﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if user is logged in redirect to default page.
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    //resend activation link
    protected void ResetValidation_Click(object sender, EventArgs e)
    {
        Activation.sendLink(Login1.UserName);
        Login1.FindControl("ResetValidation").Visible = false;
    }

    //check error if username is registered but not activated display resend link
    protected void Login1_LoginError(object sender, EventArgs e)
    {
        MembershipUser u = Membership.GetUser(Login1.UserName);
        if (u == null)
        {
            return;
        }
        if (u.IsApproved == false)
        {
            Login1.FindControl("ResetValidation").Visible = true;
        }
    }
}