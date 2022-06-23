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
    <asp:Label ID="lblMsg" runat="server" />
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
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <asp:Label ID="lblExit" runat="server" />
      </div>

    <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
        <symbol id="check-circle-fill" fill="currentColor" viewBox="0 0 16 16">
            <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z" />
        </symbol>
        <symbol id="info-fill" fill="currentColor" viewBox="0 0 16 16">
            <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2z" />
        </symbol>
        <symbol id="exclamation-triangle-fill" fill="currentColor" viewBox="0 0 16 16">
            <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z" />
        </symbol>
    </svg>

    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootstrap.min.js.map"></script>
    <script>
        window.onload = (event) => {
            let myAlert = document.querySelector('.toast');
            let bsAlert = new bootstrap.Toast(myAlert);
            bsAlert.show();
        }
    </script>

</body>
</html>
