<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_LogsAuditoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:UpdatePanel ID="upAudit" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div class="container-fluid mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
          <h3 class="text-primary mb-0">Logs de Auditoria</h3>
          <span class="badge bg-secondary">Somente leitura</span>
        </div>

        <div class="card mb-4">
          <div class="card-header bg-primary text-white"><i class="fa-solid fa-filter me-1"></i> Filtros</div>
          <div class="card-body">
            <div class="row g-3 align-items-end">
              <div class="col-md-3">
                <label class="form-label">Ação</label>
                <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" AutoPostBack="false">
                  <asp:ListItem Text="Todos" Value="" />
                </asp:DropDownList>
              </div>
              <div class="col-md-2">
                <label class="form-label">Usuário (ID)</label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" Placeholder="Ex: 1" />
              </div>
              <div class="col-md-3">
                <label class="form-label">Data inicial</label>
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" TextMode="Date" />
              </div>
              <div class="col-md-3">
                <label class="form-label">Data final</label>
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" TextMode="Date" />
              </div>
              <div class="col-md-1 d-grid">
                <asp:LinkButton ID="btnFilter" runat="server" CssClass="btn btn-primary" OnClick="btnFilter_Click">
                  <i class="fa-solid fa-magnifying-glass"></i>
                </asp:LinkButton>
              </div>
              <div class="col-md-1 d-grid">
                <asp:LinkButton ID="btnClear" runat="server" CssClass="btn btn-outline-secondary" OnClick="btnClear_Click">
                  <i class="fa-solid fa-eraser"></i>
                </asp:LinkButton>
              </div>
            </div>
          </div>
        </div>

        <asp:Label ID="lblMsg" runat="server" CssClass="d-block mb-2" />

        <div class="card">
          <div class="card-header bg-primary text-white"><i class="fa-solid fa-list me-1"></i> Registros</div>
          <div class="card-body overflow-auto" style="max-height: 600px">
            <asp:Label ID="lblTableNull" runat="server" />
            <asp:GridView ID="gdvAudit" runat="server" AutoGenerateColumns="false"
              AllowPaging="true" PageSize="20"
              CssClass="table table-hover table-sm datatable-plugin border-top-0 border-start-0 border-end-0"
              OnPageIndexChanging="gdvAudit_PageIndexChanging">
              <Columns>
                <asp:BoundField ItemStyle-CssClass="py-2 text-black-50" DataField="log_id" HeaderText="ID" />
                <asp:BoundField ItemStyle-CssClass="py-2 text-black-50" DataField="user_id" HeaderText="Usuário" />
                <asp:BoundField ItemStyle-CssClass="py-2 text-black-50" DataField="action" HeaderText="Ação" />
                <asp:BoundField ItemStyle-CssClass="py-2 text-black-50" DataField="detail" HeaderText="Detalhe" />
                <asp:BoundField ItemStyle-CssClass="py-2 text-black-50" DataField="ip_address" HeaderText="IP" />
                <asp:BoundField ItemStyle-CssClass="py-2 text-black-50" DataField="created_at" HeaderText="Data/Hora" />
              </Columns>
            </asp:GridView>
          </div>
        </div>
      </div>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btnFilter" />
      <asp:AsyncPostBackTrigger ControlID="btnClear" />
      <asp:AsyncPostBackTrigger ControlID="gdvAudit" />
    </Triggers>
  </asp:UpdatePanel>
</asp:Content>
