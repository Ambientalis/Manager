<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="AtualizarOS.aspx.cs" Inherits="OrdemServico_AtualizarOS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Atualizar OS
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <div>
        <div class="barra">
            Atualizar data de vencimento das OS's (torná-las encerradas no prazo)
        </div>
        <div class="cph">
            <div style="float: left; width: 80%">
                <div class="label_form">
                    Código das OS's (separados por ';')*
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCodigoOS" CssClass="RequireFieldValidator"
                         ErrorMessage="Códigos" ValidationGroup="rfvAtualizarOS">Obrigatório!</asp:RequiredFieldValidator>
                </div>
                <div class="campo_form">
                    <asp:TextBox ID="tbxCodigoOS" CssClass="textBox100" runat="server"></asp:TextBox>
                </div>
            </div>
            <div style="float: right">
                <div class="label_form">
                    &nbsp;
                </div>
                <asp:UpdatePanel runat="server" ID="upAtualizarOS" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnAtualizarOS" class="botao ok" runat="server" Text="Atualizar OS's" ValidationGroup="rfvAtualizarOS"
                            ToolTip="Atualizar OS's informadas" OnClick="btnAtualizarOS_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="clear: both">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

