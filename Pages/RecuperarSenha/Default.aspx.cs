using System;

public partial class RecuperarSenha : System.Web.UI.Page
{
    private readonly UserService _userService = new UserService();
    private readonly PasswordResetService _passwordResetService = new PasswordResetService();

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        if (string.IsNullOrEmpty(email))
        {
            lblMsg.Text = "<div class='alert alert-warning mt-2'>Digite seu email.</div>";
            return;
        }

        User user = _userService.FindByEmail(email);
        if (user == null)
        {
            lblMsg.Text = ToastHelper.Error("Email não encontrado.");
            return;
        }

        string token = Guid.NewGuid().ToString("N");
        string expiresAt = DateTime.Now.AddHours(1).ToString(@"dd/MM/yyyy HH:mm:ss");
        _passwordResetService.CreateResetToken(user.UserId, token, expiresAt);

        string resetLink = Request.Url.GetLeftPart(UriPartial.Authority) +
            "/Pages/RedefinirSenha/Default.aspx?token=" + token;

        EmailService.Send(user.Email, "Recuperação de Senha - GC Desk",
            $"<p>Olá {user.Name},</p><p>Clique no link para redefinir sua senha:</p>" +
            $"<p><a href='{resetLink}'>{resetLink}</a></p>" +
            "<p>Este link expira em 1 hora.</p>");

        lblMsg.Text = ToastHelper.Success("Email enviado! Verifique sua caixa de entrada.");
    }
}
