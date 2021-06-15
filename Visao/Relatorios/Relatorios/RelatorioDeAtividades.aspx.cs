using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeAtividades : PageBase
{
    private Msg msg = new Msg();

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
        this.CarregarVisitantes();
        this.CarregarTiposAtividade();
        this.CarregarClientes();
    }

    private void CarregarTiposAtividade()
    {
        ddlTipoAtividade.Items.Clear();

        ddlTipoAtividade.DataValueField = "Id";
        ddlTipoAtividade.DataTextField = "Nome";

        IList<TipoAtividade> tiposAtividades = TipoAtividade.ConsultarTodosOrdemAlfabetica();

        ddlTipoAtividade.DataSource = tiposAtividades != null ? tiposAtividades : new List<TipoAtividade>();
        ddlTipoAtividade.DataBind();

        ddlTipoAtividade.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarVisitantes()
    {
        ddlResponsavel.Items.Clear();

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

        if (permissao != null && permissao.AcessaOS == Permissao.RESPONSAVEL)
        {
            ddlResponsavel.Items.Insert(0, new ListItem(WebUtil.FuncionarioLogado.NomeRazaoSocial, WebUtil.FuncionarioLogado.Id.ToString()));
        }
        else
        {
            ddlResponsavel.DataValueField = "Id";
            ddlResponsavel.DataTextField = "NomeRazaoSocial";

            IList<Funcionario> responsaveis = Funcionario.ConsultarTodosOrdemAlfabetica();

            ddlResponsavel.DataSource = responsaveis != null ? responsaveis : new List<Funcionario>();
            ddlResponsavel.DataBind();

            ddlResponsavel.Items.Insert(0, new ListItem("-- Todos --", "0"));
        }
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

    private void CarregarRelatorioAtividades()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

        IList<Atividade> atividades = Atividade.Filtrar(tbxNumeroOs.Text,
            tbxNumeroPedido.Text,
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlResponsavel.SelectedValue.ToInt32(),
            ddlStatusVisita.SelectedValue,
            ddlTipoAtividade.SelectedValue.ToInt32(),
            tbxDescricao.Text,
            permissao,
            WebUtil.FuncionarioLogado.Id);

        if (ddlResponsavel.SelectedValue.ToInt32() <= 0)
            atividades.AddRange<Atividade>(Atividade.FiltrarAtividadesDoUsuarioLogadoComMesmosFrutos(tbxNumeroOs.Text,
                tbxNumeroPedido.Text,
                dataDe, dataAte,
                ddlClientePesquisa.SelectedValue.ToInt32(),
                ddlStatusVisita.SelectedValue,
                ddlTipoAtividade.SelectedValue.ToInt32(),
                tbxDescricao.Text,
                WebUtil.FuncionarioLogado.Id));

        if (ddlOrdenacao.SelectedIndex == 0)
            atividades = atividades.Distinct().OrderBy(ati => ati.Data).ToList();
        else
            atividades = atividades.Distinct().OrderByDescending(ati => ati.Data).ToList();

        string descricaoNumeroOS = tbxNumeroOs.Text.IsNotNullOrEmpty() ? tbxNumeroOs.Text : "Não definido";
        string descricaoNumeroPedido = tbxNumeroPedido.Text.IsNotNullOrEmpty() ? tbxNumeroPedido.Text : "Não definido";
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoCliente = ddlClientePesquisa.SelectedValue == "0" ? "Todos" : ddlClientePesquisa.SelectedItem.Text;
        string descricaoResponsavel = ddlResponsavel.SelectedValue == "0" ? "Todos" : ddlResponsavel.SelectedItem.Text;
        string descricaoStatus = ddlStatusVisita.SelectedItem.Text;
        string descricaoTipoAtividade = ddlTipoAtividade.SelectedValue == "0" ? "Todos" : ddlTipoAtividade.SelectedItem.Text;
        string descricaoDescricao = tbxDescricao.Text.IsNotNullOrEmpty() ? tbxDescricao.Text : "Não definido";

        grvRelatorio.DataSource = atividades != null && atividades.Count > 0 ? atividades : new List<Atividade>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Número da OS", descricaoNumeroOS);
        CtrlHeader.InsertFiltroEsquerda("Número do Pedido", descricaoNumeroPedido);
        CtrlHeader.InsertFiltroEsquerda("Período", descricaoPeriodo);

        CtrlHeader.InsertFiltroCentro("Cliente", descricaoCliente);
        CtrlHeader.InsertFiltroCentro("Executor", descricaoResponsavel);
        CtrlHeader.InsertFiltroCentro("Descrição", descricaoDescricao);

        CtrlHeader.InsertFiltroDireita("Tipo de Atividade", descricaoTipoAtividade);
        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Atividades");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioAtividades();
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
}