using System;
using System.Collections.Generic;
using System.Net.Mail;
using Modelo;
using Utilitarios;

public partial class ServicosAutomaticos_AvisosDeVencimentosDeOS : System.Web.UI.Page
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        //List<String> IpsPermitidos = new List<string>() {
        //    "162.214.71.39",
        //    "162.214.71.37",
        //    "177.223.225.128"
        //};
        //if (IpsPermitidos.Contains(Request.ServerVariables["remote_addr"]))
        //{
        //    lblResult.Text = Request.ServerVariables["remote_addr"];
        //    return;
        //}
        try
        {
            Session["idConfig"] = "0";
            Transacao.Instance.Abrir();
            this.EnviarAvisoVencimentosOS();
            this.EnviarAvisosOSsVencidasOntemAindaAbertas();
            lblResult.Text = "Avisos de vencimentos enviados com sucesso!";
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

    private void EnviarAvisoVencimentosOS()
    {
        IList<OrdemServico> ordensAVencer = OrdemServico.ConsultarOrdensComVencimentoNosProximosDias(30);
        if (ordensAVencer != null && ordensAVencer.Count > 0)
        {
            DateTime dataHoje = DateTime.Now.ToMaxHourOfDay();
            int index = 0;
            foreach (OrdemServico ordem in ordensAVencer)
            {
                try
                {
                    if (ordem.GetDataVencimento == dataHoje)
                        this.EnviarEmailAvisoOs(0, ordem);

                    if (ordem.GetDataVencimento == dataHoje.AddDays(1))
                        this.EnviarEmailAvisoOs(1, ordem);

                    if (ordem.GetDataVencimento == dataHoje.AddDays(5))
                        this.EnviarEmailAvisoOs(5, ordem);

                    if (ordem.GetDataVencimento == dataHoje.AddDays(15))
                        this.EnviarEmailAvisoOs(15, ordem);

                    if (ordem.GetDataVencimento == dataHoje.AddDays(30))
                        this.EnviarEmailAvisoOs(30, ordem);
                    index++;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    private void EnviarEmailAvisoOs(int qtdDiasProVencimento, OrdemServico ordem)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Aviso de Vencimento de Prazo de Ordem de Serviço";
        mail.BodyHtml = true;

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Aviso de Vencimento de Prazo de Ordem de Serviço
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A OS " + ordem.Codigo + @", sob responsabilidade do funcionário " + (ordem.Responsavel != null ? ordem.Responsavel.NomeRazaoSocial : "N/I") + @", vence " + (qtdDiasProVencimento > 0 ? "daqui a " + qtdDiasProVencimento + " dia(s) (" + ordem.GetDataVencimento.ToShortDateString() + ")" : "hoje (" + ordem.GetDataVencimento.ToShortDateString() + ")") + @".<br /><br />
        Detalhes da Ordem de Serviço:             
    </div>
    <div>
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
            <tr>
                <td align='left'>
                    <strong>Número da OS:</strong> " + ordem.Codigo + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Data da OS:</strong> " + ordem.Data.ToShortDateString() + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Descrição da OS:</strong> " + ordem.Descricao + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Número do Pedido:</strong> " + ordem.Pedido.Codigo + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Cliente:</strong> " + ordem.Pedido.Cliente.NomeRazaoSocial + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Responsável:</strong> " + (ordem.Responsavel != null ? ordem.Responsavel.NomeRazaoSocial : "N/I") + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong style='color:red;'>Data de Vencimento:</strong> " + ordem.GetDataVencimento.ToShortDateString() + @"
                </td>
            </tr>            
        </table>        
    </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Aviso de Vencimento de Prazo de Ordem de Serviço", conteudoemail);

        Funcionario responsavel = ordem.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Aviso de vencimento de prazo de OS", null, null, 587, false);
    }

    private void EnviarAvisosOSsVencidasOntemAindaAbertas()
    {
        IList<OrdemServico> ordensVencidasOntem = OrdemServico.ConsultarOrdensVencidasOntemAindaAbertas();

        if (ordensVencidasOntem != null && ordensVencidasOntem.Count > 0)
        {
            DateTime dataOntem = DateTime.Now.ToMinHourOfDay().AddDays(-1);

            foreach (OrdemServico ordem in ordensVencidasOntem)
            {
                if (ordem.GetDataVencimento == dataOntem)
                {
                    Email mail = new Email();
                    mail.Assunto = "Ambientalis Manager - Aviso de Perda de Prazo de Ordem de Serviço";
                    mail.BodyHtml = true;

                    string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Aviso de Perda de Prazo de Ordem de Serviço
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A OS" + ordem.Codigo + @", sob responsabilidade do funcionário " + (ordem.Responsavel != null ? ordem.Responsavel.NomeRazaoSocial : "N/I") + @", venceu ontem (" + ordem.GetDataVencimento.ToShortDateString() + @"), e seu prazo foi perdido.<br /><br />
        Detalhes da Ordem de Serviço:             
    </div>
    <div>
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
            <tr>
                <td align='left'>
                    <strong>Número da OS:</strong> " + ordem.Codigo + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Data da OS:</strong> " + ordem.Data.ToShortDateString() + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Descrição da OS:</strong> " + ordem.Descricao + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Número do Pedido:</strong> " + ordem.Pedido.Codigo + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Cliente:</strong> " + ordem.Pedido.Cliente.NomeRazaoSocial + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Responsável:</strong> " + (ordem.Responsavel != null ? ordem.Responsavel.NomeRazaoSocial : "N/I") + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong style='color:red;'>Data de Vencimento:</strong> " + ordem.GetDataVencimento.ToShortDateString() + @"
                </td>
            </tr>            
        </table>        
    </div>";

                    mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Aviso de Perda de Prazo de Ordem de Serviço", conteudoemail);

                    Funcionario responsavel = ordem.Responsavel;

                    if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
                    {
                        if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                            mail.AdicionarDestinatario(responsavel.EmailCorporativo);
                    }

                    if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
                    {
                        mail.EnviarAutenticado("Aviso perda de prazo de OS", null, null, 587, false);
                    }
                }

            }
        }
    }
}