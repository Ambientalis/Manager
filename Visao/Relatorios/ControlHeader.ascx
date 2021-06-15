<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlHeader.ascx.cs" Inherits="Relatorios_ControlHeader" %>
<div>
    <style>
        table.bordasimples
        {
            border-collapse: collapse;
            width: 100%;
        }

            table.bordasimples tr td
            {
                border: 1px solid black;
            }

        .tabela_relatorio
        {
            border: 1px solid #808080;
            margin-top: 4px;
        }

            .tabela_relatorio th
            {
                padding: 4px 4px;
                background-color: #e5e5e5;
                color: #000;
            }

            .tabela_relatorio td
            {
                padding: 1px 4px;
                font-size: 10px;
            }

            .tabela_relatorio tr.odd
            {
                background-color: #fafafa !important;
            }

            .tabela_relatorio tr.even
            {
                background-color: white;
            }

        .dataTables_info
        {
            font-family: Arial;
            margin-top: 5px;
            margin-bottom: 5px;
            font-size: 9px;
        }

        thead
        {
            display: table-header-group;
        }

        tfoot
        {
            display: table-footer-group;
        }
    </style>
    <table class="bordasimples">
        <tr>
            <td rowspan="3" style="width: 120px; vertical-align: middle; text-align: center;">
                <asp:Image ID="lblImg" runat="server" Style="max-height: 50px; max-width: 120px;" ImageUrl="../imagens/logo_relatorio.png" />
            </td>
            <td style="text-align: center; font-family: Arial; font-size: small; background-color: #F0F0F0;">
                <asp:Label ID="lblFazenda" runat="server" Style="font-family: Arial; font-size: small" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 140px; text-align: center">
                <asp:Label ID="lblHora0" runat="server" Style="font-family: Arial; font-size: small" Font-Bold="True" Text="Sistema: Manager"></asp:Label>
            </td>
        </tr>
        <tr>
            <td rowspan="2" style="text-align: center">
                <asp:Label ID="lblNomeRelatorio" runat="server" Style="text-transform: uppercase; font-family: Arial; font-size: large;" Font-Bold="True" Text=""></asp:Label>
            </td>
            <td style="text-align: center">
                <asp:Label ID="lblHora" runat="server" Style="font-family: Arial; font-size: small" Font-Bold="True" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblUsuario" runat="server" Style="font-family: Arial; font-size: small" Font-Bold="True" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 100%; font-family: Arial; font-size: x-small">
        <tr>
            <td valign="top">
                <asp:Repeater ID="rptEsquerda" runat="server">
                    <ItemTemplate>
                        <b><%#Eval("Atributo") %>:</b>  <%#Eval("Valor") %><br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td valign="top">
                <asp:Repeater ID="rptCentro" runat="server">
                    <ItemTemplate>
                        <b><%#Eval("Atributo") %>:</b>  <%#Eval("Valor") %><br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
            <td valign="top">
                <asp:Repeater ID="rptDireita" runat="server">
                    <ItemTemplate>
                        <b><%#Eval("Atributo") %>:</b>  <%#Eval("Valor") %><br />
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
</div>
