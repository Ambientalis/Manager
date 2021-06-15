using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeReservas : PageBase
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

    #region ______________Métodos______________

    private void CarregarCampos()
    {
        this.CarregarTiposReservas();
        this.CarregarVeiculos();
        this.CarregarResponsaveis();
    }

    private void CarregarTiposReservas()
    {
        ddlTipoReserva.Items.Clear();

        ddlTipoReserva.DataValueField = "Id";
        ddlTipoReserva.DataTextField = "Descricao";

        IList<TipoReservaVeiculo> tiposReserva = TipoReservaVeiculo.ConsultarTodosOrdemAlfabetica();

        ddlTipoReserva.DataSource = tiposReserva != null ? tiposReserva : new List<TipoReservaVeiculo>();
        ddlTipoReserva.DataBind();

        ddlTipoReserva.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarVeiculos()
    {
        ddlVeiculo.Items.Clear();

        IList<Veiculo> veiculos = Veiculo.ConsultarTodosOrdemAlfabetica();

        if (veiculos != null && veiculos.Count > 0)
        {
            foreach (Veiculo veiculo in veiculos)
            {
                ddlVeiculo.Items.Add(new ListItem(veiculo.Descricao + " - " + veiculo.Placa, veiculo.Id.ToString()));
            }
        }

        ddlVeiculo.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void CarregarRelatorioReservas()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        DateTime dataDe = tbxDataDe.Text != string.Empty ? Convert.ToDateTime(tbxDataDe.Text).ToMinHourOfDay() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text != string.Empty ? Convert.ToDateTime(tbxDataAte.Text).ToMaxHourOfDay() : SqlDate.MaxValue;

        IList<Reserva> reservas = Reserva.Filtrar(dataDe, dataAte, tbxDescricao.Text, ddlTipoReserva.SelectedValue.ToInt32(), ddlVeiculo.SelectedValue.ToInt32(), ddlResponsavel.SelectedValue.ToInt32(), Convert.ToChar(ddlStatus.SelectedValue));

        string descricaoDescricao = tbxDescricao.Text.IsNotNullOrEmpty() ? tbxDescricao.Text : "Não definida";
        string descricaoTipoReserva = ddlTipoReserva.SelectedValue == "0" ? "Todos" : ddlTipoReserva.SelectedItem.Text;
        string descricaoPeriodo = WebUtil.GetDescricaoDataRelatorio(dataDe, dataAte);
        string descricaoResponsavel = ddlResponsavel.SelectedValue == "0" ? "Todos" : ddlResponsavel.SelectedItem.Text;
        string descricaoVeiculo = ddlVeiculo.SelectedValue == "0" ? "Todos" : ddlVeiculo.SelectedItem.Text;

        grvRelatorio.DataSource = reservas != null && reservas.Count > 0 ? reservas : new List<Reserva>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Data", descricaoPeriodo);
        CtrlHeader.InsertFiltroEsquerda("Descrição", descricaoDescricao);

        CtrlHeader.InsertFiltroCentro("Tipo de Reserva", descricaoTipoReserva);
        CtrlHeader.InsertFiltroCentro("Veículo", descricaoVeiculo);

        CtrlHeader.InsertFiltroDireita("Responsável", descricaoResponsavel);        

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Reservas");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioReservas();
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

    #endregion
}