<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeFile="FiltrosRelatorios.aspx.cs" Inherits="Relatorios_FiltrosRelatorios" %>

<%@ Register Assembly="SisWebControls" Namespace="SisWebControls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#menu_principal").children().each(function (index) {
                if ($(this).hasClass("relatorios")) {
                    $(this).removeClass("relatorios");
                    $(this).addClass("menu_ativado");
                    $(this).children('ul').show();
                    $(this).children("ul").children(".relatorios").children("a").addClass("menu_sub_ativado");
                    $(this).children("ul").children(".relatorios").children("a").children("span").addClass("bg_branco");
                }
            });

            var quantidade_quadros = 100 / $('.container_geral').length;
            $('.container_geral').css('width', (quantidade_quadros - 1) + '%');
        });

        function minimizar(a) {
            $("#" + a + "").slideToggle("normal");
        }
    </script>
    <style type="text/css">
        .container_geral {
            border: 1px solid #ccc;
            border-radius: 3px;
            background: #efefef;
            display: inline-block;
            margin: 0.3%;
            vertical-align: top;
        }

            .container_geral .titulo {
                text-align: center;
                font-size: 1.2em;
                padding: 5px;
                border-bottom: 1px solid #ccc;
                background: #d5d5d5;
            }

            .container_geral .container {
                padding: 10px;
                background: #ffffff;
            }

                .container_geral .container .item_relatorio {
                    border-bottom: 1px solid #aaa;
                    position: relative;
                    color: #444;
                }

                    .container_geral .container .item_relatorio > a {
                        padding: 6px;
                        text-decoration: none;
                        color: #464a4c;
                        display: block;
                    }

                    .container_geral .container .item_relatorio:hover {
                        background: #ccc;
                    }

        @media only screen and (max-width: 1240px) {
            .container_geral {
                width: 100% !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="Server">
    <p>
        Relatórios e gráficos
    </p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <cc1:C2MessageBox ID="C2MessageBox3" runat="server" ScriptOnCancelClick="" ScriptOnOKClick="" />
    <div>
        <asp:Panel runat="server" CssClass="container_geral" ID="pnlRelatorios">
            <div class="titulo">
                <i class="fa fa-file-text-o"></i>&nbsp;Relatórios gerais
            </div>
            <div class="container">
                <asp:Repeater runat="server" ID="rptRelatorios">
                    <ItemTemplate>
                        <div class="item_relatorio">
                            <a target="_blank" href='<%# BindingUrlRelatorio(Container.DataItem) %>'><i class='<%#Eval("Icone") %>'></i>&nbsp;<%#Eval("Nome") %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" CssClass="container_geral" ID="pnlGraficos">
            <div class="titulo" style="background:#9ccddc;color:#fff">
                <i class="fa fa-area-chart"></i>&nbsp;Gráficos
            </div>
            <div class="container">
                <asp:Repeater runat="server" ID="rptGraficos">
                    <ItemTemplate>
                        <div class="item_relatorio">
                            <a target="_blank" href='<%# BindingUrlRelatorio(Container.DataItem) %>'><i class='<%#Eval("Icone") %>'></i>&nbsp;<%#Eval("Nome") %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" CssClass="container_geral" ID="pnlRelatoriosFinanceiros">
            <div class="titulo">
                <i class="fa fa-file-text-o"></i>&nbsp;Relatórios financeiros
            </div>
            <div class="container">
                <asp:Repeater runat="server" ID="rptRelatoriosFinanceiros">
                    <ItemTemplate>
                        <div class="item_relatorio">
                            <a target="_blank" href='<%# BindingUrlRelatorio(Container.DataItem) %>'><i class='<%#Eval("Icone") %>'></i>&nbsp;<%#Eval("Nome") %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" CssClass="container_geral" ID="pnlGraficosFinanceiros">
            <div class="titulo" style="background:#9ccddc;color:#fff">
                <i class="fa fa-area-chart"></i>&nbsp;Gráficos financeiros
            </div>
            <div class="container">
                <asp:Repeater runat="server" ID="rptGraficosFinanceiros">
                    <ItemTemplate>
                        <div class="item_relatorio">
                            <a target="_blank" href='<%# BindingUrlRelatorio(Container.DataItem) %>'><i class='<%#Eval("Icone") %>'></i>&nbsp;<%#Eval("Nome") %></a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="popups" runat="Server">
</asp:Content>

