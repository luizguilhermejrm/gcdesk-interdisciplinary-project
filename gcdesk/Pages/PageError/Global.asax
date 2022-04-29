<%@ Application Language="C#" %>

<script runat="server">

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
        Exception exc = Server.GetLastError(); 
 
        // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException)) 
            { 
            if (exc.Message.Contains("NoCatch") || 
                exc.Message.Contains("maxUrlLength")) 
                return; 
                //Server.Transfer("HttpErrorPage.aspx");
            } 
            else
            { 
                 Response.Write("<h2>Mensagem de erro</h2>\n"); 
                 Response.Write( 
                 "<p>" + exc.Message + "</p>\n"); 
                 Response.Write( 
                 "<p>" + exc.InnerException + "</p>\n"); 
                 Response.Write("<a href='~Exemplo.aspx'>" + 
                 "Default Page</a>\n"); 
                 Server.ClearError(); 
             }
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
