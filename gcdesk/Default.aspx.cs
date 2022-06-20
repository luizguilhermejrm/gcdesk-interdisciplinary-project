using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if(Request.QueryString!=null)
        {
            string fromPage = Request.QueryString["from"];
            if (fromPage == "logout")
            {
                MensageExitToast();
            }
        }
       
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


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = Function.HashText(txtPassword.Text.Trim());

        if (!IsComplete(email))
        {

            lblMsg.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                              <div class='toast'>
                                 <div class='toast-header'>
                                    <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                    <strong class='me-auto'>Aviso!</strong>
                                    <small>Agora</small>
                                  </div>
                                  <div class='toast-body'>
                                    Preencha o e-mail e a senha corretamente.
                                  </div>
                               </div>
                            </div> ";
            txtEmail.Focus();
            return;
        }

        if (!IsComplete(password))
        {
            lblMsg.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                              <div class='toast'>
                                 <div class='toast-header'>
                                    <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                    <strong class='me-auto'>Aviso!</strong>
                                    <small>Agora</small>
                                  </div>
                                  <div class='toast-body'>
                                    Preencha o e-mail e a senha corretamente.
                                  </div>
                               </div>
                            </div> ";
            txtPassword.Focus();
            return;
        }

        UserBD bd = new UserBD();
        User user = new User();
        user = bd.Authenticate(email, password);

        if (user != null)
        {
            Session["USER_BD"] = user;
            switch (user.TypeAccess)
            {
                case 0:
                    Response.Redirect("Pages/Sistema/Analista/Index.aspx");
                    break;

                case 1:
                    Response.Redirect("Pages/Sistema/Colaborador/Index.aspx");
                    break;

                default:
                    Response.Redirect("Default.aspx");
                    lblMsg.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                      <div class='toast'>
                                         <div class='toast-header'>
                                            <svg class='bi flex-shrink-0 me-2 text-warning' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                            <strong class='me-auto'>Aviso!</strong>
                                            <small>Agora</small>
                                          </div>
                                          <div class='toast-body'>
                                            Usuário não corresponde as credenciais necessárias!
                                          </div>
                                       </div>
                                    </div> ";
                    break;
            }

        }
        else
        {
            lblMsg.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                              <div class='toast'>
                                 <div class='toast-header'>
                                    <svg class='bi flex-shrink-0 me-2 text-danger' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                    <strong class='me-auto'>Aviso!</strong>
                                    <small>Agora</small>
                                  </div>
                                  <div class='toast-body'>
                                    Usuário não existe!
                                  </div>
                             </div>
                           </div> ";
        }
    }


    public void MensageExitToast()
    {

        lblExit.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                              <div class='toast'>
                                                 <div class='toast-header'>
                                                    <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                    <strong class='me-auto'>Sucesso!</strong>
                                                    <small>Agora</small>
                                                  </div>
                                                  <div class='toast-body'>
                                                    Logout realizado!
                                                  </div>
                                               </div>
                                            </div> ";
        

    }


}