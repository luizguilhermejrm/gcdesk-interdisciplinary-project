<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta name="author" content="Luiz Henrique Romão de Carvalho, Luiz Guilherme José Ribeiro Moreira e Aureo Alexandre Bueno Azevedo Filho">
    <meta name="description" content="Projeto Interdisciplinar">
    <meta name="keywords" content="Atendimento ao Usuário e gestão de chamados">
     <!-- Font Awesome -->
     <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css">
     <!-- icheck bootstrap -->
     <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <link rel="stylesheet" href="dist/css/adminlte.min.css">
    <link rel="stylesheet" href="dist/css/adminlte.min.css.map">
    
    <title>GCDesk - Login </title>
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <!-- /.login-logo -->
        <div class="card card-outline card-primary">
          <div class="card-header text-center">
            <a href="Default.aspx" class="h1">Bem-Vindo!</a>
          </div>
          <div class="card-body">
            <p class="login-box-msg">
              <asp:Label ID="lblMsg" runat="server" />
            </p>
      
            <form runat="server">
              <div class="input-group mb-3">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" />
                <div class="input-group-append">
                  <div class="input-group-text">
                    <span class="fas fa-envelope"></span>
                  </div>
                </div>
              </div>
              <div class="input-group mb-3">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Senha"
                                TextMode="Password"></asp:TextBox>
                <div class="input-group-append">
                  <div class="input-group-text">
                    <span class="fas fa-lock"></span>
                  </div>
                </div>
              </div>
              <div class="row">
                <!-- /.col -->
                <div class="col-8">
                    <asp:Button ID="btnAcessar" runat="server" Text="Login"
                    CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                </div>
                <!-- /.col -->
              </div>
            </form>
          </div>
          <!-- /.card-body -->
        </div>
        <!-- /.card -->
      </div>
      <!-- /.login-box -->

     <!-- jQuery -->
     <script src="plugins/jquery/jquery.min.js"></script>
     <!-- Bootstrap 4 -->
     <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
     <!-- AdminLTE App -->
     <script src="dist/js/adminlte.min.js"></script>
</body>
</html>
