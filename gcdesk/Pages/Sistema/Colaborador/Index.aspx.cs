using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Sistema_Colaborador_Index : System.Web.UI.Page
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
        }

        txtData.Text = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
        txtData.Attributes.Add("readonly", "true");

        TicketBD Tbd = new TicketBD();

        CarregarTickets();
        if (gdvTickets.Rows.Count > 0)
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    private bool IsAnalisty(int tipo)
    {
        bool retorno = false;
        if (tipo == 1)
        {
            retorno = true;
        }
        return retorno;
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {});" +
           "document.onreadystatechange = function () {" +
           " myModal.show();" +
           "};", true);
    }

    protected void btnTicket_Click(object sender, EventArgs e)
    {
        Ticket tic = new Ticket();
        tic.Description = txtProblem.Text;
        tic.Localization = txtLocal.Text;
        tic.OpenTime = txtData.Text;

        if (TicketBD.Insert(tic) == 0)
        {
            lblMsgSuccess.Visible = true;
        }
        else
        {
            lblMsgError.Visible = true;
        }
    }

    void CarregarTickets()
    {
        DataSet dsTicket = TicketBD.SelecionarTodos();
        int qtd = dsTicket.Tables[0].Rows.Count;
        gdvTickets.Visible = false;
        if (qtd > 0)
        {
            gdvTickets.DataSource = dsTicket.Tables[0].DefaultView;
            gdvTickets.DataBind();
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvTickets.Visible = true;
        }
    }

}
