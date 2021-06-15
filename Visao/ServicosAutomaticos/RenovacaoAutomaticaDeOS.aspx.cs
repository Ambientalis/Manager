using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class ServicosAutomaticos_RenovacaoAutomaticaDeOS : System.Web.UI.Page
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
                this.RenovarOSAutomaticamente();
                lblResult.Text = "OSs renovadas com sucesso!";
            }
            catch (Exception ex)
            {
                lblResult.Text = "Erro ao renovar OSs automaticamente!";
                Email mail = new Email();
                mail.Assunto = "ERRO de email - renovar os automaticamente (" + DateTime.Now + ")";
                mail.BodyHtml = true;
                mail.Mensagem = "ERRO: " + ex.Message + " - " + ex.InnerException;
                mail.EmailsDestino.Add("hugo@c2ti.com.br");
                mail.EnviarAutenticado("Erro email renovação de OS ", null, null, 587, false);
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

    private void RenovarOSAutomaticamente()
    {
        IList<OrdemServico> ordens = OrdemServico.ConsultarOdensQueSeraoRenovadasAutomaticamente();

        if (ordens != null && ordens.Count > 0)
        {
            foreach (OrdemServico ordem in ordens)
            {
                if (ordem.TipoRenovacao == OrdemServico.DIAS)
                {
                    if (ordem.Data.AddDays(ordem.PrazoRenovacao) == DateTime.Now.ToMinHourOfDay())
                        this.RenovarOS(ordem);
                }
                else if (ordem.TipoRenovacao == OrdemServico.MESES)
                {
                    if (ordem.Data.AddMonths(ordem.PrazoRenovacao) == DateTime.Now.ToMinHourOfDay())
                        this.RenovarOS(ordem);
                }

            }
        }
    }

    private void RenovarOS(OrdemServico ordem)
    {
        OrdemServico ordemNova = ordem.CloneObject<OrdemServico>();

        ordemNova.Id = 0;

        ordemNova.Codigo = OrdemServico.GerarNumeroCodigo();

        ordemNova.Data = DateTime.Now.ToMinHourOfDay();

        TipoOrdemServico tipo = ordem.Tipo;

        ordemNova.PrazoPadrao = ordem.TipoRenovacao == OrdemServico.DIAS ? ordemNova.Data.AddDays(tipo.PrazoPadrao) : ordemNova.Data.AddMonths(tipo.PrazoPadrao);

        if (ordem.PrazoLegal != SqlDate.MinValue)
            ordemNova.PrazoLegal = ordemNova.PrazoPadrao;

        if (ordem.PrazoDiretoria != SqlDate.MinValue)
            ordemNova.PrazoDiretoria = ordemNova.PrazoPadrao;

        ordemNova.Renovavel = true;
        ordemNova.PrazoRenovacao = ordem.PrazoRenovacao;
        ordemNova.TipoRenovacao = ordem.TipoRenovacao;
        ordemNova.JaRenovada = false;

        ordemNova = ordemNova.Salvar();

        ordem.JaRenovada = true;
        ordem = ordem.Salvar();

        this.EnviarEmailOSCadastradaParaOResponsavelEGestorDoDepartamento(ordemNova);

    }

    private void EnviarEmailOSCadastradaParaOResponsavelEGestorDoDepartamento(OrdemServico ordem)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Cadastro de Ordem de Serviço";
        mail.BodyHtml = true;

        String dadosOS = Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + ordem.Id.ToString());

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
            Cadastro de Ordem de Serviço
        </div>
         <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
            Uma Ordem de Serviço foi criada no Sistema Ambientalis Manager com as seguintes especificações:<br />       
        <div>
            <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
                <tr>
                    <td align='left'>
                        <strong>Número da OS:</strong> " + ordem.Codigo + @"
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <strong>Tipo de OS:</strong> " + ordem.Tipo.Nome + @"
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <strong>Data da OS:</strong> " + ordem.Data.ToShortDateString() + @"
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
                        <strong>Responsável:</strong> " + ordem.Responsavel.NomeRazaoSocial + @"
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <strong>Departamento:</strong> " + ordem.Setor.Departamento.Nome + @"
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <strong>Setor:</strong> " + ordem.Setor.Nome + @"
                    </td>
                </tr>
                <tr>
                    <td align='left'>
                        <strong>Descrição da OS:</strong> " + ordem.Descricao + @"
                    </td>
                </tr>                       
            </table> 
            <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:25px;'>
                <a href='http://ambientalismanager.com.br/OrdemServico/CadastroDeOS.aspx" + dadosOS + @"'>Clique aqui</a> para acessar esta ordem de serviço agora!</div>         
            </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Cadastro de Ordem de Serviço", conteudoemail);

        Funcionario responsavel = ordem.Responsavel;
        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        Departamento departamento = ordem.Setor.Departamento;

        //Adicionado os destinatarios para o email de acordo com as funcoes
        IList<Funcionario> funcionariosDiretorGestorDoDepartamento = Funcionario.ConsultarFuncionariosQueSaoGestoresDiretoresDoDepartamentoDaOS(departamento);

        funcionariosDiretorGestorDoDepartamento = funcionariosDiretorGestorDoDepartamento.Distinct().ToList();

        if (funcionariosDiretorGestorDoDepartamento != null && funcionariosDiretorGestorDoDepartamento.Count > 0)
        {
            foreach (Funcionario funcDiretorGestor in funcionariosDiretorGestorDoDepartamento)
            {
                if (funcDiretorGestor != null && funcDiretorGestor.EmailCorporativo != null && funcDiretorGestor.EmailCorporativo != "")
                {
                    if (!mail.EmailsDestino.Contains(new MailAddress(funcDiretorGestor.EmailCorporativo)))
                        mail.AdicionarDestinatario(funcDiretorGestor.EmailCorporativo);
                }
            }
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Renovação de OS", null, null, 587, false);
    }
}