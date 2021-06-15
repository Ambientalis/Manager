<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Permissoes.aspx.cs" Inherits="Permissoes_Permissoes" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("permissoes")) {
                    $(this).removeClass("permissoes");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".permissoes").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".permissoes").children("a").children("span").addClass("bg_branco");
                }
            });
        });

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }
    </script>

    <style type="text/css">
        .ckbTela tr td {
            margin: 4px;
            height: 20px;
            line-height: 1px;
        }

        .item_permissao {
            padding: 10px;
            border-radius: 3px;
            margin-bottom: 10px;
            background: #f4f5f4;
        }

            .item_permissao .titulo_funcao {
                padding: 5px 0px;
            }

                .item_permissao .titulo_funcao > span {
                    font-size: 1.3em;
                }

            .item_permissao .permissoes_funcao {
                margin-top: 10px;
                text-align: center;
            }

                .item_permissao .permissoes_funcao .item_permissao_funcao {
                    text-align: left;
                    border: 1px solid #ccc;
                    border-radius: 3px;
                    display: inline-block;
                    width: 32.3%;
                    margin-right: 0.5%;
                    vertical-align: top;
                    background: #fff;
                }

        .item_permissao_funcao .titulo_funcao {
            text-align: center;
            font-size: 1.2em;
            padding-bottom: 5px;
            border-bottom: 1px solid #ccc;
            background: #d5d5d5;
        }

        .item_permissao .permissao_funcao {
            padding: 10px;
        }

            .item_permissao .permissao_funcao > div {
                border-bottom: 1px solid #ccc;
                margin: 5px 0px;
                padding: 5px;
                position: relative;
            }

        .gerencia_os > div {
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    Controle de Permissões
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
            <div style="padding-left: 20px; padding-right: 20px;">
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 23%; padding-right: 15px">
                                <div class="filtros_titulo">
                                    Departamento
                                </div>
                                <div class="filtros_campo">
                                    <asp:DropDownList ID="ddlDepartamento" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 23%; padding-right: 15px">
                                <div class="filtros_titulo">
                                    Cargo
                                </div>
                                <div class="filtros_campo">
                                    <asp:DropDownList ID="ddlCargo" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 23%; padding-right: 15px">
                                <div class="filtros_titulo">
                                    Usuário
                                </div>
                                <div class="filtros_campo">
                                    <asp:DropDownList ID="ddlUsuario" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 15%; padding-right: 15px">
                                <div class="filtros_titulo">
                                    Status
                                </div>
                                <div class="filtros_campo">
                                    <asp:DropDownList ID="ddlStatus" CssClass="dropDownList100" runat="server">
                                        <asp:ListItem Selected="True" Value="T"> Somente Ativos</asp:ListItem>
                                        <asp:ListItem Value="N">Todos</asp:ListItem>
                                        <asp:ListItem Value="F">Inativos</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 15%; padding-right: 5px; text-align: right;" valign="bottom">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 15px;">
        <asp:UpdatePanel ID="upAux" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate></ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="upPermissoes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <span style="display: block; text-align: center; font-size: 1.5em;">
                    <asp:Label runat="server" ID="lblStatusPesquisa" />
                </span>
                <asp:Repeater runat="server" ID="rptPermissoes">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfIdPermissao" runat="server" Value='<%# Eval("Id") %>' />
                        <div class="item_permissao">
                            <div class="titulo_funcao">
                                Usuário: <span><%# Eval("GetNomeUsuario") %></span>
                                <br />
                                Unidade: <span><%# Eval("GetNomeUnidade") %></span><br />
                                Departamento: <span><%# Eval("GetNomeDepartamento") %></span><br />
                                Setor: <span><%# Eval("GetNomeSetor") %></span><br />
                                Função: <span><%# Eval("GetNomeFuncao") %></span><br />
                            </div>
                            <div class="permissoes_funcao">
                                <div class="item_permissao_funcao">
                                    <div class="titulo_funcao">
                                         <i class="fa fa-cog"></i>&nbsp;Acesso às telas do sistema
                                    </div>
                                    <div class="permissao_funcao">
                                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# BindingTelas(Container.DataItem) %>'>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:CheckBox runat="server" AutoPostBack="true" OnInit="chkTela_Init" OnCheckedChanged="chkTela_CheckedChanged" title='<%# Eval("ToolTip") %>'
                                                        Checked='<%# BindingTelaChecked(Container.DataItem) %>' ID="chkTela" ValidationGroup='<%# Eval("Id") %>' Text='<%# Eval("Nome") %>' />
                                                    <div style="position: absolute; right: 10px; bottom: 3px;" >
                                                        <asp:Label runat="server" Visible='<%# Eval("TelaFinanceiro") %>' ToolTip="Módulo financeiro"
                                                            CssClass="fa fa-dollar fa-border" style="font-size: 1.5em; cursor: help" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="item_permissao_funcao">
                                    <div class="titulo_funcao" style="background: #9ccddc;">
                                         <i class="fa fa-print"></i>&nbsp;Acesso aos relatórios do sistema
                                    </div>
                                    <div class="permissao_funcao">
                                        <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# BindingRelatorios(Container.DataItem) %>'>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:CheckBox runat="server" AutoPostBack="true" OnInit="chkTela_Init" OnCheckedChanged="chkTela_CheckedChanged" title='<%# Eval("ToolTip") %>'
                                                        Checked='<%# BindingTelaChecked(Container.DataItem) %>' ID="chkRelatorio" ValidationGroup='<%# Eval("Id") %>' Text='<%# Eval("Nome") %>' />
                                                    <div style="position: absolute; right: 10px; bottom: 3px;" >
                                                        <asp:Label runat="server" Visible='<%# Eval("TelaFinanceiro") %>' ToolTip="Módulo financeiro"
                                                            CssClass="fa fa-dollar fa-border" style="font-size: 1.5em; cursor: help" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <h2 style="margin:10px 0px">Gráficos</h2>
                                        <asp:Repeater ID="Repeater3" runat="server" DataSource='<%# BindingRelatoriosGraficos(Container.DataItem) %>'>
                                            <ItemTemplate>
                                                <div>
                                                    <asp:CheckBox runat="server" AutoPostBack="true" OnInit="chkTela_Init" OnCheckedChanged="chkTela_CheckedChanged" title='<%# Eval("ToolTip") %>'
                                                        Checked='<%# BindingTelaChecked(Container.DataItem) %>' ID="chkRelatorio" ValidationGroup='<%# Eval("Id") %>' Text='<%# Eval("Nome") %>' />
                                                    <div style="position: absolute; right: 10px; bottom: 3px;" >
                                                        <asp:Label runat="server" Visible='<%# Eval("TelaFinanceiro") %>' ToolTip="Módulo financeiro"
                                                            CssClass="fa fa-dollar fa-border" style="font-size: 1.5em; cursor: help" />
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="item_permissao_funcao">
                                    <div class="titulo_funcao" style="background: #9cdcad;">
                                         <i class="fa fa-sliders"></i>&nbsp;Gerência
                                    </div>
                                    <div class="permissao_funcao gerencia_os">
                                        <div>
                                            <div class="filtros_titulo">
                                                Acesso às OS's
                                            </div>
                                            <asp:DropDownList ID="ddlAcessoAsOSs" CssClass="dropDownList100" runat="server" SelectedValue='<%# BindSelectValueAcessoOs(Container.DataItem) %>'
                                                OnInit="ddlGerenciaOS_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlAcessoAsOSs_SelectedIndexChanged">
                                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                                <asp:ListItem Value="R">Sob sua responsabilidade</asp:ListItem>
                                                <asp:ListItem Value="S">Todas do Setor</asp:ListItem>
                                                <asp:ListItem Value="D">Todas do Departamento</asp:ListItem>
                                                <asp:ListItem Value="T">Todas da Empresa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <div class="filtros_titulo">
                                                Aprova OS's
                                            </div>
                                            <asp:DropDownList ID="ddlAprovaOSs" CssClass="dropDownList100" runat="server" SelectedValue='<%# BindSelectValueAprovaOs(Container.DataItem) %>'
                                                OnInit="ddlGerenciaOS_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlAprovaOSs_SelectedIndexChanged">
                                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                                <asp:ListItem Value="R">Sob sua responsabilidade</asp:ListItem>
                                                <asp:ListItem Value="S">Todas do Setor</asp:ListItem>
                                                <asp:ListItem Value="D">Todas do Departamento</asp:ListItem>
                                                <asp:ListItem Value="T">Todas da Empresa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <div class="filtros_titulo">
                                                Aprova despesa
                                            </div>
                                            <asp:DropDownList ID="ddlAprovaDespesa" CssClass="dropDownList100" runat="server" SelectedValue='<%# BindSelectValueAprovaDespesa(Container.DataItem) %>'
                                                OnInit="ddlGerenciaOS_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlAprovaDespesa_SelectedIndexChanged">
                                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                                <asp:ListItem Value="S">Todas do Setor</asp:ListItem>
                                                <asp:ListItem Value="D">Todas do Departamento</asp:ListItem>
                                                <asp:ListItem Value="T">Todas da Empresa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <div class="filtros_titulo">
                                                Pode Adiar Prazo Legal das OS's
                                            </div>
                                            <asp:DropDownList ID="ddlAdiaPrazoLegalOSs" CssClass="dropDownList100" runat="server" SelectedValue='<%# BindSelectValueAdiaPrazoLegalOs(Container.DataItem) %>'
                                                OnInit="ddlGerenciaOS_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlAdiaPrazoLegalOSs_SelectedIndexChanged">
                                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                                <asp:ListItem Value="R">Apenas as que ele é responsável</asp:ListItem>
                                                <asp:ListItem Value="S">Todas do Setor</asp:ListItem>
                                                <asp:ListItem Value="D">Todas do Departamento</asp:ListItem>
                                                <asp:ListItem Value="T">Todas da Empresa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <div class="filtros_titulo">
                                                Pode Adiar Prazo Diretoria das OS's
                                            </div>
                                            <asp:DropDownList ID="ddlAdiaPrazoDiretoriaOSs" CssClass="dropDownList100" runat="server" SelectedValue='<%# BindSelectValueAdiaPrazoDiretoriaOs(Container.DataItem) %>'
                                                OnInit="ddlGerenciaOS_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlAdiaPrazoDiretoriaOSs_SelectedIndexChanged">
                                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                                <asp:ListItem Value="R">Sob sua responsabilidade</asp:ListItem>
                                                <asp:ListItem Value="S">Todas do Setor</asp:ListItem>
                                                <asp:ListItem Value="D">Todas do Departamento</asp:ListItem>
                                                <asp:ListItem Value="T">Todas da Empresa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div>
                                            <div class="filtros_titulo">
                                                Pode visualizar o Controle de Viagem
                                            </div>
                                            <asp:DropDownList ID="ddlVisualizaControleViagens" CssClass="dropDownList100" runat="server" SelectedValue='<%# BindSelectValueVisualizaControleViagens(Container.DataItem) %>'
                                                OnInit="ddlGerenciaOS_Init" AutoPostBack="true" OnSelectedIndexChanged="ddlVisualizaControleViagens_SelectedIndexChanged">
                                                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                                                <asp:ListItem Value="R">Sob sua responsabilidade</asp:ListItem>
                                                <asp:ListItem Value="S">Todas do Setor</asp:ListItem>
                                                <asp:ListItem Value="D">Todas do Departamento</asp:ListItem>
                                                <asp:ListItem Value="T">Todas da Empresa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

