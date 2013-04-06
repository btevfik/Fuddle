using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class admin_ManageUsers : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        List<string> roles = new List<string>(System.Web.Security.Roles.GetAllRoles());

        foreach (var role in roles)
        {
            CheckBox box = new CheckBox();

            box.ID = role.ToString();
            box.Text = role.ToString();

            roleDiv.Controls.Add(box);

            LiteralControl control = new LiteralControl("<br />");

            roleDiv.Controls.Add(control);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void UserGrid_IndexChanged(object sender, EventArgs e)
    {
        UserNameText.Text = UserGrid.SelectedRow.Cells[1].Text;
        UserEmailTxt.Text = UserGrid.SelectedRow.Cells[2].Text;

        MembershipUser user = Membership.GetUser(UserNameText.Text);

        if (user != null)
        {

            ActiveBox.Checked = user.IsApproved;

            List<string> roles = new List<string>(System.Web.Security.Roles.GetAllRoles());
            List<string> rolesOfUser = new List<string>(System.Web.Security.Roles.GetRolesForUser(user.UserName));

            foreach (string role in roles)
            {
                CheckBox box = (CheckBox)roleDiv.FindControl(role);
                if (rolesOfUser.Contains(role))
                {
                    box.Checked = true;
                }
            }

            UserGrid_ModalPopupExtender.Show();
        }

    }

    protected void ManageUserSave(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(UserGrid.SelectedRow.Cells[1].Text);

        if (user != null)
        {
            user.Email = UserEmailTxt.Text;
            user.IsApproved = ActiveBox.Checked;
            Membership.UpdateUser(user);

            foreach (Control control in roleDiv.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox box = (CheckBox)control;
                    List<string> roleUsers = new List<string>(System.Web.Security.Roles.GetUsersInRole(box.ID));

                    if (box.Checked)
                    {
                        if (!roleUsers.Contains(user.UserName))
                        {
                            System.Web.Security.Roles.AddUserToRole(user.UserName, box.ID);
                        }
                    }
                    else
                    {
                        if (roleUsers.Contains(user.UserName))
                        {
                            System.Web.Security.Roles.RemoveUserFromRole(user.UserName, box.ID);
                        }
                    }
                }

            }

            UserGrid.DataBind();
        }
    }

    protected void DeleteUser(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(UserNameText.Text))
        {
            Membership.DeleteUser(UserNameText.Text);
            UserGrid.DataBind();
        }

    }

    protected void AskMessage(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
}