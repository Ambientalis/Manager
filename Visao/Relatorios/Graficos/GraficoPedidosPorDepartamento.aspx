<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="GraficoPedidosPorDepartamento.aspx.cs" Inherits="Relatorios_Graficos_GraficoPedidosPorDepartamento" %>

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
    Gráfico - Pedidos por Departamento
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" runat="Server">
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 25%; padding-right: 5%">
                <div class="labelRelatorio">
                    Data de*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="grafico" ControlToValidate="tbxDataDe" ErrorMessage="*Obrigatório!" ForeColor="Red" />
                </div>
                <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 25%; padding-right: 5%">
                <div class="labelRelatorio">
                    Até*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="grafico" ControlToValidate="tbxDataAte" ErrorMessage="*Obrigatório!" ForeColor="Red" />
                </div>
                <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 25%">
                <div class="labelRelatorio">
                    Data de                 
                </div>
                <asp:DropDownList ID="ddlDataDe" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Selected="True" Value="1">Data de Criação do pedido</asp:ListItem>
                    <asp:ListItem Value="2">Data de Vencimento da OS</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 20%; padding-right: 15px; text-align: right;">
                <div style="margin-top: 10px;">
                    <asp:Button ID="btnExibirRelatorio" runat="server" ValidationGroup="grafico" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Gráfico" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tabela_relatorio" style="text-align: center;">
        <span style="font-family: Arial, Helvetica, sans-serif; font-weight: 700;">GRÁFICO DE PEDIDOS POR DEPARTAMENTO</span><br />
        <asp:Label ID="lblGrafico" runat="server" Text="" EnableViewState="False"></asp:Label>
        <div id='container_grafico' style="height: 400px; max-width: 90%; margin: 0 auto;"></div>
        <div id="totalizadores" runat="server" visible="false" style="text-align: left; padding: 15px;">
            <div>
                TOTALIZADORES:
            </div>
            <div>
                Total de Pedidos no Período:
                <asp:Label ID="lblTotalPedidos" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Repeater ID="rptToTalizadores" runat="server">
                    <ItemTemplate>
                        <div>
                            <%# BindValorTotalizador(Container.DataItem) %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>

