using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Permissoes_Permissoes : PageBase
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
                C2MessageBox.Show(msg);
            }
    }

    #region _______________ Métodos _______________

    private void CarregarCampos()
    {
        this.CarregarDepartamentos();
        this.CarregarCargos();
        this.CarregarUsuarios();
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamento.Items.Clear();

        IList<Departamento> departamentos = Departamento.ConsultarTodosOrdemAlfabetica();

        ddlDepartamento.DataValueField = "Id";
        ddlDepartamento.DataTextField = "Nome";

        ddlDepartamento.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamento.DataBind();

        ddlDepartamento.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarCargos()
    {
        ddlCargo.Items.Clear();

        IList<Cargo> cargos = Cargo.ConsultarTodosOrdemAlfabetica();

        ddlCargo.DataValueField = "Id";
        ddlCargo.DataTextField = "Nome";

        ddlCargo.DataSource = cargos != null ? cargos : new List<Cargo>();
        ddlCargo.DataBind();

        ddlCargo.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void CarregarUsuarios()
    {
        ddlUsuario.Items.Clear();
        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();
        ddlUsuario.DataValueField = "Id";
        ddlUsuario.DataTextField = "NomeRazaoSocial";

        ddlUsuario.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlUsuario.DataBind();
        ddlUsuario.Items.Insert(0, new ListItem("-- Todos --", "0"));
    }

    private void Pesquisar()
    {
        lblStatusPesquisa.Text = string.Empty;
        IList<Permissao> permissoes = Permissao.Filtrar(ddlDepartamento.SelectedValue.ToInt32(), ddlCargo.SelectedValue.ToInt32(), ddlUsuario.SelectedValue.ToInt32(), ddlStatus.SelectedValue);
        rptPermissoes.DataSource = permissoes;
        rptPermissoes.DataBind();

        if (permissoes == null || permissoes.Count <= 0)
            lblStatusPesquisa.Text = "Nenhuma permissão encontrada!";
    }

    #endregion

    #region _______________ Bindins _______________

    private Permissao permissaoAtual;
    public IList<Tela> BindingTelas(object o)
    {
        this.permissaoAtual = (Permissao)o;
        return Tela.ConsultarTodasTelasOrdemPrioridade();
    }

    public IList<Tela> BindingRelatorios(object o)
    {
        this.permissaoAtual = (Permissao)o;
        return Tela.ConsultarTodasTelasRelatoriosOrdemPrioridade(0, 2);
    }

    public IList<Tela> BindingRelatoriosGraficos(object o)
    {
        this.permissaoAtual = (Permissao)o;
        return Tela.ConsultarTodasTelasRelatoriosOrdemPrioridade(0, 1);
    }

    public bool BindingTelaChecked(object o)
    {
        return this.permissaoAtual != null && this.permissaoAtual.Telas != null && this.permissaoAtual.Telas.Contains((Tela)o);
    }

    public string BindSelectValueAdiaPrazoLegalOs(object o)
    {
        Permissao permissao = ((Permissao)o);
        return permissao != null && permissao.AdiaPrazoLegalOS != null ? permissao.AdiaPrazoLegalOS.ToString() : "N";
    }

    public string BindSelectValueAdiaPrazoDiretoriaOs(object o)
    {
        Permissao permissao = ((Permissao)o);
        return permissao != null && permissao.AdiaPrazoDiretoriaOS != null ? permissao.AdiaPrazoDiretoriaOS.ToString() : "N";
    }

    public string BindSelectValueVisualizaControleViagens(object o)
    {
        Permissao permissao = ((Permissao)o);
        return permissao != null && permissao.VisualizaControleViagens != null ? permissao.VisualizaControleViagens.ToString() : "N";
    }

    public string BindSelectValueAprovaOs(object o)
    {
        Permissao permissao = ((Permissao)o);
        return permissao != null ? permissao.AprovaOS.ToString() : "N";
    }

    public string BindSelectValueAcessoOs(object o)
    {
        Permissao permissao = ((Permissao)o);
        return permissao != null ? permissao.AcessaOS.ToString() : "N";
    }

    public string BindSelectValueAprovaDespesa(object o)
    {
        Permissao permissao = ((Permissao)o);
        return permissao != null ? permissao.AprovaDespesa.ToString() : "N";
    }

    #endregion

    #region _______________ Eventos _______________

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            this.Pesquisar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void chkTela_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = ((CheckBox)sender);
            Tela tela = Tela.ConsultarPorId(chk.ValidationGroup.ToInt32());
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)chk.Parent.Parent.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.Telas.Remove(tela);
            if (chk.Checked)
            {
                permissao.Telas.Add(tela);
                msg.CriarMensagem("Permissão adicionada!", "Informação", MsgIcons.Sucesso);
            }
            else
                msg.CriarMensagem("Permissão removida!", "Informação", MsgIcons.Sucesso);
            permissao.Salvar();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void ddlAcessoAsOSs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drop = ((DropDownList)sender);
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)drop.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.AcessaOS = drop.SelectedValue.ToCharArray()[0];
            permissao.Salvar();
            msg.CriarMensagem("Permissão alterada!", "Informação", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }
    protected void ddlAprovaOSs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drop = ((DropDownList)sender);
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)drop.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.AprovaOS = drop.SelectedValue.ToCharArray()[0];
            permissao.Salvar();
            msg.CriarMensagem("Permissão alterada!", "Informação", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }
    protected void ddlAprovaDespesa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drop = ((DropDownList)sender);
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)drop.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.AprovaDespesa = drop.SelectedValue.ToCharArray()[0];
            permissao.Salvar();
            msg.CriarMensagem("Permissão alterada!", "Informação", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }
    protected void ddlAdiaPrazoLegalOSs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drop = ((DropDownList)sender);
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)drop.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.AdiaPrazoLegalOS = drop.SelectedValue.ToCharArray()[0];
            permissao.Salvar();
            msg.CriarMensagem("Permissão alterada!", "Informação", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }
    protected void ddlAdiaPrazoDiretoriaOSs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drop = ((DropDownList)sender);
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)drop.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.AdiaPrazoDiretoriaOS = drop.SelectedValue.ToCharArray()[0];
            permissao.Salvar();
            msg.CriarMensagem("Permissão alterada!", "Informação", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }
    protected void ddlVisualizaControleViagens_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drop = ((DropDownList)sender);
            Permissao permissao = Permissao.ConsultarPorId(((HiddenField)drop.Parent.FindControl("hfIdPermissao")).Value.ToInt32());
            permissao.VisualizaControleViagens = drop.SelectedValue.ToCharArray()[0];
            permissao.Salvar();
            msg.CriarMensagem("Permissão alterada!", "Informação", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void chkTela_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "CheckedChanged", upAux);
    }

    protected void ddlGerenciaOS_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "SelectedIndexChanged", upAux);
    }

    #endregion

}