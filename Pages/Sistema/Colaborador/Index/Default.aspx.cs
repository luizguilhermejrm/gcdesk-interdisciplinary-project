using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Colaborador_Index : BasePage
{
    protected override int? RequiredAccessType => 1;

    private readonly TicketService _ticketService = new TicketService();
    private readonly NotificationService _notificationService = new NotificationService();
    private readonly EquipmentRepository _equipRepo = new EquipmentRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        User user = GetCurrentUser();

        txtData.Text = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
        txtData.Attributes.Add("readonly", "true");

        if (!IsPostBack)
            LoadEquipments();

        LoadTickets();
        LoadNotification();
        if (gdvTickets.Rows.Count > 0)
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;

        int ID = user.UserId;
        lblFinishedCalls.Text = _ticketService.CountFinishedByCollaborator(ID);
        lblProgressCalls.Text = _ticketService.CountProgressByCollaborator(ID);
        lblOpenCalls.Text = _ticketService.CountOpenByCollaborator(ID);
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {});" +
           "document.onreadystatechange = function () {" +
           " myModal.show();" +
           "};", true);
    }

    private bool IsComplete(string str)
    {
        return str != string.Empty;
    }

    private void LoadEquipments()
    {
        var list = _equipRepo.GetAll();
        ddlEquip.Items.Clear();
        ddlEquip.Items.Add(new ListItem("Selecione um equipamento", "0"));
        foreach (var e in list)
            ddlEquip.Items.Add(new ListItem(e.Description, e.EquipmentId.ToString()));
    }

    protected void btnTicket_Click(object sender, EventArgs e)
    {
        string description = txtProblem.Text.Trim();
        string type = ddlTypeTicket.Text.Trim();
        string localization = txtLocal.Text.Trim();

        if (!IsComplete(description) || !IsComplete(type) || !IsComplete(localization))
        {
            lblValidator.Text = ToastHelper.Warning("Há campos vazios que devem ser preenchidos!");
            return;
        }

        User user = GetCurrentUser();
        int ID = user.UserId;

        Ticket tic = new Ticket();
        tic.Description = txtProblem.Text;
        tic.TypeTicket = ddlTypeTicket.Text;
        tic.Localization = txtLocal.Text;
        tic.OpenTime = txtData.Text;
        tic.UserId = ID;
        tic.Priority = int.Parse(ddlPriority.SelectedValue);
        tic.EquipId = int.Parse(ddlEquip.SelectedValue);

        if (_ticketService.Create(tic) == 0)
        {
            LogAction("CREATE_TICKET", $"Chamado criado: {tic.Description}");
            string timeMensagem = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
            _notificationService.InsertNotificationStatusCreate(timeMensagem);

            lblCdTicket.Text = ToastHelper.Success("Chamado criado com sucesso.");
            LoadTickets();
            LoadNotification();
            User u = GetCurrentUser();
            lblFinishedCalls.Text = _ticketService.CountFinishedByCollaborator(u.UserId);
            lblProgressCalls.Text = _ticketService.CountProgressByCollaborator(u.UserId);
            lblOpenCalls.Text = _ticketService.CountOpenByCollaborator(u.UserId);
        }
        else
        {
            lblCdTicket.Text = ToastHelper.Error("Nao foi possivel criar o Chamado.");
        }
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Avaliar")
            return;

        int ticketId = Convert.ToInt32(e.CommandArgument);
        Response.Redirect("/Pages/Sistema/Colaborador/AvaliarChamado/Default.aspx?id=" + ticketId);
    }

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int priority = Convert.IsDBNull(DataBinder.Eval(e.Row.DataItem, "tic_priority")) ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "tic_priority"));
            Label lblBadge = (Label)e.Row.Cells[0].FindControl("lblSlaBadge");
            if (priority == 1)
                lblBadge.Text = "<span class='badge bg-warning text-dark'>Alta</span>";
            else if (priority == 2)
                lblBadge.Text = "<span class='badge bg-danger'>Urgente</span>";
            else
                lblBadge.Text = "<span class='badge bg-secondary'>Normal</span>";
        }

        if (e.Row.Cells[4].Text == "1")
        {
            e.Row.Cells[4].Text = "<i class='text-warning fa fa-clock'></i> ";
        }
        else if (e.Row.Cells[4].Text == "0")
        {
            e.Row.Cells[4].Text = "<i class='text-primary fa fa-spinner'></i>";
        }
        else if (e.Row.Cells[4].Text == "2")
        {
            e.Row.Cells[4].Text = "<i class='text-success fa fa-check'></i>";
        }
    }

    void LoadTickets()
    {
        User user = GetCurrentUser();
        int ID = user.UserId;

        DataSet dsTicket = _ticketService.GetByCollaboratorId(ID);
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
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhum chamado em aberto!</div>";
        }
    }

    void LoadNotification()
    {
        User user = GetCurrentUser();
        int ID = user.UserId;

        DataSet dsNotification = _notificationService.GetByCollaboratorId(ID);
        int qtd = dsNotification.Tables[0].Rows.Count;
        gdvNotification.Visible = false;
        if (qtd > 0)
        {
            gdvNotification.DataSource = dsNotification.Tables[0].DefaultView;
            gdvNotification.DataBind();
            gdvNotification.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvNotification.Visible = true;
        }
        else
        {
            lblTableNullNotification.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhuma notificação em aberto!</div>";
        }
    }

    protected void gdvNotification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "0")
            {
                e.Row.Cells[0].Text = "<i class='text-primary fa fa-spinner'></i>";
            }
            else if (e.Row.Cells[0].Text == "1")
            {
                e.Row.Cells[0].Text = "<i class='text-warning fa fa-clock'></i>";
            }
            else if (e.Row.Cells[0].Text == "2")
            {
                e.Row.Cells[0].Text = "<i class='text-success fa fa-check'></i>";
            }
        }
    }
}
