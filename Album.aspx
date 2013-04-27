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
    <div id="container">
        <div style="margin-left: 50px;">
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:Label ID="AlbumTitle" CssClass="album-title" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="NewAlbumTitle" runat="server" Visible="false"></asp:TextBox>
                <asp:Button ID="UpdateTitleButton" CssClass="submitButton" OnClick="UpdateTitleButton_Click" Visible="false" runat="server" Text="Update Title" />
                <asp:Button ID="SaveTitleButton" CssClass="submitButton" OnClick="SaveTitleButton_Click" Visible="false" runat="server" Text="Save" ValidationGroup="AlbumTitle" />
                <asp:Button ID="DeleteAlbumButton" CssClass="submitButton" OnClick="DeleteAlbumButton_Click" Visible="false" runat="server" Text="Delete Album" OnClientClick="if (!confirm('Are you sure you want to DELETE this album? (The images will NOT be deleted)')) return false;"  />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" ControlToValidate="NewAlbumTitle" ValidationGroup="AlbumTitle"><span style="color:#d14545;font-size:15px;">Title can't be empty.</span></asp:RequiredFieldValidator>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="album-user">by
                <asp:HyperLink ID="AlbumUser" runat="server"></asp:HyperLink></div>
        </div>
        <asp:Table ID="ImageTable" runat="server"></asp:Table>
        <asp:Button ID="deleteSelectedButton" runat="server" CssClass="submitButton removeButton" Text="Remove Selected Images" Visible="false" OnClick="DeleteSelectedButton_Click"  OnClientClick="if (!confirm('Are you sure you want to REMOVE selected images from the album? (The images will NOT be deleted)')) return false;" />
    </div>
</asp:Content>

