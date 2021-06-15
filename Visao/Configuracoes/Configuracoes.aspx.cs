using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Configuracoes_Configuracoes : PageBase
{
    private Msg msg = new Msg();

    private Configuracoes ConfiguracoesSistema
    {
        get
        {
            if (Session["configuracoes_sistema_manager"] == null)
                Session["configuracoes_sistema_manager"] = Configuracoes.GetConfiguracoesSistema();
            return (Configuracoes)Session["configuracoes_sistema_manager"];
        }
        set
        {
            Session["configuracoes_sistema_manager"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
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

    private void CarregarCampos()
    {
        this.CarregarUnidades();
        this.CarregarClassificacoes();
    }

    private void CarregarUnidades()
    {
        ddlUnidadeDepartamento.Items.Clear();
        ddlUnidadeDepartamento.Items.Add(new ListItem("-- Selecione --", "0"));

        ddlUnidadeSetor.Items.Clear();
        ddlUnidadeSetor.Items.Add(new ListItem("-- Selecione --", "0"));
        IList<Unidade> unidades = new Unidade().ConsultarOrdemAcendente(0);
        foreach (Unidade uni in unidades)
        {
            ddlUnidadeDepartamento.Items.Add(new ListItem(uni.Descricao, uni.Id.ToString()));
            ddlUnidadeSetor.Items.Add(new ListItem(uni.Descricao, uni.Id.ToString()));
        }

        ddlUnidadeDepartamento.SelectedIndex = ddlUnidadeSetor.SelectedIndex = 0;
        this.CarregarDepartamentosSetor();
    }

    private void CarregarDepartamentosSetor()
    {
        ddlDepartamentoSetor.Items.Clear();
        ddlDepartamentoSetor.Items.Add(new ListItem("-- Selecione --", "0"));
        if (ddlUnidadeSetor.SelectedIndex > 0)
        {
            IList<Departamento> deptos = Departamento.ConsultarDepartamentosDaUnidade(ddlUnidadeSetor.SelectedValue.ToInt32());
            foreach (Departamento depto in deptos)
                ddlDepartamentoSetor.Items.Add(new ListItem(depto.Nome, depto.Id.ToString()));

            ddlDepartamentoSetor.SelectedIndex = 0;
        }
    }

    private void CarregarConfiguracoesGerais()
    {
        txtEmailsAvisoConfirmacaoOrcamento.Text = this.ConfiguracoesSistema.EmailsAvisoOrcamentoAceito;
        txtEmailsAvisoConfirmacaoRespostaPesquisaSatisfacao.Text = this.ConfiguracoesSistema.EmailsAvisoPesquisaSatisfacaoRespondida;
        txtEmailsAvisoContratacaoAceita.Text = this.ConfiguracoesSistema.EmailsAvisoContratacaoAceita;
        txtEmailsSolicitacaoLiberacaoDespesa.Text = this.ConfiguracoesSistema.EmailsSolicitacaoLiberacaoDespesa;
    }

    private void CarregarTiposPedidos()
    {
        IList<TipoPedido> tiposPedidos = TipoPedido.ConsultarTodosOrdemAlfabetica();

        gdvTiposPedido.DataSource = tiposPedidos != null && tiposPedidos.Count > 0 ? tiposPedidos : new List<TipoPedido>();
        gdvTiposPedido.DataBind();
    }

    private void ExcluirTipoPedido(GridViewDeleteEventArgs e)
    {
        TipoPedido tipoPedido = TipoPedido.ConsultarPorId(gdvTiposPedido.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirTipoPedido(tipoPedido);
    }

    private void ExcluirTipoPedido(TipoPedido tipoPedido)
    {
        if (tipoPedido != null)
        {
            if (tipoPedido.Pedidos != null && tipoPedido.Pedidos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir tipos de pedidos que estejam associados à algum pedido.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoPedido.Excluir())
                msg.CriarMensagem("Tipo de Pedido excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaTipoPedido(int id)
    {
        TipoPedido tipoPedido = TipoPedido.ConsultarPorId(id);

        if (tipoPedido != null)
        {
            tbxNomeTipoPedido.Text = tipoPedido.Nome;

            campos_cadastro_tipo_pedido.Visible = true;

            hfIdTipoPedido.Value = tipoPedido.Id.ToString();
        }
    }

    private void SalvarTipoPedido()
    {
        TipoPedido tipoPedido = TipoPedido.ConsultarPorId(hfIdTipoPedido.Value.ToInt32());

        if (tipoPedido == null)
            tipoPedido = new TipoPedido();

        tipoPedido.Nome = tbxNomeTipoPedido.Text;

        tipoPedido = tipoPedido.Salvar();

        msg.CriarMensagem("Tipo de Pedido salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoTipoPedido();

        campos_cadastro_tipo_pedido.Visible = false;
    }

    private void NovoTipoPedido()
    {
        tbxNomeTipoPedido.Text = hfIdTipoPedido.Value = "";
        campos_cadastro_tipo_pedido.Visible = true;
    }

    private void CarregarTiposOS()
    {
        IList<TipoOrdemServico> tiposOS = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

        gdvTiposOs.DataSource = tiposOS != null && tiposOS.Count > 0 ? tiposOS : new List<TipoOrdemServico>();
        gdvTiposOs.DataBind();
    }

    private void ExcluirTipoOS(GridViewDeleteEventArgs e)
    {
        TipoOrdemServico tipoOrdem = TipoOrdemServico.ConsultarPorId(gdvTiposOs.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirTipoOrdem(tipoOrdem);
    }

    private void ExcluirTipoOrdem(TipoOrdemServico tipoOrdem)
    {
        if (tipoOrdem != null)
        {
            if (tipoOrdem.OrdensServico != null && tipoOrdem.OrdensServico.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir tipos de ordem de serviço que estejam associados à alguma OS.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoOrdem.Excluir())
                msg.CriarMensagem("Tipo de Ordem de Serviço excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaTipoOS(int id)
    {
        TipoOrdemServico tipoOS = TipoOrdemServico.ConsultarPorId(id);

        if (tipoOS != null)
        {
            tbxNomeTipoOS.Text = tipoOS.Nome;
            tbxPrazoPadraoTipoOS.Text = tipoOS.PrazoPadrao.ToString();

            campos_cadastro_tipo_os.Visible = true;

            hfIdTipoOS.Value = tipoOS.Id.ToString();
        }
    }

    private void NovoTipoOS()
    {
        tbxNomeTipoOS.Text = hfIdTipoOS.Value = tbxPrazoPadraoTipoOS.Text = "";
        campos_cadastro_tipo_os.Visible = true;
    }

    private void SalvarTipoOS()
    {
        TipoOrdemServico tipoOS = TipoOrdemServico.ConsultarPorId(hfIdTipoOS.Value.ToInt32());

        if (tipoOS == null)
            tipoOS = new TipoOrdemServico();

        tipoOS.Nome = tbxNomeTipoOS.Text;
        tipoOS.PrazoPadrao = tbxPrazoPadraoTipoOS.Text.ToInt32();

        tipoOS = tipoOS.Salvar();

        msg.CriarMensagem("Tipo de Ordem de Serviço salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoTipoOS();

        campos_cadastro_tipo_os.Visible = false;
    }

    private void CarregarOrgaos()
    {
        IList<Orgao> orgao = Orgao.ConsultarTodosOrdemAlfabetica();

        gdvOrgaos.DataSource = orgao != null && orgao.Count > 0 ? orgao : new List<Orgao>();
        gdvOrgaos.DataBind();
    }

    private void ExcluirOrgao(GridViewDeleteEventArgs e)
    {
        Orgao orgao = Orgao.ConsultarPorId(gdvOrgaos.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirOrgao(orgao);
    }

    private void ExcluirOrgao(Orgao orgao)
    {
        if (orgao != null)
        {
            if (orgao.OrdensServico != null && orgao.OrdensServico.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir órgãos que estejam associados à alguma OS.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (orgao.Excluir())
                msg.CriarMensagem("Órgão excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaOrgao(int id)
    {
        Orgao orgao = Orgao.ConsultarPorId(id);

        if (orgao != null)
        {
            tbxNomeOrgao.Text = orgao.Nome;

            campos_cadastro_orgao.Visible = true;

            hfIdOrgao.Value = orgao.Id.ToString();
        }
    }

    private void NovoOrgao()
    {
        tbxNomeOrgao.Text = hfIdOrgao.Value = "";
        campos_cadastro_orgao.Visible = true;
    }

    private void SalvarOrgao()
    {
        Orgao orgao = Orgao.ConsultarPorId(hfIdOrgao.Value.ToInt32());

        if (orgao == null)
            orgao = new Orgao();

        orgao.Nome = tbxNomeOrgao.Text;

        orgao = orgao.Salvar();

        msg.CriarMensagem("Órgão salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoOrgao();

        campos_cadastro_orgao.Visible = false;
    }

    private void CarregarTiposVisita()
    {
        IList<TipoVisita> tiposVisita = TipoVisita.ConsultarTodosOrdemAlfabetica();

        gdvTiposVisita.DataSource = tiposVisita != null && tiposVisita.Count > 0 ? tiposVisita : new List<TipoVisita>();
        gdvTiposVisita.DataBind();
    }

    private void ExcluirTipoVisita(GridViewDeleteEventArgs e)
    {
        TipoVisita tipoVisita = TipoVisita.ConsultarPorId(gdvTiposVisita.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluiTipoVisita(tipoVisita);
    }

    private void ExcluiTipoVisita(TipoVisita tipoVisita)
    {
        if (tipoVisita != null)
        {
            if (tipoVisita.Visitas != null && tipoVisita.Visitas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir tipos de visita que estejam associados à alguma visita.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoVisita.Excluir())
                msg.CriarMensagem("Tipo de Visita excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaTipoVisita(int id)
    {
        TipoVisita tipoVisita = TipoVisita.ConsultarPorId(id);

        if (tipoVisita != null)
        {
            tbxNomeTipoVisita.Text = tipoVisita.Nome;

            campos_cadastro_tipo_visita.Visible = true;

            hfIdTipoVisita.Value = tipoVisita.Id.ToString();
        }
    }

    private void NovoTipoVisita()
    {
        tbxNomeTipoVisita.Text = hfIdTipoVisita.Value = "";
        campos_cadastro_tipo_visita.Visible = true;
    }

    private void SalvarTipoVisita()
    {
        TipoVisita tipoVisita = TipoVisita.ConsultarPorId(hfIdTipoVisita.Value.ToInt32());

        if (tipoVisita == null)
            tipoVisita = new TipoVisita();

        tipoVisita.Nome = tbxNomeTipoVisita.Text;

        tipoVisita = tipoVisita.Salvar();

        msg.CriarMensagem("Tipo de Visita salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoTipoOS();

        campos_cadastro_tipo_visita.Visible = false;
    }

    private void CarregarTiposAtividade()
    {
        IList<TipoAtividade> tiposAtividade = TipoAtividade.ConsultarTodosOrdemAlfabetica();

        gdvTiposAtividade.DataSource = tiposAtividade != null && tiposAtividade.Count > 0 ? tiposAtividade : new List<TipoAtividade>();
        gdvTiposAtividade.DataBind();
    }

    private void ExcluirTipoAtividade(GridViewDeleteEventArgs e)
    {
        TipoAtividade tipoAtividade = TipoAtividade.ConsultarPorId(gdvTiposAtividade.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluiTipoAtividade(tipoAtividade);
    }

    private void ExcluiTipoAtividade(TipoAtividade tipoAtividade)
    {
        if (tipoAtividade != null)
        {
            if (tipoAtividade.Atividades != null && tipoAtividade.Atividades.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir tipos de atividade que estejam associados à alguma atividade.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoAtividade.Excluir())
                msg.CriarMensagem("Tipo de Atividade excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaTipoAtividade(int id)
    {
        TipoAtividade tipoAtividade = TipoAtividade.ConsultarPorId(id);

        if (tipoAtividade != null)
        {
            tbxNomeTipoAtividade.Text = tipoAtividade.Nome;

            campos_cadastro_tipo_atividade.Visible = true;

            hfIdTipoAtividade.Value = tipoAtividade.Id.ToString();
        }
    }

    private void NovoTipoAtividade()
    {
        tbxNomeTipoAtividade.Text = hfIdTipoAtividade.Value = "";
        campos_cadastro_tipo_atividade.Visible = true;
    }

    private void SalvarTipoAtividade()
    {
        TipoAtividade tipoAtividade = TipoAtividade.ConsultarPorId(hfIdTipoAtividade.Value.ToInt32());

        if (tipoAtividade == null)
            tipoAtividade = new TipoAtividade();

        tipoAtividade.Nome = tbxNomeTipoAtividade.Text;

        tipoAtividade = tipoAtividade.Salvar();

        msg.CriarMensagem("Tipo de Atividade salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoTipoAtividade();

        campos_cadastro_tipo_atividade.Visible = false;
    }

    public bool BindingVisivelEditTipoReserva(object o)
    {
        TipoReservaVeiculo tipoReserva = (TipoReservaVeiculo)o;
        return tipoReserva != null && !tipoReserva.TipoVisitaOS;
    }

    private void CarregarTiposReservas()
    {
        IList<TipoReservaVeiculo> tiposReserva = TipoReservaVeiculo.ConsultarTodosOrdemAlfabetica();

        gdvTiposReserva.DataSource = tiposReserva != null && tiposReserva.Count > 0 ? tiposReserva : new List<TipoReservaVeiculo>();
        gdvTiposReserva.DataBind();
    }

    private void ExcluirTipoReserva(GridViewDeleteEventArgs e)
    {
        TipoReservaVeiculo tipoReserva = TipoReservaVeiculo.ConsultarPorId(gdvTiposReserva.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluiTipoReserva(tipoReserva);
    }

    private void ExcluiTipoReserva(TipoReservaVeiculo tipoReserva)
    {
        if (tipoReserva != null)
        {
            if (tipoReserva.Reservas != null && tipoReserva.Reservas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir tipos de reserva que estejam associados à alguma reserva.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoReserva.TipoVisitaOS)
            {
                msg.CriarMensagem("Não é possível excluir o tipo de Reserva de Visita de OS.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoReserva.Excluir())
                msg.CriarMensagem("Tipo de Reserva de Veículo excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaTipoReserva(int id)
    {
        TipoReservaVeiculo tipoReserva = TipoReservaVeiculo.ConsultarPorId(id);

        if (tipoReserva != null)
        {
            tbxDescricaoTipoReserva.Text = tipoReserva.Descricao;

            campos_cadastro_tipo_reserva.Visible = true;

            hfIdTipoReserva.Value = tipoReserva.Id.ToString();
        }
    }

    private void NovoTipoReserva()
    {
        tbxDescricaoTipoReserva.Text = hfIdTipoReserva.Value = "";
        campos_cadastro_tipo_reserva.Visible = true;
    }

    private void SalvarTipoReserva()
    {
        TipoReservaVeiculo tipoReserva = TipoReservaVeiculo.ConsultarPorId(hfIdTipoReserva.Value.ToInt32());

        if (tipoReserva == null)
            tipoReserva = new TipoReservaVeiculo();

        tipoReserva.Descricao = tbxDescricaoTipoReserva.Text;
        tipoReserva.TipoVisitaOS = false;

        tipoReserva = tipoReserva.Salvar();

        msg.CriarMensagem("Tipo de Reserva de Veículo salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoTipoReserva();

        campos_cadastro_tipo_reserva.Visible = false;
    }

    private void CarregarTiposOcorrencias()
    {
        IList<TipoOcorrenciaVeiculo> tiposOcorrencia = TipoOcorrenciaVeiculo.ConsultarTodosOrdemAlfabetica();

        gdvTiposOcorrencias.DataSource = tiposOcorrencia != null && tiposOcorrencia.Count > 0 ? tiposOcorrencia : new List<TipoOcorrenciaVeiculo>();
        gdvTiposOcorrencias.DataBind();
    }

    private void ExcluirTipoOcorrencia(GridViewDeleteEventArgs e)
    {
        TipoOcorrenciaVeiculo tipoOcorrencia = TipoOcorrenciaVeiculo.ConsultarPorId(gdvTiposOcorrencias.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluiTipoOcorrencia(tipoOcorrencia);
    }

    private void ExcluiTipoOcorrencia(TipoOcorrenciaVeiculo tipoOcorrencia)
    {
        if (tipoOcorrencia != null)
        {
            if (tipoOcorrencia.Ocorrencias != null && tipoOcorrencia.Ocorrencias.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir tipos de ocorrências que estejam associados à alguma ocorrência.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (tipoOcorrencia.Excluir())
                msg.CriarMensagem("Tipo de Ocorrência de Veículo excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaTipoOcorrencia(int id)
    {
        TipoOcorrenciaVeiculo tipoOcorrencia = TipoOcorrenciaVeiculo.ConsultarPorId(id);

        if (tipoOcorrencia != null)
        {
            tbxDescricaoTipoOcorrencia.Text = tipoOcorrencia.Descricao;

            campos_cadastro_tipo_ocorrencia.Visible = true;

            hfIdTipoOcorrencia.Value = tipoOcorrencia.Id.ToString();
        }
    }

    private void NovoTipoOcorrencia()
    {
        tbxDescricaoTipoOcorrencia.Text = hfIdTipoOcorrencia.Value = "";
        campos_cadastro_tipo_ocorrencia.Visible = true;
    }

    private void SalvarTipoOcorrencia()
    {
        TipoOcorrenciaVeiculo tipoOcorrencia = TipoOcorrenciaVeiculo.ConsultarPorId(hfIdTipoOcorrencia.Value.ToInt32());

        if (tipoOcorrencia == null)
            tipoOcorrencia = new TipoOcorrenciaVeiculo();

        tipoOcorrencia.Descricao = tbxDescricaoTipoOcorrencia.Text;

        tipoOcorrencia = tipoOcorrencia.Salvar();

        msg.CriarMensagem("Tipo de Ocorrência de Veículo salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

        this.NovoTipoOcorrencia();

        campos_cadastro_tipo_ocorrencia.Visible = false;
    }

    private void CarregarTiposDespesas()
    {
        IList<TipoDespesa> tipos = TipoDespesa.ConsultarTodos();

        grvTiposDespesas.DataSource = (tipos != null && tipos.Count > 0 ? tipos : new List<TipoDespesa>());
        grvTiposDespesas.DataBind();
    }
    private void ExcluirTipoDespesa(GridViewDeleteEventArgs e)
    {
        TipoDespesa tipo = TipoDespesa.ConsultarPorId(grvTiposDespesas.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirTipoDespesa(tipo);
    }
    private void CarregaTipoDespesa(int id)
    {
        TipoDespesa tipo = TipoDespesa.ConsultarPorId(id);

        if (tipo != null)
        {
            if (tipo.Classificacao != null)
                ddlClassificacaoTipoDespesa.SelectedValue = tipo.Classificacao.Id.ToString();
            else
                ddlClassificacaoTipoDespesa.SelectedIndex = 0;
            txtNomeTipoDespesa.Text = tipo.Nome;
            chkTipoDespesaPreAprovada.Checked = tipo.PreAprovada;
            campos_cadastro_tipo_depesa.Visible = true;

            hfIdTipoDespesa.Value = tipo.Id.ToString();
        }
    }
    private void NovoTipoDespesa()
    {
        hfIdTipoDespesa.Value = txtNomeTipoDespesa.Text = string.Empty;
        campos_cadastro_tipo_depesa.Visible = true;
    }
    private void ExcluirTipoDespesa(TipoDespesa tipoDespesa)
    {
        if (tipoDespesa != null)
        {
            if (tipoDespesa.Excluir())
                msg.CriarMensagem("Tipo de despesa excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }
    private void SalvarTipoDespesa()
    {
        TipoDespesa tipo = TipoDespesa.ConsultarPorId(hfIdTipoDespesa.Value.ToInt32());

        if (tipo == null)
            tipo = new TipoDespesa();

        tipo.Classificacao = ClassificacaoTipoDespesa.ConsultarPorId(ddlClassificacaoTipoDespesa.SelectedValue.ToInt32());
        tipo.Nome = txtNomeTipoDespesa.Text;
        tipo.PreAprovada = chkTipoDespesaPreAprovada.Checked;
        tipo = tipo.Salvar();

        msg.CriarMensagem("Tipo de despesa salva com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.NovoTipoDespesa();
        campos_cadastro_tipo_depesa.Visible = false;
    }

    private void CarregarFormasPagamento()
    {
        IList<FormaDePagamento> formas = FormaDePagamento.ConsultarTodos();

        grvFormasDePagamento.DataSource = (formas != null && formas.Count > 0 ? formas : new List<FormaDePagamento>());
        grvFormasDePagamento.DataBind();
    }
    private void ExcluirFormaPagamento(GridViewDeleteEventArgs e)
    {
        FormaDePagamento forma = FormaDePagamento.ConsultarPorId(grvFormasDePagamento.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirFormaPagamento(forma);
    }
    private void ExcluirFormaPagamento(FormaDePagamento forma)
    {
        if (forma != null)
        {
            if ((forma.OrcamentosComoFormaSelecionada != null && forma.OrcamentosComoFormaSelecionada.Count > 0)
                || (forma.OrcamentosComoFormaDisponivel != null && forma.OrcamentosComoFormaDisponivel.Count > 0))
            {
                msg.CriarMensagem("Não é possível excluir uma forma de pagamento que esteja associada à orçamentos.", "Informação", MsgIcons.Informacao);
                return;
            }
            if (forma.Excluir())
                msg.CriarMensagem("Forma de pagamento excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }
    private void SalvarFormaPagamento()
    {
        if (txtQtdVezesFormaPagamento.Text.ToInt32() <= 0)
        {
            msg.CriarMensagem("Não é possível salvar uma forma de pagamento com a quantidade de vezes menor ou igual a 0(zero)", "Erro", MsgIcons.Informacao);
            return;
        }
        FormaDePagamento forma = FormaDePagamento.ConsultarPorId(hfIdFormaPagamento.Value.ToInt32());
        if (forma == null)
            forma = new FormaDePagamento();

        forma.Tipo = ddlTipoFormaPagamento.SelectedValue.Trim();
        forma.AcrescimoDesconto = txtAcrescimoDescontoFormaPagamento.Text.ToDecimal() * ddlTipoAcrescimoDescontoFormaPagamento.SelectedValue.ToInt32();
        forma.PrazoPrimeiroPagamento = txtDiasPrimeiroPagamentoFormaPagamento.Text.ToInt32();
        forma.QtdVezes = txtQtdVezesFormaPagamento.Text.ToInt32();
        forma = forma.Salvar();

        msg.CriarMensagem("Forma de pagamento salva com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.NovaFormaPagamento();
        campos_forma_pagamento.Visible = false;
    }
    private void CarregaFormaPagamento(int id)
    {
        FormaDePagamento forma = FormaDePagamento.ConsultarPorId(id);

        if (forma != null)
        {
            hfIdFormaPagamento.Value = forma.Id.ToString();

            ddlTipoFormaPagamento.SelectedValue = forma.Tipo;
            txtAcrescimoDescontoFormaPagamento.Text = (forma.AcrescimoDesconto < 0 ? forma.AcrescimoDesconto * -1 : forma.AcrescimoDesconto).ToString("#0.0");
            ddlTipoAcrescimoDescontoFormaPagamento.SelectedValue = (forma.AcrescimoDesconto < 0 ? "-1" : "1");
            txtDiasPrimeiroPagamentoFormaPagamento.Text = forma.PrazoPrimeiroPagamento.ToString();
            txtQtdVezesFormaPagamento.Text = forma.QtdVezes.ToString();
            campos_forma_pagamento.Visible = true;
        }
    }
    private void NovaFormaPagamento()
    {
        hfIdFormaPagamento.Value = string.Empty;
        ddlTipoFormaPagamento.SelectedIndex = 0;
        txtAcrescimoDescontoFormaPagamento.Text = "0.0";
        txtDiasPrimeiroPagamentoFormaPagamento.Text = txtQtdVezesFormaPagamento.Text = "0";
        campos_forma_pagamento.Visible = true;
    }

    private void CarregarDepartamentos()
    {
        IList<Departamento> deptos = Departamento.ConsultarTodosSemMultiEmpresa();

        grvDepartamentos.DataSource = (deptos != null && deptos.Count > 0 ? deptos : new List<Departamento>());
        grvDepartamentos.DataBind();
    }
    private void ExcluirDepartamento(GridViewDeleteEventArgs e)
    {
        Departamento depto = Departamento.ConsultarPorId(grvDepartamentos.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirDepartamento(depto);
    }
    private void ExcluirDepartamento(Departamento depto)
    {
        if (depto != null)
        {
            //if (tipoDespesa.Ocorrencias != null && tipoDespesa.Ocorrencias.Count > 0)
            //{
            //    msg.CriarMensagem("Não é possível excluir tipos de ocorrências que estejam associados à alguma ocorrência.", "Informação", MsgIcons.Informacao);
            //    return;
            //}

            if (depto.Excluir())
                msg.CriarMensagem("Departamento excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }
    private void CarregarDepartamento(int id)
    {
        Departamento depto = Departamento.ConsultarPorId(id);

        if (depto != null)
        {
            hfIdDepartamento.Value = depto.Id.ToString();
            ddlUnidadeDepartamento.SelectedValue = depto.Emp.ToString();
            txtNomeDepartamento.Text = depto.Nome;
            campos_departamento.Visible = true;
        }
    }
    private void NovoDepartamento()
    {
        hfIdDepartamento.Value = txtNomeDepartamento.Text = string.Empty;
        ddlUnidadeDepartamento.SelectedIndex = 0;
        campos_departamento.Visible = true;
    }
    private void SalvarDepartamento()
    {
        Departamento depto = Departamento.ConsultarPorId(hfIdDepartamento.Value.ToInt32());

        if (depto == null)
            depto = new Departamento();

        depto.MultiEmpresa = false;
        depto.Emp = ddlUnidadeDepartamento.SelectedValue.ToInt32();
        depto.Nome = txtNomeDepartamento.Text.Trim();
        depto = depto.Salvar();

        msg.CriarMensagem("Departamento salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.NovoDepartamento();
        campos_departamento.Visible = false;
    }

    private void CarregarSetores()
    {
        IList<Setor> setores = Setor.ConsultarTodosSemMultiEmpresa();

        grvSetores.DataSource = (setores != null && setores.Count > 0 ? setores : new List<Setor>());
        grvSetores.DataBind();
    }
    private void ExcluirSetor(GridViewDeleteEventArgs e)
    {
        Setor setor = Setor.ConsultarPorId(grvSetores.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirSetor(setor);
    }
    private void ExcluirSetor(Setor setor)
    {
        if (setor != null)
        {
            //if (tipoDespesa.Ocorrencias != null && tipoDespesa.Ocorrencias.Count > 0)
            //{
            //    msg.CriarMensagem("Não é possível excluir tipos de ocorrências que estejam associados à alguma ocorrência.", "Informação", MsgIcons.Informacao);
            //    return;
            //}

            if (setor.Excluir())
                msg.CriarMensagem("Setor excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }
    private void CarregarSetor(int id)
    {
        Setor setor = Setor.ConsultarPorId(id);

        if (setor == null)
            return;
        if (setor.Departamento.Emp <= 0)
        {
            msg.CriarMensagem("O departamento do setor não possui uma unidade. Salve a unidade do departamento antes", "Informação", MsgIcons.Informacao);
            return;
        }
        hfIdSetor.Value = setor.Id.ToString();
        ddlUnidadeSetor.SelectedValue = setor.Departamento.Emp.ToString();
        this.CarregarDepartamentosSetor();
        ddlDepartamentoSetor.SelectedValue = setor.Departamento.Id.ToString();
        txtNomeSetor.Text = setor.Nome;
        campos_setor.Visible = true;
    }
    private void NovoSetor()
    {
        hfIdSetor.Value = txtNomeSetor.Text = string.Empty;
        this.CarregarUnidades();
        campos_setor.Visible = true;
    }
    private void SalvarSetor()
    {
        Setor setor = Setor.ConsultarPorId(hfIdSetor.Value.ToInt32());

        if (setor == null)
            setor = new Setor();

        setor.MultiEmpresa = false;
        setor.Emp = ddlUnidadeSetor.SelectedValue.ToInt32();
        setor.Departamento = Departamento.ConsultarPorId(ddlDepartamentoSetor.SelectedValue.ToInt32());
        setor.Nome = txtNomeSetor.Text.Trim();
        setor = setor.Salvar();

        msg.CriarMensagem("Setor salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.NovoSetor();
        campos_setor.Visible = false;
    }

    private void CarregarClassificacoes()
    {
        IList<ClassificacaoTipoDespesa> classes = ClassificacaoTipoDespesa.ConsultarTodosOrdemAlfabetica();

        grvClassificacoes.DataSource = (classes != null && classes.Count > 0 ? classes : new List<ClassificacaoTipoDespesa>());
        grvClassificacoes.DataBind();

        ddlClassificacaoTipoDespesa.Items.Clear();
        ddlClassificacaoTipoDespesa.Items.Add(new ListItem("-- Selecione --", "0"));
        foreach (ClassificacaoTipoDespesa classe in classes)
            ddlClassificacaoTipoDespesa.Items.Add(new ListItem(classe.Nome, classe.Id.ToString()));
        ddlClassificacaoTipoDespesa.SelectedIndex = 0;
    }
    private void ExcluirClassificacao(GridViewDeleteEventArgs e)
    {
        ClassificacaoTipoDespesa classe = ClassificacaoTipoDespesa.ConsultarPorId(grvClassificacoes.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirClassificacao(classe);
    }
    private void ExcluirClassificacao(ClassificacaoTipoDespesa classe)
    {
        if (classe != null)
        {
            if (classe.TiposDespesas != null && classe.TiposDespesas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir classificações que possuam tipos de despesa.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (classe.Excluir())
                msg.CriarMensagem("Classificação de tipos de despesas excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }
    private void CarregarClassificacao(int id)
    {
        ClassificacaoTipoDespesa classe = ClassificacaoTipoDespesa.ConsultarPorId(id);

        if (classe == null)
            return;
        hfIdClassificacao.Value = classe.Id.ToString();
        txtNomeClassificacao.Text = classe.Nome;
        campos_classificacao.Visible = true;
    }
    private void NovaClassficacao()
    {
        hfIdClassificacao.Value = txtNomeClassificacao.Text = string.Empty;
        this.CarregarClassificacoes();
        campos_classificacao.Visible = true;
    }
    private void SalvarClassificacao()
    {
        ClassificacaoTipoDespesa classe = ClassificacaoTipoDespesa.ConsultarPorId(hfIdClassificacao.Value.ToInt32());

        if (classe == null)
            classe = new ClassificacaoTipoDespesa();

        classe.Nome = txtNomeClassificacao.Text.Trim();
        classe = classe.Salvar();

        msg.CriarMensagem("Classificação de tipo de despesa salva com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.NovaClassficacao();
        campos_classificacao.Visible = false;
    }

    private void CarregarOrigens()
    {
        IList<Origem> classes = Origem.ConsultarTodosOrdemAlfabetica();
        gdvOrigens.DataSource = (classes != null && classes.Count > 0 ? classes : new List<Origem>());
        gdvOrigens.DataBind();
    }
    private void ExcluirOrigem(GridViewDeleteEventArgs e)
    {
        Origem classe = Origem.ConsultarPorId(gdvOrigens.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        this.ExcluirOrigem(classe);
    }
    private void ExcluirOrigem(Origem classe)
    {
        if (classe != null)
        {
            if (classe.Pessoas != null && classe.Pessoas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir origens que possuam clientes.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (classe.Excluir())
                msg.CriarMensagem("Origem de cliente excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }
    private void CarregarOrigem(int id)
    {
        Origem classe = Origem.ConsultarPorId(id);

        if (classe == null)
            return;
        hfIdOrigem.Value = classe.Id.ToString();
        txtNomeOrigem.Text = classe.Nome;
        campos_origem.Visible = true;
    }
    private void NovaOrigem()
    {
        hfIdOrigem.Value = txtNomeOrigem.Text = string.Empty;
        this.CarregarOrigens();
        campos_origem.Visible = true;
    }
    private void SalvarOrigem()
    {
        Origem classe = Origem.ConsultarPorId(hfIdOrigem.Value.ToInt32());

        if (classe == null)
            classe = new Origem();

        classe.Nome = txtNomeOrigem.Text.Trim();
        classe = classe.Salvar();

        msg.CriarMensagem("Origem de cliente salva com sucesso", "Sucesso", MsgIcons.Sucesso);
        this.NovaOrigem();
        campos_origem.Visible = false;
    }

    #endregion

    #region _______________ Trigers _______________

    protected void btnGridEditarTipoPedido_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoPedido);
    }

    protected void btnExcluirTipoPedido_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposPedidos);
    }

    protected void btnSalvarTipoPedido_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposPedidos);
    }

    protected void btnGridEditarTipoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoOS);
    }

    protected void btnSalvarTipoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposOS);
    }

    protected void btnExcluirTipoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposOS);
    }

    protected void btnGridEditarOrgao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadOrgao);
    }

    protected void btnSalvarOrgao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOrgaos);
    }

    protected void btnExcluirOrgao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOrgaos);
    }

    protected void btnGridEditarTipoVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoVisita);
    }

    protected void btnSalvarTipoVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposVisita);
    }

    protected void btnExcluirTipoVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposVisita);
    }

    protected void btnGridEditarTipoAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoAtividade);
    }

    protected void btnSalvarAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposAtividade);
    }

    protected void btnExcluirAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposAtividade);
    }

    protected void btnGridEditarTipoReserva_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoReserva);
    }

    protected void btnSalvarTipoReserva_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposReserva);
    }

    protected void btnExcluirTipoReserva_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposReserva);
    }

    protected void btnGridEditarTipoOcorrencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoOcorrencia);
    }

    protected void btnSalvarTipoOcorrencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposOcorrencias);
    }

    protected void btnExcluirTipoOcorrencia_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposOcorrencias);
    }

    protected void btnGridEditarTipoDespesa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadTipoDespesa);
    }
    protected void btnExcluirTipoDespesa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposDespesas);
    }
    protected void btnSalvarTipoDespesa_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upTiposDespesas);
    }

    protected void btnGridEditarFormaPagamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadFormaPagamento);
    }
    protected void btnExcluirFormaPagamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormasDePagamento);
    }
    protected void btnSalvarFormaPagamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormasDePagamento);
    }

    protected void btnGridEditarDepartamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadDepartamento);
    }
    protected void btnExcluirDepartamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upDepartamentos);
    }
    protected void btnSalvarDepartamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upDepartamentos);
    }

    protected void btnGridEditarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadSetor);
    }
    protected void btnExcluirSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSetores);
    }
    protected void btnSalvarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSetores);
    }

    protected void btnGridEditarClassificacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastroClassificacao);
    }
    protected void btnSalvarClassificacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upClassificacoes);
    }
    protected void btnExcluirClassficacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upClassificacoes);
    }

    protected void btnGridEditarOrigem_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upNovaOrigem);
    }
    protected void btnSalvarOrigem_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOrigens);
    }
    protected void btnExcluirOrigem_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOrigens);
    }

    #endregion

    #region _______________ PreRender ______________

    protected void gdvTiposPedido_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposPedido.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void gdvTiposOs_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposOs.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnExcluirTipoPedido_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de pedido serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnGridExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de pedido serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnGridExcluirTipoOs_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de ordem de serviço serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnExcluirTipoOS_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de ordem de serviço serão perdidos. Deseja realmente excluir ?");
    }

    protected void gdvOrgaos_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvOrgaos.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirOrgao_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este órgão serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnExcluirOrgao_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este órgão serão perdidos. Deseja realmente excluir ?");
    }

    protected void gdvTiposVisita_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposVisita.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirTipoVisita_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de visita serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnExcluirTipoVisita_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de visita serão perdidos. Deseja realmente excluir ?");
    }

    protected void gdvTiposAtividade_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposAtividade.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirTipoAtividade_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de atividade serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnExcluirAtividade_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de atividade serão perdidos. Deseja realmente excluir ?");
    }

    protected void gdvTiposReserva_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposReserva.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirTipoReserva_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de reserva serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnExcluirTipoReserva_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de reserva de veículo serão perdidos. Deseja realmente excluir ?");
    }

    protected void gdvTiposOcorrencias_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposOcorrencias.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirTipoOcorrencia_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de ocorrência serão perdidos. Deseja realmente excluir ?");
    }

    protected void btnExcluirTipoOcorrencia_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de ocorrência de veículo serão perdidos. Deseja realmente excluir?");
    }

    protected void btnGridExcluirTipoDespesa_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de despesa serão perdidos. Deseja realmente excluir?");
    }
    protected void btnExcluirTipoDespesa_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este tipo de despesa serão perdidos. Deseja realmente excluir?");
    }

    protected void btnGridExcluirFormaPagamento_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta forma de pagamento serão perdidos. Deseja realmente excluir?");
    }
    protected void btnExcluirFormaPagamento_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta forma de pagamento serão perdidos. Deseja realmente excluir?");
    }

    protected void btnGridExcluirDepartamento_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este departamento serão perdidos. Deseja realmente excluir?");
    }
    protected void btnExcluirDepartamento_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este departamento serão perdidos. Deseja realmente excluir?");
    }

    protected void btnGridExcluirSetor_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este setor serão perdidos. Deseja realmente excluir?");
    }
    protected void btnExcluirSetor_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este setor serão perdidos. Deseja realmente excluir?");
    }

    protected void btnGridExcluirClassificacao_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir esta classificação de tipos de despesas?");
    }
    protected void btnExcluirClassficacao_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir esta classificação de tipos de despesas?");
    }

    protected void gdvOrigens_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvTiposOcorrencias.BottomPagerRow;
        if (pager != null)
            pager.Visible = true;
    }
    protected void btnGridExcluirOrigem_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir esta origem de clientes?");
    }
    protected void btnExcluirOrigem_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir esta origem de clientes?");
    }

    #endregion

    #region _______________ Eventos _______________

    protected void gdvTiposPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvTiposPedido.PageIndex = e.NewPageIndex;
            this.CarregarTiposPedidos();
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
    protected void gdvTiposOs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvTiposOs.PageIndex = e.NewPageIndex;
            this.CarregarTiposOS();
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
    protected void gdvOrgaos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvOrgaos.PageIndex = e.NewPageIndex;
            this.CarregarOrgaos();
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
    protected void gdvTiposVisita_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvTiposVisita.PageIndex = e.NewPageIndex;
            this.CarregarTiposVisita();
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
    protected void gdvTiposAtividade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvTiposAtividade.PageIndex = e.NewPageIndex;
            this.CarregarTiposAtividade();
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
    protected void gdvTiposReserva_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvTiposReserva.PageIndex = e.NewPageIndex;
            this.CarregarTiposReservas();
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

    protected void gdvTiposPedido_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoPedido(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposPedidos();

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
    protected void btnGridEditarTipoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoPedido(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnExcluirTipoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoPedido.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a tipo de pedido para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirTipoPedido(TipoPedido.ConsultarPorId(hfIdTipoPedido.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposPedidos();

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
    protected void btnNovoTipoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoPedido();
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
    protected void btnSalvarTipoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoPedido();
            Transacao.Instance.Recarregar();
            this.CarregarTiposPedidos();
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
    protected void gdvTiposOs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoOS(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposOS();

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

    protected void btnGridEditarTipoOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoOS(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoTipoOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoOS();
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

    protected void btnSalvarTipoOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoOS();
            Transacao.Instance.Recarregar();
            this.CarregarTiposOS();
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

    protected void btnExcluirTipoOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a tipo de ordem de serviço para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirTipoOrdem(TipoOrdemServico.ConsultarPorId(hfIdTipoOS.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposOS();

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

    protected void btnCancelarTipoOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoOS();
            campos_cadastro_tipo_os.Visible = false;
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

    protected void btnCancelarTipoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoPedido();
            campos_cadastro_tipo_pedido.Visible = false;
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

    protected void gdvOrgaos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirOrgao(e);
            Transacao.Instance.Recarregar();
            this.CarregarOrgaos();
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

    protected void btnGridEditarOrgao_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaOrgao(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoOrgao_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoOrgao();
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

    protected void btnSalvarOrgao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarOrgao();
            Transacao.Instance.Recarregar();
            this.CarregarOrgaos();
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

    protected void btnExcluirOrgao_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdOrgao.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o órgão para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirOrgao(Orgao.ConsultarPorId(hfIdOrgao.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarOrgaos();

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

    protected void btnCancelarOrgao_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoOrgao();
            campos_cadastro_orgao.Visible = false;
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

    protected void gdvTiposVisita_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoVisita(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposVisita();

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

    protected void btnGridEditarTipoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoVisita(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoTipoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoVisita();
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

    protected void btnSalvarTipoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoVisita();
            Transacao.Instance.Recarregar();
            this.CarregarTiposVisita();
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

    protected void btnExcluirTipoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o tipo de visita para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluiTipoVisita(TipoVisita.ConsultarPorId(hfIdTipoVisita.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposVisita();

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

    protected void btnCancelarTipoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoVisita();
            campos_cadastro_tipo_visita.Visible = false;
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

    protected void gdvTiposAtividade_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoAtividade(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposAtividade();
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

    protected void btnGridEditarTipoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoAtividade(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoTipoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoAtividade();
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

    protected void btnSalvarAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoAtividade();
            Transacao.Instance.Recarregar();
            this.CarregarTiposAtividade();
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

    protected void btnExcluirAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o tipo de atividade para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluiTipoAtividade(TipoAtividade.ConsultarPorId(hfIdTipoAtividade.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposAtividade();

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

    protected void btnCancelarAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoAtividade();
            campos_cadastro_tipo_atividade.Visible = false;
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

    protected void gdvTiposReserva_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoReserva(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposReservas();
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

    protected void btnGridEditarTipoReserva_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoReserva(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoTipoReserva_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoReserva();
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

    protected void btnSalvarTipoReserva_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoReserva();
            Transacao.Instance.Recarregar();
            this.CarregarTiposReservas();
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

    protected void btnExcluirTipoReserva_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoReserva.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o tipo de reserva de veículo para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluiTipoReserva(TipoReservaVeiculo.ConsultarPorId(hfIdTipoReserva.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposReservas();

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

    protected void btnCancelarTipoReserva_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoReserva();
            campos_cadastro_tipo_reserva.Visible = false;
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

    protected void gdvTiposOcorrencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvTiposOcorrencias.PageIndex = e.NewPageIndex;
            this.CarregarTiposOcorrencias();
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

    protected void gdvTiposOcorrencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoOcorrencia(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposOcorrencias();
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

    protected void btnGridEditarTipoOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoOcorrencia(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoTipoOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoOcorrencia();
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

    protected void btnSalvarTipoOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoOcorrencia();
            Transacao.Instance.Recarregar();
            this.CarregarTiposOcorrencias();
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

    protected void btnExcluirTipoOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoOcorrencia.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o tipo de ocorrência de veículo para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluiTipoOcorrencia(TipoOcorrenciaVeiculo.ConsultarPorId(hfIdTipoOcorrencia.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposOcorrencias();

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

    protected void btnCancelarTipoOcorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoOcorrencia();
            campos_cadastro_tipo_ocorrencia.Visible = false;
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

    protected void btnSalvarConfiguracoesGerais_Click(object sender, EventArgs e)
    {
        try
        {
            String[] emailsContratacao = txtEmailsAvisoContratacaoAceita.Text.Split(';');
            foreach (string email in emailsContratacao)
                if (email.IsNotNullOrEmpty() && !Validadores.ValidaEmail(email))
                {
                    msg.CriarMensagem("O e-mail " + email + " no campo para aviso de CONTRATACAO não é válido!", "Informação", MsgIcons.Informacao);
                    return;
                }
            String[] emailsOrcamento = txtEmailsAvisoConfirmacaoOrcamento.Text.Split(';');
            foreach (string email in emailsOrcamento)
                if (email.IsNotNullOrEmpty() && !Validadores.ValidaEmail(email))
                {
                    msg.CriarMensagem("O e-mail " + email + " no campo para aviso de ORÇAMENTO não é válido!", "Informação", MsgIcons.Informacao);
                    return;
                }
            String[] emailsPesquisa = txtEmailsAvisoConfirmacaoRespostaPesquisaSatisfacao.Text.Split(';');
            foreach (string email in emailsPesquisa)
                if (email.IsNotNullOrEmpty() && !Validadores.ValidaEmail(email))
                {
                    msg.CriarMensagem("O e-mail " + email + " no campo para aviso de PESQUISA DE SATISFAÇÃO não é válido!", "Informação", MsgIcons.Informacao);
                    return;
                }

            String[] emailsSolicitacao = txtEmailsSolicitacaoLiberacaoDespesa.Text.Split(';');
            foreach (string email in emailsSolicitacao)
                if (email.IsNotNullOrEmpty() && !Validadores.ValidaEmail(email))
                {
                    msg.CriarMensagem("O e-mail " + email + " no campo para solicitação de LIBERAÇÃO DE DESPESA não é válido!", "Informação", MsgIcons.Informacao);
                    return;
                }


            this.ConfiguracoesSistema.EmailsAvisoContratacaoAceita = txtEmailsAvisoContratacaoAceita.Text.Trim();
            this.ConfiguracoesSistema.EmailsAvisoOrcamentoAceito = txtEmailsAvisoConfirmacaoOrcamento.Text.Trim();
            this.ConfiguracoesSistema.EmailsAvisoPesquisaSatisfacaoRespondida = txtEmailsAvisoConfirmacaoRespostaPesquisaSatisfacao.Text.Trim();
            this.ConfiguracoesSistema.EmailsSolicitacaoLiberacaoDespesa = txtEmailsSolicitacaoLiberacaoDespesa.Text.Trim();

            this.ConfiguracoesSistema = this.ConfiguracoesSistema.Salvar();
            msg.CriarMensagem("Configurações gerais salvas com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    protected void grvTiposDespesas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirTipoDespesa(e);
            Transacao.Instance.Recarregar();
            this.CarregarTiposDespesas();
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
    protected void btnGridEditarTipoDespesa_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaTipoDespesa(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnNovoTipoDespesa_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoDespesa();
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
    protected void btnCancelarTipoDespesa_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoTipoDespesa();
            campos_cadastro_tipo_depesa.Visible = false;
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
    protected void btnExcluirTipoDespesa_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdTipoDespesa.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o tipo de despesa para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirTipoDespesa(TipoDespesa.ConsultarPorId(hfIdTipoDespesa.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarTiposDespesas();
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
    protected void btnSalvarTipoDespesa_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarTipoDespesa();
            Transacao.Instance.Recarregar();
            this.CarregarTiposDespesas();
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

    protected void grvFormasDePagamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirFormaPagamento(e);
            Transacao.Instance.Recarregar();
            this.CarregarFormasPagamento();
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
    protected void btnGridEditarFormaPagamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaFormaPagamento(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnNovaFormaPagamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaFormaPagamento();
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
    protected void btnCancelarFormaPagamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaFormaPagamento();
            campos_forma_pagamento.Visible = false;
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
    protected void btnExcluirFormaPagamento_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdFormaPagamento.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a forma de pagamento para poder excluí-la.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirFormaPagamento(FormaDePagamento.ConsultarPorId(hfIdFormaPagamento.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarFormasPagamento();
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
    protected void btnSalvarFormaPagamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarFormaPagamento();
            Transacao.Instance.Recarregar();
            this.CarregarFormasPagamento();
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

    protected void grvDepartamentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirDepartamento(e);
            Transacao.Instance.Recarregar();
            this.CarregarDepartamentos();
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
    protected void btnGridEditarDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarDepartamento(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnSalvarDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarDepartamento();
            Transacao.Instance.Recarregar();
            this.CarregarDepartamentos();
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
    protected void btnNovoDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoDepartamento();
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
    protected void btnCancelarDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoDepartamento();
            campos_departamento.Visible = false;
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
    protected void btnExcluirDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdDepartamento.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o departamento para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirDepartamento(Departamento.ConsultarPorId(hfIdDepartamento.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarDepartamentos();
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

    protected void grvSetores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirSetor(e);
            Transacao.Instance.Recarregar();
            this.CarregarSetores();
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
    protected void btnGridEditarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarSetor(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnNovoSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoSetor();
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
    protected void btnCancelarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoSetor();
            campos_setor.Visible = false;
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
    protected void btnExcluirSetor_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdSetor.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o setor para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirSetor(Setor.ConsultarPorId(hfIdSetor.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarSetores();
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
    protected void btnSalvarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarSetor();
            Transacao.Instance.Recarregar();
            this.CarregarSetores();
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
    protected void ddlUnidadeSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarDepartamentosSetor();
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

    protected void grvClassificacoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirClassificacao(e);
            Transacao.Instance.Recarregar();
            this.CarregarClassificacoes();
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
    protected void btnGridEditarClassificacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarClassificacao(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnNovaClassificacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaClassficacao();
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
    protected void btnSalvarClassificacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarClassificacao();
            Transacao.Instance.Recarregar();
            this.CarregarClassificacoes();
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
    protected void btnExcluirClassficacao_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdClassificacao.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a classificação para poder excluí-la.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirClassificacao(ClassificacaoTipoDespesa.ConsultarPorId(hfIdClassificacao.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarClassificacoes();
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
    protected void btnCancelarClassificacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaClassficacao();
            campos_classificacao.Visible = false;
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

    protected void btnExibirTiposPedidos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposPedidos();
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
    protected void btnExibirConfiguracaoGeral_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarConfiguracoesGerais();
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
    protected void btnExibirTiposOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposOS();
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
    protected void btnExibirOrgaos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOrgaos();
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
    protected void btnExibirTiposVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposVisita();
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
    protected void btnExibirTiposAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposAtividade();
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
    protected void btnExibirTiposReservasVeiculos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposReservas();
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
    protected void btnExibirTiposOcorrenciasVeiculos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposOcorrencias();
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
    protected void btnClassificacoesTiposDespesas_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarClassificacoes();
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
    protected void btnExibirTiposDespesas_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarTiposDespesas();
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
    protected void btnExibirFormasPagamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarFormasPagamento();
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
    protected void btnExibirDepartamentos_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarDepartamentos();
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
    protected void btnExibirSetores_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarSetores();
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
    protected void btnExibirOrigem_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOrigens();
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

    protected void gdvOrigens_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvOrigens.PageIndex = e.NewPageIndex;
            this.CarregarOrigens();
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
    protected void gdvOrigens_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirOrigem(e);
            Transacao.Instance.Recarregar();
            this.CarregarOrigens();
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
    protected void btnGridEditarOrigem_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOrigem(((Button)sender).CommandArgument.ToInt32());
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
    protected void btnNovaOrigem_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaOrigem();
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
    protected void btnSalvarOrigem_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarOrigem();
            Transacao.Instance.Recarregar();
            this.CarregarOrigens();
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
    protected void btnExcluirOrigem_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdOrigem.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a origem para poder excluí-la.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirOrigem(Origem.ConsultarPorId(hfIdOrigem.Value.ToInt32()));
            Transacao.Instance.Recarregar();
            this.CarregarOrigens();
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
    protected void btnCancelarOrigem_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaOrigem();
            campos_origem.Visible = false;
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