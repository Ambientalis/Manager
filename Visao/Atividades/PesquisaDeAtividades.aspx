<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisaDeAtividades.aspx.cs" Inherits="Atividades_PesquisaDeAtividades" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("atividades")) {
                    $(this).removeClass("atividades");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".atividades").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".atividades").children("a").children("span").addClass("bg_branco");
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
        Pesquisa de Atividades
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('visitas_container');">
        </div>
    </div>
    <div id="visitas_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table align="center" width="100%">
                <tr>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            OS
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxNumeroOs" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            &nbsp;Pedido
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxNumeroPedido" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td colspan="2" style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Descrição
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDescricao" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding-right: 15px" colspan="2">
                        <div class="filtros_titulo">
                            Cliente
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlClientePesquisa" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Data de
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="tbxDataDe">
                            </asp:CalendarExtender>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Até
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy HH:mm" TargetControlID="tbxDataAte">
                            </asp:CalendarExtender>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px;">
                        <div class="filtros_titulo">
                            Ordenação
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlOrdenacao" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Value="Crescente">Crescente</asp:ListItem>
                                <asp:ListItem Selected="True" Value="Decrescente">Decrescente</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Responsável
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlResponsavel" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Tipo
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlTipoAtividade" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Status
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlStatusVisita" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                                <asp:ListItem Value="N">Todos</asp:ListItem>
                                <asp:ListItem Value="F">Inativos</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding-right: 15px; text-align: right;">
                        <div class="filtros_campo" style="margin-top: 10px;">
                            <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upAtividades" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvAtividades" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvAtividades_PageIndexChanging" OnPreRender="gdvAtividades_PreRender" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="GetData" HeaderText="Data">
                            <HeaderStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GetNomeTipoAtividade" HeaderText="Tipo" />
                        <asp:BoundField DataField="GetNumeroOS" HeaderText="OS" />
                        <asp:BoundField DataField="GetNumeroPedido" HeaderText="Pedido" />
                        <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                        <asp:BoundField DataField="GetNomeCliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Executor" />
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:HyperLink ID="btnGridEditar" runat="server" CssClass="botao editar_mini" NavigateUrl="<%# BindEditar(Container.DataItem) %>" ToolTip="Editar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
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
                <asp:AsyncPostBackTrigger ControlID="gdvAtividades" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

