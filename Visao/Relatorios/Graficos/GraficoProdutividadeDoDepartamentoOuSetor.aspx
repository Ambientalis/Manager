<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="GraficoProdutividadeDoDepartamentoOuSetor.aspx.cs" Inherits="Relatorios_Graficos_GraficoProdutividadeDoDepartamentoOuSetor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataAte.ClientID %>"));
        });

    </script>
    <style type="text/css">
        table tr td
        {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Gráfico - Produtividade por departamento ou setor
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" runat="Server">
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-right: 15px; width: 35%">
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%; padding-right: 5%">
                            <div class="labelRelatorio">
                                Data de*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="grafico" ControlToValidate="tbxDataDe" ErrorMessage="*Obrigatório!" ForeColor="Red" />
                            </div>
                            <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
                        </td>
                        <td style="width: 50%">
                            <div class="labelRelatorio">
                                Até*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="grafico" ControlToValidate="tbxDataAte" ErrorMessage="*Obrigatório!" ForeColor="Red" />
                            </div>
                            <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="padding-right: 15px; width: 35%">
                <div class="labelRelatorio">
                    Departamento
                </div>
                <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="dropDownFiltros" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px; width: 30%">
                <asp:UpdatePanel ID="upSetores" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="labelRelatorio">
                            Setor
                        </div>
                        <asp:DropDownList ID="ddlSetor" runat="server" CssClass="dropDownFiltros">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px; width: 35%">&nbsp;</td>
            <td style="padding-right: 15px; width: 35%">&nbsp;</td>
            <td style="padding-right: 15px; width: 30%; text-align: right;">
                <div style="margin-top: 10px;">
                    <asp:Button ID="btnExibirRelatorio" runat="server" ValidationGroup="grafico" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Gráfico" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tabela_relatorio" style="text-align: center;">
        <span style="font-family: Arial, Helvetica, sans-serif; font-weight: 700;">GRÁFICO DE PRODUTIVIDADE POR DEPARTAMENTO OU SETOR</span><br />
        <asp:Label ID="lblGrafico" runat="server" Text="" EnableViewState="False"></asp:Label>
        <div id='container_grafico' style="height: 400px; max-width: 90%; margin: 0 auto;"></div>
        <div id="totalizadores" runat="server" visible="false" style="text-align: left; padding: 15px;">
            <div>
                TOTALIZADORES:
            </div>
            <div>
                <asp:Label ID="lblTotalOS" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

