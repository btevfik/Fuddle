<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=DeleteImagesText.ClientID%>").keyup(function () {
                $("#<%=result.ClientID%>").html("");
            });
            $("#<%=DeleteAlbumsText.ClientID%>").keyup(function () {
                $("#<%=result.ClientID%>").html("");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="text-align:center;">
    <h2>
        Welcome to Admin Pages.
    </h2>
        <br /><br />
        Image ids
        <asp:TextBox ID="DeleteImagesText" runat="server"></asp:TextBox><asp:Button ID="DeleteImagesButton" OnClick="DeleteImagesButton_Click" runat="server" Text="Delete Images" />
        <br /><br />
        Album ids
        <asp:TextBox ID="DeleteAlbumsText" runat="server"></asp:TextBox><asp:Button ID="DeleteAlbumsButton" OnClick="DeleteAlbumsButton_Click" runat="server" Text="Delete Albums" />
        <br />
        Tip: Pass ids seperated by comma.
        <br /><br />
        <asp:Label ID="result" runat="server" Text="" style="color:red"></asp:Label>
    </div>
</asp:Content>
