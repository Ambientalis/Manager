<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Funcionarios.aspx.cs" Inherits="Funcionario_Funcionarios" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("funcionarios")) {
                    $(this).removeClass("funcionarios");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".funcionarios").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".funcionarios").children("a").children("span").addClass("bg_branco");
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
        Pesquisa de Funcionários
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('funcionarios_container');">
        </div>
    </div>
    <div id="funcionarios_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table align="center" width="100%">
                <tr>
                    <td style="width: 12%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Código
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxCodigoPesquisa" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 37%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Nome / Apelido
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxNomeApelidoPesquisa" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 17%; padding-right: 15px">
                        <div class="filtros_titulo">
                            CPF
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxCpfPesquisa" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Status
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlStatusPesquisa" CssClass="dropDownList100" runat="server">
                                <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                                <asp:ListItem Value="N">Todos</asp:ListItem>
                                <asp:ListItem Value="F">Inativos</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Função
                        </div>
                        <div class="filtros_campo">
                            <asp:UpdatePanel ID="upFuncoesPesqFuncionarios" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlFuncaoPesquisa" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td style="width: 20%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Vendedor
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlVendendor" CssClass="dropDownList100" runat="server">
                                <asp:ListItem Value="0"> Todos os funcionários</asp:ListItem>
                                <asp:ListItem Value="1">Somente Vendedores</asp:ListItem>
                                <asp:ListItem Value="2">Somente os Não Vendedores</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px; text-align: right;">
                         <div class="filtros_titulo">
                            &nbsp;
                        </div>
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upCamposFuncionarios" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvFuncionarios" CssClass="grid" runat="server" AutoGenerateColumns="False"
                    EnableModelValidation="True" AllowPaging="True" DataKeyNames="Id" OnRowDeleting="gdvFuncionarios_RowDeleting"
                    BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvFuncionarios_PageIndexChanging"
                    OnPreRender="gdvFuncionarios_PreRender">
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
                        <asp:BoundField HeaderText="Vendedor" DataField="GetDescricaoVendedor" />
                        <asp:BoundField HeaderText="Unidade" DataField="GetDescricaoUnidade" />
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
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPaginacao" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="gdvFuncionarios" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvFuncionarios" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

