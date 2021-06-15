<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissaoInsuficiente.aspx.cs" Inherits="Site_PermissaoInsuficiente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            width: 150px;
            height: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 800px; height: 348px; margin-top: 10px; margin-bottom: 50px; font-family: Arial, Helvetica, sans-serif; font-weight: 700; font-size: xx-large; margin-left: auto; margin-right: auto;"
            align="center">
            <img alt="negado" class="style1" src="../imagens/acesso.png" /><br />
            <br />
            Permissão insuficiente para acessar        
            <br />
            esta página.
            <br />
            <br />
            <asp:Button ID="btnTelaTelaLogin" runat="server" Text="Ir para tela de Login" PostBackUrl="~/Account/Login.aspx" />
        </div>
    </form>
</body>
</html>
