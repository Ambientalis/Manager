using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeClientes : PageBase
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
        this.CarregarOrigens();
        this.CarregarEstados(ddlEstado);
        this.CarregarCidades(ddlEstado, ddlCidades);
    }

    private void CarregarOrigens()
    {
        ddlOrigem.Items.Clear();
        ddlOrigem.Items.Add(new ListItem("-- Todos --", "0"));
        IList<Origem> origens = Origem.ConsultarTodosOrdemAlfabetica();
        if (origens != null)
            foreach (Origem or in origens)
                ddlOrigem.Items.Add(new ListItem(or.Nome, or.Id.ToString()));
        ddlOrigem.SelectedIndex = 0;
    }

    private void CarregarEstados(DropDownList drop)
    {
        drop.DataTextField = "Nome";
        drop.DataValueField = "Id";

        IList<Estado> estados = Estado.ConsultarTodosOrdemAlfabetica();
        Estado estadoAux = new Estado();
        estadoAux.Nome = "-- Todos --";
        estados.Insert(0, estadoAux);

        drop.DataSource = estados;
        drop.DataBind();

        drop.SelectedIndex = 0;
    }

    private void CarregarCidades(DropDownList dropEstado, DropDownList dropCidade)
    {
        Estado estado = Estado.ConsultarPorId(dropEstado.SelectedValue.ToInt32());
        dropCidade.DataValueField = "Id";
        dropCidade.DataTextField = "Nome";
        dropCidade.DataSource = estado != null && estado.Cidades != null ? estado.Cidades : new List<Cidade>();
        dropCidade.DataBind();
        dropCidade.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarRelatorioClientes()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<Cliente> clientes = Cliente.FiltrarRelatorio(1, tbxCodigoPesquisa.Text, tbxNomeRazaoApelidoPesquisa.Text, tbxCpfCnpjPesquisa.Text, Convert.ToChar(ddlUnidadePesquisa.SelectedValue), ddlStatusPesquisa.SelectedValue, ddlEstado.SelectedValue.ToInt32(), ddlCidades.SelectedValue.ToInt32(), ddlOrigem.SelectedValue.ToInt32());

        string descricaoCodigo = tbxCodigoPesquisa.Text.IsNotNullOrEmpty() ? tbxCodigoPesquisa.Text : "Não definido";
        string descricaoNomeRazaoApelido = tbxNomeRazaoApelidoPesquisa.Text.IsNotNullOrEmpty() ? tbxNomeRazaoApelidoPesquisa.Text : "Não definido";
        string descricaoCpfCnpj = tbxCpfCnpjPesquisa.Text.IsNotNullOrEmpty() ? tbxCpfCnpjPesquisa.Text : "Não definido";
        string descricaoStatus = ddlStatusPesquisa.SelectedValue == "0" ? "Todos" : ddlStatusPesquisa.SelectedItem.Text;
        string descricaoUnidade = ddlUnidadePesquisa.SelectedItem.Text;
        string descricaoEstado = ddlEstado.SelectedIndex == 0 ? "Todos" : ddlEstado.SelectedItem.Text;
        string descricaoCidade = ddlCidades.SelectedIndex <= 0 ? "Todos" : ddlCidades.SelectedItem.Text;

        grvRelatorio.DataSource = clientes != null && clientes.Count > 0 ? clientes : new List<Cliente>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Código", descricaoCodigo);
        CtrlHeader.InsertFiltroEsquerda("Razão Social/ Nome / Apelido", descricaoNomeRazaoApelido);
        CtrlHeader.InsertFiltroEsquerda("CPF/CNPJ", descricaoCpfCnpj);

        CtrlHeader.InsertFiltroCentro("Unidade", descricaoUnidade);
        CtrlHeader.InsertFiltroCentro("Status", descricaoStatus);

        CtrlHeader.InsertFiltroDireita("Estado", descricaoEstado);
        CtrlHeader.InsertFiltroDireita("Cidade", descricaoCidade);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Clientes");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades(ddlEstado, ddlCidades);
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
            this.CarregarRelatorioClientes();
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