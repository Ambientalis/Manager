<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="CadastroDeControleDeViagem.aspx.cs" Inherits="ControleDeViagens_CadastroDeControleDeViagem" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("veiculos")) {
                    $(this).removeClass("veiculos");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".veiculos").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".veiculos").children("a").children("span").addClass("bg_branco");
                }
            });

            adicionarMascara();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                adicionarMascara();
            });
        });

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }

        function adicionarMascara() {
            $('.calcular_valor_total').blur(function () {
                calcularValorTotalAbastecimento();
            });
        }

        function calcularValorTotalAbastecimento() {
            var qtdLitros = parseFloat(($('.qtd_litros').val() ? $('.qtd_litros').val().replace(',', '.') : 0));
            console.log(qtdLitros);
            var valor_unitario = parseFloat(($('.valor_unitario').val() ? $('.valor_unitario').val().replace(',', '.') : 0));
            console.log(valor_unitario);
            var valorTotal = qtdLitros * valor_unitario;
            $('.valor_total').val('R$ ' + valorTotal.toFixed(2).replace('.', ','));
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>Controle de Viagens de Veículos</p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:HiddenField ID="hfPopupNovoAbastecimento" runat="server" />
    <asp:ModalPopupExtender ID="hfPopupNovoAbastecimento_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarAbastecimento"
        Enabled="True" PopupControlID="PopupNovoAbastecimento" TargetControlID="hfPopupNovoAbastecimento">
    </asp:ModalPopupExtender>

    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div id="campos_form_cadastro">
        <asp:UpdatePanel ID="upFormularioControleViagens" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <asp:HiddenField ID="hfId" runat="server" />
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="padding-right: 10px">
                            <div class="label_form">
                                Data*
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="tbxData" CssClass="RequireFieldValidator"
                                          ErrorMessage="- Data" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="tbxData" CssClass="textBox100" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tbxData" Format="dd/MM/yyyy" />
                            </div>
                        </td>
                        <td colspan="2" style="padding-right: 10px">
                            <div class="label_form">
                                Responsável*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlResponsavel" InitialValue="0" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Responsável" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:DropDownList ID="ddlResponsavel" CssClass="dropDownList100" runat="server">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td colspan="2" style="padding-right: 10px">
                            <div class="label_form">
                                Motorista*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMotorista" InitialValue="0" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Motorista" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:DropDownList ID="ddlMotorista" CssClass="dropDownList100" runat="server">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 10px">
                            <div class="label_form">
                                Departamento do Veículo
                            </div>
                            <div class="campo_form">
                                <asp:DropDownList ID="ddlDepartamento" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="padding-right: 10px">
                            <div class="label_form">
                                Veículo*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddLVeiculo" InitialValue="0" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Veículo" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:UpdatePanel runat="server" ID="upVeiculos" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlVeiculo" CssClass="dropDownList100" runat="server">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamento" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                        <td style="padding-right: 10px">
                            <div class="label_form">
                                Departamento que Utilizou
                            </div>
                            <div class="campo_form">
                                <asp:DropDownList ID="ddlDepartamentoUtilizou" CssClass="dropDownList100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamentoUtilizou_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="padding-right: 10px">
                            <div class="label_form">
                                Setor que Utilizou*
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddLVeiculo" InitialValue="0" CssClass="RequireFieldValidator"
                                          ErrorMessage="- Setor que Utilizou" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:UpdatePanel runat="server" ID="upSetores" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSetorUtilizou" CssClass="dropDownList100" runat="server">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoUtilizou" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; padding-right: 10px">
                            <div class="label_form">
                                Data e hora de Saída*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxDataHoraSaida" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Data e Hora de Saída" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="tbxDataHoraSaida" CssClass="textBox100" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbxDataHoraSaida" Format="dd/MM/yyyy HH:mm:ss" />
                            </div>
                        </td>
                        <td style="width: 20%; padding-right: 10px">
                            <div class="label_form">
                                Quilometragem de Saída*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbxQuilometragemSaida" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Quilometragem de Saída" ValidationGroup="vlgControleViagens">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="campo_form">
                                <asp:TextBox runat="server" ID="tbxQuilometragemSaida" CssClass="textBox100 mascara_uma_casa_decimal" />
                            </div>
                        </td>
                        <td style="width: 20%; padding-right: 10px">
                            <div class="label_form">
                                Roteiro
                            </div>
                            <div class="campo_form">
                                <asp:DropDownList ID="ddlRoteiro" CssClass="dropDownList100" runat="server">
                                    <asp:ListItem Selected="True" Value="Castelo">Castelo</asp:ListItem>
                                    <asp:ListItem Value="Intermunicipal">Intermunicipal</asp:ListItem>
                                    <asp:ListItem Value="Interestadual">InterEstadual</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20%; padding-right: 10px">
                            <div class="label_form">
                                Data e Hora de Chegada
                            </div>
                            <div class="campo_form">
                                <asp:TextBox ID="tbxDataHoraChegada" CssClass="textBox100" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxDataHoraChegada" Format="dd/MM/yyyy HH:mm:ss" />
                            </div>
                        </td>
                        <td style="width: 20%; padding-right: 10px">
                            <div class="label_form">
                                Quilometragem de Chegada
                            </div>
                            <div class="campo_form">
                                <asp:TextBox runat="server" ID="tbxQuilometragemChegada" CssClass="textBox100 mascara_uma_casa_decimal" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div class="label_form">
                                Observações
                            </div>
                            <div class="campo_form">
                                <asp:TextBox TextMode="MultiLine" Height="80px" runat="server" ID="tbxObservacoes" CssClass="textBox100" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="margin-top: 15px;">
        <div class="label_form" style="font-size: 8pt;">
            &nbsp;<span style="color: Red;">*</span>Campos Obrigatórios
        </div>
    </div>
    <div style="text-align: right">
        <div style="text-align: right; margin-top: 10px;">
            <asp:Button ID="btnNovoCadastro" runat="server" Text="Novo" CssClass="botao novo" OnClick="btnNovoCadastro_Click" />&nbsp;
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvar_Click" ValidationGroup="vlgControleViagens" />&nbsp;
        </div>
    </div>
    <div>
        <div class="barra" style="margin-top: 15px">
            Abastecimentos &nbsp;
                        <asp:Button runat="server" ID="btnNovoAbastecimento" CssClass="botao novo" Text="Novo Abastecimento" OnClick="btnNovoAbastecimento_Click" OnInit="btnNovoAbastecimento_Init" />
            <div class="close" onclick="minimizar('abastecimentos_container');"></div>
        </div>
        <div id="abastecimentos_container" class="cph">
            <div>
                <asp:UpdatePanel ID="upControleViagens" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gdvAbastecimentos" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CssClass="grid" DataKeyNames="Id" EnableModelValidation="True"
                            OnPageIndexChanging="gdvAbastecimentos_PageIndexChanging" OnPreRender="gdvAbastecimentos_PreRender" PageSize="10" OnRowDeleting="gdvAbastecimentos_RowDeleting" OnRowEditing="gdvAbastecimentos_RowEditing">
                            <Columns>
                                <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="QtdLitros" HeaderText="Qtd de Litros" DataFormatString="{0:N1}" />
                                <asp:BoundField DataField="QuilometragemGeral" HeaderText="KM Geral" DataFormatString="{0:N1}" />
                                <asp:BoundField DataField="ValorUnitario" HeaderText="Valor Unitário" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="ValorTotal" HeaderText="Valor Total" DataFormatString="{0:c}" />
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button ID="btnEditarAbastecimento" runat="server" CssClass="botao editar_mini" Text="" CommandName="Edit" ToolTip="Editar este Abastecimento" OnInit="btnEditarAbastecimento_Init" />
                                        </div>
                                        <div style="float: left; margin-left: 5px; margin-top: 5px; margin-bottom: 5px;">
                                            <asp:Button runat="server" CssClass="botao excluir_mini" Text="" CommandName="Delete" ToolTip="Excluir" OnPreRender="btnGridExcluir_PreRender" />
                                        </div>
                                        <div style="clear: both; height: 1px;"></div>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="PopupNovoAbastecimento" class="pop_up" style="width: 630px;">
        <div class="barra">
            Abastecimento
        </div>
        <div class="pop_m20">
            <asp:UpdatePanel ID="upAbastecimento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hfIdAbastecimento" />
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding-right: 10px; width: 33%">
                                <div class="label_form">
                                    Data e Hora* 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxDataAbastecimento" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Data" ValidationGroup="vlgAbastecimento">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox ID="tbxDataAbastecimento" CssClass="textBox100" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbxDataAbastecimento" Format="dd/MM/yyyy HH:mm:ss" />
                                </div>
                            </td>
                            <td style="padding-right: 10px; width: 33%">
                                <div class="label_form">
                                    Qtd de Litros* 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbxQtdDeLitros" CssClass="RequireFieldValidator"
                                        ErrorMessage="- Litros" ValidationGroup="vlgAbastecimento">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox runat="server" ID="tbxQtdDeLitros" CssClass="textBox100 mascara_uma_casa_decimal qtd_litros calcular_valor_total" />
                                </div>
                            </td>
                            <td style="padding-right: 10px; width: 33%">
                                <div class="label_form">
                                    Quilometragem Geral*
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbxQuilometragem" CssClass="RequireFieldValidator"
                                         ErrorMessage="- Litros" ValidationGroup="vlgAbastecimento">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox runat="server" ID="tbxQuilometragem" CssClass="textBox100 mascara_inteiro" />
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td style="padding-right: 10px">
                                <div class="label_form">
                                    Valor Unitário*
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbxValorUnitarioAbastecimento" CssClass="RequireFieldValidator"
                                         ErrorMessage="- Litros" ValidationGroup="vlgAbastecimento">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox runat="server" ID="tbxValorUnitarioAbastecimento" CssClass="textBox100 mascara_dinheiro valor_unitario calcular_valor_total" />
                                </div>
                            </td>
                            <td style="padding-right: 10px">
                                <div class="label_form">
                                    Valor Total
                                </div>
                                <div class="campo_form">
                                    <asp:TextBox runat="server" Enabled="false" ID="tbxValorTotalAbastecimento" CssClass="textBox100 valor_total mascara_dinheiro" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSalvarAbastecimento" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <asp:Button ID="btnSalvarAbastecimento" runat="server" Text="Salvar" CssClass="botao salvar" ValidationGroup="vlgAbastecimento" OnClick="btnSalvarAbastecimento_Click" OnInit="btnSalvarAbastecimento_Init" />&nbsp;
            <a id="cancelarAbastecimento" class="botao vermelho">Cancelar</a>
        </div>
    </div>
</asp:Content>
