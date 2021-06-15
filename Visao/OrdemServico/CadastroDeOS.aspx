<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroDeOS.aspx.cs" Inherits="OrdemServico_CadastroDeOS" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("ordens_de_servico")) {
                    $(this).removeClass("ordens_de_servico");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".ordens_de_servico").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".ordens_de_servico").children("a").children("span").addClass("bg_branco");
                }
            });

            $("#<%= lkbExibirDadosVisita.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirDadosVisita.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirReservaVeiculoVisita.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirReservaVeiculoVisita.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirArquivosVisita.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirArquivosVisita.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= btnNovaVisita.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirDadosVisita.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirDadosAtividade.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirDadosAtividade.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirArquivosAtividade.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirArquivosAtividade.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= btnNovaAtividade.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirDadosAtividade.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

        });

        function SelecionarPrimeiraTab() {
            MudarClassesTabs();
            $("#<%= lkbExibirDadosVisita.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
        }

        function SelecionarPrimeiraTabAtividades() {
            MudarClassesTabs();
            $("#<%= lkbExibirDadosAtividade.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
        }

        function MudarClassesTabs() {

            $('.link_aba_escolhida').removeClass('link_aba_escolhida').addClass('link_aba');
        }

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }

    </script>
    <style type="text/css">
        .ajax__calendar {
            position: fixed !important;
            left: initial !important;
            top: initial !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Cadastro de Ordem de Serviço
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:Label ID="lblHistoricoDetalhamentos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblHistoricoDetalhamentos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarHistoricoDetalhamentos"
        Enabled="True" PopupControlID="Popup_Historico_Detalhamentos" TargetControlID="lblHistoricoDetalhamentos">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblCadastroVisita" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroVisita_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_cad_visita"
        Enabled="True" PopupControlID="Popup_Visita" TargetControlID="lblCadastroVisita">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblCadastroAtividade" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroAtividade_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_cad_atividade"
        Enabled="True" PopupControlID="PopAtividades" TargetControlID="lblCadastroAtividade">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblUploadArquivos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblUploadArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarUploadArquivos"
        Enabled="True" PopupControlID="popArquivos" TargetControlID="lblUploadArquivos">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblRenomearArquivos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblRenomearArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_renomear_arquivo"
        Enabled="True" PopupControlID="popRenomearArquivo" TargetControlID="lblRenomearArquivos">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblSolicitarAdiamentoPrazo" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblSolicitarAdiamentoPrazo_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_solicitar_adiamento"
        Enabled="True" PopupControlID="PopSolicitacaoAdiamento" TargetControlID="lblSolicitarAdiamentoPrazo">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblEncerramentoOS" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblEncerramentoOS_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_encerramento_os"
        Enabled="True" PopupControlID="PopEncerramentoOS" TargetControlID="lblEncerramentoOS">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblSolicitarAprovacao" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblSolicitarAprovacao_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_solicitacao_aprovacao"
        Enabled="True" PopupControlID="PopSolicitacaoAprovacao" TargetControlID="lblSolicitarAprovacao">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblAprovacaoOS" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblAprovacaoOS_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_aprovar_os"
        Enabled="True" PopupControlID="PopAprovacaoOS" TargetControlID="lblAprovacaoOS">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblEmailsAceiteVisita" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblEmailsAceiteVisita_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarEmailAceiteVisita"
        Enabled="True" PopupControlID="PopEmailsAceiteVisita" TargetControlID="lblEmailsAceiteVisita">
    </asp:ModalPopupExtender>
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioOS" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="float: left; width: 48%; margin-right: 10px;">
                    <div class="barra">
                        Dados Básicos
                    </div>
                    <div class="cph">
                        <div style="display: block;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 50%">
                                                    <div class="campo_form" style="vertical-align: bottom;">
                                                        <asp:CheckBox ID="chkOSAtivo" runat="server" Text="Ativa" Checked="True" Enabled="false" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="campo_form" style="vertical-align: bottom;">
                                                        <asp:CheckBox ID="chkOsComFaturamento" runat="server" Text="OS com Faturamento" Checked="True" Enabled="false" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 50%; padding-right: 15px">
                                                    <div class="label_form">
                                                        Código
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxCodigoOS" CssClass="textBox100" runat="server" Text="Gerado automáticamente" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="label_form">
                                                        Data
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxDataOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="label_form">
                                            Cliente
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="txtCliente" CssClass="textBox100" runat="server" Text="Carregado automaticamente..." ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="label_form">
                                            Pedido
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxPedidoOS" CssClass="textBox100" runat="server" Text="Carregado automaticamente..." ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Tipo de OS<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlTipoOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Data" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form">
                                            <asp:DropDownList ID="ddlTipoOS" CssClass="dropDownList100" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Descrição<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="tbxDescricaoOS" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Data" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxDescricaoOS" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 33%;">
                                                    <div class="label_form">
                                                        Prazo Padrão<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="tbxPrazoPadraoOS" CssClass="RequireFieldValidator"
                                                            ErrorMessage="- Data" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 7%;">
                                                        <asp:TextBox ID="tbxPrazoPadraoOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="tbxPrazoPadraoOS" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                    </div>
                                                </td>
                                                <td style="width: 33%;">
                                                    <div class="label_form">
                                                        Prazo Legal
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 7%;">
                                                        <asp:TextBox ID="tbxPrazoLegalOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="tbxPrazoLegalOS" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                    </div>
                                                </td>
                                                <td style="width: 33%;">
                                                    <div class="label_form">
                                                        Prazo de Diretoria
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxPrazoDiretoriaOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="tbxPrazoDiretoriaOS" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Departamento
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 3%;">
                                                        <asp:DropDownList ID="ddlDepartamentoOS" CssClass="dropDownList100" runat="server" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Setor
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:DropDownList ID="ddlSetorOS" CssClass="dropDownList100" runat="server" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Órgão
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 3%;">
                                                        <asp:DropDownList ID="ddlOrgaoOS" CssClass="dropDownList100" runat="server" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td style="width: 50%; vertical-align: bottom;">
                                                    <div class="label_form">
                                                        Número do Processo
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxNumeroProcessoOrgaoOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="label_form">
                                                        Técnico Responsável<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlResponsavelOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                            ErrorMessage="- Data" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:DropDownList ID="ddlResponsavelOS" CssClass="dropDownList100" runat="server" Enabled="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            OS Matriz
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox runat="server" ID="txtOSMAtriz" CssClass="textBox100" Enabled="false" Text="Nenhuma OS vinculada" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Estado
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxEstadoOS" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div id="detalhes_os_encerrada" runat="server" visible="false">
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 25%;">
                                                        <div class="label_form">
                                                            Data de Encerramento
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 8%; margin-top: 5px;">
                                                            <asp:TextBox ID="tbxDataEncerramentoExibicao" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td runat="server" id="campo_exibicao_protocolo_encerramento">
                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 50%;">
                                                                    <div class="label_form">
                                                                        Protocolo de Encerramento
                                                                    </div>
                                                                    <div class="campo_form" style="padding-right: 7%; margin-top: 5px;">
                                                                        <asp:TextBox ID="tbxProtocoloEncerramentoExibicao" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="label_form">
                                                                        Data do Protocolo de Encerramento
                                                                    </div>
                                                                    <div class="campo_form" style="margin-top: 5px;">
                                                                        <asp:TextBox ID="tbxDataProtocoEncerramentoExibicao" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td runat="server" id="campo_exibicao_observacao_encerramento">
                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <div class="label_form">
                                                                        Observações
                                                                    </div>
                                                                    <div class="campo_form" style="margin-top: 5px;">
                                                                        <asp:TextBox ID="tbxVisualizacaoObservacoesEncerramento" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Detalhamento&nbsp;                                                
                                                <asp:Button ID="btnVerHistorioDetalhamentosOS" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnInit="btnVerHistorioDetalhamentosPedidos_Init" OnClick="btnVerHistorioDetalhamentosOS_Click" />
                                        </div>
                                        <div class="campo_form" style="margin-top: 5px; overflow: auto;">
                                            <div id="detalhamento_visualizacao" runat="server">
                                                <div style="border: 1px solid silver;">
                                                    <asp:UpdatePanel runat="server" ID="upDetalhamentoOs" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Label ID="tbxDetalhamentoVisualizacao" runat="server" CssClass="textBox100" Height="250px" Style="overflow: auto; max-height: 250px;"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarOS" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Histórico&nbsp;
                                                <asp:Button ID="btnEditarObservacaoOS" class="botao editar_mini" runat="server" ToolTip="Alterar Observação" OnClick="btnEditarObservacaoOS_Click" />
                                            <asp:Button ID="btnVerHistorioObservacoesOS" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações da Observação" OnClick="btnVerHistorioObservacoesOS_Click" OnInit="btnVerHistorioObservacoesOS_Init" />
                                        </div>
                                        <div class="campo_form" style="margin-top: 5px; overflow: auto;">
                                            <div id="observacoes_edicao" runat="server" visible="false">
                                                <cc2:Editor ID="editObservacao" runat="server" Height="250px" NoUnicode="true" />
                                            </div>
                                            <div id="observacoes_visualizacao" runat="server" visible="false">
                                                <div style="border: 1px solid silver;">
                                                    <asp:Label ID="tbxObservacaoVisualizacao" runat="server" CssClass="textBox100" Height="250px" Style="overflow: auto; max-height: 250px;"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-top: 10px;">
                            <div class="label_form" style="font-size: 8pt;">
                                &nbsp;<span style="color: Red;">*</span>Campos Obrigatórios
                            </div>
                        </div>
                    </div>

                </div>
                <div style="float: right; width: 51%;">
                    <div class="barra">
                        Visitas
                            <div class="close" onclick="minimizar('visitas_container');"></div>
                    </div>
                    <div id="visitas_container" class="cph">
                        <div>
                            <asp:UpdatePanel runat="server" ID="upVisitas" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvVisitas" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvVisitas_PageIndexChanging" OnPreRender="gdvVisitas_PreRender" PageSize="3" OnRowDeleting="gdvVisitas_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="GetDataInicio" HeaderText="Início">
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GetDataFim" HeaderText="Fim">
                                                <HeaderStyle Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                            <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
                                            <asp:BoundField DataField="GetAceita" HeaderText="Aceita">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmailAceitou" HeaderText="E-mail que respondeu" />
                                            <asp:BoundField DataField="MotivoNaoAceite" HeaderText="Motivo Não Aceite" />
                                            <asp:TemplateField HeaderText="Ações">
                                                <ItemTemplate>
                                                    <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Button ID="btnGridEditarVisita" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="botao editar_mini" Text="" ToolTip="Editar" OnClick="btnGridEditarVisita_Click" OnInit="btnGridEditarVisita_Init" Visible="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" Enabled="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Button ID="btnGridExcluirVisita" runat="server" CommandName="Delete" CssClass="botao excluir_mini" Text="" ToolTip="Excluir" OnPreRender="btnGridExcluirVisita_PreRender" Visible="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" Enabled="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" />
                                                    </div>
                                                    <div style="clear: both; height: 1px;">
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridRow" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gdvVisitas" EventName="PageIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div style="text-align: right; margin-top: 10px;">
                            <asp:Button ID="btnNovaVisita" runat="server" Text="Nova Visita" CssClass="botao novo" OnClick="btnNovaVisita_Click" OnInit="btnNovaVisita_Init" />
                        </div>
                    </div>
                    <div class="barra" style="margin-top: 10px;">
                        Atividades
                                <div class="close" onclick="minimizar('atividades_container');">
                                </div>
                    </div>
                    <div id="atividades_container" class="cph">
                        <div>
                            <asp:UpdatePanel runat="server" ID="upAtividades" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdvAtividades" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvAtividades_PageIndexChanging" OnPreRender="gdvAtividades_PreRender" PageSize="3" OnRowDeleting="gdvAtividades_RowDeleting">
                                        <Columns>
                                            <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                            <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
                                            <asp:TemplateField HeaderText="Ações">
                                                <ItemTemplate>
                                                    <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Button ID="btnGridEditarAtividade" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="botao editar_mini" Text="" ToolTip="Editar" OnClick="btnGridEditarAtividade_Click" OnInit="btnGridEditarAtividade_Init" Visible="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" Enabled="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Button ID="btnGridExcluirAtividade" runat="server" CommandName="Delete" CssClass="botao excluir_mini" Text="" ToolTip="Excluir" OnPreRender="btnGridExcluirAtividade_PreRender" Visible="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" Enabled="<%#BindingVisivelBotoesVisit(Container.DataItem) %>" />
                                                    </div>
                                                    <div style="clear: both; height: 1px;">
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                        <PagerStyle CssClass="gridPager" />
                                        <RowStyle CssClass="gridRow" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gdvAtividades" EventName="PageIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div style="text-align: right; margin-top: 10px;">
                            <asp:Button ID="btnNovaAtividade" runat="server" Text="Nova Atividade" CssClass="botao novo" OnClick="btnNovaAtividade_Click" OnInit="btnNovaAtividade_Init" />
                        </div>
                    </div>
                    <div id="oss_vinculadas" runat="server" visible="false">
                        <div class="barra" style="margin-top: 10px;">
                            OS's Vinculadas
                                <div class="close" onclick="minimizar('oss_vinculadas_container');"></div>
                        </div>
                        <div id="oss_vinculadas_container" class="cph">
                            <div>
                                <asp:UpdatePanel runat="server" ID="upOrdensVinculadas" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gdvOrdensVinculadas" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvOrdensVinculadas_PageIndexChanging" OnPreRender="gdvOrdensVinculadas_PreRender" PageSize="3">
                                            <Columns>
                                                <asp:BoundField DataField="GetNumeroPedido" HeaderText="Pedido" />
                                                <asp:BoundField DataField="Codigo" HeaderText="OS" />
                                                <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                <asp:BoundField DataField="GetResponsavel" HeaderText="Responsável" />
                                                <asp:TemplateField HeaderText="Visualizar">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="btnGridVisualizarOS" runat="server" CssClass="botao visualizar_mini" Target="_blank" NavigateUrl="<%# BindVisualizarOS(Container.DataItem) %>" ToolTip="Visualizar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                            <PagerStyle CssClass="gridPager" />
                                            <RowStyle CssClass="gridRow" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gdvOrdensVinculadas" EventName="PageIndexChanging" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="barra" style="margin-top: 10px;">
                        Anexos
                                <div class="close" onclick="minimizar('anexos_container');">
                                </div>
                    </div>
                    <div id="anexos_container" class="cph">
                        <div>
                            <asp:TreeView ID="trvAnexosOS" runat="server" OnSelectedNodeChanged="trvAnexosOS_SelectedNodeChanged">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px" VerticalPadding="0px" />
                            </asp:TreeView>
                        </div>
                        <div id="visualizar_arquivos_os" runat="server" style="text-align: right; margin-top: 10px;" visible="false">
                            <asp:HyperLink ID="hplVisualizarArquivoOs" runat="server" Target="_blank" CssClass="botao visualizar" Text="Visualizar Arquivo"></asp:HyperLink>
                        </div>
                        <div style="text-align: right; margin-top: 15px;">
                            <asp:Button ID="btnNovoArquivoOS" runat="server" Text="Anexar Arquivo" CssClass="botao novo" OnClick="btnNovoArquivoOS_Click" />&nbsp;
                                <asp:Button ID="btnExcluirArquivoOS" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoOS_Click" OnPreRender="btnExcluirArquivoOS_PreRender" OnInit="btnExcluirArquivoOS_Init" />&nbsp;
                                <asp:Button ID="btnRenomearArquivoOS" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoOS_Click" OnInit="btnRenomearArquivoOS_Init" />
                        </div>
                    </div>

                    <div class="barra" style="margin-top: 15px;">
                        Dados do Cliente
                            <div class="close" onclick="minimizar('dados_cliente_container');"></div>
                    </div>
                    <div id="dados_cliente_container" class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Nome/Razão Social
                                        </div>
                                        <div class="campo_form" style="margin-top: 5px; padding-right: 15px;">
                                            <asp:TextBox ID="tbxNomeCliente" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Telefone 1
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 3%;">
                                                        <asp:TextBox ID="tbxTelefone1Cliente" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Telefone 2
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 15px;">
                                                        <asp:TextBox ID="tbxTelefone2Cliente" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            E-mail
                                        </div>
                                        <div class="campo_form" style="margin-top: 5px; padding-right: 15px;">
                                            <asp:TextBox ID="tbxEmailCliente" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;">
                                        <div class="label_form">
                                            Endereço
                                        </div>
                                        <div class="campo_form" style="margin-top: 5px; padding-right: 15px;">
                                            <asp:TextBox ID="tbxEnderecoCliente" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 30%;">
                                                    <div class="label_form">
                                                        CEP
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 15px;">
                                                        <asp:TextBox ID="tbxCepCliente" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 70%;">
                                                    <div class="label_form">
                                                        Cidade/Estado
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 15px;">
                                                        <asp:TextBox ID="tbxCidadeEstadoCliente" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfId" runat="server" />
                <div style="clear: both"></div>

                <div style="text-align: right">
                    <div style="text-align: right; margin-top: 10px;">
                        <asp:Button ID="btnAprovarOS" runat="server" Text="Aprovar OS" CssClass="botao ok" OnClick="btnAprovarOS_Click" OnInit="btnAprovarOS_Init" />
                        <asp:Button ID="btnSolicitarAprovacao" runat="server" Text="Solicitar Aprovação" CssClass="botao email" OnClick="btnSolicitarAprovacao_Click" OnInit="btnSolicitarAprovacao_Init" />&nbsp;
                            <asp:Button ID="btnSolicitarAdiamentoPrazo" runat="server" Text="Solicitar Adiamento de Prazo" CssClass="botao email" OnClick="btnSolicitarAdiamentoPrazo_Click" OnInit="btnSolicitarAdiamentoPrazo_Init" />&nbsp;
                            <asp:Button ID="btnSalvarOS" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvarOS_Click" ValidationGroup="vlgOS" />&nbsp;
                            <asp:Button ID="btnEncerrarOS" runat="server" Text="Encerrar OS" CssClass="botao ok" OnClick="btnEncerrarOS_Click" OnInit="btnEncerrarOS_Init" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnEditarObservacaoOS" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSalvarOS" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluirArquivoOS" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="trvAnexosOS" EventName="SelectedNodeChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">

    <div id="Popup_Historico_Detalhamentos" class="pop_up" style="width: 750px;">
        <div class="barra">
            Histórico de Detalhamentos/Históricos
        </div>
        <div class="pop_m20">
            <div style="position: relative; overflow: auto; max-height: 600px;">
                <asp:UpdatePanel ID="upHistoricosDetalhamentos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="rptHistoricoDetalhamentos" runat="server">
                            <ItemTemplate>
                                <div style="overflow: auto; max-height: 400px;">
                                    <div style="border-bottom: 1px solid black; font-size: 12pt; font-weight: bold; margin-top: 10px;">
                                        <%# Eval("DataSalvamento") %> - <%# Eval("Usuario") %>
                                    </div>
                                    <div style="margin-top: 10px;">
                                        <asp:Label ID="lblConteudoDetalhamento" runat="server" Text='<%# BindConteudoDetalhamento(Container.DataItem) %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <a id="cancelarHistoricoDetalhamentos" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="Popup_Visita" class="pop_up" style="width: 750px;">
        <div class="barra">
            Cadastro de Visita                      
        </div>
        <div class="pop_m20">
            <div style="text-align: right; vertical-align: middle; border-bottom: 2px solid #6D0909 !important;">
                <asp:LinkButton ID="lkbExibirDadosVisita" runat="server" CssClass="link_aba_escolhida" Height="30px" OnClick="lkbExibirDadosVisita_Click">Dados</asp:LinkButton>
                <asp:LinkButton ID="lkbExibirReservaVeiculoVisita" runat="server" CssClass="link_aba" Height="30px" OnClick="lkbExibirReservaVeiculoVisita_Click">Reserva de Veículo</asp:LinkButton>
                <asp:LinkButton ID="lkbExibirArquivosVisita" runat="server" CssClass="link_aba" Height="30px" OnClick="lkbExibirArquivosVisita_Click">Anexos</asp:LinkButton>
            </div>
            <div class="cph" style="position: relative;">
                <div style="overflow: auto; max-height: 400px;">
                    <asp:UpdatePanel ID="upExibicoesDaVisita" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:MultiView ID="mvFormVisita" runat="server" ActiveViewIndex="0">
                                <asp:View ID="dados_visita_view" runat="server">
                                    <asp:UpdatePanel ID="upFormVisita" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">&nbsp;</div>
                                                        <div class="campo_form" style="vertical-align: bottom;">
                                                            <asp:CheckBox ID="ckbAtivoVisita" runat="server" Text="Ativo" Checked="True" />
                                                            <asp:HiddenField ID="hfIdVisita" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 50%">
                                                        <div class="label_form">
                                                            Data de Início<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxDataInicioVisita" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Data" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxDataInicioVisita" CssClass="textBox100" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbxDataInicioVisita" Format="dd/MM/yyyy HH:mm">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="label_form">
                                                            Data de Fim<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxDataFimVisita" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Data" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxDataFimVisita" CssClass="textBox100" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="tbxDataFimVisita" Format="dd/MM/yyyy HH:mm">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Pedido
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxPedidoVisita" CssClass="textBox100" runat="server" Text="Carregado Automaticamente..." ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            OS
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxOSVisita" CssClass="textBox100" runat="server" Text="Carregado Automaticamente..." ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Tipo de Visita<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoVisita" InitialValue="0" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Tipo" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="margin-top: 5px; padding-right: 15px;">
                                                            <asp:DropDownList ID="ddlTipoVisita" CssClass="dropDownList100" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Descrição<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="tbxDescricaoVisita" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Descrição" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxDescricaoVisita" CssClass="textBox100" runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Visitante<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlVisitante" InitialValue="0" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Responsável" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:DropDownList ID="ddlVisitante" CssClass="dropDownList100" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form" style="margin-top: 20px;">
                                                            Detalhamento
                                                            <asp:Button ID="btnEditarDetalhamentosVisita" class="botao editar_mini" runat="server" ToolTip="Alterar Detalhamento" OnClick="btnEditarDetalhamentosVisita_Click" />
                                                            <asp:Button ID="btnVisualizarDetalhamentosVisita" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnClick="btnVisualizarDetalhamentosVisita_Click" OnInit="btnVisualizarDetalhamentosVisita_Init" />
                                                        </div>
                                                        <div class="campo_form" style="margin-top: 5px; overflow: auto; max-height: 250px; padding-right: 15px;">
                                                            <div id="detalhamento_edicao_visita" runat="server" visible="false">
                                                                <cc2:Editor ID="editDetalhamentoVisita" runat="server" Height="250px" NoUnicode="true" />
                                                            </div>
                                                            <div id="detalhamento_visualizacao_visita" runat="server" visible="false">
                                                                <div style="border: 1px solid silver;">
                                                                    <asp:Label ID="tbxVisualizarDetalhamentoVisita" runat="server" CssClass="textBox100" Height="250px" Style="overflow: auto; max-height: 250px;"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroVisita" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarVisita" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnEditarDetalhamentosVisita" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </asp:View>
                                <asp:View ID="reservas_view" runat="server">
                                    <div class="barra">
                                        Reserva de Veículo
                                        <div class="close" onclick="minimizar('reservas_container');">
                                        </div>
                                    </div>
                                    <div id="reservas_container" class="cph">
                                        <div>
                                            <asp:UpdatePanel ID="upReservaVeiculoVisita" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td colspan="4">
                                                                <div class="label_form">
                                                                    Veículo<span>*:</span>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlVeículoReserva" InitialValue="0" CssClass="RequireFieldValidator"
                                                                        ErrorMessage="- Responsável" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="campo_form">
                                                                    <asp:DropDownList ID="ddlVeículoReserva" CssClass="dropDownList100" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 30%;" valign="bottom">
                                                                <div class="label_form">
                                                                    Data de Início<span>*:</span>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxDataInicioReserva" CssClass="RequireFieldValidator"
                                                                        ErrorMessage="- Data" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 5%;">
                                                                    <asp:TextBox ID="tbxDataInicioReserva" CssClass="textBox100" runat="server"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbxDataInicioReserva" Format="dd/MM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <div class="label_form">
                                                                    período
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 10%;">
                                                                    <asp:DropDownList ID="ddlPeriodoInicioReserva" CssClass="dropDownList100" runat="server">
                                                                        <asp:ListItem Value="M">Manhã</asp:ListItem>
                                                                        <asp:ListItem Value="T">Tarde</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                            <td style="width: 30%;">
                                                                <div class="label_form">
                                                                    Data de Fim<span>*:</span>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxDataFimReserva" CssClass="RequireFieldValidator"
                                                                        ErrorMessage="- Data" ValidationGroup="vlgVisita">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 5%;">
                                                                    <asp:TextBox ID="tbxDataFimReserva" CssClass="textBox100" runat="server"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxDataFimReserva" Format="dd/MM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <div class="label_form">
                                                                    período
                                                                </div>
                                                                <div class="campo_form">
                                                                    <asp:DropDownList ID="ddlPeriodoFimReserva" CssClass="dropDownList100" runat="server">
                                                                        <asp:ListItem Value="M">Manhã</asp:ListItem>
                                                                        <asp:ListItem Value="T">Tarde</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lkbExibirReservaVeiculoVisita" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="arquivos_view" runat="server">
                                    <div class="barra">
                                        Arquivos
                                        <div class="close" onclick="minimizar('arquivos_visita_container');">
                                        </div>
                                    </div>
                                    <div id="arquivos_visita_container" class="cph">
                                        <div>
                                            <asp:UpdatePanel ID="upArquivosVisita" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="trvArquivosVisita" runat="server" OnSelectedNodeChanged="trvArquivosVisita_SelectedNodeChanged">
                                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                        <ParentNodeStyle Font-Bold="False" />
                                                        <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px" VerticalPadding="0px" />
                                                    </asp:TreeView>
                                                    <div id="visualizar_arquivos_visita" runat="server" style="text-align: right; margin-top: 10px;" visible="false">
                                                        <asp:HyperLink ID="hplVisualizarArquivosVisita" runat="server" Target="_blank" CssClass="botao visualizar" Text="Visualizar Arquivo"></asp:HyperLink>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnExcluirArquivoVisita" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnRenomearArquivoVisita" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoArquivoVisita" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroVisita" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="trvArquivosVisita" EventName="SelectedNodeChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div style="text-align: right; margin-top: 15px;">
                                            <asp:Button ID="btnNovoArquivoVisita" runat="server" Text="Anexar Arquivo" CssClass="botao novo" OnClick="btnNovoArquivoVisita_Click" />&nbsp;
                                            <asp:Button ID="btnExcluirArquivoVisita" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoVisita_Click" OnInit="btnExcluirArquivoVisita_Init" />&nbsp;
                                            <asp:Button ID="btnRenomearArquivoVisita" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoVisita_Click" OnInit="btnRenomearArquivoVisita_Init" />
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirDadosVisita" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirReservaVeiculoVisita" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirArquivosVisita" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroVisita" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnEnviarEmailAceiteVisita" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnEnviarEmailAceiteVisita" runat="server" Text="Enviar E-mail de Aceite" CssClass="botao email" OnClick="btnEnviarEmailAceiteVisita_Click" OnInit="btnEnviarEmailAceiteVisita_Init" />&nbsp;
                <asp:Button ID="btnNovoCadastroVisita" runat="server" Text="Nova Visita" CssClass="botao novo" OnClick="btnNovoCadastroVisita_Click" />&nbsp;
                <asp:Button ID="btnSalvarVisita" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgVisita" OnClick="btnSalvarVisita_Click" OnInit="btnSalvarVisita_Init" />&nbsp;
                <a id="cancelar_cad_visita" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="popArquivos" class="pop_up" style="width: 550px;">
        <div class="barra">
            Upload de Arquivos
        </div>
        <div class="pop_m20">
            <div style="position: relative;">
                <cc1:UpLoad ID="UpLoad2" runat="server" OnUpLoadComplete="UpLoad2_UpLoadComplete" Pagina="HandlerArquivosOS.ashx" TamanhoMaximoArquivo="102400" TamanhoParteUpload="102400" OnInit="UpLoad2_Init">
                </cc1:UpLoad>
                <asp:Label ID="Label1" runat="server" OnInit="Label1_Init"></asp:Label>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <a id="cancelarUploadArquivos" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="popRenomearArquivo" class="pop_up" style="width: 430px;">
        <div class="barra">
            Renomear Arquivo
        </div>
        <div class="pop_m20">
            <div class="filtros_titulo">
                Novo Nome<span>*:</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxNovoNomeArquivo" CssClass="RequireFieldValidator" ErrorMessage="- Nome"
                    ValidationGroup="vlgRenomearArquivo">*</asp:RequiredFieldValidator>
            </div>
            <div class="filtros_campo" style="padding-right: 15px;">
                <asp:UpdatePanel ID="upRenomearArquivo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="campo_form">
                            <asp:TextBox ID="tbxNovoNomeArquivo" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:HiddenField ID="hfIdArquivoRenomear" runat="server" />
                            <asp:HiddenField ID="hfArquivoOS" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnSalvarRenomearArquivo" CssClass="botao salvar" runat="server" Text="Salvar" ValidationGroup="vlgRenomearArquivo" OnClick="btnSalvarRenomearArquivo_Click" OnInit="btnSalvarRenomearArquivo_Init" />&nbsp;                                   
            <a id="cancelar_renomear_arquivo" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="PopAtividades" class="pop_up" style="width: 750px;">
        <div class="barra">
            Cadastro de Atividade            
        </div>
        <div class="pop_m20">
            <div style="text-align: right; vertical-align: middle; border-bottom: 2px solid #6D0909 !important;">
                <asp:LinkButton ID="lkbExibirDadosAtividade" runat="server" CssClass="link_aba_escolhida" Height="30px" OnClick="lkbExibirDadosAtividade_Click">Dados</asp:LinkButton>
                <asp:LinkButton ID="lkbExibirArquivosAtividade" runat="server" CssClass="link_aba" Height="30px" OnClick="lkbExibirArquivosAtividade_Click">Anexos</asp:LinkButton>
            </div>
            <div class="cph" style="position: relative;">
                <div style="overflow: auto; max-height: 400px;">
                    <asp:UpdatePanel ID="upExibicoesAtividade" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:MultiView ID="mvFormAtividade" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View_dados_atividade" runat="server">
                                    <asp:UpdatePanel ID="upFormAtividade" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">&nbsp;</div>
                                                        <div class="campo_form" style="vertical-align: bottom;">
                                                            <asp:CheckBox ID="ckbAtivoAtividade" runat="server" Text="Ativo" Checked="True" />
                                                            <asp:HiddenField ID="hfIdAtividade" runat="server" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Data<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbxDataAtividade" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Data" ValidationGroup="vlgAtividade">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 3%;">
                                                            <asp:TextBox ID="tbxDataAtividade" CssClass="textBox50" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbxDataAtividade" Format="dd/MM/yyyy">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Pedido
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxPedidoAtividade" runat="server" CssClass="textBox100" ReadOnly="true" Text="Carregado Automaticamente..."></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            OS
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxOSAtividade" CssClass="textBox100" runat="server" Text="Carregado Automaticamente..." ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Tipo de Atividade<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlTipoAtividade" InitialValue="0" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Tipo" ValidationGroup="vlgAtividade">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="margin-top: 5px; padding-right: 15px;">
                                                            <asp:DropDownList ID="ddlTipoAtividade" CssClass="dropDownList100" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Descrição<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tbxDescricaoAtividade" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Descrição" ValidationGroup="vlgAtividade">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:TextBox ID="tbxDescricaoAtividade" CssClass="textBox100" runat="server"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form">
                                                            Executor<span>*:</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlExecutorAtividade" InitialValue="0" CssClass="RequireFieldValidator"
                                                                ErrorMessage="- Executor" ValidationGroup="vlgAtividade">*</asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="campo_form" style="padding-right: 15px;">
                                                            <asp:DropDownList ID="ddlExecutorAtividade" CssClass="dropDownList100" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="label_form" style="margin-top: 20px;">
                                                            Detalhamento
                                                            <asp:Button ID="btnEditarDetalhamentosAtividade" class="botao editar_mini" runat="server" ToolTip="Alterar Detalhamento" OnClick="btnEditarDetalhamentosAtividade_Click" />
                                                            <asp:Button ID="btnVisualizarDetalhamentosAtividade" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnClick="btnVisualizarDetalhamentosAtividade_Click" OnInit="btnVisualizarDetalhamentosAtividade_Init" />
                                                        </div>
                                                        <div class="campo_form" style="margin-top: 5px; overflow: auto; max-height: 250px; padding-right: 15px;">
                                                            <div id="edicao_detalhamento_atividade" runat="server" visible="false">
                                                                <cc2:Editor ID="editDetalhamentoAtividade" runat="server" Height="250px" NoUnicode="true" />
                                                            </div>
                                                            <div id="visualizacao_detalhamento_atividade" runat="server" visible="false">
                                                                <div style="border: 1px solid silver;">
                                                                    <asp:Label ID="tbxVisualizarDetalhamentoAtividade" runat="server" CssClass="textBox100" Height="250px" Style="overflow: auto; max-height: 250px;"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroAtividade" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarAtividade" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnEditarDetalhamentosAtividade" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </asp:View>
                                <asp:View ID="View_arquivos_atividade" runat="server">
                                    <div class="barra">
                                        Arquivos
                                        <div class="close" onclick="minimizar('arquivos_atividade_container');">
                                        </div>
                                    </div>
                                    <div id="arquivos_atividade_container" class="cph">
                                        <div>
                                            <asp:UpdatePanel ID="upArquivosAtividade" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="trvArquivosAtividade" runat="server" OnSelectedNodeChanged="trvArquivosAtividade_SelectedNodeChanged">
                                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                        <ParentNodeStyle Font-Bold="False" />
                                                        <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px" VerticalPadding="0px" />
                                                    </asp:TreeView>
                                                    <div id="visualizar_arquivos_atividade" runat="server" style="text-align: right; margin-top: 10px;" visible="false">
                                                        <asp:HyperLink ID="hplArquivosAtividade" runat="server" Target="_blank" CssClass="botao visualizar" Text="Visualizar Arquivo"></asp:HyperLink>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnExcluirArquivoAtividade" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnRenomearArquivoAtividade" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoArquivoAtividade" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroAtividade" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="trvArquivosAtividade" EventName="SelectedNodeChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div style="text-align: right; margin-top: 20px;">
                                            <asp:Button ID="btnNovoArquivoAtividade" runat="server" Text="Anexar Arquivo" CssClass="botao novo" OnClick="btnNovoArquivoAtividade_Click" />&nbsp;
                                            <asp:Button ID="btnExcluirArquivoAtividade" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoAtividade_Click" OnInit="btnExcluirArquivoAtividade_Init" />&nbsp;
                                            <asp:Button ID="btnRenomearArquivoAtividade" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoAtividade_Click" OnInit="btnRenomearArquivoAtividade_Init" />
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirDadosAtividade" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirArquivosAtividade" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroAtividade" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoCadastroAtividade" runat="server" Text="Nova Atividade" CssClass="botao novo" OnClick="btnNovoCadastroAtividade_Click" />&nbsp;
                <asp:Button ID="btnSalvarAtividade" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgAtividade" OnClick="btnSalvarAtividade_Click" OnInit="btnSalvarAtividade_Init" />&nbsp;
                <a id="cancelar_cad_atividade" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="PopSolicitacaoAdiamento" class="pop_up" style="width: 500px;">
        <div class="barra">
            Solicitação de Adiamento
        </div>
        <div class="cph">
            <asp:UpdatePanel ID="upSolicitacaoAdiamentoPrazo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Data da Solicitação<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="tbxDataSolicitacaoAdiamento" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgSolicitarAdiamento">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataSolicitacaoAdiamento" runat="server" CssClass="textBox50"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbxDataSolicitacaoAdiamento" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Solicitante
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxSolicitanteAdiamento" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Justificativa<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbxJustificativaAdiamento" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgSolicitarAdiamento">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxJustificativaAdiamento" runat="server" CssClass="textBox100" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: red">*Por favor informe a quantidade de dias de prazo que deseja solicitar no campo Justificativa
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnSolicitarAdiamento" CssClass="botao email" runat="server" Text="Solicitar" ValidationGroup="vlgSolicitarAdiamento" OnClick="btnSolicitarAdiamento_Click" OnInit="btnSolicitarAdiamento_Init" />&nbsp;                                   
            <a id="cancelar_solicitar_adiamento" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="PopEncerramentoOS" class="pop_up" style="width: 50%">
        <div class="barra">
            Encerramento de OS
        </div>
        <div class="cph">
            <asp:UpdatePanel ID="upCamposEncerramentoOS" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Data
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataEncerramentoOS" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form">
                                    &nbsp;
                                </div>
                                <div class="campo_form">
                                    <asp:CheckBox ID="chkPossuiProtocolo" AutoPostBack="true" runat="server" Text="Possui Protocolo" Checked="true" CssClass="checkBox" OnCheckedChanged="chkPossuiProtocolo_CheckedChanged" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:MultiView runat="server" ID="mtvProtocoloEncerramento" ActiveViewIndex="0">
                                    <asp:View runat="server">
                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div class="label_form" style="margin-top: 10px;">
                                                        Protocolo/Oficio de Encerramento<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="tbxProtocoloOficioEncerramento" CssClass="RequireFieldValidator"
                                                            ErrorMessage="- Protocolo" ValidationGroup="vlgEncerramento">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxProtocoloOficioEncerramento" runat="server" CssClass="textBox100"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="label_form" style="margin-top: 10px;">
                                                        Data do Protocolo de Encerramento<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="tbxDataProtocoloEncerramento" CssClass="RequireFieldValidator"
                                                            ErrorMessage="- Protocolo" ValidationGroup="vlgEncerramento">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxDataProtocoloEncerramento" runat="server" CssClass="textBox100"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="tbxDataProtocoloEncerramento" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View runat="server">
                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <div class="label_form" style="margin-top: 10px;">
                                                        Observações:
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxObservacoesEncerramento" runat="server" CssClass="textBox100"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chkPossuiProtocolo" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnSalvarEncerramentoOS" CssClass="botao ok" ValidationGroup="vlgEncerramento" runat="server" Text="Encerrar" OnClick="btnSalvarEncerramentoOS_Click" OnInit="btnSalvarEncerramentoOS_Init" />&nbsp;                                   
            <asp:Button ID="btnEncerrarEEnviarPesquisaOS" CssClass="botao branco" runat="server" ValidationGroup="vlgEncerramento" Text="Encerrar e enviar pesquisa de satisfação" OnClick="btnEncerrarEEnviarPesquisaOS_Click" OnInit="btnSalvarEncerramentoOS_Init" />&nbsp;                                   
            <a id="cancelar_encerramento_os" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="PopSolicitacaoAprovacao" class="pop_up" style="width: 500px;">
        <div class="barra">
            Solicitação de Aprovação
        </div>
        <div class="cph">
            <asp:UpdatePanel ID="upCamposSolicitacaoAprovacao" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Data da Solicitação<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="tbxDataSolicitacaoAProvacao" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgSolicitarAprovacao">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataSolicitacaoAProvacao" runat="server" CssClass="textBox50"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="tbxDataSolicitacaoAProvacao" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Solicitante
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxSolicitanteAprovacao" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Justificativa<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="tbxJustificativaAprovacao" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgSolicitarAprovacao">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxJustificativaAprovacao" runat="server" CssClass="textBox100" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnEnviarSolicitarAprovacao" CssClass="botao email" runat="server" Text="Solicitar" ValidationGroup="vlgSolicitarAprovacao" OnClick="btnEnviarSolicitarAprovacao_Click" OnInit="btnEnviarSolicitarAprovacao_Init" />&nbsp;                                   
            <a id="cancelar_solicitacao_aprovacao" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="PopAprovacaoOS" class="pop_up" style="width: 500px;">
        <div class="barra">
            Aprovação de OS
        </div>
        <div class="cph">
            <asp:UpdatePanel ID="upCamposAprovacaoOS" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Data da Aprovação
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataAprovacaoOS" runat="server" CssClass="textBox50" Enabled="false"></asp:TextBox>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Usuário que aprovou
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxUsuarioAprovadorOS" runat="server" CssClass="textBox100" Enabled="false"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Parecer
                                </div>
                                <div class="campo_form">
                                    <asp:DropDownList ID="ddlParecerAprovacaoOS" CssClass="dropDownList100" runat="server">
                                        <asp:ListItem Value="A">Aprovada</asp:ListItem>
                                        <asp:ListItem Value="R">Reprovada</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Justificativa<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="tbxJustificativaAProvacaoOS" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgAprovacaoOS">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxJustificativaAProvacaoOS" runat="server" CssClass="textBox100" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnEmitirParecerAprovacaoOS" CssClass="botao ok" runat="server" Text="Emitir Parecer" ValidationGroup="vlgAprovacaoOS" OnClick="btnEmitirParecerAprovacaoOS_Click" OnInit="btnEmitirParecerAprovacaoOS_Init" />&nbsp;                                   
            <a id="cancelar_aprovar_os" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="PopEmailsAceiteVisita" class="pop_up" style="width: 630px;">
        <div class="barra">
            Enviar E-mail de Aceite de Visita
        </div>
        <div class="pop_m20">
            <asp:UpdatePanel ID="upEmailsAceiteVisita" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="filtros_titulo">
                        O e-mail de aceite de visita será enviado para os e-mails listados abaixo:
                    </div>
                    <div class="filtros_campo" style="padding-right: 15px; margin-top: 5px;">
                        <div class="campo_form">
                            <asp:ListBox ID="ltbEmailsAceiteVisita" runat="server" Enabled="false" CssClass="textBox100"></asp:ListBox>
                        </div>
                    </div>
                    <div class="filtros_titulo">
                        Para enviar o e-mail de aceite para mais destinatários, digite os e-mails desejados abaixo:<br />
                        Observação: Para adicionar mais de um e-mail, separe-os por ";"
                    </div>
                    <div class="filtros_campo" style="padding-right: 15px; margin-top: 5px;">
                        <asp:TextBox ID="tbxOutrosEmailsAceiteVisite" runat="server" CssClass="textBox100" TextMode="MultiLine" Height="70px"></asp:TextBox>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEnviarEmailsAceiteVisita" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnEnviarEmailsAceiteVisita" CssClass="botao email" runat="server" Text="Enviar" OnClick="btnEnviarEmailsAceiteVisita_Click" />&nbsp;  
            <a id="cancelarEmailAceiteVisita" class="botao vermelho">Cancelar</a>
        </div>
    </div>

</asp:Content>

