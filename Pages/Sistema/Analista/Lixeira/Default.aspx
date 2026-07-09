<%@ Page Title="" Language="C#"
MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true"
CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_Lixeira" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:UpdatePanel ID="upLixeira" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div class="container-fluid">
        <h1 class="mt-4"><i class="fa-solid fa-trash-can me-2"></i>Lixeira</h1>
        <p class="text-muted">Registros deletados. Clique em <strong>Restaurar</strong> para reativar.</p>

        <asp:Label ID="lblMsg" runat="server" />

        <div class="card mb-4">
          <div class="card-header bg-danger text-white"><i class="fa-solid fa-ticket me-1"></i> Chamados Deletados</div>
          <div class="card-body overflow-auto">
            <asp:Label ID="lblNullTickets" runat="server" />
            <asp:GridView ID="gdvTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm datatable-plugin"
              OnRowCommand="gdvTickets_RowCommand" DataKeyNames="tic_id">
              <Columns>
                <asp:BoundField DataField="tic_id" HeaderText="ID" />
                <asp:BoundField DataField="tic_description" HeaderText="Descrição" />
                <asp:BoundField DataField="tic_type" HeaderText="Tipo" />
                <asp:BoundField DataField="deleted_at" HeaderText="Deletado em" />
                <asp:TemplateField HeaderText="Ação">
                  <ItemTemplate>
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-sm btn-success"
                      CommandName="Restore" CommandArgument='<%# Eval("tic_id") %>'
                      OnClientClick="return confirm('Restaurar este chamado?');">
                      <i class="fa-solid fa-rotate-left"></i> Restaurar
                    </asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
          </div>
        </div>

        <div class="card mb-4">
          <div class="card-header bg-danger text-white"><i class="fa-solid fa-user me-1"></i> Usuários Deletados</div>
          <div class="card-body overflow-auto">
            <asp:Label ID="lblNullUsers" runat="server" />
            <asp:GridView ID="gdvUsers" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm datatable-plugin"
              OnRowCommand="gdvUsers_RowCommand" DataKeyNames="user_id">
              <Columns>
                <asp:BoundField DataField="user_name" HeaderText="Nome" />
                <asp:BoundField DataField="user_email" HeaderText="Email" />
                <asp:BoundField DataField="user_typeAnalyst" HeaderText="Tipo" />
                <asp:BoundField DataField="deleted_at" HeaderText="Deletado em" />
                <asp:TemplateField HeaderText="Ação">
                  <ItemTemplate>
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-sm btn-success"
                      CommandName="Restore" CommandArgument='<%# Eval("user_id") %>'
                      OnClientClick="return confirm('Restaurar este usuário?');">
                      <i class="fa-solid fa-rotate-left"></i> Restaurar
                    </asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
          </div>
        </div>

        <div class="card mb-4">
          <div class="card-header bg-danger text-white"><i class="fa-solid fa-building me-1"></i> Departamentos Deletados</div>
          <div class="card-body overflow-auto">
            <asp:Label ID="lblNullDepts" runat="server" />
            <asp:GridView ID="gdvDepartments" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm"
              OnRowCommand="gdvDepartments_RowCommand" DataKeyNames="dep_id">
              <Columns>
                <asp:BoundField DataField="dep_sector" HeaderText="Departamento" />
                <asp:BoundField DataField="deleted_at" HeaderText="Deletado em" />
                <asp:TemplateField HeaderText="Ação">
                  <ItemTemplate>
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-sm btn-success"
                      CommandName="Restore" CommandArgument='<%# Eval("dep_id") %>'
                      OnClientClick="return confirm('Restaurar este departamento?');">
                      <i class="fa-solid fa-rotate-left"></i> Restaurar
                    </asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
          </div>
        </div>

        <div class="card mb-4">
          <div class="card-header bg-danger text-white"><i class="fa-solid fa-desktop me-1"></i> Equipamentos Deletados</div>
          <div class="card-body overflow-auto">
            <asp:Label ID="lblNullEquip" runat="server" />
            <asp:GridView ID="gdvEquipment" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm"
              OnRowCommand="gdvEquipment_RowCommand" DataKeyNames="equip_id">
              <Columns>
                <asp:BoundField DataField="equip_description" HeaderText="Equipamento" />
                <asp:BoundField DataField="equip_number" HeaderText="Número" />
                <asp:BoundField DataField="deleted_at" HeaderText="Deletado em" />
                <asp:TemplateField HeaderText="Ação">
                  <ItemTemplate>
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-sm btn-success"
                      CommandName="Restore" CommandArgument='<%# Eval("equip_id") %>'
                      OnClientClick="return confirm('Restaurar este equipamento?');">
                      <i class="fa-solid fa-rotate-left"></i> Restaurar
                    </asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
          </div>
        </div>

        <div class="card mb-4">
          <div class="card-header bg-danger text-white"><i class="fa-solid fa-wrench me-1"></i> Serviços Deletados</div>
          <div class="card-body overflow-auto">
            <asp:Label ID="lblNullServices" runat="server" />
            <asp:GridView ID="gdvServices" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm"
              OnRowCommand="gdvServices_RowCommand" DataKeyNames="service_id">
              <Columns>
                <asp:BoundField DataField="service_solution" HeaderText="Solução" />
                <asp:BoundField DataField="deleted_at" HeaderText="Deletado em" />
                <asp:TemplateField HeaderText="Ação">
                  <ItemTemplate>
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-sm btn-success"
                      CommandName="Restore" CommandArgument='<%# Eval("service_id") %>'
                      OnClientClick="return confirm('Restaurar este serviço?');">
                      <i class="fa-solid fa-rotate-left"></i> Restaurar
                    </asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
          </div>
        </div>

        <div class="card mb-4">
          <div class="card-header bg-danger text-white"><i class="fa-solid fa-bell me-1"></i> Notificações Deletadas</div>
          <div class="card-body overflow-auto">
            <asp:Label ID="lblNullNotif" runat="server" />
            <asp:GridView ID="gdvNotifications" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-sm"
              OnRowCommand="gdvNotifications_RowCommand" DataKeyNames="not_id">
              <Columns>
                <asp:BoundField DataField="not_title" HeaderText="Título" />
                <asp:BoundField DataField="not_description" HeaderText="Descrição" />
                <asp:BoundField DataField="deleted_at" HeaderText="Deletado em" />
                <asp:TemplateField HeaderText="Ação">
                  <ItemTemplate>
                    <asp:LinkButton ID="btnRestore" runat="server" CssClass="btn btn-sm btn-success"
                      CommandName="Restore" CommandArgument='<%# Eval("not_id") %>'
                      OnClientClick="return confirm('Restaurar esta notificação?');">
                      <i class="fa-solid fa-rotate-left"></i> Restaurar
                    </asp:LinkButton>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
          </div>
        </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
