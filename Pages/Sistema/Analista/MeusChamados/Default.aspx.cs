using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_MeusChamados : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly TicketService _ticketService = new TicketService();

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadTickets();
        if (gdvTickets.Rows.Count > 0)
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    void LoadTickets()
    {
        User user = GetCurrentUser();
        int ID = user.UserId;

        DataSet dsTicket = _ticketService.GetByAnalystId(ID);
        int qtd = dsTicket.Tables[0].Rows.Count;
        gdvTickets.Visible = false;
        if (qtd > 0)
        {
            gdvTickets.DataSource = dsTicket.Tables[0].DefaultView;
            gdvTickets.DataBind();
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvTickets.Visible = true;
        }
        else
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Chamados em seu nome!</div>";
        }
    }

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int priority = Convert.IsDBNull(DataBinder.Eval(e.Row.DataItem, "tic_priority")) ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "tic_priority"));
            bool slaBreached = !Convert.IsDBNull(DataBinder.Eval(e.Row.DataItem, "sla_breached")) && Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "sla_breached"));
            Label lblBadge = (Label)e.Row.Cells[0].FindControl("lblSlaBadge");
            string badge = "";
            if (slaBreached)
                badge += "<span class='badge bg-danger me-1'>SLA</span>";
            if (priority == 1)
                badge += "<span class='badge bg-warning text-dark'>Alta</span>";
            else if (priority == 2)
                badge += "<span class='badge bg-danger'>Urgente</span>";
            else
                badge += "<span class='badge bg-secondary'>Normal</span>";
            lblBadge.Text = badge;

            LinkButton lkb = (LinkButton)e.Row.Cells[6].FindControl("lkbUpdate");
            if (e.Row.Cells[5].Text == "1")
            {
                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "andamento";
                e.Row.Cells[5].Text = "<i class='text-warning fa fa-clock'></i> ";
            }
            else if (e.Row.Cells[5].Text == "2")
            {
                lkb.Text = "<a class='disable' aria-disabled='true'><i class='text-danger fas fa-sync'></i></a>";
                lkb.CommandName = "fechado";
                e.Row.Cells[5].Text = "<i class='text-success fa fa-check'></i>";
            }
        }
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idTicket = Convert.ToInt32(e.CommandArgument.ToString());

        switch (e.CommandName)
        {
            case "andamento":
                _ticketService.UpdateStatus(1, idTicket, DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss"));
                LogAction("TICKET_IN_PROGRESS", $"Chamado #{idTicket} em andamento");
                break;
            case "fechado":
                _ticketService.Finish(idTicket);
                LogAction("TICKET_FINISHED", $"Chamado #{idTicket} finalizado");
                break;
        }

        LoadTickets();

        lblMsg.Text = ToastHelper.Success("Chamado Finalizado!");
    }
}
