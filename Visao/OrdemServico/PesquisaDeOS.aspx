<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="PesquisaDeOS.aspx.cs" Inherits="OrdemServico_PesquisaDeOS" %>

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
        });

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Pesquisa de Ordem de Serviço
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:HiddenField ID="hfPopupPesquisaSatisfacao" runat="server" />
    <asp:ModalPopupExtender ID="PopupPesquisaSatisfacao_ModalPopupExtender" runat="server" BackgroundCssClass="simplemodal" CancelControlID="cancelarPesquisaSatisfacao"
        Enabled="True" PopupControlID="Popup_PesquisaSatisfacao" TargetControlID="hfPopupPesquisaSatisfacao">
    </asp:ModalPopupExtender>
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div class="barra" id="cph_click" style="border-radius: 0px !important; border: 0px;">
        Filtros
        <div class="close" onclick="minimizar('os_container');">
        </div>
    </div>
    <div id="os_container" class="cph">
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPesquisar">
            <table align="center" width="100%">
                <tr>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Código
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxCodigoPesquisa" CssClass="textBox100" runat="server"></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Pedido
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxNumeroPedidoPesquisa" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding-right: 15px" colspan="2">
                        <div class="filtros_titulo">
                            Descrição
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxDescricaoOSPesquisa" runat="server" CssClass="textBox100"></asp:TextBox>
                        </div>
                    </td>
                    <td style="padding-right: 15px;">
                        <div class="filtros_titulo">
                            Trazer OS's
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlStatusPesquisa" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Selected="True" Value="T"> Somente Ativas</asp:ListItem>
                                <asp:ListItem Value="N">Todas</asp:ListItem>
                                <asp:ListItem Value="F">Inativas</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Tipo de OS
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlTipoOS" CssClass="dropDownList100" runat="server">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Responsável
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlResponsavelPesquisa" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td colspan="2" style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Departamento
                        </div>
                        <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="dropDownList100">
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" style="padding-right: 15px">
                        <div class="filtros_titulo">
                            Cliente
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlClientePesquisa" runat="server" CssClass="dropDownList100">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Ordenar por
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlOrdenarPor" runat="server" CssClass="dropDownList100" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenarPor_SelectedIndexChanged">
                                <asp:ListItem Value="C">Data de Criação</asp:ListItem>
                                <asp:ListItem Value="V">Prazo de Vencimento</asp:ListItem>
                                <asp:ListItem Value="E">Prazo de Encerramento</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            <asp:UpdatePanel runat="server" ID="upDatas" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblDatas" runat="server" Text="Data de" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlOrdenarPor" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxVencimentoDe" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxVencimentoDe">
                            </asp:CalendarExtender>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Até
                        </div>
                        <div class="filtros_campo">
                            <asp:TextBox ID="tbxVencimentoAte" runat="server" CssClass="textBox100"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxVencimentoAte">
                            </asp:CalendarExtender>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Status da OS
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlEstadoOSPesquisa" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Value="Todos">Todos</asp:ListItem>
                                <asp:ListItem Selected="True" Value="B">Aberta</asp:ListItem>
                                <asp:ListItem Value="AbertaEVencida">Aberta e já Vencida</asp:ListItem>
                                <asp:ListItem Value="A">Aprovada</asp:ListItem>
                                <asp:ListItem Value="R">Reprovada</asp:ListItem>
                                <asp:ListItem Value="Encerrada">Encerrada</asp:ListItem>
                                <asp:ListItem Value="ComPedidoAdiamento">Com Pedido de adiamento de Prazo</asp:ListItem>
                                <asp:ListItem Value="ComPedidoAdiamentoSemParecer">Com Pedido de adiamento de Prazo pendente de parecer</asp:ListItem>
                                <asp:ListItem Value="P">Pendentes de Aprovação</asp:ListItem>
                                <asp:ListItem Value="EncerradaComPedido">Encerrada no Prazo Sem Pedido de Adiamento</asp:ListItem>
                                <asp:ListItem Value="EncerradaSemPedido">Encerrada no Prazo Com Pedido de Adiamento</asp:ListItem>
                                <asp:ListItem Value="Vencida">Vencida</asp:ListItem>
                                <asp:ListItem Value="EncerradaComPedidoAposVencimento">Encerrada no Prazo Com Pedido de Adiamento após o Vencimento</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Ordenação
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlOrdenacao" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Value="Crescente">Crescente</asp:ListItem>
                                <asp:ListItem Selected="True" Value="Decrescente">Decrescente</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="width: 15%; padding-right: 15px">
                        <div class="filtros_titulo">
                            Faturamento
                        </div>
                        <div class="filtros_campo">
                            <asp:DropDownList ID="ddlFaturadas" runat="server" CssClass="dropDownList100">
                                <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
                                <asp:ListItem Value="2">Apenas OSs sem custo</asp:ListItem>
                                <asp:ListItem Value="1">Apenas OSs Faturadas</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="padding-right: 15px; text-align: right;">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="botao pesquisaBtn" OnClick="btnPesquisar_Click" Text="Pesquisar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="position: relative; margin-top: 20px;">
        <asp:UpdatePanel ID="upOrdens" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gdvOrdensServico" CssClass="grid" runat="server" AutoGenerateColumns="False" PageSize="50"
                    EnableModelValidation="True" AllowPaging="True" DataKeyNames="Id" BorderStyle="None" BorderWidth="0px" OnPageIndexChanging="gdvOrdensServico_PageIndexChanging"
                    OnPreRender="gdvOrdensServico_PreRender">
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="Codigo">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Data" DataField="Data" DataFormatString="{0:d}">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="GetDescricaoDoStatusDaOS" />
                        <asp:BoundField HeaderText="Pedido" DataField="GetNumeroPedido" />
                        <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                        <asp:BoundField HeaderText="Unidade" DataField="GetUnidade" />
                        <asp:BoundField HeaderText="Cliente" DataField="GetNomeCliente" />
                        <asp:BoundField HeaderText="Responsável" DataField="GetNomeResponsavel" />
                        <asp:BoundField HeaderText="Prazo de Vencimento" DataField="GetDataVencimento" DataFormatString="{0:d}"></asp:BoundField>
                        <asp:BoundField HeaderText="Descrição do Prazo" DataField="GetDescricaoPrazoVencimento" />
                        <asp:BoundField HeaderText="Encerramento" DataField="GetDataEncerramentoString" />
                        <asp:BoundField HeaderText="OS's Vinculadas" DataField="GetNumerosOSsVinculadas" />
                        <asp:TemplateField HeaderText="Satisfação">
                            <ItemTemplate>
                                <asp:Panel runat="server" Visible='<%# Eval("IsEncerrada") %>'>
                                    <asp:LinkButton ID="btnReenviarPesquisaDeSatisfacao" CommandArgument='<%#Eval("Id") %>' OnPreRender="btnReenviarPesquisaDeSatisfacao_PreRender" runat="server" Visible='<%# (!(bool)Eval("IsPesquisaSatisfacaoRespondida")) %>'
                                        ToolTip='<%# BindingTooltipReenviarPesquisa(Container.DataItem) %>' OnInit="btnReenviarPesquisaDeSatisfacao_Init" OnClick="btnReenviarPesquisaDeSatisfacao_Click">
                                        N/R
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnVisualizarPesquisaSatisfacao" CommandArgument='<%#Eval("Id") %>' runat="server" Visible='<%# (bool)Eval("IsPesquisaSatisfacaoRespondida") %>'
                                        ToolTip="Clique aqui para ver os detalhes da pesquisa" OnInit="btnVisualizarPesquisaSatisfacao_Init" OnClick="btnVisualizarPesquisaSatisfacao_Click">
                                        <%# Eval("GetDescricaoMediaSatisfacao") %>
                                    </asp:LinkButton>
                                </asp:Panel>
                            </ItemTemplate>
                            <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:HyperLink ID="btnGridEditar" runat="server" CssClass="botao editar_mini" NavigateUrl="<%# BindEditar(Container.DataItem) %>" ToolTip="Editar" Style="display: inline-block; height: 16px; width: 16px;"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="60px" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                    <PagerStyle CssClass="gridPager" />
                    <RowStyle CssClass="gridRow" />
                </asp:GridView>
                <br />
                <br />
                <br />
                <div class="quantidade_pesquisada filtros_titulo">
                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label><br />
                    <span>*N/R = Não respondida</span>
                </div>
                <div class="quantidades_pagina">
                    <asp:DropDownList ID="ddlPaginacao" CssClass="dropDownListAuto" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPaginacao_SelectedIndexChanged" ToolTip="Resultados por página">
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                        <asp:ListItem>200</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlPaginacao" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="gdvOrdensServico" EventName="RowDeleting" />
                <asp:AsyncPostBackTrigger ControlID="gdvOrdensServico" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
    <div id="Popup_PesquisaSatisfacao" class="pop_up" style="width: 70%">
        <div class="barra">
            Pesquisa de satisfação
        </div>
        <div class="pop_m20">
            <asp:UpdatePanel ID="upPopupPesquisaSatisfacao" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <div style="">
                            <div>
                                <span style="font-weight: bold;">Data de criação:</span>
                                <asp:Label runat="server" ID="lblDataCriacaoPesquisa" />
                            </div>
                            <div>
                                <span style="font-weight: bold;">Data da resposta:</span>
                                <asp:Label runat="server" ID="lblDataRespostaPesquisa" />
                            </div>
                        </div>
                        <div>
                            <div>
                                <span style="font-weight: bold;">Cliente:</span>
                                <asp:Label runat="server" ID="lblClientePesquisa" />
                            </div>
                            <div>
                                <span style="font-weight: bold;">Número da OS:</span>
                                <asp:Label runat="server" ID="lblNumeroOSPesquisa" />
                            </div>
                        </div>
                    </div>

                    <div style="border: 1px dashed #ccc; margin: 10px 0px; border-radius: 4px; max-height: 300px; overflow-y: auto;">
                        <asp:GridView ID="grvPerguntasPesquisa" CssClass="grid" runat="server" AutoGenerateColumns="False" PageSize="50"
                            EnableModelValidation="True" DataKeyNames="Id" BorderStyle="None" BorderWidth="0px">
                            <Columns>
                                <asp:BoundField HeaderText="Pergunta" DataField="GetPergunta" />
                                <asp:BoundField HeaderText="Resposta" DataField="GetDescricaoSatisfacao">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                            <PagerStyle CssClass="gridPager" />
                            <RowStyle CssClass="gridRow" />
                        </asp:GridView>
                    </div>
                    <div>
                        <span style="font-weight: bold;">Sugestões:</span><br />
                        <div style="max-height: 100px; overflow-y: auto">
                            <asp:Label runat="server" ID="lblSugestoesPesquisa" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="text-align: right; margin-bottom: 15px; margin-top: 10px; margin-right: 10px;">
            <a id="cancelarPesquisaSatisfacao" class="botao vermelho">Fechar</a>
        </div>
    </div>
</asp:Content>

