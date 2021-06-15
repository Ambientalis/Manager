<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisaDeVeiculos.aspx.cs" Inherits="Veiculos_PesquisaDeVeiculos" %>

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
        Pesquisa de Veículos
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('veiculos_container');">
        </div>
    </div>
    <div id="veiculos_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table align="center" width="100%">
                <tr>
                    <td style="width: 12%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Placa
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxPlaca" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 35%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Descrição
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDescricao" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            GESTOR
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlGestor" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Status
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                                <asp:ListItem Value="N">Todos</asp:ListItem>
                                <asp:ListItem Value="F">Inativos</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Departamento Responsável
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlDepartamentoResponsavel" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="padding-right: 15px">&nbsp;</td>
                    <td style="padding-right: 15px; text-align: right;">
                        <div style="margin-top: 10px;">
                            <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upVeiculos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvVeiculos" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvVeiculos_PageIndexChanging" OnPreRender="gdvVeiculos_PreRender" PageSize="10" OnRowDeleting="gdvVeiculos_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Placa" HeaderText="Placa" />
                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                        <asp:BoundField DataField="GetNomeGestor" HeaderText="Gestor" />
                        <asp:BoundField DataField="GetNomeDepartamentoResponsavel" HeaderText="Departamento Responsável" />
                        <asp:BoundField DataField="GetDescDepartamentosUtilizam" HeaderText="Departamentos que podem utilizar" />
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                    <asp:HyperLink ID="btnGridEditar" runat="server" CssClass="botao editar_mini" NavigateUrl="<%# BindEditar(Container.DataItem) %>" ToolTip="Editar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
                                </div>
                                <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                    <asp:Button ID="btnGridExcluir" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluir_PreRender" />
                                </div>
                                <div style="clear: both; height: 1px;"></div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                </asp:GridView>
                <br />
                <br />
                <br />
                <div class="quantidade_pesquisada filtros_titulo">
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                </div>
                <div class="quantidades_pagina">
                    <asp:DropDownList ID="ddlPaginacao" CssClass="dropDownListAuto" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPaginacao_SelectedIndexChanged" ToolTip="Resultados por página">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPaginacao" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="gdvVeiculos" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvVeiculos" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

