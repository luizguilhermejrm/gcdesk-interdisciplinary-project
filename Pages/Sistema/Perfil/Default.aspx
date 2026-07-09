<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <div class="card-header bg-primary text-white"><i class="fa-solid fa-user me-1"></i> Meu Perfil</div>
          <div class="card-body">
            <div class="text-center mb-4">
              <asp:Image ID="imgProfile" runat="server" CssClass="rounded-circle" style="width:100px;height:100px;object-fit:cover" />
              <div class="mt-2">
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control form-control-sm" />
              </div>
              <h4 class="mt-2"><asp:Label ID="lblName" runat="server" /></h4>
              <span class="badge bg-info"><asp:Label ID="lblType" runat="server" /></span>
            </div>

            <div class="mb-3">
              <label class="form-label">Nome</label>
              <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
              <label class="form-label">Email</label>
              <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
              <label class="form-label">Cargo</label>
              <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
              <label class="form-label">Nova Senha (deixe em branco para manter)</label>
              <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>

            <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary w-100" OnClick="btnSave_Click">
              <i class="fa-solid fa-floppy-disk me-1"></i> Salvar
            </asp:LinkButton>
            <asp:Label ID="lblMsg" runat="server" CssClass="mt-3 d-block" />
          </div>
        </div>
      </div>
    </div>
  </div>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
