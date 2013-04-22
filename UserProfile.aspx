<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

    
<%@ Register Assembly="FreshClickmedia.Web" Namespace="FreshClickMedia.Web.UI.WebControls" TagPrefix="fcm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/stylesheets/userprofile.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class = "centerPage"> 
        <!-- User Biography -->
        <div class="userBio">
            <!-- User's Avatar -->
            <div class ="avatar">
                <asp:Image ID="AvatarImage" runat="server"/>
            </div>
            <!-- Username -->
            <div class ="username">
                <h1>
                    <asp:Label ID="userLabel" runat="server" Text=""></asp:Label>
                </h1>
            </div>
            <!-- User's About Me -->
            <div class ="aboutme">
                <asp:Label ID="aboutmeLabel" runat="server">Our goal at Fuddle is to create one website where visitors are greeted with an abundance of popular images from across the globe submitted by fellow users who share their interests. </asp:Label>
            </div>
        </div>

        <!-- Albums / Images Tab -->
        <div id="albums-image-tabs">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <ul>
                        <li runat="server" id="imageListItem">
                            <asp:LinkButton ID="albumLink" runat="server" OnClick="albumLink_Click">Albums</asp:LinkButton>
                        </li>
                        <li runat="server" id="userListItem">
                            <asp:LinkButton ID="cuddleLink" runat="server" OnClick="cuddleLink_Click">Cuddles</asp:LinkButton>
                        </li>

                        <asp:Table ID="Table1" runat="server"></asp:Table>
                    </ul>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>



        <!--Recent Uploads-->
        <div class="uploads">
            <h2>Uploads</h2>
            <asp:UpdatePanel ID="RecentUpload" runat="server">
                <ContentTemplate>
                    <asp:Button CssClass="uploadButton" ID="loaduploads" runat="server" Text="Load More" OnClick="loaduploads_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>


