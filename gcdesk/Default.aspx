<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta name="author" content="Luiz Henrique Romão de Carvalho, Luiz Guilherme José Ribeiro Moreira e Aureo Alexandre Bueno Azevedo Filho">
    <meta name="description" content="Projeto Interdisciplinar">
    <meta name="keywords" content="Atendimento ao Usuário e gestão de chamados">
    <link rel="stylesheet" href="css/bootstrap.min.css">

    <title>GCDesk - Login </title>
</head>
<body class="hold-transition login-page bg-light bg-gradient">

    <div class="container">
        <div class="d-flex justify-content-center align-items-center min-vh-100 my-3">
            <div class="card shadow bg-body rounded-start border-0" style="width: 1100px;">
                <div class="row d-flex">
                    <div class="col-md-6">
                        <div class="card-body p-5">
                            <div class="content-form p-5">
                                <a href="Default.aspx" class="fs-4 lh-sm fw-bold text-decoration-none text-black">Bem vindo ao sistema de<br />
                                    <span class="text-primary">gerenciamento de chamados Desk.</span></a>
                                <p class="my-3 fw-light">
                                    Faça login para continuar.
                                </p>
                                <p class="login-box-msg">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="text-warning mb-3" />
                                </p>
                                <form runat="server">
                                    <div class="col-12 mb-4">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email" />
                                    </div>
                                    <div class="col-12 mb-4">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Senha" TextMode="Password" />
                                    </div>
                                    <div class="col-12 mb-4">
                                        <asp:Button ID="Button1" runat="server" Text="Login"
                                            CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
                                    </div>
                                    <div class="col-12">
                                        <div class="form-check d-flex">
                                            <input class="form-check-input" type="checkbox" id="gridCheck">
                                            <label class="form-check-label ms-2" for="gridCheck">
                                                Lembrar-me
                                            </label>
                                            <a href="#" class="text-decoration-none ms-auto">Esqueceu a senha?</a>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <img src="image/background.png" class="img-fluid rounded-end w-100 h-100" alt="Imagem background do login.">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootstrap.min.js.map"></script>
</body>
</html>
