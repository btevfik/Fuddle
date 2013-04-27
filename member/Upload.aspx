<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Fuddle | Upload Image</title>

    <!-- Required plugins for the preview -->
    <link class="jsbin" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script class="jsbin" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/jquery-ui.min.js"></script>

    <!-- Scripts -->
    <script type="text/javascript" src="/scripts/uploadPreview.js"></script>
    <script type="text/javascript" src="/scripts/progress.js"></script>
    <script src="/scripts/jquery.screwdefaultbuttonsV2.js"></script>

    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/upload.css" />

    <script>
        window.onload(function () {
            $('#content input:radio').screwDefaultButtons({
                image: 'url("/resources/checkboxSmall.jpg")',
                width: 43,
                height: 43
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

                <asp:UpdatePanel ID="UpdatePanel2" RenderMode="Inline" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="pickAlbum" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Button ID="pickAlbum" CssClass="albumButton" OnClick="pickAlbum_Click" runat="server" Text="Pick Album" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <asp:Button ID="uploadButton" CssClass="submitButton" runat="server" Text="Upload" OnClick="uploadButton_Click" OnClientClick="loadProgress()" />

            <div id="progress" style="display: none; text-align: center">
                <img id="progressImg" src="/resources/loader.gif" alt="Loading..." style="padding-left: 5px; padding-top: 5px;" />
            </div>

            <asp:Label ID="uploadStatus" Visible="false" CssClass="uploadError" ClientIDMode="Static" runat="server"></asp:Label>
        </div>
    </div>
    <div id="right-half">
        <!-- This works for Chrome/Safari/FF -->
        <div style="width: 370px; margin-left: 30px; text-align: center;">Image Preview</div>
        <img id="image" class="preImg" src="/resources/placeholder.png" alt="Your image" />
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chooseButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cancelButton" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel runat="server" ClientIDMode="Static" ID="lightbox" Visible="false">
                <asp:Panel runat="server" ID="content" ClientIDMode="Static">
                    <asp:RadioButtonList ID="buttonList" runat="server"></asp:RadioButtonList>
                     <asp:Button ID="chooseButton" runat="server" Text="Choose" CssClass="submitButton" OnClick="chooseButton_Click" />
                    <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick="cancelButton_Click" CssClass="submitButton" />
                    <div style="font-size: 13px">Tip: Create new albums through your profile page.</div>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- clear floats -->
    <div style="clear: both; margin-bottom: 20px"></div>

</asp:Content>
