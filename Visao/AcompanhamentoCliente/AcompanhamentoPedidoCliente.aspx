<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcompanhamentoPedidoCliente.aspx.cs" Inherits="AcompanhamentoCliente_AcompanhamentoPedidoCliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Src="../Utilitarios/Update.ascx" TagName="Update" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <!--[if IE]>
    <link rel="stylesheet" type="text/css" href="../Styles/Hackie7.css" />
<![endif]-->
    <link href="../Styles/popup_basic.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/jQuery/css/ui-lightness/jquery-ui-1.8.20.custom.css" rel="stylesheet"
        type="text/css" />
    <link href="../Styles/Validations.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jQuery/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jQuery/js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/jQuery/development-bundle/ui/i18n/jquery.ui.datepicker-pt-BR.js"
        type="text/javascript"></script>
    <script src="../Scripts/jquery.cycle.all.latest.js" type="text/javascript"></script>
    <script src="../Scripts/Funcoes.js" type="text/javascript"></script>
    <script src="../Scripts/script.js" type="text/javascript"></script>
    <script src="../Scripts/tabs.js" type="text/javascript"></script>
    <script src="../Utilitarios/jquery.maskedinput-1.1.4.pack.js" type="text/javascript"></script>
    <link href="../C2Box/Style1/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="../C2Box/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="../C2Box/InitC2Box.js" type="text/javascript"></script>
    <link rel="icon" href="../imagens/favicon2.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../imagens/favicon2.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="../imagens/favicon2.png" type="image/x-icon" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none;">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
                EnableScriptLocalization="true">
            </asp:ToolkitScriptManager>
            <cc1:C2MessageBox ID="C2MessageBox1" runat="server" />
            <asp:HiddenField ID="hfIdEmpresa" runat="server" />
        </div>
        <div id="barra_info">
            <ul style="height: 36px;">
                <li><a href="../Site/Index.aspx">
                    <asp:Image ID="imgLogoEmpresa" runat="server" ImageUrl="../imagens/logo_amb_manager_mini.png" /></a> </li>
                <li class="barra_separador"></li>
                <li style="font-size: 10px; padding-top: 7px; color: #aaa7a7;"><span style="color: black">Ambientalis Manager</span> Beta</li>
                <li class="barra_separador"></li>
                <li class="fr" style="margin-right: 20px; text-align: center"><a href="../Account/LoginCliente.aspx"
                    class="botao vermelho fr">Logout</a> &nbsp;</li>

                <li class="fr" style="margin-right: 6px; padding-top: 6px;">
                    <asp:Label ID="lblDadosUsuarioLogado" runat="server"></asp:Label>
                </li>
            </ul>
        </div>
        <div style="height: 40px; background-color: black; border: none;"></div>
        <div id="container">
            <div class="barra barra_verde">
                Acompanhamento de Pedidos
            </div>
            <div class="cph">
                <div>
                    <asp:CheckBox ID="ckbVisualizarPedidosOSAbertas" runat="server" Text="Visualizar apenas pedidos com OS's abertas" Checked="true" AutoPostBack="True" OnCheckedChanged="ckbVisualizarPedidosOSAbertas_CheckedChanged" />
                </div>
                <div style="margin-top: 15px;">
                    <asp:UpdatePanel ID="upPedidos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                        <ContentTemplate>
                            <asp:Repeater ID="rptPedidos" runat="server">
                                <ItemTemplate>
                                    <div style="font-size: 16pt; font-family: Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif; font-weight: bold;">
                                        <asp:Label ID="lblDescricaoProjeto" runat="server" Text="<%# BindDescricaoProjeto(Container.DataItem) %>"></asp:Label>
                                    </div>
                                    <div style="margin-top: 10px; margin-bottom: 35px;">
                                        <asp:GridView ID="gdvOrdensServico" DataSource='<%# BindOrdensServico(Container.DataItem) %>' CssClass="grid" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                                            DataKeyNames="Id" BorderStyle="None" BorderWidth="0px" OnPreRender="gdvOrdensServico_PreRender" OnRowDataBound="gdvOrdensServico_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="Código" DataField="Codigo">
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Data" DataField="Data" DataFormatString="{0:d}">
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Tipo" DataField="GetTipoOS" />
                                                <asp:BoundField HeaderText="Responsável" DataField="GetNomeResponsavel" />
                                                <asp:BoundField HeaderText="Descrição" DataField="Descricao" />
                                                <asp:BoundField HeaderText="Status" DataField="GetDescricaoDoStatusDaOS">
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Atividades">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="gdvAtividades" DataSource='<%# BindAtividades(Container.DataItem) %>' runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px"
                                                            CssClass="grid" DataKeyNames="Id" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="GetData" HeaderText="Data">
                                                                    <HeaderStyle Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="GetNomeTipoAtividade" HeaderText="Tipo" />
                                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
                                                            </Columns>
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="gridPager" />
                                                            <RowStyle CssClass="gridRow" />
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Visitas">
                                                    <ItemTemplate>
                                                        <asp:GridView ID="gdvVisitas" DataSource='<%# BindVisitas(Container.DataItem) %>' runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px"
                                                            CssClass="grid" DataKeyNames="Id" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="GetDataInicio" HeaderText="Início">
                                                                    <HeaderStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="GetDataFim" HeaderText="Fim">
                                                                    <HeaderStyle Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="GetNomeTipoVisita" HeaderText="Tipo" />
                                                                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                                                                <asp:BoundField DataField="GetNomeResponsavel" HeaderText="Responsável" />
                                                            </Columns>
                                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                                            <PagerStyle CssClass="gridPager" />
                                                            <RowStyle CssClass="gridRow" />
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridHeader" HorizontalAlign="Left" />
                                            <PagerStyle CssClass="gridPager" />
                                            <RowStyle CssClass="gridRow" />
                                        </asp:GridView>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="font-size: 16pt; font-family: Arial, 'DejaVu Sans', 'Liberation Sans', Freesans, sans-serif; font-weight: bold;">
                                <asp:Label ID="lblNenhumPedidoParaEsteCliente" runat="server" Text="Nenhum pedido cadastrado para você" Visible="false"></asp:Label>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ckbVisualizarPedidosOSAbertas" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <div id="footer">
            <div style="text-align: center; margin-top: 20px; font-family: Tahoma; font-size: 11px; color: black; margin-bottom: 100px;">Telefone: (28) 3542-6013 / Email: contato@ambientalis-es.com.br</div>
            <br />
            <br />
        </div>
        <uc1:MBOX ID="MBOX1" runat="server" />
        <uc2:Update ID="Update1" runat="server" />
    </form>
</body>
</html>
