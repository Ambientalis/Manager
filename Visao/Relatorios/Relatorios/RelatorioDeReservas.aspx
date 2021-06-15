<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioDeReservas.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioDeReservas" %>

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
    Relatório - Reservas
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
            <td>
                <table align="center" width="100%">
                    <tr>
                        <td style="width: 50%; padding-right: 15px">
                            <div class="labelRelatorio">
                                Data de Início de
                            </div>
                            <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
                        </td>
                        <td style="width: 50%; padding-right: 15px">
                            <div class="labelRelatorio">
                                até
                            </div>
                            <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 33%; padding-right: 15px">
                <div class="labelRelatorio">
                    Descrição
                </div>
                <asp:TextBox ID="tbxDescricao" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="width: 33%; padding-right: 15px" colspan="2">
                <div class="labelRelatorio">
                    Tipo de Reserva
                </div>
                <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px;">
                <div class="labelRelatorio">
                    Veículo
                </div>
                <asp:DropDownList ID="ddlVeiculo" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="width: 33%; padding-right: 15px">
                <div class="labelRelatorio">
                    Responsável
                </div>
                <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="width: 18%; padding-right: 15px">
                <div class="labelRelatorio">
                    Status
                </div>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                    <asp:ListItem Value="A">Aprovadas</asp:ListItem>
                    <asp:ListItem Value="G">Aguardando Aprovação</asp:ListItem>
                    <asp:ListItem Value="E">Encerradas</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px; text-align: right;">
                <div class="labelRelatorio">
                    &nbsp;
                </div>
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
            <asp:BoundField DataField="GetDescricaoStatus" HeaderText="Status">
                <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="GetNomeTipo" HeaderText="Tipo" />
            <asp:BoundField DataField="GetDescricaoNomeCliente" HeaderText="Cliente" />
            <asp:BoundField DataField="GetDescricaoVeiculo" HeaderText="Veículo" />
            <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
            <asp:BoundField DataField="GetQuilometragem" HeaderText="Quilometragem (KM)" />
            <asp:BoundField DataField="GetConsumo" HeaderText="Consumo (Lts)" />
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

