using System;
using System.Configuration;

public partial class Pages_Sistema_Perfil : BasePage
{
    protected override int? RequiredAccessType => null;

    private readonly UserService _userService = new UserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadProfile();
    }

    private void LoadProfile()
    {
        User user = GetCurrentUser();
        if (user == null) return;

        txtName.Text = user.Name;
        txtEmail.Text = user.Email;
        txtPosition.Text = user.Position;
        lblName.Text = user.Name;
        lblType.Text = user.TypeAccess == 0 ? "Analista" : "Colaborador";

        SetProfileImage(user.Image, user.Name);
    }

    private void SetProfileImage(string image, string name)
    {
        string imgUrl = ConfigurationManager.AppSettings["uploadHTTP"] + image;
        imgProfile.ImageUrl = imgUrl;
        imgProfile.Attributes["onerror"] = $"this.src='https://ui-avatars.com/api/?name={name}'";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        User user = GetCurrentUser();

        user.Name = txtName.Text.Trim();
        user.Email = txtEmail.Text.Trim();
        user.Position = txtPosition.Text.Trim();

        if (fileUpload.HasFile)
        {
            if (!UploadService.HasValidExtension(fileUpload.FileName))
            {
                lblMsg.Text = ToastHelper.Error("Formato de imagem inválido. Use jpg, jpeg, png, gif ou webp.");
                return;
            }
            if (!UploadService.IsValidSize(fileUpload.PostedFile.ContentLength))
            {
                lblMsg.Text = ToastHelper.Error("Arquivo muito grande. Máximo de 1MB.");
                return;
            }
            user.Image = UploadService.Save(fileUpload);
        }

        string password = txtPassword.Text.Trim();
        if (!string.IsNullOrEmpty(password))
            user.Password = password;

        if (_userService.UpdateProfile(user))
        {
            Session["USER_BD"] = user;
            lblMsg.Text = ToastHelper.Success("Perfil atualizado com sucesso!");
            LogAction("UPDATE_PROFILE");
            if (!string.IsNullOrEmpty(user.Image))
                SetProfileImage(user.Image, user.Name);
        }
        else
        {
            lblMsg.Text = ToastHelper.Error("Erro ao atualizar perfil.");
        }
    }
}
