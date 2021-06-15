using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoOSPorTipoServico : PageBase
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
            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Ordens de Serviço por Tipo de Serviço!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

            float quantidadeTotalOSNoPeriodo = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, ddlDepartamento.SelectedValue.ToInt32(), ddlSetor.SelectedValue.ToInt32(), ddlFiltrarDataPor.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());
            lblTotalOS.Text = quantidadeTotalOSNoPeriodo.ToString();

            if (quantidadeTotalOSNoPeriodo > 0)
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
                        float quantidadeOSDoTipoEDepartamentoNoPeriodo = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoTipoOSNoPeriodo(dataDe, dataAte, ddlDepartamento.SelectedValue.ToInt32(), tipoOS.Id, ddlSetor.SelectedValue.ToInt32(), ddlFiltrarDataPor.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());

                        if (quantidadeOSDoTipoEDepartamentoNoPeriodo > 0)
                        {
                            float percentualDoDepartamento = ((quantidadeOSDoTipoEDepartamentoNoPeriodo * 100) / quantidadeTotalOSNoPeriodo);
                            if (percentualDoDepartamento > 0)
                            {
                                nomesTipos.Append(tipoOS.Nome.Replace(";", "").Trim() + " - " + quantidadeOSDoTipoEDepartamentoNoPeriodo.ToString() + ";");
                                percentualDosTipos.Append(percentualDoDepartamento.ToString("N2").Replace(',', '.') + ";");
                            }
                        }
                    }
                }

                if (nomesTipos.Length > 0)
                {
                    nomesTipos.Remove(nomesTipos.Length - 1, 1);
                    percentualDosTipos.Replace(',', '.');
                    totalizadores.Visible = true;
                    string titulo = "Ordens de Serviço por Tipo de Serviço";
                    titulo += ddlDepartamento.SelectedValue.ToInt32() > 0 ? "<br />" + ddlDepartamento.SelectedItem.Text : "";
                    titulo += ddlSetor.SelectedValue.ToInt32() > 0 ? "<br />" + ddlSetor.SelectedItem.Text : "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoBarraSimplesLegendaEmbaixo('#container_grafico', '" + titulo + @"','" + nomesTipos + @"', '" + percentualDosTipos + @"', '', ' " + ddlFiltrarDataPor.SelectedItem.Text + @" de " + dataDe.ToShortDateString() + @" a " + dataAte.ToShortDateString() + @"');</script>", false);
                }
                else
                {
                    lblTotalOS.Text = "0";
                    totalizadores.Visible = true;
                }

            }
            else
            {
                lblTotalOS.Text = "0";
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