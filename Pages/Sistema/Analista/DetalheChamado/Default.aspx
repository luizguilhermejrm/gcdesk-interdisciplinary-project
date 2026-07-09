<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Sistema_Analista_DetalheChamado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="container-fluid mt-4">
    <div class="row">
      <div class="col-md-8">
        <div class="card mb-4">
          <div class="card-header bg-primary text-white d-flex justify-content-between">
            <span><i class="fa-solid fa-ticket me-1"></i> Chamado #<asp:Label ID="lblTicketId" runat="server" /></span>
            <asp:Label ID="lblStatus" runat="server" CssClass="badge" />
          </div>
          <div class="card-body">
            <p><strong>Descrição:</strong> <asp:Label ID="lblDescription" runat="server" /></p>
            <p><strong>Tipo:</strong> <asp:Label ID="lblType" runat="server" /></p>
            <p><strong>Localização:</strong> <asp:Label ID="lblLocalization" runat="server" /></p>
            <p><strong>Abertura:</strong> <asp:Label ID="lblOpenTime" runat="server" /></p>
            <p><strong>Fechamento:</strong> <asp:Label ID="lblCloseTime" runat="server" /></p>
            <p><strong>Solicitante:</strong> <asp:Label ID="lblRequester" runat="server" /></p>
            <p><strong>Analista:</strong> <asp:Label ID="lblAnalyst" runat="server" /></p>

            <asp:Panel ID="pnlRating" runat="server" Visible="false" CssClass="mt-3 p-3 bg-light rounded">
              <h5>Avaliação</h5>
              <p><strong>Nota:</strong> <asp:Label ID="lblRating" runat="server" /></p>
              <p><strong>Comentário:</strong> <asp:Label ID="lblRatingComment" runat="server" /></p>
            </asp:Panel>
          </div>
        </div>

        <asp:UpdatePanel ID="upComments" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <div class="card mb-4">
              <div class="card-header bg-primary text-white"><i class="fa-solid fa-comments me-1"></i> Comentários</div>
              <div class="card-body" style="max-height: 400px; overflow-y: auto;">
                <asp:Label ID="lblNoComments" runat="server" CssClass="text-muted" />
                <asp:Repeater ID="rptComments" runat="server">
                  <ItemTemplate>
                    <div class="d-flex mb-3">
                      <div class="me-3">
                        <img src='<%# ConfigurationManager.AppSettings["uploadHTTP"] + Eval("user_image") %>' class="rounded-circle" style="width:40px;height:40px;object-fit:cover" onerror="this.src='https://ui-avatars.com/api/?name=<%# Eval("user_name") %>'" />
                      </div>
                      <div class="flex-grow-1">
                        <strong><%# Eval("user_name") %></strong>
                        <small class="text-muted ms-2"><%# Eval("created_at") %></small>
                        <p class="mb-1"><%# Eval("comment_text") %></p>
                      </div>
                    </div>
                  </ItemTemplate>
                </asp:Repeater>
              </div>
              <div class="card-footer">
                <div class="input-group">
                  <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" Placeholder="Escreva um comentário..." TextMode="MultiLine" Rows="2" />
                  <asp:LinkButton ID="btnSendComment" runat="server" CssClass="btn btn-primary" OnClick="btnSendComment_Click">
                    <i class="fa-solid fa-paper-plane"></i> Enviar
                  </asp:LinkButton>
                </div>
              </div>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>

      <div class="col-md-4">
        <div class="card mb-4">
          <div class="card-header bg-primary text-white"><i class="fa-solid fa-clock-rotate-left me-1"></i> Histórico</div>
          <div class="card-body" style="max-height: 600px; overflow-y: auto;">
            <asp:Label ID="lblNoHistory" runat="server" CssClass="text-muted" />
            <asp:Repeater ID="rptHistory" runat="server">
              <ItemTemplate>
                <div class="mb-3">
                  <div class="d-flex align-items-center">
                    <div class="me-2">
                      <%# GetActionIcon(Eval("action").ToString()) %>
                    </div>
                    <div>
                      <strong><%# Eval("user_name") %></strong>
                      <small class="text-muted d-block"><%# Eval("created_at") %></small>
                      <span><%# Eval("description") %></span>
                    </div>
                  </div>
                  <hr class="my-2" />
                </div>
              </ItemTemplate>
            </asp:Repeater>
          </div>
        </div>
      </div>
    </div>
  </div>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
  <script>
    window.onload = (event) => {
      let myAlert = document.querySelector(".toast");
      if (myAlert) { let bsAlert = new bootstrap.Toast(myAlert); bsAlert.show(); }
    };
  </script>
</asp:Content>
