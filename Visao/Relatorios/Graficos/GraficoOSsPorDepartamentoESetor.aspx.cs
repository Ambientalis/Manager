using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoOSsPorDepartamentoESetor : PageBase
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

    public string BindValorTotalizadorDepartamento(object o)
    {
        return o.ToString();
    }

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Ordens de Serviço por Departamento e Setores!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

            float quantidadeTotalOSNoPeriodo = OrdemServico.ObterQuantidadeDeOSNoPeriodo(dataDe, dataAte, ddlFiltrarDataPor.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());
            String descricaoTotal = quantidadeTotalOSNoPeriodo.ToString();
            if (ddlFaturadas.SelectedValue.ToInt32() == 0)
            {
                float quantidadeTotalFaturada = OrdemServico.ObterQuantidadeDeOSNoPeriodo(dataDe, dataAte, ddlFiltrarDataPor.SelectedValue.ToInt32(), 1);
                float quantidadeNaoFaturada = OrdemServico.ObterQuantidadeDeOSNoPeriodo(dataDe, dataAte, ddlFiltrarDataPor.SelectedValue.ToInt32(), 2);
                descricaoTotal += " [Faturadas: " + (quantidadeTotalFaturada) + "; Sem Custo: " + (quantidadeNaoFaturada) + "]";
            }
            lblTotalOS.Text = descricaoTotal;

            trvTotalizadores.Nodes.Clear();

            if (quantidadeTotalOSNoPeriodo > 0)
            {
                StringBuilder nomesDepartamentos = new StringBuilder();
                StringBuilder percentualDosDepartamentos = new StringBuilder();
                IList<string> valoresTotalizadores = new List<string>();

                IList<Departamento> departamentos = new List<Departamento>();

                if (ddlDepartamento.SelectedValue.ToInt32() > 0)
                    departamentos.Add(Departamento.ConsultarPorId(ddlDepartamento.SelectedValue.ToInt32()));
                else
                    departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

                if (departamentos != null && departamentos.Count > 0)
                {
                    foreach (Departamento departamento in departamentos)
                    {
                        float quantidadeOsDoDepartamento = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, departamento.Id, ddlSetor.SelectedValue.ToInt32(), ddlFiltrarDataPor.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());
                        if (quantidadeOsDoDepartamento > 0)
                        {
                            String descricaoDepartamento = departamento.Nome + " - " + quantidadeOsDoDepartamento.ToString();
                            if (ddlFaturadas.SelectedValue.ToInt32() == 0)
                            {
                                float quantidadeOsFaturadasDepartamento = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, departamento.Id, ddlSetor.SelectedValue.ToInt32(), ddlFiltrarDataPor.SelectedValue.ToInt32(), 1);
                                float quantidadeOsNaoFaturadasDepartamento = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, departamento.Id, ddlSetor.SelectedValue.ToInt32(), ddlFiltrarDataPor.SelectedValue.ToInt32(), 2);
                                descricaoDepartamento += " [Faturadas: " + (quantidadeOsDoDepartamento) + "; Sem Custo: " + (quantidadeOsNaoFaturadasDepartamento) + "]";
                            }
                            TreeNode noDepartamento = new TreeNode(descricaoDepartamento, departamento.Nome);

                            nomesDepartamentos.Append(departamento.Nome.Replace(";", "").Trim() + ";");
                            float percentualDoDepartamento = ((quantidadeOsDoDepartamento * 100) / quantidadeTotalOSNoPeriodo);
                            percentualDosDepartamentos.Append(percentualDoDepartamento.ToString("N1") + ";");
                            valoresTotalizadores.Add(departamento.Nome + " - " + quantidadeOsDoDepartamento);

                            if (departamento.Setores != null && departamento.Setores.Count > 0)
                            {
                                IList<Setor> setores = new List<Setor>();

                                if (ddlSetor.SelectedValue.ToInt32() > 0)
                                    setores.Add(Setor.ConsultarPorId(ddlSetor.SelectedValue.ToInt32()));
                                else
                                    setores = departamento.Setores;

                                foreach (Setor setor in setores)
                                {
                                    float quantidadeOsDoSetor = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, departamento.Id, setor.Id, ddlFiltrarDataPor.SelectedValue.ToInt32(), ddlFaturadas.SelectedValue.ToInt32());

                                    if (quantidadeOsDoSetor > 0)
                                    {
                                        String descricaoSetor = setor.Nome + " - " + quantidadeOsDoSetor.ToString();
                                        if (ddlFaturadas.SelectedValue.ToInt32() == 0)
                                        {
                                            float quantidadeOSFaturadasSetor = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, departamento.Id, setor.Id, ddlFiltrarDataPor.SelectedValue.ToInt32(), 1);
                                            float quantidadeOSNaoFaturadasSetor = OrdemServico.ObterQuantidadeDeOSDoDepartamentoEDoSetorNoPeriodo(dataDe, dataAte, departamento.Id, setor.Id, ddlFiltrarDataPor.SelectedValue.ToInt32(), 2);
                                            descricaoSetor += " [Faturadas: " + (quantidadeOSFaturadasSetor) + "; Sem Custos: " + (quantidadeOSNaoFaturadasSetor) + "]";
                                        }
                                        TreeNode noSetor = new TreeNode(descricaoSetor, setor.Nome);
                                        noDepartamento.ChildNodes.Add(noSetor);
                                    }
                                }
                            }

                            trvTotalizadores.Nodes.Add(noDepartamento);
                        }
                    }
                }

                if (nomesDepartamentos.Length > 0)
                {
                    nomesDepartamentos.Remove(nomesDepartamentos.Length - 1, 1);
                    percentualDosDepartamentos.Remove(percentualDosDepartamentos.Length - 1, 1);
                    percentualDosDepartamentos.Replace(',', '.');
                    totalizadores.Visible = true;
                    trvTotalizadores.ExpandAll();
                    string titulo = "Ordens de Serviço por Departamento e Setor";
                    titulo += ddlDepartamento.SelectedValue.ToInt32() > 0 ? "<br />" + ddlDepartamento.SelectedItem.Text : "";
                    titulo += ddlSetor.SelectedValue.ToInt32() > 0 ? "<br />" + ddlSetor.SelectedItem.Text : "";
                    titulo += ddlFaturadas.SelectedIndex > 0 ? "<br />" + ddlFaturadas.SelectedItem.Text : "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoBarraSimples('#container_grafico', '" + titulo + @"','" + nomesDepartamentos + @"', '" + percentualDosDepartamentos + @"', '', ' " + ddlFiltrarDataPor.SelectedItem.Text + @" de " + dataDe.ToShortDateString() + @" a " + dataAte.ToShortDateString() + @"');</script>", false);
                }
                else
                {
                    totalizadores.Visible = true;
                    lblTotalOS.Text = "0";
                    trvTotalizadores.Nodes.Clear();
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