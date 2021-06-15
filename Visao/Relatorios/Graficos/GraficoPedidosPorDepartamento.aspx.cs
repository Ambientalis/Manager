using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoPedidosPorDepartamento : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string BindValorTotalizador(object o)
    {
        return o.ToString();
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Pedidos por Departamento!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;


            IList<Pedido> pedidos = Pedido.ConsultarPedidosRelatorio(dataDe, dataAte, ddlDataDe.SelectedValue.ToInt32());
            if (pedidos.Count > 0)
            {
                lblTotalPedidos.Text = pedidos.Count.ToString();
                StringBuilder nomesDepartamentos = new StringBuilder();
                StringBuilder percentualDosDepartamentos = new StringBuilder();
                IList<float> quantidadesDosDepartamentos = new List<float>();

                IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();
                IList<string> valoresTotalizadores = new List<string>();
                float totalQuantidadePercentual = 0;

                if (departamentos != null && departamentos.Count > 0)
                {
                    float quantidadePedidosDoDepartamento = 0;

                    foreach (Departamento departamento in departamentos)
                    {
                        quantidadePedidosDoDepartamento = pedidos.Count(pedido =>
                            (pedido.OrdensServico != null && pedido.OrdensServico.Count(ordem => ordem.Setor != null && ordem.Setor.Departamento.Equals(departamento)) > 0));

                        if (quantidadePedidosDoDepartamento > 0)
                        {
                            nomesDepartamentos.Append(departamento.Nome.Replace(";", "").Trim() + ";");
                            quantidadesDosDepartamentos.Add(quantidadePedidosDoDepartamento);
                            totalQuantidadePercentual += quantidadePedidosDoDepartamento;
                            valoresTotalizadores.Add("Total de pedidos com OS's no " + departamento.Nome + " - " + quantidadePedidosDoDepartamento);
                        }
                    }

                    if (quantidadesDosDepartamentos.Count > 0)
                    {
                        for (int i = 0; i < quantidadesDosDepartamentos.Count; i++)
                        {
                            float percentualDoDepartamento = ((quantidadesDosDepartamentos[i] * 100) / totalQuantidadePercentual);
                            percentualDosDepartamentos.Append(percentualDoDepartamento.ToString("N1") + ";");
                        }
                    }
                }

                if (nomesDepartamentos.Length > 0)
                {
                    nomesDepartamentos.Remove(nomesDepartamentos.Length - 1, 1);
                    percentualDosDepartamentos.Remove(percentualDosDepartamentos.Length - 1, 1);
                    percentualDosDepartamentos.Replace(',', '.');
                    totalizadores.Visible = true;
                    rptToTalizadores.DataSource = valoresTotalizadores;
                    rptToTalizadores.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoBarraSimples('#container_grafico', 'Pedidos por Departamentos','" + nomesDepartamentos + @"', '" + percentualDosDepartamentos + @"', '', 'de " + dataDe.ToShortDateString() + @" a " + dataAte.ToShortDateString() + @"');</script>", false);
                }
                else
                {
                    totalizadores.Visible = true;
                    lblTotalPedidos.Text = "0";
                    rptToTalizadores.DataSource = new List<string>();
                    rptToTalizadores.DataBind();
                }
            }
            else
            {
                totalizadores.Visible = true;
                lblTotalPedidos.Text = "0";
                rptToTalizadores.DataSource = new List<string>();
                rptToTalizadores.DataBind();
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