<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Colaborador_AvaliarChamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <style>
    .star-rating { direction: rtl; display: inline-flex; font-size: 2rem; }
    .star-rating input { display: none; }
    .star-rating label { color: #ddd; cursor: pointer; padding: 0 2px; }
    .star-rating input:checked ~ label, .star-rating label:hover, .star-rating label:hover ~ label { color: #FFC107; }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <div class="card-header bg-primary text-white"><i class="fa-solid fa-star me-1"></i> Avaliar Chamado</div>
          <div class="card-body">
            <asp:Label ID="lblInfo" runat="server" CssClass="text-muted" />
            <asp:HiddenField ID="hiddenTicketId" runat="server" />

            <div class="mb-3">
              <label class="form-label">Nota:</label>
              <div class="star-rating">
                <input type="radio" id="star5" name="rating" value="5" runat="server" /><label for="star5">&#9733;</label>
                <input type="radio" id="star4" name="rating" value="4" runat="server" /><label for="star4">&#9733;</label>
                <input type="radio" id="star3" name="rating" value="3" runat="server" /><label for="star3">&#9733;</label>
                <input type="radio" id="star2" name="rating" value="2" runat="server" /><label for="star2">&#9733;</label>
                <input type="radio" id="star1" name="rating" value="1" runat="server" /><label for="star1">&#9733;</label>
              </div>
            </div>

            <div class="mb-3">
              <label class="form-label">Comentário (opcional):</label>
              <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>

            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary w-100" OnClick="btnSubmit_Click">
              <i class="fa-solid fa-paper-plane me-1"></i> Enviar Avaliação
            </asp:LinkButton>

            <asp:Label ID="lblMsg" runat="server" CssClass="mt-3 d-block" />
          </div>
        </div>
      </div>
    </div>
  </div>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
