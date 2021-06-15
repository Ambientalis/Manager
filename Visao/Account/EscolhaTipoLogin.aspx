<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EscolhaTipoLogin.aspx.cs" Inherits="Account_EscolhaTipoLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Escolha o tipo de login</title>
    <link rel="icon" href="../imagens/logo_amb.png" type="image/x-icon" />
    <link rel="shortcut icon" href="../imagens/logo_amb.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="../imagens/logo_amb.png" type="image/x-icon" />
    <script src="../Scripts/jQuery/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jQuery/js/jquery-ui-1.8.20.custom.min.js" type="text/javascript"></script>
    <link href="../Styles/TipoLogin.css" rel="stylesheet" type="text/css" />

    <link href="../imagens/icons/icon_iphone_classic.png" rel="apple-touch-icon" />
    <link href="../imagens/icons/icon_ipad.png" rel="apple-touch-icon" sizes="76x76" />
    <link href="../imagens/icons/icon_iphone_retina.png" rel="apple-touch-icon" sizes="120x120" />
    <link href="../imagens/icons/icon_ipad_retina.png" rel="apple-touch-icon" sizes="152x152" />
    <link href="../imagens/icons/icon_iphone_plus.png" rel="apple-touch-icon" sizes="180x180" />
    <link href="../imagens/icons/icon_android_hi_res.png" rel="icon" sizes="192x192" />
    <link href="../imagens/icons/icon_android.png" rel="icon" sizes="128x128" />
</head>
<body>
    <form id="form1" runat="server">


        <a href="GuiaAtalho.html">
            <div class="header">
                <span>Clique aqui e salve o atalho do Ambientalis Manager!</span>
            </div>
        </a>

        <div class="container">
            <div class="content">
                <div class="logo">
                    <img alt="logo" src="../imagens/logo_amb_manager.png" />
                </div>
                <div class="container-login">
                    <span style="font-size: 1.4em; width: 100%; float: left;">Escolha a forma de login no sistema:</span>
                    <a href="LoginCliente.aspx">
                        <div class="button">
                            <span>Sou Cliente</span>
                        </div>
                    </a>
                    <a href="Login.aspx">
                        <div class="button">
                            <span>Sou Funcionário</span>
                        </div>
                    </a>
                </div>
                <div style="clear: both;"></div>
                <p>Ambientalis © 2014. Todos os direitos reservados.</p>
            </div>
        </div>
    </form>
</body>
</html>
