using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_Lixeira : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly TrashService _trashService = new TrashService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadAll();
    }

    private void LoadAll()
    {
        LoadDeletedTickets();
        LoadDeletedUsers();
        LoadDeletedDepartments();
        LoadDeletedEquipment();
        LoadDeletedServices();
        LoadDeletedNotifications();
    }

    private void LoadDeletedTickets()
    {
        DataSet ds = LixeiraRepository.GetDeletedTickets();
        gdvTickets.Visible = ds.Tables[0].Rows.Count > 0;
        if (!gdvTickets.Visible)
            lblNullTickets.Text = "<div class='alert alert-info'>Nenhum chamado na lixeira.</div>";
        else
        {
            gdvTickets.DataSource = ds.Tables[0].DefaultView;
            gdvTickets.DataBind();
        }
    }

    private void LoadDeletedUsers()
    {
        DataSet ds = LixeiraRepository.GetDeletedUsers();
        gdvUsers.Visible = ds.Tables[0].Rows.Count > 0;
        if (!gdvUsers.Visible)
            lblNullUsers.Text = "<div class='alert alert-info'>Nenhum usuário na lixeira.</div>";
        else
        {
            gdvUsers.DataSource = ds.Tables[0].DefaultView;
            gdvUsers.DataBind();
        }
    }

    private void LoadDeletedDepartments()
    {
        DataSet ds = LixeiraRepository.GetDeletedDepartments();
        gdvDepartments.Visible = ds.Tables[0].Rows.Count > 0;
        if (!gdvDepartments.Visible)
            lblNullDepts.Text = "<div class='alert alert-info'>Nenhum departamento na lixeira.</div>";
        else
        {
            gdvDepartments.DataSource = ds.Tables[0].DefaultView;
            gdvDepartments.DataBind();
        }
    }

    private void LoadDeletedEquipment()
    {
        DataSet ds = LixeiraRepository.GetDeletedEquipment();
        gdvEquipment.Visible = ds.Tables[0].Rows.Count > 0;
        if (!gdvEquipment.Visible)
            lblNullEquip.Text = "<div class='alert alert-info'>Nenhum equipamento na lixeira.</div>";
        else
        {
            gdvEquipment.DataSource = ds.Tables[0].DefaultView;
            gdvEquipment.DataBind();
        }
    }

    private void LoadDeletedServices()
    {
        DataSet ds = LixeiraRepository.GetDeletedServices();
        gdvServices.Visible = ds.Tables[0].Rows.Count > 0;
        if (!gdvServices.Visible)
            lblNullServices.Text = "<div class='alert alert-info'>Nenhum serviço na lixeira.</div>";
        else
        {
            gdvServices.DataSource = ds.Tables[0].DefaultView;
            gdvServices.DataBind();
        }
    }

    private void LoadDeletedNotifications()
    {
        DataSet ds = LixeiraRepository.GetDeletedNotifications();
        gdvNotifications.Visible = ds.Tables[0].Rows.Count > 0;
        if (!gdvNotifications.Visible)
            lblNullNotif.Text = "<div class='alert alert-info'>Nenhuma notificação na lixeira.</div>";
        else
        {
            gdvNotifications.DataSource = ds.Tables[0].DefaultView;
            gdvNotifications.DataBind();
        }
    }

    private void Restore(string table, string pkName, int id)
    {
        _trashService.Restore(table, pkName, id);
        LogAction("RESTORE", $"{table}#{id} restaurado");
        lblMsg.Text = ToastHelper.Success("Registro restaurado com sucesso!");
        LoadAll();
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
            Restore("ticket", "tic_id", Convert.ToInt32(e.CommandArgument));
    }

    protected void gdvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
            Restore("user", "user_id", Convert.ToInt32(e.CommandArgument));
    }

    protected void gdvDepartments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
            Restore("department", "dep_id", Convert.ToInt32(e.CommandArgument));
    }

    protected void gdvEquipment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
            Restore("equipment", "equip_id", Convert.ToInt32(e.CommandArgument));
    }

    protected void gdvServices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
            Restore("service", "service_id", Convert.ToInt32(e.CommandArgument));
    }

    protected void gdvNotifications_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Restore")
            Restore("notification", "not_id", Convert.ToInt32(e.CommandArgument));
    }
}
