<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="ListCollaborator.aspx.cs" Inherits="Pages_Sistema_Analista_ListCollaborator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="pageHeader">
        <div class="container-fluid">
            <div class="row d-flex justify-content-between">
                <div class="col-auto mb-2">
                    <asp:LinkButton ID="btn" CssClass="btn btn-primary  mt-3" OnClick="btn_Click" runat="server"><i class="fa-solid fa-plus mx-1 fs-6 me-3"></i>Criar novo Colaborador</asp:LinkButton>
                </div>

                <hr />
                <p class="fs-3 mb-5 text-primary">
                    Tabela de Colaboradores
                </p>
            </div>
        </div>
    </section>
    <section id="pageContent">
        <asp:Label ID="lblCdCollaborator" runat="server" />
    </section>
    <section id="pageFooter">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12">
                    <div class="card mb-4">
                        <div class="card-header bg-primary text-white">
                            <i class="fa-solid fa-table me-1"></i>
                            Colaboradores registrados
                        </div>
                        <div class="card-body overflow-auto">
                            <div class="table-responsive p-3">
                                <asp:GridView ID="gdvCollaborator" runat="server" AutoGenerateColumns="false" CssClass="table table-hover datatable-plugin border-top-0 border-start-0 border-end-0">
                                    <Columns>
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="user_name" HeaderText="Nome" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="user_email" HeaderText="Email" />
                                        <asp:BoundField ItemStyle-CssClass="py-4 text-black-50" DataField="user_position" HeaderText="Posição" />
                                    </Columns>
                                </asp:GridView>
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
                <button type="button" class="btn-close mt-2 ms-2" data-bs-dismiss="modal" aria-label="Close"></button>
                <div class="modal-header border-0">
                    <h5 class="modal-title text-center text-primary fw-bold fs-2 py-1 mx-auto" id="exampleModalLabel">Criar novo Colaborador</h5>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control " placeholder="Insira o nome" TextMode="SingleLine" required />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mt-3 " placeholder="Insira o email" TextMode="Email" required />
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mt-3" placeholder="Insira a senha" TextMode="Password" required />
                    <asp:DropDownList ID="ddlPositionUser" runat="server" CssClass="form-control mt-3" placeholder="Posição do Usuario" TextMode="SingleLine" >
                        <asp:ListItem Text="Escolha qual o tipo de ticket" Value="Escolha o cargo do usuario" Disabled="true" Selected="True"/>
                        <asp:ListItem Text="Administrativo" Value="1"/>
                        <asp:ListItem Text="Financeiro" Value="2" />
                        <asp:ListItem Text="Recursos Humanos" Value="3" />
                        <asp:ListItem Text="Setor Comercial" Value="4" />
                        <asp:ListItem Text="Tecnologia da Informação" Value="5" />
                    </asp:DropDownList>
                </div>
                <div class="modal-footer border-0">
                    <asp:Button ID="btnCollaborator" runat="server" Text="Enviar" CssClass="btn btn-primary w-100" OnClick="btnCollaborator_Click" />
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
    <script>
        window.onload = (event) => {
            let myAlert = document.querySelector('.toast');
            let bsAlert = new bootstrap.Toast(myAlert);
            bsAlert.show();
        }
    </script>
</asp:Content>

