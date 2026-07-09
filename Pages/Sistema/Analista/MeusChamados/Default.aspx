<%@ Page Title="" Language="C#"
MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true"
CodeFile="Default.aspx.cs"
Inherits="Pages_Sistema_Analista_MeusChamados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content
  ID="Content2"
  ContentPlaceHolderID="ContentPlaceHolder1"
  runat="Server"
>
  <asp:UpdatePanel ID="upTickets" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div class="container-fluid">
        <div class="row mt-5">
          <div class="col-md-12">
            <div class="card mb-4">
              <div class="card-header bg-primary text-white">
                <i class="fa-solid fa-table me-1"></i>
                Meus Chamados
              </div>
              <div class="card-body overflow-auto">
                <div class="table-responsive p-3">
                  <asp:Label ID="lblTableNull" runat="server"></asp:Label>
                  <asp:GridView
                    ID="gdvTickets"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover datatable-plugin border-top-0 border-start-0 border-end-0"
                    OnRowDataBound="gdvTickets_RowDataBound"
                    OnRowCommand="gdvTickets_RowCommand"
                  >
                    <Columns>
                      <asp:TemplateField HeaderText="SLA/Prioridade" ItemStyle-CssClass="py-4 text-black-50">
                        <ItemTemplate>
                          <asp:Label ID="lblSlaBadge" runat="server" />
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_description" HeaderText="Descrição" />
                      <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_localization" HeaderText="Localização" />
                      <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_openTime" HeaderText="Horário de abertura" />
                      <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_closeTime" HeaderText="Horário de fechamento" />
                      <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_status" HeaderText="Status" />
                      <asp:TemplateField HeaderText="Ações" ItemStyle-CssClass="py-4">
                        <ItemTemplate>
                          <a href='/Pages/Sistema/Analista/DetalheChamado/Default.aspx?id=<%# Eval("tic_id") %>' class="btn btn-sm btn-outline-info me-1" title="Detalhes"><i class="fa-solid fa-eye"></i></a>
                          <asp:LinkButton ID="lkbUpdate" runat="server" CommandArgument='<%# Eval("tic_id") %>'></asp:LinkButton>
                        </ItemTemplate>
                      </asp:TemplateField>
                    </Columns>
                  </asp:GridView>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-12 mt-5">
            <asp:Label ID="lblMsg" runat="server" />
          </div>
        </div>
      </div>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="gdvTickets" />
    </Triggers>
  </asp:UpdatePanel>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
  <script>
    window.onload = (event) => {
      let myAlert = document.querySelector(".toast");
      if (myAlert) {
        let bsAlert = new bootstrap.Toast(myAlert);
        bsAlert.show();
      }
    };
  </script>
</asp:Content>
