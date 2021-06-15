using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeVisitas : PageBase
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
        this.CarregarTiposVisita();
        this.CarregarVisitantes();
        this.CarregarClientes();
        this.CarregarDepartamentos();
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamento.Items.Clear();
        ddlDepartamento.DataValueField = "Id";
        ddlDepartamento.DataTextField = "Nome";
        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();
        ddlDepartamento.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamento.DataBind();
        ddlDepartamento.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarTiposVisita()
    {
        ddlTipoVisita.Items.Clear();

        ddlTipoVisita.DataValueField = "Id";
        ddlTipoVisita.DataTextField = "Nome";

        IList<TipoVisita> tiposVisitas = TipoVisita.ConsultarTodosOrdemAlfabetica();

        ddlTipoVisita.DataSource = tiposVisitas != null ? tiposVisitas : new List<TipoVisita>();
        ddlTipoVisita.DataBind();

        ddlTipoVisita.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarVisitantes()
    {
        ddlResponsavel.Items.Clear();

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

        if (permissao != null && permissao.AcessaOS == Permissao.RESPONSAVEL)
        {
            ddlResponsavel.Items.Insert(0, new ListItem(WebUtil.FuncionarioLogado.NomeRazaoSocial, WebUtil.FuncaoLogada.Id.ToString()));
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

    private void CarregarRelatorioVisitas()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

        IList<Visita> visitas = Visita.Filtrar(tbxNumeroOs.Text,
            tbxNumeroPedido.Text,
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlResponsavel.SelectedValue.ToInt32(),
            ddlStatusVisita.SelectedValue,
            ddlTipoVisita.SelectedValue.ToInt32(),
            tbxDescricao.Text,
            ddlDepartamento.SelectedValue.ToInt32(),
            permissao,
            WebUtil.FuncionarioLogado.Id,
            ddlOrdenacao.SelectedIndex);

        if (ddlResponsavel.SelectedValue.ToInt32() <= 0)
            visitas.AddRange<Visita>(Visita.FiltrarVisitasDoUsuarioLogadoComMesmosFiltros(tbxNumeroOs.Text,
                tbxNumeroPedido.Text,
                dataDe, dataAte,
                ddlClientePesquisa.SelectedValue.ToInt32(),
                ddlStatusVisita.SelectedValue,
                ddlTipoVisita.SelectedValue.ToInt32(),
                tbxDescricao.Text,
                WebUtil.FuncionarioLogado.Id,
                ddlOrdenacao.SelectedIndex));

        if (ddlOrdenacao.SelectedIndex == 0)
            visitas = visitas.Distinct().OrderBy(vis => vis.DataInicio).ToList();
        else
            visitas = visitas.Distinct().OrderByDescending(vis => vis.DataInicio).ToList();

        string descricaoNumeroOS = tbxNumeroOs.Text.IsNotNullOrEmpty() ? tbxNumeroOs.Text : "Não definido";
        string descricaoNumeroPedido = tbxNumeroPedido.Text.IsNotNullOrEmpty() ? tbxNumeroPedido.Text : "Não definido";
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoCliente = ddlClientePesquisa.SelectedValue == "0" ? "Todos" : ddlClientePesquisa.SelectedItem.Text;
        string descricaoResponsavel = ddlResponsavel.SelectedValue == "0" ? "Todos" : ddlResponsavel.SelectedItem.Text;
        string descricaoStatus = ddlStatusVisita.SelectedItem.Text;
        string descricaoTipoVisita = ddlTipoVisita.SelectedValue == "0" ? "Todos" : ddlTipoVisita.SelectedItem.Text;
        string descricaoDescricao = tbxDescricao.Text.IsNotNullOrEmpty() ? tbxDescricao.Text : "Não definido";
        string descricaoDepartamento = ddlDepartamento.SelectedIndex <= 0 ? "Todos" : ddlDepartamento.SelectedItem.Text;

        grvRelatorio.DataSource = visitas != null && visitas.Count > 0 ? visitas : new List<Visita>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Número da OS", descricaoNumeroOS);
        CtrlHeader.InsertFiltroEsquerda("Número do Pedido", descricaoNumeroPedido);
        CtrlHeader.InsertFiltroEsquerda("Período", descricaoPeriodo);

        CtrlHeader.InsertFiltroCentro("Cliente", descricaoCliente);
        CtrlHeader.InsertFiltroCentro("Responsável", descricaoResponsavel);
        CtrlHeader.InsertFiltroCentro("Descrição", descricaoDescricao);

        CtrlHeader.InsertFiltroDireita("Tipo de Visita", descricaoTipoVisita);
        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);
        CtrlHeader.InsertFiltroDireita("Departamento", descricaoDepartamento);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Visitas");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioVisitas();
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