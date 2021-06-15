<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="Cliente_Clientes" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {


            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("clientes")) {
                    $(this).removeClass("clientes");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".clientes").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".clientes").children("a").children("span").addClass("bg_branco");
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
        <asp:Label runat="server" ID="lblTituloTela">Pesquisa de clientes</asp:Label>
        <asp:HiddenField runat="server" ID="hfTipoCarregado" Value="1" />
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:Label ID="lblImportacaoClientes" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblImportacaoClientes_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarImportacaoClientes"
        Enabled="True" PopupControlID="popImportacaoClientes" TargetControlID="lblImportacaoClientes">
    </asp:ModalPopupExtender>
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('pesquisa_container');">
        </div>
    </div>
    <div id="pesquisa_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table align="center" width="100%">
                <tr>
                    <td style="width: 10%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Código
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxCodigoPesquisa" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Razão Social / Nome Fantasia / Nome / Apelido
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxNomeRazaoApelidoPesquisa" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 20%; padding-right: 15px">
                        <div class="filtros_titulo">
                            CPF/CNPJ
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxCpfCnpjPesquisa" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px; text-align: right;">
                        <div class="filtros_titulo">
                            Origem
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlOrigem" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px; text-align: right;">
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
                    <td colspan="6" style="padding-right: 15px; text-align: right; padding-top: 5px;">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="padding-left: 20px; padding-right: 20px; margin-top: 20px; margin-bottom: 10px;">
        <asp:Button ID="btnImportarClientes" runat="server" CssClass="botao diminuir" Text="Importar" OnClick="btnImportarClientes_Click" />
    </div>
    <div style="position: relative;">
        <asp:UpdatePanel ID="upCamposClientes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvClientes" CssClass="grid" runat="server" AutoGenerateColumns="False"
                    EnableModelValidation="True" AllowPaging="True" DataKeyNames="Id" OnRowDeleting="gdrClientes_RowDeleting"
                    BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdrClientes_PageIndexChanging"
                    OnPreRender="gdvClientes_PreRender">
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="Codigo">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nome/Razão social" DataField="NomeRazaoSocial" />
                        <asp:BoundField HeaderText="Apelido/Nome fantasia" DataField="ApelidoNomeFantasia" />
                        <asp:BoundField HeaderText="CPF/CNPJ" DataField="GetCPFCNPJComMascara" />
                        <asp:BoundField HeaderText="Tipo" DataField="GetDescricaoTipo" />
                        <asp:BoundField HeaderText="Unidade" DataField="GetDescricaoUnidade" />
                        <asp:BoundField HeaderText="Origem" DataField="GetDescricaoOrigem" />
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
                <asp:AsyncPostBackTrigger ControlID="gdvClientes" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvClientes" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="popImportacaoClientes" class="pop_up" style="width: 550px;">
        <div class="barra">
            Importar Clientes
        </div>
        <div class="pop_m20">
            <asp:FileUpload ID="fulArquivoImportarClientes" class="botao" runat="server" />&nbsp;
            <asp:Button ID="btnCarregarArquivo" runat="server" CssClass="botao ok" Text="Carregar" OnClick="btnCarregarArquivo_Click" />
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <a id="cancelarImportacaoClientes" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>

