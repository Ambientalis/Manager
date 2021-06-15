<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroDeAtividade.aspx.cs" Inherits="Atividades_CadastroDeAtividade" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" Runat="Server">
    <p>
        Cadastro de Atividade</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
    <asp:Label ID="lblHistoricoDetalhamentos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblHistoricoDetalhamentos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarHistoricoDetalhamentos"
        Enabled="True" PopupControlID="Popup_Historico_Detalhamentos" TargetControlID="lblHistoricoDetalhamentos">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblUploadArquivos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblUploadArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarUploadArquivos"
        Enabled="True" PopupControlID="popArquivos" TargetControlID="lblUploadArquivos">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblRenomearArquivos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblRenomearArquivos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_renomear_arquivo"
        Enabled="True" PopupControlID="popRenomearArquivo" TargetControlID="lblRenomearArquivos">
    </asp:ModalPopupExtender>
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
            <asp:UpdatePanel ID="upFormularioAtividade" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="float: left; width: 48%; margin-right: 10px;">
                        <div class="barra">
                            Dados Básicos
                        </div>
                        <div class="cph">
                            <div style="display: block;">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkAtivo" runat="server" Checked="True" Enabled="false" Text="Ativo" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form">
                                                Data
                                            </div>
                                            <div class="campo_form" style="padding-right: 3%;">
                                                <asp:TextBox ID="tbxDataAtividade" CssClass="textBox50" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form">
                                                Pedido
                                            </div>
                                            <div class="campo_form">
                                                <asp:TextBox ID="tbxPedidoAtividade" runat="server" CssClass="textBox100" ReadOnly="true" Text="Carregado Automaticamente..."></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form">
                                                OS
                                            </div>
                                            <div class="campo_form">
                                                <asp:TextBox ID="tbxOSAtividade" CssClass="textBox100" runat="server" Text="Carregado Automaticamente..." ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form">
                                                Tipo de Atividade
                                            </div>
                                            <div class="campo_form">
                                                <asp:DropDownList ID="ddlTipoAtividade" CssClass="dropDownList100" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form">
                                                Descrição
                                            </div>
                                            <div class="campo_form">
                                                <asp:TextBox ID="tbxDescricaoAtividade" CssClass="textBox100" runat="server" TextMode="MultiLine" Enabled="false" style="resize:none" Rows="5" ></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form">
                                                Executor
                                            </div>
                                            <div class="campo_form">
                                                <asp:DropDownList ID="ddlExecutorAtividade" CssClass="dropDownList100" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="label_form" style="margin-top: 10px;">
                                                Detalhamento
                                                <asp:Button ID="btnEditarDetalhamentosAtividade" class="botao editar_mini" runat="server" ToolTip="Alterar Detalhamento" OnClick="btnEditarDetalhamentosAtividade_Click" />
                                                <asp:Button ID="btnVisualizarDetalhamentosAtividade" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnClick="btnVisualizarDetalhamentosAtividade_Click" OnInit="btnVisualizarDetalhamentosAtividade_Init" />
                                            </div>
                                            <div class="campo_form" style="margin-top: 5px; overflow: auto; max-height: 350px">
                                                <div id="edicao_detalhamento_atividade" runat="server" visible="false">
                                                    <cc2:Editor ID="editDetalhamentoAtividade" runat="server" Height="400px" NoUnicode="true" />
                                                </div>
                                                <div id="visualizacao_detalhamento_atividade" runat="server" visible="false">
                                                    <div style="border: 1px solid silver;">
                                                        <asp:Label ID="tbxVisualizarDetalhamentoAtividade" runat="server" CssClass="textBox100" Height="350px" style="overflow:auto; max-height:250px;"></asp:Label>
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
                            Anexos
                            <div class="close" onclick="minimizar('anexos_container');"></div>
                        </div>
                        <div id="anexos_container" class="cph">
                            <div>
                                <asp:TreeView ID="trvArquivosAtividade" runat="server" OnSelectedNodeChanged="trvArquivosAtividade_SelectedNodeChanged">
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px" VerticalPadding="0px" />
                                </asp:TreeView>
                            </div>
                            <div id="visualizar_arquivos" runat="server" style="text-align: right; margin-top: 10px;" visible="false">                                
                                <asp:HyperLink ID="hplVisualizarArquivo" runat="server" Target="_blank" CssClass="botao visualizar" Text="Visualizar Arquivo"></asp:HyperLink>
                            </div>
                            <div style="text-align: right; margin-top: 15px;">                                
                                <asp:Button ID="btnNovoArquivoAtividade" runat="server" Text="Anexar Arquivo" CssClass="botao novo" OnClick="btnNovoArquivoAtividade_Click" />&nbsp;
                                <asp:Button ID="btnExcluirArquivoAtividade" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoAtividade_Click" />&nbsp;
                                <asp:Button ID="btnRenomearArquivoAtividade" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoAtividade_Click" OnInit="btnRenomearArquivoAtividade_Init" />
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfId" runat="server" />
                    <div style="clear: both"></div>
                    <div style="text-align: right">
                        <div style="text-align: right; margin-top: 10px;">
                            <asp:Button ID="btnSalvarAtividade" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvarAtividade_Click"/>
                        </div>
                    </div>
                </ContentTemplate>                
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNovoArquivoAtividade" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnExcluirArquivoAtividade" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSalvarAtividade" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnRenomearArquivoAtividade" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnEditarDetalhamentosAtividade" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="trvArquivosAtividade" EventName="SelectedNodeChanged" />                    
                </Triggers>
            </asp:UpdatePanel>
        </div>        
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" Runat="Server">

    <div id="Popup_Historico_Detalhamentos" class="pop_up" style="width: 750px;">
        <div class="barra">
            Histórico de Detalhamentos
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

    <div id="popArquivos" class="pop_up" style="width: 550px;">
        <div class="barra">
            Upload de Arquivos
        </div>
        <div class="pop_m20">
            <div style="position: relative;">
                <cc1:UpLoad ID="UpLoad2" runat="server" OnUpLoadComplete="UpLoad2_UpLoadComplete" Pagina="HandlerArquivosAtividade.ashx" TamanhoMaximoArquivo="102400" TamanhoParteUpload="102400" OnInit="UpLoad2_Init">
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

</asp:Content>

