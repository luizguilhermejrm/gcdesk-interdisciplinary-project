using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Pages_Sistema_Analista_ListCollaborator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString != null)
        {
            string fromPage = Request.QueryString["from"];

            if (fromPage == "updateUser")
            {
                MensageUpdatedUserToast();
            }
        }
        User user = (User)Session["USER_BD"];
        UserBD userbd = new UserBD();

        if (user != null)
        {
            LoadCollaboratorActive();
            if (gdvCollaboratorActive.Rows.Count > 0)
                gdvCollaboratorActive.HeaderRow.TableSection = TableRowSection.TableHeader;

            LoadCollaboratorInactive();
            if (gdvCollaboratorInactive.Rows.Count > 0)
                gdvCollaboratorInactive.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            Response.Redirect("../Pages/PageError/Error404.aspx");
        }
    }

    public void MensageUpdatedUserToast()
    {
        lblUpdatedUserMsg.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                              <div class='toast'>
                                                 <div class='toast-header'>
                                                    <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                    <strong class='me-auto'>Sucesso!</strong>
                                                    <small>Agora</small>
                                                  </div>
                                                  <div class='toast-body'>
                                                    Usuario Alterado Com sucesso!
                                                  </div>
                                               </div>
                                            </div> ";

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
        bool retorno = false;
        if (str != string.Empty)
        {
            retorno = true;
        }
        return retorno;
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
            lblValidator.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Aviso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                         Há campos vazios que devem ser preenchidos!
                                      </div>
                                   </div>
                                </div> ";
            txtName.Focus();
            txtPosition.Focus();
            txtEmail.Focus();
            txtPassword.Focus();
            ddlPositionUser.Focus();
            FileUpload1.Focus();
            return;
        }
        else
        {
            if (FileUpload1.PostedFile.ContentLength <= 800000)
            {
                string arquivo = FileUpload1.FileName;
                FileUpload1.SaveAs(ConfigurationManager.AppSettings["uploadServer"] + arquivo);

                User user = new User();
                user.Name = txtName.Text;
                user.Position = txtPosition.Text;
                user.Email = txtEmail.Text;
                user.Password = Function.HashText(txtPassword.Text);
                user.DepartId = Convert.ToInt32(ddlPositionUser.Text);
                user.Image = Convert.ToString(FileUpload1.FileName);


                if (UserBD.Insert(user) == 0)
                {
                    lblCdCollaborator.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Aviso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                        Novo Colaborador criado com sucesso.
                                      </div>
                                   </div>
                                </div> ";

                }
                else
                {
                    lblCdCollaborator.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-danger' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Aviso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                        Nao foi possivel criar um novo Colaborador.
                                      </div>
                                   </div>
                                </div> ";
                }

            }
            else
            {

                lblValidator.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Aviso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                         O tamanho do arquivo de imagem deve ser menor!
                                      </div>
                                   </div>
                                </div> ";
            }
        }
    }

    void LoadCollaboratorActive()
    {
        DataSet dsCollaborator = UserBD.SelectAllActive();
        int qtd = dsCollaborator.Tables[0].Rows.Count;
        gdvCollaboratorActive.Visible = false;
        if (qtd > 0)
        {
            gdvCollaboratorActive.DataSource = dsCollaborator.Tables[0].DefaultView;
            gdvCollaboratorActive.DataBind();
            gdvCollaboratorActive.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvCollaboratorActive.Visible = true;
        }
        else
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Colaboradores Ativos!</div>";
        }
    }

    void LoadCollaboratorInactive()
    {
        DataSet dsCollaborator = UserBD.SelectAllInactive();
        int qtd = dsCollaborator.Tables[0].Rows.Count;
        gdvCollaboratorInactive.Visible = false;
        if (qtd > 0)
        {
            gdvCollaboratorInactive.DataSource = dsCollaborator.Tables[0].DefaultView;
            gdvCollaboratorInactive.DataBind();
            gdvCollaboratorInactive.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvCollaboratorInactive.Visible = true;
        }
        else
        {
            lblTableNullInactive.Text = @"<div class='alert alert-info text-center' role='alert'>Sem Colaboradores Inativos!</div>";
        }
    }




    protected void gdvCollaboratorActive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UserId = 0;
        if (e.CommandName == "Atualizar")
        {
            int Userid = Convert.ToInt32(e.CommandArgument);
            Session["USER_SELECT_TABLE"] = Userid;
            Response.Redirect("/Pages/Sistema/Analista/UpdateUser.aspx");
        }
        else if (e.CommandName == "Deletar")
        {
            int Userid = Convert.ToInt32(e.CommandArgument);
            UserBD userbd = new UserBD();
            if (userbd.DeleteUser(Userid))
            {
                lblMsgDeleteUser.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement' style='z-index:999; '>
                                                  <div class='toast'>
                                                     <div class='toast-header'>
                                                        <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                        <strong class='me-auto'>Sucesso!</strong>
                                                        <small>Agora</small>
                                                      </div>
                                                      <div class='toast-body'>
                                                        Usuario Deletado com Sucesso!
                                                      </div>
                                                   </div>
                                                </div> ";

            }
            else
            {
                lblMsgDeleteUser.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement' style='z-index:999; '>
                                                  <div class='toast'>
                                                     <div class='toast-header'>
                                                        <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                        <strong class='me-auto'>Sucesso!</strong>
                                                        <small>Agora</small>
                                                      </div>
                                                      <div class='toast-body'>
                                                        Nao foi possivel deletar o usuario!
                                                      </div>
                                                   </div>
                                                </div> ";
            }
        }
        else
        {
            Response.Redirect("/Pages/Sistema/Analista/ListCollaborator.aspx");

        }
    }

    protected void gdvCollaboratorInactive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Ativar")
        {
            int Userid = Convert.ToInt32(e.CommandArgument);
            UserBD userbd = new UserBD();
            if (userbd.UpdateUserActive(Userid))
            {
                lblMsgDeleteUser.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement' style='z-index:999; '>
                                                  <div class='toast'>
                                                     <div class='toast-header'>
                                                        <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                        <strong class='me-auto'>Sucesso!</strong>
                                                        <small>Agora</small>
                                                      </div>
                                                      <div class='toast-body'>
                                                        Usuario ativado com sucesso!
                                                      </div>
                                                   </div>
                                                </div> ";

            }
            else
            {
                lblMsgDeleteUser.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement' style='z-index:999; '>
                                                  <div class='toast'>
                                                     <div class='toast-header'>
                                                        <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                        <strong class='me-auto'>Erro!</strong>
                                                        <small>Agora</small>
                                                      </div>
                                                      <div class='toast-body'>
                                                        Nao foi possivel ativar o usuario!
                                                      </div>
                                                   </div>
                                                </div> ";
            }
        }
        else
        {
            Response.Redirect("/Pages/Sistema/Analista/ListCollaborator.aspx");

        }
    }
}