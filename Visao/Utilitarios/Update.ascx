<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Update.ascx.cs" Inherits="Utilitarios_Update" %>
<style type="text/css">
.update_cont
{
    width:180px;
    height:70px;
    border-radius:5px;
    opacity:0.9;
    filter: alpha(opacity=90);
    background-color:#333;
    position:fixed;
    top:10px;
    right:10px;
    z-index:99999999;
}
</style>
<asp:UpdateProgress ID="UpsProgress" runat="server">
<ProgressTemplate>
    <div class="update_cont">
    <div style="height:auto; margin-top:5px; width:100%; text-align:center;">
        <img alt="load" src="../Utilitarios/loader.gif" /></div>
    <div style="width:100%; height:auto; margin-top:3px; color:White;font-family:Arial; font-size:9pt; text-align:center;">Aguarde...</div>
</div>
</ProgressTemplate>
</asp:UpdateProgress>

