using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class AceiteVisita_AceiteVisita : System.Web.UI.Page
{
    private Msg msg = new Msg();
    private Funcionario FuncionarioLogado
    {
        get
        {
            return (Funcionario)Session["usuario_logado"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        if (!Request.Browser.Crawler && !IsPostBack)
            try
            {
                Session.Clear();
                this.SetIdConfig();

                hfIdVisita.Value = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("idVisita", this.Request));
                hfEmailQueRespondeu.Value = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("emailAceitou", this.Request));

                this.IniciarSessaoIdConfig();

                Transacao.Instance.Abrir();

                Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

                if (visita != null)
                {
                    if (visita.Aceita)
                        this.CarregarDadosRespostaSim(visita);
                    else
                        if (visita.EmailAceitou.IsNotNullOrEmpty())
                        {
                            this.CarregarDadosRespostaNao(visita);
                        }
                        else
                        {
                            pergunta.Visible = true;
                            resposta_sim.Visible = false;
                            resposta_nao.Visible = false;
                            motivo_negacao.Visible = false;
                        }
                }

            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Transacao.Instance.Fechar(ref msg);
                C2MessageBox.Show(msg);
            }
    }

    #region _________________ Métodos _________________

    private void SetIdConfig()
    {
        Session["idConfig"] = ConfigurationManager.AppSettings["idConfig"];
    }

    private bool IniciarSessaoIdConfig()
    {
        if (Session["idConfig"] != null && Session["idConfig"].ToString() != string.Empty)
            return true;
        if (Session["idConfig"] == null)
        {
            msg.CriarMensagem("Não foi possível acessar o sistema. Feche o navegador de internet e tente novamente.", "Informação", MsgIcons.Erro);
            return false;
        }
        return true;
    }

    private void CarregarDadosRespostaSim(Visita visita)
    {
        lblEmailAceitouSim.Text = visita.EmailAceitou;
        lblVisitanteSim.Text = visita.GetNomeResponsavel;
        lblClienteSim.Text = visita.GetNomeCliente;
        lblDataInicioSim.Text = visita.GetDataInicio;
        lblDataFimSim.Text = visita.GetDataFim;

        pergunta.Visible = false;
        resposta_sim.Visible = true;
        resposta_nao.Visible = false;
        motivo_negacao.Visible = false;
    }

    private void CarregarDadosRespostaNao(Visita visita)
    {
        lblEmailAceitouNao.Text = visita.EmailAceitou;

        lblVisitanteNao.Text = visita.GetNomeResponsavel;
        lblClienteNao.Text = visita.GetNomeCliente;
        lblDataInicioNao.Text = visita.GetDataInicio;
        lblDataFimNao.Text = visita.GetDataFim;
        lblMotivoNegacaoNao.Text = visita.MotivoNaoAceite;

        pergunta.Visible = false;
        resposta_sim.Visible = false;
        resposta_nao.Visible = true;
        motivo_negacao.Visible = false;
    }

    private void EnviarEmailAceiteVisitaDeOSParaOResponsavelDaOS(Visita visita)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Resposta à Solicitação de Visita";
        mail.BodyHtml = true;

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Resposta à Solicitação de Visita
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        O e-mail " + visita.EmailAceitou + (visita.Aceita ? ", aceitou" : ", negou") + @" a realização da visita referente à OS " + visita.OrdemServico.Codigo + @" sob sua responsabilidade, conforme os dados abaixo:<br /><br />        
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Cliente:</strong>
                    </div>
                    <div>
                        " + visita.GetNomeCliente + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Objetivo da Visita:</strong>
                    </div>
                    <div>
                        " + visita.GetNomeTipoVisita + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Finalidade da Visita:</strong>
                    </div>
                    <div style='margin-bottom:10px;'>
                        " + visita.Descricao + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' style='width:30%;'>
                    <div>
                        <strong>Início:</strong>
                    </div>
                    <div>
                        " + visita.DataInicio.ToString("dd/MM/yyyy") + @"
                    </div>                    
                </td>
                <td align='left' style='width:20%;'>
                    <div>
                        <strong>Horário:</strong>
                    </div>
                    <div>
                        " + visita.DataInicio.ToString("HH:mm") + @"
                    </div>
                </td>
                <td align='left' style='width:30%;'>
                    <div>
                        <strong>Fim:</strong>
                    </div>
                    <div>
                        " + visita.DataFim.ToString("dd/MM/yyyy") + @"
                    </div>
                    
                </td>
                <td align='left' style='width:20%;'>
                    <div>
                        <strong>Horário:</strong>
                    </div>
                    <div>
                        " + visita.DataFim.ToString("HH:mm") + @"
                    </div>
                </td>
            </tr>
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Responsável pela visita:</strong>
                    </div>
                    <div>
                        " + visita.GetDadosDoVisitante + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Pedido:</strong>
                    </div>
                    <div>
                        " + visita.OrdemServico.Pedido.Codigo + @"
                    </div>                    
                </td>
            </tr>
            
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Ordem de Serviço:</strong>
                    </div>
                    <div>
                        " + visita.OrdemServico.Codigo + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Resposta à solicitação de visita:</strong>
                    </div>
                    <div>
                        " + (visita.Aceita ? "Aceita" : "Negada") + @"
                    </div>                    
                </td>
            </tr>
            " + (visita.Aceita ? "" : "<tr><td align='left' colspan='4'><div style='margin-top:10px;'><strong>Motivo do não aceite:</strong></div><div>" + visita.MotivoNaoAceite + @"</div></td></tr>") + @"            
            <tr>
                <td align='center' colspan='4'>
                    <div style='margin-top:20px;'>
                        Observação: Favor NÃO responder a este e-mail.
                    </div>                                        
                </td>
            </tr>            
        </table>           
    </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Resposta à Solicitação de Visita", conteudoemail.ToString());

        Funcionario responsavel = visita.OrdemServico.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Resposta à solicitação de visita", this.FuncionarioLogado, null, 587, false);
    }

    #endregion

    #region _________________ Eventos _________________

    protected void btnAceitarVisita_Click(object sender, EventArgs e)
    {
        try
        {
            Transacao.Instance.Abrir();

            Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

            if (visita != null)
            {
                visita.EmailAceitou = lblEmailAceitouSim.Text = hfEmailQueRespondeu.Value;
                visita.Aceita = true;
                visita = visita.Salvar();

                if (visita.OrdemServico != null && visita.OrdemServico.Id > 0)
                    this.EnviarEmailAceiteVisitaDeOSParaOResponsavelDaOS(visita);

                lblVisitanteSim.Text = visita.GetNomeResponsavel;
                lblClienteSim.Text = visita.GetNomeCliente;
                lblDataInicioSim.Text = visita.GetDataInicio;
                lblDataFimSim.Text = visita.GetDataFim;

                pergunta.Visible = false;
                resposta_sim.Visible = true;
                resposta_nao.Visible = false;
                motivo_negacao.Visible = false;

            }

        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Transacao.Instance.Fechar(ref msg);
            C2MessageBox.Show(msg);
        }

    }

    protected void btnNegarVisita_Click(object sender, EventArgs e)
    {
        pergunta.Visible = false;
        resposta_sim.Visible = false;
        resposta_nao.Visible = false;
        motivo_negacao.Visible = true;
        tbxMotivoNegacao.Text = "";
    }

    protected void btnSalvarNegacaoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (!tbxMotivoNegacao.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Informe o motivo de negação da visita para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
            }

            Transacao.Instance.Abrir();

            Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

            if (visita != null)
            {

                visita.EmailAceitou = lblEmailAceitouNao.Text = hfEmailQueRespondeu.Value;
                visita.Aceita = false;
                visita.MotivoNaoAceite = lblMotivoNegacaoNao.Text = tbxMotivoNegacao.Text;
                visita = visita.Salvar();

                if (visita.OrdemServico != null && visita.OrdemServico.Id > 0)
                    this.EnviarEmailAceiteVisitaDeOSParaOResponsavelDaOS(visita);

                lblVisitanteNao.Text = visita.GetNomeResponsavel;
                lblClienteNao.Text = visita.GetNomeCliente;
                lblDataInicioNao.Text = visita.GetDataInicio;
                lblDataFimNao.Text = visita.GetDataFim;

                pergunta.Visible = false;
                resposta_sim.Visible = false;
                resposta_nao.Visible = true;
                motivo_negacao.Visible = false;

            }

        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Transacao.Instance.Fechar(ref msg);
            C2MessageBox.Show(msg);
        }
    }

    protected void btnCancelarNegacaoVisita_Click(object sender, EventArgs e)
    {
        pergunta.Visible = true;
        resposta_sim.Visible = false;
        resposta_nao.Visible = false;
        motivo_negacao.Visible = false;
        tbxMotivoNegacao.Text = "";
    }

    #endregion

}