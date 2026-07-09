using System;
using System.Web;

/// <summary>Base class for all pages providing CSRF protection, access control, and auditing.</summary>
public abstract class BasePage : System.Web.UI.Page
{
    /// <summary>Required user access type (0 = analyst, 1 = collaborator, null = any authenticated).</summary>
    protected abstract int? RequiredAccessType { get; }

    /// <summary>Initializes CSRF token and checks access on every request.</summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        CheckAccess();
        InitCSRF();
    }

    /// <summary>Validates CSRF token on postbacks.</summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (IsPostBack)
            ValidateCSRF();
    }

    /// <summary>Generates and stores a CSRF token in the session and ViewStateUserKey.</summary>
    private void InitCSRF()
    {
        if (Session["CSRF_TOKEN"] == null)
            Session["CSRF_TOKEN"] = Guid.NewGuid().ToString("N");
        ViewStateUserKey = Session["CSRF_TOKEN"].ToString();
    }

    /// <summary>Validates the CSRF token from session against ViewStateUserKey.</summary>
    private void ValidateCSRF()
    {
        string token = Session["CSRF_TOKEN"] as string;
        if (string.IsNullOrEmpty(token) || ViewStateUserKey != token)
        {
            AuditService.Log("0", "CSRF_INVALID", Request.RawUrl);
            Response.Redirect("~/Default.aspx");
        }
    }

    /// <summary>Checks whether the current user is authenticated and authorized for this page.</summary>
    private void CheckAccess()
    {
        User user = Session["USER_BD"] as User;

        if (user == null)
        {
            AuditService.Log("0", "ACCESS_DENIED_NO_SESSION", Request.RawUrl);
            Response.Redirect("~/Default.aspx");
            return;
        }

        if (user.StatusUser != 1)
        {
            AuditService.Log(user.UserId.ToString(), "ACCESS_DENIED_INACTIVE", Request.RawUrl);
            Response.Redirect("~/Default.aspx?from=accessdenied");
            return;
        }

        if (RequiredAccessType.HasValue && user.TypeAccess != RequiredAccessType.Value)
        {
            AuditService.Log(user.UserId.ToString(), "ACCESS_DENIED_TYPE", Request.RawUrl);
            Response.Redirect("~/Default.aspx?from=accessdenied");
            return;
        }
    }

    /// <summary>Returns the currently authenticated user from the session.</summary>
    protected User GetCurrentUser()
    {
        return Session["USER_BD"] as User;
    }

    /// <summary>Logs an action to the audit trail for the current user.</summary>
    protected void LogAction(string action)
    {
        User u = GetCurrentUser();
        if (u != null)
            AuditService.Log(u.UserId.ToString(), action, Request.RawUrl);
    }

    /// <summary>Logs an action to the audit trail for the current user with extra detail.</summary>
    protected void LogAction(string action, string detail)
    {
        User u = GetCurrentUser();
        if (u != null)
            AuditService.Log(u.UserId.ToString(), action, detail);
    }
}
