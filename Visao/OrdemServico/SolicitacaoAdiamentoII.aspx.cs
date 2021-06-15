using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class OrdemServico_SolicitacaoAdiamento : PageBase
{
    Msg msg = new Msg();

    private Funcionario FuncionarioLogado
    {
        get
        {
            return (Funcionario)Session["usuario_logado"];
        }
    }

    private Funcao FuncaoLogada
    {
        get
        {
            return Session["funcao_logada"] == null ? null : (Funcao)Session["funcao_logada"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                string idSolicitacao = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("idSolicit", this.Request));

                if (idSolicitacao.ToInt32() > 0)
                {
                    hfIdSolicitacao.Value = idSolicitacao;
                    this.CarregarSolicitacaoAdiamento(idSolicitacao.ToInt32());
                }
                else
                {
                    msg.CriarMensagem("Solicitação de Adiamento de Prazo não foi carregada corretamente. Por favor, volte ao e-mail recebido e tente novamente.", "Erro", MsgIcons.Erro);
                    this.DesbilitarEsconderBotoes();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    #region _______________ Métodos _______________

    private void CarregarSolicitacaoAdiamento(int id)
    {
        SolicitacaoAdiamento solicitacao = SolicitacaoAdiamento.ConsultarPorId(id);

        if (solicitacao != null)
        {
            lblNumeroOS.Text = solicitacao.OrdemServico != null ? solicitacao.OrdemServico.Codigo : "";
            lblDataOS.Text = solicitacao.OrdemServico != null ? solicitacao.OrdemServico.Data.ToShortDateString() : "";
            lblDescricaoOS.Text = solicitacao.OrdemServico != null ? solicitacao.OrdemServico.Descricao : "";
            lblPedidoOS.Text = solicitacao.OrdemServico != null && solicitacao.OrdemServico.Pedido != null ? solicitacao.OrdemServico.Pedido.Codigo : "";
            lblClienteOS.Text = solicitacao.OrdemServico != null ? solicitacao.OrdemServico.GetNomeCliente : "";

            lblDataSolicitacao.Text = solicitacao.Data.ToShortDateString();


            tbxPrazoPadraoAntigoOS.Text = solicitacao.PrazoPadraoAnterior != SqlDate.MinValue ? solicitacao.PrazoPadraoAnterior.ToShortDateString() : "";
            tbxPrazoLegalAntigoOS.Text = solicitacao.PrazoLegalAnterior != SqlDate.MinValue ? solicitacao.PrazoLegalAnterior.ToShortDateString() : "";
            tbxPrazoDiretoriaAntigoOS.Text = solicitacao.PrazoDiretoriaAnterior != SqlDate.MinValue ? solicitacao.PrazoDiretoriaAnterior.ToShortDateString() : "";

            tbxNovoPrazoPadraoOS.Text = solicitacao.PrazoPadraoNovo != SqlDate.MinValue ? solicitacao.PrazoPadraoNovo.ToShortDateString() : solicitacao.PrazoPadraoAnterior != SqlDate.MinValue ? solicitacao.PrazoPadraoAnterior.ToShortDateString() : "";
            tbxNovoPrazoLegalOS.Text = solicitacao.PrazoLegalNovo != SqlDate.MinValue ? solicitacao.PrazoLegalNovo.ToShortDateString() : solicitacao.PrazoLegalAnterior != SqlDate.MinValue ? solicitacao.PrazoLegalAnterior.ToShortDateString() : "";
            tbxNovoPrazoDiretoriaOS.Text = solicitacao.PrazoDiretoriaNovo != SqlDate.MinValue ? solicitacao.PrazoDiretoriaNovo.ToShortDateString() : solicitacao.PrazoDiretoriaAnterior != SqlDate.MinValue ? solicitacao.PrazoDiretoriaAnterior.ToShortDateString() : "";

            tbxObservacoesSolicitacaoAdiamento.Text = solicitacao.Observacoes;

            Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.FuncionarioLogado.Id, this.FuncaoLogada.Id);

            this.DesabilitarCamposPrazos();

            //Adiamento de prazo legal
            if (permissao.AdiaPrazoLegalOS == Permissao.TODOS)
                tbxNovoPrazoLegalOS.Enabled = true;

            if (permissao.AdiaPrazoLegalOS == Permissao.DEPARTAMENTO)
            {
                if (permissao.Funcao.Setor.Departamento.Id == solicitacao.OrdemServico.Setor.Departamento.Id)
                    tbxNovoPrazoLegalOS.Enabled = true;
            }

            if (permissao.AdiaPrazoLegalOS == Permissao.SETOR)
            {
                if (permissao.Funcao.Setor.Id == solicitacao.OrdemServico.Setor.Id)
                    tbxNovoPrazoLegalOS.Enabled = true;
            }

            if (permissao.AdiaPrazoLegalOS == Permissao.RESPONSAVEL)
            {
                if (this.FuncionarioLogado.Id == solicitacao.OrdemServico.Responsavel.Id)
                    tbxNovoPrazoLegalOS.Enabled = true;
            }


            //Adiamento de prazo de diretoria
            if (permissao.AdiaPrazoDiretoriaOS == Permissao.TODOS)
                tbxNovoPrazoDiretoriaOS.Enabled = true;

            if (permissao.AdiaPrazoDiretoriaOS == Permissao.DEPARTAMENTO)
            {
                if (permissao.Funcao.Setor.Departamento.Id == solicitacao.OrdemServico.Setor.Departamento.Id)
                    tbxNovoPrazoDiretoriaOS.Enabled = true;
            }

            if (permissao.AdiaPrazoDiretoriaOS == Permissao.SETOR)
            {
                if (permissao.Funcao.Setor.Id == solicitacao.OrdemServico.Setor.Id)
                    tbxNovoPrazoDiretoriaOS.Enabled = true;
            }

            if (permissao.AdiaPrazoDiretoriaOS == Permissao.RESPONSAVEL)
            {
                if (this.FuncionarioLogado.Id == solicitacao.OrdemServico.Responsavel.Id)
                    tbxNovoPrazoDiretoriaOS.Enabled = true;
            }


            if (solicitacao.Parecer == SolicitacaoAdiamento.ACEITA || solicitacao.Parecer == SolicitacaoAdiamento.NEGADA)
            {
                lblEstadoSolicitacao.Text = solicitacao.Parecer == SolicitacaoAdiamento.ACEITA ? "aceita" : "negada";

                lblUsuarioQueRespondeuSolicit.Text = solicitacao.UsuarioAdiou;
                lblDataRespostaSolicitacao.Text = solicitacao.DataResposta.ToShortDateString();
                solicitacao_ja_respondida.Visible = true;
                this.DesbilitarEsconderBotoes();
            }

            if (tbxNovoPrazoDiretoriaOS.Enabled == false && tbxNovoPrazoLegalOS.Enabled == false)
            {
                this.DesbilitarEsconderBotoes();
                msg.CriarMensagem("Você não possui permissão para adiar os prazos desta OS.", "Informação", MsgIcons.Informacao);

            }
        }
        else
        {
            this.DesbilitarEsconderBotoes();
            msg.CriarMensagem("Solicitação não encontrada! Por favor, refaça a solicitação", "Erro", MsgIcons.Alerta);
        }
    }

    private void DesabilitarCamposPrazos()
    {
        tbxNovoPrazoLegalOS.Enabled = tbxNovoPrazoDiretoriaOS.Enabled = false;
    }

    private void DesbilitarEsconderBotoes()
    {
        btnAdiarPrazo.Enabled = btnAdiarPrazo.Visible = btnNegarAdiamentoPrazo.Enabled = btnNegarAdiamentoPrazo.Visible = false;
    }

    private void NegarAdiamento()
    {
        SolicitacaoAdiamento solicitacao = SolicitacaoAdiamento.ConsultarPorId(hfIdSolicitacao.Value.ToInt32());

        OrdemServico ordem = OrdemServico.ConsultarPorId(solicitacao.OrdemServico.Id);

        solicitacao.DataResposta = DateTime.Now;
        solicitacao.Parecer = SolicitacaoAdiamento.NEGADA;
        solicitacao.UsuarioAdiou = this.FuncionarioLogado.NomeRazaoSocial;
        solicitacao.Observacoes = tbxObservacoesSolicitacaoAdiamento.Text;

        solicitacao = solicitacao.Salvar();

        //Incluindo a resposta da solicitação no detalhamento da OS
        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new List<Detalhamento>();

        Detalhamento detalhamento = new Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";

        detalhamento.Conteudo += @"<br /><br /> Solicitação de Adiamento de Prazo da Ordem de Serviço nº " + ordem.Codigo + " foi negada em " + solicitacao.DataResposta.ToShortDateString() + @" pelo funcionário " + solicitacao.UsuarioAdiou + @" <br /> Observações:<br />" + solicitacao.Observacoes;

        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

        detalhamento = detalhamento.Salvar();

        ordem.Detalhamentos.Add(detalhamento);

        ordem = ordem.Salvar();

        msg.CriarMensagem("Solicitação de Adiamento de Prazo negada com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.DesbilitarEsconderBotoes();

        solicitacao_ja_respondida.Visible = true;

        lblEstadoSolicitacao.Text = "negada";
        lblUsuarioQueRespondeuSolicit.Text = solicitacao.UsuarioAdiou;
        lblDataRespostaSolicitacao.Text = solicitacao.DataResposta.ToShortDateString();

        this.EnviarEmailRespostaSolicitacaoParaOResponsavelDaOS(solicitacao);

    }

    private void AdiarPrazo()
    {
        SolicitacaoAdiamento solicitacao = SolicitacaoAdiamento.ConsultarPorId(hfIdSolicitacao.Value.ToInt32());

        OrdemServico ordem = OrdemServico.ConsultarPorId(solicitacao.OrdemServico.Id);

        if (tbxNovoPrazoLegalOS.Enabled && tbxNovoPrazoLegalOS.Text.IsNotNullOrEmpty())
            ordem.PrazoLegal = tbxNovoPrazoLegalOS.Text.ToDateTime();

        if (tbxNovoPrazoDiretoriaOS.Enabled && tbxNovoPrazoDiretoriaOS.Text.IsNotNullOrEmpty())
            ordem.PrazoDiretoria = tbxNovoPrazoDiretoriaOS.Text.ToDateTime();

        ordem = ordem.Salvar();

        solicitacao.PrazoLegalNovo = ordem.PrazoLegal;
        solicitacao.PrazoDiretoriaNovo = ordem.PrazoDiretoria;
        solicitacao.PrazoPadraoNovo = tbxNovoPrazoPadraoOS.Text.ToDateTime();

        solicitacao.DataResposta = DateTime.Now;
        solicitacao.Parecer = SolicitacaoAdiamento.ACEITA;
        solicitacao.UsuarioAdiou = this.FuncionarioLogado.NomeRazaoSocial;
        solicitacao.Observacoes = tbxObservacoesSolicitacaoAdiamento.Text;

        solicitacao = solicitacao.Salvar();

        //Incluindo a resposta da solicitação no detalhamento da OS
        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new List<Detalhamento>();

        Detalhamento detalhamento = new Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";

        detalhamento.Conteudo += @"<br /><br /> Os Prazos da Ordem de Serviço nº " + ordem.Codigo + " foram adiados em " + solicitacao.DataResposta.ToShortDateString() + @" pelo funcionário " + solicitacao.UsuarioAdiou + @" <br /> Observações:<br />" + solicitacao.Observacoes;

        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

        detalhamento = detalhamento.Salvar();

        ordem.Detalhamentos.Add(detalhamento);

        ordem = ordem.Salvar();

        msg.CriarMensagem("Solicitação de Adiamento de Prazo adiada com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.EnviarEmailRespostaSolicitacaoParaOResponsavelDaOS(solicitacao);

        this.DesbilitarEsconderBotoes();

        solicitacao_ja_respondida.Visible = true;

        lblEstadoSolicitacao.Text = "adiada";
        lblUsuarioQueRespondeuSolicit.Text = solicitacao.UsuarioAdiou;
        lblDataRespostaSolicitacao.Text = solicitacao.DataResposta.ToShortDateString();

    }

    private void EnviarEmailRespostaSolicitacaoParaOResponsavelDaOS(SolicitacaoAdiamento solicitacao)
    {
        String tipoSolicitacao = solicitacao.Parecer == SolicitacaoAdiamento.ACEITA ? "Adiada" : "Negada";
        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Resposta solicitação adiamento", solicitacao.OrdemServico.Codigo, "Envio de e-mail = " + tipoSolicitacao);
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Resposta a Solicitação de Adiamento de Prazo de OS";
        mail.BodyHtml = true;

        string conteudoemailNegado = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Resposta á
        Solicitação de Adiamento de Prazo de OS
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A solicitação para adiamento de prazo da OS " + solicitacao.OrdemServico.Codigo + @", sob responsabilidade do funcionário " + solicitacao.OrdemServico.Responsavel.NomeRazaoSocial + @", foi negada em " + solicitacao.DataResposta.ToShortDateString() + @" pelo usuário " + solicitacao.UsuarioAdiou + @"
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
            <tr>
                <td align='left'>
                    <div>
                        <strong>Observações:</strong>
                    </div>
                    <div>
                        " + solicitacao.Observacoes + @"
                    </div>
                    
                </td>
            </tr>            
        </table>        
    </div>";


        string conteudoemailAceito = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Resposta á
        Solicitação de Adiamento de Prazo de OS
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A OS " + solicitacao.OrdemServico.Codigo + @", sob responsabilidade do funcionário " + solicitacao.OrdemServico.Responsavel.NomeRazaoSocial + @", teve os seus prazos adiados em " + solicitacao.DataResposta.ToShortDateString() + @" pelo usuário " + solicitacao.UsuarioAdiou + @".<br /><br />
        Os novos prazos da OS passam a ser:
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
            <tr>                
                <td style='width:33%;'>
                    <div>
                        <strong>Prazo Padrão:</strong>
                    </div>
                    <div>
                        " + (solicitacao.PrazoPadraoNovo != SqlDate.MinValue ? solicitacao.PrazoPadraoNovo.ToShortDateString() : "") + @"
                    </div>
                </td>
                <td style='width:33%;'>
                    <div>
                        <strong>Prazo Legal:</strong>
                    </div>
                    <div>
                        " + (solicitacao.PrazoLegalNovo != SqlDate.MinValue ? solicitacao.PrazoLegalNovo.ToShortDateString() : "") + @"
                    </div>
                </td>
                <td style='width:33%;'>
                    <div>
                        <strong>Prazo de Diretoria:</strong>
                    </div>
                    <div>
                        " + (solicitacao.PrazoDiretoriaNovo != SqlDate.MinValue ? solicitacao.PrazoDiretoriaNovo.ToShortDateString() : "") + @"
                    </div>
                </td>
            </tr>
            <tr>
                <td align='left' colspan='3'>
                    <div style='margin-top:20px;'>
                        <strong>Observações:</strong>
                    </div>
                    <div>
                        " + solicitacao.Observacoes + @"
                    </div>                    
                </td>
            </tr>            
        </table>        
    </div>";

        log.adicionarLog("Carregando a mensagem do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Resposta a Solicitação de Adiamento de Prazo de OS", solicitacao.Parecer == SolicitacaoAdiamento.ACEITA ? conteudoemailAceito.ToString() : conteudoemailNegado);

        Funcionario responsavel = solicitacao.OrdemServico.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            log.adicionarLog("Adicionando o responsável da OS");
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Resposta solicitação adiamento", this.FuncionarioLogado, solicitacao.OrdemServico.Pedido, 587, false))
            {
                log.adicionarLog("E-mail enviado");
            }
            else
            {
                log.adicionarLog("Erro ao enviar e-mail. " + mail.Erro);
                msg.CriarMensagem("Erro ao enviar e-mail. " + mail.Erro, "Erro", MsgIcons.Erro);
            }
        }
    }

    #endregion

    #region _______________ Eventos _______________

    protected void btnNegarAdiamentoPrazo_Click(object sender, EventArgs e)
    {
        try
        {
            this.NegarAdiamento();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void btnAdiarPrazo_Click(object sender, EventArgs e)
    {
        try
        {
            this.AdiarPrazo();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    #endregion

}