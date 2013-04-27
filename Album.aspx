<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>
            <asp:Label ID="AlbumTitle" runat="server" Text=""></asp:Label>
        </h1>
            by <asp:HyperLink ID="AlbumUser" runat="server"></asp:HyperLink>
    </div>
</asp:Content>

