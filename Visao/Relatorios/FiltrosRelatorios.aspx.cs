using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Relatorios_FiltrosRelatorios : PageBase
{
    private Funcionario UsuarioLogado
    {
        get
        {
            return Session["usuario_logado"] == null ? null : (Funcionario)Session["usuario_logado"];
        }
        set { Session["usuario_logado"] = value; }
    }
    protected Funcao FuncaoLogada
    {
        get
        {
            return Session["funcao_logada"] == null ? null : (Funcao)Session["funcao_logada"];
        }
        set { Session["funcao_logada"] = value; }
    }
    private string Emp
    {
        get { return (Session["idEmp"] != null ? Session["idEmp"].ToString() : null); }
        set { Session["idEmp"] = value; }
    }
    private Cliente GetUsuarioClienteLogado
    {
        get
        {
            return Session["usuario_cliente_logado"] == null ? null : (Cliente)Session["usuario_cliente_logado"];
        }
    }
    private Configuracoes ConfiguracoesSistema
    {
        get
        {
            if (Session["configuracoes_sistema_manager"] == null)
                Session["configuracoes_sistema_manager"] = Configuracoes.GetConfiguracoesSistema();
            return (Configuracoes)Session["configuracoes_sistema_manager"];
        }
        set
        {
            Session["configuracoes_sistema_manager"] = value;
        }
    }
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
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

    private void CarregarCampos()
    {
        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

        IList<Tela> relatoriosGerais = permissao.Telas.Where(tela => tela.Relatorio && !tela.TelaFinanceiro && !tela.RelatorioGrafico).ToList();
        IList<Tela> graficosGerais = permissao.Telas.Where(tela => tela.Relatorio && !tela.TelaFinanceiro && tela.RelatorioGrafico).ToList();

        IList<Tela> relatoriosFinanceiros = permissao.Telas.Where(tela => tela.Relatorio && tela.TelaFinanceiro && !tela.RelatorioGrafico).ToList();
        IList<Tela> graficosFinanceiros = permissao.Telas.Where(tela => tela.Relatorio && tela.TelaFinanceiro && tela.RelatorioGrafico).ToList();

        pnlRelatorios.Visible = relatoriosGerais != null && relatoriosGerais.Count > 0;
        pnlGraficos.Visible = graficosGerais != null && graficosGerais.Count > 0;
        pnlRelatoriosFinanceiros.Visible = relatoriosFinanceiros != null && relatoriosFinanceiros.Count > 0;
        pnlGraficosFinanceiros.Visible = graficosFinanceiros != null && graficosFinanceiros.Count > 0;

        rptRelatorios.DataSource = relatoriosGerais;
        rptRelatorios.DataBind();

        rptGraficos.DataSource = graficosGerais;
        rptGraficos.DataBind();

        rptRelatoriosFinanceiros.DataSource = relatoriosFinanceiros;
        rptRelatoriosFinanceiros.DataBind();

        rptGraficosFinanceiros.DataSource = graficosFinanceiros;
        rptGraficosFinanceiros.DataBind();
    }

    public string BindingUrlRelatorio(object tela)
    {
        return ((Tela)tela).GetURLFormatada(this.ConfiguracoesSistema, this.UsuarioLogado, this.FuncaoLogada, Session["idConfig"].ToString().ToInt32());
    }
}