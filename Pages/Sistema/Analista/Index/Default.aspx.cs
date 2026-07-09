using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_Index : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly TicketService _ticketService = new TicketService();
    private readonly UserService _userService = new UserService();
    private readonly NotificationService _notificationService = new NotificationService();
    private readonly TicketRepository _ticketRepository = new TicketRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        User user = GetCurrentUser();

        LoadTickets();
        LoadNotification();
        LoadStats(user.UserId);
        LoadChartData();
    }

    private void LoadStats(int userId)
    {
        lblOpenCalls.Text = _ticketService.CountAllOpen();
        lblProgressCalls.Text = _ticketService.CountProgressByAnalyst(userId);
        lblFinishedCalls.Text = _ticketService.CountFinishedByAnalyst(userId);
        lblQuantityPerson.Text = _userService.GetActiveCount();
    }

    private void LoadChartData()
    {
        hiddenChartStatus.Value = DataSetToJson(_ticketService.GetTicketsByStatus(), "status_name", "total");
        hiddenChartDept.Value = DataSetToJson(_ticketService.GetTicketsByDepartment(), "dep_sector", "total");
        hiddenChartPeriod.Value = DataSetToJson(_ticketService.GetTicketsByPeriod(7), "dia", "total");
        hiddenChartBottleneck.Value = DataSetToJson(_ticketService.GetReopenByType(), "tic_type", "total_reopens");
        LoadSlaAlerts();
    }

    private void LoadSlaAlerts()
    {
        DataSet slaBreached = _ticketService.GetSlaBreached();
        StringBuilder html = new StringBuilder();
        if (slaBreached.Tables[0].Rows.Count > 0)
        {
            html.Append("<div class='alert alert-danger'>");
            foreach (DataRow row in slaBreached.Tables[0].Rows)
            {
                html.Append("<p class='mb-1'><strong>#").Append(row["tic_id"])
                    .Append("</strong> — ").Append(row["tic_description"])
                    .Append(" <span class='badge bg-danger'>SLA Expirado</span></p>");
            }
            html.Append("</div>");
        }
        DataSet autoClose = _ticketService.GetTicketsAwaitingAutoClose();
        if (autoClose.Tables[0].Rows.Count > 0)
        {
            html.Append("<div class='alert alert-info mt-2'>");
            html.Append("<i class='fa-solid fa-clock me-1'></i> <strong>Encerramento Automático:</strong> ");
            html.Append(autoClose.Tables[0].Rows.Count).Append(" chamado(s) aguardando fechamento (48h sem resposta).");
            html.Append("</div>");
        }
        if (html.Length == 0)
            html.Append("<p class='text-muted mb-0'>Nenhum alerta de SLA no momento.</p>");
        lblSlaAlerts.Text = html.ToString();
    }

    private static string DataSetToJson(DataSet ds, string labelField, string valueField)
    {
        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return "[]";

        StringBuilder sb = new StringBuilder("[");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            sb.Append("{\"name\":\"")
              .Append(row[labelField].ToString().Replace("\"", "\\\""))
              .Append("\",\"total\":").Append(row[valueField])
              .Append("},");
        }
        if (sb.Length > 1) sb.Length--;
        sb.Append("]");
        return sb.ToString();
    }

    void LoadTickets()
    {
        DataSet dsTicket = _ticketService.GetAllOpen();
        gdvTickets.Visible = dsTicket.Tables[0].Rows.Count > 0;

        if (!gdvTickets.Visible)
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Chamados em Aberto!</div>";
            return;
        }

        gdvTickets.DataSource = dsTicket.Tables[0].DefaultView;
        gdvTickets.DataBind();
        gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    void LoadNotification()
    {
        User user = GetCurrentUser();
        DataSet dsNotification = _notificationService.GetByAnalystId(user.UserId);
        gdvNotification.Visible = dsNotification.Tables[0].Rows.Count > 0;

        if (!gdvNotification.Visible)
        {
            lblTableNullNotification.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhuma notificação em aberto!</div>";
            return;
        }

        gdvNotification.DataSource = dsNotification.Tables[0].DefaultView;
        gdvNotification.DataBind();
        gdvNotification.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvNotification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

        string status = e.Row.Cells[0].Text;
        if (status == "0")
            e.Row.Cells[0].Text = "<i class='text-primary fa fa-spinner'></i>";
        else if (status == "1")
            e.Row.Cells[0].Text = "<i class='text-warning fa fa-clock'></i>";
        else if (status == "2")
            e.Row.Cells[0].Text = "<i class='text-success fa fa-check'></i>";
    }

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;

        LinkButton lkbPegar = (LinkButton)e.Row.Cells[3].FindControl("lkbPegar");
        lkbPegar.Text = "<i class='fa-solid fa-hand'></i>";
        lkbPegar.CommandName = "vazio";
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        User user = GetCurrentUser();
        int idTicket = Convert.ToInt32(e.CommandArgument);

        _ticketService.Claim(user.UserId, idTicket);
        LogAction("CLAIM_TICKET");

        lblMsg.Text = ToastHelper.Success("Este chamado agora está em Desenvolvimento por você!");
        LoadTickets();
    }
}
