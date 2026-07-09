<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="RedefinirSenha" %>

<!DOCTYPE html>
<html>
<head runat="server">
  <title>Redefinir Senha - GC Desk</title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
</head>
<body class="bg-light">
  <form runat="server">
    <div class="container">
      <div class="row justify-content-center mt-5">
        <div class="col-md-4">
          <div class="card shadow">
            <div class="card-body text-center">
              <i class="fa-solid fa-lock fs-1 text-primary mb-3"></i>
              <h4>Redefinir Senha</h4>
              <asp:HiddenField ID="hiddenToken" runat="server" />
              <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-2" TextMode="Password" Placeholder="Nova senha" />
              <asp:TextBox ID="txtConfirm" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Confirmar senha" />
              <asp:LinkButton ID="btnReset" runat="server" CssClass="btn btn-primary w-100" OnClick="btnReset_Click">
                <i class="fa-solid fa-check me-1"></i> Redefinir
              </asp:LinkButton>
              <asp:Label ID="lblMsg" runat="server" CssClass="mt-3 d-block" />
              <a href="../../Default.aspx" class="d-block mt-3">Voltar ao login</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </form>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
