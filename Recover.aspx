<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Recover.aspx.cs" Inherits="Recover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Recover Password</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/recover.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>Recover Password</h1>
    </div>
    <br />
    <div id="recover-content">
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server"
            OnSendingMail="PasswordRecovery1_SendingMail">
            <QuestionTemplate>
                <fieldset id="inputs"> 
                        Answer the following question to get your password.
                    <br />
                    <br />
                        <asp:Literal ID="Question" runat="server"></asp:Literal>
                   <br />
                        <asp:TextBox ID="Answer" placeholder="Your answer" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" CssClass="recoverError" ControlToValidate="Answer" ErrorMessage="Answer is required." ToolTip="Answer is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                  </fieldset>
                   <div class="recoverError">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                   </div>
                        <asp:Button ID="SubmitButton" runat="server" CssClass="submitButton" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
            </QuestionTemplate>
            <SuccessTemplate>
                 Your password has been sent to your email.     
            </SuccessTemplate>
            <UserNameTemplate>
                <fieldset id="inputs">   
                    Forgot Your Password?
                <br />
                    Enter your User Name below.
                <br />
                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" CssClass="recoverError" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                </fieldset>
                <div class="recoverError">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </div>
                <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" CssClass="submitButton" Text="Submit" ValidationGroup="PasswordRecovery1" />
            </UserNameTemplate>
        </asp:PasswordRecovery>
    </div>
</asp:Content>

