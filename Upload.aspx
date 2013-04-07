<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Fuddle | Upload Images</title>

    <!-- Required plugins for the preview -->
    <link class="jsbin" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script class="jsbin" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/jquery-ui.min.js"></script>
    
    <!-- Scripts -->
    <script src="/scripts/uploadPreview.js"></script>
    <script src="/scripts/uploadPreview_IE.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center"><h1>Upload Image</h1>
        <p>Preliminary upload page where you can select an image, a preview will be shown, and the image will
            be saved to the database.</p>
    </div>

    <br /><br /><br />

    <!-- Some formatting spaces -->
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <!-- ASP File Upload control -->
    <asp:FileUpload ID="uploadFile" runat="server" onchange="previewImage(this)" />

    <br /><br />
    <!-- Some formatting spaces -->
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    <asp:Label ID="imgTitleLabel" runat="server" Text="Title"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="title" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;
    <br /><br />
        
    <!-- Some formatting spaces -->
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    <asp:Label ID="imgDescLabel" runat="server" Text="Description"></asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="description" runat="server" TextMode="MultiLine" Width="229px"></asp:TextBox>

    <br /><br />
        
    <!-- Some formatting spaces -->
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="uploadButton" runat="server" Text="Upload" OnClick="uploadButton_Click" />

    <br /><br />
        
    <!-- Some formatting spaces -->
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="uploadStatus" runat="server"></asp:Label>
    

    <!-- This works for Chrome -->
    <div style="text-align:center">
        <asp:Label ID="imgPrevLabel" runat="server" Text="Image Preview: "></asp:Label>
        <br />
        <img id="image" src="#" alt="Your image" />
    </div>

    <!-- This is needed for compatibility with IE -->
    <div id="preview_IE" style="FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)"></div>
    
    <br /><br /><br />
    <div style="text-align: center">
        <asp:Label ID="Label1" runat="server" Text="Image title:"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="retrieveFile" runat="server" Width="184px"></asp:TextBox>
        <br />
        <asp:Button ID="retrieve" runat="server" Text="Retrieve Image" OnClick="retrieve_Click" />
        <br />
        <asp:Label ID="retrieveStatus" runat="server"></asp:Label>
        <br />
        <asp:Image ID="Image1" runat="server" />
    </div>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:fuddleConnectionString %>" SelectCommand="SELECT Image_id FROM Image_table WHERE (Image_title = @title)">
    <SelectParameters>
        <asp:ControlParameter ControlID="retrieveFile" Name="title" PropertyName="Text" />
    </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
