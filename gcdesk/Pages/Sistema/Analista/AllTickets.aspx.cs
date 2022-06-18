using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Sistema_Analista_AllTickets_Default : System.Web.UI.Page
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
        DataSet dsTicket = TicketBD.Select();
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
            lkb = (LinkButton)e.Row.Cells[4].FindControl("lkbUpdate");
            if (e.Row.Cells[3].Text == "0" )
            {
                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "aberto";
                e.Row.Cells[3].Text = "<i class='text-danger fa fa-times'></i> ";
            } else if (e.Row.Cells[3].Text == "1")
            {
                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "andamento";
                e.Row.Cells[3].Text = "<i class='text-warning fa fa-clock'></i> ";
            }
            else if (e.Row.Cells[3].Text == "2") 
            {
                lkb.Text = "<i class='text-info fas fa-sync'></i>";
                lkb.CommandName = "fechado";
                e.Row.Cells[3].Text = "<i class='text-success fa fa-check'></i>";
            }
        }
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int codigoTicket = Convert.ToInt32(e.CommandArgument.ToString());
        int status = 0;

        switch (e.CommandName)
        {
            case "aberto":
                status = 1;
                break;
            case "andamento":
                status = 2;
                break;
            case "fechado":
                status = 0;
                break;
            default:
                break;
        }

        if (TicketBD.UpdateTicket(status, codigoTicket) == 0)
        {
            LoadTickets();
        }

    }

}