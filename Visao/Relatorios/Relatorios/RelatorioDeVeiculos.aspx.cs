using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeVeiculos : PageBase
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
        this.CarregarGestores();
        this.CarregarDepartamentosResponsavel();
    }

    private void CarregarGestores()
    {
        ddlGestor.Items.Clear();

        ddlGestor.DataValueField = "Id";
        ddlGestor.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlGestor.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlGestor.DataBind();

        ddlGestor.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarDepartamentosResponsavel()
    {
        ddlDepartamentoResponsavel.Items.Clear();

        ddlDepartamentoResponsavel.DataValueField = "Id";
        ddlDepartamentoResponsavel.DataTextField = "Nome";

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamentoResponsavel.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamentoResponsavel.DataBind();

        ddlDepartamentoResponsavel.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarRelatorioVeiculos()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<Veiculo> veiculos = Veiculo.Filtrar(tbxPlaca.Text, tbxDescricao.Text, ddlGestor.SelectedValue.ToInt32(), ddlStatus.SelectedValue, ddlDepartamentoResponsavel.SelectedValue.ToInt32());
                
        string descricaoPlaca = tbxPlaca.Text.IsNotNullOrEmpty() ? tbxPlaca.Text : "Não definido";
        string descricaoDepartamentoResp = ddlDepartamentoResponsavel.SelectedValue == "0" ? "Todos" : ddlDepartamentoResponsavel.SelectedItem.Text;
        string descricaoDescricao = tbxDescricao.Text.IsNotNullOrEmpty() ? tbxDescricao.Text : "Não definido";
        string descricaoGestor = ddlGestor.SelectedValue == "0" ? "Todos" : ddlGestor.SelectedItem.Text;
        string descricaoStatus = ddlStatus.SelectedItem.Text;
        
        grvRelatorio.DataSource = veiculos != null && veiculos.Count > 0 ? veiculos : new List<Veiculo>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        
        CtrlHeader.InsertFiltroEsquerda("Placa", descricaoPlaca);
        CtrlHeader.InsertFiltroEsquerda("Departamento Responsável", descricaoDepartamentoResp);

        CtrlHeader.InsertFiltroCentro("Descrição", descricaoDescricao);
        CtrlHeader.InsertFiltroCentro("Gestor", descricaoGestor);
        
        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Veículos");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________ Eventos ______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioVeiculos();
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