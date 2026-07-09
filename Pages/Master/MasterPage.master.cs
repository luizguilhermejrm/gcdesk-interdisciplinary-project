using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class Pages_Master_MasterPage : System.Web.UI.MasterPage
{
    private readonly UserService _userService = new UserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        User user = Session["USER_BD"] as User;
        if (user == null)
        {
            Response.Redirect("../PageError/Error404.aspx");
            return;
        }

        lblLogado.Text = user.Name;
        lblEmail.Text = user.Email;

        if (!IsPostBack)
            SetupPage(user);
    }

    private void SetupPage(User user)
    {
        if (user.StatusUser != 1)
        {
            Response.Redirect("../../../Default.aspx?from=accessdenied");
            return;
        }

        ShowFirstLoginModal(user);
        ImgLogado.Text = $"<img src='{ConfigurationManager.AppSettings["uploadHTTP"]}{user.Image}' style='width:50px' />";
        lblLogadoType.Text = user.TypeAccess == 0 ? "Analista" : "Colaborador";
        lblNavMenu.Text = user.TypeAccess == 0 ? NavAnalyst() : NavCollaborator();
    }

    private void ShowFirstLoginModal(User user)
    {
        if (user.FirstLogin != 0)
            return;

        string script = @"var myModal = new bootstrap.Modal(document.getElementById('ModalFirstLogin'), {});
                          document.onreadystatechange = function () {
                            myModal.show();
                          };";
        Page.ClientScript.RegisterStartupScript(GetType(), "script", script, true);
    }

    private static string NavAnalyst()
    {
        return @"<div class='nav rounded bg-primary m-3'>
                  <a class='nav-link p-2' href='/Pages/Sistema/Analista/Index/Default.aspx'>
                     <div class='sb-nav-link-icon'><i class='fa-solid fa-chart-line mx-2 fs-6 text-light'></i></div>
                     <span class='text-light'><b>Dashboard</b></span>
                  </a>
                </div>
                <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                  <a class='nav-link p-2' href='/Pages/Sistema/Analista/MeusChamados/Default.aspx'>
                     <div class='sb-nav-link-icon'><i class='fa-solid fa-headset mx-2 fs-6' style='color: #3381E2'></i></div>
                     <span style='color: #3381E2'><b>Meus Chamados</b></span>
                  </a>
                </div>
                <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                  <a class='nav-link p-2' href='/Pages/Sistema/Analista/TodosChamados/Default.aspx'>
                     <div class='sb-nav-link-icon'><i class='fa-solid fa-message mx-2 fs-6' style='color: #3381E2'></i></div>
                     <span style='color: #3381E2'><b>Todos Chamados</b></span>
                  </a>
                </div>
            <div class='nav rounded m-3' style='background-color: #F5F9FF'>
              <a class='nav-link p-2' href='/Pages/Sistema/Analista/ListaColaboradores/Default.aspx'>
                 <div class='sb-nav-link-icon'><i class='fa-solid fa-table mx-2 fs-6' style='color: #3381E2'></i></div>
                 <span style='color: #3381E2; font-size:15px'><b>Tab. Colaboradores</b></span>
              </a>
            </div>
            <div class='nav rounded m-3' style='background-color: #F5F9FF'>
              <a class='nav-link p-2' href='/Pages/Sistema/Analista/Relatorios/Default.aspx'>
                 <div class='sb-nav-link-icon'><i class='fa-solid fa-file-export mx-2 fs-6' style='color: #3381E2'></i></div>
                 <span style='color: #3381E2; font-size:15px'><b>Relatórios</b></span>
               </a>
             </div>
             <div class='nav rounded m-3' style='background-color: #F5F9FF'>
               <a class='nav-link p-2' href='/Pages/Sistema/Analista/LogsAuditoria/Default.aspx'>
                  <div class='sb-nav-link-icon'><i class='fa-solid fa-clipboard-list mx-2 fs-6' style='color: #3381E2'></i></div>
                  <span style='color: #3381E2; font-size:15px'><b>Logs de Auditoria</b></span>
               </a>
             </div>
             <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                <a class='nav-link p-2' href='/Pages/Sistema/Analista/GerenciarUsuarios/Default.aspx'>
                   <div class='sb-nav-link-icon'><i class='fa-solid fa-users-gear mx-2 fs-6' style='color: #3381E2'></i></div>
                   <span style='color: #3381E2; font-size:15px'><b>Gerenciar Usuários</b></span>
                </a>
              </div>
              <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                <a class='nav-link p-2' href='/Pages/Sistema/Analista/GerenciarDepartamentos/Default.aspx'>
                   <div class='sb-nav-link-icon'><i class='fa-solid fa-building mx-2 fs-6' style='color: #3381E2'></i></div>
                   <span style='color: #3381E2; font-size:15px'><b>Departamentos</b></span>
                </a>
              </div>
              <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                <a class='nav-link p-2' href='/Pages/Sistema/Analista/GerenciarEquipamentos/Default.aspx'>
                   <div class='sb-nav-link-icon'><i class='fa-solid fa-desktop mx-2 fs-6' style='color: #3381E2'></i></div>
                   <span style='color: #3381E2; font-size:15px'><b>Equipamentos</b></span>
                </a>
              </div>
              <div class='nav rounded m-3' style='background-color: #F5F9FF'>
                <a class='nav-link p-2' href='/Pages/Sistema/Analista/Lixeira/Default.aspx'>
                  <div class='sb-nav-link-icon'><i class='fa-solid fa-trash-can mx-2 fs-6' style='color: #3381E2'></i></div>
                  <span style='color: #3381E2; font-size:15px'><b>Lixeira</b></span>
               </a>
             </div>";
    }

    private static string NavCollaborator()
    {
        return @"<div class='nav rounded bg-primary m-3'>
                  <a class='nav-link p-2' href='/Pages/Sistema/Colaborador/Index/Default.aspx'>
                     <div class='sb-nav-link-icon'><i class='fa-solid fa-chart-line mx-2 fs-6 text-light'></i></div>
                     <span class='text-light'><b>Dashboard</b></span>
                  </a>
                </div>";
    }

    protected void btnUpdateFirstLogin_Click(object sender, EventArgs e)
    {
        User user = Session["USER_BD"] as User;

        string newPassword = Function.HashText(txtPassword.Text);
        string retypePassword = Function.HashText(txtRetypePassword.Text);

        if (newPassword != retypePassword)
        {
            lblMsgFirstLogin.Text = ToastHelper.Error("Senhas não conferem!");
            return;
        }

        if (_userService.UpdatePassword(user.UserId, txtPassword.Text))
            lblMsgFirstLogin.Text = ToastHelper.Success("Senha Alterada com Sucesso!");
        else
            lblMsgFirstLogin.Text = ToastHelper.Error("ERRO ao alterar senha!");
    }

    protected void lbExit_click(object sender, EventArgs e)
    {
        User user = Session["USER_BD"] as User;
        if (user != null)
            AuditService.Log(user.UserId.ToString(), "LOGOUT", "User initiated logout");

        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~/Default.aspx?from=logout", false);
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}
