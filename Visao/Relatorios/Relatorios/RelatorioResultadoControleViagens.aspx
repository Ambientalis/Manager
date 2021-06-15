<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioResultadoControleViagens.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioResultadoControleViagens" %>

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

        .elemento_resultado
        {
            margin-bottom: 10px;
        }

        .detalhamento_resultado
        {
            padding: 5px;
            font-size: 11pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Relatório - Resultado do Controle de Viagens
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" runat="Server">
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
    <hr />
    <div style="margin-top: 15px">
        <asp:Repeater runat="server" ID="rptResultados">
            <ItemTemplate>
                <div class="elemento_resultado">
                    <div>
                        <strong><%# DataBinder.Eval(Container.DataItem, "Nome") %></strong>
                    </div>
                    <div class="detalhamento_resultado">
                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# DataBinder.Eval(Container.DataItem, "Detalhamento") %>'>
                            <ItemTemplate>
                                <div>
                                    <%# DataBinder.Eval(Container.DataItem, "DescricaoDetalhamento") %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

