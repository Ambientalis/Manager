<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginCliente.aspx.cs" Inherits="Account_LoginCliente" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>ACESSO RESTRITO - Ambientalis Project</title>
    <link id="favicon" runat="server" rel="shortcut icon" type="image/x-icon" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('<%= tbxLogin.ClientID %>').focus();
        });
    </script>
    <style type="text/css">
        .container-login {
            color: #777;
            -webkit-box-shadow: 5px 5px 30px -8px rgba(0,0,0,0.75);
            -moz-box-shadow: 5px 5px 30px -8px rgba(0,0,0,0.75);
            box-shadow: 5px 5px 30px -8px rgba(0,0,0,0.75);
        }

            .container-login .input-group-addon i {
                width: 18px;
            }

            .container-login .input-group {
                margin-bottom: 5px;
            }

            .container-login .panel-heading {
                background: #fff;
                font-size: 150%;
                position: relative;
                color: #777;
            }

            .container-login .panel-footer {
                background: #fff;
            }

            .container-login .btn {
                color: #fff;
                text-shadow: none;
                border-color: #777;
            }

                .container-login .btn:hover {
                    opacity: 0.9;
                }
        .login_direitos {
            text-align: center;
        }
        .container-login .btn {
             background: #1abc9c;
             background: <%= ConfiguracaoUtil.GetCorPadrao%>;
        }
    </style>
</head>
<body style="background: #D8D6D8 url(../imagens/bg_login.png);">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="container-fluid">
            <div class="row" style="margin-top: 80px;">
                <div class="col-md-4 col-md-offset-4">
                    <div style="text-align: center; margin: 10px; padding: 10px; border-radius: 4px; margin: 3px 0px;">
                        <asp:Image runat="server" ID="imgLogo" AlternateText="logo" Style="max-height: 85px" />
                    </div>
                    <div class="panel panel-default container-login">
                        <div class="panel-heading">
                            Acesso restrito
                            <i class="fa fa-lock fa-2x" style="position: absolute; right: 15px; top: 5px;"></i>
                        </div>
                        <div class="panel-body">
                            <div style="margin-bottom: 15px; text-align: center;">
                                <b>Administração:</b> Faça o login para acessar o painel.
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                <asp:TextBox ID="tbxLogin" CssClass="form-control" runat="server" placeholder="Login..." ValidationGroup="tg"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="tbxLogin" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-unlock"></i></span>
                                <asp:TextBox CssClass="form-control" ID="tbxSenha" placeholder="Senha..." runat="server" ValidationGroup="tg" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" SetFocusOnError="true" ControlToValidate="tbxSenha" ValidationGroup="rfv"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:UpdatePanel runat="server" ID="upAux">
                                        <ContentTemplate></ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnLogar" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <div style="padding-top: 7px">
                                                <i class="fa fa-refresh fa-spin"></i>&nbsp;Aguarde
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="col-md-6" style="text-align: right">
                                    <asp:Button ID="btnLogar" CssClass="btn btn-default" runat="server" Style="width: 100%" Text="Acessar" ValidationGroup="rfv" OnClick="btnLogar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="login_direitos">
                        © 2017. Todos os direitos reservados.<br />
                        <asp:Label ID="lblErro" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>


        <cc1:C2MessageBox ID="C2MessageBox1" runat="server" />
    </form>
</body>
</html>
