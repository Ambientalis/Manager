<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioDeFuncionarios.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioDeFuncionarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Relatório - Funcionários
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
                    Código:
                </div>
                <asp:TextBox ID="tbxCodigoPesquisa" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="width: 30%; padding-right: 15px">
                <div class="labelRelatorio">
                    Nome / Apelido
                </div>
                <asp:TextBox ID="tbxNomeApelidoPesquisa" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="width: 25%; padding-right: 15px">
                <div class="labelRelatorio">
                    CPF
                </div>
                <asp:TextBox ID="tbxCpfPesquisa" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="padding-right: 15px" colspan="2">
                <div class="labelRelatorio">
                    Status
                </div>
                <asp:DropDownList ID="ddlStatusPesquisa" CssClass="dropDownFiltros" runat="server">
                    <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                    <asp:ListItem Value="N">Todos</asp:ListItem>
                    <asp:ListItem Value="F">Inativos</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px" colspan="5">
                <table style="width: 100%; margin-right: 0px;">
                    <tr>
                        <td style="width: 30%; padding-right: 15px">
                            <div class="labelRelatorio">
                                Função
                            </div>
                            <asp:DropDownList ID="ddlFuncao" CssClass="dropDownFiltros" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 30%; padding-right: 15px">
                            <div class="labelRelatorio">
                                Vendedor
                            </div>
                            <asp:DropDownList ID="ddlVendendor" CssClass="dropDownFiltros" runat="server">
                                <asp:ListItem Value="0"> Todos os funcionários</asp:ListItem>
                                <asp:ListItem Value="1">Somente Vendedores</asp:ListItem>
                                <asp:ListItem Value="2">Somente os Não Vendedores</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;">
                            <div class="labelRelatorio">
                                &nbsp;
                            </div>
                            <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 12%; padding-right: 15px">&nbsp;</td>
            <td style="width: 40%; padding-right: 15px">&nbsp;</td>
            <td style="width: 20%; padding-right: 15px">&nbsp;</td>
            <td style="width: 15%; padding-right: 15px">&nbsp;</td>
            <td style="width: 15%; padding-right: 15px; text-align: right; padding-top: 5px;"></td>
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
            <asp:BoundField HeaderText="Nome" DataField="NomeRazaoSocial" />
            <asp:BoundField HeaderText="Apelido" DataField="ApelidoNomeFantasia" />
            <asp:BoundField HeaderText="CPF" DataField="GetCPFCNPJComMascara" />
            <asp:BoundField HeaderText="Funções (Cargo - Setor - Departamento)" DataField="GetDescricaoFuncoes">
                <HeaderStyle Width="40%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Unidade" DataField="GetDescricaoUnidade" />
            <asp:BoundField HeaderText="Vendedor" DataField="GetDescricaoVendedor" />
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

