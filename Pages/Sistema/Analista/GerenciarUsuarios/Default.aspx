<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_GerenciarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <h3 class="mb-4"><i class="fa-solid fa-users-gear me-2"></i>Gerenciar Usuários</h3>

  <asp:Label ID="lblMsg" runat="server" />

  <div class="mb-3">
    <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalUser">
      <i class="fa-solid fa-plus me-1"></i> Novo Usuário
    </button>
  </div>

  <div class="table-responsive">
    <asp:GridView ID="gdvUsers" runat="server" CssClass="table table-striped table-bordered datatable-plugin"
      AutoGenerateColumns="false" DataKeyNames="UserId"
      OnRowCommand="gdvUsers_RowCommand">
      <Columns>
        <asp:BoundField DataField="UserId" HeaderText="ID" ReadOnly="true" />
        <asp:BoundField DataField="Name" HeaderText="Nome" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Position" HeaderText="Cargo" />
        <asp:TemplateField HeaderText="Tipo">
          <ItemTemplate>
            <span class="badge <%# Eval("TypeAccess").ToString() == "0" ? "bg-primary" : "bg-secondary" %>">
              <%# Eval("TypeAccess").ToString() == "0" ? "Analista" : "Colaborador" %>
            </span>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DepartId" HeaderText="Departamento" />
        <asp:TemplateField HeaderText="Status">
          <ItemTemplate>
            <span class="badge <%# Eval("StatusUser").ToString() == "1" ? "bg-success" : "bg-danger" %>">
              <%# Eval("StatusUser").ToString() == "1" ? "Ativo" : "Inativo" %>
            </span>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
          <ItemTemplate>
            <button type="button" class="btn btn-sm btn-warning" onclick='<%# "openEdit(" + Eval("UserId") + ",\"" + HttpUtility.JavaScriptStringEncode(Eval("Name").ToString()) + "\",\"" + HttpUtility.JavaScriptStringEncode(Eval("Email").ToString()) + "\",\"" + HttpUtility.JavaScriptStringEncode(Eval("Position")?.ToString() ?? "") + "\"," + Eval("TypeAccess") + "," + Eval("DepartId") + ")" %>'>
              <i class="fa-solid fa-pen"></i>
            </button>
            <asp:LinkButton runat="server" CssClass="btn btn-sm btn-danger" CommandName="DeleteUser"
              CommandArgument='<%# Eval("UserId") %>'
              OnClientClick="return confirm('Excluir este usuário?');">
              <i class="fa-solid fa-trash"></i>
            </asp:LinkButton>
          </ItemTemplate>
        </asp:TemplateField>
      </Columns>
    </asp:GridView>
  </div>

  <div class="modal fade" id="modalUser" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title"><i class="fa-solid fa-user me-2"></i><asp:Label ID="lblModalTitle" runat="server" Text="Novo Usuário" /></h5>
          <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
        </div>
        <div class="modal-body">
          <asp:HiddenField ID="hiddenUserId" runat="server" Value="0" />
          <div class="mb-3">
            <label class="form-label">Nome</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
          </div>
          <div class="mb-3">
            <label class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
          </div>
          <div class="mb-3">
            <label class="form-label">Cargo</label>
            <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" />
          </div>
          <div class="mb-3">
            <label class="form-label">Tipo</label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
              <asp:ListItem Value="0">Analista</asp:ListItem>
              <asp:ListItem Value="1">Colaborador</asp:ListItem>
            </asp:DropDownList>
          </div>
          <div class="mb-3">
            <label class="form-label">Departamento</label>
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" />
          </div>
          <div class="mb-3">
            <label class="form-label">Senha</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
            <small class="text-muted">Deixe em branco para manter a senha atual (ao editar).</small>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
          <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSave_Click" />
        </div>
      </div>
    </div>
  </div>

  <script>
    function openEdit(id, name, email, position, typeAccess, departId) {
      document.getElementById('<%= hiddenUserId.ClientID %>').value = id;
      document.getElementById('<%= txtName.ClientID %>').value = name;
      document.getElementById('<%= txtEmail.ClientID %>').value = email;
      document.getElementById('<%= txtPosition.ClientID %>').value = position;
      document.getElementById('<%= ddlType.ClientID %>').value = typeAccess;
      document.getElementById('<%= ddlDepartment.ClientID %>').value = departId;
      document.getElementById('<%= txtPassword.ClientID %>').value = '';
      document.getElementById('<%= lblModalTitle.ClientID %>').innerText = 'Editar Usuário';
      var modal = new bootstrap.Modal(document.getElementById('modalUser'));
      modal.show();
    }
  </script>
</asp:Content>
