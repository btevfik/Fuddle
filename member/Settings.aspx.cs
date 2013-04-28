using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class member_Account : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangeQA_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.GetUser();
            // change the password question and answer
            string newQuestion = NewQuestion.Text;
            string newAnswer = NewAnswer.Text;
            string password = Password.Text;
            Boolean result = user.ChangePasswordQuestionAndAnswer(password, newQuestion, newAnswer);

            if (result)
                Msg.Text = "Password Question and Answer changed.";
            else
                Msg.Text = "Password Question and Answer change failed.";
        }
        catch (Exception err)
        {
            Msg.Text = "Change failed. Please re-enter your values and try again.";
        }
    }
    protected void ChangeEmail_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.GetUser();
            // change the email
            string newEmail = NewEmail.Text;
            user.Email = newEmail;
            //unactivate account
            user.IsApproved = false;
            Membership.UpdateUser(user);
            //resend Activation link
            Activation.sendLink(user.UserName);
            Msg1.Text = "Email updated. Activation link sent to your email.";
        }
        catch (Exception err)
        {
            Msg1.Text = "Change failed. Please re-enter your values and try again.";
        }
    }

    protected void Close_Account(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.GetUser();
            Boolean result = Membership.ValidateUser(user.UserName, closePassword.Text);

            if (result)
            {
                //delete all comments by this user
                FuddleUser fUser = new FuddleUser(user.UserName);
                fUser.deleteAllComments();
                //delete user
                Membership.DeleteUser(user.UserName);
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
            else
                Msg2.Text = "Wrong password. Try again.";
        }
        catch (Exception err)
        {
            Msg2.Text = "Account deletion failed.";
        }  
    }
}