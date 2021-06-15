using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Graficos_GraficoProdutividadeIndividualTecnicoPorTipoServico : PageBase
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
        this.CarregarTiposServico();
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamento.Items.Clear();

        ddlDepartamento.DataValueField = "Id";
        ddlDepartamento.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamento.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamento.DataBind();

        ddlDepartamento.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarResponsaveis()
    {
        ddlResponsavel.Items.Clear();

        ddlResponsavel.DataValueField = "Id";
        ddlResponsavel.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlResponsavel.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlResponsavel.DataBind();

        ddlResponsavel.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            totalizadores.Visible = false;

            if (!tbxDataDe.Text.IsNotNullOrEmpty() || !tbxDataAte.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Escolha um período para gerar o gráfico de Produtividade por Tipo de Serviço!", "Informação", MsgIcons.Informacao);
                return;
            }

            if (ddlResponsavel.SelectedValue.ToInt32() <= 0)
            {
                msg.CriarMensagem("Escolha um funcionário para gerar o gráfico de Produtividade por Tipo de Serviço!", "Informação", MsgIcons.Informacao);
                return;
            }

            DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
            DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

            if (dataDe > DateTime.Now.ToMaxHourOfDay() || dataAte > DateTime.Now.ToMaxHourOfDay())
            {
                msg.CriarMensagem("As datas do gráfico de Produtividade não devem ser maiores que a data de hoje!", "Informação", MsgIcons.Informacao);
                return;
            }

            Funcionario funcionario = Funcionario.ConsultarPorId(ddlResponsavel.SelectedValue.ToInt32());

            IList<TipoOrdemServico> tiposOrdemServico = new List<TipoOrdemServico>();

            if (ddlTipoServico.SelectedValue.ToInt32() > 0)
                tiposOrdemServico.Add(TipoOrdemServico.ConsultarPorId(ddlTipoServico.SelectedValue.ToInt32()));
            else
                tiposOrdemServico = TipoOrdemServico.ConsultarTodosOrdemAlfabetica();

            if (tiposOrdemServico != null && tiposOrdemServico.Count > 0)
            {
                float maiorValorGrafico = 0;

                float totalGeralOS = 0;
                float totalOSEncerradaNoPrazo = 0;
                float totalOSEncerradaNoPrazoComPedidoAdiamento = 0;
                float totalOSVencida = 0;

                StringBuilder nomesTipos = new StringBuilder();
                StringBuilder quantidadesOSEncerradasPrazo = new StringBuilder();
                StringBuilder quantidadesOSEncerradasPrazoComPedidoAdiamento = new StringBuilder();
                StringBuilder quantidadesOSVencidas = new StringBuilder();

                foreach (TipoOrdemServico tipoOs in tiposOrdemServico)
                {
                    IList<OrdemServico> ordens = OrdemServico.ObterOssDoFuncionarioNoPeriodo(dataDe, dataAte, tipoOs.Id, ddlResponsavel.SelectedValue.ToInt32(), ddlDepartamento.SelectedValue.ToInt32());
                    ordens = ordens.Where(x => x.GetDataVencimento.CompareTo(dataDe) > -1 && x.GetDataVencimento.CompareTo(dataAte) < 1).ToList();

                    if (ordens != null && ordens.Count > 0)
                    {
                        if (ordens.Count > maiorValorGrafico)
                            maiorValorGrafico = ordens.Count;

                        totalGeralOS += ordens.Count;

                        nomesTipos.Append(tipoOs.Nome.Replace(";", "").Trim() + ";");

                        int quantidadeOSEncerradasPrazo = ordens.Count(ordem => ordem.IsEncerradaNoPrazoSemPedidoAdiamento);
                        quantidadesOSEncerradasPrazo.Append(quantidadeOSEncerradasPrazo + ";");
                        totalOSEncerradaNoPrazo += quantidadeOSEncerradasPrazo;

                        int quantidadeOSEncerradasPrazoComPedidoAdiamento = ordens.Count(ordem => ordem.IsEncerradaNoPrazoComPedidoAdiamento);
                        quantidadesOSEncerradasPrazoComPedidoAdiamento.Append(quantidadeOSEncerradasPrazoComPedidoAdiamento + ";");
                        totalOSEncerradaNoPrazoComPedidoAdiamento += quantidadeOSEncerradasPrazoComPedidoAdiamento;

                        int quantidadeOSVencidas = ordens.Count(ordem => ordem.IsVencida || ordem.IsEncerradaNoPrazoComPedidoAdiamentoAposVencimento);
                        quantidadesOSVencidas.Append((quantidadeOSVencidas) + ";");
                        totalOSVencida += quantidadeOSVencidas;
                    }
                }

                if (nomesTipos.Length > 0)
                {
                    nomesTipos.Remove(nomesTipos.Length - 1, 1);
                    quantidadesOSEncerradasPrazo.Remove(quantidadesOSEncerradasPrazo.Length - 1, 1);
                    quantidadesOSEncerradasPrazoComPedidoAdiamento.Remove(quantidadesOSEncerradasPrazoComPedidoAdiamento.Length - 1, 1);
                    quantidadesOSVencidas.Remove(quantidadesOSVencidas.Length - 1, 1);

                    string titulo = "Monitoramento de Produtividade por Colaborador e Tipo de Serviço";
                    string titulosSeries = "Encerrada no Prazo;Encerrada com pedido de prazo;Vencidas";
                    titulo += ddlDepartamento.SelectedValue.ToInt32() > 0 ? "<br />" + ddlDepartamento.SelectedItem.Text : "";
                    titulo += ddlResponsavel.SelectedValue.ToInt32() > 0 ? "<br />" + ddlResponsavel.SelectedItem.Text : "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "<script>CriarGraficoBarraDuplasProdutividadePorResponsavel('#container_grafico', '"
                        + titulo + @"', '"
                        + nomesTipos + @"','"
                        + titulosSeries + "', '"
                        + quantidadesOSEncerradasPrazo + @"', '" + quantidadesOSEncerradasPrazoComPedidoAdiamento + @"', '" + quantidadesOSVencidas + @"', '"
                        + maiorValorGrafico + @"', 'de " + dataDe.ToShortDateString() + @" a " + dataAte.ToShortDateString() + @"', '');</script>", false);


                    lblTotalOS.Text = "TOTALIZADORES (" + totalGeralOS.ToString() + "):";
                    lblTotalOsEncerradasNoPrazo.Text = totalOSEncerradaNoPrazo.ToString();
                    lblTotalOsEncerradasNoPrazoComPedidoAdiamento.Text = totalOSEncerradaNoPrazoComPedidoAdiamento.ToString();
                    lblTotalOsVencidas.Text = totalOSVencida.ToString();
                }
                else
                {
                    lblTotalOS.Text = "TOTALIZADORES";
                    lblTotalOsEncerradasNoPrazo.Text =
                    lblTotalOsEncerradasNoPrazoComPedidoAdiamento.Text =
                    lblTotalOsVencidas.Text = "0";
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
