<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_Relatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container-fluid mt-4">
    <div class="card">
      <div class="card-header bg-primary text-white d-flex justify-content-between">
        <span><i class="fa-solid fa-file-export me-1"></i> Relatórios</span>
        <asp:LinkButton ID="btnExportExcel" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnExportExcel_Click">
          <i class="fa-solid fa-file-excel me-1"></i> Exportar Excel
        </asp:LinkButton>
      </div>
      <div class="card-body">
        <div class="row mb-3">
          <div class="col-md-4">
            <label class="form-label">Filtrar por Status</label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
              <asp:ListItem Text="Todos" Value="" />
              <asp:ListItem Text="Aberto" Value="0" />
              <asp:ListItem Text="Em Andamento" Value="1" />
              <asp:ListItem Text="Finalizado" Value="2" />
            </asp:DropDownList>
          </div>
          <div class="col-md-4">
            <label class="form-label">Filtrar por Departamento</label>
            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" />
          </div>
          <div class="col-md-4 d-flex align-items-end">
            <asp:LinkButton ID="btnExportFiltered" runat="server" CssClass="btn btn-outline-success" OnClick="btnExportExcel_Click">
              <i class="fa-solid fa-file-excel me-1"></i> Exportar Filtrados
            </asp:LinkButton>
          </div>
        </div>
        <div class="table-responsive">
          <asp:GridView ID="gdvReport" runat="server" AutoGenerateColumns="true"
            CssClass="table table-striped table-hover" AllowPaging="true" PageSize="20"
            OnPageIndexChanging="gdvReport_PageIndexChanging" />
        </div>
      </div>
    </div>
  </div>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
