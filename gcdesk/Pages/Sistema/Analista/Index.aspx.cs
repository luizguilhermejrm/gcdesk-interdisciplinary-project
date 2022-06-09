using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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

        LoadTickets();
        if (gdvTickets.Rows.Count > 0)
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;


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


    void LoadTickets()
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

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[4].FindControl("lkbUpdate");
            if (e.Row.Cells[3].Text == "0")
            {
                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "ativar";
                e.Row.Cells[3].Text = "<i class='text-danger fa fa-times'></i> ";
            }
            else
            {
                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "inativar";
                e.Row.Cells[3].Text = "<i class='text-success fa fa-check'></i>";
            }
        }

    }

     

    protected void btn_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {});" +
           "document.onreadystatechange = function () {" +
           " myModal.show();" +
           "};", true);

        long objetoID = Convert.ToInt64((sender as LinkButton).CommandArgument);

        string ticketID = objetoID.ToString();
        lblId.Text = ticketID;


        //TicketBD ticket = new TicketBD();
        //ticket.SelecionarIndividualTicket(ticketID);

        Ticket tic = new Ticket();
        tic.Description = lblDescricao.Text;






    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int codigoTicket = Convert.ToInt32(e.CommandArgument.ToString());
        int status = 0;
        if (e.CommandName == "ativar")
        {
            status = 1; 
        } 
      
        if (TicketBD.UpdateTicket(status, codigoTicket) == 0)
        {
            LoadTickets();
        }
        else
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "$('#idModal').modal('show')", true);
        }

    }
}