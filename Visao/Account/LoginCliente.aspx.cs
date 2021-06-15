using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Account_LoginCliente : System.Web.UI.Page
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        if (!Request.Browser.Crawler && !IsPostBack)
            try
            {
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

        Cliente user = new Cliente();
        user.Login = tbxLogin.Text.Trim().RemoverCaracteresEspeciais();
        user.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxSenha.Text.Trim().RemoverCaracteresEspeciais(), true);

        if (Cliente.ValidarUsuario(ref user))
        {
            if (!user.Ativo)
            {
                msg.CriarMensagem("Este usuário esta desativado. Por favor, contacte o administrador do sistema.", "", MsgIcons.Alerta);
                return;
            }

            Session["usuario_cliente_logado"] = user;
            string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
            if (pagina.IsNotNullOrEmpty())
            {
                int index = pagina.IndexOf('=');
                //Se contém parametros na página passada para recarregamento
                if (index > -1)
                    pagina = pagina.Substring(0, index + 1) + Server.UrlEncode(pagina.Substring(index + 1, (pagina.Length - index) - 1));
                Response.Redirect("../AcompanhamentoCliente/AcompanhamentoPedidoCliente.aspx" + "?page=" + pagina);
            }
            else
            {
                Response.Redirect("../AcompanhamentoCliente/AcompanhamentoPedidoCliente.aspx", false);
            }
        }




        msg.CriarMensagem("Login e/ou senha incorreto(s). Verifique se a tecla \"Caps Lock\" está ativada e tente novamente.", "", MsgIcons.Alerta);
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