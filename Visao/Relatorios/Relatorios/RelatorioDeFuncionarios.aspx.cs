using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Utilitarios;

public partial class Relatorios_Relatorios_RelatorioDeFuncionarios : PageBase
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
        this.CarregarFuncoes();
    }

    private void CarregarFuncoes()
    {
        ddlFuncao.Items.Clear();

        IList<Funcao> funcoes = Funcao.ConsultarTodosOrdemAlfabetica();

        if (funcoes != null && funcoes.Count > 0)
        {
            foreach (Funcao funcao in funcoes)
            {
                ddlFuncao.Items.Add(new ListItem(funcao.GetDescricao, funcao.Id.ToString()));
            }
        }

        ddlFuncao.Items.Insert(0, new ListItem("-- Todas --", "0"));
    }

    private void CarregarRelatorioFuncionarios()
    {
        RelatorioUtil.ExibirColunas(grvRelatorio, ckbColunas);

        IList<Funcionario> funcionarios = Funcionario.Filtrar(tbxCodigoPesquisa.Text, tbxNomeApelidoPesquisa.Text, tbxCpfPesquisa.Text, ddlStatusPesquisa.SelectedValue, ddlFuncao.SelectedValue.ToInt32(), ddlVendendor.SelectedValue.ToInt32(), -1);

        string descricaoCodigo = tbxCodigoPesquisa.Text.IsNotNullOrEmpty() ? tbxCodigoPesquisa.Text : "Não definido";
        string descricaoNomeApelido = tbxNomeApelidoPesquisa.Text.IsNotNullOrEmpty() ? tbxNomeApelidoPesquisa.Text : "Não definido";
        string descricaoCpfCnpj = tbxCpfPesquisa.Text.IsNotNullOrEmpty() ? tbxCpfPesquisa.Text : "Não definido";
        string descricaoStatus = ddlStatusPesquisa.SelectedValue == "0" ? "Todos" : ddlStatusPesquisa.SelectedItem.Text;
        string descricaoVendedor = ddlVendendor.SelectedItem.Text;

        string descricaoFuncao = ddlFuncao.SelectedIndex == 0 ? "Todos" : ddlFuncao.SelectedItem.Text;

        grvRelatorio.DataSource = funcionarios != null && funcionarios.Count > 0 ? funcionarios : new List<Funcionario>();
        grvRelatorio.DataBind();

        if (grvRelatorio.HeaderRow != null)
            grvRelatorio.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (grvRelatorio.FooterRow != null)
            grvRelatorio.FooterRow.TableSection = TableRowSection.TableFooter;

        CtrlHeader.InsertFiltroEsquerda("Código", descricaoCodigo);
        CtrlHeader.InsertFiltroEsquerda("Nome / Apelido", descricaoNomeApelido);

        CtrlHeader.InsertFiltroCentro("CPF", descricaoCpfCnpj);
        CtrlHeader.InsertFiltroCentro("Função", descricaoFuncao);

        CtrlHeader.InsertFiltroDireita("Filtrar Vendedor", descricaoVendedor);
        CtrlHeader.InsertFiltroDireita("Status", descricaoStatus);

        CtrlHeader.Atualizar();
        CtrlHeader.ConfigurarCabecalho("Funcionários");

        RelatorioUtil.OcultarFiltros(this.Page);
        RelatorioUtil.SalvarPreferencias(ckbColunas, this.Page);
    }

    #endregion

    #region ______________Eventos______________

    protected void btnExibirRelatório_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregarRelatorioFuncionarios();
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