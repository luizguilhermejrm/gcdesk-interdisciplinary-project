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
        DataSet dsTicket = TicketBD.SelectTicketAllAnalyst();
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
        if (e.Row.Cells[5].Text == "1")
        {
            e.Row.Cells[5].Text = "<i class='text-warning fa fa-clock'></i> ";
        }
        else if (e.Row.Cells[5].Text == "0")
        {
            e.Row.Cells[5].Text = "<i class='text-primary fa fa-spinner'></i>";
        }
        else if (e.Row.Cells[5].Text == "2")
        {
            e.Row.Cells[5].Text = "<i class='text-success fa fa-check'></i>";
        }
    }

}