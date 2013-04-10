<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="member_MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Fuddle | My Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <h1>
            <asp:Label ID="userLabel" runat="server" Text=""></asp:Label>
        </h1>
            <a href="/member/Settings.aspx" runat="server">Settings</a>
            <br />
            email: <asp:Label ID="userEmail" runat="server" Text=""></asp:Label>
            <br />
            public profile: <asp:HyperLink ID="publicLink" runat="server"></asp:HyperLink>
    </div>
</asp:Content>

