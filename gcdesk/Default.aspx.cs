using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private bool IsComplete(string str)
    {
        bool retorno = false;
        if (str != string.Empty)
        {
          retorno = true;
        }
        return retorno;
    }
    private bool UserFound(User user)
    {
        bool retorno = false;
        if (user != null)
        {
           retorno = true;
        }
        return retorno;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = Function.HashText(txtPassword.Text.Trim());

        if (!IsComplete(email))
        {
            lblMsg.Text = "Preencha o email";
            txtEmail.Focus();
            return;
        }

        if (!IsComplete(password))
        {
            lblMsg.Text = "Preencha a senha";
            txtPassword.Focus();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$('#modal_aviso').modal('show');</script>", false);
            return;
        }

        UserBD bd = new UserBD();
        User user = new User();
        user = bd.Authenticate(email, password);
        if (!UserFound(user))
        {
            lblMsg.Text = "Usuário não encontrado";
            txtEmail.Focus();
            return;
        }

        Session["ID"] = user.UserId;

        switch (user.TypeAccess)
        {
            case 0:
            Response.Redirect("Pages/Sistema/Analista/Index.aspx");
            break;

            case 1:
            Response.Redirect("Pages/Sistema/Colaborador/Index.aspx");
            break;

            default:
            break;
        }
    }
}