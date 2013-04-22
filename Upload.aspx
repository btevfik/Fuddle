<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Upload Image</title>

    <!-- Required plugins for the preview -->
    <link class="jsbin" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script class="jsbin" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/jquery-ui.min.js"></script>

    <!-- Scripts -->
    <script type="text/javascript" src="/scripts/uploadPreview.js"></script>
    <script type="text/javascript" src="/scripts/progress.js"></script>

    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/upload.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>Upload Image</h1>
    </div>

    <br />

    <div id="left-half">
        <div id="upload-content">
            <!-- ASP File Upload control -->
            <div id="inputs">
                Select an image
            <asp:FileUpload ID="uploadFile" Width="240px" runat="server" onchange="previewImage(this)" />

                <br />

                <asp:Label ID="imgTitleLabel" runat="server" Text="Title"></asp:Label>
                <asp:TextBox ID="title" Width="240px" runat="server"></asp:TextBox>

                <br />

                <asp:Label ID="imgDescLabel" runat="server" Text="Description"></asp:Label>
                <asp:TextBox ID="description" runat="server" TextMode="MultiLine" Width="240px"></asp:TextBox>
            </div>

            <br />

            <asp:Button ID="uploadButton" CssClass="submitButton" runat="server" Text="Upload" OnClick="uploadButton_Click" OnClientClick="loadProgress()" />

            <div id="progress" style="display:none; text-align:center">
                <img id ="progressImg" src="/resources/loader.gif" alt="Loading..." style="padding-left:5px; padding-top:5px;" />
            </div>
            
            <asp:Label ID="uploadStatus" Visible="false" CssClass="uploadError" ClientIDMode="Static" runat="server"></asp:Label>
        </div>
    </div>
    <div id="right-half">
        <!-- This works for Chrome/Safari/FF -->
        <div style="width: 370px;margin-left: 30px;text-align: center;">Image Preview</div>
        <img id="image" class="preImg" src="/resources/placeholder.png" alt="Your image" />
    </div>

    <!-- clear floats -->
    <div style="clear:both;margin-bottom:20px"></div>

</asp:Content>
