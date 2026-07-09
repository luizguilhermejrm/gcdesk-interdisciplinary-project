using System;

public partial class _Default : System.Web.UI.Page
{
    private readonly UserService _userService = new UserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        string fromPage = Request.QueryString?["from"];
        if (fromPage == "logout")
            MensageExitToast();
        if (fromPage == "accessdenied")
            MensageAccessDenied();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();

        if (!IsComplete(email) || !IsComplete(password))
        {
            lblMsg.Text = ToastHelper.Warning("Preencha o e-mail e a senha corretamente.");
            txtEmail.Focus();
            return;
        }

        User user = _userService.Authenticate(email, password);

        if (user == null)
        {
            AuditService.Log("0", "LOGIN_FAILED", "Email: " + email);
            lblMsg.Text = ToastHelper.Error("Usuário não existe!");
            return;
        }

        Session.RemoveAll();
        Session["USER_BD"] = user;
        AuditService.Log(user.UserId.ToString(), "LOGIN", Request.RawUrl);

        switch (user.TypeAccess)
        {
            case 0:
                Response.Redirect("Pages/Sistema/Analista/Index/Default.aspx");
                break;
            case 1:
                Response.Redirect("Pages/Sistema/Colaborador/Index/Default.aspx");
                break;
        }
    }

    private static bool IsComplete(string str)
    {
        return str != string.Empty;
    }

    public void MensageExitToast()
    {
        lblExit.Text = ToastHelper.Success("Logout realizado!");
    }

    public void MensageAccessDenied()
    {
        lblExit.Text = ToastHelper.Error("Acesso Negado, entre em contato com Analista do Sistema!");
    }
}
