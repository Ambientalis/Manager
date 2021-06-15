using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_MasterPage_MasterPageRelatorios : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        if (Session["idConfig"] == null || Session["usuario_logado"] == null)
        {
            String paginaLogin = "../../Account/Login.aspx?page=" + this.Request.Url.AbsoluteUri + "&ic=" + WebUtil.IdConfig;
            Response.Redirect(paginaLogin);
            return;
        }
        else
        {
            Funcionario user = (Funcionario)Session["usuario_logado"];
            if (!UsuarioPossuiAcessoUrl())
                Response.Redirect("../Account/PermissaoInsufuciente.aspx");

            lblDadosUsuarioLogado.Text = WebUtil.FuncionarioLogado != null && WebUtil.FuncaoLogada != null ? WebUtil.FuncionarioLogado.NomeRazaoSocial + " - " + WebUtil.FuncaoLogada.ConsultarPorId().GetDescricao : "Anônimo";

            IList<Tela> telas = Session["telas_relatorios_usuario_logado"] != null ? (IList<Tela>)Session["telas_relatorios_usuario_logado"] : new List<Tela>();
            rptRelatorios.DataSource = telas;
            rptRelatorios.DataBind();

            favicon.Href = ConfiguracaoUtil.GetFavIcon;
            imgLogoEmpresa.ImageUrl = ConfiguracaoUtil.GetLinkLogomarca;
            lblContato.Text = ConfiguracaoUtil.GetContatoEmpresa;
        }
    }

    public bool UsuarioPossuiAcessoUrl()
    {
        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);

        IList<Tela> telas = permissao != null ? Tela.ConsultarTelasTodosRelatorioDaPermissaoPorOrdemPrioridade(permissao) : null;

        Session["telas_relatorios_usuario_logado"] = telas;

        if (Session["telas_relatorios_usuario_logado"] != null && WebUtil.FuncionarioLogado != null && Session["idConfig"] != null)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri.Trim();

            foreach (Tela tela in (IList<Tela>)Session["telas_relatorios_usuario_logado"])
                if (tela.Url != null)
                    if (url.Contains(tela.Url.Replace("../", "").Trim()))
                        return true;
            return false;
        }
        else
        {
            return false;
        }

    }

    #region ______________ Eventos _________________

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["idConfig"] != null)
            {
                Page.Unload += new EventHandler(Page_Unload);
                Page.Error += new EventHandler(Page_Error);
                Transacao.Instance.Abrir();
            }
        }
        catch (Exception ex)
        {
            Alert.Show("ERRO ao tentar se comunicar com o servidor. ERRO:" + ex.Message);
            throw;
        }

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Transacao.Instance.Fechar(ref msg);
        Alert.Show(msg.Mensagem);
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Transacao.Instance.Fechar(ref msg);
        Alert.Show(msg.Mensagem);
    }

    protected void ibtnResetarPreferencias_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.ResetarPreferencias();
        }
        catch (Exception ex)
        {
            Alert.Show(ex.Message);
        }
    }

    #endregion

    #region ______________ Métodos _________________

    public string BindingCaminhoTela(Object tela)
    {
        Tela telaAcessar = (Tela)tela;
        return telaAcessar.Url.Replace("../Relatorios/", "../");
    }

    private void ResetarPreferencias()
    {
        Funcionario usuario = Funcionario.ConsultarPorId(WebUtil.FuncionarioLogado.Id);
        PreferenciaRelatorio pref = PreferenciaRelatorio.Consultar(this.Page.AppRelativeVirtualPath.Replace("~", "..").Replace("../Relatorios/", ""), usuario);
        if (pref != null)
        {
            pref.Excluir();
            Transacao.Instance.Recarregar();
        }
        WebUtil.RedirectToPage(this.Page);
    }

    #endregion
}
