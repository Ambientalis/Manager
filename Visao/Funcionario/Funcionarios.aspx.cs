using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Funcionario_Funcionarios : PageBase
{
    private Msg msg = new Msg();

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
        this.CarregarFuncoes();
    }

    public string BindEditar(Object o)
    {
        Funcionario n = (Funcionario)o;
        return "CadastroFuncionario.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
    }

    private void CarregarFuncoes()
    {
        ddlFuncaoPesquisa.Items.Clear();

        IList<Funcao> funcoes = Funcao.ConsultarTodosOrdemAlfabetica();

        if (funcoes != null && funcoes.Count > 0)
        {
            foreach (Funcao funcao in funcoes)
            {
                ddlFuncaoPesquisa.Items.Add(new ListItem(funcao.GetDescricao, funcao.Id.ToString()));
            }
        }

        ddlFuncaoPesquisa.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void Pesquisar()
    {
        IList<Funcionario> funcionarios = Funcionario.Filtrar(tbxCodigoPesquisa.Text, tbxNomeApelidoPesquisa.Text, tbxCpfPesquisa.Text, ddlStatusPesquisa.SelectedValue, ddlFuncaoPesquisa.SelectedValue.ToInt32(), ddlVendendor.SelectedValue.ToInt32(), -1);

        gdvFuncionarios.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        gdvFuncionarios.DataBind();

        lblStatus.Text = funcionarios.Count + " funcionário(s) encontrado(s)";
    }

    private void ExcluirFuncionario(GridViewDeleteEventArgs e)
    {
        Funcionario funcionario = Funcionario.ConsultarPorId(gdvFuncionarios.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (funcionario != null)
        {
            if (funcionario.OrdensServico != null && funcionario.OrdensServico.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que sejam responsáveis por alguma Ordem Serviço. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }
            if (funcionario.Visitas != null && funcionario.Visitas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que possuam Visitas. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcionario.Atividades != null && funcionario.Atividades.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que possuam Atividades. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcionario.Reservas != null && funcionario.Reservas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que possuam Reservas de Veículo. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcionario.Excluir())
                msg.CriarMensagem("Funcionário excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.Pesquisar();
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
            gdvFuncionarios.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    protected void gdvFuncionarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvFuncionarios.PageIndex = e.NewPageIndex;
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

    protected void gdvFuncionarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirFuncionario(e);
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

    #region ______________Pre-Render_______________

    protected void gdvFuncionarios_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvFuncionarios.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este funcionário serão perdidos. Deseja realmente excluir este funcionário ?");
    }

    #endregion

}