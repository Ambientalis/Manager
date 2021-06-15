<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MBOX.ascx.cs" Inherits="MBOX" %>

<link href="../MBOX/styles/StyleMBOX.css" rel="stylesheet" type="text/css" />
<script src="../MBOX/scripts/JSMBOX.js" type="text/javascript"></script>

<asp:UpdatePanel ID="upPopUps" runat="server">
    <ContentTemplate> 
        <div id="pnUpFundo" runat="server" class="fundo_MBOX_Visivel"> 
         </div>      
        <div id="pnUp" runat="server" class="pop_MBOX_Visivel">
            <div id="fechar_mbox" class="pop_fechar" onclick="HideMBOX();">
                X
            </div>
            <asp:Label ID="lblCaption" runat="server" Text="" CssClass="popCaption"></asp:Label>
            <asp:Label ID="lblTexto" runat="server"  Text="" CssClass="popTexto" Width="240px"></asp:Label>
            
            <asp:Image ID="iIcon" runat="server" CssClass="popIMG" />
            <div class="popButton">
                <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="BtnOK_Click" CssClass="popUpButtonsUnicos ok"></asp:Button>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="BtnSalvar_Click" CssClass="popUpButtonsUnicos salvar"></asp:Button>
                <asp:Button ID="btnExcluir" runat="server" Text="Excluir" OnClick="BtnExcluir_Click" CssClass="popUpButtonsUnicos excluir"></asp:Button>
                <asp:Button ID="btnAlterar" runat="server" Text="Alterar" OnClick="BtnAlterar_Click" CssClass="popUpButtonsUnicos alterar"></asp:Button>
                <asp:Button ID="btnAtualizar" runat="server" Text="Atualizar" OnClick="BtnAtualizar_Click" CssClass="popUpButtonsUnicos atualizar"></asp:Button>
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" CssClass="popUpButtonsUnicos cancelar"></asp:Button>
            </div>
            <div style="width:100%; height:10px;"></div>
        </div>
       
    </ContentTemplate>
</asp:UpdatePanel>
