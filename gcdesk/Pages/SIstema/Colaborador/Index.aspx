<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="_Index" %>

<!DOCTYPE html>
<html lang="pt-br">
<head runat="server">
    <meta charset="utf-8">
     <!-- Font Awesome -->
     <link rel="stylesheet" href="../../plugins/fontawesome-free/css/all.min.css">
     <!-- icheck bootstrap -->
     <link rel="stylesheet" href="../../plugins/icheck-bootstrap/icheck-bootstrap.min.css">
    <link rel="stylesheet" href="../../dist/css/adminlte.min.css">
    <link rel="stylesheet" href="../../dist/css/adminlte.min.css.map">
    
    <title>Colaborador </title>
</head>
<body class="hold-transition login-page">
    <p>Colaborador</p>
    <form runat="server">
        <asp:Label ID="lblTitle" runat="server" Text="Tela do Colaborador" />
        <asp:LinkButton ID="lblLogout" runat="server" Text="Sair" />
    </form>

     <!-- jQuery -->
     <script src="../../plugins/jquery/jquery.min.js"></script>
     <!-- Bootstrap 4 -->
     <script src="../../plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
     <!-- AdminLTE App -->
     <script src="../../dist/js/adminlte.min.js"></script>
</body>
</html>