<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Image.aspx.cs" Inherits="Image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- StyleSheets -->
    <link rel="stylesheet" type="text/css" href="/stylesheets/image.css" />
    <!-- Scripts -->
    <script type="text/javascript" src="/scripts/jquery.autosize.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
        <div class="outer">
            <div class="inner">
                <br />
                <asp:Label ID="imageTitle" runat="server" Text="Title of the image goes here"></asp:Label>
                <br />
                <asp:Image ID="Image1" runat="server" CssClass="image" />
                <br />
                <asp:Label ID="imageDescription" runat="server" Text="Description of the image goes here"></asp:Label>
                <br /><br />
                <div class="buttons">
                    <asp:Button runat="server" ID="upButton" Text="Up &uarr;" CssClass="uploadButton" />
                    <asp:TextBox runat="server" ID="upCount" Width="10" Text="0" Enabled="false" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="downButton" Text="Down &darr;" CssClass="uploadButton" />
                    <asp:TextBox runat="server" ID="downCount" Width="10" Text="0" Enabled="false" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="cuddleBUtotn" Text="Cuddle &hearts;" CssClass="uploadButton" />
                    <asp:TextBox runat="server" ID="cuddleCount" Width="10" Text="0" Enabled="false" />
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
                    <br /><br />
                    <b>Comments</b> 
                    <br /><br />
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <script>
        $(document).ready(function () {
            $("#AddCommentBox").keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { //Enter keycode
                    $('#AddCommentBox').val("");
                    __doPostBack('__Page', 'something');
                }
            });
            $('textarea').autosize();
        });
    </script>
</asp:Content>

