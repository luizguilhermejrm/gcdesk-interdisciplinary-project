using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_Relatorios : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly TicketService _ticketService = new TicketService();
    private readonly DepartmentRepository _departmentRepository = new DepartmentRepository();
    private readonly ReportService _reportService = new ReportService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepartments();
            LoadReport();
        }
    }

    private void LoadDepartments()
    {
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Todos", ""));
        foreach (Department dept in _departmentRepository.GetAll())
            ddlDepartment.Items.Add(new ListItem(dept.DepSector, dept.DepartmentId.ToString()));
    }

    private void LoadReport()
    {
        string status = ddlStatus.SelectedValue;
        string dept = ddlDepartment.SelectedValue;
        DataSet ds = _reportService.GetTicketReport(status, dept);
        gdvReport.DataSource = ds;
        gdvReport.DataBind();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void gdvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvReport.PageIndex = e.NewPageIndex;
        LoadReport();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        LogAction("EXPORT_REPORT", "Relatório de chamados exportado para Excel");
        string status = ddlStatus.SelectedValue;
        string dept = ddlDepartment.SelectedValue;
        DataSet ds = _reportService.GetTicketReport(status, dept);
        string excel = ReportHelper.DataSetToExcel(ds, "Relatorio Chamados");

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=relatorio_chamados.xls");
        Response.Write(excel);
        Response.End();
    }
}
