using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_AnalystTickets_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        User user = (User)Session["USER_BD"];


        if (user != null)
        {
            LoadTickets();
            if (gdvTickets.Rows.Count > 0)
                gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;


        }
        else
        {
            Response.Redirect("../../../Default.aspx");
        }
    }

    void LoadTickets()
    {
        User user = (User)Session["USER_BD"];
        UserBD userBD = new UserBD();
        int ID = user.UserId;

        DataSet dsTicket = TicketBD.SelectTicketAna(ID);
        int qtd = dsTicket.Tables[0].Rows.Count;
        gdvTickets.Visible = false;
        if (qtd > 0)
        {
            gdvTickets.DataSource = dsTicket.Tables[0].DefaultView;
            gdvTickets.DataBind();
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvTickets.Visible = true;
        }
    }

    protected void gdvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lkb = new LinkButton();
            lkb = (LinkButton)e.Row.Cells[5].FindControl("lkbUpdate");
            if (e.Row.Cells[4].Text == "1")
            {

                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "andamento";
                e.Row.Cells[4].Text = "<i class='text-warning fa fa-clock'></i> ";
            }
            else if (e.Row.Cells[4].Text == "2")
            {
                lkb.Text = "<a class='disable' aria-disabled='true'><i class='text-danger fas fa-sync'></i></a>";
                lkb.CommandName = "fechado";
                e.Row.Cells[4].Text = "<i class='text-success fa fa-check'></i>";



            }

        }
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        User user = (User)Session["USER_BD"];
        TicketBD ticketBD = new TicketBD();

        int idTicket = Convert.ToInt32(e.CommandArgument.ToString());
        int status = 0;

        string closeTime = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");

        switch (e.CommandName)
        {
            case "andamento":
                status = 2;
                break;
            case "fechado":
                status = 1;
                break;
            default:
                break;
        }


        lblMsg.Text = @"<div class='toast-container position-absolute top-0 end-0 p-3' id='toastPlacement'>
                                  <div class='toast'>
                                     <div class='toast-header'>
                                        <svg class='bi flex-shrink-0 me-2 text-success' width='24' height='24' role='img' aria-label='Warning: '><use xlink:href='#exclamation-triangle-fill'/></svg>
                                        <strong class='me-auto'>Sucesso!</strong>
                                        <small>Agora</small>
                                      </div>
                                      <div class='toast-body'>
                                        Chamado Finalizado!
                                      </div>
                                   </div>
                                </div> ";

        ticketBD.InsertNotificationStatusFinished(idTicket, closeTime);

        if (TicketBD.UpdateTicket(status, idTicket, closeTime) == 0)
        {
            LoadTickets();
        }


    }
}