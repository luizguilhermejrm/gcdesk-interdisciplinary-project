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
        string title = lblTitle.Text;

        if (!IsCollaborator(user.TypeAccess))
        {
           Response.Redirect("../../Default.aspx");
        }
        else
        {
           title = "Bem vindo (Colaborador) : " + user.Email;
        }
    }

    private bool IsCollaborator(int tipo)
    {
        bool retorno = false;
        if (tipo == 1)
        {
            retorno = true;
        }
        return retorno;
    }
    
}