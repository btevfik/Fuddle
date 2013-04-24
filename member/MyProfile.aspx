<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="member_MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | My Profile</title>
    <style>
        .avatar img{
            max-height: 200px;
            max-width: 200px;
            padding: 5px;
            background-color: white;
            box-shadow: 0px 1px 3px #434141;
        }
        .avatar figure {
            position:relative;
            display:inline-block;
            margin:0;
        }

        .avatar figcaption {
            padding:5px;
            margin: 2px 3px 3px 2px;
            font-size:12px;
            bottom: 4px;
            left: 0;
            right: 0;
            position:absolute;
            display:none;
            background-color:rgba(0, 0, 0, 0.80);
            color:white;
            text-decoration:none;
            overflow:hidden;
        }

        .avatar img:hover ~ figcaption{
            display:block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">  
        <div class="avatar">     
            <figure>
                <asp:Image ID="AvatarImage" runat="server"/>
                <figcaption><a href="ChangeAvatar.aspx">Change Avatar</a></figcaption>
            </figure>
        </div>
        <h1>
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

