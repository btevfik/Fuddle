<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" MaintainScrollPositionOnPostback="True" %>

    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/stylesheets/myprofile.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class = "centerPage"> 
        <!-- User Biography -->
        <div class="userBio">
            <!-- User's Avatar -->
            <div class ="avatar">
                <figure><asp:Image ID="AvatarImage" runat="server"/></figure>
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
        <div class ="imgtab">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="loadrows"/>
                    <asp:AsyncPostBackTrigger ControlID="cuddleLink" />
                    <asp:AsyncPostBackTrigger ControlID="albumLink" />
                </Triggers>
                <ContentTemplate>
                    <div id="albums-image-tabs">
                        <ul>
                            <li runat="server" id="albumListItem">
                                <asp:LinkButton ID="albumLink" runat="server" OnClick="albumLink_Click">Albums</asp:LinkButton>
                            </li>
                            <li runat="server" id="cuddleListItem">
                                <asp:LinkButton ID="cuddleLink" runat="server" OnClick="cuddleLink_Click">Cuddles</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                    <asp:Table ID="Table1" runat="server"></asp:Table>
                    <asp:Button CssClass="uploadButton" ID="loadrows" runat="server" Text="Load More" OnClick="loadrows_Click"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <!--Recent Uploads-->
        <div class="uploads">
            <h2>Uploads</h2>
            <asp:UpdatePanel ID="RecentUpload" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="loaduploads"/>
                </Triggers>
                <ContentTemplate>
                    <asp:Button CssClass="uploadButton" ID="loaduploads" runat="server" Text="Load More" OnClick="loaduploads_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>


