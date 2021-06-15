<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioDeVeiculos.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioDeVeiculos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" Runat="Server">
    Relatório - Veículos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" Runat="Server">
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
            <td style="width: 12%; padding-right: 15px">
                <div class="labelRelatorio">
                    Placa
                </div>
                <asp:TextBox ID="tbxPlaca" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="width: 35%; padding-right: 15px">
                <div class="labelRelatorio">
                    Descrição
                </div>
                <asp:TextBox ID="tbxDescricao" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Gestor
                </div>
                <asp:DropDownList ID="ddlGestor" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Status
                </div>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                    <asp:ListItem Value="N">Todos</asp:ListItem>
                    <asp:ListItem Value="F">Inativos</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>            
            <td style="padding-right: 15px" colspan="2">
                <div class="labelRelatorio">
                    Departamento Responsável
                </div>
                <asp:DropDownList ID="ddlDepartamentoResponsavel" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                &nbsp;</td>
            <td style="padding-right: 15px; text-align: right;">
                <div style="margin-top:10px;">
                    <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" />
                </div>
            </td>
        </tr>        
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt">
        <Columns>
            <asp:BoundField DataField="Placa" HeaderText="Placa" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="GetNomeGestor" HeaderText="Gestor" />
            <asp:BoundField DataField="GetNomeDepartamentoResponsavel" HeaderText="Departamento Responsável" />
            <asp:BoundField DataField="GetDescDepartamentosUtilizam" HeaderText="Departamentos que podem utilizar" />
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

