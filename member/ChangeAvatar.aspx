<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeAvatar.aspx.cs" Inherits="member_ChangeAvatar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Change Avatar</title>
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/updateavatar.css" />
    <link href="/stylesheets/jquery.Jcrop.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <!--Scripts -->
    <script type="text/javascript" src="/scripts/jquery.Jcrop.js"></script>
    <script src="/scripts/jquery.screwdefaultbuttonsV2.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            $('.radios input:radio').screwDefaultButtons({
                image: 'url("/resources/checkboxSmall.jpg")',
                width: 43,
                height: 43
            });

            $('.lightbox_trigger').click(function (e) {
                e.preventDefault();
                //clear any previous errors
                $("#lblError").html("");
                //get lightbox
                var lightbox = $("#lightbox");
                lightbox.css("display", "block");
            });

            $("#content").draggable();

            $('#lightbox').click(function (e) {
                if ($(e.target).is("#content") || $(e.target).is("#Upload") || $(e.target).is("#uploadButton")) return;
                $('#lightbox').css("display", "none");
            });
        });

       function loadJCrop() {
            $('#imgCrop').Jcrop({
                onChange: storeCoords,
                onSelect: storeCoords,
                aspectRatio: 1,
                maxSize: [400, 400],
                minSize: [80, 80],
                setSelect: [100, 100, 80, 80]
            });
        }

        function storeCoords(c) {
            $('#X').val(c.x);
            $('#Y').val(c.y);
            $('#W').val(c.w);
            $('#H').val(c.h);
        }
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
        <span class="radios">
        <asp:RadioButton AutoPostBack="true" CssClass="radioButton" ID="PickGravatar" ViewStateMode="Enabled" OnCheckedChanged="PickGravatar_CheckedChanged" GroupName="PickChoice" runat="server" />
        </span>
        <div style="clear: both"></div>
        <br />
        <asp:HyperLink runat="server" CssClass="uploadButton" NavigateUrl="http://gravatar.com" Target="_blank">Get your gravatar</asp:HyperLink>
    </div>
    <div class="right">
        <asp:Image CssClass="profile-image" ID="UploadedImage" runat="server" />
        <br />
        <span class="useLabel">Use Uploaded Picture</span>
        <span class="radios">
        <asp:RadioButton AutoPostBack="true" CssClass="radioButton" ID="PickUpload" ViewStateMode="Enabled" OnCheckedChanged="PickUpload_CheckedChanged" GroupName="PickChoice" runat="server" />
        </span>
        <div style="clear: both"></div>
        <br />
        <button class="uploadButton lightbox_trigger">Upload a new picture</button>
        <asp:Panel ID="lightbox" ClientIDMode="Static" Style="display: none" runat="server">
            <div id="content" draggable="true">
                <asp:FileUpload ID="Upload" ClientIDMode="Static" runat="server" />
                <br />
                <br />
                <asp:Button CssClass="uploadButton" ClientIDMode="Static" ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Add picture" />
                <br />
                <asp:Label ID="lblError" ClientIDMode="Static" CssClass="avatarError" runat="server" />
            </div>
        </asp:Panel>
    </div>

    <div style="clear: both"></div>

    <asp:Panel ID="pnlCrop" ClientIDMode="Static" runat="server" Visible="false">
        <div class="outer">
            <div class="inner">
                <asp:Image ID="imgCrop" ClientIDMode="Static" runat="server" />

                <br />

                <asp:HiddenField ID="X" ClientIDMode="Static" Value="0" runat="server" />

                <asp:HiddenField ID="Y" ClientIDMode="Static" Value="0" runat="server" />

                <asp:HiddenField ID="W" ClientIDMode="Static" Value="80" runat="server" />

                <asp:HiddenField ID="H" ClientIDMode="Static" Value="80" runat="server" />


            </div>
        </div>

        <div style="clear: both"></div>

        <div id="cropPanelButtons">
            <asp:Button ID="btnCrop" CssClass="submitButton" runat="server" Text="Crop and Save" OnClick="btnCrop_Click" />
            &nbsp;
            <asp:Button ID="cancelBut" CssClass="submitButton" runat="server" Text="Cancel" OnClick="cancelBut_Click" />
        </div>
    </asp:Panel>

    <div style="clear: both"></div>

</asp:Content>

