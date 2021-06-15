<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisaDeControleDeViagens.aspx.cs" Inherits="ControleDeViagens_PesquisaDeControleDeViagens" %>

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
    <p>Pesquisa de Controle de Viagens</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('controles_viagem_container');">
        </div>
    </div>
    <div id="controles_viagem_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table width="100%">
                <tr>
                    <td style="width: 20%">
                        <div class="filtros_titulo">
                            Data de
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDataDe" CssClass="textBox100" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbxDataDe" Format="dd/MM/yyyy" />
                        </div>
                    </td>
                    <td style="width: 20%">
                        <div class="filtros_titulo">
                            até
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDataAte" CssClass="textBox100" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbxDataAte" Format="dd/MM/yyyy" />
                        </div>
                    </td>
                    <td style="width: 20%">
                        <div class="filtros_titulo">
                            Responsável
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlResponsavel" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 20%">
                        <div class="filtros_titulo">
                            Motorista
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlMotorista" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 20%">
                        <div class="filtros_titulo">
                            Roteiro
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlRoteiro" CssClass="dropDownList100" runat="server">
                                <asp:ListItem Selected="True" Value="">Todos</asp:ListItem>
                                <asp:ListItem Value="Castelo">Castelo</asp:ListItem>
                                <asp:ListItem Value="Intermunicipal">Intermunicipal</asp:ListItem>
                                <asp:ListItem Value="Interestadual">InterEstadual</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="filtros_titulo">
                            Departamento do Veículo
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlDepartamentoVeiculo" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoVeiculo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="filtros_titulo">
                            Veículo
                        </div>
                        <div class="filtros_campo">
                            <asp:UpdatePanel runat="server" ID="upVeiculos" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlVeiculo" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoVeiculo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td>
                        <div class="filtros_titulo">
                            Departamento que Utilizou
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlDepartamentoUtilizou" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoUtilizou_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="filtros_titulo">
                            Setor que Utilizou
                        </div>
                        <div class="filtros_campo">
                            <asp:UpdatePanel runat="server" ID="upSetores" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlSetorUtilizou" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoUtilizou" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td>
                        <div class="filtros_titulo">
                            Abastecimento
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlPossuiAbastecimento" CssClass="dropDownList100" runat="server">
                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Somente com Abastecimento</asp:ListItem>
                                <asp:ListItem Value="2">Somente sem Abastecimento</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div style="margin-top: 10px; text-align: right">
                            <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upControleViagens" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvControleViagens" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvControleViagens_PageIndexChanging" OnPreRender="gdvControleViagens_PreRender" PageSize="10" OnRowDeleting="gdvControleViagens_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="GetDescricaoResponsavel" HeaderText="Responsável" />
                        <asp:BoundField DataField="GetDescricaoMotorista" HeaderText="Motorista" />
                        <asp:BoundField DataField="GetDescricaoVeiculo" HeaderText="Veículo" />
                        <asp:BoundField DataField="GetDescricaoSetorUtilizou" HeaderText="Setor que Utilizou" />
                        <asp:BoundField DataField="DataHoraSaida" HeaderText="Data e Hora de Saída" DataFormatString="{0:G}" />
                        <asp:BoundField DataField="QuilometragemSaida" HeaderText="Quilometragem Saída" DataFormatString="{0:N1}" />
                        <asp:BoundField DataField="GetDescricaoHoraChegada" HeaderText="Data e Hora Chegada" />
                        <asp:BoundField DataField="GetDescricaoQuilometragemChegada" HeaderText="Quilometragem Chegada" />
                        <asp:TemplateField HeaderText="Abastecimento(s)">
                            <ItemTemplate>
                                <%#Eval("GetDescricaoAbastecimentos") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ações">
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
                <asp:AsyncPostBackTrigger ControlID="gdvControleViagens" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvControleViagens" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

