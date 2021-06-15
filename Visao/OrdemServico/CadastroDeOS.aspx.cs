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

public partial class OrdemServico_CadastroDeOS : PageBase
{
    private Msg msg = new Msg();

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

    public bool ExibirBotesVisitAtividade
    {
        get
        {
            if (Session["ExibirBotesVisitAtividade"] == null)
                return true;
            else
                return (bool)Session["ExibirBotesVisitAtividade"];
        }
        set { Session["ExibirBotesVisitAtividade"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
                string idOS = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                string soVisualizacao = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("sovisu", this.Request));

                if (idOS.ToInt32() > 0)
                {
                    this.ExibirBotesVisitAtividade = false;
                    this.DesbilitarEsconderBotoes();

                    hfId.Value = idOS;
                    this.CarregarOrdemServico(idOS.ToInt32());

                    if (soVisualizacao == "sim")
                        this.DesbilitarEsconderTodosOSBotoes();
                }
                else
                {
                    msg.CriarMensagem("Ordem de Serviço não foi carregada corretamente. Por favor, volte à Pesquisa de OS e tente novamente.", "Erro", MsgIcons.Erro);
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

    private void DesbilitarEsconderTodosOSBotoes()
    {
        detalhamento_visualizacao.Visible = true;
        observacoes_visualizacao.Visible = true;
        observacoes_edicao.Visible = btnEditarObservacaoOS.Visible = btnEditarObservacaoOS.Enabled = btnNovaVisita.Enabled = btnNovaVisita.Visible = btnNovaAtividade.Visible = btnNovaAtividade.Enabled =
            btnNovoArquivoOS.Visible = btnNovoArquivoOS.Enabled = btnRenomearArquivoOS.Enabled = btnRenomearArquivoOS.Visible = btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible =
            btnSalvarOS.Enabled = btnSalvarOS.Visible = btnEncerrarOS.Visible = btnEncerrarOS.Enabled = btnExcluirArquivoOS.Enabled = btnExcluirArquivoOS.Visible = btnAprovarOS.Enabled = btnAprovarOS.Visible = btnAprovarOS.Visible = btnSolicitarAprovacao.Visible = btnSolicitarAdiamentoPrazo.Visible = btnSalvarOS.Visible = btnEncerrarOS.Visible = false;
    }

    #region _______________ Métodos _______________

    private void CarregarCampos()
    {
        this.CarregarTiposOS();
        this.CarregarDepartamentosOS();
        this.CarregarOrgaos();
        this.CarregarResponsaveisOS();
    }

    private void DesbilitarEsconderBotoes()
    {
        detalhamento_visualizacao.Visible = true;
        btnEditarObservacaoOS.Visible = btnEditarObservacaoOS.Enabled = btnNovaVisita.Enabled = btnNovaVisita.Visible = btnNovaAtividade.Visible = btnNovaAtividade.Enabled =
            btnNovoArquivoOS.Visible = btnNovoArquivoOS.Enabled = btnRenomearArquivoOS.Enabled = btnRenomearArquivoOS.Visible = btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible =
            btnSalvarOS.Enabled = btnSalvarOS.Visible = btnEncerrarOS.Visible = btnEncerrarOS.Enabled = btnEncerrarEEnviarPesquisaOS.Enabled = btnEncerrarEEnviarPesquisaOS.Visible =
            btnExcluirArquivoOS.Enabled = btnExcluirArquivoOS.Visible = btnAprovarOS.Enabled = btnAprovarOS.Visible = false;
    }

    private void HabilitarBotoes()
    {
        btnEditarObservacaoOS.Visible = btnEditarObservacaoOS.Enabled = btnNovaVisita.Enabled = btnNovaVisita.Visible = btnNovaAtividade.Visible = btnNovaAtividade.Enabled =
            btnNovoArquivoOS.Visible = btnNovoArquivoOS.Enabled = btnRenomearArquivoOS.Enabled = btnRenomearArquivoOS.Visible = btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible =
            btnSolicitarAdiamentoPrazo.Visible = btnSolicitarAdiamentoPrazo.Enabled = btnSalvarOS.Enabled = btnSalvarOS.Visible = btnEncerrarOS.Visible = btnEncerrarOS.Enabled = btnExcluirArquivoOS.Enabled = btnExcluirArquivoOS.Visible = true;
    }

    private void CarregarTiposOS()
    {
        ddlTipoOS.Items.Clear();

        ddlTipoOS.DataValueField = "Id";
        ddlTipoOS.DataTextField = "Nome";

        IList<TipoOrdemServico> tiposOrdensServico = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

        ddlTipoOS.DataSource = tiposOrdensServico != null ? tiposOrdensServico : new List<TipoOrdemServico>();
        ddlTipoOS.DataBind();

        ddlTipoOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarTiposVisita()
    {
        ddlTipoVisita.Items.Clear();

        ddlTipoVisita.DataValueField = "Id";
        ddlTipoVisita.DataTextField = "Nome";

        IList<TipoVisita> tiposVisitas = TipoVisita.ConsultarTodosOrdemAlfabetica();

        ddlTipoVisita.DataSource = tiposVisitas != null ? tiposVisitas : new List<TipoVisita>();
        ddlTipoVisita.DataBind();

        ddlTipoVisita.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarTiposAtividade()
    {
        ddlTipoAtividade.Items.Clear();

        ddlTipoAtividade.DataValueField = "Id";
        ddlTipoAtividade.DataTextField = "Nome";

        IList<TipoAtividade> tiposAtividade = TipoAtividade.ConsultarTodosOrdemAlfabetica();

        ddlTipoAtividade.DataSource = tiposAtividade != null ? tiposAtividade : new List<TipoAtividade>();
        ddlTipoAtividade.DataBind();

        ddlTipoAtividade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVisitantes()
    {
        ddlVisitante.Items.Clear();

        ddlVisitante.DataValueField = "Id";
        ddlVisitante.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlVisitante.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlVisitante.DataBind();

        ddlVisitante.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarExecutoresAtividade()
    {
        ddlExecutorAtividade.Items.Clear();

        ddlExecutorAtividade.DataValueField = "Id";
        ddlExecutorAtividade.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlExecutorAtividade.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlExecutorAtividade.DataBind();

        ddlExecutorAtividade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarDepartamentosOS()
    {
        ddlDepartamentoOS.Items.Clear();

        ddlDepartamentoOS.DataValueField = "Id";
        ddlDepartamentoOS.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamentoOS.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamentoOS.DataBind();

        ddlDepartamentoOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));

        ddlSetorOS.Items.Clear();

        ddlSetorOS.Items.Insert(0, new ListItem("-- Selecione primeiro o departamento --", "0"));
    }

    private void CarregarSetoresOS()
    {
        ddlSetorOS.Items.Clear();

        ddlSetorOS.DataValueField = "Id";
        ddlSetorOS.DataTextField = "Nome";

        Departamento departamento = Departamento.ConsultarPorId(ddlDepartamentoOS.SelectedValue.ToInt32());

        ddlSetorOS.DataSource = departamento != null && departamento.Setores != null ? departamento.Setores : new List<Setor>();
        ddlSetorOS.DataBind();

        ddlSetorOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarOrgaos()
    {
        ddlOrgaoOS.Items.Clear();

        ddlOrgaoOS.DataValueField = "Id";
        ddlOrgaoOS.DataTextField = "Nome";

        IList<Orgao> orgaos = Orgao.ConsultarTodosOrdemAlfabetica();

        ddlOrgaoOS.DataSource = orgaos != null ? orgaos : new List<Orgao>();
        ddlOrgaoOS.DataBind();

        ddlOrgaoOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarResponsaveisOS()
    {
        ddlResponsavelOS.Items.Clear();

        ddlResponsavelOS.DataValueField = "Id";
        ddlResponsavelOS.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlResponsavelOS.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlResponsavelOS.DataBind();

        ddlResponsavelOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVeiculosReserva()
    {
        ddlVeículoReserva.Items.Clear();

        IList<Veiculo> veiculos = Veiculo.ConsultarQueODepartamentoUsaOrdemAlfabetica(ddlDepartamentoOS.SelectedValue.ToInt32());

        if (veiculos != null && veiculos.Count > 0)
        {
            foreach (Veiculo veiculo in veiculos)
            {
                ddlVeículoReserva.Items.Add(new ListItem(veiculo.Descricao + " - " + veiculo.Placa, veiculo.Id.ToString()));
            }
        }

        ddlVeículoReserva.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarOrdemServico(int id)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(id);

        if (ordem != null)
        {
            chkOSAtivo.Checked = ordem.Ativo;
            chkOsComFaturamento.Checked = !ordem.SemCusto;
            tbxCodigoOS.Text = ordem.Codigo;
            tbxDataOS.Text = ordem.Data.ToShortDateString();
            txtCliente.Text = (ordem.Pedido != null ? ordem.Pedido.GetNomeCliente : "");
            tbxPedidoOS.Text = (ordem.Pedido != null ? ordem.Pedido.Codigo + " - " + ordem.Pedido.Data.ToShortDateString() + " - " + ordem.Pedido.GetDescricaoTipo : "");

            ddlTipoOS.SelectedValue = ordem.Tipo.Id.ToString();

            tbxDescricaoOS.Text = ordem.Descricao;
            tbxPrazoPadraoOS.Text = ordem.PrazoPadrao.ToShortDateString();
            tbxPrazoLegalOS.Text = ordem.GetPrazoLegal;
            tbxPrazoDiretoriaOS.Text = ordem.GetPrazoDiretoria;

            ddlDepartamentoOS.SelectedValue = ordem.Setor != null ? ordem.Setor.Departamento.Id.ToString() : "0";

            this.CarregarSetoresOS();

            ddlSetorOS.SelectedValue = ordem.Setor != null ? ordem.Setor.Id.ToString() : "0";

            ddlOrgaoOS.SelectedValue = ordem.Orgao != null ? ordem.Orgao.Id.ToString() : "0";
            tbxNumeroProcessoOrgaoOS.Text = ordem.NumeroProcessoOrgao;

            ddlResponsavelOS.SelectedValue = ordem.Responsavel != null ? ordem.Responsavel.Id.ToString() : "0";

            if (ordem.Responsavel != null && this.FuncionarioLogado.Id == ordem.Responsavel.Id)
            {
                this.ExibirBotesVisitAtividade = true;
                this.HabilitarBotoes();
            }

            txtOSMAtriz.Text = "Nenhuma OS vinculada";
            if (ordem.OsMatriz != null && ordem.OsMatriz.Id > 0)
                txtOSMAtriz.Text = ordem.OsMatriz.GetDescricaoOSMatriz;

            tbxEstadoOS.Text = ordem.GetDescricaoDoStatusDaOS;

            btnEncerrarOS.Enabled = btnEncerrarOS.Visible = btnEncerrarEEnviarPesquisaOS.Enabled = btnEncerrarEEnviarPesquisaOS.Visible = false;

            //Somente o responsavel da OS pode solicitar adiamento de prazo e aprovação da OS
            btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible = btnSolicitarAdiamentoPrazo.Visible = btnSolicitarAdiamentoPrazo.Enabled = ordem.Responsavel != null && this.FuncionarioLogado.Id == ordem.Responsavel.Id;

            //Carregando os botoes conforme as permissoes do usuario
            if (ordem.Status == OrdemServico.APROVADA)
            {
                btnEncerrarOS.Enabled = btnEncerrarOS.Visible = true;
                btnEncerrarEEnviarPesquisaOS.Enabled = btnEncerrarEEnviarPesquisaOS.Visible = PerguntaPesquisaSatisfacao.PossuiAlgumaPerguntaAtivaCadastrada();
                btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible = btnAprovarOS.Enabled = btnAprovarOS.Visible = false;
            }
            else
            {
                Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.FuncionarioLogado.Id, this.FuncaoLogada.Id);
                //Usuário aprova os
                if ((permissao.AprovaOS == Permissao.TODOS) || (permissao.AprovaOS == Permissao.DEPARTAMENTO && ordem.Setor.Departamento.Id == permissao.Funcao.Setor.Departamento.Id) || (permissao.AprovaOS == Permissao.SETOR && ordem.Setor.Id == permissao.Funcao.Setor.Id) || (permissao.AprovaOS == Permissao.RESPONSAVEL && ordem.Responsavel.Id == this.FuncionarioLogado.Id))
                {
                    btnAprovarOS.Visible = btnAprovarOS.Enabled = true;
                    btnEditarObservacaoOS.Visible = btnEditarObservacaoOS.Enabled = btnNovoArquivoOS.Visible = btnNovoArquivoOS.Enabled = btnExcluirArquivoOS.Enabled = btnExcluirArquivoOS.Visible = btnRenomearArquivoOS.Enabled = btnRenomearArquivoOS.Visible = btnSalvarOS.Enabled = btnSalvarOS.Visible = true;
                }
                else
                {
                    btnAprovarOS.Visible = btnAprovarOS.Enabled = false;
                }

                //se o usuario logado for gestor ou diretor do departamento da OS o mesmo pode alterar o responsavel da mesma
                if ((permissao.AdiaPrazoLegalOS == Permissao.TODOS) || (permissao.AdiaPrazoLegalOS == Permissao.DEPARTAMENTO && ordem.Setor.Departamento.Id == permissao.Funcao.Setor.Departamento.Id) || (permissao.AdiaPrazoLegalOS == Permissao.SETOR && ordem.Setor.Id == permissao.Funcao.Setor.Id) || (permissao.AdiaPrazoLegalOS == Permissao.RESPONSAVEL && ordem.Responsavel.Id == this.FuncionarioLogado.Id))
                {
                    ddlResponsavelOS.Enabled = true;
                    btnSalvarOS.Enabled = btnSalvarOS.Visible = true;
                }
            }

            //Carregando os detalhamentos da OS 
            Detalhamento ultimoDetalhamentoDaOS = ordem.GetUltimoDelhamento;
            tbxDetalhamentoVisualizacao.Text = ultimoDetalhamentoDaOS != null ? ultimoDetalhamentoDaOS.Conteudo : "";

            //Carregando as observacoes da OS
            if (ordem.Observacoes != null && ordem.Observacoes.Count > 0)
            {
                observacoes_edicao.Visible = false;
                observacoes_visualizacao.Visible = true;
                Detalhamento ultimaObservacaoDaOS = ordem.GetUltimaObservacao;
                tbxObservacaoVisualizacao.Text = ultimaObservacaoDaOS != null ? ultimaObservacaoDaOS.Conteudo : "";
            }
            else
            {
                observacoes_edicao.Visible = true;
                observacoes_visualizacao.Visible = false;
            }

            //OS esta encerrada
            if (ordem.IsEncerrada)
            {
                this.DesbilitarEsconderBotoes();
                //Habilitar os botões de alteração de arquivos
                btnNovoArquivoOS.Visible = btnNovoArquivoOS.Enabled = btnExcluirArquivoOS.Enabled = btnExcluirArquivoOS.Visible = btnRenomearArquivoOS.Enabled = btnRenomearArquivoOS.Visible = btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible =
                    tbxProtocoloEncerramentoExibicao.Enabled = tbxVisualizacaoObservacoesEncerramento.Enabled =
                    btnSalvarOS.Visible = btnSalvarOS.Enabled =
                    ordem.GetFuncionarioPodeAlterarProtocoloOuArquivos(this.FuncionarioLogado);
                this.ExibirBotesVisitAtividade = false;
                btnSolicitarAdiamentoPrazo.Visible = btnSolicitarAdiamentoPrazo.Enabled = false;
                tbxDataEncerramentoExibicao.Text = ordem.DataEncerramento.ToShortDateString();
                detalhes_os_encerrada.Visible = true;
                campo_exibicao_protocolo_encerramento.Visible = ordem.PossuiProtocolo;
                campo_exibicao_observacao_encerramento.Visible = !ordem.PossuiProtocolo;
                if (ordem.PossuiProtocolo)
                {
                    tbxProtocoloEncerramentoExibicao.Text = ordem.ProtocoloOficioEncerramento;
                    tbxDataProtocoEncerramentoExibicao.Text = ordem.DataProtocoloEncerramento.ToShortDateString();
                }
                else
                {
                    tbxVisualizacaoObservacoesEncerramento.Text = ordem.ProtocoloOficioEncerramento;
                }
            }

            this.CarregarVisitas(ordem);
            this.CarregarAtividades(ordem);

            //OS vinculadas
            if (ordem.OssVinculadas != null && ordem.OssVinculadas.Count > 0)
            {
                oss_vinculadas.Visible = true;
                this.CarregarGridOSsVinculadas(ordem);
            }
            else
            {
                oss_vinculadas.Visible = false;
            }

            this.CarregarArvoreArquivosOS(ordem);
            this.CarregarDadosDoCliente(ordem);
        }
    }

    private void CarregarDadosDoCliente(OrdemServico ordem)
    {
        Cliente cliente = ordem.Pedido != null && ordem.Pedido.Cliente != null ? ordem.Pedido.Cliente : null;

        if (cliente != null)
        {
            tbxNomeCliente.Text = cliente.NomeRazaoSocial;
            tbxTelefone1Cliente.Text = cliente.Telefone1;
            tbxTelefone2Cliente.Text = cliente.Telefone2;
            tbxEmailCliente.Text = cliente.Email;
            tbxEnderecoCliente.Text = cliente.GetDescricaoCompletaDoEndereco;
            tbxCepCliente.Text = cliente.GetCepEndereco;
            tbxCidadeEstadoCliente.Text = cliente.GetCidade + "/" + cliente.GetSiglaEstado;
        }
    }

    private void CarregarGridOSsVinculadas(OrdemServico ordem)
    {
        gdvOrdensVinculadas.DataSource = ordem.OssVinculadas;
        gdvOrdensVinculadas.DataBind();
    }

    private void CarregarVisitas(OrdemServico ordem)
    {
        gdvVisitas.DataSource = ordem != null && ordem.Visitas != null && ordem.Visitas.Count > 0 ? ordem.Visitas : new List<Visita>();
        gdvVisitas.DataBind();
    }

    private void CarregarAtividades(OrdemServico ordem)
    {
        gdvAtividades.DataSource = ordem != null && ordem.Atividades != null && ordem.Atividades.Count > 0 ? ordem.Atividades : new List<Atividade>();
        gdvAtividades.DataBind();
    }

    private void CarregarArvoreArquivosOS(OrdemServico ordem)
    {
        TreeNode noSelecionado = trvAnexosOS.SelectedNode;
        trvAnexosOS.Nodes.Clear();

        if (ordem != null && ordem.Id > 0)
        {
            Pedido pedido = ordem.Pedido;

            if (pedido != null)
            {
                TreeNode noPedido = new TreeNode("Pedido " + pedido.Codigo, pedido.Id.ToString());
                noPedido.ImageUrl = "../imagens/icone_pasta.png";

                if (pedido.Arquivos != null && pedido.Arquivos.Count > 0)
                {
                    foreach (Arquivo arquivoPedido in pedido.Arquivos)
                    {
                        TreeNode noArquivoPedido = new TreeNode()
                        {
                            Text = arquivoPedido.Nome,
                            Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                            ImageUrl = "../imagens/icone_tarefa.png"
                        };

                        noPedido.ChildNodes.Add(noArquivoPedido);
                    }
                }

                TreeNode noPai = new TreeNode("OS " + ordem.Codigo, ordem.Id.ToString());
                noPai.ImageUrl = "../imagens/icone_pasta.png";

                //adicionando arquivos da OS na arvore
                if (ordem.Arquivos != null && ordem.Arquivos.Count > 0)
                {
                    foreach (Arquivo arquivoPedido in ordem.Arquivos)
                    {
                        TreeNode noArquivoPedido = new TreeNode()
                        {
                            Text = arquivoPedido.Nome,
                            Value = "ARQPED_" + arquivoPedido.Id.ToString(),
                            ImageUrl = "../imagens/icone_tarefa.png"
                        };

                        noPai.ChildNodes.Add(noArquivoPedido);
                    }
                }

                //Adicionando as visitas na arvore
                if (ordem.Visitas != null && ordem.Visitas.Count > 0)
                {
                    foreach (Visita visita in ordem.Visitas)
                    {
                        if (visita.Arquivos != null && visita.Arquivos.Count > 0)
                        {
                            TreeNode noVisita = new TreeNode("Visita " + visita.DataInicio.ToShortDateString(), visita.Id.ToString());
                            noVisita.ImageUrl = "../imagens/icone_pasta.png";

                            this.AdicionarArquivosNaArvore(ref noVisita, visita.Arquivos);

                            noPai.ChildNodes.Add(noVisita);
                        }
                    }
                }

                //adicionando atividades na arvore
                if (ordem.Atividades != null && ordem.Atividades.Count > 0)
                {
                    foreach (Atividade atividade in ordem.Atividades)
                    {
                        if (atividade.Arquivos != null && atividade.Arquivos.Count > 0)
                        {
                            TreeNode noAtividade = new TreeNode("Atividade " + atividade.Data.ToShortDateString(), atividade.Id.ToString());
                            noAtividade.ImageUrl = "../imagens/icone_pasta.png";

                            this.AdicionarArquivosNaArvore(ref noAtividade, atividade.Arquivos);

                            noPai.ChildNodes.Add(noAtividade);
                        }
                    }
                }

                noPedido.ChildNodes.Add(noPai);

                trvAnexosOS.Nodes.Add(noPedido);
            }


        }

        trvAnexosOS.ExpandAll();
    }

    private void AdicionarArquivosNaArvore(ref TreeNode noPai, IList<Arquivo> arquivos)
    {
        foreach (Arquivo arquivo in arquivos)
        {
            TreeNode noArquivo = new TreeNode()
            {
                Text = arquivo.Nome,
                Value = "ARQPED_" + arquivo.Id.ToString(),
                ImageUrl = "../imagens/icone_tarefa.png"
            };

            noPai.ChildNodes.Add(noArquivo);
        }
    }

    public string BindConteudoDetalhamento(object o)
    {
        Detalhamento detalhamento = (Detalhamento)o;
        return detalhamento.Conteudo;
    }

    private void CarregarHistoricoDetalhamentosOS()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        IList<Detalhamento> detalhamentos = ordem != null && ordem.Detalhamentos != null ? ordem.Detalhamentos.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Esta ordem de serviço não possui detalhamentos salvos para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void CarregarHistoricoObservacoesOS()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        IList<Detalhamento> detalhamentos = ordem != null && ordem.Observacoes != null ? ordem.Observacoes.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Esta ordem de serviço não possui históricos salvas para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void ExcluirVisita(GridViewDeleteEventArgs e)
    {
        Visita visita = Visita.ConsultarPorId(gdvVisitas.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (visita != null)
        {
            if (visita.DataFim < DateTime.Now)
            {
                msg.CriarMensagem("Não é possível excluir visitas que já ocorreram.", "Informação", MsgIcons.Informacao);
                return;
            }

            //this.IncluirRegistroExclusaoOsNoDetalhamentoDoPedido(ordem);

            if (visita.Excluir())
                msg.CriarMensagem("Visita excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void ExcluirAtividade(GridViewDeleteEventArgs e)
    {
        Atividade atividade = Atividade.ConsultarPorId(gdvAtividades.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (atividade != null)
        {
            if (atividade.Data < DateTime.Now)
            {
                msg.CriarMensagem("Não é possível excluir atividades que já ocorreram.", "Informação", MsgIcons.Informacao);
                return;
            }

            //this.IncluirRegistroExclusaoOsNoDetalhamentoDoPedido(ordem);

            if (atividade.Excluir())
                msg.CriarMensagem("Atividade excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregarArvoreArquivosVisita(Visita visita)
    {
        TreeNode noSelecionado = trvArquivosVisita.SelectedNode;
        trvArquivosVisita.Nodes.Clear();

        if (visita != null && visita.Id > 0)
        {
            //Adicionando o pedido da visita
            Pedido pedido = visita.GetPedido;

            if (pedido != null)
            {
                TreeNode noPedido = new TreeNode("Pedido " + pedido.Codigo, pedido.Id.ToString());
                noPedido.ImageUrl = "../imagens/icone_pasta.png";

                if (pedido.Arquivos != null && pedido.Arquivos.Count > 0)
                {
                    foreach (Arquivo arquivoPedido in pedido.Arquivos)
                    {
                        TreeNode noArquivoPedido = new TreeNode()
                        {
                            Text = arquivoPedido.Nome,
                            Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                            ImageUrl = "../imagens/icone_tarefa.png"
                        };

                        noPedido.ChildNodes.Add(noArquivoPedido);
                    }
                }

                //Adicionando a OS da visita
                OrdemServico ordem = visita.GetOS;

                if (ordem != null)
                {
                    TreeNode noOS = new TreeNode("OS " + ordem.Codigo, ordem.Id.ToString());
                    noOS.ImageUrl = "../imagens/icone_pasta.png";

                    if (ordem.Arquivos != null && ordem.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivoPedido in ordem.Arquivos)
                        {
                            TreeNode noArquivoPedido = new TreeNode()
                            {
                                Text = arquivoPedido.Nome,
                                Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noOS.ChildNodes.Add(noArquivoPedido);
                        }
                    }



                    TreeNode noPai = new TreeNode("Visita " + visita.DataInicio.ToShortDateString(), visita.Id.ToString());
                    noPai.ImageUrl = "../imagens/icone_pasta.png";

                    //adicionando arquivos da OS na arvore
                    if (visita.Arquivos != null && visita.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivoPedido in visita.Arquivos)
                        {
                            TreeNode noArquivoPedido = new TreeNode()
                            {
                                Text = arquivoPedido.Nome,
                                Value = "ARQPED_" + arquivoPedido.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noPai.ChildNodes.Add(noArquivoPedido);
                        }
                    }

                    noOS.ChildNodes.Add(noPai);
                    noPedido.ChildNodes.Add(noOS);
                }


                trvArquivosVisita.Nodes.Add(noPedido);

            }

        }

        trvArquivosVisita.ExpandAll();
    }

    private void NovaVisita()
    {
        mvFormVisita.ActiveViewIndex = 0;
        hfIdVisita.Value = tbxDescricaoVisita.Text = tbxDataInicioVisita.Text = tbxDataFimVisita.Text = tbxVisualizarDetalhamentoVisita.Text = editDetalhamentoVisita.Content = tbxDataInicioReserva.Text = tbxDataFimReserva.Text = "";

        tbxPedidoVisita.Text = tbxPedidoOS.Text;

        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        tbxOSVisita.Text = ordem != null ? ordem.Codigo + " - " + ordem.Data.ToShortDateString() + " - " + ordem.GetDescricaoDepartamento + " - " + ordem.GetDescricaoSetor : "";

        this.CarregarTiposVisita();
        this.CarregarVisitantes();
        ddlVisitante.SelectedValue = ordem != null && ordem.Responsavel != null ? ordem.Responsavel.Id.ToString() : "0";
        detalhamento_edicao_visita.Visible = detalhamento_visualizacao_visita.Visible = false;

        this.CarregarVeiculosReserva();

        this.CarregarArvoreArquivosVisita(null);

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>SelecionarPrimeiraTab();</script>", false);

    }

    private void CarregarVisita(int id)
    {
        Visita visita = Visita.ConsultarPorId(id);

        if (visita != null)
        {
            mvFormVisita.ActiveViewIndex = 0;
            hfIdVisita.Value = visita.Id.ToString();
            ckbAtivoVisita.Checked = visita.Ativo;
            tbxDataInicioVisita.Text = visita.DataInicio.ToShortDateString();
            tbxDataFimVisita.Text = visita.DataFim.ToShortDateString();

            tbxPedidoVisita.Text = tbxPedidoOS.Text;

            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

            tbxOSVisita.Text = ordem != null ? ordem.Codigo + " - " + ordem.Data.ToShortDateString() + " - " + ordem.GetDescricaoDepartamento + " - " + ordem.GetDescricaoSetor : "";

            this.CarregarTiposVisita();

            ddlTipoVisita.SelectedValue = visita.TipoVisita != null ? visita.TipoVisita.Id.ToString() : "0";
            tbxDescricaoVisita.Text = visita.Descricao;

            this.CarregarVisitantes();

            ddlVisitante.SelectedValue = visita.Visitante != null ? visita.Visitante.Id.ToString() : "0";

            //Carregando os detalhamentos
            if (visita.Detalhamentos != null && visita.Detalhamentos.Count > 0)
            {
                detalhamento_edicao_visita.Visible = false;
                detalhamento_visualizacao_visita.Visible = true;

                Detalhamento ultimoDetalhamentoDaVisita = visita.GetUltimoDelhamento;

                tbxVisualizarDetalhamentoVisita.Text = ultimoDetalhamentoDaVisita != null ? ultimoDetalhamentoDaVisita.Conteudo : "";
            }
            else
            {
                detalhamento_edicao_visita.Visible = true;
                detalhamento_visualizacao_visita.Visible = false;
            }

            //Carregando a reserva de veiculos
            this.CarregarVeiculosReserva();

            ddlVeículoReserva.SelectedValue = visita.Reserva != null && visita.Reserva.Veiculo != null ? visita.Reserva.Veiculo.Id.ToString() : "0";
            tbxDataInicioReserva.Text = visita.Reserva != null ? visita.Reserva.DataInicio.ToShortDateString() : "";
            tbxDataFimReserva.Text = visita.Reserva != null ? visita.Reserva.DataFim.ToShortDateString() : "";

            if (visita.Reserva != null)
            {
                if (visita.Reserva.DataInicio.ToString("HH:mm") == "00:00")
                    ddlPeriodoInicioReserva.SelectedValue = "M";
                else
                    ddlPeriodoInicioReserva.SelectedValue = "T";

                if (visita.Reserva.DataFim.ToString("HH:mm") == "11:59")
                    ddlPeriodoFimReserva.SelectedValue = "M";
                else
                    ddlPeriodoFimReserva.SelectedValue = "T";

            }

            this.CarregarArvoreArquivosVisita(visita);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>SelecionarPrimeiraTab();</script>", false);

            lblCadastroVisita_ModalPopupExtender.Show();
        }
    }

    private void ExcluirArquivosVisita()
    {
        if (trvArquivosVisita.SelectedValue.Contains("ARQPED_"))
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosVisita.SelectedNode.Value.Split('_')[1].ToInt32());

            arquivo.Excluir();

            msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void SalvarVisita()
    {
        if (ddlVeículoReserva.SelectedValue.ToInt32() <= 0)
        {
            msg.CriarMensagem("Selecione um veículo para reserva da visita.", "Informação", MsgIcons.Informacao);
            return;
        }

        //Setando a data de inicio da reserva de acordo com o periodo escolhido
        DateTime dataInicioReserva = tbxDataInicioReserva.Text.ToDateTime();

        if (ddlPeriodoInicioReserva.SelectedValue == "T")
            dataInicioReserva = new DateTime(dataInicioReserva.Year, dataInicioReserva.Month, dataInicioReserva.Day, 12, 0, 0);

        //Setando a data de fim da reserva de acordo com o periodo escolhido
        DateTime dataFimReserva = tbxDataFimReserva.Text.ToDateTime();

        if (ddlPeriodoFimReserva.SelectedValue == "T")
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 23, 59, 59);
        else
            dataFimReserva = new DateTime(dataFimReserva.Year, dataFimReserva.Month, dataFimReserva.Day, 11, 59, 59);

        if (dataInicioReserva > dataFimReserva)
        {
            msg.CriarMensagem("A data de início da reserva não deve ser maior que a data de fim da mesma.", "Informação", MsgIcons.Informacao);
            return;
        }


        Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

        Reserva aux = visita != null && visita.Reserva != null ? visita.Reserva : new Reserva();

        if (Reserva.ExisteAlgumaReservaParaOVeiculoNestePeriodoDeOutraVisita(ddlVeículoReserva.SelectedValue.ToInt32(), dataInicioReserva, dataFimReserva, aux))
        {
            msg.CriarMensagem("Já existe uma reserva para este veículo aprovada neste período.", "Informação", MsgIcons.Informacao);
            return;
        }


        if (visita == null)
            visita = new Visita();

        visita.Ativo = ckbAtivoVisita.Checked;
        visita.DataInicio = tbxDataInicioVisita.Text.ToDateTime();
        visita.DataFim = tbxDataFimVisita.Text.ToDateTime();

        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        visita.OrdemServico = ordem;
        visita.TipoVisita = TipoVisita.ConsultarPorId(ddlTipoVisita.SelectedValue.ToInt32());
        visita.Descricao = tbxDescricaoVisita.Text;

        visita.Visitante = Funcionario.ConsultarPorId(ddlVisitante.SelectedValue.ToInt32());

        //se  detalhamento esta em modo de edição, salvar um novo detalhamento para o pedido
        if (detalhamento_edicao_visita.Visible == true && hfIdVisita.Value.ToInt32() > 0)
        {
            if (visita.Detalhamentos == null)
                visita.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            detalhamento.Conteudo = editDetalhamentoVisita.Content;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            detalhamento = detalhamento.Salvar();

            visita.Detalhamentos.Add(detalhamento);

            tbxVisualizarDetalhamentoVisita.Text = detalhamento.Conteudo;

            detalhamento_edicao_visita.Visible = false;
            detalhamento_visualizacao_visita.Visible = true;
        }

        //Carregando os detalhamentos
        if (visita.Detalhamentos != null && visita.Detalhamentos.Count > 0)
        {
            detalhamento_edicao_visita.Visible = false;
            detalhamento_visualizacao_visita.Visible = true;

            Detalhamento ultimoDetalhamentoDaVisita = visita.GetUltimoDelhamento;

            tbxVisualizarDetalhamentoVisita.Text = ultimoDetalhamentoDaVisita != null ? ultimoDetalhamentoDaVisita.Conteudo : "";
        }
        else
        {
            detalhamento_edicao_visita.Visible = true;
            detalhamento_visualizacao_visita.Visible = false;
        }

        //Salvando a Reserva do Veiculo
        Reserva reserva = visita.Reserva != null ? visita.Reserva : new Reserva();

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeículoReserva.SelectedValue.ToInt32());

        reserva.Veiculo = veiculo;

        if (reserva == null || reserva.Id == 0)
        {
            reserva.Status = veiculo != null && veiculo.Gestor != null && veiculo.Gestor.Id == this.FuncionarioLogado.Id ? Reserva.APROVADA : Reserva.AGUARDANDO;
        }

        bool houveMudancaNaReserva = false;

        if (reserva != null && reserva.Id > 0)
        {
            if (reserva.DataInicio != dataInicioReserva || reserva.DataFim != dataFimReserva || reserva.Responsavel.Id != ddlVisitante.SelectedValue.ToInt32())
            {
                houveMudancaNaReserva = true;
                this.EnviarEmailAlteracoesNaReservaDeVeiculos(reserva, dataInicioReserva, dataFimReserva);
            }

        }

        reserva.DataInicio = dataInicioReserva;
        reserva.DataFim = dataFimReserva;
        reserva.Descricao = "Reserva para Visita da OS " + ordem.Codigo;
        reserva.Responsavel = Funcionario.ConsultarPorId(ddlVisitante.SelectedValue.ToInt32());
        reserva.TipoReservaVeiculo = TipoReservaVeiculo.ConsultarTipoVisitaParaOS();

        if (houveMudancaNaReserva && reserva.Status == Reserva.APROVADA)
            reserva.Status = Reserva.AGUARDANDO;

        reserva = reserva.Salvar();
        visita = visita.Salvar();

        visita.Reserva = reserva;
        reserva.Visita = visita;

        reserva = reserva.Salvar();
        visita = visita.Salvar();

        if (reserva.Status != Reserva.APROVADA && !houveMudancaNaReserva)
        {
            this.EnviarEmailSolicitacaoReservaDeVeiculoParaOGestorDoVeiculo(reserva);
        }

        if (hfIdVisita.Value.ToInt32() <= 0)
            this.IncluirRegistroInclusaoVisitaNoDetalhamentoDaOS(visita);

        hfIdVisita.Value = visita.Id.ToString();

        msg.CriarMensagem("Visita salva com sucesso", "Sucesso", MsgIcons.Sucesso);

    }

    private void EnviarEmailAlteracoesNaReservaDeVeiculos(Reserva reserva, DateTime dataInicioReserva, DateTime dataFimReserva)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Alteração de Reserva de Veículo";
        mail.BodyHtml = true;

        Veiculo veiculo = Veiculo.ConsultarPorId(ddlVeículoReserva.SelectedValue.ToInt32());

        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        Funcionario gestorVeiculo = veiculo.Gestor;

        String dadosVisita = Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + reserva.Id.ToString() + "&visitOS=sim");

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Alteração de Reserva de Veículo</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A reserva para o Veículo " + veiculo.Descricao + " - " + veiculo.Placa + (gestorVeiculo != null ? ", gerido pelo usuário " + gestorVeiculo.NomeRazaoSocial : "") + @", foi alterada no Sistema Ambientalis Manager.<br /><br />  
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
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + reserva.Descricao + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Ordem de Serviço:</strong>
                    </div>
                    <div>
                        " + tbxOSVisita.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Pedido:</strong>
                    </div>
                    <div>
                        " + tbxPedidoVisita.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Cliente:</strong>
                    </div>
                    <div>
                        " + ordem.GetNomeCliente + @"
                    </div>                    
                </td>
            </tr>                         
        </table> 
        <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:10px;'>
            <a href='http://ambientalismanager.com.br/Reservas/CadastroDeReserva.aspx" + dadosVisita + @"'>Clique aqui</a> para responder a esta nova solicitação de reserva de veículo.</div>              
    </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Alteração de Reserva de Veículo", conteudoemail);

        mail.AdicionarDestinatario(gestorVeiculo.EmailCorporativo);

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            mail.EnviarAutenticado("Alteração reserva veículos", this.FuncionarioLogado, ordem.Pedido, 587, false);
        }
    }

    private void EnviarEmailSolicitacaoReservaDeVeiculoParaOGestorDoVeiculo(Reserva reserva)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Solicitação de Reserva de Veículo";
        mail.BodyHtml = true;

        Veiculo veiculo = reserva.Veiculo;

        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        Funcionario gestorVeiculo = veiculo.Gestor;

        String dadosVisita = Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + reserva.Id.ToString() + "&visitOS=sim");

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Solicitação de Reserva de Veículo</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        Uma solicitação de reserva para o Veículo " + veiculo.Descricao + " - " + veiculo.Placa + (gestorVeiculo != null ? ", gerido pelo usuário " + gestorVeiculo.NomeRazaoSocial : "") + @", foi realizada no Sistema Ambientalis Manager.<br /><br />  
        Dados da Reserva:      
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Início:</strong>
                    </div>
                    <div>
                        " + reserva.DataInicio.ToString("dd/MM/yyyy HH:mm") + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Fim:</strong>
                    </div>
                    <div>
                        " + reserva.DataFim.ToString("dd/MM/yyyy HH:mm") + @"
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
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + reserva.Descricao + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Ordem de Serviço:</strong>
                    </div>
                    <div>
                        " + tbxOSVisita.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Pedido:</strong>
                    </div>
                    <div>
                        " + tbxPedidoVisita.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Cliente:</strong>
                    </div>
                    <div>
                        " + ordem.GetNomeCliente + @"
                    </div>                    
                </td>
            </tr>                         
        </table> 
        <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:10px;'>
            <a href='http://ambientalismanager.com.br/Reservas/CadastroDeReserva.aspx" + dadosVisita + @"'>Clique aqui</a> para responder a esta solicitação de reserva de veículo.</div>              
    </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Solicitação de Reserva de Veículo", conteudoemail);

        mail.AdicionarDestinatario(gestorVeiculo.EmailCorporativo);

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            mail.EnviarAutenticado("Solicitação de reserva de veículo", this.FuncionarioLogado, ordem.Pedido, 587, false);
        }
    }

    private void IncluirRegistroInclusaoVisitaNoDetalhamentoDaOS(Visita visita)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new List<Detalhamento>();

        Detalhamento detalhamento = new Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";

        detalhamento.Conteudo += @"<br /><br /> Incluída Visita com início em " + visita.DataInicio.ToShortDateString() + " e término em " + visita.DataFim.ToShortDateString() + ". Visitante: " + visita.Visitante.NomeRazaoSocial + @"<br /> Descrição:<br />" + visita.Descricao;

        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

        detalhamento = detalhamento.Salvar();

        ordem.Detalhamentos.Add(detalhamento);

        ordem = ordem.Salvar();

        tbxDetalhamentoVisualizacao.Text = detalhamento.Conteudo;
    }

    private void IncluirRegistroInclusaoAtividadeNoDetalhamentoDaOS(Atividade atividade)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new List<Detalhamento>();

        Detalhamento detalhamento = new Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";

        detalhamento.Conteudo += @"<br /><br /> Incluída Atividade realizada em " + atividade.Data.ToShortDateString() + ". Executor: " + atividade.Executor.NomeRazaoSocial + @"<br /> Descrição:<br />" + atividade.Descricao;

        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

        detalhamento = detalhamento.Salvar();

        ordem.Detalhamentos.Add(detalhamento);

        ordem = ordem.Salvar();

        tbxDetalhamentoVisualizacao.Text = detalhamento.Conteudo;
    }

    private void CarregarHistoricoDetalhamentosVisita()
    {
        Visita vsita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

        IList<Detalhamento> detalhamentos = vsita != null && vsita.Detalhamentos != null ? vsita.Detalhamentos.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Esta visita não possui detalhamentos salvos para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void CarregarAtividade(int id)
    {
        Atividade atividade = Atividade.ConsultarPorId(id);

        if (atividade != null)
        {
            hfIdAtividade.Value = atividade.Id.ToString();
            ckbAtivoAtividade.Checked = atividade.Ativo;
            tbxDataAtividade.Text = atividade.Data.ToShortDateString();

            tbxPedidoAtividade.Text = tbxPedidoOS.Text;

            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

            tbxOSAtividade.Text = ordem != null ? ordem.Codigo + " - " + ordem.Data.ToShortDateString() + " - " + ordem.GetDescricaoDepartamento + " - " + ordem.GetDescricaoSetor : "";

            this.CarregarTiposAtividade();

            ddlTipoAtividade.SelectedValue = atividade.TipoAtividade != null ? atividade.TipoAtividade.Id.ToString() : "0";

            tbxDescricaoAtividade.Text = atividade.Descricao;

            this.CarregarExecutoresAtividade();

            ddlExecutorAtividade.SelectedValue = atividade.Executor != null ? atividade.Executor.Id.ToString() : "0";

            //Carregando os detalhamentos
            if (atividade.Detalhamentos != null && atividade.Detalhamentos.Count > 0)
            {
                edicao_detalhamento_atividade.Visible = false;
                visualizacao_detalhamento_atividade.Visible = true;

                Detalhamento ultimoDetalhamentoDaAtividade = atividade.GetUltimoDelhamento;

                tbxVisualizarDetalhamentoAtividade.Text = ultimoDetalhamentoDaAtividade != null ? ultimoDetalhamentoDaAtividade.Conteudo : "";
            }
            else
            {
                edicao_detalhamento_atividade.Visible = true;
                visualizacao_detalhamento_atividade.Visible = false;
            }

            this.CarregarArvoreArquivosAtividade(atividade);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>SelecionarPrimeiraTabAtividades();</script>", false);

            lblCadastroAtividade_ModalPopupExtender.Show();
        }
    }

    private void CarregarArvoreArquivosAtividade(Atividade atividade)
    {
        TreeNode noSelecionado = trvArquivosAtividade.SelectedNode;
        trvArquivosAtividade.Nodes.Clear();

        if (atividade != null && atividade.Id > 0)
        {
            //Adicionando o pedido da visita
            Pedido pedido = atividade.GetPedido;

            if (pedido != null)
            {
                TreeNode noPedido = new TreeNode("Pedido " + pedido.Codigo, pedido.Id.ToString());
                noPedido.ImageUrl = "../imagens/icone_pasta.png";

                if (pedido.Arquivos != null && pedido.Arquivos.Count > 0)
                {
                    foreach (Arquivo arquivoPedido in pedido.Arquivos)
                    {
                        TreeNode noArquivoPedido = new TreeNode()
                        {
                            Text = arquivoPedido.Nome,
                            Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                            ImageUrl = "../imagens/icone_tarefa.png"
                        };

                        noPedido.ChildNodes.Add(noArquivoPedido);
                    }
                }

                //Adicionando a OS da visita
                OrdemServico ordem = atividade.GetOS;

                if (ordem != null)
                {
                    TreeNode noOS = new TreeNode("OS " + ordem.Codigo, ordem.Id.ToString());
                    noOS.ImageUrl = "../imagens/icone_pasta.png";

                    if (ordem.Arquivos != null && ordem.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivoPedido in ordem.Arquivos)
                        {
                            TreeNode noArquivoPedido = new TreeNode()
                            {
                                Text = arquivoPedido.Nome,
                                Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noOS.ChildNodes.Add(noArquivoPedido);
                        }
                    }

                    TreeNode noPai = new TreeNode("Atividade " + atividade.Data.ToShortDateString(), atividade.Id.ToString());
                    noPai.ImageUrl = "../imagens/icone_pasta.png";

                    //adicionando arquivos da Atividade na arvore
                    if (atividade.Arquivos != null && atividade.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivo in atividade.Arquivos)
                        {
                            TreeNode noArquivo = new TreeNode()
                            {
                                Text = arquivo.Nome,
                                Value = "ARQPED_" + arquivo.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noPai.ChildNodes.Add(noArquivo);
                        }
                    }

                    noOS.ChildNodes.Add(noPai);
                    noPedido.ChildNodes.Add(noOS);
                }

                trvArquivosAtividade.Nodes.Add(noPedido);
            }

        }

        trvArquivosAtividade.ExpandAll();
    }

    private void NovaAtividade()
    {
        mvFormAtividade.ActiveViewIndex = 0;
        ckbAtivoAtividade.Checked = true;

        hfIdAtividade.Value = tbxDataAtividade.Text = tbxDescricaoAtividade.Text = editDetalhamentoAtividade.Content = tbxVisualizarDetalhamentoAtividade.Text = "";

        tbxPedidoAtividade.Text = tbxPedidoOS.Text;

        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        tbxOSAtividade.Text = ordem != null ? ordem.Codigo + " - " + ordem.Data.ToShortDateString() + " - " + ordem.GetDescricaoDepartamento + " - " + ordem.GetDescricaoSetor : "";

        this.CarregarTiposAtividade();
        this.CarregarExecutoresAtividade();
        ddlExecutorAtividade.SelectedValue = ordem != null && ordem.Responsavel != null ? ordem.Responsavel.Id.ToString() : "0";
        edicao_detalhamento_atividade.Visible = visualizacao_detalhamento_atividade.Visible = false;

        this.CarregarArvoreArquivosAtividade(null);

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>SelecionarPrimeiraTabAtividades();</script>", false);
    }

    private void CarregarHistoricoDetalhamentosAtividade()
    {
        Atividade atividade = Atividade.ConsultarPorId(hfIdAtividade.Value.ToInt32());

        IList<Detalhamento> detalhamentos = atividade != null && atividade.Detalhamentos != null ? atividade.Detalhamentos.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Esta atividade não possui detalhamentos salvos para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void ExcluirArquivosAtividade()
    {
        if (trvArquivosAtividade.SelectedValue.Contains("ARQPED_"))
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosAtividade.SelectedNode.Value.Split('_')[1].ToInt32());

            arquivo.Excluir();

            msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void SalvarAtividade()
    {
        Atividade atividade = Atividade.ConsultarPorId(hfIdAtividade.Value.ToInt32());

        if (atividade == null)
            atividade = new Atividade();

        atividade.Ativo = ckbAtivoAtividade.Checked;
        atividade.Data = tbxDataAtividade.Text.ToDateTime();
        atividade.OrdemServico = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        atividade.TipoAtividade = TipoAtividade.ConsultarPorId(ddlTipoAtividade.SelectedValue.ToInt32());
        atividade.Descricao = tbxDescricaoAtividade.Text;
        atividade.Executor = Funcionario.ConsultarPorId(ddlExecutorAtividade.SelectedValue.ToInt32());

        //Se o detalhamento esta em modo de edição, salvar um novo detalhamento para o pedido
        if (edicao_detalhamento_atividade.Visible == true && hfIdAtividade.Value.ToInt32() > 0)
        {
            if (atividade.Detalhamentos == null)
                atividade.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            detalhamento.Conteudo = editDetalhamentoAtividade.Content;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            detalhamento = detalhamento.Salvar();

            atividade.Detalhamentos.Add(detalhamento);

            tbxVisualizarDetalhamentoAtividade.Text = detalhamento.Conteudo;

            edicao_detalhamento_atividade.Visible = false;
            visualizacao_detalhamento_atividade.Visible = true;
        }

        //Carregando os detalhamentos
        if (atividade.Detalhamentos != null && atividade.Detalhamentos.Count > 0)
        {
            edicao_detalhamento_atividade.Visible = false;
            visualizacao_detalhamento_atividade.Visible = true;

            Detalhamento ultimoDetalhamentoDaVisita = atividade.GetUltimoDelhamento;

            tbxVisualizarDetalhamentoAtividade.Text = ultimoDetalhamentoDaVisita != null ? ultimoDetalhamentoDaVisita.Conteudo : "";
        }
        else
        {
            edicao_detalhamento_atividade.Visible = true;
            visualizacao_detalhamento_atividade.Visible = false;
        }

        atividade = atividade.Salvar();

        if (hfIdAtividade.Value.ToInt32() <= 0)
            this.IncluirRegistroInclusaoAtividadeNoDetalhamentoDaOS(atividade);

        hfIdAtividade.Value = atividade.Id.ToString();

        msg.CriarMensagem("Atividade salva com sucesso", "Sucesso", MsgIcons.Sucesso);

    }

    private void ExcluirArquivosOS()
    {
        if (trvAnexosOS.SelectedValue.Contains("ARQPED_"))
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(trvAnexosOS.SelectedNode.Value.Split('_')[1].ToInt32());
            this.IncluirRegistroExclusaoArquivoNoDetalhamentoDaOS(arquivo);
            arquivo.Excluir();
            msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void IncluirRegistroExclusaoArquivoNoDetalhamentoDaOS(Arquivo arquivo)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new System.Collections.Generic.List<Modelo.Detalhamento>();

        Modelo.Detalhamento detalhamento = new Modelo.Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Modelo.Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";
        detalhamento.Conteudo += @"<br /><br />Arquivo " + arquivo.Nome + " Excluído em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". Usuário: " + this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento = detalhamento.Salvar();
        ordem.Detalhamentos.Add(detalhamento);
        ordem = ordem.Salvar();
    }

    private void IncluirRegistroAlteracaoNomeArquivoNoDetalhamentoDaOS(String nomeAntigo, String nomeNovo)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new System.Collections.Generic.List<Modelo.Detalhamento>();

        Modelo.Detalhamento detalhamento = new Modelo.Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Modelo.Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";
        detalhamento.Conteudo += @"<br /><br />Nome de Arquivo alterado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". Usuário: " + this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento.Conteudo += @"<br />Nome Antigo: '" + nomeAntigo + "'. Novo Nome: '" + nomeNovo + "'";
        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento = detalhamento.Salvar();
        ordem.Detalhamentos.Add(detalhamento);
        ordem = ordem.Salvar();
    }

    private void IncluirRegistroDetalhamentoOs(String alteracao1, String alteracao2)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new System.Collections.Generic.List<Modelo.Detalhamento>();

        Modelo.Detalhamento detalhamento = new Modelo.Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Modelo.Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";
        detalhamento.Conteudo += @"<br /><br />" + alteracao1;
        if (alteracao2.IsNotNullOrEmpty())
            detalhamento.Conteudo += @"<br />" + alteracao2;
        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento = detalhamento.Salvar();
        ordem.Detalhamentos.Add(detalhamento);
        ordem = ordem.Salvar();
    }

    private void SalvarOS()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        if (ordem == null)
        {
            msg.CriarMensagem("Ordem de Serviço não foi carregada corretamente. Por favor, volte à Pesquisa de OS e tente novamente.", "Erro", MsgIcons.Erro);
            return;
        }

        if (ordem.IsEncerrada)
        {
            //Verificar a alteração de protocolo, data ou obsevação
            if (ordem.PossuiProtocolo)
            {
                if (!tbxProtocoloEncerramentoExibicao.Text.Trim().Equals(ordem.ProtocoloOficioEncerramento))
                {
                    this.IncluirRegistroDetalhamentoOs("Protocolo Alterado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". Usuário: " + this.FuncionarioLogado.NomeRazaoSocial,
                        "Antigo: " + ordem.ProtocoloOficioEncerramento + ". Novo: " + tbxProtocoloEncerramentoExibicao.Text.Trim());
                    ordem.ProtocoloOficioEncerramento = tbxProtocoloEncerramentoExibicao.Text.Trim();
                }
                if (!tbxDataProtocoEncerramentoExibicao.Text.ToDateTime().Equals(ordem.Data))
                {
                    this.IncluirRegistroDetalhamentoOs("Data do protocolo de Encerramento Alterada em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". Usuário: " + this.FuncionarioLogado.NomeRazaoSocial,
                        "Antiga: " + ordem.ProtocoloOficioEncerramento + ". Nova: " + tbxDataProtocoEncerramentoExibicao.Text.Trim());
                    ordem.DataProtocoloEncerramento = tbxProtocoloEncerramentoExibicao.Text.ToDateTime();
                }
            }
            else
                if (!tbxVisualizacaoObservacoesEncerramento.Text.Trim().Equals(ordem.ProtocoloOficioEncerramento))
            {
                this.IncluirRegistroDetalhamentoOs("Observações de Encerramento alterada em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". Usuário: " + this.FuncionarioLogado.NomeRazaoSocial,
                    "Antiga: " + ordem.ProtocoloOficioEncerramento + ". Nova: " + tbxVisualizacaoObservacoesEncerramento.Text.Trim());
                ordem.ProtocoloOficioEncerramento = tbxVisualizacaoObservacoesEncerramento.Text.Trim();
            }

            ordem = ordem.Salvar();
            hfId.Value = ordem.Id.ToString();

            msg.CriarMensagem("Ordem de Serviço salva com sucesso.", "Sucesso", MsgIcons.Sucesso);
            return;
        }


        //Se  observações esta em modo de edição, salvar uma nova observação para a OS
        if (observacoes_edicao.Visible == true)
        {
            if (ordem.Observacoes == null)
                ordem.Observacoes = new List<Detalhamento>();

            Detalhamento observacao = new Detalhamento();
            observacao.DataSalvamento = DateTime.Now;
            observacao.Conteudo = editObservacao.Content;
            observacao.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            observacao = observacao.Salvar();

            ordem.Observacoes.Add(observacao);

            tbxObservacaoVisualizacao.Text = observacao.Conteudo;

            observacoes_edicao.Visible = false;
            observacoes_visualizacao.Visible = true;

            if (this.FuncionarioLogado.Id != ordem.Responsavel.Id)
                this.EnviarEmailObservacoesAlteradasParaOResponsavel(ordem);
        }

        int idResponsavelAtualOS = ordem.Responsavel.Id;

        //O Responsável da OS foi alterado
        if (idResponsavelAtualOS != ddlResponsavelOS.SelectedValue.ToInt32())
        {
            ordem.Responsavel = Funcionario.ConsultarPorId(ddlResponsavelOS.SelectedValue.ToInt32());
            this.EnviarEmailAlteracaoResponsavelDaOs(ordem, idResponsavelAtualOS, ddlResponsavelOS.SelectedValue.ToInt32());
        }

        ordem = ordem.Salvar();
        hfId.Value = ordem.Id.ToString();

        msg.CriarMensagem("Ordem de Serviço salva com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    private void EnviarEmailAlteracaoResponsavelDaOs(OrdemServico ordem, int idResponsavelAntigo, int idNovoResponsavel)
    {
        Funcionario responsavelAntigo = Funcionario.ConsultarPorId(idResponsavelAntigo);
        Funcionario responsavelNovo = Funcionario.ConsultarPorId(idNovoResponsavel);

        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Troca de Responsável de OS - Anterior: " + responsavelAntigo.NomeRazaoSocial + @", Atual: " + responsavelNovo.NomeRazaoSocial + @"";
        mail.BodyHtml = true;

        String dadosOS = Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + ordem.Id.ToString());

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Troca de Responsável de OS - Anterior: " + responsavelAntigo.NomeRazaoSocial + @", Atual: " + responsavelNovo.NomeRazaoSocial + @"
    </div>
     <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A Ordem de Serviço " + ordem.Codigo + @", teve seu responsável alterado de  " + responsavelAntigo.NomeRazaoSocial + @" para " + responsavelNovo.NomeRazaoSocial + @", no Sistema Ambientalis Manager, conforme especificações abaixo:<br />       
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
                    <strong>Novo Responsável:</strong> " + responsavelNovo.NomeRazaoSocial + @"
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

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Troca de Responsável de OS - Anterior: " + responsavelAntigo.NomeRazaoSocial + @", Atual: " + responsavelNovo.NomeRazaoSocial + @"", conteudoemail);

        //Enviando e-mail para o Responsavel Antigo
        if (responsavelAntigo != null && responsavelAntigo.EmailCorporativo != null && responsavelAntigo.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavelAntigo.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavelAntigo.EmailCorporativo);
        }

        //Enviando e-mail para o Novo Responsavel
        if (responsavelNovo != null && responsavelNovo.EmailCorporativo != null && responsavelNovo.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavelNovo.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavelNovo.EmailCorporativo);
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
        {
            mail.EnviarAutenticado("Troca de responsável de OS", this.FuncionarioLogado, ordem.Pedido, 587, false);
        }
    }

    private void EnviarEmailObservacoesAlteradasParaOResponsavel(OrdemServico ordem)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Alteração nas observações da Ordem de Serviço";
        mail.BodyHtml = true;

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Alteração nas observações da Ordem de Serviço
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A OS " + ordem.Codigo + @", sob responsabilidade do funcionário " + ordem.Responsavel.NomeRazaoSocial + @", teve suas observacoes alteradas em " + DateTime.Now.ToShortDateString() + @" pelo usuário " + this.FuncionarioLogado.NomeRazaoSocial + @".<br /><br />
        As observações da OS passam a ser:
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left'>
                    <div style='margin-top:10px;'>
                        <strong>Observações:</strong>
                    </div>
                    <div>
                        " + tbxObservacaoVisualizacao.Text + @"
                    </div>
                    
                </td>
            </tr>            
        </table>        
    </div>";


        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Alteração nas observações da Ordem de Serviço", conteudoemail);

        Funcionario responsavel = ordem.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            mail.EnviarAutenticado("Observações alteradas", this.FuncionarioLogado, ordem.Pedido, 587, false);
        }
    }

    private void EnviarEmailArquivosAdicionadosAlteradasParaOResponsavel(OrdemServico ordem)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Alteração nas anexos da Ordem de Serviço";
        mail.BodyHtml = true;

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Alteração nas observações da Ordem de Serviço
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A OS " + ordem.Codigo + @", sob responsabilidade do funcionário " + ordem.Responsavel.NomeRazaoSocial + @", teve seus anexos alterados em " + DateTime.Now.ToShortDateString() + @" pelo usuário " + this.FuncionarioLogado.NomeRazaoSocial + @".<br /><br />
        Acesse o sistema para visualizar as alterações realizadas na OS.               
    </div>";


        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Alteração nas observações da Ordem de Serviço", conteudoemail);

        Funcionario responsavel = ordem.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            mail.EnviarAutenticado("Arquivos adicionados", this.FuncionarioLogado, ordem.Pedido, 587, false);
        }
    }

    private void EnviarSolicitacaoAdiamento()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Solicitação de adiamento", ordem.Codigo, "Início do processo");
        SolicitacaoAdiamento solicitacao = new SolicitacaoAdiamento();
        solicitacao.Data = tbxDataSolicitacaoAdiamento.Text.ToDateTime();
        solicitacao.Solicitante = tbxSolicitanteAdiamento.Text;
        solicitacao.Justificativa = tbxJustificativaAdiamento.Text;
        solicitacao.OrdemServico = ordem;
        solicitacao.PrazoPadraoAnterior = ordem.PrazoPadrao;
        solicitacao.PrazoLegalAnterior = ordem.PrazoLegal;
        solicitacao.PrazoDiretoriaAnterior = ordem.PrazoDiretoria;
        solicitacao = solicitacao.Salvar();

        Transacao.Instance.Recarregar();
        ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        solicitacao = solicitacao.ConsultarPorId();
        if (solicitacao == null)
        {
            log.adicionarLog("Não foi possível salvar a solicitação");
            msg.CriarMensagem("Não foi possível salvar a solicitação de adiamento de prazo, por favor tente novamente mais tarde!", "Sucesso", MsgIcons.Sucesso);
            return;
        }

        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Solicitação de Adiamento de Prazo de OS";
        mail.BodyHtml = true;

        String dadosSolicitacao = Utilitarios.Criptografia.Seguranca.MontarParametros("idSolicit=" + solicitacao.Id.ToString() + "&idOs=" + ordem.Id);

        log.adicionarLog("Carregando conteúdo do e-mail");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Solicitação de Adiamento de Prazo de OS
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
                    
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Data da Solicitação:</strong> " + tbxDataSolicitacaoAdiamento.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Solicitante:</strong> " + tbxSolicitanteAdiamento.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Justificativa:</strong> " + tbxJustificativaAdiamento.Text + @"
                </td>
            </tr>
        </table>
        <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:10px;'>
            <a href='http://ambientalismanager.com.br/OrdemServico/SolicitacaoAdiamento.aspx" + dadosSolicitacao + @"'>Clique aqui</a> para aceitar ou negar a solicitação de adiamento de prazo</div>
    </div>";

        log.adicionarLog("Carregando a mensagem completa do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Solicitação de Adiamento de Prazo de OS", conteudoemail.ToString());
        //Adicionado os destinatarios para o email de acordo com as funcoes
        log.adicionarLog("Adicionando os destinatários do e-mail");
        IList<Permissao> permissoesQueAdiamPrazo = new List<Permissao>();
        permissoesQueAdiamPrazo.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAdiamOSDaEmpresa());
        permissoesQueAdiamPrazo.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAdiamOSDoDepartamento(ordem.Setor.Departamento.Id));
        permissoesQueAdiamPrazo.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAdiamOSDoSetor(ordem.Setor.Id));
        permissoesQueAdiamPrazo.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAdiamOSDoResponsavel(ordem.Responsavel.Id));

        permissoesQueAdiamPrazo = permissoesQueAdiamPrazo.Distinct().ToList();

        if (permissoesQueAdiamPrazo != null && permissoesQueAdiamPrazo.Count > 0)
        {
            foreach (Permissao permissao in permissoesQueAdiamPrazo)
            {
                if (permissao != null && permissao.Funcionario != null && permissao.Funcionario.EmailCorporativo != null && permissao.Funcionario.EmailCorporativo != "" && !mail.EmailsDestino.Contains(new MailAddress(permissao.Funcionario.EmailCorporativo)))
                    mail.AdicionarDestinatario(permissao.Funcionario.EmailCorporativo);
            }
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Solicitação de adiamento de prazo", this.FuncionarioLogado, ordem.Pedido, 587, false))
            {
                log.adicionarLog("E-mail enviado com sucesso!");
                msg.CriarMensagem("Solicitação de Adiamento de Prazo de OS enviada com sucesso", "Sucesso", MsgIcons.Sucesso);
            }
            else
            {
                log.adicionarLog("Erro ao enviar e-mail. " + mail.Erro);
                msg.CriarMensagem("Erro ao enviar o e-mail. " + mail.Erro + ". Tente novamente!", "Erro", MsgIcons.Erro);
                return;
            }

            log.adicionarLog("Incluindo detalhamento");
            //Incluindo a solicitação no detalhamento da OS
            if (ordem.Detalhamentos == null)
                ordem.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
            detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";

            detalhamento.Conteudo += @"<br /><br /> Realizada Solicitação de Adiamento de Prazo da Ordem de Serviço nº " + ordem.Codigo + " em " + tbxDataSolicitacaoAdiamento.Text + @" pelo funcionário " + tbxSolicitanteAdiamento.Text + @" <br /> Justificativa:<br />" + tbxJustificativaAdiamento.Text;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;
            detalhamento = detalhamento.Salvar();
            ordem.Detalhamentos.Add(detalhamento);
            ordem = ordem.Salvar();
            log.adicionarLog("Detalhamento salvo");
            tbxDetalhamentoVisualizacao.Text = detalhamento.Conteudo;
        }
        else
        {
            log.adicionarLog("Sem usuários pra receber o e-mail");
            msg.CriarMensagem("Não há nenhum usuário que possa receber a solicitação de adiamento e adiar os prazos solicitados", "Informação", MsgIcons.Informacao);
        }

    }

    private void EncerrarOS(bool enviarPesquisaSatisfacao)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        if (ordem.IsEncerrada)
        {
            msg.CriarMensagem("Essa OS já foi Encerrada. Por favor recarregue a página!", "Alerta", MsgIcons.Informacao);
            return;
        }
        if (ordem.PossuiReservaDeVeiculoAberta)
        {
            msg.CriarMensagem("Não é possível encerrar uma OS que possua Reserva de Veículo aberta!", "Alerta", MsgIcons.Informacao);
            return;
        }
        if (ordem == null)
        {
            msg.CriarMensagem("A OS não foi corretamente carregada, por favor recarregue a página e tente novamente!", "Alerta", MsgIcons.Informacao);
            return;
        }
        if (ordem.GetCliente == null)
        {
            msg.CriarMensagem("Não foi possível encontrar o cliente desta OS! Recarregue a página e tente novamente", "Alerta", MsgIcons.Informacao);
            return;
        }
        List<string> emailsCliente = ordem.GetCliente.GetEmailsNotificacoes;
        if (enviarPesquisaSatisfacao && PerguntaPesquisaSatisfacao.PossuiAlgumaPerguntaAtivaCadastrada() && emailsCliente.Count < 1)
        {
            msg.CriarMensagem("O cliente desta OS não possui algum e-mail habilitado para receber a pesquisa de satisfação! Insira ao menos um e-mail na tela de clientes e tente novamente!", "Alerta", MsgIcons.Informacao);
            return;
        }

        if (ordem != null)
        {
            ordem.DataEncerramento = tbxDataEncerramentoOS.Text.ToDateTime();
            ordem.PossuiProtocolo = chkPossuiProtocolo.Checked;
            if (ordem.PossuiProtocolo)
            {
                ordem.ProtocoloOficioEncerramento = tbxProtocoloOficioEncerramento.Text;
                ordem.DataProtocoloEncerramento = tbxDataProtocoloEncerramento.Text.ToDateTime();
            }
            else
            {
                ordem.ProtocoloOficioEncerramento = tbxObservacoesEncerramento.Text.Trim();
                ordem.DataProtocoloEncerramento = SqlDate.MinValue;
            }

            ordem = ordem.Salvar();

            this.DesbilitarEsconderBotoes();
            tbxEstadoOS.Text = "Encerrada";

            btnSalvarOS.Enabled = btnSalvarOS.Visible = true;
            btnSolicitarAdiamentoPrazo.Visible = btnSolicitarAdiamentoPrazo.Enabled = false;
            tbxDataProtocoEncerramentoExibicao.Text = ordem.DataProtocoloEncerramento.ToShortDateString();

            detalhes_os_encerrada.Visible = true;
            campo_exibicao_protocolo_encerramento.Visible = ordem.PossuiProtocolo;
            campo_exibicao_observacao_encerramento.Visible = !ordem.PossuiProtocolo;
            if (ordem.PossuiProtocolo)
            {
                tbxProtocoloEncerramentoExibicao.Text = ordem.ProtocoloOficioEncerramento;
                tbxDataEncerramentoExibicao.Text = ordem.DataEncerramento.ToShortDateString();
                tbxProtocoloEncerramentoExibicao.Enabled = ordem.GetFuncionarioPodeAlterarProtocoloOuArquivos(this.FuncionarioLogado);
            }
            else
            {
                tbxVisualizacaoObservacoesEncerramento.Text = ordem.ProtocoloOficioEncerramento;
                tbxVisualizacaoObservacoesEncerramento.Enabled = ordem.GetFuncionarioPodeAlterarProtocoloOuArquivos(this.FuncionarioLogado);
            }

            if (enviarPesquisaSatisfacao && PerguntaPesquisaSatisfacao.PossuiAlgumaPerguntaAtivaCadastrada())
            {
                PesquisaSatisfacao novaPesquisa = new PesquisaSatisfacao();
                novaPesquisa.OrdemServico = ordem;
                novaPesquisa.DataCriacao = DateTime.Now;
                novaPesquisa.DataResposta = SqlDate.MinValue;
                ordem.PesquisaSatisfacao = novaPesquisa.Salvar();
                ordem.Salvar();

                TextoPadrao textoEmail = TextoPadrao.ConsultarPorTipo(TextoPadrao.MODELOPESQUISASATISFACAO);
                String mensagemMail = textoEmail.AtualizarVariaveis(ordem).GetTextoComSubstituicaoDeTags();

                foreach (string aux in emailsCliente)
                    if (aux.IsNotNullOrEmpty() && Validadores.ValidaEmail(aux))
                    {
                        Email email = new Email();
                        email.AdicionarDestinatario(aux);
                        email.Assunto = "Pesquisa de satisfação Ambientalis";
                        email.BodyHtml = true;
                        email.Mensagem = mensagemMail;
                        email.EnviarAutenticado("Pesquisa de satisfação", this.FuncionarioLogado, ordem.Pedido, 587, false);
                    }

                msg.CriarMensagem("Ordem de Serviço encerrada e pesquisa de satisfação enviada com sucesso", "Sucesso", MsgIcons.Sucesso);
            }
            else
            {
                msg.CriarMensagem("Ordem de Serviço encerrada com sucesso", "Sucesso", MsgIcons.Sucesso);
            }
            lblEncerramentoOS_ModalPopupExtender.Hide();
        }
    }

    public bool BindingVisivelBotoesVisit(object o)
    {
        return this.ExibirBotesVisitAtividade;
    }

    private void CarregarEmailsParaAceiteDeVisita()
    {
        Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

        if (visita != null && visita.EmailAceitou.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Esta visita já foi respondida, por favor crie uma nova solicitação de visita", "Informação", MsgIcons.Informacao);
            return;
        }

        Cliente cliente = visita.GetCliente;

        if (visita != null && cliente != null)
        {
            ltbEmailsAceiteVisita.Items.Clear();

            if (cliente.Email.IsNotNullOrEmpty() && cliente.EmailRecebeNotificacoes)
                ltbEmailsAceiteVisita.Items.Add(new ListItem(cliente.Email, cliente.Email));

            if (cliente.Contatos != null && cliente.Contatos.Count > 0)
            {
                //adicionando e-mail do primeiro contato
                if (cliente.Contatos[0] != null && cliente.Contatos[0].Email.IsNotNullOrEmpty() && cliente.Contatos[0].RecebeNotificacoes && !ltbEmailsAceiteVisita.Items.Contains(new ListItem(cliente.Contatos[0].Email, cliente.Contatos[0].Email)))
                    ltbEmailsAceiteVisita.Items.Add(new ListItem(cliente.Contatos[0].Email, cliente.Contatos[0].Email));

                //adicionando e-mail do segundo contato
                if (cliente.Contatos[1] != null && cliente.Contatos[1].Email.IsNotNullOrEmpty() && cliente.Contatos[1].RecebeNotificacoes && !ltbEmailsAceiteVisita.Items.Contains(new ListItem(cliente.Contatos[1].Email, cliente.Contatos[1].Email)))
                    ltbEmailsAceiteVisita.Items.Add(new ListItem(cliente.Contatos[1].Email, cliente.Contatos[1].Email));

                //adicionando e-mail do terceiro contato
                if (cliente.Contatos[2] != null && cliente.Contatos[2].Email.IsNotNullOrEmpty() && cliente.Contatos[2].RecebeNotificacoes && !ltbEmailsAceiteVisita.Items.Contains(new ListItem(cliente.Contatos[2].Email, cliente.Contatos[2].Email)))
                    ltbEmailsAceiteVisita.Items.Add(new ListItem(cliente.Contatos[2].Email, cliente.Contatos[2].Email));
            }

            if (ltbEmailsAceiteVisita.Items == null || ltbEmailsAceiteVisita.Items.Count == 0)
                ltbEmailsAceiteVisita.Items.Add(new ListItem("Este cliente não possui nenhum e-mail habilitado para receber notificações de visitas.", ""));

            tbxOutrosEmailsAceiteVisite.Text = "";

            lblEmailsAceiteVisita_ModalPopupExtender.Show();
        }
    }

    private void EnviarEmailAceiteVisitaParaOCliente()
    {
        if (tbxOutrosEmailsAceiteVisite.Text.Trim().Replace(" ", "") != "")
        {
            if (!Utilitarios.Validadores.ValidarEmailInformado(tbxOutrosEmailsAceiteVisite.Text))
            {
                msg.CriarMensagem("O(s) outro(s) e-mail(s) informado(s) não é(são) válido(s). Insira e-mails válidos para enviar o aceite de visita. Para adicionar mais de um email, separe-os por ponto e vírgula: \";\". \"(Exemplo) exemplo@ambientalismanager.com.br\".", "Alerta", MsgIcons.Alerta);
                return;
            }
        }

        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());
        Cliente cliente = visita.GetCliente;
        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Envio aceite visita", ordem.Codigo, "Início do processo");

        if (visita != null && cliente != null)
        {
            Email mail = new Email();
            mail.Assunto = "Ambientalis Manager - Aceite e Confirmação de Visita";
            mail.BodyHtml = true;

            IList<string> emails = new List<string>();

            if (cliente.Email.IsNotNullOrEmpty() && cliente.EmailRecebeNotificacoes)
                emails.Add(cliente.Email);

            if (cliente.Contatos != null && cliente.Contatos.Count > 0)
            {
                //adicionando e-mail do primeiro contato
                if (cliente.Contatos[0] != null && cliente.Contatos[0].Email.IsNotNullOrEmpty() && cliente.Contatos[0].RecebeNotificacoes && !emails.Contains(cliente.Contatos[0].Email))
                    emails.Add(cliente.Contatos[0].Email);

                //adicionando e-mail do segundo contato
                if (cliente.Contatos[1] != null && cliente.Contatos[1].Email.IsNotNullOrEmpty() && cliente.Contatos[1].RecebeNotificacoes && !emails.Contains(cliente.Contatos[1].Email))
                    emails.Add(cliente.Contatos[1].Email);

                //adicionando e-mail do terceiro contato
                if (cliente.Contatos[2] != null && cliente.Contatos[2].Email.IsNotNullOrEmpty() && cliente.Contatos[2].RecebeNotificacoes && !emails.Contains(cliente.Contatos[2].Email))
                    emails.Add(cliente.Contatos[2].Email);
            }

            //adicionando os outros e-mails informados
            if (tbxOutrosEmailsAceiteVisite.Text.IsNotNullOrEmpty())
            {
                string outroEmailsInformados = tbxOutrosEmailsAceiteVisite.Text;
                string[] outrosEmails = outroEmailsInformados.Split(';');

                for (int i = 0; i < outrosEmails.Length; i++)
                {
                    if (outrosEmails[i].Trim() != "" && !emails.Contains(outrosEmails[i].Trim()))
                        emails.Add(outrosEmails[i].Trim());
                }
            }

            if (emails == null || emails.Count == 0)
            {
                log.adicionarLog("Nenhum e-mail habilitado no cliente para receber notificações");
                msg.CriarMensagem("O cliente desta visita não possui nenhum e-mail que esteja habilitado para receber notificações de visitas.", "Informação", MsgIcons.Informacao);
                return;
            }

            string emailsRetorno = "";

            foreach (string email in emails)
            {
                log.adicionarLog("Enviando para  o e-mail = " + email);
                mail.EmailsDestino.Clear();

                mail.AdicionarDestinatario(email);

                emailsRetorno += email + "<br />";

                String dadosVisita = Utilitarios.Criptografia.Seguranca.MontarParametros("idVisita=" + visita.Id.ToString() + "&emailAceitou=" + email);

                string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Aceite e Confirmação de Visita
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A Ambientalis consultoria propõe o agendamento da visita técnica ao Sr(a), conforme os dados abaixo:<br /><br />        
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
                    <div>
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
                        " + tbxPedidoVisita.Text + @"
                    </div>                    
                </td>
            </tr>
            
            <tr>
                <td align='left' colspan='4'>
                    <div style='margin-top:10px;'>
                        <strong>Ordem de Serviço:</strong>
                    </div>
                    <div>
                        " + tbxOSVisita.Text + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='center' colspan='4'>
                    <div style='margin-top:20px;'>
                        Observação: Favor NÃO responder a este e-mail.
                    </div>                                        
                </td>
            </tr>            
        </table>   
        <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:25px; font-size:18px;'>
            <a href='http://ambientalismanager.com.br/AceiteVisita/AceiteVisita.aspx" + dadosVisita + @"'>Clique aqui</a> para aceitar ou não a visita!</div>     
    </div>";

                mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Aceite e Confirmação de Visita", conteudoemail.ToString());

                if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
                {
                    log.adicionarLog("Enviando aceite");
                    if (mail.EnviarAutenticado("Aceite e confirmação de visita", this.FuncionarioLogado, ordem.Pedido, 587, false))
                        log.adicionarLog("Aceite enviado com sucesso!");
                    else
                        log.adicionarLog("Erro ao enviar e-mail. " + mail.Erro);
                }
            }

            msg.CriarMensagem("E-mail de Aceite e Confirmação de Visita enviado com sucesso para os e-mails abaixo:<br />" + emailsRetorno, "Sucesso", MsgIcons.Sucesso);
        }
        else
        {
            log.adicionarLog("Cliente não carregado corretamente");
            msg.CriarMensagem("Cliente da Visita não foi carregado corretamente. Por favor tente novamente.", "Erro", MsgIcons.Erro);
            return;
        }

        lblEmailsAceiteVisita_ModalPopupExtender.Hide();

    }

    private void EnviarSolicitacaoAprovacao()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Solicitação de aprovação", ordem.Codigo, "Início do processo");

        SolicitacaoAprovacao solicitacao = new SolicitacaoAprovacao();
        solicitacao.Data = tbxDataSolicitacaoAProvacao.Text.ToDateTime();
        solicitacao.Solicitante = tbxSolicitanteAprovacao.Text;
        solicitacao.Justificativa = tbxJustificativaAprovacao.Text;
        solicitacao.OrdemServico = ordem;

        solicitacao = solicitacao.Salvar();

        ordem.Status = OrdemServico.PENDENTEAPROVACAO;
        ordem = ordem.Salvar();

        tbxEstadoOS.Text = "Pendente de Aprovação";

        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Solicitação de Aprovação de OS";
        mail.BodyHtml = true;

        String dadosSolicitacao = Utilitarios.Criptografia.Seguranca.MontarParametros("idSolicit=" + solicitacao.Id.ToString() + "&id=" + ordem.Id);

        log.adicionarLog("Carregando o conteúdo do e-mail");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Solicitação de Aprovação de OS
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
                    
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Data da Solicitação:</strong> " + tbxDataSolicitacaoAProvacao.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Solicitante:</strong> " + tbxSolicitanteAprovacao.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Justificativa:</strong> " + tbxJustificativaAprovacao.Text + @"
                </td>
            </tr>
        </table>
        <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:10px;'>
            <a href='http://ambientalismanager.com.br/OrdemServico/CadastroDeOS.aspx" + dadosSolicitacao + @"'>Clique aqui</a> para acessar o sistema e aceitar ou negar a solicitação de aprovação de OS</div>
    </div>";

        log.adicionarLog("Carregando a mensagem completa do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Solicitação de Aprovação de OS", conteudoemail.ToString());

        //Adicionado os destinatarios para o email de acordo com as funcoes
        IList<Permissao> permissoesQueAprovamOS = new List<Permissao>();
        log.adicionarLog("Adicionando os destinatários");
        permissoesQueAprovamOS.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAprovamOSDaEmpresa());
        permissoesQueAprovamOS.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAprovamOSDoDepartamento(ordem.Setor.Departamento.Id));
        permissoesQueAprovamOS.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAprovamOSDoSetor(ordem.Setor.Id));
        permissoesQueAprovamOS.AddRange<Permissao>(Permissao.ConsultarPermissoesQueAprovamOSDoResponsavel(ordem.Responsavel.Id));

        permissoesQueAprovamOS = permissoesQueAprovamOS.Distinct().ToList();

        if (permissoesQueAprovamOS != null && permissoesQueAprovamOS.Count > 0)
        {
            foreach (Permissao permissao in permissoesQueAprovamOS)
            {
                if (permissao != null && permissao.Funcionario != null && permissao.Funcionario.EmailCorporativo != null && permissao.Funcionario.EmailCorporativo != "" && !mail.EmailsDestino.Contains(new MailAddress(permissao.Funcionario.EmailCorporativo)))
                    mail.AdicionarDestinatario(permissao.Funcionario.EmailCorporativo);
            }
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            if (mail.EnviarAutenticado("Solicitação de aprovação", this.FuncionarioLogado, ordem.Pedido, 587, false))
            {
                log.adicionarLog("E-mail enviado com sucesso!");
                msg.CriarMensagem("Solicitação de Aprovação de OS enviada com sucesso", "Sucesso", MsgIcons.Sucesso);
            }
            else
            {
                log.adicionarLog("Erro ao enviar e-mail. " + mail.Erro);
                msg.CriarMensagem("Erro ao enviar o e-mail. " + mail.Erro + ". Tente novamente!", "Erro", MsgIcons.Erro);
                return;
            }

            log.adicionarLog("Incluindo o detalhamento");
            //Incluindo a solicitação no detalhamento da OS
            if (ordem.Detalhamentos == null)
                ordem.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
            detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";

            detalhamento.Conteudo += @"<br /><br /> Realizada a Solicitação de Aprovação da Ordem de Serviço nº " + ordem.Codigo + " em " + tbxDataSolicitacaoAProvacao.Text + @" pelo funcionário " + tbxSolicitanteAprovacao.Text + @" <br /> Justificativa:<br />" + tbxJustificativaAprovacao.Text;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;
            detalhamento = detalhamento.Salvar();

            ordem.Detalhamentos.Add(detalhamento);
            ordem = ordem.Salvar();
            log.adicionarLog("Detalhamento salvo");

            tbxDetalhamentoVisualizacao.Text = detalhamento.Conteudo;
        }
        else
        {
            msg.CriarMensagem("Não há nenhum usuário que possa receber a solicitação de aprovação da OS", "Informação", MsgIcons.Informacao);
            log.adicionarLog("Nenhum usuário para receber a solicitação");
        }
    }

    private void AprovarOS()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

        ordem.Status = Convert.ToChar(ddlParecerAprovacaoOS.SelectedValue);
        ordem.JustificativaAprovacao = tbxJustificativaAProvacaoOS.Text;
        ordem.DataAprovacao = tbxDataAprovacaoOS.Text.ToDateTime();
        ordem.UsuarioAprovou = tbxUsuarioAprovadorOS.Text;

        ordem = ordem.Salvar();

        this.EnviarEmailParecerAProvacaoOSParaOResponsavel(ordem);

        msg.CriarMensagem("Ordem de Serviço aprovada com sucesso", "Sucesso", MsgIcons.Sucesso);

        lblAprovacaoOS_ModalPopupExtender.Hide();

        tbxEstadoOS.Text = "Aprovada";

        this.HabilitarDesbilitarCamposParecer();
    }

    private void HabilitarDesbilitarCamposParecer()
    {
        btnAprovarOS.Enabled = btnAprovarOS.Visible = btnSolicitarAprovacao.Enabled = btnSolicitarAprovacao.Visible = btnSolicitarAdiamentoPrazo.Enabled = btnSolicitarAdiamentoPrazo.Visible = false;

        btnEncerrarOS.Enabled = btnEncerrarOS.Visible = true;
    }

    private void EnviarEmailParecerAProvacaoOSParaOResponsavel(OrdemServico ordem)
    {
        LogEventosOS log = LogEventosOS.SalvarLog(this.FuncionarioLogado, "Parecer de aprovação", ordem.Codigo, "Início do processo");
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Parecer de Aprovação de OS";
        mail.BodyHtml = true;
        log.adicionarLog("Criando conteúdo do e-mail");
        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Parecer de Aprovação de OS
    </div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A OS " + ordem.Codigo + @", sob responsabilidade do funcionário " + ordem.Responsavel.NomeRazaoSocial + @", foi " + ddlParecerAprovacaoOS.SelectedItem.Text + @" em " + tbxDataAprovacaoOS.Text + @" pelo usuário " + this.FuncionarioLogado.NomeRazaoSocial + @".<br /><br />
        Detalhes do Parecer de Aprovação:               
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
                    
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Parecer:</strong> " + ddlParecerAprovacaoOS.SelectedItem.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Data do parecer:</strong> " + tbxDataAprovacaoOS.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Usuário que respondeu:</strong> " + tbxUsuarioAprovadorOS.Text + @"
                </td>
            </tr>
            <tr>
                <td align='left'>
                    <strong>Justificativa:</strong> " + tbxJustificativaAProvacaoOS.Text + @"
                </td>
            </tr>
        </table>        
    </div>";

        log.adicionarLog("carregando a mensagem do e-mail");
        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Parecer de Aprovação de OS", conteudoemail);

        Funcionario responsavel = ordem.Responsavel;

        if (responsavel != null && responsavel.EmailCorporativo != null && responsavel.EmailCorporativo != "")
        {
            log.adicionarLog("Adicionando responsável");
            if (!mail.EmailsDestino.Contains(new MailAddress(responsavel.EmailCorporativo)))
                mail.AdicionarDestinatario(responsavel.EmailCorporativo);
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
        {
            log.adicionarLog("Enviando e-mail");
            if (mail.EnviarAutenticado("Parecer de aprovação", this.FuncionarioLogado, ordem.Pedido, 587, false))
                log.adicionarLog("E-mail enviado com sucesso");
            else
            {
                log.adicionarLog("Erro ao enviar e-mail. " + mail.Erro);
                msg.CriarMensagem("Erro ao enviar o e-mail. " + mail.Erro + ". Tente novamente!", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    public string BindVisualizarOS(Object o)
    {
        OrdemServico n = (OrdemServico)o;
        return "../OrdemServico/CadastroDeOS.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id + "&sovisu=sim");
    }

    #endregion

    #region _______________ Trigers _______________

    protected void btnVerHistorioDetalhamentosPedidos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnVerHistorioObservacoesOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnNovaVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExibicoesDaVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upReservaVeiculoVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosVisita);
    }

    protected void btnGridEditarVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExibicoesDaVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upReservaVeiculoVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosVisita);
    }

    protected void btnGridEditarAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExibicoesAtividade);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormAtividade);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosAtividade);
    }

    protected void btnExcluirArquivoVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnRenomearArquivoVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void btnSalvarRenomearArquivo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosAtividade);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upDetalhamentoOs);
    }

    protected void Label1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upFormularioOS);
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upArquivosVisita);
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upArquivosAtividade);
    }

    protected void UpLoad2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upFormularioOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upArquivosVisita);
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upArquivosAtividade);
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upDetalhamentoOs);
    }

    protected void btnSalvarVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnVisualizarDetalhamentosVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnNovaAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExibicoesAtividade);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormAtividade);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosAtividade);
    }

    protected void btnVisualizarDetalhamentosAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnExcluirArquivoAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnRenomearArquivoAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void btnSalvarAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnRenomearArquivoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void btnSolicitarAdiamentoPrazo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upSolicitacaoAdiamentoPrazo);
    }

    protected void btnSolicitarAdiamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnEncerrarOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposEncerramentoOS);
    }

    protected void btnSalvarEncerramentoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnSolicitarAprovacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposSolicitacaoAprovacao);
    }

    protected void btnEnviarSolicitarAprovacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnAprovarOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposAprovacaoOS);
    }

    protected void btnEmitirParecerAprovacaoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioOS);
    }

    protected void btnEnviarEmailAceiteVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEmailsAceiteVisita);
    }

    protected void btnExcluirArquivoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upDetalhamentoOs);
    }

    #endregion

    #region ______________ PreRenders _____________

    protected void gdvVisitas_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvVisitas.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void gdvAtividades_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvAtividades.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void gdvOrdensVinculadas_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvOrdensVinculadas.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirAtividade_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta atividade serão perdidos. Deseja realmente excluir esta atividade ?");
    }

    protected void btnGridExcluirVisita_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta visita serão perdidos. Deseja realmente excluir esta visita ?");
    }

    protected void btnExcluirArquivoOS_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao(sender as Button, "Deseja realmente excluir este Arquivo?");
    }

    #endregion

    #region _______________ Eventos _______________

    protected void btnVerHistorioDetalhamentosOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarHistoricoDetalhamentosOS();
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

    protected void btnVerHistorioObservacoesOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarHistoricoObservacoesOS();
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

    protected void btnEditarObservacaoOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para poder alterar seu histórico.", "Informação", MsgIcons.Informacao);
                return;
            }

            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());

            Detalhamento ultimaObservacao = ordem.GetUltimaObservacao;

            editObservacao.Content = ultimaObservacao != null ? ultimaObservacao.Conteudo : "";

            observacoes_edicao.Visible = true;
            observacoes_visualizacao.Visible = false;
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

    protected void gdvVisitas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvVisitas.PageIndex = e.NewPageIndex;

            this.CarregarVisitas(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void gdvVisitas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirVisita(e);
            Transacao.Instance.Recarregar();
            this.CarregarVisitas(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));

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

    protected void gdvAtividades_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvAtividades.PageIndex = e.NewPageIndex;

            this.CarregarAtividades(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void gdvAtividades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirAtividade(e);
            Transacao.Instance.Recarregar();
            this.CarregarAtividades(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));

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

    protected void btnNovaVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaVisita();
            lblCadastroVisita_ModalPopupExtender.Show();
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

    protected void btnGridEditarVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarVisita(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovoArquivoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder inserir arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            Session["objeto_upload_OS"] = "visita";
            Session["id_visita_arquivos"] = hfIdVisita.Value;

            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnExcluirArquivoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder excluir arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosVisita.SelectedValue.IsNotNullOrEmpty() || !trvArquivosVisita.SelectedValue.Contains("ARQPED_") || trvArquivosVisita.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da visita para ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirArquivosVisita();
            Transacao.Instance.Recarregar();
            this.CarregarArvoreArquivosOS(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
            this.CarregarArvoreArquivosVisita(Visita.ConsultarPorId(hfIdVisita.Value.ToInt32()));
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

    protected void btnRenomearArquivoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder renomear arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosVisita.SelectedValue.IsNotNullOrEmpty() || !trvArquivosVisita.SelectedValue.Contains("ARQPED_") || trvArquivosVisita.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da visita para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            hfIdArquivoRenomear.Value = trvArquivosVisita.SelectedNode.Value.Split('_')[1];
            hfArquivoOS.Value = "visita";
            tbxNovoNomeArquivo.Text = "";

            lblRenomearArquivos_ModalPopupExtender.Show();
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

    protected void btnSalvarRenomearArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(hfIdArquivoRenomear.Value.ToInt32());
            String nomeAntigo = arquivo.Nome;
            String novoNome = arquivo.Nome = tbxNovoNomeArquivo.Text;
            arquivo = arquivo.Salvar();
            lblRenomearArquivos_ModalPopupExtender.Hide();
            Transacao.Instance.Recarregar();
            if (hfArquivoOS.Value == "visita")
                this.CarregarArvoreArquivosVisita(Visita.ConsultarPorId(hfIdVisita.Value.ToInt32()));
            else if (hfArquivoOS.Value == "atividade")
                this.CarregarArvoreArquivosAtividade(Atividade.ConsultarPorId(hfIdAtividade.Value.ToInt32()));
            else
                this.IncluirRegistroAlteracaoNomeArquivoNoDetalhamentoDaOS(nomeAntigo, novoNome);

            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
            this.CarregarArvoreArquivosOS(ordem);
            //Carregando os detalhamentos da OS 
            Detalhamento ultimoDetalhamentoDaOS = ordem.GetUltimoDelhamento;
            tbxDetalhamentoVisualizacao.Text = ultimoDetalhamentoDaOS != null ? ultimoDetalhamentoDaOS.Conteudo : "";
            msg.CriarMensagem("Arquivo renomeado com sucesso", "Sucesso", MsgIcons.Sucesso);
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

    protected void UpLoad2_UpLoadComplete(object sender, EventArgs e)
    {
        try
        {
            Transacao.Instance.Recarregar();
            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
            this.CarregarArvoreArquivosOS(ordem);
            if (Session["objeto_upload_OS"] == "visita")
                this.CarregarArvoreArquivosVisita(Visita.ConsultarPorId(hfIdVisita.Value.ToInt32()));

            if (Session["objeto_upload_OS"] == "atividade")
                this.CarregarArvoreArquivosAtividade(Atividade.ConsultarPorId(hfIdAtividade.Value.ToInt32()));

            if (this.FuncionarioLogado.Id != ordem.Responsavel.Id)
                this.EnviarEmailArquivosAdicionadosAlteradasParaOResponsavel(ordem);

            //Carregando os detalhamentos da OS 
            Detalhamento ultimoDetalhamentoDaOS = ordem.GetUltimoDelhamento;
            tbxDetalhamentoVisualizacao.Text = ultimoDetalhamentoDaOS != null ? ultimoDetalhamentoDaOS.Conteudo : "";
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

    protected void btnNovoCadastroVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaVisita();
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

    protected void btnSalvarVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarVisita();
            Transacao.Instance.Recarregar();
            this.CarregarVisitas(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void lkbExibirDadosVisita_Click(object sender, EventArgs e)
    {
        try
        {
            mvFormVisita.ActiveViewIndex = 0;
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

    protected void lkbExibirReservaVeiculoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            mvFormVisita.ActiveViewIndex = 1;

            if (tbxDataInicioReserva.Text == "")
                tbxDataInicioReserva.Text = tbxDataInicioVisita.Text != "" ? tbxDataInicioVisita.Text.ToDateTime().ToShortDateString() : "";

            if (tbxDataFimReserva.Text == "")
                tbxDataFimReserva.Text = tbxDataFimVisita.Text != "" ? tbxDataFimVisita.Text.ToDateTime().ToShortDateString() : "";
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

    protected void lkbExibirArquivosVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
            }

            mvFormVisita.ActiveViewIndex = 2;
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

    protected void btnVisualizarDetalhamentosVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder visualizar o historico de detalhamentos.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarHistoricoDetalhamentosVisita();
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

    protected void btnEditarDetalhamentosVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder alterar seu detalhamento.", "Informação", MsgIcons.Informacao);
                return;
            }

            Visita visita = Visita.ConsultarPorId(hfIdVisita.Value.ToInt32());

            Detalhamento ultimoDetalhamento = visita.GetUltimoDelhamento;

            editDetalhamentoVisita.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

            detalhamento_edicao_visita.Visible = true;
            detalhamento_visualizacao_visita.Visible = false;
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

    protected void btnGridEditarAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarAtividade(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovaAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaAtividade();
            lblCadastroAtividade_ModalPopupExtender.Show();
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

    protected void lkbExibirDadosAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            mvFormAtividade.ActiveViewIndex = 0;
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

    protected void lkbExibirArquivosAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
            }

            mvFormAtividade.ActiveViewIndex = 1;
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

    protected void btnVisualizarDetalhamentosAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder visualizar o historico de detalhamentos.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarHistoricoDetalhamentosAtividade();
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

    protected void btnEditarDetalhamentosAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder alterar seu detalhamento.", "Informação", MsgIcons.Informacao);
                return;
            }

            Atividade atividade = Atividade.ConsultarPorId(hfIdAtividade.Value.ToInt32());

            Detalhamento ultimoDetalhamento = atividade.GetUltimoDelhamento;

            editDetalhamentoAtividade.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

            edicao_detalhamento_atividade.Visible = true;
            visualizacao_detalhamento_atividade.Visible = false;
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

    protected void btnNovoArquivoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder inserir arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            Session["objeto_upload_OS"] = "atividade";
            Session["id_atividade_arquivos"] = hfIdAtividade.Value;

            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnExcluirArquivoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder excluir arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosAtividade.SelectedValue.IsNotNullOrEmpty() || !trvArquivosAtividade.SelectedValue.Contains("ARQPED_") || trvArquivosAtividade.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da atividade para ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirArquivosAtividade();
            Transacao.Instance.Recarregar();
            this.CarregarArvoreArquivosOS(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
            this.CarregarArvoreArquivosAtividade(Atividade.ConsultarPorId(hfIdAtividade.Value.ToInt32()));
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

    protected void btnRenomearArquivoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdAtividade.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder renomear arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosAtividade.SelectedValue.IsNotNullOrEmpty() || !trvArquivosAtividade.SelectedValue.Contains("ARQPED_") || trvArquivosAtividade.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da atividade para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            hfIdArquivoRenomear.Value = trvArquivosAtividade.SelectedNode.Value.Split('_')[1];
            hfArquivoOS.Value = "atividade";
            tbxNovoNomeArquivo.Text = "";

            lblRenomearArquivos_ModalPopupExtender.Show();
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

    protected void btnNovoCadastroAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaAtividade();
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
            this.SalvarAtividade();
            Transacao.Instance.Recarregar();
            this.CarregarAtividades(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnSalvarOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarOS();
            Transacao.Instance.Recarregar();
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

    protected void btnNovoArquivoOS_Click(object sender, EventArgs e)
    {
        try
        {
            Session["objeto_upload_OS"] = "os";
            Session["id_os_arquivos"] = hfId.Value;

            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnExcluirArquivoOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (!trvAnexosOS.SelectedValue.IsNotNullOrEmpty() || !trvAnexosOS.SelectedValue.Contains("ARQPED_") || trvAnexosOS.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da OS para ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirArquivosOS();
            Transacao.Instance.Recarregar();
            //Carregando os detalhamentos da OS 
            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
            Detalhamento ultimoDetalhamentoDaOS = ordem.GetUltimoDelhamento;
            tbxDetalhamentoVisualizacao.Text = ultimoDetalhamentoDaOS != null ? ultimoDetalhamentoDaOS.Conteudo : "";
            this.CarregarArvoreArquivosOS(ordem);
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

    protected void btnRenomearArquivoOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (!trvAnexosOS.SelectedValue.IsNotNullOrEmpty() || !trvAnexosOS.SelectedValue.Contains("ARQPED_") || trvAnexosOS.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da OS para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            hfIdArquivoRenomear.Value = trvAnexosOS.SelectedNode.Value.Split('_')[1];
            hfArquivoOS.Value = "os";
            tbxNovoNomeArquivo.Text = "";

            lblRenomearArquivos_ModalPopupExtender.Show();
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

    protected void btnSolicitarAdiamentoPrazo_Click(object sender, EventArgs e)
    {
        try
        {
            tbxDataSolicitacaoAdiamento.Text = DateTime.Now.ToShortDateString();
            tbxSolicitanteAdiamento.Text = this.FuncionarioLogado.NomeRazaoSocial;
            tbxJustificativaAdiamento.Text = "";
            lblSolicitarAdiamentoPrazo_ModalPopupExtender.Show();
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

    protected void btnSolicitarAdiamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.EnviarSolicitacaoAdiamento();
            lblSolicitarAdiamentoPrazo_ModalPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de email de adiamento de prazos (" + DateTime.Now + ")";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "");
            mail.EmailsDestino.Add("hugo@c2ti.com.br");
            mail.EnviarAutenticado("Erro email de adiamento de prazo", this.FuncionarioLogado, null, 587, false);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void btnEncerrarOS_Click(object sender, EventArgs e)
    {
        try
        {
            OrdemServico ordem = OrdemServico.ConsultarPorId(hfId.Value.ToInt32());
            if (ordem.IsEncerrada)
                msg.CriarMensagem("Esta OS ja foi encerrada. Por favor recarregue a página", "Alerta", MsgIcons.Informacao);
            else if (ordem.PossuiReservaDeVeiculoAberta)
                msg.CriarMensagem("Não é possível encerrar uma OS que possua Reserva de Veículo aberta!", "Alerta", MsgIcons.Informacao);
            else
            {
                tbxDataEncerramentoOS.Text = DateTime.Now.ToShortDateString();
                tbxDataProtocoloEncerramento.Text = tbxProtocoloOficioEncerramento.Text = "";
                lblEncerramentoOS_ModalPopupExtender.Show();
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

    protected void btnSalvarEncerramentoOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.EncerrarOS(false);
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

    protected void btnEncerrarEEnviarPesquisaOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.EncerrarOS(true);
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

    protected void btnEnviarEmailAceiteVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdVisita.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder enviar e-mails de aceite.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarEmailsParaAceiteDeVisita();

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

    protected void btnSolicitarAprovacao_Click(object sender, EventArgs e)
    {
        try
        {
            tbxDataSolicitacaoAProvacao.Text = DateTime.Now.ToShortDateString();
            tbxSolicitanteAprovacao.Text = this.FuncionarioLogado.NomeRazaoSocial;
            tbxJustificativaAprovacao.Text = "";
            lblSolicitarAprovacao_ModalPopupExtender.Show();
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

    protected void btnEnviarSolicitarAprovacao_Click(object sender, EventArgs e)
    {
        try
        {
            this.EnviarSolicitacaoAprovacao();
            lblSolicitarAprovacao_ModalPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de email de solicitação de aprovação (" + DateTime.Now + ")";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "");
            mail.EmailsDestino.Add("hugo@c2ti.com.br");
            mail.EnviarAutenticado("Erro email solicitação de aprovação", this.FuncionarioLogado, null, 587, false);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void btnAprovarOS_Click(object sender, EventArgs e)
    {
        try
        {
            tbxDataAprovacaoOS.Text = DateTime.Now.ToShortDateString();
            tbxUsuarioAprovadorOS.Text = this.FuncionarioLogado.NomeRazaoSocial;
            tbxJustificativaAProvacaoOS.Text = "";

            lblAprovacaoOS_ModalPopupExtender.Show();
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

    protected void btnEmitirParecerAprovacaoOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.AprovarOS();
            Transacao.Instance.Recarregar();
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

    protected void gdvOrdensVinculadas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvOrdensVinculadas.PageIndex = e.NewPageIndex;
            this.CarregarGridOSsVinculadas(OrdemServico.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void trvAnexosOS_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!trvAnexosOS.SelectedValue.IsNotNullOrEmpty() || !trvAnexosOS.SelectedValue.Contains("ARQPED"))
                visualizar_arquivos_os.Visible = false;
            else
            {
                Arquivo arquivo = Arquivo.ConsultarPorId(trvAnexosOS.SelectedNode.Value.Split('_')[1].ToInt32());
                visualizar_arquivos_os.Visible = true;
                hplVisualizarArquivoOs.NavigateUrl = arquivo.UrlImagem;
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

    protected void trvArquivosVisita_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!trvArquivosVisita.SelectedValue.IsNotNullOrEmpty() || !trvArquivosVisita.SelectedValue.Contains("ARQPED"))
                visualizar_arquivos_visita.Visible = false;
            else
            {
                Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosVisita.SelectedNode.Value.Split('_')[1].ToInt32());

                visualizar_arquivos_visita.Visible = true;

                hplVisualizarArquivosVisita.NavigateUrl = arquivo.UrlImagem;
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

    protected void trvArquivosAtividade_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!trvArquivosAtividade.SelectedValue.IsNotNullOrEmpty() || !trvArquivosAtividade.SelectedValue.Contains("ARQPED"))
                visualizar_arquivos_atividade.Visible = false;
            else
            {
                Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosAtividade.SelectedNode.Value.Split('_')[1].ToInt32());

                visualizar_arquivos_atividade.Visible = true;

                hplArquivosAtividade.NavigateUrl = arquivo.UrlImagem;
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

    protected void btnEnviarEmailsAceiteVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.EnviarEmailAceiteVisitaParaOCliente();
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

    protected void chkPossuiProtocolo_CheckedChanged(object sender, EventArgs e)
    {
        mtvProtocoloEncerramento.ActiveViewIndex = chkPossuiProtocolo.Checked ? 0 : 1;
    }

    #endregion

}