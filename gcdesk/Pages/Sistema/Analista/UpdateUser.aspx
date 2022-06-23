<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateUser.aspx.cs" Inherits="Pages_Sistema_Analista_UpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <asp:Label ID="lblMsgUpdateUser" runat="server"/>
            <div class="col-md-12 card">
                <h5 class="card-title text-center mt-4 mb-4">Alterar Usuario</h5>
                <div class="card-body">
                    <div class="row">
                    <div class="col-2" >
                        <asp:Label ID="ImgLogado" runat="server" CssClass="rounded-start"></asp:Label>
                    </div>
                    <div class="col-10">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control " placeholder="Insira o nome" TextMode="SingleLine" required />
                        <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control mt-3" placeholder="Insira o Cargo do usuario" TextMode="SingleLine" required />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mt-3" placeholder="Insira o email" TextMode="Email" required />
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mt-3" placeholder="Insira a senha" TextMode="Password" required />
                        <asp:DropDownList ID="ddlPositionUser" runat="server" CssClass="form-control mt-3" placeholder="Departamento do Usuario" TextMode="SingleLine">
                            <asp:ListItem Text="Escolha o Departamento do Usuario" Disabled="true" Selected="True" />
                            <asp:ListItem Text="Administrativo" Value="1" />
                            <asp:ListItem Text="Financeiro" Value="2" />
                            <asp:ListItem Text="Recursos Humanos" Value="3" />
                            <asp:ListItem Text="Setor Comercial" Value="4" />
                            <asp:ListItem Text="Tecnologia da Informação" Value="5" />
                        </asp:DropDownList>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control mt-3" />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btnUpdateUser" runat="server" Text="Enviar" CssClass="btn btn-primary w-100 mt-5 mb-2" OnClick="btnUpdateUser_Click" />
                            </div>
                            <div class="col-md-4">
                               <a href="/Pages/Sistema/Analista/ListCollaborator.aspx" class="btn mt-5 mb-2  w-100" style="background-color:#DAE9FF; color: #3381E2"> <b>Voltar Tab. Colaborador</b></a>
                            </div>
                         </div>
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

