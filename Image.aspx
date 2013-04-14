<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Image.aspx.cs" Inherits="Image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/image.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="outer">
            <div class="inner">
                <asp:Image ID="Image1" runat="server" CssClass="image" />
            </div>
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>

