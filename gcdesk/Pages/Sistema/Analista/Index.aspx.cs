using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Session["ID"]);
        UserBD bd = new UserBD();
        User user = bd.Select(id);

        if (!IsAnalisty(user.TypeAccess))
        {
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            lblTitle.Text = user.Email;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$(window).on('load', function () { $('#myModal').modal('show');});</ script > ", false);
        }
    }

    private bool IsAnalisty(int tipo)
    {
        bool retorno = false;
        if (tipo == 0)
        {
            retorno = true;
        }
        return retorno;
    }
}