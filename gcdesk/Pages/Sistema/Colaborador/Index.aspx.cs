using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Sistema_Colaborador_Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        User user = (User)Session["USER_BD"];
        TicketBD Tbd = new TicketBD();

        if (user != null)
        {
            txtData.Text = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
            txtData.Attributes.Add("readonly", "true");


            LoadTickets();
            LoadNotification();
            if (gdvTickets.Rows.Count > 0)
                gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;

            int ID = user.UserId;
            lblFinishedCalls.Text = Convert.ToString(Tbd.SelectFinished(ID));
            lblProgressCalls.Text = Convert.ToString(Tbd.SelectProgress(ID));
            lblOpenCalls.Text = Convert.ToString(Tbd.SelectOpen(ID));
        }else
        {
            Response.Redirect("../Pages/PageError/Error404.aspx");
        }

        

    }
    protected void btn_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {});" +
           "document.onreadystatechange = function () {" +
           " myModal.show();" +
           "};", true);
    }

    protected void btnTicket_Click(object sender, EventArgs e)
    {
        User user = (User)Session["USER_BD"];
        int ID = user.UserId;
        NotificationBD notificationBD = new NotificationBD();

        Ticket tic = new Ticket();
        tic.Description = txtProblem.Text;
        tic.TypeTicket = ddlTypeTicket.Text;
        tic.Localization = txtLocal.Text;
        tic.OpenTime = txtData.Text;
        tic.UserId = ID;

        if (TicketBD.Insert(tic) == 0)
        {
            string timeMensagem = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
            notificationBD.InsertNotificationStatusCreate(timeMensagem);
            

            lblCdTicket.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Aviso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                        Chamado criado com sucesso.
                                      </div>
                                   </div>
                                </div> ";

        }
        else
        {
            lblCdTicket.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-danger' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Aviso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                        Nao foi possivel criar o Chamado.
                                      </div>
                                   </div>
                                </div> ";

        }
    }

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.Cells[3].Text == "1")
        {
            e.Row.Cells[3].Text = "<i class='text-warning fa fa-clock'></i> ";
        }
        else if (e.Row.Cells[3].Text == "0")
        {
            e.Row.Cells[3].Text = "<i class='text-primary fa fa-spinner'></i>";
        }
        else if (e.Row.Cells[3].Text == "2")
        {
            e.Row.Cells[3].Text = "<i class='text-success fa fa-check'></i>";
        }
    }

    void LoadTickets()
    {
        User user = (User)Session["USER_BD"];
        UserBD userBD = new UserBD();
        int ID = user.UserId;

        DataSet dsTicket = TicketBD.SelectTicketCollaborator(ID);
        int qtd = dsTicket.Tables[0].Rows.Count;
        gdvTickets.Visible = false;
        if (qtd > 0)
        {
            gdvTickets.DataSource = dsTicket.Tables[0].DefaultView;
            gdvTickets.DataBind();
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvTickets.Visible = true;
        }
        else
        {
            lblTableNull.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhum chamado em aberto!</div>";
        }
    }
    void LoadNotification()
    {
        User user = (User)Session["USER_BD"];
        UserBD userBD = new UserBD();
        int ID = user.UserId;

        DataSet dsNotification = NotificationBD.SelectNotification(ID);
        int qtd = dsNotification.Tables[0].Rows.Count;
        gdvNotification.Visible = false;
        if (qtd > 0)
        {
            gdvNotification.DataSource = dsNotification.Tables[0].DefaultView;
            gdvNotification.DataBind();
            gdvNotification.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvNotification.Visible = true;
        }
        else
        {
            lblTableNullNotification.Text = @"<div class='alert alert-info text-center' role='alert'>Nenhuma notificação em aberto!</div>";
        }
    }

    protected void gdvNotification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "0")
            {
                e.Row.Cells[0].Text = "<i class='text-primary fa fa-spinner'></i>";
            }
            else if (e.Row.Cells[0].Text == "1")
            {
                e.Row.Cells[0].Text = "<i class='text-warning fa fa-clock'></i>";

            }
            else if (e.Row.Cells[0].Text == "2")
            {
                e.Row.Cells[0].Text = "<i class='text-success fa fa-check'></i>";
            }
        }
    }
}
