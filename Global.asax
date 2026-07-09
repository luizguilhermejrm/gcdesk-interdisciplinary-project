<%@ Application Language="C#" %>

<script runat="server">

    void Application_BeginRequest(object sender, EventArgs e)
    {
        var csp = "default-src 'self'; " +
                  "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.jsdelivr.net https://cdnjs.cloudflare.com https://code.jquery.com https://cdn.datatables.net; " +
                  "style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net https://cdnjs.cloudflare.com https://cdn.datatables.net; " +
                  "font-src 'self' https://cdnjs.cloudflare.com https://cdn.jsdelivr.net data:; " +
                   "img-src 'self' data: https://cdn.jsdelivr.net https://ui-avatars.com; " +
                   "connect-src 'self' https://cdn.jsdelivr.net; " +
                  "frame-ancestors 'none'; " +
                  "base-uri 'self'; " +
                  "form-action 'self'";

        HttpContext.Current.Response.AppendHeader("Content-Security-Policy", csp);
        HttpContext.Current.Response.AppendHeader("X-Frame-Options", "DENY");
        HttpContext.Current.Response.AppendHeader("X-Content-Type-Options", "nosniff");
        HttpContext.Current.Response.AppendHeader("Referrer-Policy", "strict-origin-when-cross-origin");
        HttpContext.Current.Response.AppendHeader("Permissions-Policy", "camera=(), microphone=(), geolocation=()");
    }

    void Application_Start(object sender, EventArgs e) { }

    void Application_End(object sender, EventArgs e) { }

    void Application_Error(object sender, EventArgs e)
    {
        Exception exc = Server.GetLastError();
        if (exc == null) return;

        try
        {
            System.IO.File.AppendAllText("/app/error_log.txt",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + exc + "\n\n");
        }
        catch { }

        try
        {
            HttpException httpExc = exc as HttpException;
            int httpCode = httpExc?.GetHttpCode() ?? 500;
            AuditService.Log("0", "UNHANDLED_" + httpCode, exc.Message);

            if (httpCode == 404)
                Server.Transfer("~/Pages/PageError/Error404.aspx");
            else
                Server.Transfer("~/Pages/PageError/Error.aspx");
        }
        catch
        {
        }
    }

    void Session_Start(object sender, EventArgs e) { }

    void Session_End(object sender, EventArgs e)
    {
        User user = Session?["USER_BD"] as User;
        if (user != null)
            AuditService.Log(user.UserId.ToString(), "SESSION_EXPIRED", "Sessão expirada ou logout");
    }

</script>
