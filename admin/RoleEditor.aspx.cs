using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_RoleEditor : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        List<string> roles = new List<string>(System.Web.Security.Roles.GetAllRoles());

        RadioButtonList editButtonList = new RadioButtonList();
        editButtonList.ID = "editButtonList";

        foreach (var role in roles)
        {
            editButtonList.Items.Add(role);
        }

        editButtonList.SelectedIndex = 0;
        editButtonList.AutoPostBack = true;


        RoleBox.Controls.Add(editButtonList);

        List<string> usersInRole = new List<string>(Roles.GetUsersInRole(editButtonList.SelectedItem.Text));
        RoleUsersList.DataSource = usersInRole;
        RoleUsersList.DataBind();

        DetermineRemaingUsers(editButtonList);
    }

    private void DetermineRemaingUsers(RadioButtonList editButtonList)
    {
        MembershipUserCollection col = Membership.GetAllUsers();
        List<string> usersInRole = new List<string>(Roles.GetUsersInRole(editButtonList.SelectedItem.Text));
        List<string> remainingUsers = new List<string>();

        foreach (MembershipUser user in col)
        {
            if (!usersInRole.Contains(user.UserName))
            {
                remainingUsers.Add(user.UserName);
            }
        }

        AllUsersList.DataSource = remainingUsers;
        AllUsersList.DataBind();
    }

    protected void EditButtonListSelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList editButtonList = (RadioButtonList)RoleBox.FindControl("editButtonList");
        RoleUsersList.DataSource = Roles.GetUsersInRole(editButtonList.SelectedItem.Text);
        RoleUsersList.DataBind();

        DetermineRemaingUsers(editButtonList);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RadioButtonList editButtonList = (RadioButtonList)RoleBox.FindControl("editButtonList");
        editButtonList.SelectedIndexChanged += EditButtonListSelectedIndexChanged;
    }

    protected void RemoveButton_OnClick(object sender, EventArgs e)
    {
        RemoveButton_ModalPopupExtender.Show();
    }

    protected void AddButton_OnClick(object sender, EventArgs e)
    {
        AddButton_ModalPopupExtender.Show();
    }

    protected void AddRole_OnClick(object sender, EventArgs e)
    {
        if (RoleNameText.Text != "")
        {
            Roles.CreateRole(RoleNameText.Text);
            Response.Redirect("/RoleEditor.aspx");
        }
    }

    protected void RemoveRole_OnClick(object sender, EventArgs e)
    {
        RadioButtonList list = (RadioButtonList)RoleBox.FindControl("editButtonList");

        ListItem item = list.SelectedItem;

        if (Roles.GetUsersInRole(item.Text).Length != 0)
        {
            Roles.RemoveUsersFromRole(Roles.GetUsersInRole(item.Text), item.Text);
        }

        Roles.DeleteRole(item.Text);

        Response.Redirect("/RoleEditor.aspx");

    }


    protected void MoveToRole_OnClick(object sender, EventArgs e)
    {
        RadioButtonList editButtonList = (RadioButtonList)RoleBox.FindControl("editButtonList");

        foreach (ListItem listItem in AllUsersList.Items)
        {
            if (listItem.Selected)
            {
                Roles.AddUserToRole(listItem.Text, editButtonList.SelectedItem.Text);
            }
        }


        RoleUsersList.DataSource = Roles.GetUsersInRole(editButtonList.SelectedItem.Text);
        RoleUsersList.DataBind();

        DetermineRemaingUsers(editButtonList);
    }

    protected void DeleteFromRole_OnClick(object sender, EventArgs e)
    {
        RadioButtonList editButtonList = (RadioButtonList)RoleBox.FindControl("editButtonList");
        foreach (ListItem listItem in RoleUsersList.Items)
        {
            if (listItem.Selected)
            {
                Roles.RemoveUserFromRole(listItem.Text, editButtonList.SelectedItem.Text);
            }
        }

        RoleUsersList.DataSource = Roles.GetUsersInRole(editButtonList.SelectedItem.Text);
        RoleUsersList.DataBind();

        DetermineRemaingUsers(editButtonList);
    }
}