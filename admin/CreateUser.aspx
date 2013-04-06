<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site.Master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="admin_CreateUser" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 62%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table style="border-style: none; border-color: inherit; border-width: 10px; width:100%;">
        <tr style="width:100%;">
            <td class="style1">
                <p><b>Create a New User</b></p>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <p><b><u>User Info</u></b></p>
                <span>
                    <asp:Label runat="server" Text="User Name:" />
                    <asp:TextBox runat="server" id="UserNameText" ValidationGroup="UserGroup" style="margin-left: 80px; width:250px;" />
                    <asp:RequiredFieldValidator ID="UserNameReq" ControlToValidate="UserNameText" ValidationGroup="UserGroup" Display="Dynamic" runat="server" style="margin: 0 auto 0 10px; color:Red" ErrorMessage="Field is Required." />
                </span>
                <br />
                <br />
                <span>
                    <asp:Label ID="Label1" runat="server" Text="Password:" />
                    <asp:TextBox runat="server" id="PassText" 
                    style="margin-left: 90px; width:250px;" TextMode="Password" ValidationGroup="UserGroup" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="UserGroup" ControlToValidate="PassText" runat="server" Display="Dynamic" style="margin: 0 auto 0 10px; color:Red" ErrorMessage="Field is Required." />
                    <asp:CompareValidator ID="ComparePasswordsValidator" ValidationGroup="UserGroup" runat="server" ErrorMessage="Passwords must match!" Display="Dynamic" style="color:red;" ControlToValidate="PassText" ControlToCompare="PassConfirmText" />
                </span>
                <br />
                <br />
                <span>
                    <asp:Label ID="Label2" runat="server" Text="Confirm Password:" />
                    <asp:TextBox runat="server" id="PassConfirmText" 
                    style="margin-left: 42px; " TextMode="Password" ValidationGroup="UserGroup" Width="250px" />
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="UserGroup" ControlToValidate="PassConfirmText" Display="Dynamic" runat="server" style="margin: 0 auto 0 10px; color:Red" ErrorMessage="Field is Required." />
                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="UserGroup" runat="server" ErrorMessage="Passwords must match!" Display="Dynamic" style="color:red;" ControlToValidate="PassConfirmText" ControlToCompare="PassText" />
                </span>
                <br />
                <br />
                <span>
                    <asp:Label ID="Label3" runat="server" Text="E-mail:" />
                    <asp:TextBox runat="server" ValidationGroup="UserGroup" id="EmailText" style="margin-left: 108px; width:250px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="UserGroup" ControlToValidate="EmailText" Display="Dynamic" runat="server" style="margin: 0 auto 0 10px; color:Red" ErrorMessage="Field is Required." />
                    <asp:RegularExpressionValidator id="RegularExpressionValidator1" ValidationGroup="UserGroup" runat="server" ControlToValidate="EmailText" style="color:red;margin: 0 auto 0 10px;" Display="Dynamic" ErrorMessage="Email Not Valid" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"/>
                </span>
                <br />
                <br />
                <span>
                    <asp:Label ID="Label4" runat="server" Text="Security Question:" />
                    <asp:TextBox runat="server" ValidationGroup="UserGroup" id="QuestionText" style="margin-left: 44px; width:250px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="QuestionText" ValidationGroup="UserGroup" Display="Dynamic" runat="server" style="margin: 0 auto 0 10px; color:Red" ErrorMessage="Field is Required." />
                </span>
                <br />
                <br />
                <span>
                    <asp:Label ID="Label5" runat="server" Text="Answer:" />
                    <asp:TextBox runat="server" ValidationGroup="UserGroup" id="AnswerText" style="margin-left: 101px; width:250px;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="AnswerText" ValidationGroup="UserGroup" Display="Dynamic" runat="server" style="margin: 0 auto 0 10px; color:Red" ErrorMessage="Field is Required." />
                </span>
                <br />
                <br />
                <asp:CheckBox ID="ActiveUser" runat="server" Text="Active User" style="margin:0 auto 0 145px" Checked="true" />
            </td>
            <td style="width:50%; vertical-align:top">
                <p><b><u>Assign to Roles</u></b></p>
                <div id="CheckBoxRoles" runat="server">
                </div>
            </td>
        </tr>
    </table>
    <br />
    <div id="buttonBank" style="width:152px; margin: 0 auto 0 232px;">
         <asp:Button runat="server" ID="CreateButton" Text="Create" ValidationGroup="UserGroup" OnClick="CreateButton_OnClick" /> 
         <asp:ModalPopupExtender ID="CreateButton_ModalPopupExtender" runat="server" 
             TargetControlID="hiddenButton" PopupControlID="MyPopUp" PopupDragHandleControlID="MyPopUp">
         </asp:ModalPopupExtender>
         <asp:Button runat="server" ID="CancelButton" Text="Cancel" OnClick="CancelButton_OnClick" style="margin-left: 7px" />
    </div>

    <div id="MyPopUp" runat="server" 
        style="width:300px; height:100px; background-color: White; border: 2px solid black; display:none;">
        <div style=" text-align:center;">
            <p><b><u>Create User Task</u></b></p>
        </div>
        <div id="PopUpText" runat="server" style="text-align:center;">
        </div>
        <div style="width:50px; margin:0 auto 0 auto;">
            <asp:Button ID="PopUpOkButton" runat="server" Text="Ok" Width="50px"/>
        </div>
    </div>
    <asp:Button ID="hiddenButton" runat="server" style="display:none;"/>
</asp:Content>
