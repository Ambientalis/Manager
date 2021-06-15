<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroPedidos2.aspx.cs" Inherits="Pedidos_CadastroPedidos2" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= btnNovoCadastroOS.ClientID %>").click(function () {
                MudarClassesTabs();
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
    <style type="text/css">
        .lista_arquivo .list-group-item {
            padding: 5px 15px;
        }

            .lista_arquivo .list-group-item:hover {
                background-color: #dcdada;
            }

        .opcoes_arquivo {
            position: absolute;
            right: 5px;
            top: 50%;
            transform: translateY(-50%);
        }

        .lista_arquivo .list-group-item:hover .opcoes_arquivo {
            display: block;
        }


        .lista_arquivo .list-group-item .opcoes_arquivo {
            display: none;
        }

        .lista_arquivo hr {
            margin: 0px;
        }
    </style>
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
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-5">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Dados básicos
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12" style="text-align: center">
                                            <asp:CheckBox ID="chkPedidoAtivo" runat="server" Text="Ativo" Checked="True" />&nbsp;
                                            <asp:CheckBox ID="chkContratoFixo" runat="server" Text="Contrato fixo" Checked="True" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>
                                                Código*
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="tbxCodigoPedido" CssClass="RequireFieldValidator" ErrorMessage="- Código" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                            </label>
                                            <asp:UpdatePanel runat="server" ID="upCodigo" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                <ContentTemplate>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="tbxCodigoPedido" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton runat="server" ID="btnGerarProximoCodigo" CssClass="btn btn-default" ToolTip="Gerar próximo código" OnClick="btnGerarProximoCodigo_Click" Style="padding-left: 25px">
                                                                <i class="fa fa-refresh"></i>&nbsp;Gerar
                                                            </asp:LinkButton>
                                                        </span>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGerarProximoCodigo" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>
                                        <div class="col-md-6">
                                            <label>
                                                Data*
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxDataPedido" CssClass="RequireFieldValidator" ErrorMessage="- Data" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                            </label>
                                            <asp:TextBox ID="tbxDataPedido" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbxDataPedido" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>
                                                Tipo*
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoPedido" InitialValue="0" CssClass="RequireFieldValidator"
                                                       ErrorMessage="- Tipo" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                            </label>
                                            <asp:DropDownList ID="ddlTipoPedido" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>
                                                Cliente*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClientePedido" InitialValue="0" CssClass="RequireFieldValidator"
                                                    ErrorMessage="- Cliente" ValidationGroup="vlgPedido">*obrigatório!</asp:RequiredFieldValidator>
                                            </label>
                                            <asp:DropDownList ID="ddlClientePedido" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>Vendedor</label>
                                            <asp:DropDownList ID="ddlVendedor" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>Contrato</label>
                                            <asp:UpdatePanel ID="upStatusContratoPedido" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-control">
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
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Detalhamento
                                    <asp:LinkButton ID="btnEditarDetalhamento" class="btn btn-warning" runat="server" ToolTip="Alterar Detalhamento" OnClick="btnEditarDetalhamento_Click">
                                        <i class="fa fa-edit"></i>&nbsp;Editar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnVerHistorioDetalhamentosPedidos" class="btn btn-default" runat="server" ToolTip="Histórico de Alterações do Detalhamento" OnClick="btnVerHistorioDetalhamentosPedidos_Click" OnInit="btnVerHistorioDetalhamentosPedidos_Init">
                                        <i class="fa fa-history"></i>&nbsp;Histórico
                                    </asp:LinkButton>
                                </div>
                                <div class="panel-body">
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
                        <div class="col-md-7">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Ordens de serviço
                                </div>
                                <div class="panel-body">
                                    <div style="max-height: 400px; overflow: auto">
                                        <asp:GridView ID="gdvOrdensServico" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvOrdensServico_PageIndexChanging" OnPreRender="gdvOrdensServico_PreRender" OnRowDeleting="gdvOrdensServico_RowDeleting">
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
                                        <asp:LinkButton ID="btnNovaOS" runat="server" CssClass="btn btn-default" OnInit="btnNovaOS_Init" OnClick="btnNovaOS_Click">
                                            <i class="fa fa-plus"></i>&nbsp;Nova OS
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Arquivos&nbsp;  
                                    <asp:LinkButton ID="btnNovoArquivoPedido" runat="server" CssClass="btn btn-default" OnClick="btnNovoArquivoPedido_Click">
                                            <i class="fa fa-plus"></i>&nbsp;Anexar arquivo/contrato
                                    </asp:LinkButton>
                                </div>
                                <div class="panel-body">
                                    <div class="lista_arquivo">
                                        <h4 style="margin-bottom: 5px">Pedido</h4>
                                        <ul class="list-group">
                                            <li class="list-group-item">
                                                <a href="#">Arquivo 1</a>
                                                <div class="opcoes_arquivo">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger btn-xs">
                                                        <i class="fa fa-trash-o"></i>&nbsp;Excluir
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-warning btn-xs">
                                                        <i class="fa fa-edit"></i>&nbsp;Renomear
                                                    </asp:LinkButton>
                                                </div>
                                            </li>
                                        </ul>
                                        <hr style="margin: 0px" />
                                        <h4 style="margin-bottom: 5px">OS 6343</h4>
                                        <ul class="list-group">
                                            <li class="list-group-item" style="">
                                                <a href="#">Arquivo 2</a>
                                                <div class="opcoes_arquivo">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-danger btn-xs">
                                                        <i class="fa fa-trash-o"></i>&nbsp;Excluir
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-warning btn-xs">
                                                        <i class="fa fa-edit"></i>&nbsp;Renomear
                                                    </asp:LinkButton>
                                                </div>
                                            </li>
                                        </ul>
                                        <hr style="margin: 0px" />
                                        <h4 style="margin-bottom: 5px">OS 6344</h4>
                                        <ul class="list-group">
                                        </ul>
                                        <hr style="margin: 0px" />
                                        <h4 style="margin-bottom: 5px">OS 6344</h4>
                                        <ul class="list-group">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="upFinanceiroPedido" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Financeiro do pedido
                                        </div>
                                        <div class="panel-body">
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
                                                                    CssClass="RequireFieldValidator" ErrorMessage="- Valor total do pedido" ValidationGroup="rfvFinanceiro">*obrigatório!</asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:TextBox ID="txtValorTotalPedido" runat="server" CssClass="textBox100 mascara_dinheiro" />
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <div class="label_form">
                                                                    Dividir em*
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtDividirEm" SetFocusOnError="true"
                                                                    CssClass="RequireFieldValidator" ErrorMessage="- Dividir em" ValidationGroup="rfvFinanceiro" InitialValue="0">*obrigatório!</asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:TextBox ID="txtDividirEm" runat="server" CssClass="textBox100 mascara_inteiro" />
                                                            </td>
                                                            <td style="width: 41%;">
                                                                <div class="label_form">
                                                                    Primeiro pagamento*
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtPrimeiroPagamento" SetFocusOnError="true"
                                                                    CssClass="RequireFieldValidator" ErrorMessage="- Primeiro pagamento" ValidationGroup="rfvFinanceiro">*obrigatório!</asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:TextBox ID="txtPrimeiroPagamento" runat="server" CssClass="textBox100" />
                                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtPrimeiroPagamento" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
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
                                                            <td>
                                                                <div class="label_form">
                                                                    Caixa das parcelas
                                                                </div>
                                                                <asp:DropDownList ID="ddlCaixaGerarFinanceiro" runat="server" CssClass="dropDownList100"></asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <div class="label_form">
                                                                    &nbsp;
                                                                </div>
                                                                <asp:Button ID="btnGerarFinanceiro" runat="server" Text="Gerar financeiro" ValidationGroup="rfvFinanceiro" CssClass="botao ok" OnClick="btnGerarFinanceiro_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:View>
                                                <asp:View ID="View3" runat="server">
                                                    <div>
                                                        <div style="display: inline-block; width: 49%">
                                                            <div class="label_form">
                                                                Valor total do pedido
                                                            </div>
                                                            <asp:TextBox ID="txtValorPedidoFinanceiro" Enabled="false" runat="server" CssClass="textBox100" />
                                                        </div>
                                                        <div style="display: inline-block; width: 50%">
                                                            <div class="label_form">
                                                                Orçamento associado
                                                            </div>
                                                            <asp:TextBox ID="txtOrcamentoAssociadoPedido" Enabled="false" runat="server" CssClass="textBox100" />
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
                                                                        <asp:BoundField DataField="GetDescricaoCaixa" HeaderText="Caixa" />
                                                                        <asp:BoundField DataField="GetDescricaoStatus" HeaderText="Status" />
                                                                        <asp:TemplateField HeaderText="Valor nominal">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ValorNominal","{0:c}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <b>
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# BindingValorNominalTotalParcelas() %>'></asp:Label></b>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Desconto">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Descontos","{0:c}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <b>
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# BindingDescontoTotalParcelas() %>'></asp:Label></b>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Valor total">
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("ValorTotal","{0:c}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <b>
                                                                                    <asp:Label ID="Label6" runat="server" ForeColor='<%# BindingCorValorTotalParcelas() %>' Text='<%# BindingValorTotalParcelas() %>'></asp:Label></b>
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
                                                            <asp:LinkButton ID="btnNovaParcela" runat="server" CssClass="btn btn-default" OnClick="btnNovaParcela_Click">
                                                                <i class="fa fa-plus"></i>&nbsp;Nova parcela
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div style="float: right">
                                                            <span class="label label-danger">Total diferente do valor total do pedido</span>
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
                    </div>
                </div>
                <asp:HiddenField ID="hfId" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSalvarPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNovoPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluirPedido" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gdvOrdensServico" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvOrdensServico" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="text-align: right; margin-top: 10px;">
        <asp:UpdatePanel ID="upOpcaoOrcamento" runat="server" ChildrenAsTriggers="True" UpdateMode="Conditional">
            <ContentTemplate>
                <a runat="server" id="linkOrcamento" href="#" target="_blank" class="botao visualizar">Visualizar orçamento associado</a>
                <asp:LinkButton ID="btnNovoPedido" runat="server" CssClass="btn btn-default" OnClick="btnNovoPedido_Click">
                    <i class="fa fa-plus"></i>&nbsp;Novo
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalvarPedido" runat="server" CssClass="btn btn-success" ValidationGroup="vlgPedido" OnClick="btnSalvarPedido_Click">
                    <i class="fa fa-save"></i>&nbsp;Salvar
                </asp:LinkButton>
                <asp:LinkButton ID="btnExcluirPedido" runat="server" CssClass="btn btn-danger" OnClick="btnExcluirPedido_Click" OnPreRender="btnExcluirPedido_PreRender">
                    <i class="fa fa-trash-o"></i>&nbsp;Excluir
                </asp:LinkButton>
                <asp:LinkButton ID="btnEnviarEmailPedido" runat="server" ToolTip="Reenvia o e-mail de confirmação de cadastro de pedido para o cliente" CssClass="btn btn-default" OnClick="btnEnviarEmailPedido_Click">
                    <i class="fa fa-send"></i>&nbsp;Reenviar confirmação
                </asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="Popup_OS" class="panel panel-default" style="width: 80%">
        <div class="panel panel-heading">
            Cadastro de OS
        </div>
        <div class="panel panel-body">
            <asp:UpdatePanel ID="upExibicoesDaOS" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-xs-6">
                            <div class="panel panel-default">
                                <div class="panel-heading">Dados</div>
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="upFormOS" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="label_form">&nbsp;</div>
                                                    <asp:CheckBox ID="ckbAtivoOS" runat="server" Text="Ativa" Checked="True" />
                                                    <asp:HiddenField ID="hdIdOS" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="label_form">Código</div>
                                                    <asp:TextBox ID="tbxCodigoOS" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="label_form">
                                                        Data<span>*:</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="tbxDataOS" CssClass="RequireFieldValidator"
                                                            SetFocusOnError="true" ErrorMessage="- Data" ValidationGroup="vlgOS">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <asp:TextBox ID="tbxDataOS" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbxDataOS" Format="dd/MM/yyyy">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="label_form">
                                                        OS Matriz
                                                    </div>
                                                    <asp:UpdatePanel runat="server" ID="upOsVinculada" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:HiddenField runat="server" ID="hfIdOsMatriz" />
                                                            <div class="input-group">
                                                                <asp:TextBox runat="server" ID="txtOsMatriz" CssClass="form-control" Enabled="false" Text="Nenhuma OS vinculada" />
                                                                <span class="input-group-btn">
                                                                    <asp:LinkButton runat="server" ID="btnEscolherOSMatriz" CssClass="btn btn-default" OnInit="btnEscolherOSMatriz_Init" OnClick="btnEscolherOSMatriz_Click">
                                                                    <i class="fa fa-search"></i>&nbsp;Escolher
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnRemoverOsMatriz" CssClass="btn btn-danger" OnClick="btnRemoverOsMatriz_Click">
                                                                    <i class="fa fa-remove"></i>&nbsp;Remover
                                                                    </asp:LinkButton>
                                                                </span>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnRemoverOsMatriz" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
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
                                                <div style="display: inline-block; width: 50%;">
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
                                                                    <div class="campo_form" style="padding-right: 3%; margin-top: 5px;">
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
                                                                    <div class="campo_form" style="margin-top: 5px;">
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
                                                                    <div class="campo_form" style="padding-right: 3%; margin-top: 5px;">
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
                                                    <td colspan="4" style="padding-right: 15px;"></td>
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
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-6">
                            <div class="panel panel-default">
                                <div class="panel-heading">Financeiro</div>
                                <div class="panel-body">
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
                                                <div style="display: inline-block; width: 25%;">
                                                    <div class="label_form">
                                                        Valor total
                                                    </div>
                                                    <asp:TextBox ID="txtValorTotalOS" Enabled="false" CssClass="textBox100 valor_total" runat="server"></asp:TextBox>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">Visitas</div>
                                <div class="panel-body">
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
                            <div class="panel panel-default">
                                <div class="panel-heading">Atividades</div>
                                <div class="panel-body">
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
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Arquivos&nbsp;
                                    <asp:Button ID="btnNovoArquivoOS" runat="server" Text="Anexar Arquivo" CssClass="botao novo" OnClick="btnNovoArquivoOS_Click" />&nbsp;
                                    <asp:Button ID="btnExcluirArquivoOS" runat="server" Text="Excluir Arquivo" CssClass="botao excluir" OnClick="btnExcluirArquivoOS_Click" OnInit="btnExcluirArquivoOS_Init" />&nbsp;
                                    <asp:Button ID="btnRenomearArquivoOS" runat="server" Text="Renomear Arquivo" CssClass="botao editar" OnClick="btnRenomearArquivoOS_Click" OnInit="btnRenomearArquivoOS_Init" />
                                </div>
                                <div class="panel-body">
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
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnNovoCadastroOS" runat="server" Text="Nova OS" CssClass="btn btn-default" OnClick="btnNovoCadastroOS_Click" />
            <asp:Button ID="btnSalvarOS" runat="server" Text="Salvar e Enviar OS" CssClass="btn btn-success" ValidationGroup="vlgOS" OnClick="btnSalvarOS_Click" OnInit="btnSalvarOS_Init" />
            <a id="cancelar_cad_os" class="btn btn-danger" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
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
                        <div style="display: inline-block; width: 25%">
                            <div class="label_form">
                                Vencimento*
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtVencimentoParcela" SetFocusOnError="true"
                                    CssClass="RequireFieldValidator" ErrorMessage="- Vencimento" ValidationGroup="rfvParcela">*obrigatório!</asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox ID="txtVencimentoParcela" runat="server" CssClass="textBox100" />
                            <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtVencimentoParcela" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <div style="display: inline-block; width: 24%">
                            <div class="label_form">
                                Valor nominal*
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtValoNominalParcela" SetFocusOnError="true"
                                    CssClass="RequireFieldValidator" ErrorMessage="- Valor nominal" ValidationGroup="rfvParcela">*obrigatório!</asp:RequiredFieldValidator>
                            </div>
                            <asp:TextBox ID="txtValoNominalParcela" runat="server" CssClass="textBox100 mascara_dinheiro" />
                        </div>
                        <div style="display: inline-block; width: 24%">
                            <div class="label_form">
                                Desconto
                            </div>
                            <asp:TextBox ID="txtDescontoParcela" runat="server" CssClass="textBox100 mascara_dinheiro" />
                        </div>
                        <div style="display: inline-block; width: 25%">
                            <div class="label_form">
                                Caixa
                            </div>
                            <asp:DropDownList ID="ddlCaixaParcela" runat="server" CssClass="dropDownList100"></asp:DropDownList>
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
