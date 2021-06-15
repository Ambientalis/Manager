<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisaDeReservas.aspx.cs" Inherits="Reservas_PesquisaDeReservas" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("reservas")) {
                    $(this).removeClass("reservas");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".reservas").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".reservas").children("a").children("span").addClass("bg_branco");
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
        Pesquisa de Reservas
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('reservas_container');">
        </div>
    </div>
    <div id="reservas_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table width="100%">
                <tr>
                    <td>
                        <table align="center" width="100%">
                            <tr>
                                <td style="width: 50%; padding-right: 5%">
                                    <div class="filtros_titulo">
                                        Data de Início de
                                    </div>
                                    <div class="filtros_campo">
                                        <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBox100"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="tbxDataDe">
                                        </asp:CalendarExtender>
                                    </div>
                                </td>
                                <td style="width: 50%; padding-right: 5%">
                                    <div class="filtros_titulo">
                                        até
                                    </div>
                                    <div class="filtros_campo">
                                        <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBox100"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="tbxDataAte">
                                        </asp:CalendarExtender>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 33%;">
                        <div class="filtros_titulo">
                            Descrição
                        </div>
                        <div class="filtros_campo" style="padding-right: 5%;">
                            <asp:TextBox ID="tbxDescricao" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 33%" colspan="2">
                        <div class="filtros_titulo">
                            tipo de reserva
                        </div>
                        <div class="filtros_campo" style="padding-right: 15px;">
                            <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="filtros_titulo">
                            veículo
                        </div>
                        <div class="filtros_campo" style="padding-right: 11px;">
                            <asp:DropDownList ID="ddlVeiculo" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 33%">
                        <div class="filtros_titulo">
                            responsável
                        </div>
                        <div class="filtros_campo" style="padding-right: 5%;">
                            <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 18%">
                        <div class="filtros_titulo">
                            status
                        </div>
                        <div class="filtros_campo" style="padding-right: 5%;">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                                <asp:ListItem Value="A">Aprovadas</asp:ListItem>
                                <asp:ListItem Value="G">Aguardando Aprovação</asp:ListItem>
                                <asp:ListItem Value="E">Encerradas</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px; text-align: right;">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upReservas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvReservas" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvReservas_PageIndexChanging" OnPreRender="gdvReservas_PreRender" PageSize="10" OnRowDeleting="gdvReservas_RowDeleting">
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
                        <asp:TemplateField HeaderText="Editar / Excluir">
                            <ItemTemplate>
                                <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                    <asp:HyperLink ID="btnGridEditar" runat="server" CssClass="botao editar_mini" NavigateUrl="<%# BindEditar(Container.DataItem) %>" ToolTip="Editar" Style="display: inline-block; height: 16px; width: 16px;" Visible="<%#BindingVisivelEditReserva(Container.DataItem) %>" Enabled="<%#BindingVisivelEditReserva(Container.DataItem) %>"></asp:HyperLink>
                                </div>
                                <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                    <asp:Button ID="btnGridExcluir" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluir_PreRender" Visible="<%#BindingVisivelEditReserva(Container.DataItem) %>" Enabled="<%#BindingVisivelEditReserva(Container.DataItem) %>" />
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
                <asp:AsyncPostBackTrigger ControlID="gdvReservas" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvReservas" EventName="SelectedIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

