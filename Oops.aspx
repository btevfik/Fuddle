<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Oops.aspx.cs" Inherits="Oops" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="/stylesheets/error.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
       <asp:Image ID="errImage" CssClass="errorImg" runat="server" />
        <br />
       <asp:Label ID="errMessage" runat="server"></asp:Label>
    </div>
</asp:Content>
