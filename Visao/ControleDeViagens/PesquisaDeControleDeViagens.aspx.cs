using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class ControleDeViagens_PesquisaDeControleDeViagens : PageBase
{
    private Msg msg = new Msg();
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

    #region _______________ Eventos _______________

    protected void gdvControleViagens_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvControleViagens.BottomPagerRow;
        if (pager != null)
            pager.Visible = true;
    }

    protected void gdvControleViagens_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvControleViagens.PageIndex = e.NewPageIndex;
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
            gdvControleViagens.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    protected void btnGridExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir este Controle de Viagem?");
    }

    protected void gdvControleViagens_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirControle(e);
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

    protected void ddlDepartamentoVeiculo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarVeiculos();
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

    protected void ddlDepartamentoUtilizou_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarSetores();
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

    #region _______________ Métodos _______________

    private void CarregarCampos()
    {
        this.CarregarFuncionarios();
        this.CarregarDepartamentos();
    }

    private void CarregarFuncionarios()
    {
        ddlResponsavel.Items.Clear();
        ddlMotorista.Items.Clear();

        ddlResponsavel.Items.Add(new ListItem("-- Selecione --", "0"));
        ddlMotorista.Items.Add(new ListItem("-- Selecione --", "0"));

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();
        foreach (Funcionario func in funcionarios)
        {
            ddlResponsavel.Items.Add(new ListItem(func.NomeRazaoSocial, func.Id.ToString()));
            ddlMotorista.Items.Add(new ListItem(func.NomeRazaoSocial, func.Id.ToString()));
        }
        ddlResponsavel.SelectedIndex = 0;
        ddlMotorista.SelectedIndex = 0;
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamentoVeiculo.Items.Clear();
        ddlDepartamentoUtilizou.Items.Clear();

        ddlDepartamentoVeiculo.Items.Add(new ListItem("-- Selecione --", "0"));
        ddlDepartamentoUtilizou.Items.Add(new ListItem("-- Selecione --", "0"));

        IList<Departamento> depts = Departamento.ConsultarTodosOrdemAlfabetica();
        foreach (Departamento dep in depts)
        {
            ddlDepartamentoVeiculo.Items.Add(new ListItem(dep.Nome, dep.Id.ToString()));
            ddlDepartamentoUtilizou.Items.Add(new ListItem(dep.Nome, dep.Id.ToString()));
        }
        ddlDepartamentoVeiculo.SelectedIndex = 0;
        ddlDepartamentoUtilizou.SelectedIndex = 0;

        this.CarregarVeiculos();
        this.CarregarSetores();
    }

    private void CarregarVeiculos()
    {
        ddlVeiculo.Items.Clear();
        ddlVeiculo.Items.Add(new ListItem("-- Selecione --", "0"));
        if (ddlDepartamentoVeiculo.SelectedIndex > 0)
            foreach (Veiculo vei in Departamento.ConsultarPorId(ddlDepartamentoVeiculo.SelectedValue.ToInt32()).VeiculosSobResponsabilidade)
                ddlVeiculo.Items.Add(new ListItem(vei.Descricao, vei.Id.ToString()));
        ddlVeiculo.SelectedIndex = 0;
    }

    private void CarregarSetores()
    {
        ddlSetorUtilizou.Items.Clear();
        ddlSetorUtilizou.Items.Add(new ListItem("-- Selecione --", "0"));
        if (ddlDepartamentoUtilizou.SelectedIndex > 0)
            foreach (Setor set in Departamento.ConsultarPorId(ddlDepartamentoUtilizou.SelectedValue.ToInt32()).Setores)
                ddlSetorUtilizou.Items.Add(new ListItem(set.Nome, set.Id.ToString()));
        ddlSetorUtilizou.SelectedIndex = 0;
    }

    private void Pesquisar()
    {
        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(this.GetUsuarioLogado.Id, this.GetFuncaoLogada.Id);
        DateTime dataDe = tbxDataDe.Text.IsNotNullOrEmpty() ? tbxDataDe.Text.ToDateTime() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text.IsNotNullOrEmpty() ? tbxDataAte.Text.ToDateTime() : SqlDate.MaxValue;
        IList<ControleViagem> controles = ControleViagem.Filtrar(
            permissao,
            dataDe, dataAte,
            ddlResponsavel.SelectedValue.ToInt32(),
            ddlMotorista.SelectedValue.ToInt32(),
            ddlRoteiro.SelectedValue.Trim(),
            ddlDepartamentoVeiculo.SelectedValue.ToInt32(),
            ddlVeiculo.SelectedValue.ToInt32(),
            ddlDepartamentoUtilizou.SelectedValue.ToInt32(),
            ddlSetorUtilizou.SelectedValue.ToInt32(),
            ddlPossuiAbastecimento.SelectedValue.ToInt32());

        gdvControleViagens.DataSource = controles != null ? controles : new List<ControleViagem>();
        gdvControleViagens.DataBind();

        lblStatus.Text = controles.Count + " Controle(s) de Viagem encontrado(s)";
    }

    public string BindEditar(object o)
    {
        ControleViagem controle = (ControleViagem)o;
        return "CadastroDeControleDeViagem.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + controle.Id);
    }

    private void ExcluirControle(GridViewDeleteEventArgs e)
    {
        ControleViagem controle = ControleViagem.ConsultarPorId(gdvControleViagens.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (controle != null)
        {
            if (controle.Excluir())
                msg.CriarMensagem("Controle de Viagem excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
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

}