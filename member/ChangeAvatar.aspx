<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeAvatar.aspx.cs" Inherits="member_ChangeAvatar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Change Avatar</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/updateavatar.css" />
    <link href="/stylesheets/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <!--Scripts -->
    <script type="text/javascript" src="/scripts/jquery.Jcrop.js"></script>
    <script src="/scripts/jquery.screwdefaultbuttonsV2.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            $('input:radio').screwDefaultButtons({
                image: 'url("/resources/checkboxSmall.jpg")',
                width: 43,
                height: 43
            });

            jQuery('#imgCrop').Jcrop({
                onSelect: storeCoords,
                aspectRatio: 1,
                maxSize: [600, 600],
                minSize: [80, 80]
            });

        });

        function storeCoords(c) {
            jQuery('#X').val(c.x);
            jQuery('#Y').val(c.y);
            jQuery('#W').val(c.w);
            jQuery('#H').val(c.h);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <h1>Change Your Picture</h1>
    </div>
    <div class="left">
        <asp:Image CssClass="profile-image" ID="GravatarImage" runat="server" />
        <br />
        <span class="useLabel">Use Gravatar</span>
        <asp:RadioButton AutoPostBack="true" CssClass="radioButton" ID="PickGravatar" ViewStateMode="Enabled" OnCheckedChanged="PickGravatar_CheckedChanged" GroupName="PickChoice" runat="server" />
        <div style="clear: both"></div>
        <br />
        <asp:HyperLink runat="server" CssClass="uploadButton" NavigateUrl="http://gravatar.com" Target="_blank">Get your gravatar</asp:HyperLink>
    </div>
    <div class="right">
        <asp:Image CssClass="profile-image" ID="UploadedImage" runat="server" />
        <br />
        <span class="useLabel">Use Uploaded Picture</span>
        <asp:RadioButton AutoPostBack="true" CssClass="radioButton" ID="PickUpload" ViewStateMode="Enabled" OnCheckedChanged="PickUpload_CheckedChanged" GroupName="PickChoice" runat="server" />
        <div style="clear: both"></div>
        <br />
        <asp:FileUpload ID="Upload" runat="server" />
        <br /><br />
        <asp:Button CssClass="uploadButton" ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
        <br />
        <asp:Label ID="lblError" CssClass="avatarError" runat="server" Visible="false" />
    </div>

    <div style="clear: both"></div>

    <asp:Panel ID="pnlCrop" runat="server" Visible="false">

        <asp:Image ID="imgCrop" ClientIDMode="Static" runat="server" />

        <br />

        <asp:HiddenField ID="X" ClientIDMode="Static" runat="server" />

        <asp:HiddenField ID="Y" ClientIDMode="Static" runat="server" />

        <asp:HiddenField ID="W" ClientIDMode="Static" runat="server" />

        <asp:HiddenField ID="H" ClientIDMode="Static" runat="server" />

        <asp:Button ID="btnCrop" CssClass="submitButton" runat="server" Text="Crop and Save" OnClick="btnCrop_Click" />

    </asp:Panel>
</asp:Content>

