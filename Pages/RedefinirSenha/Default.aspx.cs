using System;

public partial class RedefinirSenha : System.Web.UI.Page
{
    private readonly PasswordResetService _passwordResetService = new PasswordResetService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string token = Request.QueryString["token"];
            if (string.IsNullOrEmpty(token))
            {
                lblMsg.Text = ToastHelper.Error("Token inválido.");
                btnReset.Visible = false;
                return;
            }
            hiddenToken.Value = token;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        string token = hiddenToken.Value;
        string password = txtPassword.Text.Trim();
        string confirm = txtConfirm.Text.Trim();

        if (password != confirm || string.IsNullOrEmpty(password))
        {
            lblMsg.Text = ToastHelper.Warning("Senhas não conferem ou estão vazias.");
            return;
        }

        var result = _passwordResetService.GetValidToken(token);
        if (result.Count == 0)
        {
            lblMsg.Text = ToastHelper.Error("Token expirado ou inválido.");
            return;
        }

        int userId = result[0].UserId;
        int resetId = result[0].ResetId;

        _passwordResetService.ResetPassword(userId, password);
        _passwordResetService.MarkTokenUsed(resetId);

        lblMsg.Text = ToastHelper.Success("Senha redefinida com sucesso! <a href='../Default.aspx'>Fazer login</a>");
        btnReset.Visible = false;
    }
}
