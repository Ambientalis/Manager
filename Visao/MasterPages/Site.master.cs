using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using Utilitarios;
using Modelo;
using Utilitarios.Criptografia;
using SisWebControls;
using Persistencia.Fabrica;
using Modelo.Util;
using NHibernate;


public partial class SiteMaster : System.Web.UI.MasterPage
{
    private Msg msg = new Msg();

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

    public bool UsuarioPossuiAcessoUrl()
    {
        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.UsuarioLogado.Id, this.FuncaoLogada.Id);
        IList<Tela> telas = permissao != null ? Tela.ConsultarTelasDaPermissaoPorOrdemPrioridade(permissao) : null;
        Session["telas_usuario_logado"] = telas;
        if (Session["telas_usuario_logado"] != null && this.UsuarioLogado != null && Session["idConfig"] != null)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri.Trim();

            if (url.Contains("Site/Index.aspx"))
                return true;

            if (url.Contains("/OrdemServico/CadastroDeOS.aspx"))
            {
                Tela telaPesquisaOS = Tela.ConsultarPorNome("Ordens de Serviço");

                if (telaPesquisaOS != null && telas.Contains(telaPesquisaOS))
                    return true;
            }

            if (url.Contains("/Visitas/CadastroDeVisita.aspx"))
            {
                Tela telaPesquisaVisitas = Tela.ConsultarPorNome("Visitas");

                if (telaPesquisaVisitas != null && telas.Contains(telaPesquisaVisitas))
                    return true;
            }

            if (url.Contains("/Atividades/CadastroDeAtividade.aspx"))
            {
                Tela telaPesquisaAtividades = Tela.ConsultarPorNome("Atividades");

                if (telaPesquisaAtividades != null && telas.Contains(telaPesquisaAtividades))
                    return true;
            }

            if (url.Contains("SolicitacaoAdiamento.aspx"))
            {
                return (permissao.AdiaPrazoDiretoriaOS != Permissao.NENHUMA) || (permissao.AdiaPrazoLegalOS != Permissao.NENHUMA);
            }

            if (url.Contains("FiltrosRelatorios.aspx"))
            {
                return Tela.UsuarioPossuiAlgumRelatorio(permissao);
            }

