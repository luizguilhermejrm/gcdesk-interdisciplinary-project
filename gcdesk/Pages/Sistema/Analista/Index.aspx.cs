using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Session["ID"]);
        UserBD bd = new UserBD();
        User user = bd.Select(id);
        string lblEmail = lblEmail.Text;

        if (!IsAdministrator(user.TypeAccess))
        {
           Response.Redirect("../../Default.aspx");
        }
        else
        {
           lblEmail = "Bem vindo (Administrador) : " + user.Email;
        }
    }

    private bool IsAdministrator(int tipo)
    {
        bool retorno = false;
        if (tipo == 0)
        {
            retorno = true;
        }
        return retorno;
    }
}