using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeOrdensDeServico : PageBase
{
    private Msg msg = new Msg();
    private decimal totalOss = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunas(grvRelatorio, ckbColunas, this.Page);
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Alert.Show(msg.Mensagem);
            }
    }

    #region ______________Métodos______________

    private void CarregarCampos()
    {
        this.CarregarDepartamentos();
        this.CarregarClientes();
        this.CarregarResponsaveis();
        this.CarregarTipos();
        this.TrocarLabelDatas();
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

    private void CarregarClientes()
    {
        ddlClientePesquisa.Items.Clear();

        ddlClientePesquisa.DataValueField = "Id";
        ddlClientePesquisa.DataTextField = "NomeRazaoSocial";

        IList<Cliente> clientes = Cliente.ConsultarTodosOrdemAlfabetica();

        ddlClientePesquisa.DataSource = clientes != null ? clientes : new List<Cliente>();
        ddlClientePesquisa.DataBind();

        ddlClientePesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));

    }

    private void CarregarResponsaveis()
    {
        ddlResponsavelPesquisa.Items.Clear();

        ddlResponsavelPesquisa.DataValueField = "Id";
        ddlResponsavelPesquisa.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> vendedores = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlResponsavelPesquisa.DataSource = vendedores != null ? vendedores : new List<Funcionario>();
        ddlResponsavelPesquisa.DataBind();

        ddlResponsavelPesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void TrocarLabelDatas()
    {
        lblDatas.Text = ddlOrdenarPor.SelectedIndex == 0 ? "Data de Criação de" :
            ddlOrdenarPor.SelectedIndex == 1 ? "Data de Vencimento de" :
            ddlOrdenarPor.SelectedIndex == 2 ? "Data de Encerramento de" :
            "Data de";
    }

    private void CarregarRelatorioOrdensServico()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

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
            WebUtil.FuncionarioLogado.Id,
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
                WebUtil.FuncionarioLogado.Id,
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

        string descricaoCodigo = tbxCodigoPesquisa.Text.IsNotNullOrEmpty() ? tbxCodigoPesquisa.Text : "Não definido";
        string descricaoNumeroPedido = tbxNumeroPedidoPesquisa.Text.IsNotNullOrEmpty() ? tbxNumeroPedidoPesquisa.Text : "Não definido";
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoStatus = ddlEstadoOSPesquisa.SelectedItem.Text;
        string descricaoDescricaoOS = tbxDescricaoOSPesquisa.Text.IsNotNullOrEmpty() ? tbxDescricaoOSPesquisa.Text : "Não definida";
        string descricaoTrazerOS = ddlStatusPesquisa.SelectedItem.Text;
        string descricaoCliente = ddlClientePesquisa.SelectedValue == "0" ? "Todos" : ddlClientePesquisa.SelectedItem.Text;
        string descricaoResponsavel = ddlResponsavelPesquisa.SelectedValue == "0" ? "Todos" : ddlResponsavelPesquisa.SelectedItem.Text;
        string descricaoTipo = ddlTipoOS.SelectedValue == "0" ? "Todos" : ddlTipoOS.SelectedItem.Text;
        string descricaoOrdenacao = ddlOrdenarPor.SelectedItem.Text;
        string descricaoFaturadas = ddlFaturadas.SelectedIndex == 0 ? "Todas" : ddlFaturadas.SelectedItem.Text;

        grvRelatorio.DataSource = ordens != null && ordens.Count > 0 ? ordens : new List<OrdemServico>();
        grvRelatorio.DataBind();

        this.totalOss = ordens != null ? ordens.Sum(od => od.ValorTotal) : 0;

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Código", descricaoCodigo);
        CtrlHeader.InsertFiltroEsquerda("Número do Pedido", descricaoNumeroPedido);
        CtrlHeader.InsertFiltroEsquerda("Descrição", descricaoDescricaoOS);
        CtrlHeader.InsertFiltroEsquerda("Data", descricaoPeriodo);

        CtrlHeader.InsertFiltroCentro("Status", descricaoStatus);
        CtrlHeader.InsertFiltroCentro("Trazer OS's", descricaoTrazerOS);
        CtrlHeader.InsertFiltroCentro("Tipo de OS", descricaoTipo);
        CtrlHeader.InsertFiltroCentro("Ordenar por", descricaoOrdenacao);

        CtrlHeader.InsertFiltroDireita("Cliente", descricaoCliente);
        CtrlHeader.InsertFiltroDireita("Responsável", descricaoResponsavel);
        CtrlHeader.InsertFiltroDireita("Faturadas", descricaoFaturadas);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Ordens de Serviço");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    private IList<OrdemServico> IncluirOSVinculadasNaLista(IList<OrdemServico> ordens)
    {
        if (ordens != null && ordens.Count > 0)
        {
            IList<OrdemServico> ordensComOsVinculadas = ordens.Where(x => x.OssVinculadas != null && x.OssVinculadas.Count > 0 && x.Responsavel.Id == WebUtil.FuncionarioLogado.Id).ToList();
            if (ordensComOsVinculadas != null && ordensComOsVinculadas.Count > 0)
            {
                foreach (OrdemServico ordemVinculada in ordensComOsVinculadas)
                    ordens.AddRange<OrdemServico>(ordemVinculada.OssVinculadas);
                ordens = ordens.Distinct().ToList();
                return ordens;
            }
            else
                return ordens;
        }
        return ordens;
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlUnidadePesquisa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarClientes();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Alert.Show(msg.Mensagem);
        }
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioOrdensServico();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Alert.Show(msg.Mensagem);
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
            Alert.Show(msg.Mensagem);
        }
    }

    #endregion

    public String BindingPedidosAdiamentoAceitos(object os)
    {
        OrdemServico aux = (OrdemServico)os;
        StringBuilder str = new StringBuilder();
        foreach (SolicitacaoAdiamento soli in aux.GetSolicitacoesAdiamentoAceitas)
            str.Append(soli.GetDataAntiga.ToShortDateString()).Append(" - ").Append(soli.GetNovaData.ToShortDateString()).Append("<br />");
        return str.ToString();
    }

    public string BindingTotalOSs()
    {
        return String.Format("{0:c}", this.totalOss);
    }
}