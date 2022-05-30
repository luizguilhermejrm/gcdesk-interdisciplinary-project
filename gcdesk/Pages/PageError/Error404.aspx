<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error404.aspx.cs" Inherits="_Error404" %>

<!DOCTYPE html>
<html lang="pt-br">
<head runat="server">
    <meta charset="utf-8">
    <link rel="stylesheet" href="../../css/bootstrap.min.css">

    <title>Page 404 - NotFound </title>
</head>
<body class="vh-100">

    <div class="bg-light h-50">
    </div>
    <div class="position-absolute top-50 start-50 translate-middle">
        <div class="card shadow p-4 " style="width: 1100px;">
            <div class="card-body">
                <div class="container">
                    <div class="row">
                        <div class="col-md-8">
                            <h1 class="text-primary fw-normal mb-4">GC Desk</h1>
                            <h6><b>404.</b> Página não existe</h6>
                            <p>O URL solicitado não foi encontrado neste servidor.</p>
                            <p>Isso é tudo que sabemos.</p>
                            <a class="btn btn-primary">Voltar</a>
                        </div>
                        <div class="col-md-4">
                            <img src="../../image/PageError.svg" class="img-fluid mt-5" alt="Imagem background de Error 404.">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="bg-white h-50">
    </div>


    <script src="../../js/bootstrap.min.js"></script>
    <script src="../../js/bootstrap.min.js.map"></script>
</body>
</html>
