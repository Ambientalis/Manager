<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Pedidos.aspx.cs" Inherits="Pedidos_Pedidos" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("pedidos")) {
                    $(this).removeClass("pedidos");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".pedidos").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".pedidos").children("a").children("span").addClass("bg_branco");
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
        Pesquisa de Pedidos
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('pedidos_container');">
        </div>
    </div>
    <div id="pedidos_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table width="100%">
                <tr>
                    <td style="width: 33%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Código/Nº do orçamento
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxCodigoPesquisa" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 33%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Tipo de Pedido
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlTipoPedidoPesquisa" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 16%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Data de
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxDataDe" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                    </td>
                    <td style="width: 16%;">
                        <div class="filtros_titulo">
                            Até
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbxDataAte" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Cliente
                        </div>
                        <div class="filtros_campo">
                            <asp:UpdatePanel ID="upClientesPesquisa" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlClientePesquisa" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Vendedor
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlVendedorPesquisa" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Status
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlStatusPesquisa" CssClass="dropDownList100" runat="server">
                                <asp:ListItem Selected="True" Value="1">Somente Ativos</asp:ListItem>
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="2">Inativos</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="filtros_titulo">
                            Contrato fixo
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlContratoFixo" CssClass="dropDownList100" runat="server">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">COM contrato fixo</asp:ListItem>
                                <asp:ListItem Value="2">SEM contrato fixo</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: right; padding-top: 5px;">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upPedidos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="color: #e60000">
                    Com divergência de valores
                </div>
                <asp:GridView ID="gdvPedidos" CssClass="grid" runat="server" AutoGenerateColumns="False" PageSize="50"
                    EnableModelValidation="True" AllowPaging="True" DataKeyNames="Id" OnRowDeleting="gdvPedidos_RowDeleting" OnRowDataBound="gdvPedidos_RowDataBound"
                    BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvPedidos_PageIndexChanging"
                    OnPreRender="gdvPedidos_PreRender">
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="Codigo">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Data" DataField="Data" DataFormatString="{0:d}" />
                        <asp:BoundField HeaderText="Cliente" DataField="GetNomeCliente" />
                        <asp:BoundField HeaderText="Vendedor" DataField="GetNomeVendedor" />
                        <asp:BoundField HeaderText="Tipo" DataField="GetDescricaoTipo" />
                        <asp:BoundField HeaderText="OS's" DataField="GetNumerosOSs" />
                        <asp:BoundField HeaderText="Orçamento" DataField="GetNumerosOrcamento" />
                        <asp:BoundField HeaderText="Contrato" DataField="GetStatusContrato" />
                        <asp:TemplateField HeaderText="Editar / Excluir">
                            <ItemTemplate>
                                <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                    <asp:HyperLink ID="btnGridEditar" runat="server" CssClass="botao editar_mini" NavigateUrl="<%# BindEditar(Container.DataItem) %>" ToolTip="Editar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
                                </div>
                                <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">

                                    <asp:Button ID="btnGridExcluir" runat="server" CssClass="botao excluir_mini" Text=""
                                        CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluir_PreRender" />
                                </div>
                                <div style="clear: both; height: 1px;"></div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
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
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPaginacao" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="gdvPedidos" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvPedidos" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

