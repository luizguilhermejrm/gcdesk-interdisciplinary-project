using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Pages_Sistema_Analista_UpdateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserBD userbd = new UserBD();
            User user = userbd.SelectUserTable(Convert.ToInt32(Session["USER_SELECT_TABLE"]));
            txtEmail.Text = user.Email;
            txtName.Text = user.Name;
            txtPassword.Text = user.Password;
            txtPosition.Text = user.Position;
            ImgLogado.Text = "<img class='img-thumbnail rounded-circle' src='" + ConfigurationManager.AppSettings["uploadHTTP"] + user.Image + "' style='width:200px; height: 200px;' />";
        }
    }

    protected void btnUpdateUser_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            if (FileUpload1.PostedFile.ContentLength <= 1024000)
            {
                string file = FileUpload1.FileName;
                FileUpload1.SaveAs(ConfigurationManager.AppSettings["uploadServer"] + file);

                UserBD userBD = new UserBD();
                User user = userBD.SelectUserTable(Convert.ToInt32(Session["USER_SELECT_TABLE"]));
                user.Email = txtEmail.Text;
                user.Name = txtName.Text;
                user.Password = Function.HashText(txtPassword.Text);
                user.Position = txtPosition.Text;
                user.DepartId = Convert.ToInt32(ddlPositionUser.Text);
                user.Image = Convert.ToString(FileUpload1.FileName);

                if (userBD.UpdateUser(user))
                {

                    Response.Redirect("/Pages/Sistema/Analista/ListCollaborator.aspx?from=updateUser");
                }
                else
                {
                    lblMsgUpdateUser.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement' style='z-index:999; '>
                                              <div class='toast'>
                                                 <div class='toast-header'>
                                                    <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                    <strong class='me-auto'>Erro!</strong>
                                                    <small>Agora</small>
                                                  </div>
                                                  <div class='toast-body'>
                                                    Nao foi possivel Alterar o Usuario!
                                                  </div>
                                               </div>
                                            </div> ";
                }
            }
        }

    }
}