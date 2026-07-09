<%@ Page Title="" Language="C#" AutoEventWireup="true"
CodeFile="Default.aspx.cs" Inherits="RecuperarSenha" %>

<!DOCTYPE html>
<html>
  <head runat="server">
    <title>Recuperar Senha - GC Desk</title>
    <link
      href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css"
      rel="stylesheet"
    />
    <link
      rel="stylesheet"
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css"
    />
  </head>
  <body class="bg-light">
    <form runat="server">
      <div class="container">
        <div class="row justify-content-center mt-5">
          <div class="col-md-4">
            <div class="card shadow">
              <div class="card-body text-center">
                <i class="fa-solid fa-key fs-1 text-primary mb-3"></i>
                <h4>Recuperar Senha</h4>
                <p class="text-muted">
                  Digite seu email para receber o link de recuperação.
                </p>
                <asp:TextBox
                  ID="txtEmail"
                  runat="server"
                  CssClass="form-control mb-3"
                  Placeholder="Seu email"
                />
                <asp:LinkButton
                  ID="btnSend"
                  runat="server"
                  CssClass="btn btn-primary w-100"
                  OnClick="btnSend_Click"
                >
                  <i class="fa-solid fa-paper-plane me-1"></i> Enviar
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
