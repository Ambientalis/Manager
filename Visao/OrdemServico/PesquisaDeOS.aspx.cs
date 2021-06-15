using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;
using System.Text;

public partial class OrdemServico_PesquisaDeOS : PageBase
{
    private Msg msg = new Msg();

    private Funcionario GetUsuarioLogado
    {
        get
        {
            return Session["usuario_logado"] == null ? null : (Funcionario)Session["usuario_logado"];
        }
    }

    private Funcao GetFuncaoLogada
    {
        get
        {
            return Session["funcao_logada"] == null ? null : (Funcao)Session["funcao_logada"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                //Se veio para carregar a OS
                if (Request["idOS"] != null)
                {
                    int id = Modelo.Util.CriptografiaSimples.Decrypt(Request["idOS"].ToString().Replace(" ", "+")).ToInt32();
                    OrdemServico os = new OrdemServico(id).ConsultarPorId();
                    IList<OrdemServico> ordens = new List<OrdemServico>();
                    if (os != null)
                        ordens.Add(os);

                    gdvOrdensServico.DataSource = ordens;
                    gdvOrdensServico.DataBind();

                    lblStatus.Text = ordens.Count + " OS(s) encontrada(s)";
                }
                else
                    this.Pesquisar();
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
        this.CarregarTipos();
        this.CarregarDepartamentos();
        this.CarregarClientes();
        this.CarregarResponsaveis();
        this.TrocarLabelDatas();
    }

    private void CarregarClientes()
    {
        ddlClientePesquisa.Items.Clear();

        ddlClientePesquisa.DataValueField = "Id";
        ddlClientePesquisa.DataTextField = "NomeRazaoSocial";

        IList<Cliente> clientes = Cliente.ConsultarTodosOrdemAlfabetica(); ;

        ddlClientePesquisa.DataSource = clientes != null ? clientes : new List<Cliente>();
        ddlClientePesquisa.DataBind();

        ddlClientePesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarTipos()
    {
        ddlTipoOS.Items.Clear();

        ddlTipoOS.DataValueField = "Id";
        ddlTipoOS.DataTextField = "Nome";

        IList<TipoOrdemServico> tipos = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

        ddlTipoOS.DataSource = tipos != null ? tipos : new List<TipoOrdemServico>();
        ddlTipoOS.DataBind();

        ddlTipoOS.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamento.Items.Clear();

        ddlDepartamento.DataValueField = "Id";
        ddlDepartamento.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamento.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamento.DataBind();

        ddlDepartamento.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarResponsaveis()
    {
        ddlResponsavelPesquisa.Items.Clear();

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.GetUsuarioLogado.Id, this.GetFuncaoLogada.Id);

        if (permissao != null && permissao.AcessaOS == Permissao.RESPONSAVEL)
        {
            ddlResponsavelPesquisa.Items.Insert(0, new ListItem(this.GetUsuarioLogado.NomeRazaoSocial, this.GetUsuarioLogado.Id.ToString()));
        }
        else
        {
            ddlResponsavelPesquisa.DataValueField = "Id";
            ddlResponsavelPesquisa.DataTextField = "NomeRazaoSocial";

            IList<Funcionario> responsaveis = Funcionario.ConsultarTodosOrdemAlfabetica();

            ddlResponsavelPesquisa.DataSource = responsaveis != null ? responsaveis : new List<Funcionario>();
            ddlResponsavelPesquisa.DataBind();

            ddlResponsavelPesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));
        }
    }

    private void CarregarTiposOS()
    {
        ddlTipoOS.Items.Clear();

        ddlTipoOS.DataValueField = "Id";
        ddlTipoOS.DataTextField = "Nome";

        IList<TipoOrdemServico> tiposOrdensServico = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

        ddlTipoOS.DataSource = tiposOrdensServico != null ? tiposOrdensServico : new List<TipoOrdemServico>();
        ddlTipoOS.DataBind();

        ddlTipoOS.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void Pesquisar()
    {
        DateTime dataDe = tbxVencimentoDe.Text.IsDate() ? tbxVencimentoDe.Text.ToSqlDateTime().ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxVencimentoAte.Text.IsDate() ? tbxVencimentoAte.Text.ToSqlDateTime().ToMaxHourOfDay() : SqlDate.MaxValue;

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.GetUsuarioLogado.Id, this.GetFuncaoLogada.Id);

        IList<OrdemServico> ordens = OrdemServico.Filtrar(
            tbxCodigoPesquisa.Text,
            tbxNumeroPedidoPesquisa.Text,
            ddlOrdenarPor.SelectedIndex,
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlResponsavelPesquisa.SelectedValue.ToInt32(),
            ddlStatusPesquisa.SelectedValue,
            tbxDescricaoOSPesquisa.Text,
            permissao,
            this.GetUsuarioLogado.Id,
            ddlEstadoOSPesquisa.SelectedValue,
            ddlTipoOS.SelectedValue.ToInt32(),
            ddlOrdenacao.SelectedIndex,
            ddlDepartamento.SelectedValue.ToInt32(),
            ddlFaturadas.SelectedValue.ToInt32());

        if (ddlResponsavelPesquisa.SelectedValue.ToInt32() <= 0)
            ordens.AddRange<OrdemServico>(OrdemServico.FiltrarOrdensDoUsuarioLogadoComMesmosFiltros(
                tbxCodigoPesquisa.Text,
                tbxNumeroPedidoPesquisa.Text,
                ddlOrdenarPor.SelectedIndex,
                dataDe, dataAte,
                ddlClientePesquisa.SelectedValue.ToInt32(),
                ddlStatusPesquisa.SelectedValue,
                tbxDescricaoOSPesquisa.Text,
                this.GetUsuarioLogado.Id,
                ddlEstadoOSPesquisa.SelectedValue,
                ddlTipoOS.SelectedValue.ToInt32(),
                ddlOrdenacao.SelectedIndex,
                ddlDepartamento.SelectedValue.ToInt32(),
                ddlFaturadas.SelectedValue.ToInt32()));

        ordens = this.IncluirOSVinculadasNaLista(ordens);

        if (ddlOrdenarPor.SelectedIndex == 0)
            ordens = ddlOrdenacao.SelectedIndex == 0 ? ordens.Distinct().OrderBy(ord => ord.Data).ToList() : ordens.Distinct().OrderByDescending(ord => ord.Data).ToList();
        else if (ddlOrdenarPor.SelectedIndex == 1)
            ordens = ddlOrdenacao.SelectedIndex == 0 ? ordens.Distinct().OrderBy(ord => ord.GetDataVencimento).ToList() : ordens.Distinct().OrderByDescending(ord => ord.GetDataVencimento).ToList();
        else if (ddlOrdenarPor.SelectedIndex == 2)
            ordens = ddlOrdenacao.SelectedIndex == 0 ? ordens.Distinct().OrderBy(ord => ord.GetDataEncerramento).ToList() : ordens.Distinct().OrderByDescending(ord => ord.GetDataEncerramento).ToList();

        gdvOrdensServico.DataSource = ordens;
        gdvOrdensServico.DataBind();

        lblStatus.Text = ordens.Count + " OS(s) encontrada(s)";
    }

    private IList<OrdemServico> IncluirOSVinculadasNaLista(IList<OrdemServico> ordens)
    {
        if (ordens != null && ordens.Count > 0)
        {
            IList<OrdemServico> ordensComOsVinculadas = ordens.Where(x => x.OssVinculadas != null && x.OssVinculadas.Count > 0 && x.Responsavel.Id == this.GetUsuarioLogado.Id).ToList();

            if (ordensComOsVinculadas != null && ordensComOsVinculadas.Count > 0)
            {

                foreach (OrdemServico ordemVinculada in ordensComOsVinculadas)
                {
                    ordens.AddRange<OrdemServico>(ordemVinculada.OssVinculadas);
                }

                ordens = ordens.Distinct().ToList();

                return ordens;
            }
            else
                return ordens;
        }

        return ordens;
    }

    private void TrocarLabelDatas()
    {
        lblDatas.Text = ddlOrdenarPor.SelectedIndex == 0 ? "Data de Criação de" :
            ddlOrdenarPor.SelectedIndex == 1 ? "Data de Vencimento de" :
            ddlOrdenarPor.SelectedIndex == 2 ? "Data de Encerramento de" :
            "Data de";
    }

    public string BindEditar(Object o)
    {
        OrdemServico n = (OrdemServico)o;
        return "CadastroDeOS.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
    }

    public string BindingTooltipReenviarPesquisa(object o)
    {
        Cliente aux = ((OrdemServico)o).GetCliente;
        return "Clique aqui para Enviar/Reenviar a pesquisa de satisfação para o cliente."
            + (aux != null ? " (" + String.Join("; ", aux.GetEmailsNotificacoes.ToArray()) + ")" : "");
    }

    #endregion

    #region _______________ Eventos _______________

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar();
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

    protected void ddlPaginacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gdvOrdensServico.PageSize = ddlPaginacao.SelectedValue.ToInt32();
            this.Pesquisar();
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

    protected void gdvOrdensServico_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvOrdensServico.PageIndex = e.NewPageIndex;
            this.Pesquisar();
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

    protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.TrocarLabelDatas();
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

    protected void btnVisualizarPesquisaSatisfacao_Click(object sender, EventArgs e)
    {
        try
        {
            OrdemServico os = new OrdemServico(((LinkButton)sender).CommandArgument).ConsultarPorId();
            if (os.PesquisaSatisfacao == null)
            {
                msg.CriarMensagem("A pesquisa de satisfação dessa OS não é válida!", "Erro", MsgIcons.Informacao);
                return;
            }
            PesquisaSatisfacao pesquisa = os.PesquisaSatisfacao;
            lblDataCriacaoPesquisa.Text = pesquisa.DataCriacao.ToShortDateString();
            lblDataRespostaPesquisa.Text = pesquisa.DataResposta.ToShortDateString();
            lblClientePesquisa.Text = pesquisa.OrdemServico.GetNomeCliente;
            lblNumeroOSPesquisa.Text = pesquisa.OrdemServico.Codigo;
            lblSugestoesPesquisa.Text = pesquisa.Sugestoes;

            grvPerguntasPesquisa.DataSource = pesquisa.Respostas;
            grvPerguntasPesquisa.DataBind();
            this.PopupPesquisaSatisfacao_ModalPopupExtender.Show();
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

    protected void btnReenviarPesquisaDeSatisfacao_Click(object sender, EventArgs e)
    {
        try
        {
            OrdemServico os = OrdemServico.ConsultarPorId(((LinkButton)sender).CommandArgument.ToInt32());
            if (os == null)
            {
                msg.CriarMensagem("A ordem de serviço não foi corretamente carregada. Pesquise e tente novamente!", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!PerguntaPesquisaSatisfacao.PossuiAlgumaPerguntaAtivaCadastrada())
            {
                msg.CriarMensagem("Nenhuma pergunta cadastrada para enviar a pesquisa de satisfação", "Informação", MsgIcons.Informacao);
                return;
            }

            PesquisaSatisfacao novaPesquisa = new PesquisaSatisfacao();
            if (os.PesquisaSatisfacao != null)
            {
                novaPesquisa = os.PesquisaSatisfacao;
                novaPesquisa.DataCriacao = DateTime.Now;
                novaPesquisa.DataResposta = SqlDate.MinValue;
            }
            novaPesquisa.OrdemServico = os;
            os.PesquisaSatisfacao = novaPesquisa.Salvar();
            os = os.Salvar();

            TextoPadrao textoEmail = TextoPadrao.ConsultarPorTipo(TextoPadrao.MODELOPESQUISASATISFACAO);
            String mensagemMail = textoEmail.AtualizarVariaveis(os).GetTextoComSubstituicaoDeTags();

            List<string> emailsNaoEnviados = new List<string>();
            List<string> emailsCliente = os.GetCliente.GetEmailsNotificacoes;
            foreach (string aux in emailsCliente)
                if (aux.IsNotNullOrEmpty() && Validadores.ValidaEmail(aux))
                {
                    Email email = new Email();
                    email.AdicionarDestinatario(aux);
                    email.Assunto = "Pesquisa de satisfação Ambientalis";
                    email.BodyHtml = true;
                    email.Mensagem = mensagemMail;
                    if (!email.EnviarAutenticado("Pesquisa de satisfação", this.FuncionarioLogado, os.Pedido, 587, false))
                        emailsNaoEnviados.Add(aux);
                }

            if (emailsNaoEnviados.Count < 1)
                msg.CriarMensagem("Pesquisa de satisfação enviada com sucesso para o e-mail " + os.GetEmailCliente, "Sucesso", MsgIcons.Sucesso);
            else
                msg.CriarMensagem("A Pesquisa de satisfação não pôde ser enviada para os e-mails (" + String.Join("; ", emailsNaoEnviados.ToArray()) + "). Tente novamente!", "Informação", MsgIcons.Alerta);
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

    #region ______________ PreRenders _____________

    protected void gdvOrdensServico_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvOrdensServico.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnVisualizarPesquisaSatisfacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upPopupPesquisaSatisfacao);
    }

    protected void btnReenviarPesquisaDeSatisfacao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upDatas);
    }

    protected void btnReenviarPesquisaDeSatisfacao_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao(sender as LinkButton, "Deseja realmente Enviar/Reenviar a pesquisa de satisfação para o cliente?");
    }

    #endregion
}