            foreach (Tela tela in telas)
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

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["idConfig"] == null || string.IsNullOrEmpty(Session["idConfig"].ToString()))
            {
                String paginaLogin = "../Account/Login.aspx?page=" + this.Request.Url.AbsoluteUri + "&ic=" + WebUtil.IdConfig;
                Response.Redirect(paginaLogin);
                return;
            }
            else
                WebUtil.IdConfig = Session["idConfig"].ToString().ToInt32();

            //Veio de uma página do financeiro, então ler a permissão passada e depois recarregar a página
            if (Request["permission"] != null && Request["permission"].ToString().IsNotNullOrEmpty())
            {
                String[] valores = CriptografiaSimples.Decrypt(Request["permission"]).Split(';');
                int idConfig = valores[2].ToInt32();

                Session["idConfig"] = WebUtil.IdConfig = idConfig;
                Transacao.Instance.Finalizar();
                Transacao.Instance.Abrir(idConfig);
                Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(valores[0].ToInt32(), valores[1].ToInt32());
                this.CarregarPermissao(permissao);
                return;
            }

            //Usuário Funcionário
            if (this.UsuarioLogado == null || this.FuncaoLogada == null)
            {
                String paginaLogin = "../Account/Login.aspx?page=" + this.Request.Url.AbsoluteUri + "&ic=" + WebUtil.IdConfig;
                Response.Redirect(paginaLogin);
                return;
            }
            else
            {
                if (!UsuarioPossuiAcessoUrl())
                {
                    Response.Redirect("../Site/PermissaoInsuficiente.aspx" + Seguranca.MontarParametros("codigoEmpresa=" + WebUtil.IdConfig));
                    return;
                }
                else
                    this.CarregarCaracteristicasUsuario();
            }
            favicon.Href = ConfiguracaoUtil.GetFavIcon;
            imgLogomarca.ImageUrl = ConfiguracaoUtil.GetLinkLogomarca;
            btnLogout.PostBackUrl = "../Account/Login.aspx?ic=" + WebUtil.IdConfig;
            lblContato.Text = ConfiguracaoUtil.GetContatoEmpresa;
        }
        base.OnLoad(e);
    }

    private void CarregarPermissao(Permissao permissao)
    {
        bool trocaUsuarioFuncao = !permissao.Funcionario.Equals(this.UsuarioLogado)
                   || !permissao.Funcao.Equals(this.FuncaoLogada)
                   || permissao.Funcao.Setor.Departamento.Emp != this.Emp.ToInt32();

        this.UsuarioLogado = permissao.Funcionario;
        this.FuncaoLogada = permissao.Funcao;
        this.Emp = this.FuncaoLogada.Setor.Departamento.Emp.ToString();

        //Se trocou alguma coisa, recarregar o menu
        //É feita essa verificação porque o carregamento do menu é demorado
        if (trocaUsuarioFuncao)
            Session["menus_principais"] = null;

        if (UsuarioPossuiAcessoUrl())
        {
            String urlSemParametros = Request.Url.GetLeftPart(UriPartial.Path);
            //Veio com parâmetro para acessar o cadastro de pedido
            if (Request["o"] != null)
                urlSemParametros = urlSemParametros + "?o=" + Request["o"].ToString();
            //Veio com parametro para acesar a tela de fornecedores
            if (Request["frn"] != null)
                urlSemParametros = urlSemParametros + "?frn=" + Request["frn"].ToString();

            Response.Redirect(urlSemParametros);
        }
        else
            Response.Redirect("../Site/Index.aspx", false);
    }

    #region ______________ TRANSAÇÕES _________________

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
            MBOX1.Show("ERRO ao tentar se comunicar com o servidor. ERRO:" + ex.Message, "Falha");
            //throw;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Transacao.Instance.Fechar(ref msg);
        MBOX1.Show(msg);
    }

    protected void Page_Error(object sender, EventArgs e)
    {
        Transacao.Instance.Fechar(ref msg);
        MBOX1.Show(msg);
    }

    #endregion

    #region ______________ Eventos _________________

    protected void btnTrocarFuncao_Click(object sender, EventArgs e)
    {
        try
        {
            int idFuncao = ((LinkButton)sender).CommandArgument.ToInt32();
            Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.UsuarioLogado.Id, idFuncao);
            this.CarregarPermissao(permissao);
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

    #region ______________ Métodos _________________

    private void CarregarCaracteristicasUsuario()
    {
        if (this.UsuarioLogado != null)
        {
            this.CarregarIdentificacaoFuncionario();
            this.CarregarMenus();
        }
    }

    private void CarregarIdentificacaoFuncionario()
    {
        Funcao funcao = Funcao.ConsultarPorId(this.FuncaoLogada.Id);
        lblDadosUsuarioLogado.Text = this.UsuarioLogado != null && funcao != null ? this.UsuarioLogado.NomeRazaoSocial + " - " + funcao.GetDescricao : "Anônimo";
        separador_funcao.Visible = btnTrocarFuncao.Visible = this.UsuarioLogado != null && this.UsuarioLogado.Funcoes != null && this.UsuarioLogado.Funcoes.Count > 1;
        rptFuncoesFuncionario.DataSource = this.UsuarioLogado.ConsultarPorId().Funcoes;
        rptFuncoesFuncionario.DataBind();
    }

    private void CarregarMenus()
    {
        StringBuilder builder = new StringBuilder();
        if (Session["menus_principais"] == null)
        {
            IList<Tela> telasUsuario = (IList<Tela>)Session["telas_usuario_logado"];

            if (telasUsuario != null && telasUsuario.Count > 0)
            {
                builder.Append("<ul class='menu'>");
                foreach (Modelo.Menu menu in Modelo.Menu.GetMenusDasTelas(telasUsuario))
                    this.CarregarMenusRecursivo(builder, menu);
                builder.Append("</ul>");
            }

            Session["menus_principais"] = builder.ToString();
        }
        lblmenuDinamico.Text = Session["menus_principais"].ToString();

    }

    private void CarregarMenusRecursivo(StringBuilder builder, Modelo.Menu menu)
    {
        if (menu != null)
        {
            builder.Append("<li onclick='openMenu(event,this)'>");
            builder.Append("<a href='#'>").Append(menu.Nome).Append("</a>");
            builder.Append("<ul>");
            foreach (Tela tela in menu.Telas)
            {
                if (!tela.ExibirNoMenu)
                    continue;
                builder.Append("<li title='").Append(tela.ToolTip).Append("'>")
                    .Append("<a href='")
                    .Append(tela.GetURLFormatada(this.ConfiguracoesSistema, this.UsuarioLogado, this.FuncaoLogada, Session["idConfig"].ToString().ToInt32())).Append("'>")
                    .Append("<span style='margin-top: 1px;margin-right: 5px;' class='").Append(tela.Icone).Append("'></span>")
                    .Append("<span>").Append(tela.Nome.Trim()).Append("</span>")
                    .Append("</a>")
                    .Append("</li>");
            }

            if (menu.SubMenus != null)
                foreach (Modelo.Menu subMenu in menu.SubMenus)
                    this.CarregarMenusRecursivo(builder, subMenu);

            builder.Append("</ul>");
            builder.Append("</li>");
        }
    }

    #endregion
}
