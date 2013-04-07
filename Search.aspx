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
            <span id="numresult" class="num-result"></span>
        </p>
    </div>

    <!--Results-->
    <div id="searchresults" class="search-results"></div>

    <div style="margin:20px auto 0 auto;width:200px">
       <a id="loadMore" class="uploadButton">Load More</a>
    </div>
    <div id="loading" style="text-align:center;margin:0 auto;width:100px">
    </div>

    <!--Searching script -->
    <!--uses SearchService -->
    <script type="text/javascript" src="/scripts/jquery.waitforimages.min.js"></script>    
    <script type="text/javascript" src="/scripts/jquery.esn.autobrowse.js"></script>
    <script type="text/javascript" src="/scripts/jquery.json-2.2.min.js"></script>
    <script type="text/javascript" src="/scripts/jstorage.js"></script>
    <script type="text/javascript" src="/scripts/search.js"></script>
 
    <script type="text/javascript">

        //get parameter specified in the url
        var parameter = getParameterByName("q");

        //on window load, search for images by default
        $(window).load(function () {
            getImages(parameter);
        });


        $(function () {

            $("#loadMore").click(function () {
                loadMore("image", parameter);
            });

            //if images clicked
            jQuery('#<%=imagesLink.ClientID%>').click(function () {
                $('#searchresults').empty();
                getImages(parameter);
                jQuery('#<%=imageListItem.ClientID%>').addClass("activated");
                jQuery('#<%=albumListItem.ClientID%>').removeClass("activated");
                jQuery('#<%=userListItem.ClientID%>').removeClass("activated");
            });

            //if albums clicked
            jQuery('#<%=albumsLink.ClientID%>').click(function () {
                $('#searchresults').empty();
                getAlbums(parameter);
                jQuery('#<%=imageListItem.ClientID%>').removeClass("activated");
                jQuery('#<%=albumListItem.ClientID%>').addClass("activated");
                jQuery('#<%=userListItem.ClientID%>').removeClass("activated");
            });

            //if users clicked
            jQuery('#<%=usersLink.ClientID%>').click(function () {
                $('#searchresults').empty();
                getUsers(parameter);
                jQuery('#<%=imageListItem.ClientID%>').removeClass("activated");
                jQuery('#<%=albumListItem.ClientID%>').removeClass("activated");
                jQuery('#<%=userListItem.ClientID%>').addClass("activated");
            });

        });

    </script>
</asp:Content>
