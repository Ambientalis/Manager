<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="SolicitacaoAdiamentoII.aspx.cs" Inherits="OrdemServico_SolicitacaoAdiamento" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .solicitacao_respondida {
            width: 99%;
            background-color: #ffd1d1;
            min-height: 30px;
            height: auto;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            padding: 10px 15px;
            color: #000 !important;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        solicitação de adiamento de prazo de OS
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="cph">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="campos_form_cadastro">
                    <div id="solicitacao_ja_respondida" class="solicitacao_respondida" runat="server" visible="false">
                    <strong><span class="auto-style1">Solicitação de Adiamento já respondida</span></strong><br />
                    Esta Solicitação de Adiamento de Prazo de OS já foi <asp:Label ID="lblEstadoSolicitacao" runat="server"></asp:Label>&nbsp;pelo usuário
                        <asp:Label ID="lblUsuarioQueRespondeuSolicit" runat="server"></asp:Label>&nbsp;em
                         <asp:Label ID="lblDataRespostaSolicitacao" runat="server"></asp:Label>
                </div>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="campo_form">
                                    <strong>Número da OS:</strong>&nbsp;
                                    <asp:Label ID="lblNumeroOS" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="campo_form">
                                    <strong>Data da OS:</strong>&nbsp;
                                    <asp:Label ID="lblDataOS" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="campo_form">
                                    <strong>Descrição da OS:</strong>&nbsp;
                                    <asp:Label ID="lblDescricaoOS" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="campo_form">
                                    <strong>Número do Pedido:</strong>&nbsp;
                                    <asp:Label ID="lblPedidoOS" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="campo_form">
                                    <strong>Cliente:</strong>&nbsp;
                                    <asp:Label ID="lblClienteOS" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="campo_form" style="margin-top: 20px;">
                                    <strong>Data da Solicitação:</strong>&nbsp;
                                    <asp:Label ID="lblDataSolicitacao" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="barra" style="margin-top: 20px;">
                    Prazos Atuais
                </div>
                <div class="cph">
                    <div>
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>                                
                                <td style="width: 33%;">
                                    <div class="label_form">
                                        Prazo Padrão
                                    </div>
                                    <div class="campo_form" style="padding-right: 7%;">
                                        <asp:TextBox ID="tbxPrazoPadraoAntigoOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </td>
                                <td style="width: 33%;">
                                    <div class="label_form">
                                        Prazo Legal
                                    </div>
                                    <div class="campo_form" style="padding-right: 7%;">
                                        <asp:TextBox ID="tbxPrazoLegalAntigoOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </td>
                                <td style="width: 33%;">
                                    <div class="label_form">
                                        Prazo de Diretoria
                                    </div>
                                    <div class="campo_form" style="padding-right: 7%;">
                                        <asp:TextBox ID="tbxPrazoDiretoriaAntigoOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="barra" style="margin-top: 20px;">
                    Novos Prazos
                </div>
                <div class="cph">
                    <div>
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>                                
                                <td style="width: 33%;">
                                    <div class="label_form">
                                        Prazo Padrão
                                    </div>
                                    <div class="campo_form" style="padding-right: 7%;">
                                        <asp:TextBox ID="tbxNovoPrazoPadraoOS" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>                                        
                                    </div>
                                </td>
                                <td style="width: 33%;">
                                    <div class="label_form">
                                        Prazo Legal
                                    </div>
                                    <div class="campo_form" style="padding-right: 7%;">
                                        <asp:TextBox ID="tbxNovoPrazoLegalOS" CssClass="textBox100" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxNovoPrazoLegalOS" Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </td>
                                <td style="width: 33%;">
                                    <div class="label_form">
                                        Prazo de Diretoria
                                    </div>
                                    <div class="campo_form" style="padding-right: 7%;">
                                        <asp:TextBox ID="tbxNovoPrazoDiretoriaOS" CssClass="textBox100" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbxNovoPrazoDiretoriaOS" Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="cph" style="margin-top:10px;">
                    <div>
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <div class="label_form">
                                        Observações
                                    </div>
                                    <div class="campo_form">
                                        <asp:TextBox ID="tbxObservacoesSolicitacaoAdiamento" CssClass="textBox100" runat="server" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:HiddenField ID="hfIdSolicitacao" runat="server" />
                <div style="text-align: right; margin-top: 10px;">
        <asp:Button ID="btnAdiarPrazo" runat="server" Text="Adiar" CssClass="botao ok" OnClick="btnAdiarPrazo_Click" />&nbsp;
        <asp:Button ID="btnNegarAdiamentoPrazo" runat="server" Text="Negar Adiamento" CssClass="botao cancelar" OnClick="btnNegarAdiamentoPrazo_Click" />
    </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdiarPrazo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNegarAdiamentoPrazo" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

