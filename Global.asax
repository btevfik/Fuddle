<%@ Application Language="C#" %>

<script RunAt="server">

    static Regex ProfileRegex = new Regex(@"/user/(?<username>\w*$)",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //handles user profile page requests.
        //they are as "~/user/username"
        Match match = ProfileRegex.Match(Context.Request.FilePath);
        if ((match != null) && match.Success)
        {
            String path = (String.Format("~/UserProfile.aspx?user={0}",
            match.Groups["username"]));
            Context.RewritePath(path, false);
        }
    }

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
