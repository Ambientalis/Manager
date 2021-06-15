using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDePedidos : PageBase
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
        this.CarregarTipos();
        this.CarregarClientes();
        this.CarregarVendedores();
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

    private void CarregarTipos()
    {
        ddlTipoPedido.Items.Clear();

        ddlTipoPedido.DataValueField = "Id";
        ddlTipoPedido.DataTextField = "Nome";

        IList<TipoPedido> tipos = TipoPedido.ConsultarTodosOrdemAlfabetica();

        ddlTipoPedido.DataSource = tipos != null ? tipos : new List<TipoPedido>();
        ddlTipoPedido.DataBind();

        ddlTipoPedido.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarClientes()
    {
        ddlClientePesquisa.Items.Clear();

        ddlClientePesquisa.DataValueField = "Id";
        ddlClientePesquisa.DataTextField = "NomeRazaoSocial";

        IList<Cliente> clientes;

        if (ddlUnidadePesquisa.SelectedValue == "I")
            clientes = Cliente.ConsultarTodosOrdemAlfabetica();
        else
            clientes = Cliente.ConsultarClientesPelaUnidadePedidos(Convert.ToChar(ddlUnidadePesquisa.SelectedValue));

        ddlClientePesquisa.DataSource = clientes != null ? clientes : new List<Cliente>();
        ddlClientePesquisa.DataBind();

        ddlClientePesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));

    }

    private void CarregarVendedores()
    {
        ddlVendedorPesquisa.Items.Clear();

        ddlVendedorPesquisa.DataValueField = "Id";
        ddlVendedorPesquisa.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> vendedores = Funcionario.ConsultarVendedoresOrdemAlfabetica();

        ddlVendedorPesquisa.DataSource = vendedores != null ? vendedores : new List<Funcionario>();
        ddlVendedorPesquisa.DataBind();

        ddlVendedorPesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRelatorioPedidos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Pedido> pedidos = Pedido.Filtrar(tbxCodigo.Text,
            ddlTipoPedido.SelectedValue.ToInt32(),
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlStatus.SelectedValue.ToInt32(),
            0,
            ddlVendedorPesquisa.SelectedValue.ToInt32(),
            ddlDepartamento.SelectedValue.ToInt32());

        string descricaoCodigo = tbxCodigo.Text.IsNotNullOrEmpty() ? tbxCodigo.Text : "Não definido";
        string descricaoTipoPedido = ddlTipoPedido.SelectedValue == "0" ? "Todos" : ddlTipoPedido.SelectedItem.Text;
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoStatus = ddlStatus.SelectedValue == "0" ? "Todos" : ddlStatus.SelectedItem.Text;
        string descricaoUnidade = ddlUnidadePesquisa.SelectedItem.Text;
        string descricaoCliente = ddlClientePesquisa.SelectedValue == "0" ? "Todos" : ddlClientePesquisa.SelectedItem.Text;
        string descricaoVendedor = ddlVendedorPesquisa.SelectedValue == "0" ? "Todos" : ddlVendedorPesquisa.SelectedItem.Text;
        string descricaoDepartamento = ddlDepartamento.SelectedIndex <= 0 ? "Todos" : ddlDepartamento.SelectedItem.Text;

        grvRelatorio.DataSource = pedidos != null && pedidos.Count > 0 ? pedidos : new List<Pedido>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Código", descricaoCodigo);
        CtrlHeader.InsertFiltroEsquerda("Tipo de Pedido", descricaoTipoPedido);
        CtrlHeader.InsertFiltroEsquerda("Criação", descricaoPeriodo);

        CtrlHeader.InsertFiltroCentro("Unidade", descricaoUnidade);
        CtrlHeader.InsertFiltroCentro("Cliente", descricaoCliente);
        CtrlHeader.InsertFiltroCentro("Departamento", descricaoCliente);

        CtrlHeader.InsertFiltroDireita("Vendedor", descricaoVendedor);
        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Pedidos");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
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
            this.CarregarRelatorioPedidos();
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