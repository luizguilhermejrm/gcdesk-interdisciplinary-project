<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Pages_Sistema_Colaborador_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="pageHeader">
        <div class="container-fluid">
            <div class="row d-flex justify-content-between">
                <div class="col-auto mb-2">
                    <asp:LinkButton ID="btn" CssClass="btn btn-primary  mt-3" OnClick="btn_Click" runat="server"><i class="fa-solid fa-plus mx-1 fs-6 me-3"></i>Criar chamado</asp:LinkButton>
                </div>

                <hr />
                <p class="fs-3 mb-5 text-primary">
                    Resumo
               <%-- <asp:Label ID="lblTitle" runat="server" Text="" />--%>
                </p>
            </div>
        </div>
    </section>
    <section id="pageContent">
        <asp:Label ID="lblCdTicket" runat="server" />
        <div class="container">
            <div class="row">
                <div class="col-md-4 my-1 d-flex justify-content-center">
                    <div class="card w-75 text-center border-0 shadow pb-4">
                        <div class="card-header border-0 mb-2 text-start bg-transparent">
                            <i class="fa-regular fa-circle-question text-secondary"></i>
                        </div>
                        <div class="d-flex justify-content-center">
                            <div class="rounded-circle bg-success-low" style="width: 70px; height: 70px;">
                                <i class="fa-solid fa-circle-check text-black-50 fs-6 mt-4"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Label class="card-title mt-3 fs-1" ID="lblFinishedCalls" runat="server" Text=""/>
                            <p class="card-text fs-6 fw-bold text-black-50">Chamados Finalizados</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 my-1 d-flex justify-content-center">
                    <div class="card w-75 text-center border-0 shadow pb-4">
                        <div class="card-header border-0 mb-2 text-start bg-transparent">
                            <i class="fa-regular fa-circle-question text-secondary"></i>
                        </div>
                        <div class="d-flex justify-content-center">
                            <div class="rounded-circle bg-warning-low" style="width: 70px; height: 70px;">
                                <i class="fa-solid fa-clock text-black-50 fs-6 mt-4"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Label class="card-title mt-3 fs-1" ID="lblProgressCalls" runat="server" Text=""/>
                            <p class="card-text fs-6 fw-bold text-black-50">Chamados Andamento</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 my-1 d-flex justify-content-center">
                    <div class="card w-75 text-center border-0 shadow pb-4">
                        <div class="card-header border-0 mb-2 text-start bg-transparent">
                            <i class="fa-regular fa-circle-question text-secondary"></i>
                        </div>
                        <div class="d-flex justify-content-center">
                            <div class="rounded-circle bg-primary-low" style="width: 70px; height: 70px;">
                                <i class="fa-solid fa-spinner text-black-50 fs-6 mt-4"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Label class="card-title mt-3 fs-1" ID="lblOpenCalls" runat="server" Text=""/>
                            <p class="card-text fs-6 fw-bold text-black-50">Chamados Aberto</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section id="pageFooter">
        <div class="container-fluid">
            <div class="row mt-5">
                <div class="col-md-6">
                    <div class="card mb-4">
                        <div class="card-header bg-primary text-white">
                            <i class="fa-solid fa-table me-1"></i>
                            Todos os meus Chamados
                        </div>
                        <div class="card-body overflow-auto" style="height: 400px">
                            <div class="table-responsive p-3">
                                <asp:GridView ID="gdvTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-hover datatable-plugin border-top-0 border-start-0 border-end-0"  OnRowDataBound="gdvTickets_RowDataBound">
                                    <Columns>
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_description" HeaderText="Descrição" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_localization" HeaderText="Localização" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_openTime" HeaderText="Horário de abertura" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_status" HeaderText="Status" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
               <div class="col-md-6">
                    <div class="card mb-4">
                        <div class="card-header bg-primary text-white">
                            <i class="fa-solid fa-table-list me-1"></i>
                            Notificações recentes
                        </div>
                         <div class="card-body overflow-auto" style="height: 400px">
                            <div class="table-responsive p-3">
                                <asp:GridView ID="gdvNotification" runat="server" AutoGenerateColumns="false" CssClass="table table-hover datatable-plugin border-top-0 border-start-0 border-end-0"  OnRowDataBound="GridView2_RowDataBound">
                                    <Columns>
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="not_title" HeaderText="Titulo" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="not_description" HeaderText="Mensagem" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="not_timeMensage" HeaderText="Horário de Envio" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="tic_description" HeaderText="Chamado" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                       <%-- <div class="card-body overflow-auto" style="height: 400px">
                            <div class="table-responsive">
                                <table class="table table-hover">

                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class="mt-3" style="width: 70px; height: 70px;">
                                                    <img class="rounded-circle" src="https://picsum.photos/70" />
                                                </div>
                                            </td>
                                            <td>
                                                <div class="row mx-0">
                                                    <p class="fw-bold">Assunto<span class="badge bg-primary rounded-pill ms-3">Novo</span></p>
                                                    <p class="d-inline-block" style="white-space: nowrap; width: 40em; overflow: hidden; text-overflow: ellipsis;">
                                                        Praeterea iterest quasdam iterest quasdam iterest quasdam iterest iterest quasdam iterest.
                                                    </p>
                                                    <span class="text-muted">há 1 min</span>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="mt-3" style="width: 70px; height: 70px;">
                                                    <img class="rounded-circle" src="https://picsum.photos/70" />
                                                </div>
                                            </td>
                                            <td>
                                                <div class="row mx-0">
                                                    <p class="fw-bold">Assunto<span class="badge bg-primary rounded-pill ms-3">Novo</span></p>
                                                    <p class="d-inline-block" style="white-space: nowrap; width: 40em; overflow: hidden; text-overflow: ellipsis;">
                                                        Praeterea iterest quasdam iterest quasdam iterest quasdam iterest iterest quasdam iterest.
                                                    </p>
                                                    <span class="text-muted">há 1 min</span>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="mt-3" style="width: 70px; height: 70px;">
                                                    <img class="rounded-circle" src="https://picsum.photos/70" />
                                                </div>
                                            </td>
                                            <td>
                                                <div class="row mx-0">
                                                    <p class="fw-bold">Assunto<span class="badge bg-primary rounded-pill ms-3">Novo</span></p>
                                                    <p class="d-inline-block" style="white-space: nowrap; width: 40em; overflow: hidden; text-overflow: ellipsis;">
                                                        Praeterea iterest quasdam iterest quasdam iterest quasdam iterest iterest quasdam iterest.
                                                    </p>
                                                    <span class="text-muted">há 1 min</span>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="exampleModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                    <button type="button" class="btn-close mt-2 ms-2" data-bs-dismiss="modal" aria-label="Close"></button>
                <div class="modal-header border-0">
                    <h5 class="modal-title text-center text-primary fw-bold fs-2 py-1 mx-auto" id="exampleModalLabel">Criar chamado</h5>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtProblem" runat="server" CssClass="form-control" placeholder="Descreva a sua solicitação" TextMode="SingleLine" required />
                    <asp:DropDownList ID="txtTypeTicket" runat="server" CssClass="form-control mt-3" placeholder="Qual o tipo de ticket" TextMode="SingleLine" required>
                        <asp:ListItem Text="Escolha qual o tipo de ticket" Value="Escolha qual o tipo de ticket" Disabled Selected/>
                        <asp:ListItem Text="Manutenção" Value="Manutenção"/>
                        <asp:ListItem Text="Redes" Value="Redes" />
                        <asp:ListItem Text="Software" Value="Software" />
                        <asp:ListItem Text="Registro" Value="Registro" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtLocal" runat="server" CssClass="form-control mt-3" placeholder="Localização" TextMode="SingleLine" required />
                    <asp:TextBox ID="txtData" runat="server" CssClass="form-control mt-3" placeholder="Horario em que foi aberto" TextMode="DateTime" required />
                    <div class="form-check mt-3">
                      <input class="form-check-input" type="checkbox" name="flexRadioDefault" id="flexRadioDefault1">
                      <label class="form-check-label" for="flexRadioDefault1">
                        Confirmo as informações inseridas.
                      </label>
                    </div>
                </div>
                <div class="modal-footer border-0">
                    <span id="botao" style="display: none;">
                        <asp:Button ID="btnTicket" runat="server" Text="Enviar" CssClass="btn btn-primary w-100" OnClick="btnTicket_Click" />
                    </span>
                </div>
            </div>
        </div>
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
    <script src="../../../js/bootstrap.min.js"></script>
    <script src="../../../jquery/jquery.min.js"></script>

    <script>
        window.onload = (event) => {
            let myAlert = document.querySelector('.toast');
            let bsAlert = new bootstrap.Toast(myAlert);
            bsAlert.show();
        }
        $('input[type="checkbox"]').on('click touchstart', function () {
            let quantCheck = $('input[type="checkbox"]:checked').length;

            if (quantCheck != 0) {
                $('#botao').css('display', 'block')
            }
            else {
                $('#botao').css('display', 'none')
            }
        });
    </script>
</asp:Content>

