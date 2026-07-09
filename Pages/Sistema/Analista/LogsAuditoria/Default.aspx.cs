using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_LogsAuditoria : BasePage
{
    protected override int? RequiredAccessType => 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadActionFilter();
            LoadAudit();
        }
    }

    private void LoadActionFilter()
    {
        foreach (DataRow row in AuditRepository.GetDistinctActions().Tables[0].Rows)
        {
            string action = row["action"].ToString();
            ddlAction.Items.Add(new ListItem(action, action));
        }
    }

    private void LoadAudit()
    {
        DataSet ds = AuditRepository.GetAll();
        gdvAudit.Visible = ds.Tables[0].Rows.Count > 0;

        if (!gdvAudit.Visible)
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhum registro de auditoria.</div>";
            return;
        }

        gdvAudit.DataSource = ds.Tables[0].DefaultView;
        gdvAudit.DataBind();
        if (gdvAudit.HeaderRow != null)
            gdvAudit.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    private void SearchAudit()
    {
        string dateFrom = NormalizeDate(txtDateFrom.Text.Trim(), "00:00:00");
        string dateTo = NormalizeDate(txtDateTo.Text.Trim(), "23:59:59");

        DataSet ds = AuditRepository.Search(
            ddlAction.SelectedValue,
            txtUser.Text.Trim(),
            dateFrom,
            dateTo);

        gdvAudit.Visible = ds.Tables[0].Rows.Count > 0;

        if (!gdvAudit.Visible)
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhum registro encontrado para os filtros informados.</div>";
            return;
        }

        gdvAudit.DataSource = ds.Tables[0].DefaultView;
        gdvAudit.DataBind();
        if (gdvAudit.HeaderRow != null)
            gdvAudit.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        gdvAudit.PageIndex = 0;
        SearchAudit();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlAction.SelectedValue = "";
        txtUser.Text = "";
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        gdvAudit.PageIndex = 0;
        LoadAudit();
    }

    protected void gdvAudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvAudit.PageIndex = e.NewPageIndex;
        if (string.IsNullOrEmpty(txtUser.Text) && string.IsNullOrEmpty(ddlAction.SelectedValue)
            && string.IsNullOrEmpty(txtDateFrom.Text) && string.IsNullOrEmpty(txtDateTo.Text))
            LoadAudit();
        else
            SearchAudit();
    }

    /// <summary>Converts a yyyy-MM-dd value (HTML date input) to dd/MM/yyyy HH:mm:ss used by the DB, or empty.</summary>
    private static string NormalizeDate(string value, string time)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;
        if (DateTime.TryParse(value, out DateTime dt))
            return dt.ToString(@"dd/MM/yyyy") + " " + time;
        return value;
    }
}
