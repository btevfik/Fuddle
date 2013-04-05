<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["ID"];
        if (id == null)
        {
            return;
        }
        Guid oGuid = new Guid(id);
        MembershipUser oUser = Membership.GetUser(oGuid);
        if (oUser != null && oUser.IsApproved == false)
        {
            oUser.IsApproved = true;
            Membership.UpdateUser(oUser);
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(oUser.UserName, false);
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Nothing to see here. Move along.
        </div>
    </form>
</body>
</html>
