using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Sistema_Analista_Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Session["ID"]);
        UserBD bd = new UserBD();
        User user = bd.Select(id);

        if (!IsAnalisty(user.TypeAccess))
        {
            Response.Redirect("../../Default.aspx");
        }
        else
        {
            lblTitle.Text = user.Email;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script>$(window).on('load', function () { $('#myModal').modal('show');});</ script > ", false);
        }

        LoadTickets();
        if (gdvTickets.Rows.Count > 0)
            gdvTickets.HeaderRow.TableSection = TableRowSection.TableHeader;
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


    void LoadTickets()
    {
        DataSet dsTicket = TicketBD.SelecionarTodos();
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
            lkb = (LinkButton)e.Row.Cells[3].FindControl("lkbUpdate");
            if (e.Row.Cells[2].Text == "0")
            {
                lkb.Text = " <i class='fa-solid fa-table me-1'></i>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "$('#idModal').modal('show')", true);
                
            }
            else
            {
                lkb.Text = "<i class='fa-solid fa-table me-1'></i>";
                lkb.CommandName = "inactive";
                e.Row.Cells[2].Text = "<i class='fa-solid fa-table me-1'></i>";
            }
        }
    }

    protected void gdvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int idTickets = Convert.ToInt32(e.CommandArgument.ToString());
        

       
    }
}