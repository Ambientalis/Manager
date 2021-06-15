<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroDeReserva.aspx.cs" Inherits="Reservas_CadastroDeReserva" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Marcaras();
            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("reservas")) {
                    $(this).removeClass("reservas");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".reservas").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".reservas").children("a").children("span").addClass("bg_branco");
                }
            });
        });

        function Marcaras() {
            $('#<%=tbxQuilometragem.ClientID %>').unbind();
            $('#<%=tbxQuilometragem.ClientID %>').maskMoney({ thousands: '', decimal: ',' });

            $('#<%=tbxConsumo.ClientID %>').unbind();
            $('#<%=tbxConsumo.ClientID %>').maskMoney({ thousands: '', decimal: ',' });
        }

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }

    </script>
    <style>
        .cancelada {
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
        Cadastro de Reserva
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:Label ID="lblCadastroOcorrencia" runat="server"></asp:Label>
    <asp:ModalPopupExtender ID="lblCadastroOcorrencia_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelar_ocorrencia"
        Enabled="True" PopupControlID="PopCadastroOcorrencia" TargetControlID="lblCadastroOcorrencia">
    </asp:ModalPopupExtender>
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioVeiculo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () { Marcaras(); });
                </script>
                <div id="pendencia_boletim" class="cancelada" runat="server" visible="false">
                    <strong><span class="auto-style1">Reserva pendente de lançar o Boletim de Uso</span></strong><br />
                    Você possui pelo menos uma reserva pendente de lançar o boletim de uso.
                </div>
                <div style="float: left; width: 48%; margin-right: 10px;">
                    <div class="barra">
                        Dados Básicos
                    </div>
                    <div class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Descrição*
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxDescricao" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Data" ValidationGroup="vlgReserva">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxDescricao" CssClass="textBox100" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Veículo*
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVeiculo" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Data" ValidationGroup="vlgReserva">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form">
                                            <asp:DropDownList ID="ddlVeiculo" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVeiculo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 30%;">
                                                    <div class="label_form">
                                                        início*
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxDataInicio" CssClass="RequireFieldValidator"
                                                            ErrorMessage="- Data" ValidationGroup="vlgReserva">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 5%;">
                                                        <asp:TextBox ID="tbxDataInicio" runat="server" CssClass="textBox100"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataInicio">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </td>
                                                <td style="width: 20%;">
                                                    <div class="label_form">
                                                        período
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 5%;">
                                                        <asp:DropDownList ID="ddlPeriodoInicioReserva" CssClass="dropDownList100" runat="server">
                                                            <asp:ListItem Value="M">Manhã</asp:ListItem>
                                                            <asp:ListItem Value="T">Tarde</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td style="width: 30%;">
                                                    <div class="label_form">
                                                        Fim*
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbxDataFim" CssClass="RequireFieldValidator"
                                                            ErrorMessage="- Data" ValidationGroup="vlgReserva">*</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="campo_form" style="padding-right: 5%;">
                                                        <asp:TextBox ID="tbxDataFim" runat="server" CssClass="textBox100"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataFim">
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Tipo de Reserva*
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoReserva" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Data" ValidationGroup="vlgReserva">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form">
                                            <asp:DropDownList ID="ddlTipoReserva" CssClass="dropDownList100" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Responsável*
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlResponsavel" InitialValue="0" CssClass="RequireFieldValidator"
                                                ErrorMessage="- Data" ValidationGroup="vlgReserva">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="campo_form">
                                            <asp:DropDownList ID="ddlResponsavel" CssClass="dropDownList100" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="label_form">
                                            Status da Reserva
                                        </div>
                                        <div class="campo_form">
                                            <asp:TextBox ID="tbxStatusReserva" CssClass="textBox100" runat="server" Text="Não definido..." ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfId" runat="server" />
                        </div>
                    </div>
                </div>
                <div style="float: right; width: 51%;">
                    <div class="barra">
                        Boletim de Uso
                            <div class="close" onclick="minimizar('boletim_container');"></div>
                    </div>
                    <div id="boletim_container" class="cph">
                        <div>
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%;">
                                        <div class="label_form">
                                            Quilomêtragem (KM)
                                        </div>
                                        <div class="campo_form" style="padding-right: 5%; margin-top: 5px;">
                                            <asp:TextBox ID="tbxQuilometragem" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 50%;">
                                        <div class="label_form">
                                            Consumo (LTS)
                                        </div>
                                        <div class="campo_form" style="margin-top: 5px; padding-right: 5%;">
                                            <asp:TextBox ID="tbxConsumo" runat="server" CssClass="textBox100"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel ID="pnlSalvarBoletim" runat="server" Visible="false">
                            <div style="text-align: right; margin-top: 10px">
                                <asp:Button ID="btnSalvarBoletimUso" runat="server" CssClass="botao salvar" Text="Salvar Boletim de Uso" OnClick="btnSalvarBoletimUso_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="barra" style="margin-top: 15px;">
                        Ocorrências                            
                            <div class="close" onclick="minimizar('ocorrencias_container');"></div>
                    </div>
                    <div id="ocorrencias_container" class="cph">
                        <div>
                            <div style="text-align: right;">
                                <asp:Button ID="btnNovaOcorrencia" runat="server" Text="Nova Ocorrência" CssClass="botao novo" OnClick="btnNovaOcorrencia_Click" OnInit="btnNovaOcorrencia_Init" Visible="False" />
                            </div>
                            <div style="margin-top: 10px;">
                                <asp:UpdatePanel ID="upOcorrencias" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gdvOcorrencias" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True" OnPageIndexChanging="gdvOcorrencias_PageIndexChanging" OnPreRender="gdvOcorrencias_PreRender" OnRowDeleting="gdvOcorrencias_RowDeleting" PageSize="3">
                                            <Columns>
                                                <asp:BoundField DataField="GetData" HeaderText="Data">
                                                    <HeaderStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                <asp:BoundField DataField="GetNomeTipo" HeaderText="Tipo" />
                                                <asp:TemplateField HeaderText="Editar / Excluir">
                                                    <ItemTemplate>
                                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                            <asp:Button ID="btnGridEditarOcorrencia" runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="botao editar_mini" Text="" ToolTip="Editar" OnClick="btnGridEditarOcorrencia_Click" OnInit="btnGridEditarOcorrencia_Init" Visible="<%#BindingVisivelEdicaoOCorrencia(Container.DataItem) %>" Enabled="<%#BindingVisivelEdicaoOCorrencia(Container.DataItem) %>" />
                                                        </div>
                                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                                            <asp:Button ID="btnGridExcluirOcorrencia" runat="server" CommandName="Delete" CssClass="botao excluir_mini" OnPreRender="btnGridExcluirOcorrencia_PreRender" Text="" ToolTip="Excluir" Visible="<%#BindingVisivelEdicaoOCorrencia(Container.DataItem) %>" Enabled="<%#BindingVisivelEdicaoOCorrencia(Container.DataItem) %>" />
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
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="clear: both"></div>
                <div style="margin-top: 15px;">
                    <div class="label_form" style="font-size: 8pt;">
                        &nbsp;<span style="color: Red;">*</span>Campos Obrigatórios
                    </div>
                </div>
                <div style="text-align: right">
                    <div style="text-align: right; margin-top: 10px;">
                        <asp:Button ID="btnNovoCadastro" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoCadastro_Click" />&nbsp;
                            <asp:Button ID="btnSolicitarReserva" runat="server" Text="Solicitar Reserva" CssClass="botao email" OnClick="btnSolicitarReserva_Click" ValidationGroup="vlgReserva" />
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvar_Click" ValidationGroup="vlgReserva" />&nbsp;
                            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="botao excluir" OnClick="btnExcluir_Click" OnPreRender="btnExcluir_PreRender" />&nbsp;
                            <asp:Button ID="btnAprovar" runat="server" Text="Aprovar" CssClass="botao ok" OnClick="btnAprovar_Click" />&nbsp;
                            <asp:Button ID="btnEncerrar" runat="server" Text="Encerrar Reserva" CssClass="botao ok" OnClick="btnEncerrar_Click" OnPreRender="btnEncerrar_PreRender" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovoCadastro" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnExcluir" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlVeiculo" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnAprovar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEncerrar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="PopCadastroOcorrencia" class="pop_up" style="width: 580px;">
        <div class="barra">
            Ocorrência
        </div>
        <div class="cph">
            <asp:UpdatePanel ID="upCamposOcorrencia" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="label_form">
                                    Data<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="tbxDataOcorrencia" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Descrição" ValidationGroup="vlgOcorrencia">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataOcorrencia" runat="server" CssClass="textBox50"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="tbxDataOcorrencia" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Descrição<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbxDescricaoOcorrencia" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Data" ValidationGroup="vlgOcorrencia">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDescricaoOcorrencia" runat="server" CssClass="textBox100" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="label_form" style="margin-top: 10px;">
                                    Tipo de Ocorrência<span>*:</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlTipoOcorrencia" InitialValue="0" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Data" ValidationGroup="vlgOcorrencia">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:DropDownList ID="ddlTipoOcorrencia" CssClass="dropDownList100" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfIdOcorrencia" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNovoCadastroOcorrencia" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnNovoCadastroOcorrencia" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoCadastroOcorrencia_Click" />&nbsp;
            <asp:Button ID="btnSalvarOcorrencia" CssClass="botao salvar" runat="server" Text="Salvar" ValidationGroup="vlgOcorrencia" OnClick="btnSalvarOcorrencia_Click" OnInit="btnSalvarOcorrencia_Init" />&nbsp;                                   
            <a id="cancelar_ocorrencia" class="botao vermelho">Fechar</a>
        </div>
    </div>

</asp:Content>

