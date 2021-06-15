<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroPedidos.aspx.cs" Inherits="Pedidos_CadastroPedidos" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%= lkbExibirDadosOS.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirDadosOS.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirVisitasOS.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirVisitasOS.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirAtividadesOS.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirAtividadesOS.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= lkbExibirArquivosOS.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirArquivosOS.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });

            $("#<%= btnNovoCadastroOS.ClientID %>").click(function () {
                MudarClassesTabs();
                $("#<%= lkbExibirDadosOS.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
            });


            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("pedidos")) {
                    $(this).removeClass("pedidos");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".pedidos").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".pedidos").children("a").children("span").addClass("bg_branco");
                }
            });

            $('.valor_os').find('.mascara_dinheiro').blur(function () {
                calcularValorTotal();
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                $('.valor_os').find('.mascara_dinheiro').blur(function () {
                    calcularValorTotal();
                });
            });

        });

        function MudarClassesTabs() {
            $('.link_aba_escolhida').removeClass('link_aba_escolhida').addClass('link_aba');
        }

        function SelecionarPrimeiraTab() {
            MudarClassesTabs();
            $("#<%= lkbExibirDadosOS.ClientID %>").removeClass('link_aba').addClass('link_aba_escolhida');
        }

        function MascarasDecimal(id) {
            var ab = id;
            $(ab).unbind();
            $(ab).maskMoney({ thousands: '', decimal: ',' });
        }

        function AtivaBotao(lista, classe) {
            for (var i = 0; i < lista.length; i++) {
                $(lista[i]).removeAttr("disabled");
                $(lista[i]).removeClass(classe);
            }
        }

        function DesativaBotao(lista, classe) {
            for (var i = 0; i < lista.length; i++) {
                $(lista[i]).attr("disabled", "disabled");
                $(lista[i]).addClass(classe);
            }
        }

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }

        function mascaraCPF(id) {
            var ab = id;
            $(ab).unbind();
            $(ab).mask("999.999.999-99");
        }

        function mascaraCEP(id) {
            var ab = id;
            $(ab).unbind();
            $(ab).mask("99.999-999");
        }

        function changeRating(rate) {
            $('#<%= hfRating.ClientID %>').val(rate);
        }

        function calcularValorTotal() {
            var valorNominal = ($('.valor_os').find('.valor_nominal') && $('.valor_os').find('.valor_nominal').val() != '' ? parseFloat($('.valor_os').find('.valor_nominal').val().replace(',', '.')) : 0);
            var valorDesconto = ($('.valor_os').find('.desconto') && $('.valor_os').find('.desconto').val() != '' ? parseFloat($('.valor_os').find('.desconto').val().replace(',', '.')) : 0);
            var valorTotal = valorNominal - valorDesconto;
            $('.valor_os').find('.valor_total').val(valorTotal.toFixed(2).replace('.', ','));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Cadastro de pedido
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:HiddenField ID="hfContrato" runat="server"></asp:HiddenField>
    <asp:ModalPopupExtender ID="PopupContrato_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_contrato"
        Enabled="True" PopupControlID="popup_contrato" TargetControlID="hfContrato">
    </asp:ModalPopupExtender>
    <asp:HiddenField ID="hfEdicaoParcela" runat="server"></asp:HiddenField>
    <asp:ModalPopupExtender ID="PopupParcela_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_parcela"
        Enabled="True" PopupControlID="poup_parcela" TargetControlID="hfEdicaoParcela">
    </asp:ModalPopupExtender>

    <asp:Label ID="lblCadastroOS" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroOS_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_cad_os"
        Enabled="True" PopupControlID="Popup_OS" TargetControlID="lblCadastroOS">
    </asp:ModalPopupExtender>
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
    <asp:Label ID="lblCalcularPrazos" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCalcularPrazos_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarCalculadoraPrazos"
        Enabled="True" PopupControlID="popCalcularPrazos" TargetControlID="lblCalcularPrazos">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblReplicarOSPeriodica" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="PopReplicarOSPeriodica_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="fechar_replicar_os_periodica"
        Enabled="True" PopupControlID="popReplicarOSPeriodica" TargetControlID="lblReplicarOSPeriodica">
    </asp:ModalPopupExtender>
    <asp:HiddenField runat="server" ID="hfPopupOSMatriz" />
    <asp:ModalPopupExtender ID="PopupOSMatriz_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="btnCancelarOSMatriz"
        Enabled="True" PopupControlID="popup_osmatriz" TargetControlID="hfPopupOSMatriz">
    </asp:ModalPopupExtender>

    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />

    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioPedido" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlOrcamentoCarregado">
                    <div style="border: 1px solid #ccc; background: #f3c387; padding: 10px; margin: 5px; text-align: center; border-radius: 3px;">
                        Este pedido será vinculado ao orçamento:
                        <asp:Label runat="server" ID="lblOrcamentoVinculado" />. Ao salvar será necessário indicar o responsável das OSs geradas
                    </div>
                </asp:Panel>
                <div style="float: left; width: 40%; margin-right: 10px;">
                    <div class="barra">
                        Dados Básicos
                    </div>
                    <div class="cph">
                        <div style="display: block;">
                            <div>
                                <div style="display: inline-block; width: 30%; vertical-align: bottom;">
                                    <div class="campo_form">
                                        <asp:CheckBox ID="chkPedidoAtivo" runat="server" Text="Ativo" Checked="True" />
                                    </div>
                                </div>
                                <div style="display: inline-block; width: 30%; vertical-align: bottom;">
                                    <div class="campo_form">
                                        <asp:CheckBox ID="chkContratoFixo" runat="server" Text="Contrato fixo" />
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div style="display: inline-block; width: 55%; padding-right: 1%;">
                                    <div class="label_form">
                                        <caption>
                                            Código<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="tbxCodigoPedido" CssClass="RequireFieldValidator" ErrorMessage="- Código" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                        </caption>
                                    </div>
                                    <div class="campo_form">
                                        <div style="display: inline-block; width: 60%;">
                                            <asp:UpdatePanel runat="server" ID="upCodigo" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="tbxCodigoPedido" CssClass="textBox100" runat="server"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGerarProximoCodigo" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div style="display: inline-block;">
                                            <asp:Button runat="server" ID="btnGerarProximoCodigo" Text="Gerar" CssClass="botao novo mini" ToolTip="Gerar próximo código" OnClick="btnGerarProximoCodigo_Click" Style="padding-left: 25px" />
                                        </div>
                                    </div>
                                </div>
                                <div style="display: inline-block; width: 43%;">
                                    <div class="label_form">
                                        <caption>
                                            Data<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxDataPedido" CssClass="RequireFieldValidator" ErrorMessage="- Data" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                        </caption>
                                    </div>
                                    <asp:TextBox ID="tbxDataPedido" runat="server" CssClass="textBox100"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbxDataPedido" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div>
                                <div class="label_form">
                                    Tipo<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoPedido" InitialValue="0" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Tipo" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:DropDownList ID="ddlTipoPedido" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div>
                                <div class="label_form">
                                    Cliente<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClientePedido" InitialValue="0" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Cliente" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:DropDownList ID="ddlClientePedido" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div>
                                <div class="label_form">
                                    Vendedor:
                                </div>
                                <div class="campo_form">
                                    <asp:DropDownList ID="ddlVendedor" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div>
                                <div class="label_form">
                                    Contrato:
                                </div>
                                <div class="campo_form">
                                    <asp:UpdatePanel ID="upStatusContratoPedido" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div style="border: 1px solid #ccc; padding: 7px; border-radius: 2px;">
                                                <i class="fa fa-file-text-o"></i>
                                                <asp:LinkButton ID="btnContratoPedido" runat="server" OnInit="btnContratoPedido_Init" OnClick="btnContratoPedido_Click">
                                                    Salve o pedido primeiro
                                                </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarPedido" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div style="margin-top: 10px">
                                <div class="label_form">
                                    Detalhamento
                                    <asp:Button ID="btnEditarDetalhamento" class="botao editar_mini" runat="server" ToolTip="Alterar Detalhamento" OnClick="btnEditarDetalhamento_Click" />
                                    <asp:Button ID="btnVerHistorioDetalhamentosPedidos" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnClick="btnVerHistorioDetalhamentosPedidos_Click" OnInit="btnVerHistorioDetalhamentosPedidos_Init" />
                                </div>
                                <div class="campo_form" style="margin-top: 5px;">
                                    <div id="detalhamento_edicao" runat="server" visible="false">
                                        <cc2:Editor ID="editDetalhamento" runat="server" Height="300px" NoUnicode="true" />
                                    </div>
                                    <div id="detalhamento_visualizacao" runat="server" visible="false">
                                        <div style="border: 1px solid silver;">
                                            <asp:Label ID="tbxDetalhamentoVisualizacao" runat="server" CssClass="textBox100" Height="350px" Style="overflow: auto; max-height: 300px;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="float: right; width: 59%;">
                    <asp:MultiView ID="mtvOS" runat="server" ActiveViewIndex="0">
                        <asp:View runat="server"></asp:View>
                        <asp:View runat="server">
                            <div class="barra">
                                Ordens de Serviço
                                <div class="close" onclick="minimizar('ordens_container');">
                                </div>
                            </div>
                            <div id="ordens_container" class="cph" style="margin-bottom:10px">
                                <div style="max-height: 300px; overflow-y: auto">
                                    <asp:GridView ID="gdvOrdensServico" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvOrdensServico_PageIndexChanging" OnPreRender="gdvOrdensServico_PreRender" OnRowDeleting="gdvOrdensServico_RowDeleting" PageSize="5">
                                        <Columns>
                                            <asp:BoundField DataField="Codigo" HeaderText="Número" />
                                            <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                            <asp:BoundField DataField="GetDescricaoDepartamento" HeaderText="Departamento" />
                                            <asp:BoundField DataField="GetDescricaoSetor" HeaderText="Setor" />
                                            <asp:BoundField DataField="GetResponsavel" HeaderText="Responsável" />
                                            <asp:BoundField DataField="ValorTotal" HeaderText="Valor total" DataFormatString="{0:c}" />
                                            <asp:TemplateField HeaderText="Editar / Excluir">
                                                <ItemTemplate>
                                                    <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Button ID="btnGridEditarOS" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="botao editar_mini" Text="" ToolTip="Editar" OnClick="btnGridEditarOS_Click" OnInit="btnGridEditarOS_Init" />
                                                    </div>
                                                    <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Button ID="btnGridExcluirOS" runat="server" CommandName="Delete" CssClass="botao excluir_mini" Text="" ToolTip="Excluir" OnPreRender="btnGridExcluirOS_PreRender" />
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
                                </div>
                                <div style="text-align: right; margin-top: 10px;">
                                    <asp:Button ID="btnNovaOS" runat="server" Text="Nova OS" CssClass="botao novo" OnInit="btnNovaOS_Init" OnClick="btnNovaOS_Click" />
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>

                    <div class="barra">
                        Anexos<div class="close" onclick="minimizar('anexos_container');"></div>
                    </div>
                    <div id="anexos_container" class="cph">
                        <div>
                            <asp:TreeView ID="trvAnexosPedido" runat="server" OnSelectedNodeChanged="trvAnexosPedido_SelectedNodeChanged">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px" VerticalPadding="0px" />
                            </asp:TreeView>
                        </div>
                        <div id="visualizar_arquivos_pedido" runat="server" style="text-align: right; margin-top: 10px;" visible="false">
                            <asp:HyperLink ID="hplArquivosPedido" runat="server" Target="_blank" CssClass="botao visualizar" Text="Visualizar Arquivo"></asp:HyperLink>
                        </div>
                        <div style="text-align: right; margin-top: 15px;">
                            <asp:Button ID="btnNovoArquivoPedido" runat="server" Text="Anexar Arquivo/Contrato" CssClass="botao novo" OnClick="btnNovoArquivoPedido_Click" />&nbsp;
                                <asp:Button ID="btnExcluirArquivoPedido" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoPedido_Click" OnPreRender="btnExcluirArquivoPedido_PreRender" />&nbsp;
                                <asp:Button ID="btnRenomearArquivoPedido" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoPedido_Click" OnInit="btnRenomearArquivoPedido_Init" />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="upFinanceiroPedido" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="barra" style="margin-top: 10px;">
                                Financeiro do pedido
                                <div class="close" onclick="minimizar('financeiro_container');">
                                </div>
                            </div>
                            <div id="financeiro_container" class="cph">
                                <div>
                                    <div>
                                        <asp:Label ID="lblTextoAuxiliarFinanceiroPedido" runat="server" Text=""></asp:Label>
                                    </div>
                                    <asp:MultiView ID="mtvFinanceiro" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="View1" runat="server">
                                            Salve o pedido primeiro
                                        </asp:View>
                                        <asp:View ID="View2" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 33%;">
                                                        <div class="label_form">
                                                            Valor total*
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtValorTotalPedido" SetFocusOnError="true"
                                                                    CssClass="RequireFieldValidator" ErrorMessage="- Valor total do pedido" Display="Dynamic" ValidationGroup="rfvFinanceiro">*obrigatório!</asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:TextBox ID="txtValorTotalPedido" runat="server" CssClass="textBox100 mascara_dinheiro" />
                                                    </td>
                                                    <td style="width: 33%;">
                                                        <div class="label_form">
                                                            Dividir em*
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtDividirEm" SetFocusOnError="true"
                                                                    CssClass="RequireFieldValidator" ErrorMessage="- Dividir em" Display="Dynamic" ValidationGroup="rfvFinanceiro" InitialValue="0">*obrigatório!</asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:TextBox ID="txtDividirEm" runat="server" CssClass="textBox100 mascara_inteiro" />
                                                    </td>
                                                    <td style="width: 33%;">
                                                        <div class="label_form">
                                                            Primeiro pagamento*
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtPrimeiroPagamento" SetFocusOnError="true"
                                                                    CssClass="RequireFieldValidator" ErrorMessage="- Primeiro pagamento" Display="Dynamic" ValidationGroup="rfvFinanceiro">*obrigatório!</asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:TextBox ID="txtPrimeiroPagamento" runat="server" CssClass="textBox100" />
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtPrimeiroPagamento" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 33%;">
                                                        <div class="label_form">
                                                            Classificação do cliente*
                                                        </div>
                                                        <fieldset class="rating">
                                                            <asp:HiddenField runat="server" ID="hfRating" Value="0" />
                                                            <input type="radio" id="star5" name="rating" value="5" onclick="changeRating(5)" /><label class="full" for="star5"></label>
                                                            <input type="radio" id="star4" name="rating" value="4" onclick="changeRating(4)" /><label class="full" for="star4"></label>
                                                            <input type="radio" id="star3" name="rating" value="3" onclick="changeRating(3)" /><label class="full" for="star3"></label>
                                                            <input type="radio" id="star2" name="rating" value="2" onclick="changeRating(2)" /><label class="full" for="star2"></label>
                                                            <input type="radio" id="star1" name="rating" value="1" onclick="changeRating(1)" /><label class="full" for="star1"></label>
                                                        </fieldset>
                                                    </td>
                                                    <td colspan="2" style="text-align: right">
                                                        <asp:CheckBox runat="server" ID="chkXMark" />&nbsp;
                                                        <asp:Button ID="btnGerarFinanceiro" runat="server" Text="Gerar financeiro" ValidationGroup="rfvFinanceiro" CssClass="botao ok" OnClick="btnGerarFinanceiro_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="View3" runat="server">
                                            <div>
                                                <div style="display: inline-block; width: 40%">
                                                    <div class="label_form">
                                                        Valor total do pedido
                                                    </div>
                                                    <asp:TextBox ID="txtValorPedidoFinanceiro" Enabled="false" runat="server" CssClass="textBox100" />
                                                </div>
                                                <div style="display: inline-block; width: 40%">
                                                    <div class="label_form">
                                                        Orçamento associado
                                                    </div>
                                                    <asp:TextBox ID="txtOrcamentoAssociadoPedido" Enabled="false" runat="server" CssClass="textBox100" />
                                                </div>
                                                <div style="display: inline-block; width: 19%">
                                                    <div class="label_form">
                                                        &nbsp;
                                                    </div>
                                                    <asp:CheckBox runat="server" ID="chkXMarkEdicao" />
                                                </div>
                                            </div>
                                            <div style="margin-top: 5px">
                                                <asp:UpdatePanel ID="upParcelasFinanceiroPedido" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="grvParcelasPedido" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                                            BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="Numero" HeaderText="Número" />
                                                                <asp:BoundField DataField="DataEmissao" DataFormatString="{0:d}" HeaderText="Emissão" />
                                                                <asp:BoundField DataField="DataVencimento" DataFormatString="{0:d}" HeaderText="Vencimento" />
                                                                <asp:BoundField DataField="GetDescricaoStatus" HeaderText="Status" />
                                                                <asp:TemplateField HeaderText="Valor nominal">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("ValorNominal","{0:c}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>
                                                                            <asp:Label runat="server" Text='<%# BindingValorNominalTotalParcelas() %>'></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Desconto">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("Descontos","{0:c}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>
                                                                            <asp:Label runat="server" Text='<%# BindingDescontoTotalParcelas() %>'></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Valor total">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# Eval("ValorTotal","{0:c}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>
                                                                            <asp:Label runat="server" ForeColor='<%# BindingCorValorTotalParcelas() %>' Text='<%# BindingValorTotalParcelas() %>'></asp:Label></b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Editar / Excluir">
                                                                    <ItemTemplate>
                                                                        <div style="text-align: center">
                                                                            <div style="display: inline-block; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                                                <asp:Button ID="btnGridEditarParcela" Visible='<%#Eval("IsPodeEditar") %>' runat="server" CommandArgument='<%# Eval("Id") %>' OnInit="btnGridEditarParcela_Init" OnClick="btnGridEditarParcela_Click" CssClass="botao editar_mini" Text="" ToolTip="Editar" />
                                                                            </div>
                                                                            <div style="display: inline-block; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                                                <asp:Button ID="btnGridExcluirParcela" Visible='<%#Eval("IsPodeEditar") %>' runat="server" CommandArgument='<%# Eval("Id") %>' OnPreRender="btnGridExcluirParcela_PreRender" OnClick="btnGridExcluirParcela_Click"
                                                                                    CssClass="botao excluir_mini" Text="" ToolTip="Excluir" />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="gridPager" />
                                                            <FooterStyle CssClass="gridPager" />
                                                            <RowStyle CssClass="gridRow" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnNovaParcela" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div style="margin-top: 5px">
                                                <div style="display: inline-block">
                                                    <asp:Button ID="btnNovaParcela" runat="server" Text="Nova parcela" CssClass="botao novo" OnClick="btnNovaParcela_Click" />
                                                </div>
                                                <div style="float: right">
                                                    <span style="background-color: #e60000">&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;Total diferente do valor total do pedido
                                                </div>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGerarFinanceiro" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <asp:HiddenField ID="hfId" runat="server" />
                <div style="clear: both"></div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSalvarPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNovoPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluirPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluirArquivoPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gdvOrdensServico" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="trvAnexosPedido" EventName="SelectedNodeChanged" />
                <asp:AsyncPostBackTrigger ControlID="gdvOrdensServico" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="text-align: right; margin-top: 10px;">
        <asp:UpdatePanel ID="upOpcaoOrcamento" runat="server" ChildrenAsTriggers="True" UpdateMode="Conditional">
            <ContentTemplate>
                <a runat="server" id="linkOrcamento" href="#" target="_blank" class="botao visualizar">Visualizar orçamento associado</a>
                <asp:Button ID="btnNovoPedido" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoPedido_Click" />
                <asp:Button ID="btnSalvarPedido" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgPedido" OnClick="btnSalvarPedido_Click" />
                <asp:Button ID="btnExcluirPedido" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirPedido_Click" OnPreRender="btnExcluirPedido_PreRender" />
                <asp:Button ID="btnEnviarEmailPedido" runat="server" Text="Reenviar confirmação" ToolTip="Reenvia o e-mail de confirmação de cadastro de pedido para o cliente" CssClass="botao email" OnClick="btnEnviarEmailPedido_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="Popup_OS" class="pop_up" style="width: 70%;">
        <div class="barra">
            Cadastro de OS                      
        </div>
        <div class="pop_m20">
            <div style="text-align: right; vertical-align: middle; border-bottom: 2px solid #117c4a !important;">
                <asp:LinkButton ID="lkbExibirDadosOS" runat="server" CssClass="link_aba_escolhida" Height="30px" OnClick="lkbExibirDadosOS_Click">Dados</asp:LinkButton>
                <asp:LinkButton ID="lkbExibirVisitasOS" runat="server" CssClass="link_aba" Height="30px" OnClick="lkbExibirVisitasOS_Click">Visitas da OS</asp:LinkButton>
                <asp:LinkButton ID="lkbExibirAtividadesOS" runat="server" CssClass="link_aba" Height="30px" OnClick="lkbExibirAtividadesOS_Click">Atividades da OS</asp:LinkButton>
                <asp:LinkButton ID="lkbExibirArquivosOS" runat="server" CssClass="link_aba" Height="30px" OnClick="lkbExibirArquivosOS_Click">Arquivos</asp:LinkButton>
            </div>
            <div class="cph" style="position: relative; overflow: auto; max-height: 400px;">
                <div style="overflow: auto; max-height: 400px;">
                    <asp:UpdatePanel ID="upExibicoesDaOS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                        <ContentTemplate>
                            <asp:MultiView ID="mvFormOS" runat="server" ActiveViewIndex="0">
                                <asp:View ID="dados_view" runat="server">
                                    <asp:UpdatePanel ID="upFormOS" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div>
                                                <div style="display: inline-block; width: 32%; padding-right: 5px">
                                                    <div class="label_form">&nbsp;</div>
                                                    <div class="campo_form" style="vertical-align: bottom;">
                                                        <asp:CheckBox ID="ckbAtivoOS" runat="server" Text="Ativa" Checked="True" />
                                                        <asp:HiddenField ID="hdIdOS" runat="server" />
                                                    </div>
                                                </div>
                                                <div style="display: inline-block; width: 32%; padding-right: 5px">
                                                    <div class="label_form">
                                                        Código
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxCodigoOS" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="display: inline-block; width: 32%;">
                                                    <div class="label_form">
                                                        Data<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="tbxDataOS" CssClass="RequireFieldValidator"
                                                            SetFocusOnError="true" ErrorMessage="- Data" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <asp:TextBox ID="tbxDataOS" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbxDataOS" Format="dd/MM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="valor_os">
                                                <asp:UpdatePanel ID="upFaturamentoOS" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div style="display: inline-block; width: 24%; padding-right: 5px">
                                                            <div class="label_form">
                                                                Faturamento<span>*:</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlOSFaturada" InitialValue="0" CssClass="RequireFieldValidator"
                                                                    SetFocusOnError="true" ErrorMessage="- Faturamento" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="campo_form" style="vertical-align: bottom;">
                                                                <asp:DropDownList ID="ddlOSFaturada" CssClass="dropDownList100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOSFaturada_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Text="--Selecione--" Value="0" />
                                                                    <asp:ListItem Text="OS com Faturamento" Value="1" />
                                                                    <asp:ListItem Text="OS sem Faturamento" Value="2" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div style="display: inline-block; width: 24%; padding-right: 5px">
                                                            <div class="label_form">
                                                                Valor nominal
                                                            </div>
                                                            <asp:TextBox ID="txtValorNominalOS" CssClass="textBox100 mascara_dinheiro valor_nominal" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div style="display: inline-block; width: 24%; padding-right: 5px">
                                                            <div class="label_form">
                                                                Desconto
                                                            </div>
                                                            <asp:TextBox ID="txtDescontoOS" CssClass="textBox100 mascara_dinheiro desconto" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div style="display: inline-block; width: 24%;">
                                                            <div class="label_form">
                                                                Valor total
                                                            </div>
                                                            <asp:TextBox ID="txtValorTotalOS" Enabled="false" CssClass="textBox100 valor_total" runat="server"></asp:TextBox>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div>
                                                <div style="display: inline-block; width: 49%; padding-right: 5px">
                                                    <div class="label_form">
                                                        Pedido
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxPedidoOS" CssClass="textBox100" runat="server" Text="Carregado Automaticamente..." ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="display: inline-block; width: 49%;">
                                                    <div class="label_form">
                                                        Tipo de OS<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                            SetFocusOnError="true" ErrorMessage="- Tipo" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:DropDownList ID="ddlTipoOS" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoOS_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="padding-right: 5px;">
                                                <div class="label_form">
                                                    Descrição<span>*:</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="tbxDescricaoOS" CssClass="RequireFieldValidator"
                                                        SetFocusOnError="true" ErrorMessage="- Descrição" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                </div>
                                                <asp:TextBox ID="tbxDescricaoOS" CssClass="textBox100" runat="server"></asp:TextBox>
                                            </div>
                                            <div>
                                                <div style="display: inline-block; width: 32%; padding-right: 5px">
                                                    <div class="label_form">
                                                        Prazo Padrão<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbxPrazoPadraoOS" CssClass="RequireFieldValidator"
                                                            SetFocusOnError="true" ErrorMessage="- Prazo Padrão" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxPrazoPadraoOS" CssClass="textBox" runat="server" Enabled="false"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxPrazoPadraoOS" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                                <div style="display: inline-block; width: 32%; padding-right: 5px">
                                                    <div class="label_form">
                                                        Prazo Legal
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxPrazoLegalOS" CssClass="textBox" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbxPrazoLegalOS" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        <asp:Button ID="btnCalcularPrazoLegal" class="botao calcular_mini" runat="server" ToolTip="Calcular Prazo Legal" OnClick="btnCalcularPrazoLegal_Click" OnInit="btnCalcularPrazoLegal_Init" />
                                                    </div>
                                                </div>
                                                <div style="display: inline-block; width: 32%; padding-right: 5px">
                                                    <div class="label_form">
                                                        Prazo de Diretoria
                                                    </div>
                                                    <div class="campo_form">
                                                        <asp:TextBox ID="tbxPrazoDiretoriaOS" CssClass="textBox" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbxPrazoDiretoriaOS" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                        <asp:Button ID="btnCalcularPrazoDiretoria" class="botao calcular_mini" runat="server" ToolTip="Calcular Prazo de Diretoria" OnClick="btnCalcularPrazoDiretoria_Click" OnInit="btnCalcularPrazoDiretoria_Init" />
                                                    </div>
                                                </div>
                                            </div>
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">

                                                <tr>
                                                    <td colspan="3" style="padding-right: 15px;">
                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="width: 50%;">
                                                                    <div class="label_form">
                                                                        Departamento<span>*:</span>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDepartamentoOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                                            SetFocusOnError="true" ErrorMessage="- Departamento" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="campo_form" style="padding-right: 3%;">
                                                                        <asp:DropDownList ID="ddlDepartamentoOS" CssClass="dropDownList100" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamentoOS_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 50%;">
                                                                    <div class="label_form">
                                                                        Setor<span>*:</span>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSetorOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                                            SetFocusOnError="true" ErrorMessage="- Setor" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="campo_form">
                                                                        <asp:DropDownList ID="ddlSetorOS" CssClass="dropDownList100" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;">
                                                                    <div class="label_form">
                                                                        Órgão<span>*:</span>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlOrgaoOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                                            SetFocusOnError="true" ErrorMessage="- Órgão" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="campo_form" style="padding-right: 3%;">
                                                                        <asp:DropDownList ID="ddlOrgaoOS" CssClass="dropDownList100" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 50%; vertical-align: bottom;">
                                                                    <div class="label_form">
                                                                        Número do Processo
                                                                    </div>
                                                                    <div class="campo_form">
                                                                        <asp:TextBox ID="tbxNumeroProcessoOrgaoOS" CssClass="textBox100" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div class="label_form">
                                                                        Responsável<span>*:</span>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlResponsavelOS" InitialValue="0" CssClass="RequireFieldValidator"
                                                                            SetFocusOnError="true" ErrorMessage="- Responsável" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="campo_form">
                                                                        <asp:DropDownList ID="ddlResponsavelOS" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResponsavelOS_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div class="label_form" style="margin-top: 10px;">
                                                                        Corresponsável (eis)
                                                                    </div>
                                                                    <div class="campo_form">
                                                                        <div style="overflow: auto; max-height: 100px;">
                                                                            <asp:CheckBoxList ID="cblCorresponsaveis" runat="server"></asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div class="label_form" style="margin-top: 15px;"></div>
                                                                    <div class="campo_form">
                                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td style="width: 100%">
                                                                                    <asp:CheckBox ID="ckbRenovavelOS" runat="server" Text="OS períodica" AutoPostBack="true" OnCheckedChanged="ckbRenovavelOS_CheckedChanged" OnInit="ckbRenovavelOS_Init" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 100%;">
                                                                                    <div style="margin-top: 5px;">
                                                                                        <asp:Label ID="lblOSSeraReplicaraPara" runat="server"></asp:Label>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="padding-right: 15px;">
                                                        <asp:UpdatePanel runat="server" ID="upOsVinculada" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table style="width: 100%; margin-top: 20px;" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 70%;">
                                                                            <div class="label_form">
                                                                                OS Matriz
                                                                            </div>
                                                                            <div class="campo_form" style="padding-right: 5px;">
                                                                                <asp:HiddenField runat="server" ID="hfIdOsMatriz" />
                                                                                <asp:TextBox runat="server" ID="txtOsMatriz" CssClass="textBox100" Enabled="false" Text="Nenhuma OS vinculada" />
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="label_form">
                                                                                &nbsp;
                                                                            </div>
                                                                            <asp:Button runat="server" ID="btnEscolherOSMatriz" Text="Escolher" CssClass="botao novo" OnInit="btnEscolherOSMatriz_Init" OnClick="btnEscolherOSMatriz_Click" />
                                                                            <asp:Button runat="server" ID="btnRemoverOsMatriz" Text="Remover" CssClass="botao excluir" OnClick="btnRemoverOsMatriz_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnRemoverOsMatriz" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div class="label_form" style="margin-top: 20px;">
                                                            Detalhamento
                                                            <asp:Button ID="btnEditarDetalhamentoOS" class="botao editar_mini" runat="server" ToolTip="Alterar Detalhamento" OnClick="btnEditarDetalhamentoOS_Click" />
                                                            <asp:Button ID="btnVisualizarDetalhamentosOS" class="botao relogio_mini" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnClick="btnVisualizarDetalhamentosOS_Click" OnInit="btnVisualizarDetalhamentosOS_Init" />
                                                        </div>
                                                        <div class="campo_form" style="margin-top: 5px; overflow: auto; max-height: 350px; padding-right: 15px;">
                                                            <div id="detalhamento_edicao_os" runat="server" visible="false">
                                                                <cc2:Editor ID="editDetalhamentoOS" runat="server" Height="400px" NoUnicode="true" />
                                                            </div>
                                                            <div id="detalhamento_visualizacao_os" runat="server" visible="false">
                                                                <div style="border: 1px solid silver;">
                                                                    <asp:Label ID="tbxVisualizarDetalhamentoOS" runat="server" CssClass="textBox100" Height="250px" Style="overflow: auto; max-height: 250px;"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoOS" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroOS" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarOS" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlTipoOS" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlResponsavelOS" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ckbRenovavelOS" EventName="CheckedChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </asp:View>
                                <asp:View ID="visitas_view" runat="server">
                                    <div class="barra">
                                        Visitas
                                        <div class="close" onclick="minimizar('visitas_container');">
                                        </div>
                                    </div>
                                    <div id="visitas_container" class="cph">
                                        <div>
                                            <asp:UpdatePanel ID="upVisitasOS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gdvVisitas" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvVisitas_PageIndexChanging" OnPreRender="gdvVisitas_PreRender" PageSize="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="GetDataInicio" HeaderText="Início" />
                                                            <asp:BoundField DataField="GetDataFim" HeaderText="Fim" />
                                                            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                            <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
                                                            <asp:TemplateField HeaderText="Visualizar">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="btnGridVisualizarVisita" runat="server" CssClass="botao visualizar_mini" Target="_blank" NavigateUrl="<%# BindVisualizarVisita(Container.DataItem) %>" ToolTip="Visualizar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
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
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="gdvVisitas" EventName="PageIndexChanging" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="atividades_view" runat="server">
                                    <div class="barra">
                                        Atividades
                                        <div class="close" onclick="minimizar('atividades_container');">
                                        </div>
                                    </div>
                                    <div id="atividades_container" class="cph">
                                        <div>
                                            <asp:UpdatePanel ID="upAtividadesOS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gdvAtividades" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvAtividades_PageIndexChanging" OnPreRender="gdvAtividades_PreRender" PageSize="3">
                                                        <Columns>
                                                            <asp:BoundField DataField="Data" DataFormatString="{0:d}" HeaderText="Data" />
                                                            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                            <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
                                                            <asp:TemplateField HeaderText="Visualizar">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="btnGridVisualizarAtividade" runat="server" CssClass="botao visualizar_mini" Target="_blank" NavigateUrl="<%# BindVisualizarAtividade(Container.DataItem) %>" ToolTip="Visualizar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
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
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="gdvAtividades" EventName="PageIndexChanging" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="arquivos_view" runat="server">
                                    <div class="barra">
                                        Arquivos
                                        <div class="close" onclick="minimizar('arquivos_os_container');">
                                        </div>
                                    </div>
                                    <div id="arquivos_os_container" class="cph">
                                        <div>
                                            <asp:UpdatePanel ID="upArquivosOS" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TreeView ID="trvArquivosOS" runat="server" OnSelectedNodeChanged="trvArquivosOS_SelectedNodeChanged">
                                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                        <ParentNodeStyle Font-Bold="False" />
                                                        <SelectedNodeStyle Font-Underline="True" ForeColor="#339933" HorizontalPadding="0px" VerticalPadding="0px" />
                                                    </asp:TreeView>
                                                    <div id="visualizar_arquivos_os" runat="server" style="text-align: right; margin-top: 10px;" visible="false">
                                                        <asp:HyperLink ID="hplArquivosOS" runat="server" Target="_blank" CssClass="botao visualizar" Text="Visualizar Arquivo"></asp:HyperLink>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnEditarDetalhamentoOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnExcluirArquivoOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnRenomearArquivoOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoArquivoOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroOS" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="trvArquivosOS" EventName="SelectedNodeChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div style="text-align: right; margin-top: 20px;">
                                            <asp:Button ID="btnNovoArquivoOS" runat="server" Text="Anexar Arquivo" CssClass="botao novo" OnClick="btnNovoArquivoOS_Click" />&nbsp;
                                            <asp:Button ID="btnExcluirArquivoOS" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoOS_Click" OnInit="btnExcluirArquivoOS_Init" />&nbsp;
                                            <asp:Button ID="btnRenomearArquivoOS" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoOS_Click" OnInit="btnRenomearArquivoOS_Init" />
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirDadosOS" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirVisitasOS" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirAtividadesOS" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lkbExibirArquivosOS" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroOS" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoCadastroOS" runat="server" Text="Nova OS" CssClass="botao novo" OnClick="btnNovoCadastroOS_Click" />
                &nbsp;
                <asp:Button ID="btnSalvarOS" runat="server" Text="Salvar e Enviar OS" CssClass="botao salvar" ValidationGroup="vlgOS" OnClick="btnSalvarOS_Click" OnInit="btnSalvarOS_Init" />
                &nbsp;
                <a id="cancelar_cad_os" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="Popup_Historico_Detalhamentos" class="pop_up" style="width: 750px;">
        <div class="barra">
            Histórico de Detalhamentos
        </div>
        <div class="pop_m20">
            <div style="position: relative; overflow: auto; max-height: 600px;">
                <asp:UpdatePanel ID="upHistoricosDetalhamentos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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
                <cc1:UpLoad ID="UpLoad2" runat="server" OnUpLoadComplete="UpLoad2_UpLoadComplete" Pagina="HandlerArquivosPedidos.ashx" TamanhoMaximoArquivo="102400" TamanhoParteUpload="102400" OnInit="UpLoad2_Init">
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxNovoNomeArquivo" CssClass="RequireFieldValidator" ErrorMessage="- Nome"
                    ValidationGroup="vlgRenomearArquivo">*</asp:RequiredFieldValidator>
            </div>
            <div class="filtros_campo" style="padding-right: 15px;">
                <asp:UpdatePanel ID="upRenomearArquivo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
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
            <asp:Button ID="btnSalvarRenomearArquivo" CssClass="botao salvar" runat="server" Text="Salvar" ValidationGroup="vlgRenomearArquivo" OnClick="btnSalvarRenomearArquivo_Click" OnInit="btnSalvarRenomearArquivo_Init" />
            &nbsp;                                   
            <a id="cancelar_renomear_arquivo" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="popCalcularPrazos" class="pop_up" style="width: 400px;">
        <div class="barra">
            Calculadora
        </div>
        <div class="pop_m20">
            <asp:UpdatePanel ID="upCalcularPrazos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Data da Retirada<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbxDataRetirada" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Nome" ValidationGroup="vlgCalcularPrazo">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataRetirada" runat="server" CssClass="textBox100"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbxDataRetirada" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form">
                                    Prazo para cumprimento (dias)<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tbxPrazoCumprimentoDias" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Nome" ValidationGroup="vlgCalcularPrazo">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxPrazoCumprimentoDias" runat="server" CssClass="textBox100"></asp:TextBox>
                                </div>
                                <asp:HiddenField ID="hfCalcularPrazo" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnCalcular" CssClass="botao calcular" runat="server" Text="Calcular" ValidationGroup="vlgCalcularPrazo" OnClick="btnCalcular_Click" OnInit="btnCalcular_Init" />
            &nbsp;                                   
            <a id="cancelarCalculadoraPrazos" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="popReplicarOSPeriodica" class="pop_up" style="width: 400px;">
        <div class="barra">
            Replicar OS Períodica
        </div>
        <div class="pop_m20">
            <asp:UpdatePanel ID="upReplicarOSPeriodica" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Replicar OS para a data<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="tbxDataReplicarPara" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Nome" ValidationGroup="vlgReplicarOS">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataReplicarPara" runat="server" CssClass="textBox50"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="tbxDataReplicarPara" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    &nbsp;
                                    <asp:Button ID="btnAddDataReplicacao" runat="server" CssClass="botao novo_mini" ValidationGroup="vlgReplicarOS" OnClick="btnAddDataReplicacao_Click" OnInit="btnAddDataReplicacao_Init" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="margin-top: 15px;">
                        <div class="label_form">
                            Replicar OS para as Datas:
                        </div>
                        <asp:GridView ID="gdvDatasReplicacao" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" EnableModelValidation="True"
                            OnPageIndexChanging="gdvDatasReplicacao_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderText="Data">
                                    <ItemTemplate>
                                        <%# BindDataReplicacao(Container.DataItem) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridRow" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAddDataReplicacao" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="gdvDatasReplicacao" EventName="PageIndexChanging" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <a id="fechar_replicar_os_periodica" class="botao vermelho">fechar</a>
        </div>
    </div>

    <div id="popup_contrato" class="pop_up" style="width: 70%">
        <div class="barra">
            Contrato do pedido                     
        </div>
        <div class="cph">
            <div>
                <asp:UpdatePanel ID="upContrato" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div>
                            <div class="label_form">
                                Status do contrato
                            </div>
                            <asp:TextBox ID="txtStatusContratoPopup" Enabled="false" runat="server" CssClass="textBox100" />
                        </div>
                        <div>
                            <div class="label_form">
                                Destinatário(s) (separados por ';')*
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtDestinatariosContratoPopup" SetFocusOnError="true"
                                    CssClass="RequireFieldValidator" ErrorMessage="- Destinatário(s)" ValidationGroup="rfvContrato">*obrigatório!</asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox ID="txtDestinatariosContratoPopup" runat="server" CssClass="textBox100" />
                        </div>
                        <div>
                            <div class="label_form">
                                <asp:Label ID="lblTituloContratacao" runat="server" Text="E-mail para aceite do contrato"></asp:Label>
                            </div>
                            <cc2:Editor ID="txtEmailContratoPopup" runat="server" Height="300px" NoUnicode="true" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarContrato" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnEnviarContrato" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnReenviarEmailAceite" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-top: 15px;">
                <div style="display: inline-block">
                    <asp:UpdatePanel ID="upOpcoesContrato" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnReenviarEmailAceite" runat="server" Text="Reenviar e-mail para aceite" CssClass="botao email" OnInit="btnEnviarContrato_Init" OnClick="btnReenviarEmailAceite_Click" />
                            <asp:Button ID="btnSalvarContrato" runat="server" Text="Salvar" ValidationGroup="rfvContrato" OnClick="btnSalvarContrato_Click" CssClass="botao salvar" />
                            <asp:Button ID="btnEnviarContrato" runat="server" Text="Salvar e enviar" ValidationGroup="rfvContrato" OnInit="btnEnviarContrato_Init" OnClick="btnEnviarContrato_Click" CssClass="botao email" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div style="display: inline-block"><a id="cancelar_contrato" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a></div>
            </div>
        </div>
    </div>

    <div id="poup_parcela" class="pop_up" style="width: 60%">
        <div class="barra">
            Edição de parcela                   
        </div>
        <div class="cph">
            <div>
                <asp:UpdatePanel ID="upEdicaoParcela" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfIdParcelaEdicao" runat="server" />
                        <div style="display: inline-block; width: 33%">
                            <div class="label_form">
                                Vencimento*
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtVencimentoParcela" SetFocusOnError="true"
                                    CssClass="RequireFieldValidator" ErrorMessage="- Vencimento" ValidationGroup="rfvParcela">*obrigatório!</asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox ID="txtVencimentoParcela" runat="server" CssClass="textBox100" />
                            <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtVencimentoParcela" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <div style="display: inline-block; width: 33%">
                            <div class="label_form">
                                Valor nominal*
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtValoNominalParcela" SetFocusOnError="true"
                                    CssClass="RequireFieldValidator" ErrorMessage="- Valor nominal" ValidationGroup="rfvParcela">*obrigatório!</asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox ID="txtValoNominalParcela" runat="server" CssClass="textBox100 mascara_dinheiro" />
                        </div>
                        <div style="display: inline-block; width: 33%">
                            <div class="label_form">
                                Desconto
                            </div>
                            <asp:TextBox ID="txtDescontoParcela" runat="server" CssClass="textBox100 mascara_dinheiro" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-top: 10px">
                <asp:Button ID="btnSalvarParcela" runat="server" Text="Salvar" ValidationGroup="rfvParcela" OnInit="btnSalvarParcela_Init" OnClick="btnSalvarParcela_Click" CssClass="botao salvar" />
                <a id="cancelar_parcela" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="popup_osmatriz" class="pop_up" style="width: 70%">
        <div class="barra">
            Vincular OS Matriz              
        </div>
        <div class="cph">
            <div>
                <asp:UpdatePanel ID="upOsMatriz" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="display: inline-block; width: 50%">
                            <div class="label_form">
                                Pedido
                            </div>
                            <asp:DropDownList ID="ddlPedidoOSMatrizOS" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPedidoOSMatrizOS_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div style="display: inline-block; width: 49%">
                            <div class="label_form">
                                OS matriz*<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" InitialValue="0" ControlToValidate="ddlOSMatrizOS" CssClass="RequireFieldValidator" SetFocusOnError="true"
                                    ErrorMessage="- OS Matriz" ValidationGroup="rfvOSMatriz">*obrigatório!</asp:RequiredFieldValidator>
                            </div>
                            <asp:DropDownList ID="ddlOSMatrizOS" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlPedidoOSMatrizOS" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-top: 10px">
                <asp:Button ID="btnVincularOsMatriz" runat="server" Text="Vincular" ValidationGroup="rfvOSMatriz" OnInit="btnVincularOsMatriz_Init" OnClick="btnVincularOsMatriz_Click" CssClass="botao salvar" />
                <a id="btnCancelarOSMatriz" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>
</asp:Content>

