<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="member_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Settings</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/settings.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="left">
        Change Your Password
        <asp:ChangePassword ID="ChangePassword1" runat="server" ChangePasswordFailureText="Password incorrect or New Password invalid. New Password length minimum: {0}.">
            <ChangePasswordTemplate>
                <div class="content">
                    <div class="inputs">
                        Password:
                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="error" ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                        New Password:
                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="error" ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                        Confirm New Password:
                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="error" ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                    </div>
                    <asp:CompareValidator CssClass="error" ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Passwords don't match." ValidationGroup="ChangePassword1"></asp:CompareValidator>
                    <div class="error">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                    <asp:Button ID="ChangePasswordPushButton" CssClass="submitButton" runat="server" CommandName="ChangePassword" Text="Change Password" ValidationGroup="ChangePassword1" />
                </div>
            </ChangePasswordTemplate>
            <SuccessTemplate>
                <div id="content">
                    Your password has been changed!
                </div>
            </SuccessTemplate>
        </asp:ChangePassword>
    </div>

    <br />

    <div id="middle">
        Update Security Q & A
        <br />
         <div class="content">
             <div class="inputs">
                 New Question:
                <asp:TextBox ID="NewQuestion" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="QuestionRequiredValidator" runat="server" ControlToValidate="NewQuestion"
                     CssClass="error" Display="Static" ErrorMessage="*" />
                 New Answer:
                <asp:TextBox ID="NewAnswer" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="AnswerRequiredValidator" runat="server" ControlToValidate="NewAnswer"
                     CssClass="error" Display="Static" ErrorMessage="*" />
                 Password:
                 <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="OldPasswordRequiredValidator" runat="server" ControlToValidate="Password"
                     CssClass="error" Display="Static" ErrorMessage="*" />
             </div>
             <asp:Button CssClass="submitButton" ID="ChangeQA" OnClick="ChangeQA_Click" runat="server" Text="Change Q&A" />
             <asp:Label CssClass="error" ID="Msg" runat="server"></asp:Label>
         </div>


    </div>

    <div id="right">
        Update Email
        <br />
        <div class="content">
            <div class="inputs">
         New Email
    <asp:TextBox ID="NewEmail" runat="server"></asp:TextBox>
                </div>
        <asp:Button CssClass="submitButton" ID="ChangeEmail" ValidationGroup="EmailChange" runat="server" OnClick="ChangeEmail_Click" Text="Change Email" />
        <br />
        <asp:RegularExpressionValidator CssClass="error" ID="EmailValidator" runat="server" ControlToValidate="NewEmail" ValidationGroup="EmailChange" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="E-mail address must be in a valid format."></asp:RegularExpressionValidator>
        <asp:Label CssClass="error" ID="Msg1" runat="server"></asp:Label>
            </div>
    </div>

    <!-- clear floats -->
    <div style="clear: both; margin-bottom: 20px"></div>
</asp:Content>

