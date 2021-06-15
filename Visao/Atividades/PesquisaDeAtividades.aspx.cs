using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Atividades_PesquisaDeAtividades : PageBase
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

        IList<Atividade> atividades = Atividade.Filtrar(tbxNumeroOs.Text,
            tbxNumeroPedido.Text,
            dataDe, dataAte,
            ddlClientePesquisa.SelectedValue.ToInt32(),
            ddlResponsavel.SelectedValue.ToInt32(),
            ddlStatusVisita.SelectedValue,
            ddlTipoAtividade.SelectedValue.ToInt32(),
            tbxDescricao.Text,
            permissao,
            this.GetUsuarioLogado.Id);

        if (ddlResponsavel.SelectedValue.ToInt32() <= 0)
            atividades.AddRange<Atividade>(Atividade.FiltrarAtividadesDoUsuarioLogadoComMesmosFrutos(tbxNumeroOs.Text,
                tbxNumeroPedido.Text,
                dataDe, dataAte,
                ddlClientePesquisa.SelectedValue.ToInt32(),
                ddlStatusVisita.SelectedValue,
                ddlTipoAtividade.SelectedValue.ToInt32(),
                tbxDescricao.Text,
                this.GetUsuarioLogado.Id));

        if (ddlOrdenacao.SelectedIndex == 0)
            atividades = atividades.Distinct().OrderBy(ati => ati.Data).ToList();
        else
            atividades = atividades.Distinct().OrderByDescending(ati => ati.Data).ToList();

        gdvAtividades.DataSource = atividades;
        gdvAtividades.DataBind();

        lblStatus.Text = atividades.Count + " Atividade(s) encontrada(s)";
    }

    public string BindEditar(Object o)
    {
        Atividade n = (Atividade)o;
        return "CadastroDeAtividade.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
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

    protected void gdvAtividades_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvAtividades.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void ddlPaginacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gdvAtividades.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    protected void gdvAtividades_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvAtividades.PageIndex = e.NewPageIndex;
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