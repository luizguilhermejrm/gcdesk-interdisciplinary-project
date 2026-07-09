<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_GerenciarDepartamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container-fluid px-4">
    <h3 class="mb-4"><i class="fa-solid fa-building me-2"></i>Gerenciar Departamentos</h3>

    <asp:Label ID="lblMsg" runat="server" />

    <div class="mb-3">
      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDept">
        <i class="fa-solid fa-plus me-1"></i> Novo Departamento
      </button>
    </div>

    <div class="card shadow-sm">
      <div class="card-body">
        <div class="table-responsive">
          <asp:GridView ID="gdvDepts" runat="server" CssClass="table table-striped table-hover datatable-plugin"
            AutoGenerateColumns="false" DataKeyNames="DepartmentId"
            OnRowCommand="gdvDepts_RowCommand">
            <Columns>
              <asp:BoundField DataField="DepartmentId" HeaderText="ID" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
              <asp:BoundField DataField="DepSector" HeaderText="Departamento" />
              <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                  <button type="button" class="btn btn-sm btn-warning me-1" onclick='<%# "openEditDept(" + Eval("DepartmentId") + ",\"" + HttpUtility.JavaScriptStringEncode(Eval("DepSector").ToString()) + "\")" %>'>
                    <i class="fa-solid fa-pen"></i>
                  </button>
                  <asp:LinkButton runat="server" CssClass="btn btn-sm btn-danger" CommandName="DeleteDept"
                    CommandArgument='<%# Eval("DepartmentId") %>'
                    OnClientClick="return confirm('Excluir este departamento?');">
                    <i class="fa-solid fa-trash"></i>
                  </asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </div>
      </div>
    </div>
  </div>

  <div class="modal fade" id="modalDept" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title"><i class="fa-solid fa-building me-2"></i><asp:Label ID="lblModalTitle" runat="server" Text="Novo Departamento" /></h5>
          <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
        </div>
        <div class="modal-body">
          <asp:HiddenField ID="hiddenDeptId" runat="server" Value="0" />
          <div class="mb-3">
            <label class="form-label">Nome do Departamento</label>
            <asp:TextBox ID="txtSector" runat="server" CssClass="form-control" placeholder="Ex: TI, RH, Financeiro" />
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
    function openEditDept(id, sector) {
      document.getElementById('<%= hiddenDeptId.ClientID %>').value = id;
      document.getElementById('<%= txtSector.ClientID %>').value = sector;
      document.getElementById('<%= lblModalTitle.ClientID %>').innerText = 'Editar Departamento';
      var modal = new bootstrap.Modal(document.getElementById('modalDept'));
      modal.show();
    }
  </script>
</asp:Content>
