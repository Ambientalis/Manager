using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using System.Configuration;
using SisWebControls;
using Modelo;

using Utilitarios.Criptografia;
using Persistencia.Fabrica;
using Modelo.Util;

public partial class Site_Login : System.Web.UI.Page
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        if (!Request.Browser.Crawler && !IsPostBack)
            try
            {
                String urlOnline = "Data Source=162.214.71.37;Integrated Security=False;User ID=manager;Password=tHaFtlG&&9[hEa4;Initial Catalog=ambientalis; Max Pool Size=1000;";
                urlOnline = Persistencia.Utilitarios.PersistenciaUtil.Encrypt(urlOnline, true);


                Session.Clear();
                this.SetIdConfig();

                Transacao.Instance.Abrir();
                ConfiguracaoUtil.RefreshConfig();
                imgLogo.ImageUrl = ConfiguracaoUtil.GetLinkLogomarca;

                string login = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("login", this.Request));

                if (login.IsNotNullOrEmpty())
                    tbxLogin.Text = login;
                favicon.Href = ConfiguracaoUtil.GetFavIcon;
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Transacao.Instance.Fechar(ref msg);
                C2MessageBox.Show(msg);
            }
    }

    #region ______________________ Eventos ______________________

    protected void btnLogar_Click(object sender, EventArgs e)
    {
        if (this.IniciarSessaoIdConfig())
            try
            {
                Transacao.Instance.Abrir();
                ConfiguracaoUtil.RefreshConfig();
                this.Acessar();
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message + ex.InnerException;
                msg.CriarMensagem(ex);
            }
            finally
            {
                Transacao.Instance.Fechar(ref msg);
                C2MessageBox.Show(msg);
            }
    }

    #endregion

    #region ______________________ Métodos ______________________

    private void SetIdConfig()
    {
        int idConfig = Request["ic"].ToInt32();
        Session["idConfig"] = idConfig;
    }

    private void Acessar()
    {
        this.AcertarPermissoes();
        Funcionario user = new Funcionario();
        user.Login = tbxLogin.Text.Trim().RemoverCaracteresEspeciais();
        user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim().RemoverCaracteresEspeciais(), true);

        if (Funcionario.ValidarUsuario(ref user))
        {
            if (!user.Ativo)
            {
                msg.CriarMensagem("Este usuário esta desativado. Por favor, contacte o administrador do sistema.", "", MsgIcons.Alerta);
                return;
            }

            if (user.Funcoes == null || user.Funcoes.Count == 0)
            {
                msg.CriarMensagem("Este usuário não possui funções definidas. Não é possível logar sem que as fuções do usuários sejam definidas. Contate o administrador do sistema.", "", MsgIcons.Alerta);
                return;
            }

            Session["usuario_logado"] = user;
            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
            {
                int index = pagina.IndexOf('=');
                //Se contém parametros na página passada para recarregamento
                if (index > -1)
                    pagina = pagina.Substring(0, index + 1) + Server.UrlEncode(pagina.Substring(index + 1, (pagina.Length - index) - 1));
                Response.Redirect("../Account/EscolhaFuncao.aspx" + "?page=" + pagina);
            }
            else
            {
                Response.Redirect("../Account/EscolhaFuncao.aspx", false);
            }
        }
        msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente.", "", MsgIcons.Alerta);
    }

    private void AcertarPermissoes()
    {
        Funcionario aux = new Funcionario();
        aux.MultiEmpresa = false;
        IList<Funcionario> funcionarios = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<Funcionario>(aux);
        foreach (Funcionario funcionario in funcionarios)
        {
            //Atualizando as senhas java
            try
            {
                String senhaAux = CriptografiaSimples.Encrypt(Criptografia.Decrypt(funcionario.Senha, true));
                String sql = "update funcionarios set senha_java = '" + senhaAux + "' where senha_java is null and id = " + funcionario.Id;
                new FabricaDAONHibernateBase().GetDAOBase().ExecutarComandoSql(sql);
            }
            catch (Exception ex) { }

            if (funcionario.Funcoes != null)
                foreach (Funcao funcaoAux in funcionario.Funcoes.Where(funcao => funcionario.Permissoes == null || funcionario.Permissoes.Count(permi => permi.Funcao.Equals(funcao)) <= 0))
                {
                    Permissao novaPermissao = new Permissao();
                    novaPermissao.Emp = funcaoAux.Setor.Departamento.Emp;
                    novaPermissao.Funcao = funcaoAux;
                    novaPermissao.Funcionario = funcionario;
                    novaPermissao.Salvar();
                }
        }
    }

    private bool IniciarSessaoIdConfig()
    {
        if (Session["idConfig"] != null && Session["idConfig"].ToString() != string.Empty)
            return true;
        if (Session["idConfig"] == null)
        {
            msg.CriarMensagem("Não foi possível acessar o sistema. Feche o navegador de internet e tente novamente.", "Informação", MsgIcons.Erro);
            btnLogar.Visible = false;
            return false;
        }
        return true;
    }

    #endregion

}