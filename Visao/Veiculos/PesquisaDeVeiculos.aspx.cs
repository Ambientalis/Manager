using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Veiculos_PesquisaDeVeiculos : PageBase
{
    Msg msg = new Msg();

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
        this.CarregarGestores();
        this.CarregarDepartamentosResponsavel();
    }

    private void CarregarGestores()
    {
        ddlGestor.Items.Clear();

        ddlGestor.DataValueField = "Id";
        ddlGestor.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlGestor.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlGestor.DataBind();

        ddlGestor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarDepartamentosResponsavel()
    {
        ddlDepartamentoResponsavel.Items.Clear();

        ddlDepartamentoResponsavel.DataValueField = "Id";
        ddlDepartamentoResponsavel.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamentoResponsavel.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamentoResponsavel.DataBind();

        ddlDepartamentoResponsavel.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void Pesquisar()
    {
        IList<Veiculo> veiculos = Veiculo.Filtrar(tbxPlaca.Text, tbxDescricao.Text, ddlGestor.SelectedValue.ToInt32(), ddlStatus.SelectedValue, ddlDepartamentoResponsavel.SelectedValue.ToInt32());

        gdvVeiculos.DataSource = veiculos != null ? veiculos : new List<Veiculo>();
        gdvVeiculos.DataBind();

        lblStatus.Text = veiculos.Count + " Veículo(s) encontrado(s)";
    }

    public string BindEditar(object o) 
    {
        Veiculo n = (Veiculo)o;
        return "CadastroDeVeiculo.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
    }

    private void ExcluirVeiculo(GridViewDeleteEventArgs e)
    {
        Veiculo veiculo = Veiculo.ConsultarPorId(gdvVeiculos.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (veiculo != null)
        {
            if (veiculo.Reservas != null && veiculo.Reservas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir veículos que possuam reservas agendadas ou efetuadas. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }            

            if (veiculo.Excluir())
                msg.CriarMensagem("Veículo excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
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

    protected void gdvVeiculos_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvVeiculos.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void gdvVeiculos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvVeiculos.PageIndex = e.NewPageIndex;
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
            gdvVeiculos.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este veículo serão perdidos. Deseja realmente excluir este veículo ?");
    }

    protected void gdvVeiculos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirVeiculo(e);
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