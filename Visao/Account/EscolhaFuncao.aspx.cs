using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;
using System.Configuration;

public partial class Account_EscolhaFuncao : PageBase
{
    private Msg msg = new Msg();
    private Funcionario FuncionarioLogado
    {
        get
        {
            return (Funcionario)Session["usuario_logado"];
        }
    }
    private Funcao FuncaoLogada
    {
        get
        {
            return (Funcao)Session["funcao_logada"];
        }
        set { Session["funcao_logada"] = value; }
    }
    private string Emp
    {
        get { return (Session["idEmp"] != null ? Session["idEmp"].ToString() : null); }
        set { Session["idEmp"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                imgLogo.ImageUrl = ConfiguracaoUtil.GetLinkLogomarca;
                Transacao.Instance.Abrir();
                if (this.FuncionarioLogado != null && this.FuncionarioLogado.Id > 0)
                {
                    IList<Funcao> funcoes = Funcao.ConsultarFuncoesDoFuncionario(this.FuncionarioLogado.Id);
                    if (funcoes != null && funcoes.Count == 1)
                    {
                        this.LogarNaFuncao(funcoes[0]);
                        return;
                    }
                    rptFuncoes.DataSource = funcoes;
                    rptFuncoes.DataBind();
                    favicon.Href = ConfiguracaoUtil.GetFavIcon;
                }
                else
                    Response.Redirect("../Account/Login.aspx", false);
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

    protected void btnFuncaoLogar_Click(object sender, EventArgs e)
    {
        try
        {
            Transacao.Instance.Abrir();
            int idFuncao = ((Button)sender).CommandArgument.ToInt32();
            this.LogarNaFuncao(Funcao.ConsultarPorId(idFuncao));
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

    private void LogarNaFuncao(Funcao funcao)
    {
        if (funcao == null)
        {
            msg.CriarMensagem("Nenhuma função válida selecionada para logar!", "Erro", MsgIcons.Informacao);
            return;
        }

        this.FuncaoLogada = funcao;
        this.Emp = funcao.Setor.Departamento.Emp.ToString();
        string pagina = Request["page"] != null ? Request["page"].Trim() : string.Empty;
        if (pagina.IsNotNullOrEmpty())
        {
            int index = pagina.IndexOf('=');
            //Se contém parametros na página passada para recarregamento
            if (index > -1)
                pagina = pagina.Substring(0, index + 1) + Server.UrlEncode(pagina.Substring(index + 1, (pagina.Length - index) - 1));
            Response.Redirect(pagina);
        }
        else
        {
            Response.Redirect("../Site/Index.aspx", false);
        }
    }

}