using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Reservas_CadastroDeReserva : PageBase
{
    private Msg msg = new Msg();

    private Funcionario FuncionarioLogado
    {
        get
        {
            return (Funcionario)Session["usuario_logado"];
        }
    }

    private string EmailDoSolicitanteDeReserva
    {
        get
        {
            return (string)Session["EmailDoSolicitanteDeReserva"];
        }
        set { Session["EmailDoSolicitanteDeReserva"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
                this.NovaReserva();

                string idReserva = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                this.EmailDoSolicitanteDeReserva = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("emailSolicitante", this.Request));

                if (idReserva.ToInt32() > 0)
                {
                    this.CarregarReserva(idReserva.ToInt32());
                }

                else
                {
                    //Nova reserva
                    if (this.UsuarioLogadoTemPendenciasEmReservas())   //com pendencia
                    {
                        this.DesabilitarBotoes();
                        pendencia_boletim.Visible = true;
                        btnAprovar.Enabled = btnAprovar.Visible = false;
                    }
                    else    // sem pendencia
                    {
                        pendencia_boletim.Visible = false;
                        btnSolicitarReserva.Enabled = btnSolicitarReserva.Visible = btnSalvar.Enabled = btnSalvar.Visible = btnAprovar.Enabled = btnAprovar.Visible = btnEncerrar.Enabled = btnEncerrar.Visible = false;
                    }
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

    private void DasabilitarTodosOSBotoesParaReservaDeVisita()
    {
        btnAprovar.Enabled = btnAprovar.Visible = btnSolicitarReserva.Enabled = btnSolicitarReserva.Visible = btnSalvar.Enabled = btnSalvar.Visible = btnExcluir.Enabled = btnExcluir.Visible =
            pendencia_boletim.Visible = btnEncerrar.Enabled = btnEncerrar.Visible = false;
    }

    private bool UsuarioLogadoTemPendenciasEmReservas()
    {
        return Reserva.ConsultarReservasComBoletimPendenteDoUsuario(this.FuncionarioLogado.Id, DateTime.Now.ToMaxHourOfDay());
    }

    #region _______________ Métodos _______________

    private void CarregarCampos()
    {
        this.CarregarVeiculos();
        this.CarregarTiposReservas();
        this.CarregarResponsaveis();
    }

    private void CarregarVeiculos()
    {
        ddlVeiculo.Items.Clear();

        IList<Veiculo> veiculos = Veiculo.ConsultarTodosOrdemAlfabetica();

        if (veiculos != null && veiculos.Count > 0)
        {
            foreach (Veiculo veiculo in veiculos)
            {
                ddlVeiculo.Items.Add(new ListItem(veiculo.Descricao + " - " + veiculo.Placa, veiculo.Id.ToString()));
            }
        }

        ddlVeiculo.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarTiposReservas()
    {
        ddlTipoReserva.Items.Clear();

        ddlTipoReserva.DataValueField = "Id";
        ddlTipoReserva.DataTextField = "Descricao";

        IList<TipoReservaVeiculo> tiposReserva = TipoReservaVeiculo.ConsultarTodosQueNaoForemDeVisitaOrdemAlfabetica();

        ddlTipoReserva.DataSource = tiposReserva != null ? tiposReserva : new List<TipoReservaVeiculo>();
        ddlTipoReserva.DataBind();

        ddlTipoReserva.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarResponsaveis()
    {
        ddlResponsavel.Items.Clear();

        ddlResponsavel.DataValueField = "Id";
        ddlResponsavel.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlResponsavel.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlResponsavel.DataBind();

        ddlResponsavel.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarReserva(int id)
    {
        Reserva reserva = Reserva.ConsultarPorId(id);

        if (reserva != null)
        {
            btnSolicitarReserva.Enabled = btnSolicitarReserva.Visible = btnSalvar.Enabled = btnSalvar.Visible = btnAprovar.Enabled = btnAprovar.Visible = btnEncerrar.Enabled = btnEncerrar.Visible = false;

            hfId.Value = reserva.Id.ToString();
            tbxDescricao.Text = reserva.Descricao;

            Veiculo veiculo = reserva.Veiculo;

            ddlVeiculo.SelectedValue = veiculo != null ? veiculo.Id.ToString() : "0";
            tbxDataInicio.Text = reserva.DataInicio.ToShortDateString();
            tbxDataFim.Text = reserva.DataFim.ToShortDateString();

            btnNovaOcorrencia.Enabled = btnNovaOcorrencia.Visible = false;
            //se o usuario logado é o responsavel da OS ou se o usuario logado é o gestor do veículo ele pode alterar os dados da OS.
            if ((veiculo != null && veiculo.Gestor != null && veiculo.Gestor.Id == this.FuncionarioLogado.Id) || (reserva.Responsavel != null && reserva.Responsavel.Id == this.FuncionarioLogado.Id))
            {
                btnSalvar.Visible = btnSalvar.Enabled = btnExcluir.Visible = btnExcluir.Enabled = true;

                if (veiculo != null && veiculo.Gestor != null && veiculo.Gestor.Id == this.FuncionarioLogado.Id)
                {
                    tbxQuilometragem.Enabled = tbxConsumo.Enabled = false;
                    btnNovaOcorrencia.Enabled = btnNovaOcorrencia.Visible = ddlResponsavel.Enabled = true;
                }

                if (reserva.Responsavel != null && reserva.Responsavel.Id == this.FuncionarioLogado.Id)
                {
                    tbxQuilometragem.Enabled = tbxConsumo.Enabled = btnNovaOcorrencia.Enabled = btnNovaOcorrencia.Visible = true;
                    btnEncerrar.Visible = btnEncerrar.Enabled = true;
                }
            }

            tbxConsumo.Enabled = tbxQuilometragem.Enabled = pnlSalvarBoletim.Visible = (reserva.Status != Reserva.AGUARDANDO);
            if (reserva.Status == Reserva.AGUARDANDO)
            {
                btnAprovar.Visible = btnAprovar.Enabled = veiculo.Gestor != null && veiculo.Gestor.Id == this.FuncionarioLogado.Id;
                btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = veiculo.Gestor == null || veiculo.Gestor.Id != this.FuncionarioLogado.Id;
            }

            if (reserva.Status == Reserva.APROVADA)
            {
                btnAprovar.Visible = btnAprovar.Enabled = btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = false;
            }

            if (reserva.Status == Reserva.ENCERRADA)
            {
                btnAprovar.Visible = btnAprovar.Enabled = btnSalvar.Visible = btnSalvar.Enabled = btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = btnEncerrar.Visible = btnEncerrar.Enabled = btnExcluir.Visible = btnExcluir.Enabled = false;
            }

            tbxStatusReserva.Text = reserva.GetDescricaoStatus;

            if (reserva.DataInicio.ToString("HH:mm") == "00:00")
                ddlPeriodoInicioReserva.SelectedValue = "M";
            else
                ddlPeriodoInicioReserva.SelectedValue = "T";

            if (reserva.DataFim.ToString("HH:mm") == "11:59")
                ddlPeriodoFimReserva.SelectedValue = "M";
            else
                ddlPeriodoFimReserva.SelectedValue = "T";

            if (reserva.Visita != null && reserva.Visita.Id > 0)
            {
                ddlTipoReserva.Items.Clear();
                ddlTipoReserva.Items.Add(new ListItem("Reserva para Visita de OS", "1"));
            }
            else
            {
                ddlTipoReserva.SelectedValue = reserva.TipoReservaVeiculo != null ? reserva.TipoReservaVeiculo.Id.ToString() : "0";
            }

            ddlResponsavel.SelectedValue = reserva.Responsavel != null ? reserva.Responsavel.Id.ToString() : "0";


            tbxQuilometragem.Text = reserva.Quilometragem.ToString("N2");
            tbxConsumo.Text = reserva.Consumo.ToString("N2");

            this.CarregarGridOcorrencias();

        }
    }

    private void DesabilitarBotoes()
    {
        btnNovoCadastro.Enabled = btnNovoCadastro.Visible = btnSalvar.Enabled = btnSalvar.Visible = btnExcluir.Enabled = btnExcluir.Visible =
           btnSolicitarReserva.Enabled = btnSolicitarReserva.Visible = btnEncerrar.Enabled = btnEncerrar.Visible = false;
    }

    private void NovaReserva()
    {
        tbxDescricao.Text = tbxDataInicio.Text = tbxDataFim.Text = tbxQuilometragem.Text = tbxConsumo.Text = hfId.Value = "";

        tbxQuilometragem.Enabled = tbxConsumo.Enabled = false;

        ddlResponsavel.SelectedValue = this.FuncionarioLogado.Id.ToString();

        ddlResponsavel.Enabled = false;

        ddlPeriodoInicioReserva.SelectedValue = "M";
        ddlPeriodoFimReserva.Text = "T";

        tbxStatusReserva.Text = "Não definido...";

        this.CarregarCampos();
    }

    private void ExcluirReserva()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

        if (reserva != null)
        {
            if (reserva.Visita != null && reserva.Visita.Id > 0)
            {
                msg.CriarMensagem("Não é possível excluir reservas que sejam de visitas de Ordens de Serviço.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (reserva.Excluir())
            {
                this.EnviarEmailExclusaoReservaParaOGestor();
                msg.CriarMensagem("Reserva excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            }
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }

            Transacao.Instance.Recarregar();
            this.NovaReserva();
        }
    }

    private void SalvarReserva()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

        //Setando a data de inicio da reserva de acordo com o periodo escolhido
        DateTime dataInicioReserva = tbxDataInicio.Text.ToDateTime();

        if (ddlPeriodoInicioReserva.SelectedValue == "T")
            dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

        //Setando a data de fim da reserva de acordo com o periodo escolhido
        DateTime dataFimReserva = tbxDataFim.Text.ToDateTime();

        if (ddlPeriodoFimReserva.SelectedValue == "T")
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
        else
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

        if (dataInicioReserva > dataFimReserva)
        {
            msg.CriarMensagem("A data de início da reserva não deve ser maior que a data de fim da mesma.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (Reserva.ExisteAlgumaReservaParaOVeiculoNestePeriodoDeOutraVisita(ddlVeiculo.SelectedValue.ToInt32(), dataInicioReserva, dataFimReserva, reserva))
        {
            msg.CriarMensagem("Já existe uma reserva para este veículo aprovada neste período.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (reserva == null)
            reserva = new Reserva();

        reserva.Descricao = tbxDescricao.Text;

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());

        reserva.Veiculo = veiculo;

        if (hfId.Value.ToInt32() <= 0)
        {
            reserva.Status = veiculo != null && veiculo.Gestor != null && veiculo.Gestor.Id == this.FuncionarioLogado.Id ? Reserva.APROVADA : Reserva.AGUARDANDO;
        }

        if (reserva.Status == Reserva.APROVADA)
        {
            tbxStatusReserva.Text = "Aprovada";
            tbxQuilometragem.Enabled = tbxConsumo.Enabled = true;
        }

        reserva.DataInicio = dataInicioReserva;
        reserva.DataFim = dataFimReserva;
        reserva.TipoReservaVeiculo = TipoReservaVeiculo.ConsultarPorId(ddlTipoReserva.SelectedValue.ToInt32());
        reserva.Responsavel = Funcionario.ConsultarPorId(ddlResponsavel.SelectedValue.ToInt32());
        reserva.Quilometragem = tbxQuilometragem.Text.ToDecimal();
        reserva.Consumo = tbxConsumo.Text.ToDecimal();

        reserva = reserva.Salvar();

        hfId.Value = reserva.Id.ToString();
    }

    private void EnviarEmailCadastroAprovacaoDeReservaParaOGestor(DateTime dataInicioReserva, DateTime dataFimReserva)
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());
        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Solicitação de reversa de veículo", reserva.Id + " - " + reserva.Veiculo.Placa, "Início do processo");
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Solicitação de Reserva de Veículo";
        mail.BodyHtml = true;

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());

        Funcionario gestorVeiculo = veiculo.Gestor;

        String dadosVisita = Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + hfId.Value.ToString() + "&emailSolicitante=" + this.FuncionarioLogado.EmailCorporativo);

        log.adicionarLog("Carregando o conteúdo do e-mail");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Solicitação de Reserva de Veículo</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        Uma reserva para o Veículo " + veiculo.Descricao + " - " + veiculo.Placa + (gestorVeiculo != null ? ", gerido pelo usuário " + gestorVeiculo.NomeRazaoSocial : "") + @", foi realizada no Sistema Ambientalis Manager.<br /><br />  
        Dados da Reserva:      
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Início:</strong>
                    </div>
                    <div>
                        " + dataInicioReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Fim:</strong>
                    </div>
                    <div>
                        " + dataFimReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>                
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Veículo:</strong>
                    </div>
                    <div>
                        " + veiculo.Descricao + " - " + veiculo.Placa + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Tipo de Reserva:</strong>
                    </div>
                    <div>
                        " + ddlTipoReserva.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Responsável:</strong>
                    </div>
                    <div>
                        " + ddlResponsavel.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + tbxDescricao.Text + @"
                    </div>                    
                </td>
            </tr>                                                
        </table> 
        <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:10px;'>
            <a href='http://ambientalismanager.com.br/Reservas/CadastroDeReserva.aspx" + dadosVisita + @"'>Clique aqui</a> para responder a esta solicitação de reserva de veículo.</div>              
    </div>";

        log.adicionarLog("Carregando a mensagem completa do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Solicitação de Reserva de Veículo", conteudoemail);

        if (gestorVeiculo != null && gestorVeiculo.EmailCorporativo != "")
        {
            log.adicionarLog("Adicionando o gestor do veículo");
            mail.AdicionarDestinatario(gestorVeiculo.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Solicitação de reserva de veículo", this.FuncionarioLogado, null, 587, false))
                log.adicionarLog("E-mail enviado");
            else
            {
                log.adicionarLog("E-mail NÃO enviado. " + mail.Erro);
            }
        }
    }

    private void EnviarEmailExclusaoReservaParaOGestor()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());
        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Exclusão de reversa de veículo", reserva.Id + " - " + reserva.Veiculo.Placa, "Início do processo");

        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Exclusão de Reserva de Veículo";
        mail.BodyHtml = true;

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        Funcionario gestorVeiculo = veiculo.Gestor;

        DateTime dataInicioReserva = tbxDataInicio.Text.ToDateTime();

        if (ddlPeriodoInicioReserva.SelectedValue == "T")
            dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

        //Setando a data de fim da reserva de acordo com o periodo escolhido
        DateTime dataFimReserva = tbxDataFim.Text.ToDateTime();

        if (ddlPeriodoFimReserva.SelectedValue == "T")
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
        else
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

        log.adicionarLog("Carregando o conteúdo do e-mail");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Exclusão de Reserva de Veículo</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A reserva para o Veículo " + veiculo.Descricao + " - " + veiculo.Placa + (gestorVeiculo != null ? ", gerido pelo usuário " + gestorVeiculo.NomeRazaoSocial : "") + @", agendada para " + tbxDataInicio.Text + @", foi excluída do Sistema Ambientalis Manager.<br /><br />  
        Dados da Reserva:      
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Início:</strong>
                    </div>
                    <div>
                        " + dataInicioReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Fim:</strong>
                    </div>
                    <div>
                        " + dataFimReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>
                    
                </td>                
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Veículo:</strong>
                    </div>
                    <div>
                        " + veiculo.Descricao + " - " + veiculo.Placa + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Tipo de Reserva:</strong>
                    </div>
                    <div>
                        " + ddlTipoReserva.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Responsável:</strong>
                    </div>
                    <div>
                        " + ddlResponsavel.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + tbxDescricao.Text + @"
                    </div>                    
                </td>
            </tr>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div style='margin-top:10px;'>
                        <strong>Quilomêtragem (KM):</strong>
                    </div>
                    <div>
                        " + tbxQuilometragem.Text + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div style='margin-top:10px;'>
                        <strong>Consumo (Lts):</strong>
                    </div>
                    <div>
                        " + tbxConsumo.Text + @"
                    </div>                    
                </td>                
            </tr>                         
        </table>              
    </div>";

        log.adicionarLog("Carregando a mensagem completa do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Exclusão de Reserva de Veículo", conteudoemail);

        if (gestorVeiculo != null && gestorVeiculo.EmailCorporativo != "")
        {
            log.adicionarLog("Adicionando o e-mail do gestor do veículo");
            mail.AdicionarDestinatario(gestorVeiculo.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Exclusão de reserva de veículo", this.FuncionarioLogado, null, 587, false))
                log.adicionarLog("E-mail enviado com sucesso");
            else
                log.adicionarLog("E-mail NÃO enviado. " + mail.Erro);
        }
    }

    private void CarregarGridOcorrencias()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

        gdvOcorrencias.DataSource = reserva != null && reserva.Ocorrencias != null && reserva.Ocorrencias.Count > 0 ? reserva.Ocorrencias.OrderByDescending(x => x.Data).ToList() : new List<Ocorrencia>();
        gdvOcorrencias.DataBind();
    }

    private void NovaOcorrencia()
    {
        tbxDataOcorrencia.Text = tbxDescricaoOcorrencia.Text = hfIdOcorrencia.Value = "";

        this.CarregarTiposOcorrencia();
    }

    private void CarregarTiposOcorrencia()
    {
        ddlTipoOcorrencia.Items.Clear();

        ddlTipoOcorrencia.DataValueField = "Id";
        ddlTipoOcorrencia.DataTextField = "Descricao";

        IList<TipoOcorrenciaVeiculo> tiposOcorrencia = TipoOcorrenciaVeiculo.ConsultarTodosOrdemAlfabetica();

        ddlTipoOcorrencia.DataSource = tiposOcorrencia != null ? tiposOcorrencia : new List<TipoOcorrenciaVeiculo>();
        ddlTipoOcorrencia.DataBind();

        ddlTipoOcorrencia.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarOcorrencia(int id)
    {
        Ocorrencia ocorrencia = Ocorrencia.ConsultarPorId(id);

        if (ocorrencia != null)
        {
            hfIdOcorrencia.Value = ocorrencia.Id.ToString();
            tbxDataOcorrencia.Text = ocorrencia.Data.ToShortDateString();
            tbxDescricaoOcorrencia.Text = ocorrencia.Descricao;

            this.CarregarTiposOcorrencia();

            ddlTipoOcorrencia.SelectedValue = ocorrencia.TipoOcorrenciaVeiculo != null ? ocorrencia.TipoOcorrenciaVeiculo.Id.ToString() : "0";

            lblCadastroOcorrencia_ModalPopupExtender.Show();
        }
    }

    private void ExcluirOcorrencia(GridViewDeleteEventArgs e)
    {
        Ocorrencia ocorrencia = Ocorrencia.ConsultarPorId(gdvOcorrencias.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (ocorrencia != null)
        {
            if (ocorrencia.Excluir())
                msg.CriarMensagem("Ocorrência excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void SalvarOcorrencia()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

        Ocorrencia ocorrencia = Ocorrencia.ConsultarPorId(hfIdOcorrencia.Value.ToInt32());

        if (ocorrencia == null)
            ocorrencia = new Ocorrencia();

        ocorrencia.Data = tbxDataOcorrencia.Text.ToDateTime();
        ocorrencia.Descricao = tbxDescricaoOcorrencia.Text;
        ocorrencia.TipoOcorrenciaVeiculo = TipoOcorrenciaVeiculo.ConsultarPorId(ddlTipoOcorrencia.SelectedValue.ToInt32());

        ocorrencia = ocorrencia.Salvar();

        if (reserva.Ocorrencias == null)
            reserva.Ocorrencias = new List<Ocorrencia>();

        if (!reserva.Ocorrencias.Contains(ocorrencia))
            reserva.Ocorrencias.Add(ocorrencia);

        reserva = reserva.Salvar();

        hfIdOcorrencia.Value = ocorrencia.Id.ToString();

        msg.CriarMensagem("Ocorrência salva com sucesso", "Sucesso", MsgIcons.Sucesso);

        lblCadastroOcorrencia_ModalPopupExtender.Hide();
    }

    private void EnviarEmailReservaAprovadaParaOSolicitante()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());
        DateTime dataInicioReserva = tbxDataInicio.Text.ToDateTime();

        if (ddlPeriodoInicioReserva.SelectedValue == "T")
            dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

        //Setando a data de fim da reserva de acordo com o periodo escolhido
        DateTime dataFimReserva = tbxDataFim.Text.ToDateTime();

        if (ddlPeriodoFimReserva.SelectedValue == "T")
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
        else
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Reversa de veículo aprovada", reserva.Id + " - " + reserva.Veiculo.Placa, "Início do processo");
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Reserva de Veículo Aprovada com sucesso";
        mail.BodyHtml = true;

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());

        log.adicionarLog("Carregando conteúdo do e-mail");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Reserva de Veículo Aprovada com sucesso</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A reserva para o Veículo " + veiculo.Descricao + " - " + veiculo.Placa + ", agendada para " + dataInicioReserva.ToString("dd/MM/yyyy") + ", foi aprovada pelo usuário " + this.FuncionarioLogado.NomeRazaoSocial + @" em " + DateTime.Now.ToShortDateString() + @" no Sistema Ambientalis Manager.<br /><br />  
        Dados da Reserva:      
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Início:</strong>
                    </div>
                    <div>
                        " + dataInicioReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Fim:</strong>
                    </div>
                    <div>
                        " + dataFimReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>                
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Veículo:</strong>
                    </div>
                    <div>
                        " + veiculo.Descricao + " - " + veiculo.Placa + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Tipo de Reserva:</strong>
                    </div>
                    <div>
                        " + ddlTipoReserva.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Responsável:</strong>
                    </div>
                    <div>
                        " + ddlResponsavel.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + tbxDescricao.Text + @"
                    </div>                    
                </td>
            </tr>                                                
        </table>                     
    </div>";

        log.adicionarLog("Carregando a mensagem completa do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Reserva de Veículo Aprovada com sucesso", conteudoemail);

        if (this.EmailDoSolicitanteDeReserva != null && this.EmailDoSolicitanteDeReserva != "")
        {
            log.adicionarLog("Adicionando o e-mail do solicitante");
            mail.AdicionarDestinatario(this.EmailDoSolicitanteDeReserva);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Aprovação de reserva de veículo", this.FuncionarioLogado, null, 587, false))
                log.adicionarLog("E-mail enviado com sucesso!");
            else
                log.adicionarLog("E-mail NÃO enviado. " + mail.Erro);
        }
    }

    private void EnviarEmailReservaEncerradaParaOsResponsaveis()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());
        DateTime dataInicioReserva = tbxDataInicio.Text.ToDateTime();

        if (ddlPeriodoInicioReserva.SelectedValue == "T")
            dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

        //Setando a data de fim da reserva de acordo com o periodo escolhido
        DateTime dataFimReserva = tbxDataFim.Text.ToDateTime();

        if (ddlPeriodoFimReserva.SelectedValue == "T")
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
        else
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Reversa de veículo encerrada", reserva.Id + " - " + reserva.Veiculo.Placa, "Início do processo");
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Encerramento de Reserva de Veículo";
        mail.BodyHtml = true;

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());

        log.adicionarLog("Adicionando o conteúdo");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Encerramento de Reserva de Veículo</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A reserva para o Veículo " + veiculo.Descricao + " - " + veiculo.Placa + ", agendada para " + dataInicioReserva.ToString("dd/MM/yyyy") + ", foi encerada pelo usuário " + this.FuncionarioLogado.NomeRazaoSocial + @" em " + DateTime.Now.ToShortDateString() + @" no Sistema Ambientalis Manager.<br /><br />  
        Dados da Reserva:      
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Início:</strong>
                    </div>
                    <div>
                        " + dataInicioReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Fim:</strong>
                    </div>
                    <div>
                        " + dataFimReserva.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>                
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Veículo:</strong>
                    </div>
                    <div>
                        " + veiculo.Descricao + " - " + veiculo.Placa + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Tipo de Reserva:</strong>
                    </div>
                    <div>
                        " + ddlTipoReserva.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Responsável:</strong>
                    </div>
                    <div>
                        " + ddlResponsavel.SelectedItem.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + tbxDescricao.Text + @"
                    </div>                    
                </td>
            </tr>                                                
        </table>                     
    </div>";

        log.adicionarLog("Carregando a mensagem completa do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Encerramento de Reserva de Veículo", conteudoemail);

        if (this.EmailDoSolicitanteDeReserva != null && this.EmailDoSolicitanteDeReserva != "")
        {
            log.adicionarLog("Adicionando o e-mail do solicitante");
            mail.AdicionarDestinatario(this.EmailDoSolicitanteDeReserva);
        }


        Funcionario gestorVeiculo = veiculo.Gestor;
        if (gestorVeiculo != null && gestorVeiculo.EmailCorporativo != "")
        {
            log.adicionarLog("Adicionando o e-mail do gestor do veículo");
            mail.AdicionarDestinatario(gestorVeiculo.EmailCorporativo);
        }

        Funcionario responsavel = Funcionario.ConsultarPorId(ddlResponsavel.SelectedValue.ToInt32());
        if (responsavel != null && responsavel.EmailCorporativo != "")
        {
            log.adicionarLog("Adicionando o responsável");
            mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Encerramento de reserva de veículo", this.FuncionarioLogado, null, 587, false))
                log.adicionarLog("E-mail enviado");
            else
                log.adicionarLog("E-mail NÃO enviado. " + mail.Erro);
        }
    }

    private void SalvarBoletimUsoReserva()
    {
        Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());
        if (reserva == null)
        {
            msg.CriarMensagem("A reserva não foi corretamente carregada, recarregue a página e tente novamente!", "Erro", MsgIcons.Erro);
            return;
        }
        reserva.Quilometragem = tbxQuilometragem.Text.ToDecimal();
        reserva.Consumo = tbxConsumo.Text.ToDecimal();
        reserva = reserva.Salvar();
        msg.CriarMensagem("Boletim de uso salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    #endregion

    #region _______________ Eventos ________________

    protected void btnNovoCadastro_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CadastroDeReserva.aspx");
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarReserva();

            if (hfId.Value.ToInt32() > 0)
                msg.CriarMensagem("Reserva salva com sucesso", "Sucesso", MsgIcons.Sucesso);
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a reserva para poder excluí-la.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirReserva();
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

    protected void gdvOcorrencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvOcorrencias.PageIndex = e.NewPageIndex;
            this.CarregarGridOcorrencias();
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

    protected void btnNovaOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a reserva para poder inserir ocorrências na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

            if (reserva.Status == Reserva.AGUARDANDO)
            {
                msg.CriarMensagem("Não é possível inserir ocorrências em reservas que ainda não foram aprovadas.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.NovaOcorrencia();
            lblCadastroOcorrencia_ModalPopupExtender.Show();
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

    protected void btnNovoCadastroOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaOcorrencia();
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

    protected void btnGridEditarOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOcorrencia(((Button)sender).CommandArgument.ToInt32());
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

    protected void gdvOcorrencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirOcorrencia(e);
            Transacao.Instance.Recarregar();
            this.CarregarGridOcorrencias();
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

    protected void btnSalvarOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarOcorrencia();
            Transacao.Instance.Recarregar();
            this.CarregarGridOcorrencias();
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

    protected void ddlVeiculo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlVeiculo.SelectedValue.ToInt32() > 0)
            {
                Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());

                if (veiculo != null && veiculo.Id > 0)
                {
                    if (veiculo.Gestor != null && veiculo.Gestor.Id == this.FuncionarioLogado.Id)
                    {
                        if (hfId.Value.ToInt32() > 0)
                        {
                            Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

                            if (reserva.Status == Reserva.AGUARDANDO)
                            {
                                btnSalvar.Visible = btnSalvar.Enabled = false;
                                btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = false;
                                btnAprovar.Enabled = btnAprovar.Visible = true;
                            }
                            else if (reserva.Status == Reserva.APROVADA)
                            {
                                btnSalvar.Visible = btnSalvar.Enabled = true;
                                btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = false;
                                btnAprovar.Enabled = btnAprovar.Visible = false;
                            }

                        }
                        else
                        {
                            btnSalvar.Visible = btnSalvar.Enabled = true;
                            btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = false;
                            btnAprovar.Enabled = btnAprovar.Visible = false;
                        }
                    }
                    else
                    {
                        btnSalvar.Visible = btnSalvar.Enabled = false;
                        btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = true;
                        btnAprovar.Enabled = btnAprovar.Visible = false;
                    }
                }

            }
            else
            {
                btnSalvar.Visible = btnSalvar.Enabled = false;
                btnSolicitarReserva.Visible = btnSolicitarReserva.Enabled = false;
                btnAprovar.Enabled = btnAprovar.Visible = false;
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

    protected void btnSolicitarReserva_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarReserva();

            tbxStatusReserva.Text = "Aguardando Aprovação";

            DateTime dataInicioReserva = tbxDataInicio.Text.ToDateTime();

            if (ddlPeriodoInicioReserva.SelectedValue == "T")
                dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

            //Setando a data de fim da reserva de acordo com o periodo escolhido
            DateTime dataFimReserva = tbxDataFim.Text.ToDateTime();

            if (ddlPeriodoFimReserva.SelectedValue == "T")
                dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
            else
                dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

            if (hfId.Value.ToInt32() > 0)
            {
                this.EnviarEmailCadastroAprovacaoDeReservaParaOGestor(dataInicioReserva, dataFimReserva);
                msg.CriarMensagem("Solicitação de Reserva enviada com sucesso", "Sucesso", MsgIcons.Sucesso);
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

    protected void btnAprovar_Click(object sender, EventArgs e)
    {
        try
        {
            Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());

            DateTime dataInicioReserva = tbxDataInicio.Text.ToDateTime();

            if (ddlPeriodoInicioReserva.SelectedValue == "T")
                dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

            //Setando a data de fim da reserva de acordo com o periodo escolhido
            DateTime dataFimReserva = tbxDataFim.Text.ToDateTime();

            if (ddlPeriodoFimReserva.SelectedValue == "T")
                dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
            else
                dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

            if (Reserva.ExisteAlgumaReservaParaOVeiculoNestePeriodoDeOutraVisita(ddlVeiculo.SelectedValue.ToInt32(), dataInicioReserva, dataFimReserva, reserva))
            {
                msg.CriarMensagem("Já existe uma reserva para este veículo aprovada neste período.", "Informação", MsgIcons.Informacao);
                return;
            }

            reserva.Status = Reserva.APROVADA;

            reserva = reserva.Salvar();

            this.EnviarEmailReservaAprovadaParaOSolicitante();

            msg.CriarMensagem("Reserva aprovada com sucesso", "Sucesso", MsgIcons.Sucesso);

            btnSolicitarReserva.Enabled = btnSolicitarReserva.Visible = btnAprovar.Enabled = btnAprovar.Visible = false;

            if (tbxDescricao.Enabled)
            {
                btnSalvar.Enabled = btnSalvar.Visible = true;
                tbxQuilometragem.Enabled = tbxConsumo.Enabled = true;
            }

            tbxStatusReserva.Text = "Encerrada";

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

    protected void btnEncerrar_Click(object sender, EventArgs e)
    {
        try
        {
            Reserva reserva = Reserva.ConsultarPorId(hfId.Value.ToInt32());
            if (reserva.Status == Reserva.ENCERRADA)
                msg.CriarMensagem("Esta reserva já foi encerrada! Por favor recarregue a página!", "Alerta", MsgIcons.Informacao);
            if (reserva.Quilometragem <= 0)
                msg.CriarMensagem("Não é possível encerrar uma Reserva sem quilometragem!", "Alerta", MsgIcons.Informacao);
            else
            {
                reserva.Status = Reserva.ENCERRADA;
                reserva = reserva.Salvar();
                this.EnviarEmailReservaEncerradaParaOsResponsaveis();
                msg.CriarMensagem("Reserva encerrada com sucesso", "Sucesso", MsgIcons.Sucesso);
                this.CarregarGridOcorrencias();
                this.DasabilitarTodosOSBotoesParaReservaDeVisita();
                tbxStatusReserva.Text = "Encerrada";
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

    protected void btnSalvarBoletimUso_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarBoletimUsoReserva();
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

    #region ______________ PreRenders _______________

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta reserva serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnEncerrar_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Se a reserva for encerrada não será possível alterar seus dados. Deseja realmente encerrar esta reserva ?");
    }

    protected void gdvOcorrencias_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvOcorrencias.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirOcorrencia_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta ocorrência serão perdidos. Deseja realmente excluir ?");
    }

    #endregion

    #region _______________ Triggers ________________

    protected void btnNovaOcorrencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposOcorrencia);
    }

    protected void btnGridEditarOcorrencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposOcorrencia);
    }

    protected void btnSalvarOcorrencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOcorrencias);
    }

    #endregion

    public bool BindingVisivelEdicaoOCorrencia(object o)
    {
        Ocorrencia ocorrencia = (Ocorrencia)o;
        return ocorrencia != null && ocorrencia.Reserva != null && ocorrencia.Reserva.Responsavel != null &&
            (ocorrencia.Reserva.Responsavel.Equals(this.FuncionarioLogado) || ocorrencia.Reserva.Veiculo.Gestor.Equals(this.FuncionarioLogado));
    }

}