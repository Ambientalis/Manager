<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AceiteVisita.aspx.cs" Inherits="AceiteVisita_AceiteVisita" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aceite e Confirmação de Visita - Ambientalis Project</title>
    <link rel="icon" href="../imagens/logo_amb.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../imagens/logo_amb.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="../imagens/logo_amb.png" type="image/x-icon" />
    <script src="../Scripts/jQuery/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jQuery/js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <link href="../Styles/Style.css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            width: 121px;
            height: 39px;
        }

        #container_aceite {
            width: 800px;
            min-height: 290px;
            position: fixed;
            top: 50%;
            left: 50%;
            margin-left: -400px;
            margin-top: -200px;
            text-align: center;
            background-color: white;
            color: green;
        }

        .style5 {
            font-size: x-large;
            font-family: Arial, Helvetica, sans-serif;
        }

        .style6 {
            color: #5DA209;
        }

        .style7 {
            color: #EB0000;
        }

        .container_texto_aceite {
            font-size: 22px;
            padding-top: 7px;
            color: #000;
            margin-top: 7px;
            text-shadow: 0 1px 0 white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true">
        </asp:ToolkitScriptManager>
        <div id="container_aceite">
            <br />
            <img alt="logo" class="style1" src="../imagens/logo_amb_manager.png" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span style="font-weight: bold; color: Black">Ambientalis Manager</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="container_texto_aceite">Aceite e Confirmação de Visita</span><br />
            <br />
            <br />
            <br />
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfIdVisita" runat="server" />
                        <asp:HiddenField ID="hfEmailQueRespondeu" runat="server" />
                        <div id="pergunta" runat="server">
                            <span class="style6">
                                <span class="style5"><em><strong>
                                    <asp:Label ID="Label1" runat="server" Text="Aceita a realização da visita ?"></asp:Label></strong></em></span>
                            </span>
                            <div style="margin-top: 15px;">
                                <asp:Button ID="btnAceitarVisita" runat="server" Text="Sim" CssClass="botao verde" Width="200px" Height="50px" OnClick="btnAceitarVisita_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnNegarVisita" runat="server" Text="Não" CssClass="botao vermelho" Width="200px" Height="50px" OnClick="btnNegarVisita_Click" />
                            </div>
                        </div>
                        <div id="resposta_sim" runat="server" visible="false">
                            <div style="margin-bottom:15px;">
                                <span class="style6">
                                    <span class="style5"><em><strong>
                                        <asp:Label ID="lblVisitaAceitaSim" runat="server" Text="Visita aceita e confirmada com sucesso!!!"></asp:Label></strong></em></span>
                                </span>
                                <div style="margin-top: 20px; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: black; font-weight: bold;">
                                    A Visita a ser realizada no cliente
                                    <asp:Label ID="lblClienteSim" runat="server"></asp:Label>&nbsp;de
                                    <asp:Label ID="lblDataInicioSim" runat="server"></asp:Label>&nbsp;a
                                    <asp:Label ID="lblDataFimSim" runat="server"></asp:Label>, pelo visitante
                                    <asp:Label ID="lblVisitanteSim" runat="server"></asp:Label>&nbsp;foi aceita e confirmada pelo e-mail
                                    <asp:Label ID="lblEmailAceitouSim" runat="server"></asp:Label>!
                                </div>
                            </div>
                        </div>
                        <div id="motivo_negacao" runat="server" visible="false">
                            <div style="text-align:left; margin-left:10px;">
                                <span class="style7">
                                <span class="style5"><em><strong>
                                    <asp:Label ID="Label2" runat="server" Text="Informe o motivo da negação da visita:"></asp:Label></strong></em></span>
                                </span>
                            </div>
                            <div style="margin:10px;">
                                <asp:TextBox ID="tbxMotivoNegacao" runat="server" CssClass="textBox100" TextMode="MultiLine" Height="120px"></asp:TextBox>                                    
                            </div>
                            <div style="text-align:right; margin-right:10px; margin-bottom:15px;">
                                <asp:Button ID="btnSalvarNegacaoVisita" runat="server" Text="Salvar" CssClass="botao salvar" OnClick="btnSalvarNegacaoVisita_Click" />&nbsp;
                                <asp:Button ID="btnCancelarNegacaoVisita" runat="server" Text="Cancelar" CssClass="botao vermelho" OnClick="btnCancelarNegacaoVisita_Click" />
                            </div>
                        </div>
                        <div id="resposta_nao" runat="server" visible="false">
                            <div style="margin-bottom:15px;">
                                <span class="style7">
                                    <span class="style5"><em><strong>
                                        <asp:Label ID="lblVisitaAceitaNao" runat="server" Text="Visita negada!!!"></asp:Label></strong></em></span>
                                </span>
                                <div style="margin-top: 20px; font-family: Arial, Helvetica, sans-serif; font-size: 16px; color: black; font-weight: bold;">
                                    A Visita a ser realizada no cliente
                                    <asp:Label ID="lblClienteNao" runat="server"></asp:Label>&nbsp;de
                                    <asp:Label ID="lblDataInicioNao" runat="server"></asp:Label>&nbsp;a
                                    <asp:Label ID="lblDataFimNao" runat="server"></asp:Label>, pelo visitante
                                    <asp:Label ID="lblVisitanteNao" runat="server"></asp:Label>&nbsp;foi negada pelo e-mail
                                    <asp:Label ID="lblEmailAceitouNao" runat="server"></asp:Label>, pelo seguinte motivo:<br />
                                    <asp:Label ID="lblMotivoNegacaoNao" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAceitarVisita" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNegarVisita" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSalvarNegacaoVisita" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarNegacaoVisita" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>            
        </div>

    </form>
</body>
</html>
