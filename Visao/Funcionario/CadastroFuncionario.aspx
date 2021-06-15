<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroFuncionario.aspx.cs" Inherits="Funcionario_CadastroFuncionario" %>

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
                if ($(this).hasClass("funcionarios")) {
                    $(this).removeClass("funcionarios");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".funcionarios").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".funcionarios").children("a").children("span").addClass("bg_branco");
                }
            });

        });


        function esconderBotaoEmail(checado) {
            if (checado == 'true') {
                $("#<%= btnEnviarLoginSenha.ClientID %>").show();
            } else {
                $("#<%= btnEnviarLoginSenha.ClientID %>").hide();
            }
        }

        function CriarEventos() {
            ///Máscaras     
            $('#<%=tbxCPF.ClientID %>').unbind();
            $('#<%=tbxCPF.ClientID %>').mask("999.999.999-99");
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Cadastro de Funcionário
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:Label ID="lkbEditarImagemJuridica" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lkbEditarImagemJuridica_ModalPopupExtender" runat="server" DynamicServicePath="" Enabled="True" TargetControlID="lkbEditarImagemJuridica"
        PopupControlID="popImagem" CancelControlID="cancelarImagem" BackgroundCssClass="simplemodal">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblAlteracaoSenha" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="btnAlterarSenha_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelAtlSenha"
        Enabled="True" PopupControlID="Popup_AlteracaoSenha" TargetControlID="lblAlteracaoSenha">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblCadastroDepartamento" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroDepartamento_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_cadastro_departamento"
        Enabled="True" PopupControlID="popCadastroDepartamento" TargetControlID="lblCadastroDepartamento">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblCadastroCargo" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroCargo_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_cadastro_cargo"
        Enabled="True" PopupControlID="popCadastroCargo" TargetControlID="lblCadastroCargo">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblCadastroFuncoes" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroFuncoes_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarFuncoes"
        Enabled="True" PopupControlID="Popup_Funcoes" TargetControlID="lblCadastroFuncoes">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblCadastroSetores" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroSetores_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_cadastro_setor"
        Enabled="True" PopupControlID="Pop_setores" TargetControlID="lblCadastroSetores">
    </asp:ModalPopupExtender>
    <asp:Label ID="lblEnviarEmailSenha" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblEnviarEmailSenha_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_enviar_email_func"
        Enabled="True" PopupControlID="Pop_enviar_email_senha" TargetControlID="lblEnviarEmailSenha">
    </asp:ModalPopupExtender>
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioFuncionario" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { CriarEventos(); });
                </script>
                <div style="float: left; width: 50%; margin-right: 10px;">
                    <div class="barra">
                        Dados Básicos
                    </div>
                    <div class="cph">

                        <div id="pessoa_fisica" style="display: block;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" dir="ltr" class="style2">
                                        <div>
                                            <div style="float: left; width: 15%; margin-right: 10px; vertical-align: bottom;">
                                                <div class="label_form">
                                                    &nbsp;
                                                </div>
                                                <div class="campo_form" style="vertical-align: bottom;">
                                                    <asp:CheckBox ID="chkAtivo" runat="server" Text="Ativo" Checked="True" />
                                                </div>
                                            </div>
                                            <div style="float: right; width: 80%; margin-right: 10px;">
                                                <div class="label_form">
                                                    Código
                                                </div>
                                                <div class="campo_form" style="padding-right: 5%;">
                                                    <asp:TextBox ID="tbxCodigoFuncionario" CssClass="textBox100" runat="server" Text="Gerado automáticamente" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>
                                    </td>
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
                                    <td colspan="2" dir="ltr" class="style2">
                                        <div class="label_form">
                                            Nome<span style="color: Red;">*</span>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNome" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Funções &nbsp;
                                                        <asp:Button ID="btnEditarFuncoes" class="botao editar_mini" runat="server" OnClick="btnEditarFuncoes_Click" OnInit="btnEditarFuncoes_Init" />
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <div style="overflow: auto; max-height: 100px;">
                                                <asp:CheckBoxList ID="cblFuncoes" runat="server"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Apelido
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxApelido" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            CPF<span style="color: Red;">*</span>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxCPF" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="ckbVendedor" runat="server" Text="Vendedor" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Rg
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxRg" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Órgão Emissor
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxOrgaoEmissor" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Nacionalidade
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxNacionalidade" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Data de Nascimento
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxDataNascimento" CssClass="textBox100" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxDataNascimento" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="label_form">
                                            Estado civil
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEstadoCivil" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="label_form">
                                            Sexo
                                        </div>
                                        <div class="campo_form">
                                            <asp:DropDownList ID="ddlSexo" CssClass="dropDownList100" runat="server">
                                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                <asp:ListItem Value="F">Feminino</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" style="width: 100%;">
                                        <div class="label_form">
                                            Observações / Referências
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxObservacoes" CssClass="textArea" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <br />
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
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Descrição
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxDescricaoEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            Logradouro
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxLogradouroEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 20%">
                                                    <div class="label_form">
                                                        Número
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 8%;">
                                                        <asp:TextBox ID="tbxNumeroEndereco" CssClass="textBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td style="width: 30%">
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
                                        </table>
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
                        Telefones
                                <div class="close" onclick="minimizar('contatos_container');">
                                </div>
                    </div>
                    <div id="contatos_container" class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 33%">
                                        <div class="label_form">
                                            Celular Corporativo
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxCelularCorporativo" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 33%">
                                        <div class="label_form">
                                            Celular Pessoal
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxCelularPessoal" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 33%">
                                        <div class="label_form">
                                            Telefone Residencial
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxTelefoneResidencial" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td colspan="2">
                                                    <div class="label_form" style="border-bottom: 1px solid black; margin-top: 15px; margin-bottom: 5px;">Contato de Emergência</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70%">
                                                    <div class="label_form">
                                                        Nome
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 5%;">
                                                        <asp:TextBox ID="tbxNomeContatoEmergencia" runat="server" CssClass="textBox100"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="label_form">
                                                        Telefone
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 3%;">
                                                        <asp:TextBox ID="tbxTelefoneContatoEmergencia" runat="server" CssClass="textBox100"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </div>

                    </div>
                    <div class="barra" style="margin-top: 10px;">
                        E-mails
                                <div class="close" onclick="minimizar('emails_container');">
                                </div>
                    </div>
                    <div id="emails_container" class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            E-mail Corporativo
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEmailCorporativo" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 50%">
                                        <div class="label_form">
                                            E-mail Pessoal
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:TextBox ID="tbxEmailPessoal" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="barra" style="margin-top: 10px;">
                        Usuário
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
                                                <asp:TextBox ID="tbxLogin" CssClass="textBox100" runat="server" Enabled="False" ReadOnly="True" Text="Gerado automaticamente..."></asp:TextBox>
                                            </div>
                                        </td>
                                        <td style="width: 33%">
                                            <div class="label_form">
                                                Senha
                                            </div>
                                            <div class="campo_form" style="padding-right: 5%;">
                                                <asp:TextBox ID="tbxSenha" CssClass="textBox100" runat="server" runat="server" Enabled="False" ReadOnly="True" Text="Gerado automaticamente..."></asp:TextBox>
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
                                                <asp:Button ID="btnAlterarSenha" runat="server" Text="Alterar Senha" CssClass="botao editar" OnClick="btnAlterarSenha_Click" />
                                            </div>
                                        </td>
                                        <td style="width: 33%"></td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfId" runat="server" />
                <asp:HiddenField ID="hfSenhaEdicao" runat="server" />
                <div style="clear: both"></div>
                <div style="text-align: right">
                    <div style="text-align: right; margin-top: 10px;">
                        <asp:Button ID="btnEnviarLoginSenha" runat="server" Text="Enviar Senha por E-mail" CssClass="botao email" OnClick="btnEnviarLoginSenha_Click" />&nbsp; 
                    <asp:Button ID="btnNovoCadastro" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoCadastro_Click" />&nbsp;      
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvar_Click" OnInit="btnSalvar_Init" />&nbsp;   
                    <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluir_Click" OnPreRender="btnExcluir_PreRender" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovoCadastro" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
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
                <cc1:UpLoad ID="UpLoad2" runat="server" OnUpLoadComplete="UpLoad2_UpLoadComplete" Pagina="HandlerFuncionario.ashx" TamanhoMaximoArquivo="102400" TamanhoParteUpload="102400" OnInit="UpLoad2_Init">
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

    <div id="Popup_Funcoes" class="pop_up" style="width: 75%">
        <div class="barra">
            Funções
        </div>
        <div class="pop_m20">
            <div style="position: relative; max-height: 300px; overflow-y: auto">
                <asp:UpdatePanel ID="upPesquisaFuncoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gdvFuncoes" CssClass="grid" runat="server" AutoGenerateColumns="False"
                            EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="gdvFuncoes_RowDeleting"
                            BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvFuncoes_PageIndexChanging"
                            OnPreRender="gdvFuncoes_PreRender" OnInit="gdvFuncoes_Init">
                            <Columns>
                                <asp:BoundField HeaderText="Cargo" DataField="GetNomeCargo" />
                                <asp:BoundField HeaderText="Unidade" DataField="GetNomeUnidade" />
                                <asp:BoundField HeaderText="Departamento" DataField="GetNomeDepartamento" />
                                <asp:BoundField HeaderText="Setor" DataField="GetNomeSetor" />
                                <asp:BoundField HeaderText="Ativo" DataField="GetAtivo">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnAtivarDesativarFuncao" runat="server" CssClass="botao ok_mini" Text="" Visible="<%# BindUsuarioPodeAtivarDesativarFuncao(Container.DataItem) %>"
                                                CommandName="Delete" ToolTip='<%# BindToolTipFuncao(Container.DataItem) %>' OnPreRender="btnAtivarDesativarFuncao_PreRender" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarFuncao" runat="server" CssClass="botao editar_mini" Text=""
                                                CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarFuncao_Click" OnInit="btnGridEditarFuncao_Init" />
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
                        <asp:AsyncPostBackTrigger ControlID="gdvFuncoes" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvFuncoes" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovaFuncao" runat="server" Text="Nova Função" CssClass="botao novo" OnClick="btnNovaFuncao_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCamposNovaFuncao" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_funcoes" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Cargo<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCargoFuncao" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Cargo" ValidationGroup="vlgFuncao">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <div style="display: inline-block; width: 80%">
                                                <asp:DropDownList ID="ddlCargoFuncao" CssClass="dropDownList100" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="display: inline-block;">
                                                <asp:Button ID="btnEditarCargo" class="botao editar_mini" runat="server" OnClick="btnEditarCargo_Click" OnInit="btnEditarCargo_Init" />
                                            </div>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Unidade<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlUnidadeFuncao" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Unidade" ValidationGroup="vlgFuncao">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%">
                                            <asp:DropDownList ID="ddlUnidadeFuncao" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidadeFuncao_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Departamento<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDepartamentoFuncao" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Departamento" ValidationGroup="vlgFuncao">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList ID="ddlDepartamentoFuncao" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoFuncao_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 25%">
                                        <div class="label_form">
                                            Setor<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSetorFuncao" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Setor" ValidationGroup="vlgFuncao">*</asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hfIdFuncao" runat="server" />
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%;">
                                            <asp:DropDownList ID="ddlSetorFuncao" CssClass="dropDownList100" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: right; margin-top: 10px; margin-right: 10px;">
                                <asp:Button ID="btnSalvarFuncao" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgFuncao" OnClick="btnSalvarFuncao_Click" OnInit="btnSalvarFuncao_Init" />&nbsp;
                                <asp:Button ID="btnExcluirFuncao" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluirFuncao_Click" OnInit="btnExcluirFuncao_Init" OnPreRender="btnExcluirFuncao_PreRender" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarFuncao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnExcluirFuncao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovaFuncao" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoFuncao" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <a id="cancelarFuncoes" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="popCadastroCargo" class="pop_up" style="width: 430px;">
        <div class="barra">
            Cargo
        </div>
        <div class="pop_m20">
            <div class="filtros_titulo">
                Nome<span>*:</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNomeCargo" CssClass="RequireFieldValidator" ErrorMessage="- Nome"
                    ValidationGroup="vlgCargo">*</asp:RequiredFieldValidator>
            </div>
            <div class="filtros_campo" style="padding-right: 15px;">
                <asp:UpdatePanel ID="upCadastroCargo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="campo_form">
                            <asp:TextBox ID="tbxNomeCargo" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:HiddenField ID="hfIdCargo" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNovoCargo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
            <asp:Button ID="btnNovoCargo" CssClass="botao novo" runat="server" Text="Novo" OnClick="btnNovoCargo_Click" />&nbsp;
            <asp:Button ID="btnSalvarCargo" CssClass="botao salvar" runat="server" Text="Salvar" ValidationGroup="vlgCargo" OnClick="btnSalvarCargo_Click" OnInit="btnSalvarCargo_Init" />&nbsp;
            <asp:Button ID="btnExcluirCargo" CssClass="botao excluir" runat="server" Text="Excluir" OnClick="btnExcluirCargo_Click" OnInit="btnExcluirCargo_Init" />&nbsp;                        
            <a id="cancelar_cadastro_cargo" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="popCadastroDepartamento" class="pop_up" style="width: 430px;">
        <div class="barra">
            Departamento
        </div>
        <div class="pop_m20">
            <div class="filtros_titulo">
                Nome<span>*:</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxNomeDepartamento" CssClass="RequireFieldValidator" ErrorMessage="- Nome"
                    ValidationGroup="vlgDepartamento">*</asp:RequiredFieldValidator>
            </div>
            <div class="filtros_campo" style="padding-right: 15px;">
                <asp:UpdatePanel ID="upCadastroDepartamento" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="campo_form">
                            <asp:TextBox ID="tbxNomeDepartamento" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:HiddenField ID="hfIdDepartamento" runat="server" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNovoDepartamento" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
            <asp:Button ID="btnNovoDepartamento" CssClass="botao novo" runat="server" Text="Novo" OnClick="btnNovoDepartamento_Click" />&nbsp;
            <asp:Button ID="btnDalvarDepartamento" CssClass="botao salvar" runat="server" Text="Salvar" ValidationGroup="vlgDepartamento" OnClick="btnDalvarDepartamento_Click" OnInit="btnDalvarDepartamento_Init" />&nbsp;            
            <asp:Button ID="btnExcluirDepartamento" CssClass="botao excluir" runat="server" Text="Excluir" OnClick="btnExcluirDepartamento_Click" OnInit="btnExcluirDepartamento_Init" />&nbsp;                        
            <a id="cancelar_cadastro_departamento" class="botao vermelho">Cancelar</a>
        </div>
    </div>

    <div id="Pop_setores" class="pop_up" style="width: 50%;">
        <div class="barra">
            Setores
        </div>
        <div class="pop_m20">
            <div style="position: relative; max-height: 300px; overflow-y: auto">
                <asp:UpdatePanel ID="upPesquisaSetores" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gdvSetores" CssClass="grid" runat="server" AutoGenerateColumns="False"
                            EnableModelValidation="True" DataKeyNames="Id" OnRowDeleting="gdvSetores_RowDeleting"
                            BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvSetores_PageIndexChanging"
                            OnPreRender="gdvSetores_PreRender" OnInit="gdvSetores_Init">
                            <Columns>
                                <asp:BoundField HeaderText="Setor" DataField="Nome" />
                                <asp:BoundField HeaderText="Departamento" DataField="GetNomeDepartamento" />
                                <asp:TemplateField HeaderText="Editar / Excluir">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnGridEditarSetor" runat="server" CssClass="botao editar_mini" Text=""
                                                CommandArgument='<%# Eval("Id") %>' ToolTip="Editar" OnClick="btnGridEditarSetor_Click" OnInit="btnGridEditarSetor_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">

                                            <asp:Button ID="btnGridExcluirSetor" runat="server" CssClass="botao excluir_mini" Text=""
                                                CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluirSetor_PreRender" />
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
                        <asp:AsyncPostBackTrigger ControlID="gdvSetores" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="gdvSetores" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 5px; margin-top: 15px; margin-right: 10px;">
                <asp:Button ID="btnNovoSetor" runat="server" Text="Novo Setor" CssClass="botao novo" OnClick="btnNovoSetor_Click" />
            </div>
            <div>
                <asp:UpdatePanel ID="upCamposNovoSetor" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="campos_cadastro_setores" runat="server" style="margin-bottom: 15px; margin-top: 10px;" visible="false">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 43%; vertical-align: bottom;">
                                        <div class="label_form">
                                            Nome<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxNomeSetor" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Nome" ValidationGroup="vlgSetor">*</asp:RequiredFieldValidator>
                                            <asp:Button ID="Button1" class="botao editar_mini" runat="server" Visible="false" />
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%; margin-top: 5px;">
                                            <asp:TextBox ID="tbxNomeSetor" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 43%">
                                        <div class="label_form">
                                            Departamento<span>*:</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDepartamentoSetor" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Departamento" ValidationGroup="vlgSetor">*</asp:RequiredFieldValidator>
                                            <asp:Button ID="btnEditarDepartamentoCadSetor" class="botao editar_mini" runat="server" OnClick="btnEditarDepartamentoCadSetor_Click" OnInit="btnEditarDepartamentoCadSetor_Init" />
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%; margin-top: 5px;">
                                            <asp:DropDownList ID="ddlDepartamentoSetor" CssClass="dropDownList100" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 15%; text-align: right; vertical-align: bottom;">
                                        <div class="label_form">
                                            <asp:HiddenField ID="hfIdSetor" runat="server" />
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%; margin-top: 5px;">
                                            <asp:Button ID="btnSalvarSetor" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgSetor" OnClick="btnSalvarSetor_Click" OnInit="btnSalvarSetor_Init" />&nbsp;                                            
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarSetor" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNovoSetor" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
                <a id="cancelar_cadastro_setor" class="botao vermelho" style="padding-top: 5px; padding-bottom: 5px;">Fechar</a>
            </div>
        </div>
    </div>

    <div id="Pop_enviar_email_senha" class="pop_up" style="width: 430px;">
        <div class="barra">
            E-mail para receber login e senha
        </div>
        <div class="pop_m20">
            <div class="filtros_titulo">
                E-mail<span>*:</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbxEmailFuncionarioRecebeSenha" CssClass="RequireFieldValidator" ErrorMessage="- E-mail"
                    ValidationGroup="vlgEmailSenha">*</asp:RequiredFieldValidator>
            </div>
            <div class="filtros_campo" style="padding-right: 15px;">
                <asp:UpdatePanel ID="upEnviarLoginSenhaFuncionario" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="campo_form">
                            <asp:TextBox ID="tbxEmailFuncionarioRecebeSenha" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:HiddenField ID="hfIdFuncionario" runat="server" />
                            <asp:HiddenField ID="hfLoginEnviarEmail" runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 15px; margin-right: 10px;">
            <asp:Button ID="btnEnviarEmailSenhaFuncionario" CssClass="botao email" runat="server" Text="Enviar" ValidationGroup="vlgEmailSenha" OnClick="btnEnviarEmailSenhaFuncionario_Click" OnInit="btnEnviarEmailSenhaFuncionario_Init" />&nbsp;                        
            <a id="cancelar_enviar_email_func" class="botao vermelho">Cancelar</a>
        </div>
    </div>

</asp:Content>

