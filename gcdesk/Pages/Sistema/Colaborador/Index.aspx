<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Pages_Sistema_Colaborador_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="pageHeader">
        <div class="container-fluid">
            <div class="row d-flex justify-content-between">
                <div class="col-auto">
                    <asp:LinkButton ID="btn" CssClass="btn btn-primary  mt-3" OnClick="btn_Click" runat="server"><i class="fas fa-tachometer-alt mx-1 fs-6 me-3"></i>Criar chamado</asp:LinkButton>
                </div>
                <div class="col-auto">
                    <div id="lblMsgSuccess" runat="server" visible="false">
                        <div class="alert alert-success alert-dismissible fade show d-flex align-items-center mb-0 mt-3 px-3 py-2 ms-auto" role="alert">
                            <svg class="bi flex-shrink-0 me-3" width="16" height="16" role="img" aria-label="Success:">
                                <use xlink:href="#check-circle-fill" />
                            </svg>
                            <div class="me-3">
                                Cadastrado com sucesso! 
                            </div>
                            <i class="fa-solid fa-close mt-1" data-bs-dismiss="alert"></i>
                        </div>
                    </div>
                    <div id="lblMsgError" runat="server" visible="false">
                        <div class="alert alert-danger alert-dismissible fade show d-flex align-items-center mb-0 mt-3 px-3 py-2 ms-auto" role="alert">
                            <svg class="bi flex-shrink-0 me-3" width="16" height="16" role="img" aria-label="Danger:">
                                <use xlink:href="#exclamation-triangle-fill" />
                            </svg>
                            <div class="me-3">
                                Erro ao cadastrar!
                            </div>
                            <i class="fa-solid fa-close mt-1" data-bs-dismiss="alert"></i>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <p class="fs-3 mb-5">
                Resumo do colaborador: 
                <asp:Label ID="lblTitle" runat="server" Text="" />
            </p>
        </div>
    </section>
    <section id="pageContent">
        <div class="container">
            <div class="row">
                <div class="col-md-4 my-1 d-flex justify-content-center">
                    <div class="card w-75 text-center border-0 shadow pb-4">
                        <div class="card-header border-0 mb-2 text-start bg-transparent">
                            <i class="fa-solid fa-magnifying-glass text-secondary"></i>
                        </div>
                        <div class="d-flex justify-content-center">
                            <div class="rounded-circle bg-secondary" style="width: 70px; height: 70px;">
                                <i class="fa-solid fa-lock text-black-50 fs-6 mt-4"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <p class="card-title mt-3 fs-1">01</p>
                            <p class="card-text fs-6 fw-bold text-black-50">Chamados Finalizados</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 my-1 d-flex justify-content-center">
                    <div class="card w-75 text-center border-0 shadow pb-4">
                        <div class="card-header border-0 mb-2 text-start bg-transparent">
                            <i class="fa-solid fa-magnifying-glass text-secondary"></i>
                        </div>
                        <div class="d-flex justify-content-center">
                            <div class="rounded-circle bg-secondary" style="width: 70px; height: 70px;">
                                <i class="fa-solid fa-lock text-black-50 fs-6 mt-4"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <p class="card-title mt-3 fs-1">02</p>
                            <p class="card-text fs-6 fw-bold text-black-50">Chamados andamento</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 my-1 d-flex justify-content-center">
                    <div class="card w-75 text-center border-0 shadow pb-4">
                        <div class="card-header border-0 mb-2 text-start bg-transparent">
                            <i class="fa-solid fa-magnifying-glass text-secondary"></i>
                        </div>
                        <div class="d-flex justify-content-center">
                            <div class="rounded-circle bg-secondary" style="width: 70px; height: 70px;">
                                <i class="fa-solid fa-lock text-black-50 fs-6 mt-4"></i>
                            </div>
                        </div>
                        <div class="card-body">
                            <p class="card-title mt-3 fs-1">03</p>
                            <p class="card-text fs-6 fw-bold text-black-50">Chamados aberto</p>
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
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Últimos chamados
                        </div>
                        <div class="card-body overflow-auto" style="height: 400px">
                            <div class="table-responsive">
                                <asp:GridView ID="gdvTickets" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover border-top-0 border-start-0 border-end-0">
                                    <Columns>
                                        <asp:BoundField DataField="tic_description" HeaderText="Descrição" />
                                        <asp:BoundField DataField="tic_localization" HeaderText="Localização" />
                                        <asp:BoundField DataField="tic_openTime" HeaderText="Horário de abertura" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <div class="col-md-6">
                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Últimos chamados
                        </div>
                        <div class="card-body overflow-auto" style="height: 400px">
                            <div class="table-responsive">
                                <table class="table table-striped table-hover" id="datatablesSimple">
                                    <thead>
                                        <tr>
                                            <th>Descrição</th>
                                            <th>Status</th>
                                            <th>Aberto</th>
                                            <th>Fechado</th>
                                            <th>Analista</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                        <tr>
                                            <td>Senha não entra</td>
                                            <td>Fechado</td>
                                            <td>17:00</td>
                                            <td>19:00</td>
                                            <td>João Alves</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-md-6">
                    <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Notificações recentes
                        </div>
                        <div class="card-body overflow-auto" style="height: 400px">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="exampleModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title" id="exampleModalLabel">Criar chamado</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtProblem" runat="server" CssClass="form-control" placeholder="Qual o seu problema" TextMode="SingleLine" />
                    <asp:TextBox ID="txtLocal" runat="server" CssClass="form-control mt-3" placeholder="Localização" TextMode="SingleLine" />
                    <asp:TextBox ID="txtData" runat="server" CssClass="form-control mt-3" placeholder="Tempo Aberto" TextMode="DateTime" />
                </div>
                <div class="modal-footer border-0">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
                    <asp:Button ID="btnTicket" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="btnTicket_Click" />
                </div>
            </div>
        </div>
    </div>
    <script src="../../../js/bootstrap.min.js"></script>
</asp:Content>

