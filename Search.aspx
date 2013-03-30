<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Search</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/search.css" />
    <!-- Scripts -->
    <script src="scripts/search-layout.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--Header -->
    <div style="text-align: center">
        <h1>Search</h1>
        <p>
            <asp:Label ID="searchedQuery" runat="server" Text=""></asp:Label>
        </p>
    </div>
    <!--Search options-->
    <div id="options-menu">
        <ul>
            <li runat="server" id="imageListItem">
                <asp:HyperLink ID="imagesLink" runat="server">Images</asp:HyperLink>
            </li>
            <li runat="server" id="albumListItem">
                <asp:HyperLink ID="albumsLink" runat="server">Albums</asp:HyperLink>
            </li>
            <li runat="server" id="userListItem">
                <asp:HyperLink ID="usersLink" runat="server">Users</asp:HyperLink>
            </li>
        </ul>
        <p style="text-align: center">
            <asp:Label ID="numresult" CssClass="num-result" runat="server" Text=""></asp:Label>
        </p>
    </div>

    <!--Results-->
    <asp:Panel ID="searchresults" CssClass="search-results" runat="server">
        <!--Search Results go into this panel -->
    </asp:Panel>
</asp:Content>
