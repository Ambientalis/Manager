using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class ControleDeViagens_CadastroDeControleDeViagem : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                string idControle = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                if (idControle.ToInt32() > 0)
                    this.CarregarControleViagem(idControle.ToInt32());
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

    #region _______________ Eventos ________________

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarControleViagem();
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

    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void btnNovoAbastecimento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoAbastecimento();
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

    protected void btnSalvarAbastecimento_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarAbastecimento();
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

    protected void btnSalvarAbastecimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upControleViagens);
    }

    protected void btnNovoAbastecimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAbastecimento);
    }

    protected void btnEditarAbastecimento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upAbastecimento);
    }

    protected void gdvAbastecimentos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.EditarAbastecimento(e);
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

    protected void gdvAbastecimentos_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvAbastecimentos.BottomPagerRow;
        if (pager != null)
            pager.Visible = true;
    }

    protected void gdvAbastecimentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gdvAbastecimentos.PageIndex = e.NewPageIndex;
            this.CarregarAbastecimentos();
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
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir este Abastecimento?");
    }

    protected void gdvAbastecimentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirAbastecimento(e);
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

    protected void btnNovoCadastro_Click(object sender, EventArgs e)
    {
        Response.Redirect("CadastroDeControleDeViagem.aspx");
    }

    #endregion

    #region _______________ Métodos ________________

    private void CarregarCampos()
    {
        tbxData.Text = DateTime.Now.ToShortDateString();
        tbxDataHoraSaida.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
        ddlDepartamento.Items.Clear();
        ddlDepartamentoUtilizou.Items.Clear();

        ddlDepartamento.Items.Add(new ListItem("-- Selecione --", "0"));
        ddlDepartamentoUtilizou.Items.Add(new ListItem("-- Selecione --", "0"));

        IList<Departamento> depts = Departamento.ConsultarTodosOrdemAlfabetica();
        foreach (Departamento dep in depts)
        {
            ddlDepartamento.Items.Add(new ListItem(dep.Nome, dep.Id.ToString()));
            ddlDepartamentoUtilizou.Items.Add(new ListItem(dep.Nome, dep.Id.ToString()));
        }
        ddlDepartamento.SelectedIndex = 0;
        ddlDepartamentoUtilizou.SelectedIndex = 0;

        this.CarregarVeiculos();
        this.CarregarSetores();
    }

    private void CarregarVeiculos()
    {
        ddlVeiculo.Items.Clear();
        ddlVeiculo.Items.Add(new ListItem("-- Selecione --", "0"));
        if (ddlDepartamento.SelectedIndex > 0)
            foreach (Veiculo vei in Departamento.ConsultarPorId(ddlDepartamento.SelectedValue.ToInt32()).VeiculosSobResponsabilidade)
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

    private void CarregarControleViagem(int idControle)
    {
        hfId.Value = "0";
        ControleViagem controle = ControleViagem.ConsultarPorId(idControle);
        if (controle != null)
        {
            hfId.Value = controle.Id.ToString();
            tbxData.Text = controle.Data.ToShortDateString();
            ddlResponsavel.SelectedValue = controle.Responsavel.Id.ToString();
            ddlMotorista.SelectedValue = controle.Motorista.Id.ToString();

            ddlDepartamento.SelectedValue = controle.Veiculo.DepartamentoResponsavel.Id.ToString();
            this.CarregarVeiculos();
            ddlVeiculo.SelectedValue = controle.Veiculo.Id.ToString();
            ddlDepartamentoUtilizou.SelectedValue = controle.SetorQueUtilizou.Departamento.Id.ToString();
            this.CarregarSetores();
            ddlSetorUtilizou.SelectedValue = controle.SetorQueUtilizou.Id.ToString();

            tbxDataHoraSaida.Text = controle.DataHoraSaida.EmptyToMinValueLongFormat();
            tbxQuilometragemSaida.Text = controle.QuilometragemSaida.ToString("#0.0");

            ddlRoteiro.SelectedValue = controle.Roteiro;

            tbxDataHoraChegada.Text = controle.DataHoraChegada.EmptyToMinValueLongFormat();
            tbxQuilometragemChegada.Text = controle.QuilometragemChegada.ToString("#0.0");

            tbxObservacoes.Text = controle.Observacoes;

            this.CarregarAbastecimentos();
        }
    }

    private void SalvarControleViagem()
    {
        ControleViagem aux = ControleViagem.ConsultarPorId(hfId.Value.ToInt32());
        if (aux == null)
            aux = new ControleViagem();
        aux.Data = tbxData.Text.ToDateTime();
        aux.Responsavel = Funcionario.ConsultarPorId(ddlResponsavel.SelectedValue.ToInt32());
        aux.Motorista = Funcionario.ConsultarPorId(ddlMotorista.SelectedValue.ToInt32());

        aux.Veiculo = Veiculo.ConsultarPorId(ddlVeiculo.SelectedValue.ToInt32());
        aux.SetorQueUtilizou = Setor.ConsultarPorId(ddlSetorUtilizou.SelectedValue.ToInt32());

        aux.DataHoraSaida = tbxDataHoraSaida.Text.ToDateTime();
        aux.QuilometragemSaida = tbxQuilometragemSaida.Text.ToDecimal();

        aux.Roteiro = ddlRoteiro.SelectedValue;

        aux.DataHoraChegada = tbxDataHoraChegada.Text.ToDateTime();
        aux.QuilometragemChegada = tbxQuilometragemChegada.Text.ToDecimal();

        aux.Observacoes = tbxObservacoes.Text.Trim();
        aux = aux.Salvar();
        hfId.Value = aux.Id.ToString();

        msg.CriarMensagem("Controle de Viagem salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
    }

    private void NovoAbastecimento()
    {
        ControleViagem aux = ControleViagem.ConsultarPorId(hfId.Value.ToInt32());
        if (aux == null)
        {
            msg.CriarMensagem("É necessário Salvar o Controle de Viagem primeiro!", "Informação", MsgIcons.Informacao);
            return;
        }
        hfIdAbastecimento.Value = "0";
        tbxDataAbastecimento.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        tbxDataAbastecimento.Focus();
        tbxQtdDeLitros.Text =
        tbxQuilometragem.Text =
        tbxValorUnitarioAbastecimento.Text = "";
        tbxValorTotalAbastecimento.Text = "R$ 0,00";
        this.hfPopupNovoAbastecimento_ModalPopupExtender.Show();
    }

    private void SalvarAbastecimento()
    {
        ControleViagem controle = ControleViagem.ConsultarPorId(hfId.Value.ToInt32());
        if (controle == null)
        {
            msg.CriarMensagem("É necessário Salvar o Controle de Viagem primeiro!", "Informação", MsgIcons.Informacao);
            return;
        }

        Abastecimento aux = Abastecimento.ConsultarPorId(hfIdAbastecimento.Value.ToInt32());
        if (aux == null)
            aux = new Abastecimento();
        aux.ControleViagem = controle;
        aux.Data = tbxDataAbastecimento.Text.ToDateTime();
        aux.QtdLitros = tbxQtdDeLitros.Text.ToDecimal();
        aux.QuilometragemGeral = tbxQuilometragem.Text.ToDecimal();
        aux.ValorUnitario = tbxValorUnitarioAbastecimento.Text.ToDecimal();
        aux.ValorTotal = aux.QtdLitros * aux.ValorUnitario;
        aux = aux.Salvar();
        if (controle.Abastecimentos == null)
            controle.Abastecimentos = new List<Abastecimento>();
        controle.Abastecimentos.Remove(aux);
        controle.Abastecimentos.Add(aux);
        controle.Salvar();

        this.hfPopupNovoAbastecimento_ModalPopupExtender.Hide();
        this.CarregarAbastecimentos();
        msg.CriarMensagem("Abastecimento salvo com sucesso!", "Sucesso!", MsgIcons.Sucesso);
    }

    public void CarregarAbastecimentos()
    {
        ControleViagem aux = ControleViagem.ConsultarPorId(hfId.Value.ToInt32());
        gdvAbastecimentos.DataSource = null;
        if (aux != null)
        {
            List<Abastecimento> retorno = (aux.Abastecimentos != null ? aux.Abastecimentos.ToList() : new List<Abastecimento>());
            retorno.Sort((Abastecimento ab1, Abastecimento ab2) => ab1.Data.CompareTo(ab2.Data));
            gdvAbastecimentos.DataSource = retorno;
        }
        gdvAbastecimentos.DataBind();
    }

    private void EditarAbastecimento(GridViewEditEventArgs e)
    {
        Abastecimento aux = Abastecimento.ConsultarPorId(this.gdvAbastecimentos.DataKeys[e.NewEditIndex].Values[0].ToString().ToInt32());
        if (aux == null)
        {
            msg.CriarMensagem("Abastecimento inválido ou inexistente!", "Informação", MsgIcons.Informacao);
            return;
        }
        this.NovoAbastecimento();
        hfIdAbastecimento.Value = aux.Id.ToString();
        tbxDataAbastecimento.Text = aux.Data.ToString("dd/MM/yyyy HH:mm:ss");
        tbxQtdDeLitros.Text = aux.QtdLitros.ToString("#0.0");
        tbxQuilometragem.Text = aux.QuilometragemGeral.ToString("#0.0");
        tbxValorUnitarioAbastecimento.Text = aux.ValorUnitario.ToString("#0.00");
        tbxValorTotalAbastecimento.Text = aux.ValorTotal.ToString().ToCurrency();
    }

    private void ExcluirAbastecimento(GridViewDeleteEventArgs e)
    {
        Abastecimento aux = Abastecimento.ConsultarPorId(this.gdvAbastecimentos.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (aux == null)
        {
            msg.CriarMensagem("Abastecimento inválido ou inexistente!", "Informação", MsgIcons.Informacao);
            return;
        }
        ControleViagem controle = ControleViagem.ConsultarPorId(hfId.Value.ToInt32());
        controle.Abastecimentos.Remove(aux);
        aux.Excluir();
        Transacao.Instance.Recarregar();
        this.CarregarAbastecimentos();
    }

    #endregion

}