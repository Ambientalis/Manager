using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class ServicosAutomaticos_NotificacoesDeVisitas : System.Web.UI.Page
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        List<String> IpsPermitidos = new List<string>() {
            "162.214.71.39",
            "162.214.71.37",
            "177.223.225.128"
        };
        if (IpsPermitidos.Contains(Request.ServerVariables["remote_addr"]))
        {
            try
            {
                Session["idConfig"] = "0";
                Transacao.Instance.Abrir();
                this.EnviarAvisoNotificacoesVisitas();

                lblResult.Text = "Avisos de notificações de visitas enviados com sucesso!";
            }
            catch (Exception ex)
            {
                lblResult.Text = "Erro ao enviar aviso de notificações de visita!";
                Email mail = new Email();
                mail.Assunto = "ERRO de email - renovar os automaticamente (" + DateTime.Now + ")";
                mail.BodyHtml = true;
                mail.Mensagem = "ERRO: " + ex.Message + " - " + ex.InnerException;
                mail.EmailsDestino.Add("hugo@c2ti.com.br");
                mail.EnviarAutenticado("Erro email notificação de visita", null, null, 587, false);
            }
            finally
            {
                Transacao.Instance.Fechar(ref msg);
            }
        }
        else
        {
            lblResult.Text = Request.ServerVariables["remote_addr"];
        }
    }

    private void EnviarAvisoNotificacoesVisitas()
    {
        IList<Visita> visitas = Visita.ConsultarVisitasNosProximosDias(30);

        if (visitas != null && visitas.Count > 0)
        {
            DateTime dataHoje = DateTime.Now.ToMinHourOfDay();

            foreach (Visita visita in visitas)
            {

                if (visita.DataInicio.ToMinHourOfDay() == dataHoje.AddDays(1))
                    this.EnviarEmailAvisoVisita(1, visita);

                if (visita.DataInicio.ToMinHourOfDay() == dataHoje.AddDays(5))
                    this.EnviarEmailAvisoVisita(5, visita);

                if (visita.DataInicio.ToMinHourOfDay() == dataHoje.AddDays(15))
                    this.EnviarEmailAvisoVisita(15, visita);

                if (visita.DataInicio.ToMinHourOfDay() == dataHoje.AddDays(30))
                    this.EnviarEmailAvisoVisita(30, visita);
            }
        }
    }

    private void EnviarEmailAvisoVisita(int diasPraVisita, Visita visita)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Notificação de Visita";
        mail.BodyHtml = true;

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Notificação de Visita
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        Há uma visita agendada no Sistema Ambientalis Manager para o cliente " + visita.GetNomeCliente + @" para o dia " + visita.DataInicio.ToShortDateString() + @", com as seguintes especificações:<br /><br />        
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Início:</strong>
                    </div>
                    <div>
                        " + visita.DataInicio.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Fim:</strong>
                    </div>
                    <div>
                        " + visita.DataFim.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>
                    
                </td>                
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Visitante:</strong>
                    </div>
                    <div>
                        " + visita.Visitante.NomeRazaoSocial + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Ordem de Serviço:</strong>
                    </div>
                    <div>
                        " + (visita.OrdemServico != null ? visita.OrdemServico.Codigo + " - " + visita.OrdemServico.Data.ToShortDateString() + " - " + visita.OrdemServico.GetDescricaoDepartamento + " - " + visita.OrdemServico.GetDescricaoSetor : "") + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Pedido:</strong>
                    </div>
                    <div>
                        " + (visita.OrdemServico != null && visita.OrdemServico.Pedido != null ? visita.OrdemServico.Pedido.Codigo + " - " + visita.OrdemServico.Pedido.Data.ToShortDateString() + " - " + visita.OrdemServico.Pedido.GetDescricaoTipo + " - " + visita.OrdemServico.Pedido.GetNomeCliente : "") + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + visita.Descricao + @"
                    </div>                    
                </td>
            </tr>            
        </table>              
    </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Notificação de Visita", conteudoemail);

        //funções do Responsavel da OS
        Funcionario responsavel = visita.OrdemServico.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        //funções do Responsavel da Visita
        Funcionario visitante = visita.Visitante;

        if (visitante != null && visitante.EmailCorporativo != null && visitante.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(visitante.EmailCorporativo)))
                mail.AdicionarDestinatario(visitante.EmailCorporativo);
        }

        //Adicionando e-mails do cliente aos destinatarios
        Cliente cliente = visita.GetCliente;

        if (cliente != null)
        {
            if (cliente.Email.IsNotNullOrEmpty() && cliente.EmailRecebeNotificacoes)
                mail.AdicionarDestinatario(cliente.Email);

            if (cliente.Contatos != null && cliente.Contatos.Count > 0)
            {
                //adicionando e-mail do primeiro contato
                if (cliente.Contatos[0] != null && cliente.Contatos[0].Email.IsNotNullOrEmpty() && cliente.Contatos[0].RecebeNotificacoes && !mail.EmailsDestino.Contains(new MailAddress(cliente.Contatos[0].Email)))
                    mail.AdicionarDestinatario(cliente.Contatos[0].Email);

                //adicionando e-mail do segundo contato
                if (cliente.Contatos[1] != null && cliente.Contatos[1].Email.IsNotNullOrEmpty() && cliente.Contatos[1].RecebeNotificacoes && !mail.EmailsDestino.Contains(new MailAddress(cliente.Contatos[1].Email)))
                    mail.AdicionarDestinatario(cliente.Contatos[1].Email);

                //adicionando e-mail do terceiro contato
                if (cliente.Contatos[2] != null && cliente.Contatos[2].Email.IsNotNullOrEmpty() && cliente.Contatos[2].RecebeNotificacoes && !mail.EmailsDestino.Contains(new MailAddress(cliente.Contatos[2].Email)))
                    mail.AdicionarDestinatario(cliente.Contatos[2].Email);
            }
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Notificação de visita", null, null, 587, false);
    }
}