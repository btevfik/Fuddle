<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="member_MyProfile" %>

<%@ Register Assembly="FreshClickmedia.Web" Namespace="FreshClickMedia.Web.UI.WebControls" TagPrefix="fcm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | My Profile</title>
    <style>
        .gravatar-img img{
            padding: 5px;
            background-color: white;
            box-shadow: 0px 1px 3px #434141;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>
            <fcm:Gravatar ID="Gravatar1" runat="server" CssClass="gravatar-img" OutputGravatarSiteLink="true" DefaultImage="http://fuddle.apphb.com/resources/gravatar.jpg" />
            <asp:Label ID="userLabel" runat="server" Text=""></asp:Label>
        </h1>

        <a href="/member/Settings.aspx" runat="server">Settings</a>
        <br />
        email:
        <asp:Label ID="userEmail" runat="server" Text=""></asp:Label>
        <br />
        public profile:
        <asp:HyperLink ID="publicLink" runat="server"></asp:HyperLink>
    </div>
</asp:Content>

