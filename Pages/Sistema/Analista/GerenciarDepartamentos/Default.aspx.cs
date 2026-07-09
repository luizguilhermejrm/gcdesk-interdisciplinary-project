using System;
using System.Web;
using System.Web.UI;

public partial class Pages_Sistema_Analista_GerenciarDepartamentos : BasePage
{
    protected override int? RequiredAccessType => 0;

    private DepartmentRepository _deptRepo = new DepartmentRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadGrid();
    }

    private void LoadGrid()
    {
        gdvDepts.DataSource = _deptRepo.GetAll();
        gdvDepts.DataBind();
    }

    protected void gdvDepts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteDept")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            _deptRepo.Delete(id);
            LogAction("DELETE_DEPARTMENT", $"Departamento excluído: ID {id}");
            LoadGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int id = int.Parse(hiddenDeptId.Value);
        string sector = txtSector.Text.Trim();

        if (string.IsNullOrEmpty(sector))
        {
            lblMsg.Text = "<div class='alert alert-warning'>Informe o nome do departamento.</div>";
            return;
        }

        if (id == 0)
        {
            _deptRepo.Insert(new Department { DepSector = sector });
            LogAction("CREATE_DEPARTMENT", $"Departamento criado: {sector}");
        }
        else
        {
            var dept = _deptRepo.GetById(id);
            if (dept != null)
            {
                dept.DepSector = sector;
                _deptRepo.Update(dept);
                LogAction("UPDATE_DEPARTMENT", $"Departamento alterado: {sector}");
            }
        }

        LoadGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "closeModal",
            "document.querySelector('#modalDept .btn-close').click();", true);
    }
}
