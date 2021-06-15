<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroCliente.aspx.cs" Inherits="Cliente_CadastroCliente" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/mask.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            CriarEventos();
            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("clientes")) {
                    $(this).removeClass("clientes");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".clientes").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".clientes").children("a").children("span").addClass("bg_branco");
                }
            });
            marcarRating();
        });

        function changeRating(rate) {
            $('#<%= hfRating.ClientID %>').val(rate);
        }

        function marcarRating() {
            $('.rating input').removeAttr('checked');
            var valor = eval($('#<%= hfRating.ClientID %>').val());
            if (valor && valor > 0) {
                $('.rating input[type="radio"]').each(function (index) {
                    if ((5 - index) == valor) {
                        $(this).attr('checked', 'checked');
                    }
                });
            }
        }

        function esconderBotaoEmail(checado) {
            if (checado == 'true') {
                $("#<%= btnEnviarLoginSenha.ClientID %>").show();
            } else {
                $("#<%= btnEnviarLoginSenha.ClientID %>").hide();
            }
        }

        function CriarEventos() {
            if ($("#<%= rbtnTipoPessoa.ClientID %>_0").is(":checked") == true) {
                $("#pessoa_juridica").hide();
                $("#pessoa_fisica").show();
            } else {

                $("#pessoa_juridica").show();
                $("#pessoa_fisica").hide();
            }
            $("#<%= rbtnTipoPessoa.ClientID %>_0").click(function () {
                verificarTipo();
            });
            $("#<%= rbtnTipoPessoa.ClientID %>_1").click(function () {
                verificarTipo();
            });

            $("#<%= chkIsentoPessoaJuridica.ClientID %>").change(function () {
                IsentoChangeJuridica();
            });
            $("#<%= chkIsentoPessoaFisica.ClientID %>").change(function () {
                IsentoChangeFisica();
            });

            $("#<%=tbxCNPJPessoaJuridica.ClientID %>").unbind();
            $('#<%=tbxCPFPessoaFisica.ClientID %>').unbind();

            ///Máscaras
            $("#<%=tbxCNPJPessoaJuridica.ClientID %>").mask("99.999.999/9999-99");
            $('#<%=tbxCPFPessoaFisica.ClientID %>').mask("999.999.999-99");


            IsentoChangeFisica();
            IsentoChangeJuridica();
            CEP();
        }

        function CEP() {
            $('#<%=tbxCepEndereco.ClientID %>').unbind();
            $('#<%=tbxCepEndereco.ClientID %>').mask("99.999-999");
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

        function IsentoChangeJuridica() {
            if ($("#<%= chkIsentoPessoaJuridica.ClientID %>").is(":checked")) {
                $("#<%= tbxInscricaoEstadualPessoaJuridica.ClientID %>").val("");
                $("#<%= tbxInscricaoEstadualPessoaJuridica.ClientID %>").attr("disabled", true);
            }
            else
                $("#<%= tbxInscricaoEstadualPessoaJuridica.ClientID %>").attr("disabled", false);
        }

        function IsentoChangeFisica() {
            if ($("#<%= chkIsentoPessoaFisica.ClientID %>").is(":checked")) {
                $("#<%= tbxInscricaoEstadualPessoaFisica.ClientID %>").val("");
                $("#<%= tbxInscricaoEstadualPessoaFisica.ClientID %>").attr("disabled", true);
            }
            else
                $("#<%= tbxInscricaoEstadualPessoaFisica.ClientID %>").attr("disabled", false);
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

        function verificarTipo() {
            if ($("#<%= rbtnTipoPessoa.ClientID %>_0").is(":checked") == true) {

                $("#pessoa_juridica").hide();
                $("#pessoa_fisica").show();
            }
            if ($("#<%= rbtnTipoPessoa.ClientID %>_1").is(":checked") == true) {

                $("#pessoa_juridica").show();
                $("#pessoa_fisica").hide();
            }
        }

    </script>

    <style type="text/css">
        .campo_form {
            padding-right: 5%;
        }

        fieldset, label {
            margin: 0;
            padding: 0;
        }

        h1 {
            font-size: 1.5em;
            margin: 10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        <asp:Label runat="server" ID="lblTituloTela">Cadastro de cliente</asp:Label>
        <asp:HiddenField runat="server" ID="hfTipoCarregado" Value="1" />
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:Label ID="lblAlteracaoSenha" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="btnAlterarSenha_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelAtlSenha"
        Enabled="True" PopupControlID="Popup_AlteracaoSenha" TargetControlID="lblAlteracaoSenha">
    </asp:ModalPopupExtender>
    <asp:Label ID="lkbEditarImagemJuridica" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lkbEditarImagemJuridica_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="lkbEditarImagemJuridica"
        PopupControlID="popImagem" CancelControlID="cancelarImagem" BackgroundCssClass="simplemodal">
    </asp:ModalPopupExtender>

    <asp:HiddenField runat="server" ID="hfPopupHistoricoClassificacoes" />
    <asp:ModalPopupExtender ID="PopupHistoricoClassificacoes_ModalPopupExtender" runat="server" Enabled="True" TargetControlID="hfPopupHistoricoClassificacoes"
        PopupControlID="popup_historico_classificacoes" CancelControlID="btn_fechar_historico_classificacoes" BackgroundCssClass="simplemodal">
    </asp:ModalPopupExtender>

    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioCliente" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { CriarEventos(); });
                </script>
                <div style="float: left; width: 50%; margin-right: 10px;">
                    <div class="barra">
                        Dados Básicos
                    </div>
                    <div class="cph">
                        <div>
                            <div>
                                <div class="campo_form">
                                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" Checked="True" />
                                </div>
                            </div>
                            <div style="float: left; width: 32%; margin-right: 1%;">
                                <div class="label_form">
                                    Código
                                </div>
                                <div class="campo_form" style="padding-right: 5%;">
                                    <asp:TextBox ID="tbxCodigoCliente" CssClass="textBox100" runat="server" Text="Gerado automáticamente" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 32%; margin-right: 1%;">
                                <div class="label_form">
                                    Unidade                                        
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="txtUnidade" CssClass="textBox100" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 33%; margin-right: 1%;">
                                <div class="label_form">
                                    Origem
                                </div>
                                <div class="campo_form" style="padding-right: 5%;">
                                    <asp:DropDownList runat="server" ID="ddlOrigem" CssClass="dropDownList100"></asp:DropDownList>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                        </div>
                        <div style="margin-top: 10px;">
                            <div class="label_form">
                                &nbsp;
                            </div>
                            <asp:RadioButtonList CssClass="radioBotton" ID="rbtnTipoPessoa" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="true" Value="F">Pessoa Física</asp:ListItem>
                                <asp:ListItem Value="J">Pessoa Jurídica</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div id="pessoa_fisica" style="display: block;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" dir="ltr" class="style2"></td>
                                    <td rowspan="4" valign="top" style="width: 170px;">
                                        <div class="logo_cliente">
                                            <asp:Image ID="imgPessoaFisica" runat="server" CssClass="logo_img" Height="90%" ImageUrl="~/Utilitarios/Imagens/FotosIndisponiveis/FotoIndisponivelAlbum.png"
                                                Width="90%" />

                                            <asp:Button ID="btnEditarImagemFisica" class="botao editar_mini" runat="server" OnClick="btnEditarImagemFisica_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="lkbExcluirImagem" class="botao excluir_mini" runat="server" OnClick="lkbExcluirImagem_Click" OnPreRender="lkbExcluirImagem_PreRender" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Nome<span style="color: Red;">*</span>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomePessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Apelido
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxApelidoPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            CPF<span style="color: Red;">*</span>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxCPFPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Rg
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxRgPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Órgão Emissor
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxOrgaoEmissorPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Nacionalidade
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNacionalidadePessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Data de Nascimento
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxDataNascimentoPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxDataNascimentoPessoaFisica" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Estado civil
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEstadoCivilPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Sexo
                                        </div>
                                        <div class="campo_form">
                                            <asp:DropDownList ID="ddlSexoPessoaFisica" CssClass="dropDownList100" runat="server">
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <div class="label_form">
                                            Inscrição Estadual
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxInscricaoEstadualPessoaFisica" CssClass="textBox50" runat="server"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:CheckBox ID="chkIsentoPessoaFisica" runat="server" Text="&nbsp;Isento" />
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Inscrição Municipal&nbsp;&nbsp;&nbsp;
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxInscricaoMunicipalPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2" colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Telefone 1<span style="color: Red;">*</span>
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 3%;">
                                                        <asp:TextBox ID="tbxTelefone1PessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Telefone 2
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 5%;">
                                                        <asp:TextBox ID="tbxTelefone2PessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 60%;">
                                                                <div class="label_form">
                                                                    E-mail<span style="color: Red;">*</span>
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 3%;">
                                                                    <asp:TextBox ID="tbxEmailPessoaFisica" CssClass="textBox100" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 40%;">
                                                                <div class="campo_form">
                                                                    <asp:CheckBox ID="ckRecebeNotificacaoPessoaFisica" runat="server" Text="Recebe Notificações de Visita" Checked="True" />
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 3%;">
                                                                    <asp:CheckBox ID="ckRecebeLoginSenhaPessoaFisica" runat="server" Text="Recebe Login e Senha" Checked="True" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="pessoa_juridica" style="display: block;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td dir="ltr" class="label_form"></td>
                                    <td rowspan="5" valign="top" style="width: 170px;">
                                        <div class="logo_cliente" style="position: relative;">
                                            <asp:Image ID="imgPessoaJuridica" CssClass="logo_img" runat="server" Height="90%"
                                                Width="90%" ImageUrl="~/Utilitarios/Imagens/FotosIndisponiveis/FotoIndisponivelAlbum.png" /><br />
                                            <asp:Button ID="btnEditarImagemClienteJuridico" class="botao editar_mini" runat="server"
                                                ToolTip="Adicionar/Editar Imagem" OnClick="btnEditarImagemClienteJuridico_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="lkbExcluirImagemJuridica" class="botao excluir_mini" runat="server" OnClick="lkbExcluirImagem_Click" OnPreRender="lkbExcluirImagemJuridica_PreRender" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            razão social<span style="color: Red;">*</span>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxRazaoSocialPessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            nome fantasia
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeFantasiaPessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            cnpj<span style="color: Red;">*</span>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxCNPJPessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <div class="label_form">
                                            Inscrição Estadual
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxInscricaoEstadualPessoaJuridica" CssClass="textBox50" runat="server"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:CheckBox ID="chkIsentoPessoaJuridica" runat="server" Text="&nbsp;Isento" />
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Inscrição Municipal&nbsp;&nbsp;&nbsp;
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxInscricaoMunicipalPessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2" colspan="3">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Telefone 1<span style="color: Red;">*</span>
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 3%;">
                                                        <asp:TextBox ID="tbxTelefone1PessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 50%;">
                                                    <div class="label_form">
                                                        Telefone 2
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 5%;">
                                                        <asp:TextBox ID="tbxTelefone2PessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 60%;">
                                                                <div class="label_form">
                                                                    E-mail<span style="color: Red;">*</span>
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 3%;">
                                                                    <asp:TextBox ID="tbxEmailPessoaJuridica" CssClass="textBox100" runat="server"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 40%;">
                                                                <div class="campo_form">
                                                                    <asp:CheckBox ID="ckRecebeNotificacaoPessoaJuridica" runat="server" Text="Recebe Notificações de Visita" Checked="True" />
                                                                </div>
                                                                <div class="campo_form" style="padding-right: 3%;">
                                                                    <asp:CheckBox ID="ckRecebeLoginSenhaPessoaJuridica" runat="server" Text="Recebe Login e Senha" Checked="True" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Classificação
                                        </div>
                                        <div class="campo_form">

                                            <fieldset class="rating">
                                                <asp:HiddenField runat="server" ID="hfRating" Value="0" />
                                                <input type="radio" id="star5" name="rating" value="5" onclick="changeRating(5)" /><label class="full" for="star5"></label>
                                                <input type="radio" id="star4" name="rating" value="4" onclick="changeRating(4)" /><label class="full" for="star4"></label>
                                                <input type="radio" id="star3" name="rating" value="3" onclick="changeRating(3)" /><label class="full" for="star3"></label>
                                                <input type="radio" id="star2" name="rating" value="2" onclick="changeRating(2)" /><label class="full" for="star2"></label>
                                                <input type="radio" id="star1" name="rating" value="1" onclick="changeRating(1)" /><label class="full" for="star1"></label>
                                            </fieldset>

                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Última classificação
                                        </div>
                                        <div class="campo_form">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDataClassificacao" CssClass="textBox100" Enabled="false" runat="server"></asp:TextBox>
                                                    <script type="text/javascript">
                                                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { marcarRating(); });
                                                    </script>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnReclassificarCliente" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            &nbsp;
                                        </div>
                                        <div class="campo_form">
                                            <asp:Button ID="btnReclassificarCliente" runat="server" CssClass="botao salvar" Text="Reclassificar" ToolTip="Salvar a classificação na data atual" OnClick="btnReclassificarCliente_Click" />&nbsp;
                                                    <asp:Button ID="btnVerHistoricoClassificacoes" runat="server" CssClass="botao visualizar" ToolTip="Visualizar o histórico de classificações"
                                                        OnClick="btnVerHistoricoClassificacoes_Click" OnInit="btnVerHistoricoClassificacoes_Init" Text="Histórico" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <div class="label_form">
                                Observações / referências
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="txtObservacoes" CssClass="textArea" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div style="margin-top: 10px;">
                            <div class="label_form" style="font-size: 8pt;">
                                &nbsp;<span style="color: Red;">*</span>Campos obrigatórios
                            </div>
                        </div>
                    </div>
                </div>
                <div style="float: right; width: 49%;">
                    <div class="barra">
                        Endereço
                                <div class="close" onclick="minimizar('endereco_container');">
                                </div>
                    </div>
                    <div id="endereco_container" class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Logradouro
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxLogradouroEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Número
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNumeroEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Bairro
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxBairroEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Complemento
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxComplementoEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Referência
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxReferenciaEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            CEP
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxCepEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Estado
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList ID="ddlEstadoEndereco" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstadoEndereco_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Cidade
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:UpdatePanel ID="upCidadesEndereco" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCidadeEndereco" CssClass="dropDownList100" runat="server">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlEstadoEndereco" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="barra" style="margin-top: 10px;">
                        Contatos
                                <div class="close" onclick="minimizar('contatos_container');">
                                </div>
                    </div>
                    <div id="contatos_container" class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 30%">
                                        <div class="label_form">
                                            Nome
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeContato1" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 30%">
                                        <div class="label_form">
                                            Função
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxFuncaoContato1" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Telefone 1
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxPrimeiroTelefoneContato1" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Telefone 2
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxSegundoTelefoneContato1" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            E-mail
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEmailContato1" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Nascimento
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="txtNascimentoContato1" CssClass="textBox100" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtNascimentoContato1" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding-top: 10px">
                                        <asp:CheckBox ID="ckContato1RecebeNotificacoes" runat="server" Text="Recebe Notificações de Visita" Checked="True" />&nbsp;
                                        <asp:CheckBox ID="ckContato1RecebeLoginSenha" runat="server" Text="Recebe Login e Senha" Checked="True" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="border-bottom: 5px solid silver; margin-top: 10px; margin-bottom: 10px;"></div>
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 30%">
                                        <div class="label_form">
                                            Nome
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeContato2" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 30%">
                                        <div class="label_form">
                                            Função
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxFuncaoContato2" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Telefone 1
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxPrimeiroTelefoneContato2" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Telefone 2
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxSegundoTelefoneContato2" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            E-mail
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEmailContato2" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Nascimento
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="txtNascimentoContato2" CssClass="textBox100" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtNascimentoContato2" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding-top: 10px">
                                        <asp:CheckBox ID="ckContato2RecebeNotificacoes" runat="server" Text="Recebe Notificações de Visita" Checked="True" />&nbsp;
                                        <asp:CheckBox ID="ckContato2RecebeLoginSenha" runat="server" Text="Recebe Login e Senha" Checked="True" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="border-bottom: 5px solid silver; margin-top: 10px; margin-bottom: 10px;"></div>
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 30%">
                                        <div class="label_form">
                                            Nome
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNomeContato3" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 30%">
                                        <div class="label_form">
                                            Função
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxFuncaoContato3" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Telefone 1
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxPrimeiroTelefoneContato3" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 20%">
                                        <div class="label_form">
                                            Telefone 2
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxSegundoTelefoneContato3" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            E-mail
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEmailContato3" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Nascimento
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="txtNascimentoContato3" CssClass="textBox100" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtNascimentoContato3" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding-top: 10px">
                                        <asp:CheckBox ID="ckContato3RecebeNotificacoes" runat="server" Text="Recebe Notificações de Visita" Checked="True" />&nbsp;
                                        <asp:CheckBox ID="ckContato3RecebeLoginSenha" runat="server" Text="Recebe Login e Senha" Checked="True" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlUsuarioParaAcompanhamento">
                        <div class="barra" style="margin-top: 10px;">
                            Usuário para acompanhamento
                                <div class="close" onclick="minimizar('usuario_container');">
                                </div>
                        </div>
                        <div id="usuario_container" class="cph">
                            <div>
                                <div id="usuario_novo" runat="server">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 33%">
                                                <div class="label_form">
                                                    Login
                                                </div>
                                                <div class="campo_form" style="padding-right: 5%;">
                                                    <asp:TextBox ID="tbxLogin" CssClass="textBox100" runat="server" Text="Gerado Automaticamente..." ReadOnly="True" Enabled="false"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td style="width: 33%">
                                                <div class="label_form">
                                                    Senha
                                                </div>
                                                <div class="campo_form" style="padding-right: 5%;">
                                                    <asp:TextBox ID="tbxSenha" CssClass="textBox100" runat="server" Text="Gerado Automaticamente..." ReadOnly="True" Enabled="false"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td style="width: 33%"></td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="usuario_alteracao" runat="server" visible="false">
                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 33%">
                                                <div class="label_form">
                                                    Login
                                                </div>
                                                <div class="campo_form" style="padding-right: 5%;">
                                                    <asp:TextBox ID="tbxLoginEdicao" CssClass="textBox100" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td style="width: 33%">
                                                <div class="label_form">
                                                    Senha
                                                </div>
                                                <div class="campo_form" style="padding-right: 5%;">
                                                    <asp:Button ID="btnAlterarSenha" runat="server" CssClass="botao editar" OnClick="btnAlterarSenha_Click" Text="Alterar Senha" />
                                                </div>
                                            </td>
                                            <td style="width: 33%">&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfSenhaEdicao" runat="server" />
                <div style="clear: both"></div>
                <div style="text-align: right">
                    <div style="text-align: right; margin-top: 10px;">
                        <asp:Button ID="btnEnviarLoginSenha" runat="server" Text="Enviar Login e Senha" CssClass="botao email" OnClick="btnEnviarLoginSenha_Click" />&nbsp;
                        <asp:Button ID="btnNovoCadastro" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoCadastro_Click" />&nbsp;
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvar_Click" ValidationGroup="rfvCliente" />&nbsp;
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluir_Click" OnPreRender="btnExcluir_PreRender" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNovoCadastro" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEnviarLoginSenha" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="popImagem" class="pop_up" style="width: 550px;">
        <div class="barra">
            Upload de Imagem
        </div>
        <div class="pop_m20">
            <div style="position: relative;">
                <cc1:UpLoad ID="UpLoad2" runat="server" OnUpLoadComplete="UpLoad2_UpLoadComplete" Pagina="HandlerCliente.ashx" TamanhoMaximoArquivo="102400" TamanhoParteUpload="102400" OnInit="UpLoad2_Init">
                </cc1:UpLoad>
                <asp:Label ID="Label1" runat="server" OnInit="Label1_Init"></asp:Label>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <a id="cancelarImagem" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;" href="javascript:fecharPopUpUploadImagem()">Fechar</a>
            </div>
        </div>
    </div>

    <div id="Popup_AlteracaoSenha" class="pop_up" style="width: 400px">
        <div class="barra">
            Alteração de Senha
        </div>
        <div class="cph">
            <div>
                <asp:Panel ID="pnlSenha" runat="server" DefaultButton="btnSalvarSenha">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Nova Senha<span style="color: Red;">*</span><asp:RequiredFieldValidator ID="rfvSenha"
                                        runat="server" ControlToValidate="tbxNovaSenha" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Senha" ValidationGroup="vlgAltSenha">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form" style="padding-right: 5%;">
                                    <asp:TextBox ID="tbxNovaSenha" CssClass="textBox100" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form">
                                    Confirmação da Nova Senha<span style="color: Red;">*</span><asp:RequiredFieldValidator
                                        ID="rfvSenhaConfirm" runat="server" ControlToValidate="tbxNovaSenhaConfirm" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgAltSenha">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form" style="padding-right: 5%;">
                                    <asp:TextBox ID="tbxNovaSenhaConfirm" CssClass="textBox100" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div style="margin-top: 10px;">
                    <div class="label_form" style="font-size: 8pt;">
                        &nbsp;<span style="color: Red;">*</span>Campos Obrigatórios
                    </div>
                </div>
                <div style="text-align: right">
                    <asp:Button ID="btnSalvarSenha" runat="server" Text="Salvar" CssClass="botao salvar"
                        ValidationGroup="vlgAltSenha" OnClick="btnSalvarSenha_Click" OnInit="btnSalvarSenha_Init" />&nbsp;&nbsp;<a
                            id="cancelAtlSenha" class="botao vermelho">Cancelar</a>
                </div>
            </div>
        </div>
    </div>

    <div id="popup_historico_classificacoes" style="width: 50%">
        <div class="barra">
            Histórico de classificações do cliente/fornecedor
        </div>
        <div class="cph">
            <div style="max-height: 300px; overflow-y: auto; margin-bottom: 10px">
                <asp:UpdatePanel runat="server" ID="upHistoricoClassificacoes" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grvHistoricoClassificacoes" CssClass="grid" runat="server" AutoGenerateColumns="False"
                            EnableModelValidation="True" AllowPaging="False" DataKeyNames="Id" BorderStyle="None" BorderWidth="0px">
                            <Columns>
                                <asp:BoundField HeaderText="Data" DataField="Data" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                <asp:BoundField HeaderText="Classificação" DataField="Classificacao" />
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridRow" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right">
                <a id="btn_fechar_historico_classificacoes" class="botao vermelho">Fechar</a>
            </div>
        </div>
    </div>
</asp:Content>

