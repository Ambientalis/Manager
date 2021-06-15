<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioDeVisitas.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioDeVisitas" %>

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
        table tr td
        {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Relatório - Visitas
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
    <table align="center" width="100%">
        <tr>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    OS (Nº)
                </div>
                <asp:TextBox ID="tbxNumeroOs" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Pedido (Nº)
                </div>
                <asp:TextBox ID="tbxNumeroPedido" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Descrição da Visita
                </div>
                <asp:TextBox ID="tbxDescricao" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td colspan="2" style="padding-right: 15px">
                <div class="labelRelatorio">
                    Cliente
                </div>
                <asp:DropDownList ID="ddlClientePesquisa" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Data de
                </div>
                <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Até
                </div>
                <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Ordenação
                </div>
                <asp:DropDownList ID="ddlOrdenacao" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Value="Crescente">Crescente</asp:ListItem>
                    <asp:ListItem Selected="True" Value="Decrescente">Decrescente</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Tipo
                </div>
                <asp:DropDownList ID="ddlTipoVisita" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Departamento
                </div>
                <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Responsável
                </div>
                <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Status
                </div>
                <asp:DropDownList ID="ddlStatusVisita" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                    <asp:ListItem Value="N">Todos</asp:ListItem>
                    <asp:ListItem Value="F">Inativos</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="3" style="padding-right: 15px; text-align: right; padding-top: 10px;">
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="GetDataInicio" HeaderText="Início">
                <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="GetDataFim" HeaderText="Fim">
                <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="GetNomeTipoVisita" HeaderText="Tipo" />
            <asp:BoundField DataField="GetNumeroOS" HeaderText="OS" />
            <asp:BoundField DataField="GetNumeroPedido" HeaderText="Pedido" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="GetNomeCliente" HeaderText="Cliente" />
            <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Visitante" />
            <asp:BoundField DataField="GetDescricaoDepartamento" HeaderText="Departamento" />
            <asp:BoundField DataField="GetAceita" HeaderText="Aceita">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

