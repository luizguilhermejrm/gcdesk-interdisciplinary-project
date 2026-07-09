using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_TodosChamados : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly TicketService _ticketService = new TicketService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadTickets();
    }

    void LoadTickets(string search = "")
    {
        DataSet dsTicket = string.IsNullOrEmpty(search)
            ? _ticketService.GetAllForAnalyst()
            : _ticketService.SearchAllForAnalyst(search);

        gdvTickets.Visible = dsTicket.Tables[0].Rows.Count > 0;

        if (!gdvTickets.Visible)
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Chamados!</div>";
            return;
        }

        gdvTickets.DataSource = dsTicket.Tables[0].DefaultView;
        gdvTickets.DataBind();
        gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadTickets(txtSearch.Text.Trim());
    }

    protected void gdvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvTickets.PageIndex = e.NewPageIndex;
        LoadTickets(txtSearch.Text.Trim());
    }

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

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

        string status = e.Row.Cells[6].Text;
        if (status == "1")
            e.Row.Cells[6].Text = "<i class='text-warning fa fa-clock'></i> Em Andamento";
        else if (status == "0")
            e.Row.Cells[6].Text = "<i class='text-primary fa fa-spinner'></i> Aberto";
        else if (status == "2")
            e.Row.Cells[6].Text = "<i class='text-success fa fa-check'></i> Finalizado";
    }
}
