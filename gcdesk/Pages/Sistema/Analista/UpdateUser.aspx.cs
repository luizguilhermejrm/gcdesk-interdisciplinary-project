using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_UpdateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserBD userbd = new UserBD();
            User user = userbd.SelectUserTable(Convert.ToInt32(Session["USER_SELECT_TABLE"]));
        }
    }



    protected void btnUpdateUser_Click(object sender, EventArgs e)
    {

    }
}