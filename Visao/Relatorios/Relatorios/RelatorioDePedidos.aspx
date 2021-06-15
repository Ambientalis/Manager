<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioDePedidos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioDePedidos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataAte.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Relatório - Pedidos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" runat="Server">
    <div style="padding-top: 10px; padding-bottom: 10px;">
        <div style="width: 150px; text-align: left; font-family: 'Open Sans'; color: black; font-size: 13px;">
            Exibir as Colunas:
        </div>
        <div style="border: 1px solid white; padding: 10px;">
            <div>
                <asp:CheckBoxList ID="ckbColunas" runat="server" CssClass="chekListRelatorios" RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" CellSpacing="0" Font-Names="Open Sans" Font-Size="13px" ForeColor="Black">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <table style="width: 100%; margin-right: 0px;">
        <tr>
            <td style="width: 20%; padding-right: 15px">
                <div class="labelRelatorio">
                    Código
                </div>
                <asp:TextBox ID="tbxCodigo" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="width: 25%; padding-right: 15px">
                <div class="labelRelatorio">
                    Tipo de Pedido
                </div>
                <asp:DropDownList ID="ddlTipoPedido" CssClass="dropDownFiltros" runat="server">
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Criação de
                </div>
                <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Até
                </div>
                <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 25%; padding-right: 15px;">
                <div class="labelRelatorio">
                    Status
                </div>
                <asp:DropDownList ID="ddlStatus" CssClass="dropDownFiltros" runat="server">
                    <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                    <asp:ListItem Value="N">Todos</asp:ListItem>
                    <asp:ListItem Value="F">Inativos</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%; padding-right: 15px;">
                <div class="labelRelatorio">
                    Unidade
                </div>
                <asp:DropDownList ID="ddlUnidadePesquisa" CssClass="dropDownFiltros" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidadePesquisa_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="I"> Indiferente</asp:ListItem>
                    <asp:ListItem Value="C">Apenas Castelo</asp:ListItem>
                    <asp:ListItem Value="A">Apenas Cachoeiro</asp:ListItem>
                    <asp:ListItem Value="V">Apenas Vitória</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <asp:UpdatePanel ID="upClientesPesquisa" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="labelRelatorio">
                            Cliente
                        </div>
                        <asp:DropDownList ID="ddlClientePesquisa" CssClass="dropDownFiltros" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlUnidadePesquisa" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td colspan="2" style="width: 25%; padding-right: 15px;">
                <div class="labelRelatorio">
                    Vendedor
                </div>
                <asp:DropDownList ID="ddlVendedorPesquisa" CssClass="dropDownFiltros" runat="server">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Departamento
                </div>
                <asp:DropDownList ID="ddlDepartamento" CssClass="dropDownFiltros" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px; text-align: right" colspan="5">
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField HeaderText="Código" DataField="Codigo">
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Criação" DataField="Data" DataFormatString="{0:d}" />
            <asp:BoundField HeaderText="Unidade" DataField="GetDescricaoUnidade" />
            <asp:BoundField HeaderText="Cliente" DataField="GetNomeCliente" />
            <asp:BoundField HeaderText="Vendedor" DataField="GetNomeVendedor" />
            <asp:BoundField HeaderText="Tipo" DataField="GetDescricaoTipo" />
            <asp:BoundField HeaderText="OS's" DataField="GetNumerosOSs" />
            <asp:BoundField HeaderText="Possui OS no(s) Departamento(s)" DataField="GetDescricaoDepartamentos" />
            <asp:BoundField HeaderText="Orçamento" DataField="GetNumerosOrcamento" />
            <asp:BoundField HeaderText="Contrato" DataField="GetStatusContratoSimplificado" />
            <asp:BoundField HeaderText="Valor total" DataField="GetValorOss" DataFormatString="{0:c}" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

