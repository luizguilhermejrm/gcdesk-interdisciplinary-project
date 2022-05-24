<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="pt-br">
<head runat="server">
    <meta charset="utf-8">
    <meta name="author" content="Luiz Henrique Romão de Carvalho, Luiz Guilherme José Ribeiro Moreira e Aureo Alexandre Bueno Azevedo Filho">
    <meta name="description" content="Projeto Interdisciplinar">
    <meta name="keywords" content="Atendimento ao Usuário e gestão de chamados">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="stylesheet" href="fontawesome/css/all.min.css" />
    <title>GCDesk - Login </title>
</head>
<body class="hold-transition login-page" style="background-image: url('image/backgroundmain.png'); background-repeat: no-repeat; background-size: cover;">
    <div class="container">
        <div class="d-flex justify-content-center align-items-center min-vh-100">
            <div class="card shadow bg-body rounded-start border-0 mx-1" style="width: 1300px;">
                <div class="row d-flex">
                    <div class="col-md-6 d-flex justify-content-center align-items-center img-fluid" style="background-image: url('image/backgroundlogin.svg'); background-repeat: no-repeat; background-size: cover;">
                        <img src="image/vetor.svg" class="img-fluid rounded-end w-75 h-75 " alt="Imagem background do login.">
                    </div>
                    <div class="col-md-6 bg-white p-5">
                        <div class="content-form p-5 text-center">
                            <h1 class="lh-sm fw-bold display-3 text-decoration-none text-primary">Bem Vindo!</h1>
                            <h5 class="text-primary fw-normal mb-5">SISTEMA DE CHAMADO DESK</h5>
                            <p class="login-box-msg">
                                <asp:Label ID="lblMsg" runat="server" CssClass="text-warning mb-3" />
                            </p>
                            <form runat="server">
                                <div class="col-12 mb-4">
                                    <div class="input-group">
                                        <div class="input-group-text bg-white text-primary"><i class="fa-solid fa-envelope"></i></div>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control bg-primary bg-opacity-25" placeholder="Insira o seu Email" TextMode="Email" />
                                    </div>
                                </div>
                                <div class="col-12 mb-4">
                                    <div class="input-group">
                                        <div class="input-group-text bg-white text-primary"><i class="fa-solid fa-key"></i></div>
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control bg-primary bg-opacity-25" placeholder="Insira a sua Senha" TextMode="Password" />
                                    </div>
                                </div>
                                <div class="col-12 mb-4">
                                    <asp:Button ID="Button1" runat="server" Text="Entrar"
                                        CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                                </div>
                                <div class="col-12">
                                    <div class="form-check text-center">
                                        <a href="#" class="text-decoration-none">Esqueceu a senha?</a>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootstrap.min.js.map"></script>
</body>
</html>
