﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Pages_Sistema_Analista_ListCollaborator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserBD userbd = new UserBD();

        LoadCollaborator();
        if (gdvCollaborator.Rows.Count > 0)
            gdvCollaborator.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "var myModal = new bootstrap.Modal(document.getElementById('exampleModal'), {});" +
          "document.onreadystatechange = function () {" +
          " myModal.show();" +
          "};", true);
    }

    protected void btnCollaborator_Click(object sender, EventArgs e)
    {
        User user = new User();
        user.Name = txtName.Text;
        user.Position = txtPosition.Text;
        user.Email = txtEmail.Text;



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

    void LoadCollaborator()
    {
        DataSet dsCollaborator = UserBD.SelectAll();
        int qtd = dsCollaborator.Tables[0].Rows.Count;
        gdvCollaborator.Visible = false;
        if (qtd > 0)
        {
            gdvCollaborator.DataSource = dsCollaborator.Tables[0].DefaultView;
            gdvCollaborator.DataBind();
            gdvCollaborator.HeaderRow.TableSection = TableRowSection.TableHeader;
            gdvCollaborator.Visible = true;
        }
    }
}