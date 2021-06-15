<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioControleDeViagens.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioControleDeViagens" %>

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
    Relatório - Controles de Viagens
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
            <td style="width: 20%; padding-right: 15px">
                <div class="labelRelatorio">
                    Data de
                </div>
                <asp:TextBox ID="tbxDataDe" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="width: 20%; padding-right: 15px">
                <div class="labelRelatorio">
                    até
                </div>
                <asp:TextBox ID="tbxDataAte" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="width: 20%; padding-right: 15px">
                <div class="labelRelatorio">
                    Responsável
                </div>
                <asp:DropDownList ID="ddlResponsavel" CssClass="dropDownFiltros" runat="server">
                </asp:DropDownList>
            </td>
            <td style="width: 20%; padding-right: 15px">
                <div class="labelRelatorio">
                    Motorista
                </div>
                <asp:DropDownList ID="ddlMotorista" CssClass="dropDownFiltros" runat="server">
                </asp:DropDownList>
            </td>
            <td style="width: 20%; padding-right: 15px">
                <div class="labelRelatorio">
                    Roteiro
                </div>
                <asp:DropDownList ID="ddlRoteiro" CssClass="dropDownFiltros" runat="server">
                    <asp:ListItem Selected="True" Value="">Todos</asp:ListItem>
                    <asp:ListItem Value="Castelo">Castelo</asp:ListItem>
                    <asp:ListItem Value="Intermunicipal">Intermunicipal</asp:ListItem>
                    <asp:ListItem Value="Interestadual">InterEstadual</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Departamento do Veículo
                </div>
                <asp:DropDownList ID="ddlDepartamentoVeiculo" CssClass="dropDownFiltros" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoVeiculo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Veículo
                </div>
                <asp:UpdatePanel runat="server" ID="upVeiculos" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlVeiculo" CssClass="dropDownFiltros" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoVeiculo" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Departamento que Utilizou
                </div>
                <asp:DropDownList ID="ddlDepartamentoUtilizou" CssClass="dropDownFiltros" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoUtilizou_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Setor que Utilizou
                </div>
                <asp:UpdatePanel runat="server" ID="upSetores" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlSetorUtilizou" CssClass="dropDownFiltros" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoUtilizou" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Abastecimento
                </div>
                <asp:DropDownList ID="ddlPossuiAbastecimento" CssClass="dropDownFiltros" runat="server">
                    <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Somente com Abastecimento</asp:ListItem>
                    <asp:ListItem Value="2">Somente sem Abastecimento</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="padding-right: 15px">
                <div style="margin-top: 10px; text-align: right">
                    <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" />
            <asp:BoundField DataField="GetDescricaoResponsavel" HeaderText="Responsável" />
            <asp:BoundField DataField="GetDescricaoMotorista" HeaderText="Motorista" />
            <asp:BoundField DataField="GetDescricaoVeiculo" HeaderText="Veículo" />
            <asp:BoundField DataField="GetDescricaoSetorUtilizou" HeaderText="Setor que Utilizou" />
            <asp:BoundField DataField="DataHoraSaida" HeaderText="Saída" DataFormatString="{0:G}" />
            <asp:BoundField DataField="QuilometragemSaida" HeaderText="Km de Saída" DataFormatString="{0:N1}" />
            <asp:BoundField DataField="GetDescricaoHoraChegada" HeaderText="Chegada" />
            <asp:BoundField DataField="GetDescricaoQuilometragemChegada" HeaderText="Km de Chegada" />
            <asp:TemplateField HeaderText="Permanência com o Veículo">
                <ItemTemplate>
                    <%#BindingTotalTempoPermanencia(Container.DataItem) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Abastecimento(s)">
                <ItemTemplate>
                    <%#Eval("GetDescricaoAbastecimentos") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GetKmRodados" HeaderText="Km Rodado" DataFormatString="{0:N1}" />
            <asp:BoundField DataField="GetTotalLitrosAbastecidos" HeaderText="Total Abastecido(lt)" DataFormatString="{0:N1}" />
            <asp:TemplateField HeaderText="Gasto Médio">
                <ItemTemplate>
                    <%#Eval("GetGastoMedio") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GetGastoTotal" HeaderText="Gasto Total" DataFormatString="{0:c}" />
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

