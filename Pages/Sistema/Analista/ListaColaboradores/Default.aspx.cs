using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_ListaColaboradores : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly UserService _userService = new UserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString?["from"] == "updateUser")
            MensageUpdatedUserToast();

        LoadCollaboratorActive();
        LoadCollaboratorInactive();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "script",
            "var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {});" +
            "document.onreadystatechange = function () { myModal.show(); };", true);
    }

    public void MensageUpdatedUserToast()
    {
        lblUpdatedUserMsg.Text = ToastHelper.Success("Usuario Alterado Com sucesso!");
    }

    private static bool IsComplete(string str)
    {
        return str != string.Empty;
    }

    protected void btnCollaborator_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string position = txtPosition.Text.Trim();
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text.Trim();
        string department = ddlPositionUser.Text.Trim();
        string file = FileUpload1.FileName.Trim();

        if (!IsComplete(name) || !IsComplete(position) || !IsComplete(email) || !IsComplete(password) || !IsComplete(department) || !IsComplete(file))
        {
            lblValidator.Text = ToastHelper.Warning("Há campos vazios que devem ser preenchidos!");
            return;
        }

        if (!UploadService.HasValidExtension(FileUpload1.FileName))
        {
            lblValidator.Text = ToastHelper.Warning("Formato de imagem invalido. Use jpg, png, gif ou webp.");
            return;
        }

        if (!UploadService.IsValidSize(FileUpload1.PostedFile.ContentLength))
        {
            lblValidator.Text = ToastHelper.Warning("O tamanho do arquivo de imagem deve ser menor!");
            return;
        }

        string arquivo = UploadService.Save(FileUpload1);

        User user = new User();
        user.Name = txtName.Text;
        user.Position = txtPosition.Text;
        user.Email = txtEmail.Text;
        user.DepartId = Convert.ToInt32(ddlPositionUser.Text);
        user.Image = arquivo;

        if (_userService.Create(user, txtPassword.Text) == 0)
        {
            LogAction("CREATE_COLLABORATOR", $"Colaborador criado: {user.Email}");
            lblCdCollaborator.Text = ToastHelper.Success("Novo Colaborador criado com sucesso.");
            LoadCollaboratorActive();
            LoadCollaboratorInactive();
        }
        else
            lblCdCollaborator.Text = ToastHelper.Error("Não foi possível criar um novo Colaborador.");
    }

    void LoadCollaboratorActive()
    {
        DataSet dsCollaborator = _userService.GetAllActive();
        gdvCollaboratorActive.Visible = dsCollaborator.Tables[0].Rows.Count > 0;

        if (!gdvCollaboratorActive.Visible)
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Colaboradores Ativos!</div>";
            return;
        }

        gdvCollaboratorActive.DataSource = dsCollaborator.Tables[0].DefaultView;
        gdvCollaboratorActive.DataBind();
        gdvCollaboratorActive.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    void LoadCollaboratorInactive()
    {
        DataSet dsCollaborator = _userService.GetAllInactive();
        gdvCollaboratorInactive.Visible = dsCollaborator.Tables[0].Rows.Count > 0;

        if (!gdvCollaboratorInactive.Visible)
        {
            lblTableNullInactive.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Colaboradores Inativos!</div>";
            return;
        }

        gdvCollaboratorInactive.DataSource = dsCollaborator.Tables[0].DefaultView;
        gdvCollaboratorInactive.DataBind();
        gdvCollaboratorInactive.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvCollaboratorActive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Atualizar")
        {
            int Userid = Convert.ToInt32(e.CommandArgument);
            Session["USER_SELECT_TABLE"] = Userid;
            Response.Redirect("/Pages/Sistema/Analista/EditarUsuario/Default.aspx");
            return;
        }

        if (e.CommandName != "Deletar")
            return;

        int userId = Convert.ToInt32(e.CommandArgument);
        lblMsgDeleteUser.Text = _userService.Deactivate(userId)
            ? ToastHelper.Success("Usuario Deletado com Sucesso!")
            : ToastHelper.Warning("Não foi possível deletar o usuário!");
        LogAction("DEACTIVATE_USER", $"Usuario #{userId} desativado");
        LoadCollaboratorActive();
        LoadCollaboratorInactive();
    }

    protected void gdvCollaboratorInactive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Ativar")
            return;

        int userId = Convert.ToInt32(e.CommandArgument);
        lblMsgDeleteUser.Text = _userService.Activate(userId)
            ? ToastHelper.Success("Usuario ativado com sucesso!")
            : ToastHelper.Warning("Não foi possível ativar o usuário!");
        LogAction("ACTIVATE_USER", $"Usuario #{userId} ativado");
        LoadCollaboratorActive();
        LoadCollaboratorInactive();
    }
}
