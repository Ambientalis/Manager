using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioResultadoControleViagens : PageBase
{
    private Msg msg = new Msg();

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


        ResultadoControle resultado = new ResultadoControle();
        if (controles.Count > 0)
        {
            IList<Departamento> departamentos = controles.Select(control => control.SetorQueUtilizou.Departamento).ToList();
            
            ResultadoControle resultadoPermanenciaVeiculo = new ResultadoControle();
            long totalPermanenciaVeiculo = controles.Sum(control => control.GetTempoPermanenciaVeiculoTicks);
            resultadoPermanenciaVeiculo.Nome = "Permanência com os veículo(s): " + new TimeSpan(totalPermanenciaVeiculo).FormatarDiasHorasMinutos();

            ResultadoControle resultadoKmRodados = new ResultadoControle();
            decimal totalKmRodados = controles.Sum(control => control.GetKmRodados);
            resultadoKmRodados.Nome = "Km Rodados: " + totalKmRodados.ToString("#0.0");

            ResultadoControle resultadoTotalAbastecimentos = new ResultadoControle();
            int totalAbastecimentos = controles.Sum(control => (control.Abastecimentos == null ? 0 : control.Abastecimentos.Count));
            resultadoTotalAbastecimentos.Nome = "Total de Abastecimentos: " + totalAbastecimentos;

            ResultadoControle resultadoLitrosAbastecidos = new ResultadoControle();
            decimal totalLitrosAbastecidos = controles.Sum(control => control.GetTotalLitrosAbastecidos);
            resultadoLitrosAbastecidos.Nome = "Total de Litros Abastecidos: " + totalLitrosAbastecidos.ToString("#0.0");

            ResultadoControle resultadoValorMedioLitro = new ResultadoControle();
            decimal totalValorMedioLitro = controles.Where(control => control.Abastecimentos != null && control.Abastecimentos.Count > 0).Average(control => control.GetValorMedioLitroAbastecido);
            resultadoValorMedioLitro.Nome = "Valor Médio do Litro Abastecido: " + totalValorMedioLitro.ToString().ToCurrency();

            ResultadoControle resultadoGastoTotal = new ResultadoControle();
            decimal totalGastoGeral = controles.Sum(control => control.GetGastoTotal);
            resultadoGastoTotal.Nome = "Gasto Total: " + totalGastoGeral.ToString().ToCurrency();

            ResultadoControle resultadoGastoMedio = new ResultadoControle();
            resultadoGastoMedio.Nome = "Gasto Médio: " + (totalGastoGeral / (totalKmRodados > 0 ? totalKmRodados : 1)).ToString().ToCurrency() + "/km";

            #region ___ Por Departamento ______
            foreach (Departamento dept in departamentos)
            {
                IList<ControleViagem> controlesDoDepartamento = controles.Where(control => dept.Equals(control.SetorQueUtilizou.Departamento)).ToList();
                if (controles.Count < 1)
                    continue;

                long permanenciaVeiculo = controlesDoDepartamento.Sum(control => control.GetTempoPermanenciaVeiculoTicks);
                if (permanenciaVeiculo > 0)
                {
                    resultadoPermanenciaVeiculo.Detalhamento.Add(new ResultadoControle()
                    {
                        Departamento = dept,
                        PercentualDoTotal = ((decimal)permanenciaVeiculo / totalPermanenciaVeiculo) * 100,
                        Total = new TimeSpan(permanenciaVeiculo).FormatarDiasHorasMinutos(),
                    });
                }

                decimal kmRodados = controlesDoDepartamento.Sum(control => control.GetKmRodados);
                if (kmRodados > 0)
                {
                    resultadoKmRodados.Detalhamento.Add(new ResultadoControle()
                    {
                        Departamento = dept,
                        PercentualDoTotal = (kmRodados / totalKmRodados) * 100,
                        Total = kmRodados.ToString("#0.0")
                    });
                }

                decimal abastecimentos = controlesDoDepartamento.Sum(control => (control.Abastecimentos == null ? 0 : control.Abastecimentos.Count));
                if (abastecimentos > 0)
                {
                    resultadoTotalAbastecimentos.Detalhamento.Add(new ResultadoControle()
                    {
                        Departamento = dept,
                        PercentualDoTotal = (abastecimentos / totalAbastecimentos) * 100,
                        Total = abastecimentos.ToString()

                    });
                }

                decimal litrosAbastecidos = controlesDoDepartamento.Sum(control => control.GetTotalLitrosAbastecidos);
                if (litrosAbastecidos > 0)
                {
                    resultadoLitrosAbastecidos.Detalhamento.Add(new ResultadoControle()
                    {
                        Departamento = dept,
                        PercentualDoTotal = (litrosAbastecidos / totalLitrosAbastecidos) * 100,
                        Total = litrosAbastecidos.ToString("#0.0")
                    });
                }

                decimal valorMedioLitro = controlesDoDepartamento.Average(control => control.GetValorMedioLitroAbastecido);
                if (abastecimentos > 0)
                {
                    resultadoValorMedioLitro.Detalhamento.Add(new ResultadoControle()
                    {
                        Departamento = dept,
                        PercentualDoTotal = (valorMedioLitro / totalValorMedioLitro) * 100,
                        Total = valorMedioLitro.ToString().ToCurrency()
                    });
                }

                decimal totalGasto = controlesDoDepartamento.Sum(control => control.GetGastoTotal);
                if (totalGasto > 0)
                {
                    resultadoGastoTotal.Detalhamento.Add(new ResultadoControle()
                    {
                        Departamento = dept,
                        PercentualDoTotal = (totalGasto / totalGastoGeral) * 100,
                        Total = totalGasto.ToString().ToCurrency()
                    });
                }
            }

            #endregion

            resultado.Detalhamento.Add(resultadoPermanenciaVeiculo);
            resultado.Detalhamento.Add(resultadoKmRodados);
            resultado.Detalhamento.Add(resultadoTotalAbastecimentos);
            resultado.Detalhamento.Add(resultadoLitrosAbastecidos);
            resultado.Detalhamento.Add(resultadoValorMedioLitro);
            resultado.Detalhamento.Add(resultadoGastoTotal);
            resultado.Detalhamento.Add(resultadoGastoMedio);
        }

        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoResponsavel = ddlResponsavel.SelectedIndex == 0 ? "Todos" : ddlResponsavel.SelectedItem.Text;
        string descricaoMotorista = ddlMotorista.SelectedIndex == 0 ? "Todos" : ddlMotorista.SelectedItem.Text;
        string descricaoRoteiro = ddlRoteiro.SelectedIndex == 0 ? "Todos" : ddlRoteiro.SelectedItem.Text;
        string descricaoDeptVeiculo = ddlDepartamentoVeiculo.SelectedIndex == 0 ? "Todos" : ddlDepartamentoVeiculo.SelectedItem.Text;
        string descricaoVeiculo = ddlVeiculo.SelectedIndex == 0 ? "Todos" : ddlVeiculo.SelectedItem.Text;
        string descricaoDepUtilizou = ddlDepartamentoUtilizou.SelectedIndex == 0 ? "Todos" : ddlDepartamentoUtilizou.SelectedItem.Text;
        string descricaoSetorUtilizou = ddlSetorUtilizou.SelectedIndex == 0 ? "Todos" : ddlSetorUtilizou.SelectedItem.Text;
        string descricaoAbastecimento = ddlPossuiAbastecimento.SelectedIndex == 0 ? "Todos" : ddlPossuiAbastecimento.SelectedItem.Text;

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
        CtrlHeader.ConfigurarCabecalho("Resultado do Controle de Viagens");

        rptResultados.DataSource = resultado.Detalhamento;
        rptResultados.DataBind();

        RelatorioUtil.OcultarFiltros(this.Page);
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
        return "";
    }

    [Serializable]
    public class ResultadoControle
    {
        private string nome;
        private Departamento departamento;
        private string total;
        private decimal percentualDoTotal;
        private IList<ResultadoControle> detalhamento = new List<ResultadoControle>();

        public ResultadoControle()
        {
        }

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public virtual Departamento Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        public virtual string Total
        {
            get { return total; }
            set { total = value; }
        }

        public virtual decimal PercentualDoTotal
        {
            get { return percentualDoTotal; }
            set { percentualDoTotal = value; }
        }
        public virtual IList<ResultadoControle> Detalhamento
        {
            get { return detalhamento; }
            set { detalhamento = value; }
        }

        public virtual String DescricaoDetalhamento
        {
            get
            {
                return this.Departamento.Nome + ": " + this.Total + " - " + this.PercentualDoTotal.ToString("#0.0") + "%";
            }
        }
    }
}