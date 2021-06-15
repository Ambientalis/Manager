using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Veiculos_CadastroDeVeiculo : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
                this.NovoVeiculo();

                string idVeiculo = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                if (idVeiculo.ToInt32() > 0)
                    this.CarregarVeiculo(idVeiculo.ToInt32());

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
        this.CarregarDepartamentos(null);
        this.CarregarDepartamentosResponsavel();
    }

    private void CarregarDepartamentos(Veiculo veiculo)
    {
        cblDepartamentos.Items.Clear();

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        if (departamentos != null && departamentos.Count > 0)
        {
            foreach (Departamento departamento in departamentos)
            {
                ListItem itemFuncao = new ListItem(departamento.Nome, departamento.Id.ToString());

                if (veiculo != null && veiculo.DepartamentosQuePodemUtilizar != null && veiculo.DepartamentosQuePodemUtilizar.Count > 0 && veiculo.DepartamentosQuePodemUtilizar.Contains(departamento))
                    itemFuncao.Selected = true;

                cblDepartamentos.Items.Add(itemFuncao);
            }
        }
    }

    private void CarregarGestores()
    {
        ddlGestor.Items.Clear();

        ddlGestor.DataValueField = "Id";
        ddlGestor.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlGestor.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlGestor.DataBind();

        ddlGestor.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarDepartamentosResponsavel()
    {
        ddlDepartamentoResponsavel.Items.Clear();

        ddlDepartamentoResponsavel.DataValueField = "Id";
        ddlDepartamentoResponsavel.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamentoResponsavel.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamentoResponsavel.DataBind();

        ddlDepartamentoResponsavel.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVeiculo(int id)
    {
        Veiculo veiculo = Veiculo.ConsultarPorId(id);

        if (veiculo != null) 
        {
            hfId.Value = veiculo.Id.ToString();
            chkAtivo.Checked = veiculo.Ativo;            
            tbxPlaca.Text = veiculo.Placa;
            tbxDescricao.Text = veiculo.Descricao;
            ddlGestor.SelectedValue = veiculo.Gestor != null ? veiculo.Gestor.Id.ToString() : "0";
            ddlDepartamentoResponsavel.SelectedValue = veiculo.DepartamentoResponsavel != null ? veiculo.DepartamentoResponsavel.Id.ToString() : "0";

            this.CarregarDepartamentos(veiculo);
        }
    }

    private void NovoVeiculo()
    {
        hfId.Value = tbxPlaca.Text = tbxDescricao.Text = "";
        chkAtivo.Checked = true;
        this.CarregarDepartamentos(null);
        this.CarregarGestores();
        this.CarregarDepartamentosResponsavel();
    }

    private void SalvarVeiculo()
    {
        Veiculo veiculo = Veiculo.ConsultarPorId(hfId.Value.ToInt32());

        if (veiculo == null)
            veiculo = new Veiculo();

        veiculo.Ativo = chkAtivo.Checked;        
        veiculo.Placa = tbxPlaca.Text;
        veiculo.Descricao = tbxDescricao.Text;
        veiculo.Gestor = Funcionario.ConsultarPorId(ddlGestor.SelectedValue.ToInt32());
        veiculo.DepartamentoResponsavel = Departamento.ConsultarPorId(ddlDepartamentoResponsavel.SelectedValue.ToInt32());

        //Salvando os departamentos que podem utilizar o veiculo
        if (cblDepartamentos.Items != null && cblDepartamentos.Items.Count > 0)
        {
            veiculo.DepartamentosQuePodemUtilizar = new List<Departamento>();

            foreach (ListItem item in cblDepartamentos.Items)
            {
                if (item.Selected)
                {
                    Departamento departamento = Departamento.ConsultarPorId(item.Value.ToInt32());

                    if (departamento != null && !veiculo.DepartamentosQuePodemUtilizar.Contains(departamento))
                        veiculo.DepartamentosQuePodemUtilizar.Add(departamento);
                }
            }
        }

        veiculo = veiculo.Salvar();

        hfId.Value = veiculo.Id.ToString();

        msg.CriarMensagem("Veículo salvo com sucesso", "Sucesso", MsgIcons.Sucesso);

    }

    private void ExcluirVeiculo()
    {
        Veiculo veiculo = Veiculo.ConsultarPorId(hfId.Value.ToInt32());
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
        this.NovoVeiculo();
    }

    #endregion

    #region _______________ Eventos ________________

    protected void btnNovoCadastro_Click(object sender, EventArgs e)
    {
        try
        {            
            Response.Redirect("CadastroDeVeiculo.aspx");        
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarVeiculo();
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0) 
            {
                msg.CriarMensagem("Salve primeiro o veículo para poder excluí-lo.", "Informação", MsgIcons.Informacao);                
                return;
            }

            this.ExcluirVeiculo();            
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

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este veículo serão perdidos. Deseja realmente excluir este veículo ?");
    }

    #endregion
    
}