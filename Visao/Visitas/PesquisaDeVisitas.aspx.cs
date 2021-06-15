using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Visitas_PesquisaDeVisitas : PageBase
{
    Msg msg = new Msg();

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
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
                this.Pesquisar();
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

    #region _______________ Métodos _______________

    private void CarregarCampos()
    {
        this.CarregarTiposVisita();
        this.CarregarVisitantes();
        this.CarregarClientes();
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

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.GetUsuarioLogado.Id, this.GetFuncaoLogada.Id);

        if (permissao != null && permissao.AcessaOS == Permissao.RESPONSAVEL)
        {
            ddlResponsavel.Items.Insert(0, new ListItem(this.GetUsuarioLogado.NomeRazaoSocial, this.GetUsuarioLogado.Id.ToString()));
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

    private void Pesquisar()
    {
        DateTime dataDe = tbxDataDe.Text.IsDate() ? tbxDataDe.Text.ToSqlDateTime().ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text.IsDate() ? tbxDataAte.Text.ToSqlDateTime().ToMaxHourOfDay() : SqlDate.MaxValue;

        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.GetUsuarioLogado.Id, this.GetFuncaoLogada.Id);

        IList<Visita> visitas = Visita.Filtrar(tbxNumeroOs.Text,
            tbxNumeroPedido.Text,
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlResponsavel.SelectedValue.ToInt32(),
            ddlStatusVisita.SelectedValue,
            ddlTipoVisita.SelectedValue.ToInt32(),
            tbxDescricao.Text,
            permissao,
            this.GetUsuarioLogado.Id,
            ddlOrdenacao.SelectedIndex);

        if (ddlResponsavel.SelectedValue.ToInt32() <= 0)
            visitas.AddRange<Visita>(Visita.FiltrarVisitasDoUsuarioLogadoComMesmosFiltros(tbxNumeroOs.Text,
                tbxNumeroPedido.Text,
                dataDe, dataAte,
                ddlClientePesquisa.SelectedValue.ToInt32(),
                ddlStatusVisita.SelectedValue,
                ddlTipoVisita.SelectedValue.ToInt32(),
                tbxDescricao.Text,
                this.GetUsuarioLogado.Id,
                ddlOrdenacao.SelectedIndex));

        if (ddlOrdenacao.SelectedIndex == 0)
            visitas = visitas.Distinct().OrderBy(vis => vis.DataInicio).ToList();
        else
            visitas = visitas.Distinct().OrderByDescending(vis => vis.DataInicio).ToList();

        gdvVisitas.DataSource = visitas;
        gdvVisitas.DataBind();

        lblStatus.Text = visitas.Count + " Visita(s) encontrada(s)";
    }

    public string BindEditar(Object o)
    {
        Visita n = (Visita)o;
        return "CadastroDeVisita.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
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

    protected void gdvVisitas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvVisitas.PageIndex = e.NewPageIndex;
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

    protected void gdvVisitas_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvVisitas.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void ddlPaginacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gdvVisitas.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    #endregion

}