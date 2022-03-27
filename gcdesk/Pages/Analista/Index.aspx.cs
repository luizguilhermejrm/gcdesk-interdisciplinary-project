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
        PersonBD bd = new PersonBD();
        Person person = bd.Select(id);
        string title = lblTitle.Text;

        if (!IsAdministrador(person.TypeAccess))
        {
        Response.Redirect("../Erro/AcessoNegado.aspx");
        }
        else
        {
           title = "Bem vindo (Administrador) : " + person.Email;
        }
    }

    private bool IsAdministrador(int tipo)
    {
        bool retorno = false;
        if (tipo == 0)
        {
            retorno = true;
        }
        return retorno;
    }
}