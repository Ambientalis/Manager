using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoProdutividadeDoDepartamentoOuSetor : PageBase
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
        this.CarregarSetores();
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

    private void CarregarSetores()
    {
        ddlSetor.Items.Clear();

        ddlSetor.DataValueField = "Id";
        ddlSetor.DataTextField = "Nome";

        Departamento departamento = Departamento.ConsultarPorId(ddlDepartamento.SelectedValue.ToInt32());

        ddlSetor.DataSource = departamento != null && departamento.Setores != null ? departamento.Setores : new List<Setor>();
        ddlSetor.DataBind();

        ddlSetor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            totalizadores.Visible = false;

            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Produtividade por Departamento ou Setores!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

            if (dataDe > DateTime.Now.ToMaxHourOfDay() || dataAte > DateTime.Now.ToMaxHourOfDay())
            {
                msg.CriarMensagem("As datas do gráfico de Produtividade não devem ser maiores que a data de hoje!", "Informação", MsgIcons.Informacao);
                return;
            }

            IList<OrdemServico> ordensDoDepartamento = OrdemServico.ConsultarOrdensComVencimentoNoPeriodoDoDepartamento(dataDe, dataAte, ddlDepartamento.SelectedValue.ToInt32(), ddlSetor.SelectedValue.ToInt32());
            if (ordensDoDepartamento != null && ordensDoDepartamento.Count > 0)
            {
                string quantidadesGrafico = "";
                string percentuaisGrafico = "";
                IList<OrdemServico> ordensDoDepartamentoValidas = ordensDoDepartamento.Where(x => x.GetDataVencimento.CompareTo(dataDe) > -1 && x.GetDataVencimento.CompareTo(dataAte) < 1).ToList();

                float quantidadeTotalOSNoPeriodo = ordensDoDepartamentoValidas.Count;

                float quantidadeOSEncerradasNoPrazoSemPedidoAdiamento = ordensDoDepartamentoValidas.Count(ordem => ordem.IsEncerradaNoPrazoSemPedidoAdiamento);
                float percentualEncerradasNoPrazoSemPedidoAdiamento = ((quantidadeOSEncerradasNoPrazoSemPedidoAdiamento * 100) / quantidadeTotalOSNoPeriodo);
                quantidadesGrafico += quantidadeOSEncerradasNoPrazoSemPedidoAdiamento + ";";
                percentuaisGrafico += percentualEncerradasNoPrazoSemPedidoAdiamento.ToString("N1").Replace(',', '.') + ";";

                float quantidadeOSEncerradasNoPrazoComPedidoAdiamento = ordensDoDepartamentoValidas.Count(x => x.IsEncerradaNoPrazoComPedidoAdiamento);
                float percentualEncerradasNoPrazoComPedidoAdiamento = ((quantidadeOSEncerradasNoPrazoComPedidoAdiamento * 100) / quantidadeTotalOSNoPeriodo);
                quantidadesGrafico += quantidadeOSEncerradasNoPrazoComPedidoAdiamento + ";";
                percentuaisGrafico += percentualEncerradasNoPrazoComPedidoAdiamento.ToString("N1").Replace(',', '.') + ";";

                float quantidadeOSVencidas = ordensDoDepartamentoValidas.Where(x => x.IsVencida).ToList().Count;
                float percentualVencidas = ((quantidadeOSVencidas * 100) / quantidadeTotalOSNoPeriodo);

                float quantidadeOSEncerradaNoPrazoComPedidoAdiamentoAposVencimento = ordensDoDepartamentoValidas.Count(x => x.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento);
                float percentualEncerradasNoPrazoComPedidoAdiamentoAposVencimento = ((quantidadeOSEncerradaNoPrazoComPedidoAdiamentoAposVencimento * 100) / quantidadeTotalOSNoPeriodo);

                quantidadesGrafico += (quantidadeOSVencidas + quantidadeOSEncerradaNoPrazoComPedidoAdiamentoAposVencimento) + ";";
                percentuaisGrafico += (percentualVencidas + percentualEncerradasNoPrazoComPedidoAdiamentoAposVencimento).ToString("N1").Replace(',', '.') + ";";

                if (quantidadesGrafico != "")
                {
                    string titulo = (ddlDepartamento.SelectedIndex > 0 ? ddlDepartamento.SelectedItem.Text : "Todos os Departamentos")
                        + "<br />" +
                        (ddlSetor.SelectedIndex > 0 ? ddlSetor.SelectedItem.Text : "Todos os Setores");

                    string titulosSeries = "Encerrada no Prazo;Encerrada com pedido de prazo;Vencidas";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoPizza('#container_grafico', '"
                        + titulosSeries + "','"
                        + quantidadesGrafico + @"','"
                        + percentuaisGrafico + @"', '"
                        + titulo + @"', '', '%');</script>", false);
                }
                StringBuilder buider = new StringBuilder();
                IList<Departamento> depts = ddlDepartamento.SelectedIndex > 0 ? new List<Departamento>() { Departamento.ConsultarPorId(ddlDepartamento.SelectedValue.ToInt32()) } : Departamento.ConsultarTodosOrdemAlfabetica();
                foreach (Departamento dept in depts)
                {
                    IEnumerable<OrdemServico> ordensDept = ordensDoDepartamentoValidas.Where(ordem => ordem.Setor.Departamento.Equals(dept));
                    float totalDept = ordensDept.Count();

                    if (totalDept > 0)
                    {
                        float qtdOsEncerradaPrazoDept = ordensDept.Count(x => x.IsEncerradaNoPrazoSemPedidoAdiamento);
                        float percentualEncerradaPrazoDept = ((qtdOsEncerradaPrazoDept * 100) / totalDept);

                        float qtdOsEncerradaPrazoComPedidoAdiamentoDept = ordensDept.Count(x => x.IsEncerradaNoPrazoComPedidoAdiamento);
                        float percentualEncerradaPrazoComPedidoAdiamento = ((qtdOsEncerradaPrazoComPedidoAdiamentoDept * 100) / totalDept);

                        float qtdOsVencidasDept = ordensDept.Count(x => x.IsVencida);
                        float percentualVencidasDept = ((qtdOsVencidasDept * 100) / totalDept);

                        float qtdOsEncerradasComPedidoAdiamentoAposVencimento = ordensDept.Count(x => x.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento);
                        float percentualOsEncerradasComPedidoAdiamentoAposVencimento = ((qtdOsEncerradasComPedidoAdiamentoAposVencimento * 100) / totalDept);

                        buider.Append("<strong>").Append(dept.Nome).Append("</strong> (").Append(totalDept.ToString("#0")).Append(")")
                            .Append(" [Encerrada no Prazo(").Append(qtdOsEncerradaPrazoDept.ToString("#0")).Append(")=").Append(percentualEncerradaPrazoDept.ToString("#0.0")).Append("%; ")
                            .Append("Encerrada com pedido de prazo(").Append(qtdOsEncerradaPrazoComPedidoAdiamentoDept.ToString("#0")).Append(")=").Append(percentualEncerradaPrazoComPedidoAdiamento.ToString("#0.0")).Append("%; ")
                            .Append("Vencidas(").Append((qtdOsVencidasDept + qtdOsEncerradasComPedidoAdiamentoAposVencimento).ToString("#0")).Append(")=").Append((percentualVencidasDept + percentualOsEncerradasComPedidoAdiamentoAposVencimento).ToString("#0.0")).Append("%]").Append("<br />");
                    }
                }
                lblTotalOS.Text = buider.ToString();
            }
            else
            {
                lblTotalOS.Text = "0";
            }
            totalizadores.Visible = true;
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

    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
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
}