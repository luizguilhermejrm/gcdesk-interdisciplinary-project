using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Master_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        User user = (User)Session["USER_BD"];
        UserBD userBD = new UserBD();   

        if (user != null)
        {
            int ID = user.UserId;
      
           string nomeUsuario = Convert.ToString(userBD.SelectNavbarUser(ID));
            lblLogado.Text = nomeUsuario;

            if (user.TypeAccess == 0)
            {
            lblLogadoType.Text = "Analista";
            } else
            {
               lblLogadoType.Text = "Colaborador";
            }

            if (!IsPostBack)
            {
                if (user.FirstLogin == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "var myModal = new bootstrap.Modal(document.getElementById('ModalFirstLogin'), {});" +
                          "document.onreadystatechange = function () {" +
                          " myModal.show();" +
                          "};", true);
                }
            }

            if (IsAnalisty(user.TypeAccess))
            {
                lblNavMenu.Text = @"<div class='nav rounded bg-primary m-3'>
                                      <a class='nav-link p-2' href='/Pages/Sistema/Analista/Index.aspx'>
                                         <div class='sb-nav-link-icon'><i class='fa-solid fa-chart-line mx-2 fs-6 text-light'></i></div>
                                         <span class='text-light'><b>Dashboard</b></span>
                                      </a>
                                    </div>
                                    <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                                      <a class='nav-link p-2' href='/Pages/Sistema/Analista/AnalystTickets.aspx'>
                                         <div class='sb-nav-link-icon'><i class='fa-solid fa-headset mx-2 fs-6' style='color: #3381E2'></i></div>
                                         <span style='color: #3381E2'><b>Meus Chamados</b></span>
                                      </a>
                                    </div>
                                    <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                                      <a class='nav-link p-2' href='/Pages/Sistema/Analista/AllTickets.aspx'>
                                         <div class='sb-nav-link-icon'><i class='fa-solid fa-message mx-2 fs-6' style='color: #3381E2'></i></div>
                                         <span style='color: #3381E2'><b>Todos Chamados</b></span>
                                      </a>
                                    </div>
                                    <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                                      <a class='nav-link p-2' href='/Pages/Sistema/Analista/ListCollaborator.aspx'>
                                         <div class='sb-nav-link-icon'><i class='fa-solid fa-table mx-2 fs-6' style='color: #3381E2'></i></div>
                                         <span style='color: #3381E2; font-size:15px'><b>Tab. Colaboradores</b></span>
                                      </a>
                                    </div>";
            }
            else if (!IsAnalisty(user.TypeAccess))
            {
                lblNavMenu.Text = @"<div class='nav rounded bg-primary m-3'>
                                      <a class='nav-link p-2' href='/Pages/Sistema/Colaborador/Index.aspx'>
                                         <div class='sb-nav-link-icon'><i class='fa-solid fa-chart-line mx-2 fs-6 text-light'></i></div>
                                         <span class='text-light'><b>Dashboard</b></span>
                                      </a>
                                    </div>";
            }
            else
            {
                Response.Redirect("../PageError/Error404.aspx");
            }


        }else
        {
            Response.Redirect("../PageError/Error404.aspx");
        }


    }

    private bool IsAnalisty(int tipo)
    {
        bool retorno = false;
        if (tipo == 0)
        {
            retorno = true;
        }
        return retorno;
    }

    protected void btnUpdateFirstLogin_Click(object sender, EventArgs e)
    {
        UserBD userBd = new UserBD();
        User user = (User)Session["USER_BD"];

        user.Email = txtEmail.Text;
        user.Password = Function.HashText(txtPassword.Text);
        user.RetypePassword = Function.HashText(txtRetypePassword.Text);


        if (user.Password == user.RetypePassword)
        {

            if (UserBD.UpdateUserFirstLogin(user) == 0)
            {
                lblMsgFirstLogin.Text = @"<div class='toast-container position-absolute mt-5 end-0 p-3' id='toastPlacement' style='z-index:999; '>
                                              <div class='toast'>
                                                 <div class='toast-header'>
                                                    <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                    <strong class='me-auto'>Aviso!</strong>
                                                    <small>Agora</small>
                                                  </div>
                                                  <div class='toast-body'>
                                                    Senha Alterada com Sucesso!
                                                  </div>
                                               </div>
                                            </div> ";
            }
            else
            {
                lblMsgFirstLogin.Text = @"<div class='toast-container position-absolute mt-5 end-0 p-3' id='toastPlacement' style='z-index:999;'>
                                              <div class='toast'>
                                                 <div class='toast-header'>
                                                    <svg class='bi flex-shrink-0 me-2 text-danger' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                    <strong class='me-auto'>Aviso!</strong>
                                                    <small>Agora</small>
                                                  </div>
                                                  <div class='toast-body'>
                                                    ERRRRRRRRRRRRRRRO!
                                                  </div>
                                               </div>
                                            </div> ";
            }
        }
        else
        {
            lblMsgFirstLogin.Text = @"<div class='toast-container position-absolute mt-5 end-0 p-3' id='toastPlacement' style='z-index:999;'>
                                              <div class='toast'>
                                                 <div class='toast-header'>
                                                    <svg class='bi flex-shrink-0 me-2 text-danger' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                                    <strong class='me-auto'>Aviso!</strong>
                                                    <small>Agora</small>
                                                  </div>
                                                  <div class='toast-body'>
                                                    Senhas nao conhecidem!
                                                  </div>
                                               </div>
                                            </div> ";
        }
    }

    protected void lbExit_click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("../../../Default.aspx?from=logout");

       
    }

}
