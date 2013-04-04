<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Recover.aspx.cs" Inherits="Recover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Fuddle | Recover Password</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin: 0 auto; width:400px">
        <h1>Recover Password</h1>
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" 
        onsendingmail="PasswordRecovery1_SendingMail">
    </asp:PasswordRecovery>
    </div>
</asp:Content>

