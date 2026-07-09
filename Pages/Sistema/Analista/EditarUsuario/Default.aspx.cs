using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_EditarUsuario : BasePage
{
    protected override int? RequiredAccessType => 0;

    private readonly UserService _userService = new UserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            User user = _userService.GetById(Convert.ToInt32(Session["USER_SELECT_TABLE"]));
            txtEmail.Text = user.Email;
            txtName.Text = user.Name;
            txtPassword.Text = user.Password;
            txtPosition.Text = user.Position;
            ImgLogado.Text = "<img class='img-thumbnail rounded-circle' src='" + ConfigurationManager.AppSettings["uploadHTTP"] + user.Image + "' style='width:200px; height: 200px;' />";
        }
    }

    protected void btnUpdateUser_Click(object sender, EventArgs e)
    {
        if (!FileUpload1.HasFile) return;

        if (!UploadService.HasValidExtension(FileUpload1.FileName))
        {
            lblMsgUpdateUser.Text = ToastHelper.Warning("Formato de imagem invalido. Use jpg, png, gif ou webp.");
            return;
        }

        if (!UploadService.IsValidSize(FileUpload1.PostedFile.ContentLength))
        {
            lblMsgUpdateUser.Text = ToastHelper.Warning("O tamanho do arquivo de imagem deve ser menor que 1MB.");
            return;
        }

        string file = UploadService.Save(FileUpload1);

        User user = _userService.GetById(Convert.ToInt32(Session["USER_SELECT_TABLE"]));
        user.Email = txtEmail.Text;
        user.Name = txtName.Text;
        user.Password = Function.HashText(txtPassword.Text);
        user.Position = txtPosition.Text;
        user.DepartId = Convert.ToInt32(ddlPositionUser.Text);
        user.Image = file;

        if (_userService.Update(user))
        {
            LogAction("UPDATE_USER", $"Usuario #{user.UserId} atualizado");
            Response.Redirect("/Pages/Sistema/Analista/ListaColaboradores/Default.aspx?from=updateUser");
        }
        else
        {
            lblMsgUpdateUser.Text = ToastHelper.Error("Nao foi possivel Alterar o Usuario!");
        }
    }
}
