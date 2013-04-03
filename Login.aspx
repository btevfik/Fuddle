<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Login</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/login.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 style="text-align: center">Login</h1>
    <br />
    <div class="loginControl">
        <asp:Login ID="Login1" runat="server">
            <LayoutTemplate>
                <div id="login-content">
                    <div id="loginPanel" DefaultButton="LoginButton" runat="server">
                        <fieldset id="inputs">
                            <asp:TextBox ID="UserName" placeholder="Username" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" CssClass="loginError" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="Password" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" CssClass="loginError" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            <a href="/forgot">No memory like an elephant?</a>
                        </fieldset>
                        <fieldset id="actions">
                            <asp:Button ID="LoginButton" runat="server" CssClass="submitButton" CommandName="Login" Text="Let's Go" ValidationGroup="Login1" />
                            <asp:CheckBox ID="RememberMe" runat="server" CssClass="keep" Text="Remember me." />
                        </fieldset>
                        <div class="loginError">
                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
                    </div>
                </div>
            </LayoutTemplate>
        </asp:Login>
    </div>


</asp:Content>

