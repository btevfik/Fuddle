﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

    
<%@ Register Assembly="FreshClickmedia.Web" Namespace="FreshClickMedia.Web.UI.WebControls" TagPrefix="fcm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
	<link href="/stylesheets/userprofile.css" rel="stylesheet" type="text/css" /> 
    <style>
         .gravatar-img{
            padding: 5px;
            background-color: white;
            box-shadow: 0px 1px 3px #434141;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class = "centerPage">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="scripts/userprofile.js" />
            </Scripts>
        </asp:ScriptManager>
        <script type="text/javascript">
            var pageNameElements = {
                textbox: '<%= aboutmeTextBox.ClientID %>',
                label: '<%= aboutmeLabel.ClientID %>'
        };
        </script>
        <!-- User Biography -->
        <div class="userBio">
            <!-- User's Avatar -->
            <div class ="avatar">
                <asp:Image ID="Image1" runat="server" Height="200px" Width="200px" Style="display: none;" />
				<fcm:Gravatar ID="Gravatar1" runat="server" Size="200" CssClass="gravatar-img" OutputGravatarSiteLink="false" DefaultImage="http://fuddle.apphb.com/resources/gravatar.jpg" />
            </div>
            <!-- Username -->
            <div class ="username">
                <asp:Label ID="userLabel" runat="server" Text=""></asp:Label>
            </div>
            <!-- User's About Me -->
            <div class ="aboutme">
                <asp:TextBox ID="aboutmeTextBox" runat="server" Style="display: none;"></asp:TextBox>
                <asp:Label ID="aboutmeLabel" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

