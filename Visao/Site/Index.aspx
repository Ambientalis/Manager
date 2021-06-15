<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Site_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .janela_auxiliar {
            width: 35%;
            position: absolute;
            right: 10px;
            top: 10px;
            bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 3px;
            margin: 0.3%;
            vertical-align: top;
        }

            .janela_auxiliar .titulo {
                text-align: center;
                font-size: 1.2em;
                padding: 5px;
                border-bottom: 1px solid #ccc;
                background: linear-gradient(#fff,#eee);
                color: #333;
            }

            .janela_auxiliar .container {
                padding: 10px;
                height: 90%;
                background: rgba(255, 255, 255, 0.5);
            }

                .janela_auxiliar .container .item_container {
                    border-bottom: 1px solid #aaa;
                    padding: 6px;
                    position: relative;
                    color: #444;
                }

                    .janela_auxiliar .container .item_container:hover {
                        background: #ccc;
                    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        BEM VINDO
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <div>
        <div style="width: 100%; padding-left: 20px;" valign="top">
            <div>
            </div>
            <div style="width: 800px; height: 220px; margin: 120px; vertical-align: middle; text-align: center;">
                <asp:Image runat="server" ID="imgLogoIndex" />
            </div>
            <div class="janela_auxiliar">
                <div class="titulo">
                    <i class="fa fa-calendar"></i>&nbsp;Clientes aniversariantes do mês
                </div>
                <div class="container">
                    <div style="max-height: 100%; overflow-y: auto">
                        <asp:Repeater runat="server" ID="rptAniversariantesDoMes">
                            <ItemTemplate>
                                <div class="item_container">
                                    <%#Eval("DataNascimento","{0:d}") %> - <%#Eval("Nome") %> (<%#Eval("Funcao") %>) - <a target="_blank" href='<%# BindingUrlCliente(Container.DataItem) %>'><%# BindingNomeCliente(Container.DataItem) %></a> - <%#Eval("Email") %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>
