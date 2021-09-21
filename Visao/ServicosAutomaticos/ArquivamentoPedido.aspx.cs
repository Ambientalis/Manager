using System;
using System.Collections.Generic;
using System.Net.Mail;
using Modelo;
using Utilitarios;

public partial class ServicosAutomaticos_ArquivamentoPedido : System.Web.UI.Page
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["idConfig"] = "0";
            Transacao.Instance.Abrir();
            this.AtualizarStatus();
            lblResult.Text = "Pedidos Arquivados com Sucesso!";
        }
        catch (Exception ex)
        {
            lblResult.Text = "Erro ao enviar aviso de vencimento de OS!";
            Email mail = new Email();
            mail.Assunto = "ERRO de email - renovar os automaticamente (" + DateTime.Now + ")";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + " - " + ex.InnerException;
            mail.EmailsDestino.Add("hugo@c2ti.com.br");
            mail.EnviarAutenticado("Erro email vencimento de OS", null, null, 587, false);
        }
        finally
        {
            Transacao.Instance.Fechar(ref msg);
        }
    }

    private void AtualizarStatus()
    {
        Pedido.PostCancellStatus();
    }
}