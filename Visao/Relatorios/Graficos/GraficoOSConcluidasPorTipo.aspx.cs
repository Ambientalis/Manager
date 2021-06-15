 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoOSConcluidasPorTipo : PageBase
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
        this.CarregarTiposServico();
    }

    private void CarregarTiposServico()
    {
        ddlTipoServico.Items.Clear();

        ddlTipoServico.DataValueField = "Id";
        ddlTipoServico.DataTextField = "Nome";

        IList<TipoOrdemServico> tiposOS = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

        ddlTipoServico.DataSource = tiposOS != null ? tiposOS : new List<TipoOrdemServico>();
        ddlTipoServico.DataBind();

        ddlTipoServico.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    protected void btnExibirRelatorio_Click(object sender, EventArgs e)
    {
        try
        {
            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Ordens de Serviço concluídas por Tipo de Serviço!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

            float quantidadeOSConcluidasNoPeriodo = OrdemServico.ObterQuantidadeDeOSConcluidasNoPeriodo(dataDe, dataAte, ddlDepartamento.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());
            lblTotalOS.Text = quantidadeOSConcluidasNoPeriodo.ToString();

            if (quantidadeOSConcluidasNoPeriodo > 0)
            {
                StringBuilder nomesTipos = new StringBuilder();
                StringBuilder percentualDosTipos = new StringBuilder();

                IList<TipoOrdemServico> tiposServico = new List<TipoOrdemServico>();

                if (ddlTipoServico.SelectedValue.ToInt32() > 0)
                    tiposServico.Add(TipoOrdemServico.ConsultarPorId(ddlTipoServico.SelectedValue.ToInt32()));
                else
                    tiposServico = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

                if (tiposServico != null && tiposServico.Count > 0)
                {
                    foreach (TipoOrdemServico tipoOS in tiposServico)
                    {
                        float quantidadeOSConcluidaDoTipoNoPeriodo = OrdemServico.ObterQuantidadeDeOSConcluidasEDoTipoOSNoPeriodo(dataDe, dataAte, tipoOS.Id, ddlDepartamento.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());

                        if (quantidadeOSConcluidaDoTipoNoPeriodo > 0)
                        {
                            nomesTipos.Append(tipoOS.Nome.Replace(";", "").Trim() + " - " + quantidadeOSConcluidaDoTipoNoPeriodo.ToString() + ";");
                            float percentualDoTipo = ((quantidadeOSConcluidaDoTipoNoPeriodo * 100) / quantidadeOSConcluidasNoPeriodo);
                            percentualDosTipos.Append(percentualDoTipo.ToString("N2").Replace(',', '.') + ";");
                        }
                    }
                }

                if (nomesTipos.Length > 0)
                {
                    nomesTipos.Remove(nomesTipos.Length - 1, 1);
                    percentualDosTipos.Remove(percentualDosTipos.Length - 1, 1);
                    string titulo = "Ordens de Serviço concluídas por Tipo de Serviço";
                    titulo += ddlDepartamento.SelectedValue.ToInt32() > 0 ? "<br />" + ddlDepartamento.SelectedItem.Text : "";
                    titulo += ddlFaturadas.SelectedIndex > 0 ? "<br />" + ddlFaturadas.SelectedItem.Text : "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoBarraSimplesLegendaEmbaixo('#container_grafico', '" + titulo + @"','" + nomesTipos + @"', '" + percentualDosTipos + @"', '', 'de " + dataDe.ToShortDateString() + @" a " + dataAte.ToShortDateString() + @"');</script>", false);
                }
                else
                {
                    lblTotalOS.Text = "0";
                }

                totalizadores.Visible = true;

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