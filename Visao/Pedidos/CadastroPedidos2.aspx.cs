using Modelo;
using SisWebControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

public partial class Pedidos_CadastroPedidos2 : PageBase
{
    private Msg msg = new Msg();
    private Funcionario FuncionarioLogado
    {
        get
        {
            return (Funcionario)Session["usuario_logado"];
        }
    }
    protected Funcao FuncaoLogada
    {
        get
        {
            return (Funcao)Session["funcao_logada"];
        }
    }
    public IList<DateTime> DatasParaReplicacao
    {
        get
        {
            if (Session["DatasParaReplicacao"] == null)
                return null;
            else
                return (IList<DateTime>)Session["DatasParaReplicacao"];
        }
        set { Session["DatasParaReplicacao"] = value; }
    }
    public Orcamento OrcamentoCadastrar
    {
        get
        {
            if (Session["orcamento_conversao"] == null)
                return null;
            else
                return (Orcamento)Session["orcamento_conversao"];
        }
        set { Session["orcamento_conversao"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                string idPedido = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                if (idPedido.ToInt32() == 0 && Request["idPedido"] != null)
                    idPedido = Modelo.Util.CriptografiaSimples.Decrypt(Request["idPedido"].ToString().Replace(" ", "+"));

                if (idPedido.ToInt32() > 0)
                {
                    hfId.Value = idPedido;
                    this.CarregarPedido(idPedido.ToInt32());
                }
                //Veio com parâmetro para carregar orçamento
                else if (Request["o"] != null)
                {
                    int idOrcamento = Modelo.Util.CriptografiaSimples.Decrypt(Request["o"].ToString().Replace(" ", "+")).ToInt32();
                    this.OrcamentoCadastrar = new Orcamento(idOrcamento).ConsultarPorId();
                    if (this.OrcamentoCadastrar != null)
                    {
                        if (this.OrcamentoCadastrar.Pedido != null)
                        {
                            msg.CriarMensagem("O pedido deste orçamento já foi gerado! Visualizando pedido gerado", "Informação", MsgIcons.Informacao);
                            hfId.Value = this.OrcamentoCadastrar.Pedido.Id.ToString();
                            this.CarregarPedido(this.OrcamentoCadastrar.Pedido.Id);
                            this.OrcamentoCadastrar = null;
                        }
                        else
                        {
                            lblOrcamentoVinculado.Text = this.OrcamentoCadastrar.Numero;
                            tbxDataPedido.Text = this.OrcamentoCadastrar.DataAceite.ToShortDateString();
                            if (this.OrcamentoCadastrar.TipoPedido != null)
                                ddlTipoPedido.SelectedValue = this.OrcamentoCadastrar.TipoPedido.Id.ToString();
                            ddlClientePedido.SelectedValue = this.OrcamentoCadastrar.ContatoCliente.Cliente.Id.ToString();
                            btnSalvarPedido.Text = "Gerar pedido do orçamento";
                            ddlTipoPedido.Enabled = ddlClientePedido.Enabled = false;
                            if (this.OrcamentoCadastrar.DiretorResponsavel.Vendedor)
                                ddlVendedor.SelectedValue = this.OrcamentoCadastrar.DiretorResponsavel.Id.ToString();
                        }
                    }
                }
                pnlOrcamentoCarregado.Visible = this.OrcamentoCadastrar != null && this.OrcamentoCadastrar.Pedido == null;
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
        btnContratoPedido.Text = "Salve o pedido primeiro";
        btnContratoPedido.Enabled = false;
        this.OrcamentoCadastrar = null;
        linkOrcamento.Visible = false;

        this.CarregarTiposPedidos();
        this.CarregarClientes();
        this.CarregarVendedores();
        this.CarregarCaixas();
    }

    private void CarregarCaixas()
    {
        ddlCaixaGerarFinanceiro.Items.Clear();
        ddlCaixaGerarFinanceiro.Items.Add(new ListItem("-- Selecione --", "0"));

        ddlCaixaParcela.Items.Clear();
        ddlCaixaParcela.Items.Add(new ListItem("-- Nenhum --", "0"));

        IList<Caixa> caixas = Caixa.ConsultarTodosOrdemAlfabetica();
        foreach (Caixa caixa in caixas)
        {
            ddlCaixaGerarFinanceiro.Items.Add(new ListItem(caixa.Descricao, caixa.Id.ToString()));
            ddlCaixaParcela.Items.Add(new ListItem(caixa.Descricao, caixa.Id.ToString()));
        }
    }

    private void CarregarVendedores()
    {
        ddlVendedor.Items.Clear();

        ddlVendedor.DataValueField = "Id";
        ddlVendedor.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarVendedoresOrdemAlfabetica();

        ddlVendedor.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlVendedor.DataBind();

        ddlVendedor.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarTiposPedidos()
    {
        ddlTipoPedido.Items.Clear();

        ddlTipoPedido.DataValueField = "Id";
        ddlTipoPedido.DataTextField = "Nome";

        IList<TipoPedido> tiposPedidos = TipoPedido.ConsultarTodosOrdemAlfabetica();

        ddlTipoPedido.DataSource = tiposPedidos != null ? tiposPedidos : new List<TipoPedido>();
        ddlTipoPedido.DataBind();

        ddlTipoPedido.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

    private void CarregarDepartamentosOS()
    {
        ddlDepartamentoOS.Items.Clear();

        ddlDepartamentoOS.DataValueField = "Id";
        ddlDepartamentoOS.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamentoOS.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamentoOS.DataBind();

        ddlDepartamentoOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

    private void CarregarPedidosOS()
    {
        ddlPedidoOSMatrizOS.Items.Clear();

        IList<Pedido> pedidos = Pedido.ConsultarTodosOrdemAlfabetica();

        if (pedidos != null && pedidos.Count > 0)
        {
            foreach (Pedido item in pedidos)
            {
                ddlPedidoOSMatrizOS.Items.Add(new ListItem(item.Codigo + " - " + item.Data.ToShortDateString() + " - " + item.GetDescricaoTipo + " - " + item.GetNomeCliente, item.Id.ToString()));
            }
        }

        ddlPedidoOSMatrizOS.SelectedValue = hfId.Value;
    }

    private void CarregarOSsMatriz()
    {
        ddlOSMatrizOS.Items.Clear();

        ddlOSMatrizOS.DataValueField = "Id";
        ddlOSMatrizOS.DataTextField = "Nome";

        Pedido pedido = Pedido.ConsultarPorId(ddlPedidoOSMatrizOS.SelectedValue.ToInt32());

        if (pedido != null && pedido.OrdensServico != null && pedido.OrdensServico.Count > 0)
        {
            String descricaoItem = string.Empty;
            foreach (OrdemServico item in pedido.OrdensServico)
            {
                descricaoItem = item.Codigo + " - " + item.Data.ToShortDateString() + " - " + item.Descricao;
                if (item.Responsavel != null)
                    descricaoItem += " - Responsável: " + item.Responsavel.NomeRazaoSocial;
                ddlOSMatrizOS.Items.Add(new ListItem(descricaoItem, item.Id.ToString()));
            }
        }

        ddlOSMatrizOS.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

    private void CarregarClientes()
    {
        ddlClientePedido.Items.Clear();

        ddlClientePedido.DataValueField = "Id";
        ddlClientePedido.DataTextField = "NomeRazaoSocial";

        IList<Cliente> clientes = Cliente.ConsultarTodosOrdemAlfabetica();

        ddlClientePedido.DataSource = clientes != null ? clientes : new List<Cliente>();
        ddlClientePedido.DataBind();

        ddlClientePedido.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarPedido(int id)
    {
        Pedido pedido = Pedido.ConsultarPorId(id);
        mtvFinanceiro.ActiveViewIndex = 0;
        btnContratoPedido.Text = "Salve o pedido primeiro";
        btnContratoPedido.Enabled = false;
        if (pedido != null)
        {
            chkPedidoAtivo.Checked = pedido.Ativo;
            tbxCodigoPedido.Text = pedido.Codigo;
            tbxDataPedido.Text = pedido.Data.ToShortDateString();
            ddlTipoPedido.SelectedValue = pedido.TipoPedido != null ? pedido.TipoPedido.Id.ToString() : "0";
            this.CarregarClientes();
            ddlClientePedido.SelectedValue = pedido.Cliente != null ? pedido.Cliente.Id.ToString() : "";
            ddlVendedor.SelectedValue = pedido.Vendedor != null ? pedido.Vendedor.Id.ToString() : "0";

            btnContratoPedido.Enabled = true;
            btnContratoPedido.Text = pedido.GetStatusContrato;
            btnContratoPedido.ToolTip = !pedido.IsContratoEnviado ? "Clique para gerar e enviar o contrato" : "Clique para visualizar o contrato do pedido";

            if (pedido.Detalhamentos != null && pedido.Detalhamentos.Count > 0)
            {
                detalhamento_edicao.Visible = false;
                detalhamento_visualizacao.Visible = true;
                Detalhamento ultimoDetalhamentoDoPedido = pedido.GetUltimoDelhamento;
                tbxDetalhamentoVisualizacao.Text = ultimoDetalhamentoDoPedido != null ? ultimoDetalhamentoDoPedido.Conteudo : "";
            }
            else
            {
                detalhamento_edicao.Visible = true;
                detalhamento_visualizacao.Visible = false;
            }

            this.CarregarGridOrdensServico(pedido);
            this.CarregarArvoreArquivosPedido(pedido);
            this.CarregarFinanceiro(pedido);
            linkOrcamento.Visible = pedido.Orcamento != null;
            ddlTipoPedido.Enabled = ddlClientePedido.Enabled = pedido.Orcamento == null;
        }

    }

    private void CarregarFinanceiro(Pedido pedido)
    {
        linkOrcamento.Visible = false;
        linkOrcamento.HRef = string.Empty;
        mtvFinanceiro.ActiveViewIndex = (pedido != null && pedido.Id > 0 ? 1 : 0);
        lblTextoAuxiliarFinanceiroPedido.Text = string.Empty;
        grvParcelasPedido.DataSource = null;
        if (pedido != null)
        {
            if (pedido.Orcamento != null)
            {
                txtOrcamentoAssociadoPedido.Text = pedido.Orcamento.Numero;
                String caminhoTela = Configuracoes.GetConfiguracoesSistema().CaminhoSistemaFinanceiro + "Telas/cadastroOrcamento.xhtml?permission=" +
                    Modelo.Util.CriptografiaSimples.Encrypt(this.FuncionarioLogado.Id + ";" + this.FuncaoLogada.Id)
                    + "&idOrcamento=" + Modelo.Util.CriptografiaSimples.Encrypt(pedido.Orcamento.Id.ToString());

                linkOrcamento.HRef = caminhoTela;
                linkOrcamento.Visible = true;
            }
            if (pedido.Receita == null)
            {
                //Carregamento os dados financeiros de acordo com o orçamento
                if (pedido.Orcamento != null)
                {
                    Orcamento aux = pedido.Orcamento;
                    lblTextoAuxiliarFinanceiroPedido.Text = "Dados do financeiro escolhidos no orçamento <b>" + aux.Numero + "</b>";
                    txtValorTotalPedido.Text = pedido.GetValorOss.ToString("#0.00");
                    if (aux.FormaDePagamentoSelecionada != null)
                    {
                        txtDividirEm.Text = aux.FormaDePagamentoSelecionada.QtdVezes.ToString();
                        txtPrimeiroPagamento.Text = pedido.Data.AddDays(aux.FormaDePagamentoSelecionada.PrazoPrimeiroPagamento).ToShortDateString();
                    }
                }
                else
                {
                    txtValorTotalPedido.Text = pedido.GetValorOss.ToString("#0.00");
                    txtDividirEm.Text = "1";
                    txtPrimeiroPagamento.Text = DateTime.Now.ToShortDateString();
                }
            }
            else
            {
                mtvFinanceiro.ActiveViewIndex = 2;
                txtValorPedidoFinanceiro.Text = pedido.GetValorOss.ToString("#0.00");
                grvParcelasPedido.DataSource = pedido.Receita.GetParcelasOrdenadasPeloNumero;
            }
        }

        grvParcelasPedido.DataBind();
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

    private void CarregarArvoreArquivosPedido(Pedido pedido)
    {
        //TreeNode noSelecionado = trvAnexosPedido.SelectedNode;
        //trvAnexosPedido.Nodes.Clear();

        //TreeNode noPai = new TreeNode("Pedido", "Pedido");
        //noPai.ImageUrl = "../imagens/icone_pasta.png";

        //if (pedido != null && pedido.Id > 0)
        //{
        //    //adicionando arquivos do pedido na arvore
        //    if (pedido.Arquivos != null && pedido.Arquivos.Count > 0)
        //    {
        //        foreach (Arquivo arquivoPedido in pedido.Arquivos)
        //        {
        //            TreeNode noArquivoPedido = new TreeNode()
        //            {
        //                Text = arquivoPedido.Nome,
        //                Value = "ARQPED_" + arquivoPedido.Id.ToString(),
        //                ImageUrl = "../imagens/icone_tarefa.png"
        //            };

        //            noPai.ChildNodes.Add(noArquivoPedido);
        //        }
        //    }


        //    if (pedido.OrdensServico != null && pedido.OrdensServico.Count > 0)
        //    {
        //        foreach (OrdemServico ordemPedido in pedido.OrdensServico)
        //        {
        //            TreeNode noOrdem = new TreeNode("OS " + ordemPedido.Codigo, ordemPedido.Id.ToString());
        //            noOrdem.ImageUrl = "../imagens/icone_pasta.png";

        //            //adicionando arquivos da OS
        //            if (ordemPedido.Arquivos != null && ordemPedido.Arquivos.Count > 0)
        //            {
        //                this.AdicionarArquivosNaArvore(ref noOrdem, ordemPedido.Arquivos);
        //            }

        //            //Adicionando as visitas na arvore
        //            if (ordemPedido.Visitas != null && ordemPedido.Visitas.Count > 0)
        //            {
        //                foreach (Visita visita in ordemPedido.Visitas)
        //                {
        //                    if (visita.Arquivos != null && visita.Arquivos.Count > 0)
        //                    {
        //                        TreeNode noVisita = new TreeNode("Visita " + visita.DataInicio.ToShortDateString(), visita.Id.ToString());
        //                        noVisita.ImageUrl = "../imagens/icone_pasta.png";

        //                        this.AdicionarArquivosNaArvore(ref noVisita, visita.Arquivos);

        //                        noOrdem.ChildNodes.Add(noVisita);
        //                    }
        //                }
        //            }

        //            //adicionando atividades na arvore
        //            if (ordemPedido.Atividades != null && ordemPedido.Atividades.Count > 0)
        //            {
        //                foreach (Atividade atividade in ordemPedido.Atividades)
        //                {
        //                    if (atividade.Arquivos != null && atividade.Arquivos.Count > 0)
        //                    {
        //                        TreeNode noAtividade = new TreeNode("Atividade " + atividade.Data.ToShortDateString(), atividade.Id.ToString());
        //                        noAtividade.ImageUrl = "../imagens/icone_pasta.png";

        //                        this.AdicionarArquivosNaArvore(ref noAtividade, atividade.Arquivos);

        //                        noOrdem.ChildNodes.Add(noAtividade);
        //                    }
        //                }
        //            }

        //            noPai.ChildNodes.Add(noOrdem);
        //        }
        //    }
        //}

        //trvAnexosPedido.Nodes.Add(noPai);

        //trvAnexosPedido.ExpandAll();
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

    private void CarregarGridOrdensServico(Pedido pedido)
    {
        gdvOrdensServico.DataSource = pedido != null && pedido.OrdensServico != null ? pedido.OrdensServico : new List<OrdemServico>();
        gdvOrdensServico.DataBind();
    }

    public string BindConteudoDetalhamento(object o)
    {
        Detalhamento detalhamento = (Detalhamento)o;
        return detalhamento.Conteudo;
    }

    private void ExcluirOrdemServico(GridViewDeleteEventArgs e)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(gdvOrdensServico.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (ordem != null)
        {
            if (ordem.DataEncerramento != SqlDate.MinValue && ordem.DataEncerramento != SqlDate.MaxValue)
            {
                msg.CriarMensagem("Não é possível excluir ordens de serviço que estejam encerradas.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (ordem.OssVinculadas != null && ordem.OssVinculadas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir ordens de serviço que possuam outras OS's vinculadas a ela.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.IncluirRegistroExclusaoOsNoDetalhamentoDoPedido(ordem);

            if (ordem.Excluir())
                msg.CriarMensagem("Ordem de Serviço excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregarOS(int idOS)
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(idOS);

        if (ordem != null)
        {
            hdIdOS.Value = idOS.ToString();
            ckbAtivoOS.Checked = ordem.Ativo;
            tbxCodigoOS.Text = ordem.Codigo;
            ddlOSFaturada.SelectedIndex = (ordem.SemCusto ? 2 : 1);

            txtValorNominalOS.Enabled = txtDescontoOS.Enabled = ddlOSFaturada.SelectedIndex == 1;
            txtValorNominalOS.Text = ordem.ValorNominal.ToString("#0.00");
            txtDescontoOS.Text = ordem.Desconto.ToString("#0.00");
            txtValorTotalOS.Text = ordem.ValorTotal.ToString("#0.00");

            tbxDataOS.Text = ordem.Data.ToShortDateString();
            tbxPedidoOS.Text = ordem.Pedido != null ? ordem.Pedido.Codigo + " - " + ordem.Pedido.Data.ToShortDateString() + " - " + ordem.Pedido.GetDescricaoTipo + " - " + ordem.Pedido.GetNomeCliente : "";

            this.CarregarTiposOS();
            ddlTipoOS.SelectedValue = ordem.Tipo.Id.ToString();

            tbxDescricaoOS.Text = ordem.Descricao;
            tbxPrazoPadraoOS.Text = ordem.PrazoPadrao.ToShortDateString();
            tbxPrazoLegalOS.Enabled = tbxPrazoDiretoriaOS.Enabled =
                btnCalcularPrazoLegal.Enabled = btnCalcularPrazoDiretoria.Enabled = false;
            tbxPrazoLegalOS.Text = ordem.PrazoLegal != SqlDate.MinValue ? ordem.PrazoLegal.ToShortDateString() : "";
            tbxPrazoDiretoriaOS.Text = ordem.PrazoDiretoria != SqlDate.MinValue ? ordem.PrazoDiretoria.ToShortDateString() : "";

            this.CarregarDepartamentosOS();

            ddlDepartamentoOS.SelectedValue = ordem.Setor != null ? ordem.Setor.Departamento.Id.ToString() : "0";

            this.CarregarSetoresOS();

            ddlSetorOS.SelectedValue = ordem.Setor != null ? ordem.Setor.Id.ToString() : "0";

            this.CarregarOrgaos();

            ddlOrgaoOS.SelectedValue = ordem.Orgao != null ? ordem.Orgao.Id.ToString() : "0";
            tbxNumeroProcessoOrgaoOS.Text = ordem.NumeroProcessoOrgao;

            this.CarregarResponsaveisOS();

            ddlResponsavelOS.SelectedValue = ordem.Responsavel != null ? ordem.Responsavel.Id.ToString() : "0";

            this.CarregarCorresponsaveis(ordem);


            ckbRenovavelOS.Checked = ordem.Renovavel;

            hfIdOsMatriz.Value = string.Empty;
            txtOsMatriz.Text = "Nenhuma OS vinculada";
            if (ordem.OsMatriz != null && ordem.OsMatriz.Id > 0)
            {
                hfIdOsMatriz.Value = ordem.OsMatriz.Id.ToString();
                txtOsMatriz.Text = ordem.OsMatriz.GetDescricaoOSMatriz;
            }

            if (ordem.Detalhamentos != null && ordem.Detalhamentos.Count > 0)
            {
                detalhamento_edicao_os.Visible = false;
                detalhamento_visualizacao_os.Visible = true;

                Detalhamento ultimoDetalhamentoDaOS = ordem.GetUltimoDelhamento;

                tbxVisualizarDetalhamentoOS.Text = ultimoDetalhamentoDaOS != null ? ultimoDetalhamentoDaOS.Conteudo : "";
            }
            else
            {
                detalhamento_edicao_os.Visible = true;
                detalhamento_visualizacao_os.Visible = false;
            }

            this.CarregarVisitas(ordem);
            this.CarregarAtividades(ordem);
            this.CarregarArvoreArquivosOS(ordem);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>SelecionarPrimeiraTab();</script>", false);

            lblCadastroOS_ModalPopupExtender.Show();

        }
    }

    private void CarregarArvoreArquivosOS(OrdemServico ordem)
    {
        TreeNode noSelecionado = trvArquivosOS.SelectedNode;
        trvArquivosOS.Nodes.Clear();

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

                trvArquivosOS.Nodes.Add(noPedido);
            }
        }

        trvArquivosOS.ExpandAll();
    }

    private void NovaOS()
    {
        ckbAtivoOS.Checked = true;
        tbxCodigoOS.Text = OrdemServico.GerarNumeroCodigo();
        ddlOSFaturada.SelectedIndex = 0;
        txtValorNominalOS.Enabled = txtDescontoOS.Enabled = txtValorTotalOS.Enabled = false;
        txtValorNominalOS.Text = txtDescontoOS.Text = txtValorTotalOS.Text = "0,00";

        Pedido pedidoOS = Pedido.ConsultarPorId(hfId.Value.ToInt32());
        tbxDataOS.Text = pedidoOS.Data.ToShortDateString();

        txtValorNominalOS.Enabled = txtDescontoOS.Enabled = ddlOSFaturada.SelectedValue.ToInt32() == 1;

        tbxPedidoOS.Text = pedidoOS != null ? pedidoOS.Codigo + " - " + pedidoOS.Data.ToShortDateString() + " - " + pedidoOS.GetDescricaoTipo + " - " + pedidoOS.GetNomeCliente : "";

        this.CarregarTiposOS();

        tbxPrazoLegalOS.Enabled = tbxPrazoDiretoriaOS.Enabled =
                btnCalcularPrazoLegal.Enabled = btnCalcularPrazoDiretoria.Enabled = true;
        hdIdOS.Value = tbxDescricaoOS.Text = tbxPrazoPadraoOS.Text = tbxPrazoLegalOS.Text = tbxPrazoDiretoriaOS.Text = tbxNumeroProcessoOrgaoOS.Text =
             editDetalhamentoOS.Content = tbxVisualizarDetalhamentoOS.Text = "";

        this.CarregarDepartamentosOS();

        ddlSetorOS.Items.Clear();
        ddlSetorOS.Items.Insert(0, new ListItem("-- Selecione primeiro o departamento da OS --", "0"));

        this.CarregarOrgaos();
        this.CarregarResponsaveisOS();
        this.CarregarCorresponsaveis(null);

        ckbRenovavelOS.Checked = false;
        lblOSSeraReplicaraPara.Text = "";

        detalhamento_edicao_os.Visible = detalhamento_visualizacao_os.Visible = false;

        hfIdOsMatriz.Value = "0";
        txtOsMatriz.Text = "Nenhuma OS vinculada";

        this.CarregarVisitas(null);
        this.CarregarAtividades(null);
        this.CarregarArvoreArquivosOS(null);

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "script", "<script>SelecionarPrimeiraTab();</script>", false);
        tbxCodigoOS.Focus();
    }

    private void CarregarHistoricoDetalhamentosPedido()
    {
        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());

        IList<Detalhamento> detalhamentos = pedido != null && pedido.Detalhamentos != null ? pedido.Detalhamentos.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Este pedido não possui detalhamentos salvos para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void CarregarHistoricoDetalhamentosOS()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32());

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

    private void ExcluirArquivosPedido()
    {
        //if (trvAnexosPedido.SelectedValue.Contains("ARQPED_"))
        //{
        //    Arquivo arquivo = Arquivo.ConsultarPorId(trvAnexosPedido.SelectedNode.Value.Split('_')[1].ToInt32());

        //    arquivo.Excluir();

        //    msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        //}
    }

    private void ExcluirArquivosOS()
    {
        if (trvArquivosOS.SelectedValue.Contains("ARQPED_"))
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosOS.SelectedNode.Value.Split('_')[1].ToInt32());

            arquivo.Excluir();

            msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void ExcluirPedido()
    {
        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());

        if (pedido != null)
        {
            if (pedido.OrdensServico != null && pedido.OrdensServico.Count > 0)
            {
                string pedidoPodeSerExcluido = pedido.PedidoMotivoNaoPodeSerExcluido;

                if (pedidoPodeSerExcluido != "")
                {
                    msg.CriarMensagem(pedidoPodeSerExcluido, "Informação", MsgIcons.Informacao);
                    return;
                }

            }

            if (pedido.Excluir())
                msg.CriarMensagem("Pedido excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.NovoPedido();
    }

    private void NovoPedido()
    {
        chkPedidoAtivo.Checked = true;

        tbxCodigoPedido.Text = "Gerado Automaticamente...";

        tbxDataPedido.Text = DateTime.Now.ToShortDateString();

        this.CarregarCampos();

        editDetalhamento.Content = tbxDetalhamentoVisualizacao.Text = hfId.Value = "";

        this.CarregarGridOrdensServico(null);
        this.CarregarArvoreArquivosPedido(null);

        detalhamento_edicao.Visible = detalhamento_visualizacao.Visible = false;

    }

    private void SalvarPedido()
    {
        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
        if (Pedido.ExistePedidoComEsteCodigoDiferenteDesse(tbxCodigoPedido.Text, pedido))
        {
            msg.CriarMensagem("Já existe um pedido cadastrado com este código. Informe outro código para prosseguir.", "Informação", MsgIcons.Informacao);
            return;
        }

        bool enviarEmail = pedido == null;
        if (pedido == null)
            pedido = new Pedido();

        pedido.Ativo = chkPedidoAtivo.Checked;
        pedido.Codigo = tbxCodigoPedido.Text;
        pedido.Data = tbxDataPedido.Text.ToDateTime();
        pedido.TipoPedido = TipoPedido.ConsultarPorId(ddlTipoPedido.SelectedValue.ToInt32());
        pedido.Cliente = Cliente.ConsultarPorId(ddlClientePedido.SelectedValue.ToInt32());

        if (ddlVendedor.SelectedValue.ToInt32() > 0)
            pedido.Vendedor = Funcionario.ConsultarPorId(ddlVendedor.SelectedValue.ToInt32());

        //se  detalhamento esta em modo de edição, salvar um novo detalhamento para o pedido
        if (detalhamento_edicao.Visible == true && hfId.Value.ToInt32() > 0)
        {
            if (pedido.Detalhamentos == null)
                pedido.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            detalhamento.Conteudo = editDetalhamento.Content;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            detalhamento = detalhamento.Salvar();

            pedido.Detalhamentos.Add(detalhamento);

            tbxDetalhamentoVisualizacao.Text = detalhamento.Conteudo;

            detalhamento_edicao.Visible = false;
            detalhamento_visualizacao.Visible = true;
        }

        pedido = pedido.Salvar();
        hfId.Value = pedido.Id.ToString();

        if (enviarEmail)
            this.EnviarEmailAberturaPedido(pedido);

        //Vamos criar as OSs agora de acordo com os itens do orçamento!
        if (this.OrcamentoCadastrar != null && this.OrcamentoCadastrar.Pedido == null)
        {
            pedido.OrdensServico = new List<OrdemServico>();
            this.OrcamentoCadastrar = this.OrcamentoCadastrar.ConsultarPorId();
            foreach (ItemOrcamento item in this.OrcamentoCadastrar.Itens)
            {
                OrdemServico os = new OrdemServico();
                os.Status = OrdemServico.ABERTA;
                os.Codigo = OrdemServico.GerarNumeroCodigo();
                os.SemCusto = false;
                os.Data = DateTime.Now;
                os.Pedido = pedido;
                os.Tipo = item.Tipo;
                os.Descricao = item.Descricao;
                os.PrazoPadrao = os.Data.AddDays(item.Tipo.PrazoPadrao);
                os.Orgao = this.OrcamentoCadastrar.OrgaoResponsavel;
                os.Setor = item.Setor;
                os.ValorNominal = item.ValorUnitario;
                os.Desconto = item.Desconto;

                editDetalhamento.Content += @"<br /><br /> Incluída Ordem de Serviço nº " + os.Codigo + ". Data: " + os.Data.ToShortDateString() + @"<br /> Descrição:<br />" + os.Descricao;

                pedido.OrdensServico.Add(os.Salvar());
            }

            detalhamento_edicao.Visible = true;
            detalhamento_visualizacao.Visible = false;

            //Carregar os anexos do orçamento no pedido
            //if (this.OrcamentoCadastrar.Arquivos != null)
            //{
            //    if (pedido.Arquivos == null)
            //        pedido.Arquivos = new List<Arquivo>();
            //    foreach (Arquivo arquivo in this.OrcamentoCadastrar.Arquivos)
            //    {
            //        Arquivo arquivoAux = arquivo.CloneObject<Arquivo>();
            //        arquivoAux.Id = 0;
            //        pedido.Arquivos.Add(arquivoAux.Salvar());
            //    }
            //    this.CarregarArvoreArquivosPedido(pedido);
            //}

            this.CarregarGridOrdensServico(pedido);
            this.OrcamentoCadastrar.Pedido = pedido;
            pedido.Orcamento = this.OrcamentoCadastrar.Salvar();
            pedido = pedido.Salvar();
        }
        pnlOrcamentoCarregado.Visible = false;
        lblOrcamentoVinculado.Text = string.Empty;
        this.CarregarFinanceiro(pedido);
        this.OrcamentoCadastrar = null;

        btnContratoPedido.Enabled = true;
        btnContratoPedido.Text = pedido.GetStatusContrato;
        btnContratoPedido.ToolTip = !pedido.IsContratoEnviado ? "Clique para gerar e enviar o contrato" : "Clique para visualizar o contrato do pedido";
        btnSalvarPedido.Text = "Salvar";
        msg.CriarMensagem("Pedido salvo com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    private void EnviarEmailAberturaPedido(Pedido pedido)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis - Novo pedido realizado";
        mail.BodyHtml = true;
        mail.Mensagem = new Email().GetTemplateAberturaPedido(pedido);
        mail.EmailsDestino.Add(pedido.Cliente.Email);
        mail.EnviarAutenticado("Novo pedido realizado", this.FuncionarioLogado, pedido, 587, false);
    }

    private void CarregarCorresponsaveis(OrdemServico ordem)
    {
        cblCorresponsaveis.Items.Clear();

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        if (funcionarios != null && funcionarios.Count > 0)
        {
            foreach (Funcionario funcionario in funcionarios)
            {
                ListItem itemFuncionario = new ListItem(funcionario.NomeRazaoSocial, funcionario.Id.ToString());

                if (ordem != null && ordem.CoResponsaveis != null && ordem.CoResponsaveis.Count > 0 && ordem.CoResponsaveis.Contains(funcionario))
                    itemFuncionario.Selected = true;

                if (ddlResponsavelOS.SelectedValue != funcionario.Id.ToString())
                    cblCorresponsaveis.Items.Add(itemFuncionario);
            }
        }

    }

    private void SalvarOS()
    {
        OrdemServico ordem = OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32());

        if (ordem == null)
        {
            ordem = new OrdemServico();
            ordem.PrazoLegal = tbxPrazoLegalOS.Text.ToDateTime();
            ordem.PrazoDiretoria = tbxPrazoDiretoriaOS.Text.ToDateTime();
        }

        ordem.Ativo = ckbAtivoOS.Checked;
        ordem.Codigo = tbxCodigoOS.Text;

        if (hdIdOS.Value.ToInt32() <= 0)
            ordem.Status = OrdemServico.ABERTA;

        ordem.SemCusto = (ddlOSFaturada.SelectedIndex == 2);
        if (!ordem.SemCusto)
        {
            ordem.ValorNominal = txtValorNominalOS.Text.ToDecimal();
            ordem.Desconto = txtDescontoOS.Text.ToDecimal();
        }
        else
            ordem.ValorNominal = ordem.Desconto = 0;
        ordem.Data = tbxDataOS.Text.ToDateTime();
        ordem.Pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
        ordem.Tipo = TipoOrdemServico.ConsultarPorId(ddlTipoOS.SelectedValue.ToInt32());
        ordem.Descricao = tbxDescricaoOS.Text;
        ordem.PrazoPadrao = tbxPrazoPadraoOS.Text.ToDateTime();
        ordem.Setor = Setor.ConsultarPorId(ddlSetorOS.SelectedValue.ToInt32());
        ordem.Orgao = Orgao.ConsultarPorId(ddlOrgaoOS.SelectedValue.ToInt32());
        ordem.NumeroProcessoOrgao = tbxNumeroProcessoOrgaoOS.Text;
        ordem.Responsavel = Funcionario.ConsultarPorId(ddlResponsavelOS.SelectedValue.ToInt32());

        //Salvando as funções do funcionário
        if (cblCorresponsaveis.Items != null && cblCorresponsaveis.Items.Count > 0)
        {
            ordem.CoResponsaveis = new List<Funcionario>();
            foreach (ListItem item in cblCorresponsaveis.Items)
                if (item.Selected)
                {
                    Funcionario funcionario = Funcionario.ConsultarPorId(item.Value.ToInt32());

                    if (funcionario != null && !ordem.CoResponsaveis.Contains(funcionario))
                        ordem.CoResponsaveis.Add(funcionario);
                }
        }

        ordem.Renovavel = ckbRenovavelOS.Checked;
        ordem.OsMatriz = null;
        ordem.OsMatriz = OrdemServico.ConsultarPorId(hfIdOsMatriz.Value.ToInt32());
        //se  detalhamento esta em modo de edição, salvar um novo detalhamento para o pedido
        if (detalhamento_edicao_os.Visible == true && hdIdOS.Value.ToInt32() > 0)
        {
            if (ordem.Detalhamentos == null)
                ordem.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            detalhamento.Conteudo = editDetalhamentoOS.Content;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            detalhamento = detalhamento.Salvar();

            ordem.Detalhamentos.Add(detalhamento);

            tbxVisualizarDetalhamentoOS.Text = detalhamento.Conteudo;

            detalhamento_edicao_os.Visible = false;
            detalhamento_visualizacao_os.Visible = true;
        }

        ordem = ordem.Salvar();

        if (hdIdOS.Value.ToInt32() <= 0)
        {
            this.IncluirRegistroInclusaoOsNoDetalhamentoDoPedido(ordem);
            this.EnviarEmailOSCadastradaParaOResponsavelEGestorDoDepartamento(ordem);
        }


        hdIdOS.Value = ordem.Id.ToString();
        tbxCodigoOS.Text = ordem.Codigo;

        //Se a ordem de serviço for periodica e houver datas para replicação replicar a os para as datas escolhidas
        if (ckbRenovavelOS.Checked && this.DatasParaReplicacao != null && this.DatasParaReplicacao.Count > 0)
            this.ReplicarOSPeriodica(ordem);

        this.CarregarFinanceiro(ordem.Pedido);
        msg.CriarMensagem("Ordem de Serviço salva com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    private void ReplicarOSPeriodica(OrdemServico ordem)
    {
        foreach (DateTime dataReplicar in this.DatasParaReplicacao)
        {
            OrdemServico ordemNova = ordem.CloneObject<OrdemServico>();

            ordemNova.Id = 0;

            ordemNova.Codigo = OrdemServico.GerarNumeroCodigo();

            ordemNova.Data = dataReplicar;

            TipoOrdemServico tipo = ordem.Tipo;

            ordemNova.PrazoPadrao = ordem.TipoRenovacao == OrdemServico.DIAS ? ordemNova.Data.AddDays(tipo.PrazoPadrao) : ordemNova.Data.AddMonths(tipo.PrazoPadrao);

            if (ordem.PrazoLegal != SqlDate.MinValue)
                ordemNova.PrazoLegal = ordemNova.PrazoPadrao;

            if (ordem.PrazoDiretoria != SqlDate.MinValue)
                ordemNova.PrazoDiretoria = ordemNova.PrazoPadrao;

            ordemNova.Renovavel = true;
            ordemNova.PrazoRenovacao = ordem.PrazoRenovacao;
            ordemNova.TipoRenovacao = ordem.TipoRenovacao;

            ordemNova = ordemNova.Salvar();

            this.EnviarEmailOSCadastradaParaOResponsavelEGestorDoDepartamento(ordemNova);

        }

        this.DatasParaReplicacao = null;
        lblOSSeraReplicaraPara.Text = "";
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
        {
            mail.EnviarAutenticado("Cadastro de OS", this.FuncionarioLogado, ordem.Pedido, 587, false);
        }
    }

    public string BindVisualizarVisita(Object o)
    {
        Visita n = (Visita)o;
        return "../Visitas/CadastroDeVisita.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id + "&sovisu=sim");
    }

    public string BindVisualizarAtividade(Object o)
    {
        Atividade n = (Atividade)o;
        return "../Atividades/CadastroDeAtividade.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id + "&sovisu=sim");
    }

    private void IncluirRegistroInclusaoOsNoDetalhamentoDoPedido(OrdemServico ordem)
    {
        detalhamento_edicao.Visible = true;
        detalhamento_visualizacao.Visible = false;

        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());

        Detalhamento ultimoDetalhamento = pedido.GetUltimoDelhamento;

        editDetalhamento.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

        editDetalhamento.Content += @"<br /><br /> Incluída Ordem de Serviço nº " + ordem.Codigo + ". Data: " + ordem.Data.ToShortDateString() + @"<br /> Descrição:<br />" + ordem.Descricao;
    }

    private void IncluirRegistroExclusaoOsNoDetalhamentoDoPedido(OrdemServico ordem)
    {
        detalhamento_edicao.Visible = true;
        detalhamento_visualizacao.Visible = false;

        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());

        Detalhamento ultimoDetalhamento = pedido.GetUltimoDelhamento;

        editDetalhamento.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

        editDetalhamento.Content += @"<br /><br /> Excluída Ordem de Serviço nº " + ordem.Codigo + ". Data: " + ordem.Data.ToShortDateString() + @"<br /> Descrição:<br />" + ordem.Descricao;
    }

    public string BindDataReplicacao(object o)
    {
        DateTime data = (DateTime)o;
        return data.ToShortDateString();
    }

    private void CarregarDatasParaReplicacao()
    {
        gdvDatasReplicacao.DataSource = this.DatasParaReplicacao;
        gdvDatasReplicacao.DataBind();
    }

    private void CarregarContrato(Pedido pedido)
    {
        if (pedido == null)
            return;
        //Somente pode editar se ainda não foi aceito
        txtDestinatariosContratoPopup.Enabled = !pedido.IsContratoEnviado;
        txtEmailContratoPopup.Enabled = !pedido.IsContratoAceito;

        btnReenviarEmailAceite.Visible = btnSalvarContrato.Visible = pedido.IsContratoEnviado && !pedido.IsContratoAceito;
        btnEnviarContrato.Visible = !pedido.IsContratoEnviado;

        txtStatusContratoPopup.Text = pedido.GetStatusContrato;
        txtDestinatariosContratoPopup.Text = pedido.Cliente.Email;

        lblTituloContratacao.Text = (pedido.IsContratoEnviado ? "Contrato do pedido*" : "E-mail para aceite do contrato*");

        //Se não foi enviado, mostrar o e-mail que será enviado para o cliente, senão, mostrar o contrato mesmo
        if (!pedido.IsContratoEnviado)
            txtEmailContratoPopup.Content = TextoPadrao.ConsultarPorTipo(TextoPadrao.MODELOEMAILCONTRATO).AtualizarVariaveis(pedido).GetTextoComSubstituicaoDeTags();
        else
        {
            txtDestinatariosContratoPopup.Text = pedido.DestinatariosContrato;
            txtEmailContratoPopup.Content = pedido.TextoContrato;
        }
    }

    private void EnviarEmailContratoPedido()
    {
        if (string.IsNullOrEmpty(txtDestinatariosContratoPopup.Text.Trim()))
        {
            msg.CriarMensagem("Informe ao menos um destinatário!", "Erro", MsgIcons.Alerta);
            return;
        }

        if (string.IsNullOrEmpty(txtEmailContratoPopup.Content.Trim()))
        {
            msg.CriarMensagem("Informe um texto para o e-mail!", "Erro", MsgIcons.Alerta);
            return;
        }

        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
        if (pedido == null)
        {
            msg.CriarMensagem("O pedido não foi corretamente carregado! Recarregue a página!", "Erro", MsgIcons.Alerta);
            return;
        }

        pedido.DestinatariosContrato = txtDestinatariosContratoPopup.Text.Trim();
        pedido.DataEnvioContrato = DateTime.Now;
        pedido = pedido.Salvar();
        pedido.TextoContrato = TextoPadrao.ConsultarPorTipo(TextoPadrao.MODELOCONTRATO).AtualizarVariaveis(pedido).GetTextoComSubstituicaoDeTags();
        pedido = pedido.Salvar();

        Email email = new Email();
        email.Assunto = "Contrato Ambientalis";
        foreach (String aux in txtDestinatariosContratoPopup.Text.Trim().Split(';'))
            email.EmailsDestino.Add(aux);
        email.Mensagem = txtEmailContratoPopup.Content;
        email.Enviar("Envio contrato pedido", this.FuncionarioLogado, pedido);

        //Atualizando o status
        btnContratoPedido.Enabled = true;
        btnContratoPedido.Text = pedido.GetStatusContrato;
        btnContratoPedido.ToolTip = (!pedido.IsContratoEnviado ? "Clique para gerar e enviar o contrato" : "Clique para visualizar o contrato do pedido");
        this.CarregarContrato(pedido);
        msg.CriarMensagem("O e-mail para aceite de contrato foi enviado com sucesso!!", "Sucesso", MsgIcons.Sucesso);
    }

    private void GerarMovimentacaoPedido(Pedido pedido)
    {
        if (hfRating.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("Informe uma classificação para o cliente!", "Erro", MsgIcons.Alerta);
            return;
        }
        if (txtValorTotalPedido.Text.ToDecimal() <= 0)
        {
            msg.CriarMensagem("Informe um valor para o pedido!", "Erro", MsgIcons.Alerta);
            return;
        }
        if (string.IsNullOrEmpty(txtPrimeiroPagamento.Text))
        {
            msg.CriarMensagem("Informe uma data para o primeiro pagamento do pedido!", "Erro", MsgIcons.Alerta);
            return;
        }
        if (pedido.Receita != null)
        {
            this.CarregarFinanceiro(pedido);
            msg.CriarMensagem("O financeiro deste pedido já foi gerado!", "Erro", MsgIcons.Alerta);
            return;
        }

        Receita receita = new Receita();
        receita.Pedido = pedido;
        receita.ClienteFornecedor = pedido.Cliente;
        receita.Data = txtPrimeiroPagamento.Text.ToDateTime();
        receita.ValorNominal = txtValorTotalPedido.Text.ToDecimal();
        receita.Descricao = "Receita do pedido " + pedido.Codigo;

        receita = receita.Salvar();

        receita.Parcelas = new List<Parcela>();
        Caixa caixaSelecionado = Caixa.ConsultarPorId(ddlCaixaGerarFinanceiro.SelectedValue.ToInt32());
        DateTime dataPrimeiroPagamento = txtPrimeiroPagamento.Text.ToDateTime();
        int qtdParcelas = txtDividirEm.Text.ToInt32();
        decimal valorNominalParcela = receita.ValorNominal / (qtdParcelas > 0 ? qtdParcelas : 1);
        int index = 0;
        do
        {
            Parcela parcela = new Parcela();
            parcela.MovimentacaoFinanceira = receita;
            parcela.Caixa = caixaSelecionado;
            parcela.DataEmissao = DateTime.Now;
            parcela.DataVencimento = dataPrimeiroPagamento;
            parcela.Numero = (index + 1);
            parcela.Quantidade = qtdParcelas;
            parcela.ValorNominal = valorNominalParcela;
            receita.Parcelas.Add(parcela.Salvar());
            dataPrimeiroPagamento = dataPrimeiroPagamento.AddMonths(1);
            index++;
        } while (index < qtdParcelas);

        pedido.Receita = receita;

        ClassificacaoCliente classificacao = new ClassificacaoCliente();
        classificacao.Cliente = pedido.Cliente;
        classificacao.Data = DateTime.Now;
        classificacao.Classificacao = hfRating.Value.ToInt32();
        if (pedido.Cliente.Classificacoes == null)
            pedido.Cliente.Classificacoes = new List<ClassificacaoCliente>();
        pedido.Cliente.Classificacoes.Add(classificacao.Salvar());

        pedido = pedido.Salvar();
        this.CarregarFinanceiro(pedido);
        msg.CriarMensagem("Financeiro gerado com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    public string BindingValorNominalTotalParcelas()
    {
        return String.Format("{0:c}", Pedido.ConsultarPorId(hfId.Value.ToInt32()).Receita.ValorNominal);
    }

    public string BindingDescontoTotalParcelas()
    {
        return String.Format("{0:c}", Pedido.ConsultarPorId(hfId.Value.ToInt32()).Receita.Desconto);
    }

    public string BindingValorTotalParcelas()
    {
        return String.Format("{0:c}", Pedido.ConsultarPorId(hfId.Value.ToInt32()).Receita.ValorTotal);
    }

    public Color BindingCorValorTotalParcelas()
    {
        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
        return (pedido != null && pedido.Receita != null && !pedido.GetValorOss.Equals(pedido.Receita.ValorTotal) ? Color.FromArgb(230, 0, 0) : Color.FromArgb(0, 0, 0));
    }

    private void AdicionarParcelaAoPedido()
    {
        Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
        if (pedido == null)
        {
            msg.CriarMensagem("O pedido não foi corretamente carregado! Recarregue a página", "Erro", MsgIcons.Alerta);
            return;
        }
        if (pedido.Receita == null)
        {
            msg.CriarMensagem("O pedido não possui uma receita para gerar parcelas!", "Erro", MsgIcons.Alerta);
            return;
        }

        if (pedido.Receita.Parcelas == null)
            pedido.Receita.Parcelas = new List<Parcela>();
        Parcela ultimaParcela = (pedido.Receita.Parcelas.Count > 0 ? pedido.Receita.Parcelas[pedido.Receita.Parcelas.Count - 1] : null);

        Parcela novaParcela = new Parcela();
        novaParcela.DataEmissao = DateTime.Now;
        novaParcela.DataVencimento = (ultimaParcela != null ? ultimaParcela.DataVencimento.AddMonths(1) : DateTime.Now);
        novaParcela.MovimentacaoFinanceira = pedido.Receita;
        novaParcela.Numero = (ultimaParcela != null ? ultimaParcela.Numero + 1 : 1);
        novaParcela.Quantidade = 1;
        novaParcela.ValorNominal = (ultimaParcela != null ? ultimaParcela.ValorNominal : 0);

        pedido.Receita.adicionarParcela(novaParcela.Salvar());
        pedido.Receita.Salvar();
        this.CarregarFinanceiro(pedido);

        msg.CriarMensagem("Parcela adicionada!", "Sucesso", MsgIcons.Sucesso);
    }

    private void SalvarParcelaEdicao()
    {
        Parcela parcela = Parcela.ConsultarPorId(hfIdParcelaEdicao.Value.ToInt32());
        if (parcela == null)
        {
            msg.CriarMensagem("Não é possível carregar a parcela! Recarregue a página e tente novamente", "Erro", MsgIcons.Alerta);
            return;
        }

        if (!parcela.IsPodeEditar)
        {
            msg.CriarMensagem("Essa parcela já não pode ser editada!", "Erro", MsgIcons.Alerta);
            return;
        }

        parcela.DataVencimento = txtVencimentoParcela.Text.ToDateTime();
        parcela.ValorNominal = txtValoNominalParcela.Text.ToDecimal();
        parcela.Descontos = txtDescontoParcela.Text.ToDecimal();
        parcela.Caixa = Caixa.ConsultarPorId(ddlCaixaParcela.SelectedValue.ToInt32());
        parcela.MovimentacaoFinanceira.Parcelas.Remove(parcela);
        parcela.MovimentacaoFinanceira.Parcelas.Add(parcela.Salvar());

        parcela.MovimentacaoFinanceira.ValorNominal = parcela.MovimentacaoFinanceira.GetValorNominalTotalParcelas;
        parcela.MovimentacaoFinanceira.Desconto = parcela.MovimentacaoFinanceira.GetDescontoTotalParcelas;
        parcela.MovimentacaoFinanceira = parcela.MovimentacaoFinanceira.Salvar();

        this.CarregarFinanceiro(parcela.MovimentacaoFinanceira.Pedido);
        msg.CriarMensagem("Parcela salva com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    private void CarregarEdicaoParcela(Parcela parcela)
    {
        if (parcela == null)
        {
            msg.CriarMensagem("Parcela inválida! Recarregue a página e tente novamente", "Erro", MsgIcons.Alerta);
            return;
        }
        hfIdParcelaEdicao.Value = parcela.Id.ToString();
        txtVencimentoParcela.Text = parcela.DataVencimento.ToShortDateString();
        txtValoNominalParcela.Text = parcela.ValorNominal.ToString("#0.00");
        txtDescontoParcela.Text = parcela.Descontos.ToString("#0.00");
        if (parcela.Caixa != null)
            ddlCaixaParcela.SelectedValue = parcela.Caixa.Id.ToString();
        else
            ddlCaixaParcela.SelectedIndex = 0;
        this.PopupParcela_ModalPopupExtender.Show();
    }

    #endregion

    #region _______________ Eventos ________________

    protected void gdvOrdensServico_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvOrdensServico.PageIndex = e.NewPageIndex;
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            this.CarregarGridOrdensServico(pedido);
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

    protected void gdvOrdensServico_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirOrdemServico(e);
            Transacao.Instance.Recarregar();
            this.CarregarGridOrdensServico(Pedido.ConsultarPorId(hfId.Value.ToInt32()));

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

    protected void btnGridEditarOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOS(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnNovaOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o pedido para poder inserir Ordens de Serviço no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.NovaOS();
            lblCadastroOS_ModalPopupExtender.Show();
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

    protected void btnVerHistorioDetalhamentosPedidos_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o pedido para poder visualizar seu histórico de detalhamentos.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarHistoricoDetalhamentosPedido();
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

    protected void btnEditarDetalhamento_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o pedido para poder alterar seu detalhamento.", "Informação", MsgIcons.Informacao);
                return;
            }

            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());

            Detalhamento ultimoDetalhamento = pedido.GetUltimoDelhamento;

            editDetalhamento.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

            detalhamento_edicao.Visible = true;
            detalhamento_visualizacao.Visible = false;
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

    protected void btnNovoArquivoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o pedido para poder inserir arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            Session["objeto_upload_pedido"] = "pedido";
            Session["id_pedido_arquivos"] = hfId.Value;

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

    protected void btnNovoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CadastroPedidos.aspx");
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

    protected void btnExcluirArquivoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o pedido para poder excluir arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            //if (!trvAnexosPedido.SelectedValue.IsNotNullOrEmpty() || !trvAnexosPedido.SelectedValue.Contains("ARQPED_"))
            //{
            //    msg.CriarMensagem("Selecione um arquivo para ser excluído.", "Informação", MsgIcons.Informacao);
            //    return;
            //}

            this.ExcluirArquivosPedido();
            Transacao.Instance.Recarregar();
            this.CarregarArvoreArquivosPedido(Pedido.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnRenomearArquivoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o pedido para poder renomear arquivos no mesmo.", "Informação", MsgIcons.Informacao);
                return;
            }

            //if (!trvAnexosPedido.SelectedValue.IsNotNullOrEmpty() || !trvAnexosPedido.SelectedValue.Contains("ARQPED_"))
            //{
            //    msg.CriarMensagem("Selecione um arquivo para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
            //    return;
            //}

            //hfIdArquivoRenomear.Value = trvAnexosPedido.SelectedNode.Value.Split('_')[1];
            //tbxNovoNomeArquivo.Text = "";

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

            arquivo.Nome = tbxNovoNomeArquivo.Text;

            arquivo = arquivo.Salvar();

            lblRenomearArquivos_ModalPopupExtender.Hide();

            Transacao.Instance.Recarregar();

            if (hfArquivoOS.Value == "os")
                this.CarregarArvoreArquivosOS(OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32()));

            this.CarregarArvoreArquivosPedido(Pedido.ConsultarPorId(hfId.Value.ToInt32()));

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

    protected void btnExcluirPedido_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Nenhum pedido salvo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirPedido();
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

    protected void btnSalvarPedido_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarPedido();
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

    protected void ddlDepartamentoOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarSetoresOS();
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

    protected void btnNovoCadastroOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaOS();
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

    protected void ddlTipoOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TipoOrdemServico tipo = TipoOrdemServico.ConsultarPorId(ddlTipoOS.SelectedValue.ToInt32());

            DateTime dataOS = tbxDataOS.Text.ToDateTime();

            tbxPrazoPadraoOS.Text = tipo != null ? dataOS.AddDays(+tipo.PrazoPadrao).ToShortDateString() : "";
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

    protected void ddlResponsavelOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCorresponsaveis(OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32()));
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

    protected void ddlPedidoOSMatrizOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarOSsMatriz();
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

    protected void btnVisualizarDetalhamentosOS_Click(object sender, EventArgs e)
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

    protected void btnNovoArquivoOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para poder inserir arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            Session["objeto_upload_pedido"] = "os";
            Session["id_os_arquivos"] = hdIdOS.Value;

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
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para poder excluir arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosOS.SelectedValue.IsNotNullOrEmpty() || !trvArquivosOS.SelectedValue.Contains("ARQPED_") || trvArquivosOS.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da OS para ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirArquivosOS();
            Transacao.Instance.Recarregar();
            this.CarregarArvoreArquivosOS(OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32()));
            this.CarregarArvoreArquivosPedido(Pedido.ConsultarPorId(hfId.Value.ToInt32()));
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
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para poder renomear arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosOS.SelectedValue.IsNotNullOrEmpty() || !trvArquivosOS.SelectedValue.Contains("ARQPED_") || trvArquivosOS.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da OS para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            hfIdArquivoRenomear.Value = trvArquivosOS.SelectedNode.Value.Split('_')[1];
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

    protected void btnSalvarOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarOS();
            Transacao.Instance.Recarregar();
            this.CarregarGridOrdensServico(Pedido.ConsultarPorId(hfId.Value.ToInt32()));
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

            this.CarregarArvoreArquivosOS(OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32()));
            this.CarregarArvoreArquivosPedido(Pedido.ConsultarPorId(hfId.Value.ToInt32()));
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

            this.CarregarVisitas(OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32()));
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

            this.CarregarAtividades(OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32()));
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

    protected void btnEditarDetalhamentoOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para poder alterar seu detalhamento.", "Informação", MsgIcons.Informacao);
                return;
            }

            OrdemServico ordem = OrdemServico.ConsultarPorId(hdIdOS.Value.ToInt32());

            Detalhamento ultimoDetalhamento = ordem.GetUltimoDelhamento;

            editDetalhamentoOS.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

            detalhamento_edicao_os.Visible = true;
            detalhamento_visualizacao_os.Visible = false;
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

    protected void lkbExibirDadosOS_Click(object sender, EventArgs e)
    {
        try
        {

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

    protected void lkbExibirVisitasOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
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

    protected void lkbExibirAtividadesOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
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

    protected void lkbExibirArquivosOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdIdOS.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a ordem de serviço para prosseguir.", "Informação", MsgIcons.Informacao);
                return;
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

    protected void trvAnexosPedido_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            //if (!trvAnexosPedido.SelectedValue.IsNotNullOrEmpty() || !trvAnexosPedido.SelectedValue.Contains("ARQPED_"))
            //    visualizar_arquivos_pedido.Visible = false;
            //else
            //{
            //    Arquivo arquivo = Arquivo.ConsultarPorId(trvAnexosPedido.SelectedNode.Value.Split('_')[1].ToInt32());

            //    visualizar_arquivos_pedido.Visible = true;

            //    hplArquivosPedido.NavigateUrl = arquivo.UrlImagem;
            //}
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

    protected void trvArquivosOS_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!trvArquivosOS.SelectedValue.IsNotNullOrEmpty() || !trvArquivosOS.SelectedValue.Contains("ARQPED"))
                visualizar_arquivos_os.Visible = false;
            else
            {
                Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosOS.SelectedNode.Value.Split('_')[1].ToInt32());

                visualizar_arquivos_os.Visible = true;

                hplArquivosOS.NavigateUrl = arquivo.UrlImagem;
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

    protected void btnCalcularPrazoLegal_Click(object sender, EventArgs e)
    {
        try
        {
            tbxDataRetirada.Text = tbxPrazoCumprimentoDias.Text = "";
            hfCalcularPrazo.Value = "PrazoLegal";

            lblCalcularPrazos_ModalPopupExtender.Show();
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

    protected void btnCalcularPrazoDiretoria_Click(object sender, EventArgs e)
    {
        try
        {
            tbxDataRetirada.Text = tbxPrazoCumprimentoDias.Text = "";
            hfCalcularPrazo.Value = "PrazoDiretoria";

            lblCalcularPrazos_ModalPopupExtender.Show();
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

    protected void btnCalcular_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime prazo = tbxDataRetirada.Text.ToDateTime();

            prazo = prazo.AddDays(tbxPrazoCumprimentoDias.Text.ToInt32());

            if (hfCalcularPrazo.Value == "PrazoDiretoria")
                tbxPrazoDiretoriaOS.Text = prazo.ToShortDateString();

            if (hfCalcularPrazo.Value == "PrazoLegal")
                tbxPrazoLegalOS.Text = prazo.ToShortDateString();

            lblCalcularPrazos_ModalPopupExtender.Hide();
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

    protected void ckbRenovavelOS_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ckbRenovavelOS.Checked)
            {
                this.DatasParaReplicacao = null;
                tbxDataReplicarPara.Text = "";
                this.CarregarDatasParaReplicacao();
                PopReplicarOSPeriodica_ModalPopupExtender.Show();
            }

            lblOSSeraReplicaraPara.Text = "";
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

    protected void gdvDatasReplicacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvDatasReplicacao.PageIndex = e.NewPageIndex;
            this.CarregarDatasParaReplicacao();

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

    protected void btnAddDataReplicacao_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.DatasParaReplicacao == null)
                this.DatasParaReplicacao = new List<DateTime>();

            this.DatasParaReplicacao.Add(tbxDataReplicarPara.Text.ToDateTime());
            this.CarregarDatasParaReplicacao();

            if (!lblOSSeraReplicaraPara.Text.IsNotNullOrEmpty())
                lblOSSeraReplicaraPara.Text = "OS será replicada para as datas: " + tbxDataReplicarPara.Text;
            else
                lblOSSeraReplicaraPara.Text += "; " + tbxDataReplicarPara.Text;

            tbxDataReplicarPara.Text = "";
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

    protected void btnEnviarEmailPedido_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            if (pedido == null)
                msg.CriarMensagem("Nenhum pedido salvo", "Informação", MsgIcons.Informacao);
            else
                this.EnviarEmailAberturaPedido(pedido);
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

    protected void btnEnviarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            this.EnviarEmailContratoPedido();
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

    protected void btnContratoPedido_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            if (pedido != null)
            {
                this.CarregarContrato(pedido);
                this.PopupContrato_ModalPopupExtender.Show();
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

    protected void btnSalvarContrato_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            if (pedido == null)
            {
                msg.CriarMensagem("O pedido não foi corretamente carregado! Recarregue a página!", "Erro", MsgIcons.Alerta);
                return;
            }
            if (string.IsNullOrEmpty(txtEmailContratoPopup.Content.Trim()))
            {
                msg.CriarMensagem("Informe um texto para o e-mail!", "Erro", MsgIcons.Alerta);
                return;
            }
            if (pedido.IsContratoAceito)
            {
                this.CarregarContrato(pedido);
                msg.CriarMensagem("Não é possível alterar um contrato já salvo!", "Erro", MsgIcons.Alerta);
                return;
            }

            pedido.TextoContrato = txtEmailContratoPopup.Content.Trim();
            pedido = pedido.Salvar();
            msg.CriarMensagem("Contrato salvo!", "Sucesso", MsgIcons.Sucesso);
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

    protected void ddlOSFaturada_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtValorNominalOS.Enabled = txtDescontoOS.Enabled = ddlOSFaturada.SelectedValue.ToInt32() == 1;
            if (ddlOSFaturada.SelectedValue.ToInt32() != 1)
                txtValorNominalOS.Text = txtDescontoOS.Text = txtValorTotalOS.Text = "0,00";
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

    protected void btnGerarFinanceiro_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            if (pedido == null)
            {
                msg.CriarMensagem("O pedido não foi corretamente carregado! Recarregue a página!", "Erro", MsgIcons.Alerta);
                return;
            }

            this.GerarMovimentacaoPedido(pedido);
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

    protected void btnNovaParcela_Click(object sender, EventArgs e)
    {
        try
        {
            this.AdicionarParcelaAoPedido();
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

    protected void btnGridExcluirParcela_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            if (pedido == null)
            {
                msg.CriarMensagem("O pedido não foi corretamente carregado! Recarregue a página", "Erro", MsgIcons.Alerta);
                return;
            }

            Parcela parcela = Parcela.ConsultarPorId((sender as Button).CommandArgument.ToInt32());
            if (parcela == null)
            {
                msg.CriarMensagem("Parcela inválida! Recarregue a página!", "Erro", MsgIcons.Alerta);
                return;
            }

            if (parcela.IsFaturada)
            {
                msg.CriarMensagem("Não é possível excluir uma parcela faturada!", "Erro", MsgIcons.Alerta);
                return;
            }

            if (parcela.IsPaga)
            {
                msg.CriarMensagem("Não é possível excluir uma parcela paga!", "Erro", MsgIcons.Alerta);
                return;
            }

            pedido.Receita.removerParcela(parcela);
            parcela.Excluir();
            pedido.Receita.Salvar();
            pedido = pedido.Salvar();
            this.CarregarFinanceiro(pedido);
            msg.CriarMensagem("Parcela excluída com sucesso!", "Sucesso", MsgIcons.Sucesso);
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

    protected void btnSalvarParcela_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarParcelaEdicao();
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

    protected void btnGridEditarParcela_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarEdicaoParcela(Parcela.ConsultarPorId((sender as Button).CommandArgument.ToInt32()));
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

    protected void btnVincularOsMatriz_Click(object sender, EventArgs e)
    {
        try
        {
            OrdemServico os = OrdemServico.ConsultarPorId(ddlOSMatrizOS.SelectedValue.ToInt32());
            if (os == null)
            {
                msg.CriarMensagem("A OS escolhida não é uma OS válida", "Informação", MsgIcons.Informacao);
                return;
            }
            hfIdOsMatriz.Value = os.Id.ToString();
            txtOsMatriz.Text = os.GetDescricaoOSMatriz;
            this.PopupOSMatriz_ModalPopupExtender.Hide();
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

    protected void btnEscolherOSMatriz_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarPedidosOS();
            this.CarregarOSsMatriz();
            this.PopupOSMatriz_ModalPopupExtender.Show();
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

    protected void btnRemoverOsMatriz_Click(object sender, EventArgs e)
    {
        try
        {
            hfIdOsMatriz.Value = "0";
            txtOsMatriz.Text = "Nenhuma OS vinculada";
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

    protected void btnGerarProximoCodigo_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
                tbxCodigoPedido.Text = new Pedido().ProximoCodigo();
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

    protected void btnReenviarEmailAceite_Click(object sender, EventArgs e)
    {
        try
        {
            Pedido pedido = Pedido.ConsultarPorId(hfId.Value.ToInt32());
            if (pedido == null)
            {
                msg.CriarMensagem("O pedido não foi corretamente carregado! Recarregue a página!", "Erro", MsgIcons.Alerta);
                return;
            }

            txtDestinatariosContratoPopup.Enabled = txtEmailContratoPopup.Enabled = true;
            btnReenviarEmailAceite.Visible = btnSalvarContrato.Visible = false;
            btnEnviarContrato.Visible = true;
            txtStatusContratoPopup.Text = pedido.GetStatusContrato;
            lblTituloContratacao.Text = "E-mail para aceite do contrato*";
            txtEmailContratoPopup.Content = TextoPadrao.ConsultarPorTipo(TextoPadrao.MODELOEMAILCONTRATO).AtualizarVariaveis(pedido).GetTextoComSubstituicaoDeTags();
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

    #region _______________Pre-Render_______________

    protected void gdvOrdensServico_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvOrdensServico.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirOS_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta ordem de serviço serão perdidos. Deseja realmente excluir esta OS ?");
    }

    protected void btnExcluirArquivoPedido_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((LinkButton)sender, "Deseja realmente excluir este arquivo ?");
    }

    protected void btnExcluirPedido_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((LinkButton)sender, "Todos os dados referentes a este pedido serão perdidos. Deseja realmente excluir este pedido ?");
    }

    protected void gdvAtividades_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvAtividades.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void gdvVisitas_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvVisitas.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirParcela_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao(sender as Button, "Deseja realmente excluir esta parcela?");
    }

    #endregion

    #region _______________ Triggers _______________

    protected void btnVerHistorioDetalhamentosPedidos_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnGridEditarOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAtividadesOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisitasOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExibicoesDaOS);
    }

    protected void btnNovaOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAtividadesOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upVisitasOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosOS);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upExibicoesDaOS);
    }

    protected void btnRenomearArquivoPedido_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void btnSalvarRenomearArquivo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioPedido);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upArquivosOS);
    }

    protected void btnVisualizarDetalhamentosOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnExcluirArquivoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioPedido);
    }

    protected void btnRenomearArquivoOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void btnSalvarOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioPedido);
    }

    protected void Label1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upFormularioPedido);
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upArquivosOS);
    }

    protected void UpLoad2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upFormularioPedido);
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upArquivosOS);
    }

    protected void btnCalcularPrazoLegal_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCalcularPrazos);
    }

    protected void btnCalcularPrazoDiretoria_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCalcularPrazos);
    }

    protected void btnCalcular_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormOS);
    }

    protected void ckbRenovavelOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "CheckedChanged", upReplicarOSPeriodica);
    }

    protected void btnAddDataReplicacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormOS);
    }

    protected void btnContratoPedido_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upContrato);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upOpcoesContrato);
    }

    protected void btnEnviarContrato_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upStatusContratoPedido);
    }

    protected void btnSalvarParcela_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upParcelasFinanceiroPedido);
    }

    protected void btnGridEditarParcela_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upEdicaoParcela);
    }

    protected void btnVincularOsMatriz_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upOsVinculada);
    }

    protected void btnEscolherOSMatriz_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upOsMatriz);
    }

    #endregion
}