<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

    
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
        <!-- User Biography -->
        <div class="userBio">
            <!-- User's Avatar -->
            <div class ="avatar">
				<fcm:Gravatar ID="Gravatar1" runat="server" Size="200" CssClass="gravatar-img" OutputGravatarSiteLink="false" DefaultImage="http://fuddle.apphb.com/resources/gravatar.jpg" />
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
            <ul>
                <li runat="server" id="imageListItem">
                    <asp:HyperLink ID="imagesLink" runat="server">Albums</asp:HyperLink>
                </li>
                <li runat="server" id="userListItem">
                    <asp:HyperLink ID="usersLink" runat="server">Uploads</asp:HyperLink>
                </li>
            </ul>
        </div>



        <!--Recent Uploads-->
        <h2>Recent Uploads</h2>
        <div style="margin: 30px auto 0 auto; width: 200px">
            <a id="loadMore" class="uploadButton">Load More</a>
        </div>
        <div id="loading" style="text-align: center; margin: 0 auto; width: 100px">
        </div>
    
        <!-- clear floats -->
        <div style="clear: both; margin-bottom: 20px"></div>
    </div>

     <script type="text/javascript">
         $(window).load(function () {
             $("#loadMore").hide();
             return;
         });

        $("#loadMore").click(function () {
            loadMore("image");
        });
    </script>
</asp:Content>

