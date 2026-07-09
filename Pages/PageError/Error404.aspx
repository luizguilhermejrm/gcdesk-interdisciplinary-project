<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error404.aspx.cs"
Inherits="Error404" %>

<!DOCTYPE html>
<html lang="pt-br">
  <head runat="server">
    <meta charset="utf-8" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" />
    <title>404 - Page Not Found</title>
  </head>
  <body class="vh-100">
    <div class="bg-light h-50"></div>
    <div class="position-absolute top-50 start-50 translate-middle">
      <div class="card shadow p-4 p-xxl-5">
        <div class="card-body">
          <div class="container">
            <div class="row">
              <div class="col-md-8">
                <h1 class="text-primary fw-normal mb-4">GC Desk</h1>
                <h6><b>404.</b> Pagina nao existe</h6>
                <p>O URL solicitado nao foi encontrado neste servidor.</p>
                <p>Isso e tudo que sabemos.</p>
                <a href="/Default.aspx" class="btn btn-primary">Voltar ao inicio</a>
              </div>
              <div class="col-md-4">
                <img src="../../image/PageError.svg" class="img-fluid mt-5" alt="Error 404" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="bg-white h-50"></div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
  </body>
</html>
