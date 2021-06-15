using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoProdutividadePorTecnico : PageBase
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

    private void CarregarCampos()
    {
        this.CarregarDepartamentos();
        this.CarregarResponsaveis();
    }

    private void CarregarResponsaveis()
    {
        ddlResponsavel.Items.Clear();

        ddlResponsavel.DataValueField = "Id";
        ddlResponsavel.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlResponsavel.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlResponsavel.DataBind();

        ddlResponsavel.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamento.Items.Clear();

        ddlDepartamento.DataValueField = "Id";
        ddlDepartamento.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamento.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamento.DataBind();

        ddlDepartamento.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    public string BindValorTotalizador(object o)
    {
        return o.ToString();
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            totalizadores.Visible = false;

            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Produtividade por Responsável!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

            if (dataDe > DateTime.Now.ToMaxHourOfDay() || dataAte > DateTime.Now.ToMaxHourOfDay())
            {
                msg.CriarMensagem("As datas do gráfico de Produtividade não devem ser maiores que a data de hoje!", "Informação", MsgIcons.Informacao);
                return;
            }

            IList<Funcionario> funcionarios = new List<Funcionario>();

            if (ddlResponsavel.SelectedValue.ToInt32() > 0)
                funcionarios.Add(Funcionario.ConsultarPorId(ddlResponsavel.SelectedValue.ToInt32()));
            else
                funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

            if (funcionarios != null && funcionarios.Count > 0)
            {
                string nomesFuncionarios = "";
                string percentuaisOSEncerradaPrazo = "";
                string percentuaisOSEncerradaPrazoComPedido = "";
                string percentuaisOSVencidas = "";
                IList<string> valoresTotalizadores = new List<string>();
                float totalGeralOS = 0;

                Departamento departamento = Departamento.ConsultarPorId(ddlDepartamento.SelectedValue.ToInt32());

                foreach (Funcionario funcionario in funcionarios)
                {
                    IList<OrdemServico> ordens = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, 0, funcionario.Id, ddlDepartamento.SelectedValue.ToInt32());
                    ordens = ordens.Where(x => x.GetDataVencimento.CompareTo(dataDe) > -1 && x.GetDataVencimento.CompareTo(dataAte) < 1).ToList();
                    if (ordens != null && ordens.Count > 0)
                    {
                        int quantidadeOSEncerradasPrazo = ordens.Count(ordem => ordem.IsEncerradaNoPrazoSemPedidoAdiamento);
                        int quantidadeOSEncerradasPrazoComPedidoAdiamento = ordens.Count(ordem => ordem.IsEncerradaNoPrazoComPedidoAdiamento);
                        int quantidadeOSVencidas = ordens.Count(ordem => ordem.IsVencida);
                        int quantidadeOSEncerradasPrazoComPedidoAdiamentoAposVencimento = ordens.Count(ordem => ordem.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento);

                        nomesFuncionarios += funcionario.NomeRazaoSocial.Replace(";", "").Trim() + ";";
                        valoresTotalizadores.Add(funcionario.NomeRazaoSocial + " - " + (ordens.Count));
                        totalGeralOS += ordens.Count;

                        float percentualEncerradasPrazo = ((quantidadeOSEncerradasPrazo * 100) / ordens.Count);
                        percentuaisOSEncerradaPrazo += percentualEncerradasPrazo.ToString("N1").Replace(',', '.') + ";";

                        float percentualEncerradaPrazoComPedido = ((quantidadeOSEncerradasPrazoComPedidoAdiamento * 100) / ordens.Count);
                        percentuaisOSEncerradaPrazoComPedido += percentualEncerradaPrazoComPedido.ToString("N1").Replace(',', '.') + ";";

                        float percentualOSVencidas = ((quantidadeOSVencidas * 100) / ordens.Count);
                        float percentualOSEncerradasPrazoComPedidoAposVencimento = ((quantidadeOSEncerradasPrazoComPedidoAdiamentoAposVencimento * 100) / ordens.Count);

                        percentuaisOSVencidas += (percentualOSVencidas + percentualOSEncerradasPrazoComPedidoAposVencimento).ToString("N1").Replace(',', '.') + ";";
                    }
                }

                if (nomesFuncionarios != "")
                {
                    string titulo = "Monitoramento de Produtividade por Responsável";
                    string titulosSeries = "Encerrada no Prazo;Encerrada com pedido de prazo;Vencidas";
                    titulo += departamento != null ? "<br />" + departamento.Nome : "";
                    titulo += ddlResponsavel.SelectedValue.ToInt32() > 0 ? "<br />" + ddlResponsavel.SelectedItem.Text : "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoBarraDuplasProdutividadePorResponsavel('#container_grafico', '"
                        + titulo + @"', '"
                        + nomesFuncionarios + @"','"
                        + titulosSeries + "', '"
                        + percentuaisOSEncerradaPrazo + @"', '" + percentuaisOSEncerradaPrazoComPedido + @"', '" + percentuaisOSVencidas
                        + @"', '100', 'de " + dataDe.ToShortDateString() + @" a " + dataAte.ToShortDateString() + @"', ' %');</script>", false);
                    totalizadores.Visible = true;
                    lblTotalOS.Text = "TOTALIZADORES (" + totalGeralOS.ToString() + "):";
                    rptToTalizadores.DataSource = valoresTotalizadores;
                    rptToTalizadores.DataBind();
                }
                else
                {
                    totalizadores.Visible = true;
                    lblTotalOS.Text = "TOTALIZADORES";
                    rptToTalizadores.DataSource = new List<string>();
                    rptToTalizadores.DataBind();
                }
            }
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
}