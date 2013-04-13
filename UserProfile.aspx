<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<%@ Register Assembly="FreshClickmedia.Web" Namespace="FreshClickMedia.Web.UI.WebControls" TagPrefix="fcm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
         .gravatar-img{
            padding: 5px;
            background-color: white;
            box-shadow: 0px 1px 3px #434141;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>
            <fcm:Gravatar ID="Gravatar1" runat="server" CssClass="gravatar-img" OutputGravatarSiteLink="false" DefaultImage="http://fuddle.apphb.com/resources/gravatar.jpg" /> 
            <asp:Label ID="userLabel" runat="server" Text=""></asp:Label>
        </h1>
    </div>
</asp:Content>

