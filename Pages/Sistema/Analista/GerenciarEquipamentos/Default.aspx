<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_GerenciarEquipamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container-fluid px-4">
    <h3 class="mb-4"><i class="fa-solid fa-desktop me-2"></i>Gerenciar Equipamentos</h3>

    <asp:Label ID="lblMsg" runat="server" />

    <div class="mb-3">
      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalEquip">
        <i class="fa-solid fa-plus me-1"></i> Novo Equipamento
      </button>
    </div>

    <div class="card shadow-sm">
      <div class="card-body">
        <div class="table-responsive">
          <asp:GridView ID="gdvEquips" runat="server" CssClass="table table-striped table-hover datatable-plugin"
            AutoGenerateColumns="false" DataKeyNames="EquipmentId"
            OnRowCommand="gdvEquips_RowCommand">
            <Columns>
              <asp:BoundField DataField="EquipmentId" HeaderText="ID" ReadOnly="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
              <asp:BoundField DataField="Description" HeaderText="Descrição" />
              <asp:BoundField DataField="EquipNumber" HeaderText="Quantidade" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
              <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                <ItemTemplate>
                  <button type="button" class="btn btn-sm btn-warning me-1" onclick='<%# "openEditEquip(" + Eval("EquipmentId") + ",\"" + HttpUtility.JavaScriptStringEncode(Eval("Description").ToString()) + "\"," + Eval("EquipNumber") + ")" %>'>
                    <i class="fa-solid fa-pen"></i>
                  </button>
                  <asp:LinkButton runat="server" CssClass="btn btn-sm btn-danger" CommandName="DeleteEquip"
                    CommandArgument='<%# Eval("EquipmentId") %>'
                    OnClientClick="return confirm('Excluir este equipamento?');">
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

  <div class="modal fade" id="modalEquip" tabindex="-1">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header bg-primary text-white">
          <h5 class="modal-title"><i class="fa-solid fa-desktop me-2"></i><asp:Label ID="lblModalTitle" runat="server" Text="Novo Equipamento" /></h5>
          <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
        </div>
        <div class="modal-body">
          <asp:HiddenField ID="hiddenEquipId" runat="server" Value="0" />
          <div class="mb-3">
            <label class="form-label">Descrição</label>
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Ex: Desktop Dell Optiplex 3070" />
          </div>
          <div class="mb-3">
            <label class="form-label">Quantidade</label>
            <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control" TextMode="Number" min="1" />
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
    function openEditEquip(id, desc, number) {
      document.getElementById('<%= hiddenEquipId.ClientID %>').value = id;
      document.getElementById('<%= txtDescription.ClientID %>').value = desc;
      document.getElementById('<%= txtNumber.ClientID %>').value = number;
      document.getElementById('<%= lblModalTitle.ClientID %>').innerText = 'Editar Equipamento';
      var modal = new bootstrap.Modal(document.getElementById('modalEquip'));
      modal.show();
    }
  </script>
</asp:Content>
