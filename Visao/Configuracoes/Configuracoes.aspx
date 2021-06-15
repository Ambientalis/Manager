<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="Configuracoes.aspx.cs" Inherits="Configuracoes_Configuracoes" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("configuracoes")) {
                    $(this).removeClass("configuracoes");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".configuracoes").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".configuracoes").children("a").children("span").addClass("bg_branco");
                }
            });
        });

        function MascaraPrazoPadr() {
            ///Máscaras         
            $("#<%= tbxPrazoPadraoTipoOS.ClientID %>").unbind();
            $('#<%=tbxPrazoPadraoTipoOS.ClientID %>').maskMoney({ thousands: '', decimal: '', allowZero: true });
        }

        function minimizar(a) {
            $('.tela_configuracoes .cph').each(function () {
                if ($(this).attr('id') != a)
                    $(this).hide({ height: 'toggle' });
            });

            $("#" + a + "").slideToggle({ height: 'toggle' });
        }
    </script>
    <style type="text/css">
        .tela_configuracoes .cph {
            display: none;
        }

            .tela_configuracoes .cph div {
                max-height: 350px;
                overflow-y: auto;
            }

                .tela_configuracoes .cph div:nth-child(3) table {
                    border: 1px solid #ccc;
                    padding: 10px;
                    border-radius: 3px;
                    background: #f1f1f1;
                }

        .tela_configuracoes .barra {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>Configurações</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="tela_configuracoes">
        <asp:LinkButton ID="btnExibirConfiguracaoGeral" runat="server" CssClass="barra" OnClientClick="minimizar('configuracao_geral');"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none" OnClick="btnExibirConfiguracaoGeral_Click">
            Configuração geral
            <div class="close"></div>
        </asp:LinkButton>
        <div id="configuracao_geral" class="cph">
            <div>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div>
                            <div class="label_form">
                                E-mails que recebem a confirmação de contratação (separar com ';')
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="txtEmailsAvisoContratacaoAceita" runat="server" CssClass="textBox100"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="label_form">
                                E-mails que recebem a confirmação orçamento (separar com ';')
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="txtEmailsAvisoConfirmacaoOrcamento" runat="server" CssClass="textBox100"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="label_form">
                                E-mails que recebem a confirmação de resposta de pesquisa de satisfação (separar com ';')
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="txtEmailsAvisoConfirmacaoRespostaPesquisaSatisfacao" runat="server" CssClass="textBox100"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <div class="label_form">
                                E-mails que recebem solicitação de liberação de despesa (separar com ;)
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="txtEmailsSolicitacaoLiberacaoDespesa" runat="server" CssClass="textBox100"></asp:TextBox>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarConfiguracoesGerais" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirConfiguracaoGeral" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div style="text-align: right; margin-top: 10px">
                    <asp:Button ID="btnSalvarConfiguracoesGerais" Width="150px" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvarConfiguracoesGerais_Click" />
                </div>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposPedidos" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_pedido_container');" OnClick="btnExibirTiposPedidos_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de Pedido
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_pedido_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposPedidos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvTiposPedido" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvTiposPedido_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvTiposPedido_PageIndexChanging" OnPreRender="gdvTiposPedido_PreRender"
                            PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoPedido" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoPedido_Click" OnInit="btnGridEditarTipoPedido_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluir" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluir_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposPedido" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposPedido" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposPedidos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoPedido" runat="server" Text="Novo Tipo de Pedido" CssClass="botao novo" OnClick="btnNovoTipoPedido_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoPedido" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_tipo_pedido" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbxNomeTipoPedido" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgTipoPedido">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeTipoPedido" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoPedido" runat="server" />
                                            <asp:Button ID="btnSalvarTipoPedido" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgTipoPedido" OnClick="btnSalvarTipoPedido_Click" OnInit="btnSalvarTipoPedido_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirTipoPedido" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirTipoPedido_Click" OnInit="btnExcluirTipoPedido_Init" OnPreRender="btnExcluirTipoPedido_PreRender" />
                                            <asp:Button ID="btnCancelarTipoPedido" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarTipoPedido_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTipoPedido" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirTipoPedido" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoPedido" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarTipoPedido" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposOS" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_os_container');" OnClick="btnExibirTiposOS_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de OS
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_os_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposOS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvTiposOs" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvTiposOs_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvTiposOs_PageIndexChanging" OnPreRender="gdvTiposOs_PreRender" PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:BoundField HeaderText="Prazo Padrão" DataField="PrazoPadrao" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoOS" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoOS_Click" OnInit="btnGridEditarTipoOS_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirTipoOs" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirTipoOs_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposOs" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposOs" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposOS" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoOS" runat="server" Text="Novo Tipo de OS" CssClass="botao novo" OnClick="btnNovoTipoOS_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoOS" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <script type="text/javascript">
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { MascaraPrazoPadr(); });
                        </script>
                        <div id="campos_cadastro_tipo_os" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 35%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNomeTipoOS" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgTipoOS">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeTipoOS" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 35%">
                                        <div class="label_form">
                                            Prazo Padrão (dias)<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxPrazoPadraoTipoOS" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Prazo Padrão" ValidationGroup="vlgTipoOS">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxPrazoPadraoTipoOS" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoOS" runat="server" />
                                            <asp:Button ID="btnSalvarTipoOS" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgTipoOS" OnClick="btnSalvarTipoOS_Click" OnInit="btnSalvarTipoOS_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirTipoOS" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirTipoOS_Click" OnInit="btnExcluirTipoOS_Init" OnPreRender="btnExcluirTipoOS_PreRender" />
                                            <asp:Button ID="btnCancelarTipoOS" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarTipoOS_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTipoOS" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirTipoOS" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoOS" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarTipoOS" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirOrgaos" runat="server" CssClass="barra" OnClientClick="minimizar('orgaos_container');" OnClick="btnExibirOrgaos_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Órgãos
            <div class="close"></div>
        </asp:LinkButton>
        <div id="orgaos_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upOrgaos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvOrgaos" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvOrgaos_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvOrgaos_PageIndexChanging" OnPreRender="gdvOrgaos_PreRender"
                            PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarOrgao" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarOrgao_Click" OnInit="btnGridEditarOrgao_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirOrgao" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirOrgao_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvOrgaos" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvOrgaos" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirOrgaos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoOrgao" runat="server" Text="Novo Órgão" CssClass="botao novo" OnClick="btnNovoOrgao_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadOrgao" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_orgao" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxNomeOrgao" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgOrgao">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeOrgao" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdOrgao" runat="server" />
                                            <asp:Button ID="btnSalvarOrgao" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgOrgao" OnClick="btnSalvarOrgao_Click" OnInit="btnSalvarOrgao_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirOrgao" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirOrgao_Click" OnInit="btnExcluirOrgao_Init" OnPreRender="btnExcluirOrgao_PreRender" />
                                            <asp:Button ID="btnCancelarOrgao" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarOrgao_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarOrgao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirOrgao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarOrgao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoOrgao" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposVisita" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_visita_container');" OnClick="btnExibirTiposVisita_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de Visita
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_visita_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposVisita" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvTiposVisita" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvTiposVisita_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvTiposVisita_PageIndexChanging" OnPreRender="gdvTiposVisita_PreRender"
                            PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoVisita" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoVisita_Click" OnInit="btnGridEditarTipoVisita_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirTipoVisita" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirTipoVisita_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposVisita" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposVisita" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposVisita" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoVisita" runat="server" Text="Novo Tipo de Visita" CssClass="botao novo" OnClick="btnNovoTipoVisita_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoVisita" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_tipo_visita" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxNomeTipoVisita" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgTipoVisita">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeTipoVisita" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoVisita" runat="server" />
                                            <asp:Button ID="btnSalvarTipoVisita" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgTipoVisita" OnClick="btnSalvarTipoVisita_Click" OnInit="btnSalvarTipoVisita_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirTipoVisita" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirTipoVisita_Click" OnInit="btnExcluirTipoVisita_Init" OnPreRender="btnExcluirTipoVisita_PreRender" />
                                            <asp:Button ID="btnCancelarTipoVisita" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarTipoVisita_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTipoVisita" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirTipoVisita" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarTipoVisita" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoVisita" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposAtividade" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_atividade_container');" OnClick="btnExibirTiposAtividade_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de Atividade
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_atividade_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposAtividade" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvTiposAtividade" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvTiposAtividade_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvTiposAtividade_PageIndexChanging" OnPreRender="gdvTiposAtividade_PreRender"
                            PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoAtividade" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoAtividade_Click" OnInit="btnGridEditarTipoAtividade_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirTipoAtividade" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirTipoAtividade_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposAtividade" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposAtividade" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposAtividade" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoAtividade" runat="server" Text="Novo Tipo de Atividade" CssClass="botao novo" OnClick="btnNovoTipoAtividade_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoAtividade" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_tipo_atividade" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxNomeTipoAtividade" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgTipoAtividade">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeTipoAtividade" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoAtividade" runat="server" />
                                            <asp:Button ID="btnSalvarAtividade" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgTipoAtividade" OnClick="btnSalvarAtividade_Click" OnInit="btnSalvarAtividade_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirAtividade" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirAtividade_Click" OnInit="btnExcluirAtividade_Init" OnPreRender="btnExcluirAtividade_PreRender" />
                                            <asp:Button ID="btnCancelarAtividade" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarAtividade_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarAtividade" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirAtividade" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarAtividade" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoAtividade" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposReservasVeiculos" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_reserva_container');" OnClick="btnExibirTiposReservasVeiculos_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de Reserva de Veículos
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_reserva_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposReserva" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvTiposReserva" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvTiposReserva_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvTiposReserva_PageIndexChanging" OnPreRender="gdvTiposReserva_PreRender"
                            PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoReserva" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoReserva_Click" OnInit="btnGridEditarTipoReserva_Init" Visible="<%#BindingVisivelEditTipoReserva(Container.DataItem) %>" Enabled="<%#BindingVisivelEditTipoReserva(Container.DataItem) %>" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirTipoReserva" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirTipoReserva_PreRender" Visible="<%#BindingVisivelEditTipoReserva(Container.DataItem) %>" Enabled="<%#BindingVisivelEditTipoReserva(Container.DataItem) %>" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposReserva" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposReserva" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposReservasVeiculos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoReserva" runat="server" Text="Novo Tipo de Reserva" CssClass="botao novo" OnClick="btnNovoTipoReserva_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoReserva" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_tipo_reserva" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Descrição<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxDescricaoTipoReserva" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgTipoReserva">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxDescricaoTipoReserva" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoReserva" runat="server" />
                                            <asp:Button ID="btnSalvarTipoReserva" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgTipoReserva" OnClick="btnSalvarTipoReserva_Click" OnInit="btnSalvarTipoReserva_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirTipoReserva" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirTipoReserva_Click" OnInit="btnExcluirTipoReserva_Init" OnPreRender="btnExcluirTipoReserva_PreRender" />
                                            <asp:Button ID="btnCancelarTipoReserva" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarTipoReserva_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTipoReserva" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirTipoReserva" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarTipoReserva" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoReserva" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposOcorrenciasVeiculos" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_ocorrencia_veic_container');" OnClick="btnExibirTiposOcorrenciasVeiculos_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de Ocorrência de Veículo
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_ocorrencia_veic_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposOcorrencias" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvTiposOcorrencias" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvTiposOcorrencias_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvTiposOcorrencias_PageIndexChanging" OnPreRender="gdvTiposOcorrencias_PreRender"
                            PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoOcorrencia" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoOcorrencia_Click" OnInit="btnGridEditarTipoOcorrencia_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirTipoOcorrencia" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirTipoOcorrencia_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposOcorrencias" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvTiposOcorrencias" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposOcorrenciasVeiculos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoOcorrencia" runat="server" Text="Novo Tipo de Ocorrência" CssClass="botao novo" OnClick="btnNovoTipoOcorrencia_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoOcorrencia" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_tipo_ocorrencia" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Descrição<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbxDescricaoTipoOcorrencia" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgTipoOcorrencia">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxDescricaoTipoOcorrencia" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoOcorrencia" runat="server" />
                                            <asp:Button ID="btnSalvarTipoOcorrencia" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgTipoOcorrencia" OnClick="btnSalvarTipoOcorrencia_Click" OnInit="btnSalvarTipoOcorrencia_Init" />&nbsp;
                                    <asp:Button ID="btnExcluirTipoOcorrencia" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirTipoOcorrencia_Click" OnInit="btnExcluirTipoOcorrencia_Init" OnPreRender="btnExcluirTipoOcorrencia_PreRender" />
                                            <asp:Button ID="btnCancelarTipoOcorrencia" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarTipoOcorrencia_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTipoOcorrencia" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirTipoOcorrencia" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarTipoOcorrencia" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoOcorrencia" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnClassificacoesTiposDespesas" runat="server" CssClass="barra" OnClientClick="minimizar('classificacoes');" OnClick="btnClassificacoesTiposDespesas_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Classificações dos tipos de despesas
            <div class="close"></div>
        </asp:LinkButton>
        <div id="classificacoes" class="cph">
            <div>
                <asp:UpdatePanel ID="upClassificacoes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="grvClassificacoes" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="grvClassificacoes_RowDeleting" BorderStyle="None" BorderWidth="0px">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarClassificacao" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarClassificacao_Click" OnInit="btnGridEditarClassificacao_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirClassificacao" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirClassificacao_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grvClassificacoes" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="btnClassificacoesTiposDespesas" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovaClassificacao" runat="server" Text="Nova classificação" CssClass="botao novo" OnClick="btnNovaClassificacao_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadastroClassificacao" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_classificacao" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtNomeClassificacao" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="rfvClassificacao">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="txtNomeClassificacao" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdClassificacao" runat="server" />
                                            <asp:Button ID="btnSalvarClassificacao" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="rfvClassificacao" OnClick="btnSalvarClassificacao_Click" OnInit="btnSalvarClassificacao_Init" />&nbsp;
                                            <asp:Button ID="btnExcluirClassficacao" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirClassficacao_Click" OnInit="btnExcluirClassficacao_Init" OnPreRender="btnExcluirClassficacao_PreRender" />
                                            <asp:Button ID="btnCancelarClassificacao" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarClassificacao_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarClassificacao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirClassficacao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarClassificacao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovaClassificacao" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirTiposDespesas" runat="server" CssClass="barra" OnClientClick="minimizar('tipos_despesas');" OnClick="btnExibirTiposDespesas_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Tipos de Despesa
            <div class="close"></div>
        </asp:LinkButton>
        <div id="tipos_despesas" class="cph">
            <div>
                <asp:UpdatePanel ID="upTiposDespesas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="grvTiposDespesas" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="grvTiposDespesas_RowDeleting" BorderStyle="None" BorderWidth="0px" PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Classificação" DataField="GetDescricaoClassificacao" />
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:BoundField HeaderText="Pré-aprovada" DataField="GetDescricaoPreAprovada" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarTipoDespesa" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarTipoDespesa_Click" OnInit="btnGridEditarTipoDespesa_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirTipoDespesa" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirTipoDespesa_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grvTiposDespesas" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="grvTiposDespesas" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirTiposDespesas" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoTipoDespesa" runat="server" Text="Novo Tipo de Despesa" CssClass="botao novo" OnClick="btnNovoTipoDespesa_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadTipoDespesa" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_tipo_depesa" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Classificação<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlClassificacaoTipoDespesa" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Classificação" InitialValue="0" ValidationGroup="rfvTipoDespesa">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList runat="server" ID="ddlClassificacaoTipoDespesa" CssClass="dropDownList100" />
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNomeTipoDespesa" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="rfvTipoDespesa">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="txtNomeTipoDespesa" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            &nbsp;
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:CheckBox runat="server" Text="Pré-aprovada" ID="chkTipoDespesaPreAprovada" />
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdTipoDespesa" runat="server" />
                                            <asp:Button ID="btnSalvarTipoDespesa" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="rfvTipoDespesa" OnClick="btnSalvarTipoDespesa_Click" OnInit="btnSalvarTipoDespesa_Init" />&nbsp;
                                            <asp:Button ID="btnExcluirTipoDespesa" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirTipoDespesa_Click" OnInit="btnExcluirTipoDespesa_Init" OnPreRender="btnExcluirTipoDespesa_PreRender" />
                                            <asp:Button ID="btnCancelarTipoDespesa" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarTipoDespesa_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarTipoDespesa" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirTipoDespesa" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarTipoDespesa" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoTipoDespesa" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirFormasPagamento" runat="server" CssClass="barra" OnClientClick="minimizar('formas_de_pagamento');" OnClick="btnExibirFormasPagamento_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Formas de Pagamento
            <div class="close"></div>
        </asp:LinkButton>
        <div id="formas_de_pagamento" class="cph">
            <div>
                <asp:UpdatePanel ID="upFormasDePagamento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="grvFormasDePagamento" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="grvFormasDePagamento_RowDeleting" BorderStyle="None" BorderWidth="0px" PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Tipo" DataField="Tipo" />
                                <asp:BoundField HeaderText="Acréscimo/desconto" DataField="GetDescricaoAcrescimoDesconto" />
                                <asp:BoundField HeaderText="Dias para primeiro pagamento" DataField="PrazoPrimeiroPagamento" />
                                <asp:BoundField HeaderText="Quantidade de vezes" DataField="QtdVezes" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarFormaPagamento" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarFormaPagamento_Click" OnInit="btnGridEditarFormaPagamento_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirFormaPagamento" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirFormaPagamento_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grvFormasDePagamento" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="grvFormasDePagamento" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirFormasPagamento" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovaFormaPagamento" runat="server" Text="Nova Forma de Pagamento" CssClass="botao novo" OnClick="btnNovaFormaPagamento_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadFormaPagamento" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_forma_pagamento" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Tipo
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList runat="server" ID="ddlTipoFormaPagamento" CssClass="dropDownList100">
                                                <asp:ListItem Text="Boleto" Value="Boleto"></asp:ListItem>
                                                <asp:ListItem Text="Cartão" Value="Cartão"></asp:ListItem>
                                                <asp:ListItem Text="Dinheiro" Value="Dinheiro"></asp:ListItem>
                                                <asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>
                                                <asp:ListItem Text="Depósito em conta corrente" Value="Depósito em conta corrente"></asp:ListItem>
                                                <asp:ListItem Text="Débito em conta corrente" Value="Débito em conta corrente"></asp:ListItem>
                                                <asp:ListItem Text="Depósito em conta salário" Value="Depósito em conta salário"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Acréscimo/Desconto (%)
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <div style="float: left">
                                                <asp:TextBox runat="server" ID="txtAcrescimoDescontoFormaPagamento" CssClass="textBox100 mascara_uma_casa_decimal">
                                                </asp:TextBox>
                                            </div>
                                            <div style="float: left">
                                                <asp:DropDownList runat="server" ID="ddlTipoAcrescimoDescontoFormaPagamento" CssClass="dropDownList100">
                                                    <asp:ListItem Text="Acréscimo" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Desconto" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Dias para primeiro pagamento
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox runat="server" ID="txtDiasPrimeiroPagamentoFormaPagamento" CssClass="textBox100 mascara_inteiro">
                                            </asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Quantidade de vezes
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox runat="server" ID="txtQtdVezesFormaPagamento" CssClass="textBox100 mascara_inteiro">
                                            </asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdFormaPagamento" runat="server" />
                                            <asp:Button ID="btnSalvarFormaPagamento" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvarFormaPagamento_Click" OnInit="btnSalvarFormaPagamento_Init" />&nbsp;
                                            <asp:Button ID="btnExcluirFormaPagamento" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirFormaPagamento_Click" OnInit="btnExcluirFormaPagamento_Init" OnPreRender="btnExcluirFormaPagamento_PreRender" />
                                            <asp:Button ID="btnCancelarFormaPagamento" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarFormaPagamento_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarFormaPagamento" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirFormaPagamento" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarFormaPagamento" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovaFormaPagamento" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirDepartamentos" runat="server" CssClass="barra" OnClientClick="minimizar('departamentos');" OnClick="btnExibirDepartamentos_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Departamentos
            <div class="close"></div>
        </asp:LinkButton>
        <div id="departamentos" class="cph">
            <div>
                <asp:UpdatePanel ID="upDepartamentos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="grvDepartamentos" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="grvDepartamentos_RowDeleting" BorderStyle="None" BorderWidth="0px" PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Unidade" DataField="GetUnidade" />
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarDepartamento" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarDepartamento_Click" OnInit="btnGridEditarDepartamento_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirDepartamento" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirDepartamento_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grvDepartamentos" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="grvDepartamentos" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirDepartamentos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoDepartamento" runat="server" Text="Novo departamento" CssClass="botao novo" OnClick="btnNovoDepartamento_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadDepartamento" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_departamento" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Unidade<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlUnidadeDepartamento" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Unidade" ValidationGroup="rfvDepartamento">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList runat="server" ID="ddlUnidadeDepartamento" CssClass="dropDownList100">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNomeDepartamento" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="rfvDepartamento">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox runat="server" ID="txtNomeDepartamento" CssClass="textBox100">
                                            </asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdDepartamento" runat="server" />
                                            <asp:Button ID="btnSalvarDepartamento" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="rfvDepartamento" OnClick="btnSalvarDepartamento_Click" OnInit="btnSalvarDepartamento_Init" />&nbsp;
                                            <asp:Button ID="btnExcluirDepartamento" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirDepartamento_Click" OnInit="btnExcluirDepartamento_Init" OnPreRender="btnExcluirDepartamento_PreRender" />
                                            <asp:Button ID="btnCancelarDepartamento" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarDepartamento_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarDepartamento" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirDepartamento" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarDepartamento" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoDepartamento" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirSetores" runat="server" CssClass="barra" OnClientClick="minimizar('setores');" OnClick="btnExibirSetores_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Setores
            <div class="close"></div>
        </asp:LinkButton>
        <div id="setores" class="cph">
            <div>
                <asp:UpdatePanel ID="upSetores" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="grvSetores" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="grvSetores_RowDeleting" BorderStyle="None" BorderWidth="0px" PageSize="25">
                            <Columns>
                                <asp:BoundField HeaderText="Unidade" DataField="GetUnidade" />
                                <asp:BoundField HeaderText="Departamento" DataField="GetNomeDepartamento" />
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarSetor" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarSetor_Click" OnInit="btnGridEditarSetor_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirSetor" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirSetor_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grvSetores" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="grvSetores" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirSetores" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoSetor" runat="server" Text="Novo setor" CssClass="botao novo" OnClick="btnNovoSetor_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCadSetor" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_setor" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Unidade
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList runat="server" ID="ddlUnidadeSetor" CssClass="dropDownList100" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidadeSetor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Departamento<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDepartamentoSetor" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Departamento" ValidationGroup="rfvSetor">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:UpdatePanel runat="server" ID="upDepartamentoSetor" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlDepartamentoSetor" CssClass="dropDownList100">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlUnidadeSetor" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNomeSetor" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="rfvSetor">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox runat="server" ID="txtNomeSetor" CssClass="textBox100">
                                            </asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdSetor" runat="server" />
                                            <asp:Button ID="btnSalvarSetor" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="rfvSetor" OnClick="btnSalvarSetor_Click" OnInit="btnSalvarSetor_Init" />&nbsp;
                                            <asp:Button ID="btnExcluirSetor" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirSetor_Click" OnInit="btnExcluirSetor_Init" OnPreRender="btnExcluirSetor_PreRender" />
                                            <asp:Button ID="btnCancelarSetor" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarSetor_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarSetor" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirSetor" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarSetor" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoSetor" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <asp:LinkButton ID="btnExibirOrigem" runat="server" CssClass="barra" OnClientClick="minimizar('origens_container');" OnClick="btnExibirOrigem_Click"
            Style="border-radius: 0px !important; border: 0px; display: block; text-decoration: none">
            Origens de clientes
            <div class="close"></div>
        </asp:LinkButton>
        <div id="origens_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upOrigens" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:GridView ID="gdvOrigens" CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" DataKeyNames="Id"
                            OnRowDeleting="gdvOrigens_RowDeleting" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvOrigens_PageIndexChanging" OnPreRender="gdvOrigens_PreRender"
                            PageSize="50">
                            <Columns>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarOrigem" runat="server" CssClass="botao editar_mini" Text="" CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarOrigem_Click" OnInit="btnGridEditarOrigem_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridExcluirOrigem" runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirOrigem_PreRender" />
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
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gdvOrigens" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvOrigens" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="btnExibirOrigem" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovaOrigem" runat="server" Text="Nova origem de cliente" CssClass="botao novo" OnClick="btnNovaOrigem_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upNovaOrigem" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_origem" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNomeOrigem" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgOrigem">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="txtNomeOrigem" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td valign="bottom">
                                        <div class="label_form"></div>
                                        <div class="campo_form" style="padding-right: 5%; vertical-align: bottom;">
                                            <asp:HiddenField ID="hfIdOrigem" runat="server" />
                                            <asp:Button ID="btnSalvarOrigem" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgOrigem" OnClick="btnSalvarOrigem_Click" OnInit="btnSalvarOrigem_Init" />&nbsp;
                                            <asp:Button ID="btnExcluirOrigem" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirOrigem_Click" OnInit="btnExcluirOrigem_Init" OnPreRender="btnExcluirOrigem_PreRender" />
                                            <asp:Button ID="btnCancelarOrigem" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarOrigem_Click" />&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarOrigem" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirOrigem" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarOrigem" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovaOrigem" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

