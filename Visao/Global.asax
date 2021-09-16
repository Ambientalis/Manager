<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        JobScheduler.Start();

        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        Response.Write("<font face=\"Tahoma\" size=\"2\" color=\"red\">");
        Response.Write("Oops! Looks like an error occurred!!<hr></font>");
        Response.Write("<font face=\"Arial\" size=\"2\">");
        Response.Write(Server.GetLastError().Message.ToString());
        Response.Write("<hr>" + Server.GetLastError().ToString());
        Server.ClearError();

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        com.flajaxian.FileUploader.RegisterAspCookies();
        if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://www."))
        {
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://www.", "http://"));
        }
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
