using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioControleDeViagens : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                CtrlHeader.Visible = false;
                RelatorioUtil.CriarColunas(grvRelatorio, ckbColunas, this.Page);
            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Alert.Show(msg.Mensagem);
            }
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorio();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Alert.Show(msg.Mensagem);
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
            Alert.Show(msg.Mensagem);
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
            Alert.Show(msg.Mensagem);
        }
    }

    private void CarregarRelatorio()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;
        Permissao permissao = Permissao.GetPermissaoDaFuncaoEFuncionario(WebUtil.FuncionarioLogado.Id, WebUtil.FuncaoLogada.Id);
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

        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoResponsavel = ddlResponsavel.SelectedIndex == 0 ? "Todos" : ddlResponsavel.SelectedItem.Text;
        string descricaoMotorista = ddlMotorista.SelectedIndex == 0 ? "Todos" : ddlMotorista.SelectedItem.Text;
        string descricaoRoteiro = ddlRoteiro.SelectedIndex == 0 ? "Todos" : ddlRoteiro.SelectedItem.Text;
        string descricaoDeptVeiculo = ddlDepartamentoVeiculo.SelectedIndex == 0 ? "Todos" : ddlDepartamentoVeiculo.SelectedItem.Text;
        string descricaoVeiculo = ddlVeiculo.SelectedIndex == 0 ? "Todos" : ddlVeiculo.SelectedItem.Text;
        string descricaoDepUtilizou = ddlDepartamentoUtilizou.SelectedIndex == 0 ? "Todos" : ddlDepartamentoUtilizou.SelectedItem.Text;
        string descricaoSetorUtilizou = ddlSetorUtilizou.SelectedIndex == 0 ? "Todos" : ddlSetorUtilizou.SelectedItem.Text;
        string descricaoAbastecimento = ddlPossuiAbastecimento.SelectedIndex == 0 ? "Todos" : ddlPossuiAbastecimento.SelectedItem.Text;

        grvRelatorio.DataSource = controles != null && controles.Count > 0 ? controles : new List<ControleViagem>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Período", descricaoPeriodo);
        CtrlHeader.InsertFiltroEsquerda("Responsável", descricaoResponsavel);
        CtrlHeader.InsertFiltroEsquerda("Motorista", descricaoMotorista);

        CtrlHeader.InsertFiltroCentro("Roteiro", descricaoRoteiro);
        CtrlHeader.InsertFiltroCentro("Departamento(Veículo)", descricaoDeptVeiculo);
        CtrlHeader.InsertFiltroCentro("Veículo", descricaoVeiculo);

        CtrlHeader.InsertFiltroDireita("Departamento(Utilizou)", descricaoDepUtilizou);
        CtrlHeader.InsertFiltroDireita("Setor", descricaoSetorUtilizou);
        CtrlHeader.InsertFiltroDireita("Abastecimento", descricaoAbastecimento);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Controles de Viagens");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

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

    public string BindingTotalTempoPermanencia(object controle)
    {
        return ((ControleViagem)controle).GetTempoPermanenciaVeiculo.FormatarDiasHorasMinutos();
    }
}