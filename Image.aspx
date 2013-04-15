﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Image.aspx.cs" Inherits="Image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/image.css" />
    <!-- Scripts -->
    <script type="text/javascript" src="/scripts/jquery.autosize.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- FACEBOOK JS SDK -->
    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
    </script>

    <div class="content">
        <div class="outer">
            <div class="inner">
                <br />
                <asp:Label ID="imageTitle" runat="server" Text="Title of the image goes here"></asp:Label>
                <br />
                <asp:Image ID="Image1" runat="server" CssClass="image" />
                <br />
                <asp:Label ID="imageDescription" runat="server" Text="Description of the image goes here"></asp:Label>
                <br />
                <br />
                <!--SCRIPT MANAGER FOR UPDATE PANEL -->
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                    <ContentTemplate>
                        <div class="buttons">
                            <asp:Button runat="server" ID="upButton" OnClick="upButton_Click" Text="Up &uarr;" CssClass="uploadButton" />
                            <asp:TextBox runat="server" ID="upCount" Width="10" Text="0" Enabled="false" />
                            &nbsp;&nbsp;
                    <asp:Button runat="server" ID="downButton" OnClick="downButton_Click" Text="Down &darr;" CssClass="uploadButton" />
                            <asp:TextBox runat="server" ID="downCount" Width="10" Text="0" Enabled="false" />
                            &nbsp;&nbsp;
                    <asp:Button runat="server" ID="cuddleButton" OnClick="cuddleButton_Click" Text="Cuddle &hearts;" CssClass="uploadButton" />
                            <asp:TextBox runat="server" ID="cuddleCount" Width="10" Text="0" Enabled="false" />
                            &nbsp;&nbsp;
                    <fb:like send="true" layout="button_count" width="0" show_faces="true"></fb:like>
                            <br />
                            <asp:Label ID="error" runat="server" Style="color: red" Text=""></asp:Label>
                        </div>
                        <div class="comments">
                            <asp:LoginView ID="LoginView1" runat="server">
                                <AnonymousTemplate>
                                    <asp:LoginStatus ID="LoginStatus1" runat="server" />
                                    to leave a comment.
                                </AnonymousTemplate>
                                <LoggedInTemplate>
                                    <asp:TextBox ID="AddCommentBox" Rows="1" Columns="60" placeholder="Add a comment..." TextMode="MultiLine" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </LoggedInTemplate>
                            </asp:LoginView>
                            <asp:Button ID="commentButton" CssClass="uploadButton" runat="server" OnClick="commentButton_Click" Style="display: none;" Text="Comment"></asp:Button>
                            <br />
                            <br />
                            <b>Comments</b>
                            <asp:Panel ID="commentPanel" runat="server"></asp:Panel>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="clear"></div>
    </div>

    <!-- COMMENT ON ENTER KEY PRESSED-->
    <script>
        function pageLoad() {
            $("#AddCommentBox").keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { //Enter keycode
                    var comment = $('#AddCommentBox').val();
                    //simulate the button click
                    __doPostBack("<%=commentButton.UniqueID %>", "");
                }
            });
            $('textarea').autosize();
        }
    </script>

    <!-- PARSE FB AFTER UPDATE PANEL IS UPDATED -->
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_pageLoaded(pageLoaded);

        var _panels, _count;

        function pageLoaded(sender, args) {
            if (_panels != undefined && _panels.length > 0) {
                for (i = 0; i < _panels.length; i++)
                    _panels[i].dispose();
            }

            var panels = args.get_panelsUpdated();

            if (panels.length > 0) {
                updateFbLike();
            }
        }

        function updateFbLike() {
            FB.XFBML.parse();
        }
    </script>
</asp:Content>

