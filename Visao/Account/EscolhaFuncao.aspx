<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EscolhaFuncao.aspx.cs" Inherits="Account_EscolhaFuncao" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Escolha de função</title>
    <link id="favicon" runat="server" rel="shortcut icon" type="image/x-icon" />
    <script src="../Scripts/jQuery/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jQuery/js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .botao {
            border: 1px solid #ccc;
            max-width: 100%;
            overflow: hidden;
        }

            .botao:hover {
                border: 1px solid #000;
            }
    </style>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <style type="text/css">
            .conteudo_topo, .div_escolha_funcao, .container_menu .menu > li > ul {
                border-color: <%= ConfiguracaoUtil.GetCorPadrao%>;
            }
            .container_menu, .barra_verde {
                background: <%= ConfiguracaoUtil.GetCorPadrao%>;
            }
            .conteudo_topo a:hover, .conteudo_topo a:hover i{
                color: <%= ConfiguracaoUtil.GetCorPadrao%>;
            }
        </style>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: fixed; top: 80px; left: 50%; transform: translateX(-50%); width: 50%">
            <div id="container_login_header">
                <div style="text-align: center">
                    <asp:Image runat="server" ID="imgLogo" AlternateText="logo" Style="max-height: 85px" />
                </div>
            </div>
            <div style="padding: 5px">
                Escolha a função a qual gostaria de logar no sistema
            </div>
            <div style="background-color: white; border-radius: 5px; padding: 15px; text-align: center">
                <asp:Repeater ID="rptFuncoes" runat="server">
                    <ItemTemplate>
                        <div style="margin: 5px 0px;">
                            <asp:Button ID="btnFuncaoLogar" runat="server" Text='<%# Eval("GetDescricao") %>' CssClass="botao barra_verde"
                                CommandArgument='<%# Eval("Id") %>' OnClick="btnFuncaoLogar_Click" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="login_direitos">
                © 2017. Todos os direitos reservados.<br />
                <asp:Label ID="lblErro" runat="server"></asp:Label>
            </div>
            <cc1:C2MessageBox ID="C2MessageBox1" runat="server" />
        </div>
    </form>
</body>
</html>
