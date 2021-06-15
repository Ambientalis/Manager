<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/MasterPage/MasterPageRelatorios.master" AutoEventWireup="true" CodeFile="RelatorioDeOrdensDeServico.aspx.cs" Inherits="Relatorios_Relatorios_RelatorioDeOrdensDeServico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Relatorios/ControlHeader.ascx" TagPrefix="uc1" TagName="ControlHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            adicionarDatePicker($("#<%= tbxDataDe.ClientID %>"));
            adicionarDatePicker($("#<%= tbxDataAte.ClientID %>"));
        });
    </script>
    <style type="text/css">
        table tr td {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTituloRelatorio" runat="Server">
    Relatório - Ordens de Serviço
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFiltros" runat="Server">
    <div style="padding-top: 10px; padding-bottom: 10px;">
        <div style="width: 150px; text-align: left; font-family: 'Open Sans'; color: black; font-size: 13px;">
            Exibir as Colunas:
        </div>
        <div style="border: 1px solid white; padding: 10px;">
            <div>
                <asp:CheckBoxList ID="ckbColunas" runat="server" CssClass="chekListRelatorios" RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="0" CellSpacing="0" Font-Names="Open Sans" Font-Size="13px" ForeColor="Black">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <table style="width: 100%; margin-right: 0px;">
        <tr>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Código
                </div>
                <asp:TextBox ID="tbxCodigoPesquisa" CssClass="textBoxFiltros" runat="server"></asp:TextBox>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    &nbsp;Pedido
                </div>
                <asp:TextBox ID="tbxNumeroPedidoPesquisa" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td colspan="3" style="padding-right: 15px">
                <div class="labelRelatorio">
                    Descrição
                </div>
                <asp:TextBox ID="tbxDescricaoOSPesquisa" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Trazer OS's
                </div>
                <asp:DropDownList ID="ddlStatusPesquisa" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Selected="True" Value="T"> Somente Ativas</asp:ListItem>
                    <asp:ListItem Value="N">Todas</asp:ListItem>
                    <asp:ListItem Value="F">Inativas</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding-right: 15px" colspan="2">
                <div class="labelRelatorio">
                    Responsável
                </div>
                <asp:DropDownList ID="ddlResponsavelPesquisa" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Tipo de OS
                </div>
                <asp:DropDownList ID="ddlTipoOS" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px">
                <div class="labelRelatorio">
                    Departamento
                </div>
                <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="dropDownFiltros">
                </asp:DropDownList>
            </td>
            <td style="padding-right: 15px" colspan="2">
                <div class="labelRelatorio">
                    Cliente
                </div>
                <asp:DropDownList ID="ddlClientePesquisa" CssClass="dropDownFiltros" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Ordenar por
                </div>
                <asp:DropDownList ID="ddlOrdenarPor" runat="server" CssClass="dropDownFiltros" AutoPostBack="True" OnSelectedIndexChanged="ddlOrdenarPor_SelectedIndexChanged">
                    <asp:ListItem Value="C">Data de Criação</asp:ListItem>
                    <asp:ListItem Value="V">Prazo de Vencimento</asp:ListItem>
                    <asp:ListItem Value="E">Prazo de Encerramento</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
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
                    <asp:TextBox ID="tbxDataDe" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
                </div>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Até
                </div>
                <div class="filtros_campo">
                    <asp:TextBox ID="tbxDataAte" runat="server" CssClass="textBoxFiltros"></asp:TextBox>
                </div>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Status da OS
                </div>
                <asp:DropDownList ID="ddlEstadoOSPesquisa" runat="server" CssClass="dropDownFiltros">
                    <asp:ListItem Value="Todos">Todos</asp:ListItem>
                    <asp:ListItem Selected="True" Value="B">Aberta</asp:ListItem>
                    <asp:ListItem Value="AbertaEVencida">Aberta e já Vencida</asp:ListItem>
                    <asp:ListItem Value="A">Aprovada</asp:ListItem>
                    <asp:ListItem Value="R">Reprovada</asp:ListItem>
                    <asp:ListItem Value="Encerrada">Encerrada</asp:ListItem>
                    <asp:ListItem Value="ComPedidoAdiamento">Com Pedido de adiamento de Prazo</asp:ListItem>
                    <asp:ListItem Value="P">Pendentes de Aprovação</asp:ListItem>
                    <asp:ListItem Value="EncerradaComPedido">Encerrada no Prazo Sem Pedido de Adiamento</asp:ListItem>
                    <asp:ListItem Value="EncerradaSemPedido">Encerrada no Prazo Com Pedido de Adiamento</asp:ListItem>
                    <asp:ListItem Value="Vencida">Vencida</asp:ListItem>
                    <asp:ListItem Value="EncerradaComPedidoAposVencimento">Encerrada no Prazo Com Pedido de Adiamento após o Vencimento</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Ordenação
                </div>
                <div class="filtros_campo">
                    <asp:DropDownList ID="ddlOrdenacao" runat="server" CssClass="dropDownFiltros">
                        <asp:ListItem Value="Crescente">Crescente</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Decrescente">Decrescente</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
            <td style="width: 15%; padding-right: 15px">
                <div class="labelRelatorio">
                    Faturamento
                </div>
                <div class="filtros_campo">
                    <asp:DropDownList ID="ddlFaturadas" runat="server" CssClass="dropDownFiltros">
                        <asp:ListItem Selected="True" Value="0">Todas</asp:ListItem>
                        <asp:ListItem Value="2">Apenas OSs sem custo</asp:ListItem>
                        <asp:ListItem Value="1">Apenas OSs Faturadas</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6" style="padding-right: 15px; text-align: right;">
                <div class="labelRelatorio">
                    &nbsp;
                </div>
                <asp:Button ID="btnExibirRelatorio" runat="server" CssClass="gerarRelatorioBotao" OnClick="btnExibirRelatório_Click" Text="Exibir Relatório" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ControlHeader runat="server" ID="CtrlHeader" />
    <asp:GridView ID="grvRelatorio" CssClass="tabela_relatorio" runat="server" Font-Names="Arial" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" EnableTheming="False" EnableViewState="False" Font-Size="9pt"
        ShowFooter="true">
        <Columns>
            <asp:BoundField HeaderText="Código" DataField="Codigo"></asp:BoundField>
            <asp:BoundField HeaderText="Data" DataField="Data" DataFormatString="{0:d}"></asp:BoundField>
            <asp:BoundField HeaderText="Tipo" DataField="GetTipoOS" />
            <asp:BoundField HeaderText="Status" DataField="GetDescricaoDoStatusDaOS" />
            <asp:BoundField HeaderText="Pedido" DataField="GetNumeroPedido" />
            <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
            <asp:BoundField HeaderText="Unidade" DataField="GetUnidade" />
            <asp:BoundField HeaderText="Cliente" DataField="GetNomeCliente" />
            <asp:BoundField HeaderText="Responsável" DataField="GetNomeResponsavel" />
            <asp:BoundField HeaderText="Órgão" DataField="GetOrgao" />
            <asp:BoundField HeaderText="Protocolo" DataField="ProtocoloOficioEncerramento" />
            <asp:BoundField HeaderText="Data do Protocolo" DataField="GetDataProtocolo" />
            <asp:BoundField HeaderText="Prazo de Vencimento" DataField="GetDataVencimento" DataFormatString="{0:d}"></asp:BoundField>
            <asp:BoundField HeaderText="Descrição do Prazo" DataField="GetDescricaoPrazoVencimento" />
            <asp:BoundField HeaderText="Encerramento" DataField="GetDataEncerramentoString" />
            <asp:BoundField HeaderText="Faturada" DataField="GetDescricaoFaturada" />
            <asp:BoundField HeaderText="OS's Vinculadas" DataField="GetNumerosOSsVinculadas" />
            <asp:TemplateField HeaderText="Pedido de Adiamento (dt Antiga - dt Nova)" ItemStyle-Width="135px">
                <ItemTemplate>
                    <%#BindingPedidosAdiamentoAceitos(Container.DataItem) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Valor Total" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <%#Eval("ValorTotal","{0:c}") %>
                </ItemTemplate>
                <FooterTemplate>
                   <b><%=BindingTotalOSs() %></b>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Left" />
    </asp:GridView>
</asp:Content>

