<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Fuddle | Search</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center"><h1>Search Results</h1> 
        <p>You have searched for: <asp:Label ID="searchedQuery" runat="server" Text=""></asp:Label></p>
    </div>
</asp:Content>

