using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_GerenciarUsuarios : BasePage
{
    protected override int? RequiredAccessType => 0;

    private UserRepository _userRepo = new UserRepository();
    private DepartmentRepository _deptRepo = new DepartmentRepository();
    private UserService _userSvc = new UserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepartments();
            LoadGrid();
        }
    }

    private void LoadDepartments()
    {
        var depts = _deptRepo.GetAll();
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("-- Selecione --", "0"));
        foreach (var dept in depts)
            ddlDepartment.Items.Add(new ListItem(dept.DepSector, dept.DepartmentId.ToString()));
    }

    private void LoadGrid()
    {
        gdvUsers.DataSource = _userRepo.GetAll();
        gdvUsers.DataBind();
    }

    protected void gdvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteUser")
        {
            int userId = Convert.ToInt32(e.CommandArgument);
            _userRepo.SoftDelete(userId);
            LogAction("DELETE_USER", $"Usuário #{userId} excluído");
            LoadGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int userId = int.Parse(hiddenUserId.Value);
        string name = txtName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string position = txtPosition.Text.Trim();
        int typeAccess = int.Parse(ddlType.SelectedValue);
        int departId = int.Parse(ddlDepartment.SelectedValue);
        string password = txtPassword.Text.Trim();

        if (departId == 0)
        {
            lblMsg.Text = "<div class='alert alert-warning'>Selecione um departamento.</div>";
            return;
        }

        if (userId == 0)
        {
            if (string.IsNullOrEmpty(password))
            {
                lblMsg.Text = "<div class='alert alert-warning'>A senha é obrigatória para novos usuários.</div>";
                return;
            }
            User u = new User
            {
                Name = name,
                Email = email,
                Position = position,
                TypeAccess = typeAccess,
                DepartId = departId,
                StatusUser = 1
            };
            _userSvc.Create(u, password);
            LogAction("CREATE_USER", $"Usuário criado: {email}");
        }
        else
        {
            User u = _userRepo.GetById(userId);
            if (u == null) return;
            u.Name = name;
            u.Email = email;
            u.Position = position;
            u.TypeAccess = typeAccess;
            u.DepartId = departId;
            if (!string.IsNullOrEmpty(password))
                u.Password = Function.HashText(password);
            _userRepo.Update(u);
            LogAction("UPDATE_USER", $"Usuário #{userId} atualizado: {email}");
        }

        LoadGrid();
        ScriptManager.RegisterStartupScript(this, GetType(), "closeModal",
            "document.querySelector('#modalUser .btn-close').click();", true);
    }
}
