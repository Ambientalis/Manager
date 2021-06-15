using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;
using System.Drawing;
using Persistencia.Fabrica;

public partial class Pedidos_Pedidos : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
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
        this.CarregarClientes();
        this.CarregarVendedores();
    }

    private void CarregarTipos()
    {
        ddlTipoPedidoPesquisa.Items.Clear();

        ddlTipoPedidoPesquisa.DataValueField = "Id";
        ddlTipoPedidoPesquisa.DataTextField = "Nome";

        IList<TipoPedido> tipos = TipoPedido.ConsultarTodosOrdemAlfabetica();

        ddlTipoPedidoPesquisa.DataSource = tipos != null ? tipos : new List<TipoPedido>();
        ddlTipoPedidoPesquisa.DataBind();

        ddlTipoPedidoPesquisa.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarClientes()
    {
        ddlClientePesquisa.Items.Clear();
        ddlClientePesquisa.DataValueField = "Id";
        ddlClientePesquisa.DataTextField = "NomeRazaoSocial";
        ddlClientePesquisa.Items.Add(new ListItem("-- Todos --", "0"));

        IList<Cliente> clientes = Cliente.ConsultarTodosOrdemAlfabetica();
        foreach (Cliente cli in clientes)
            ddlClientePesquisa.Items.Add(new ListItem(cli.NomeRazaoSocial + " - " + cli.CpfCnpj, cli.Id.ToString()));
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

    private void Pesquisar()
    {
        DateTime dataDe = tbxDataDe.Text.IsDate() ? tbxDataDe.Text.ToSqlDateTime().ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text.IsDate() ? tbxDataAte.Text.ToSqlDateTime().ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Pedido> pedidos = Pedido.Filtrar(
            tbxCodigoPesquisa.Text,
            ddlTipoPedidoPesquisa.SelectedValue.ToInt32(),
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlStatusPesquisa.SelectedValue.ToInt32(),
            ddlContratoFixo.SelectedValue.ToInt32(),
            ddlVendedorPesquisa.SelectedValue.ToInt32());

        gdvPedidos.DataSource = pedidos != null ? pedidos : new List<Pedido>();
        gdvPedidos.DataBind();

        lblStatus.Text = pedidos.Count + " pedido(s) encontrado(s)";
    }

    private void ExcluirPedido(GridViewDeleteEventArgs e)
    {
        Pedido pedido = Pedido.ConsultarPorId(gdvPedidos.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
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

            new FabricaDAONHibernateBase().GetDAOBase().ExecutarComandoSql("delete from emails where id_pedido = " + pedido.Id);
            if (pedido.Excluir())
                msg.CriarMensagem("Pedido excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.Pesquisar();
    }

    public string BindEditar(Object o)
    {
        Pedido n = (Pedido)o;
        return "CadastroPedidos.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
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
            gdvPedidos.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    protected void gdvPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvPedidos.PageIndex = e.NewPageIndex;
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

    protected void gdvPedidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirPedido(e);
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

    protected void gdvPedidos_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvPedidos.BottomPagerRow;
        if (pager != null)
            pager.Visible = true;
    }

    protected void btnGridExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este pedido serão perdidos. Deseja realmente excluir este pedido ?");
    }

    protected void gdvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1 && e.Row.DataItem != null)
            e.Row.Cells[0].ForeColor = ((Pedido)e.Row.DataItem).IsPossuiDivergenciaDeValores ? Color.FromArgb(230, 00, 00) : Color.Black;
    }

    #endregion

    #region _______________ Trigers _______________
    #endregion


}