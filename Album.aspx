<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/album.css" />
    <!-- Scripts -->
    <script src="/scripts/jquery.screwdefaultbuttonsV2.js"></script>
    <script>
        $(document).ready(function () {
            $('.remove-box input:checkbox').screwDefaultButtons({
                image: 'url("/resources/checkboxSmall.jpg")',
                width: 43,
                height: 43
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- FACEBOOK JS SDK -->
    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
    </script>
    <div id="container">
        <div style="margin-left: 50px;">
            <!-- FB LIKE / SHARE BUTTON -->
            <fb:like send="true" layout="button_count" width="0" style="float: right; margin-right: 30px; margin-top: 30px;" show_faces="true"></fb:like>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="AlbumTitle" CssClass="album-title" runat="server" Text=""></asp:Label>
                    <asp:TextBox ID="NewAlbumTitle" runat="server" Visible="false"></asp:TextBox>
                    <asp:Button ID="UpdateTitleButton" CssClass="submitButton" OnClick="UpdateTitleButton_Click" Visible="false" runat="server" Text="Update Title" />
                    <asp:Button ID="SaveTitleButton" CssClass="submitButton" OnClick="SaveTitleButton_Click" Visible="false" runat="server" Text="Save" ValidationGroup="AlbumTitle" />
                    <asp:Button ID="DeleteAlbumButton" CssClass="submitButton" OnClick="DeleteAlbumButton_Click" Visible="false" runat="server" Text="Delete Album" OnClientClick="if (!confirm('Are you sure you want to DELETE this album? (The images will NOT be deleted)')) return false;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" ControlToValidate="NewAlbumTitle" ValidationGroup="AlbumTitle"><span style="color:#d14545;font-size:15px;">Title can't be empty.</span></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator runat="server" ID="titleLenghtValidator"
                        ControlToValidate="NewAlbumTitle"
                        ValidationExpression="^[\s\S]{0,40}$"
                        ErrorMessage=""
                        Display="Dynamic" ValidationGroup="AlbumTitle"><span style="font-size:14px;color:#d14545;">Please enter a maximum of 40 characters.</span></asp:RegularExpressionValidator>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="album-user">
                by
                <asp:HyperLink ID="AlbumUser" runat="server"></asp:HyperLink>
            </div>
        </div>
        <asp:Table ID="ImageTable" ClientIDMode="Static" runat="server"></asp:Table>
        <asp:Button ID="deleteSelectedButton" runat="server" CssClass="submitButton removeButton" Text="Remove Selected Images" Visible="false" OnClick="DeleteSelectedButton_Click" OnClientClick="if (!confirm('Are you sure you want to REMOVE selected images from the album? (The images will NOT be deleted)')) return false;" />
        </div>
</asp:Content>

