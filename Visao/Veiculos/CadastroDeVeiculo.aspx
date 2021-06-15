<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroDeVeiculo.aspx.cs" Inherits="Veiculos_CadastroDeVeiculo" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("veiculos")) {
                    $(this).removeClass("veiculos");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".veiculos").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".veiculos").children("a").children("span").addClass("bg_branco");
                }
            });
        });

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Cadastro de Veículo
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioVeiculo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <div class="label_form">
                            </div>
                            <div class="campo_form">
                                <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" Checked="True" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="label_form">
                                Placa<span>*:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="tbxPlaca" CssClass="RequireFieldValidator"
                                    ErrorMessage="- Data" ValidationGroup="vlgVeiculo">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form" style="padding-right: 5%;">
                                <asp:TextBox ID="tbxPlaca" CssClass="textBox100" runat="server"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="label_form">
                                Descrição<span>*:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxDescricao" CssClass="RequireFieldValidator"
                                    ErrorMessage="- Data" ValidationGroup="vlgVeiculo">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form" style="padding-right: 5%;">
                                <asp:TextBox ID="tbxDescricao" CssClass="textBox100" runat="server"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="label_form">
                                Gestor<span>*:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGestor" InitialValue="0" CssClass="RequireFieldValidator"
                                    ErrorMessage="- Data" ValidationGroup="vlgVeiculo">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form" style="padding-right: 5%;">
                                <asp:DropDownList ID="ddlGestor" CssClass="dropDownList100" runat="server">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="label_form">
                                Departamento Responsável<span>*:</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDepartamentoResponsavel" InitialValue="0" CssClass="RequireFieldValidator"
                                    ErrorMessage="- Data" ValidationGroup="vlgVeiculo">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form" style="padding-right: 5%;">
                                <asp:DropDownList ID="ddlDepartamentoResponsavel" CssClass="dropDownList100" runat="server">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="label_form" style="margin-top: 15px;">
                                Departamentos que podem utilizar o veículo
                            </div>
                            <div class="campo_form" style="padding-right: 5%;">
                                <div style="overflow: auto; max-height: 400px;">
                                    <asp:CheckBoxList ID="cblDepartamentos" runat="server"></asp:CheckBoxList>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfId" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovoCadastro" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="margin-top: 15px;">
        <div class="label_form" style="font-size: 8pt;">
            &nbsp;<span style="color: Red;">*</span>Campos Obrigatórios
        </div>
    </div>
    <div style="text-align: right">
        <div style="text-align: right; margin-top: 10px;">
            <asp:Button ID="btnNovoCadastro" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoCadastro_Click" />&nbsp;
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvar_Click" ValidationGroup="vlgVeiculo" />&nbsp;
                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluir_Click" OnPreRender="btnExcluir_PreRender" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

