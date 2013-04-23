<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Image.aspx.cs" Inherits="Image" %>

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
        <br />
        <!--SCRIPT MANAGER FOR UPDATE PANELS -->
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="outer">
            <div class="inner">
                <!-- UPDATE PANEL 1 CONTAINS THE TITLE OF IMAGE -->
                <asp:UpdatePanel ID="UpdatePanel1" ClientIDMode="Static" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="updateButton" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="saveButton" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label ID="imageTitle" CssClass="img-title" runat="server" Text="Title of the image goes here"></asp:Label>
                        <asp:TextBox ID="updateTitle" ClientIDMode="Static" CssClass="img-title updateBox" Visible="false" runat="server"></asp:TextBox>
                        <asp:Label ID="imageUser" CssClass="img-user" runat="server" Text="Label"></asp:Label>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- UPDATE PANEL 1 ENDS -->
                <div class="img-container">
                    <!-- MAIN IMAGE IS OUTSIDE OF UPDATE PANELS -->
                    <asp:Image ID="Image1" runat="server" CssClass="image" />
                    <!-- UPDATE PANEL 2 CONTAINS THE DELETE/UPDATE/SAVE BUTTONS -->
                    <asp:UpdatePanel ID="UpdatePanel2" ClientIDMode="Static" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="updateButton" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="saveButton" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="deleteButton" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="buttonGroup">
                                <asp:LinkButton ID="deleteButton" CssClass="updateButton  submitButton" Visible="false" OnClick="delete_Click" OnClientClick="if (!confirm('Are you sure you want to delete this image?')) return false;" runat="server">Delete</asp:LinkButton>
                                <asp:LinkButton ID="updateButton" CssClass="updateButton  submitButton" Visible="false" OnClick="updateButton_Click" runat="server">Update Info</asp:LinkButton>
                                <asp:LinkButton ID="saveButton" CssClass="updateButton  submitButton" Visible="false" OnClick="saveButton_Click" runat="server">Save</asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--  UPDATE PANEL 2 ENDS -->
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <!-- UPDATE PANEL 3 CONTAINS THE IMG DESCRIPTION -->
        <asp:UpdatePanel ID="UpdatePanel3" ClientIDMode="Static" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="updateButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="saveButton" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="imageDescription" CssClass="img-desc" runat="server" Text="Description of the image goes here"></asp:Label>
                <asp:TextBox ID="updateDesc" ClientIDMode="Static" CssClass="img-desc" TextMode="MultiLine" Visible="false" runat="server"></asp:TextBox>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- UPDATE PANEL 3 ENDS -->
        <div class="buttons">
            <!-- UPDATE PANEL 4 CONTAINS THE BUTTONS FOR VOTING -->
            <asp:UpdatePanel ID="UpdatePanel4" RenderMode="Inline" ClientIDMode="Static" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:Button runat="server" ID="upButton" OnClick="upButton_Click" Text="Up &uarr;" CssClass="uploadButton" />
                    <asp:TextBox runat="server" ID="upCount" Width="10" Text="0" Enabled="false" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="downButton" OnClick="downButton_Click" Text="Down &darr;" CssClass="uploadButton" />
                    <asp:TextBox runat="server" ID="downCount" Width="10" Text="0" Enabled="false" />
                    &nbsp;&nbsp;
                    <asp:Button runat="server" ID="cuddleButton" OnClick="cuddleButton_Click" Text="Cuddle &hearts;" CssClass="uploadButton" />
                    <asp:TextBox runat="server" ID="cuddleCount" Width="10" Text="0" Enabled="false" />
                    &nbsp;&nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- UPDATE PANEL 4 ENDS -->
            <!-- FB LIKE / SHARE BUTTON -->
            <fb:like send="true" layout="button_count" width="0" show_faces="true"></fb:like>
            <!-- AddThis Button BEGIN -->
            <div style="display: inline-block" id="addThis" class="addthis_toolbox addthis_default_style addthis_16x16_style">
                <a style="font-size: 13px; top: 4px; color: #3B5998; position: relative" class="addthis_button_email">Email</a>
            </div>
            <!-- AddThis Button END -->
            <br />
        </div>
        <!-- UPDATE PANEL 5 CONTAINS THE COMMENTS -->
        <asp:UpdatePanel ID="UpdatePanel5" ClientIDMode="Static" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="commentButton" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div class="comments">
                    <asp:LoginView ID="LoginView1" runat="server">
                        <AnonymousTemplate>
                            <asp:LoginStatus ID="LoginStatus1" runat="server" />
                            to leave a comment. Don't have an account?
                                    <a href="Register.aspx">Sign up.</a>
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <asp:TextBox ID="AddCommentBox" CssClass="comment-box" Rows="1" Columns="60" placeholder="Add a comment..." TextMode="MultiLine" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </LoggedInTemplate>
                    </asp:LoginView>
                    <asp:Button ID="commentButton" CssClass="uploadButton" runat="server" OnClick="commentButton_Click" Style="display: none;" Text="Comment"></asp:Button>
                    <br />
                    <br />
                    <b>Comments</b>
                    <asp:Panel ID="commentPanel" runat="server">
                        <asp:Panel ID="nocomment" Style='font-size: 16px; margin-top: 10px;' runat="server">No comments yet.</asp:Panel>
                    </asp:Panel>
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- UPDATE PANEL 5 ENDS -->
        <!-- UPDATE PANEL 6 CONTAINS ERROR -->
        <asp:UpdatePanel ID="UpdatePanel6" ClientIDMode="Static" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="deleteButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="updateButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="saveButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="closeError" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel runat="server" ClientIDMode="Static" ID="lightbox" Visible="false">
                    <div id="content">
                        <asp:Label ID="error" CssClass="error" runat="server" Text=""></asp:Label>
                        <asp:Button ID="closeError" runat="server" OnClick="closeError_Click" Text="Close" CssClass="submitButton" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- ENTER KEY PRESSED ON COMMENT BOX-->
    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        function pageLoaded(sender, args) {

            //autosize textarea
            $('textarea').autosize();

            //get panels updated
            var panelsUpdated = args.get_panelsUpdated();

            for (var i = 0; i < panelsUpdated.length; i++) {
                if (panelsUpdated[i].id === "UpdatePanel5") {
                    //it's ok
                    //console.log("YES: "+i + " "+ panelsUpdated[i].id);
                }
                //we don't want other update panels
                else {
                    //console.log("NO: " + panelsUpdated[i].id);
                    return;
                }
            }

            //console.log("HERE");

            $("#AddCommentBox").keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { //Enter keycode
                    //simulate the button click
                    __doPostBack("<%=commentButton.UniqueID %>", "");
                        }
                    });
        }
    </script>
</asp:Content>

