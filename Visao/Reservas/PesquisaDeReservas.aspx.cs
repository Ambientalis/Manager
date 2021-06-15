using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Reservas_PesquisaDeReservas : PageBase
{
    Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
                this.Pesquisar();
            }
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
        tbxDataDe.Text = DateTime.Now.ToShortDateString() + " 00:00";
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

    private void Pesquisar()
    {
        DateTime dataDe = tbxDataDe.Text.IsDate() ? tbxDataDe.Text.ToSqlDateTime() : SqlDate.MinValue;
        DateTime dataAte = tbxDataAte.Text.IsDate() ? tbxDataAte.Text.ToSqlDateTime() : SqlDate.MaxValue;

        IList<Reserva> reservas = Reserva.Filtrar(dataDe, dataAte, tbxDescricao.Text, ddlTipoReserva.SelectedValue.ToInt32(), ddlVeiculo.SelectedValue.ToInt32(), ddlResponsavel.SelectedValue.ToInt32(), Convert.ToChar(ddlStatus.SelectedValue));

        gdvReservas.DataSource = reservas != null ? reservas : new List<Reserva>();
        gdvReservas.DataBind();

        lblStatus.Text = reservas.Count + " Reserva(s) encontrada(s)";
    }

    private void ExcluirReserva(GridViewDeleteEventArgs e)
    {
        Reserva reserva = Reserva.ConsultarPorId(gdvReservas.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (reserva != null)
        {
            if (reserva.Visita != null && reserva.Visita.Id > 0)
            {
                msg.CriarMensagem("Não é possível excluir reservas que sejam de visitas de Ordens de Serviço.", "Informação", MsgIcons.Informacao);
                return;
            }

            string descricaoVeiculo = reserva != null && reserva.Veiculo != null ? reserva.Veiculo.Descricao + " - " + reserva.Veiculo.Placa : "";
            string nomeGestor = reserva != null && reserva.Veiculo != null && reserva.Veiculo.Gestor != null ? reserva.Veiculo.Gestor.NomeRazaoSocial : "";
            string inicioReserva = reserva.DataInicio.ToString("dd/MM/yyyy HH:mm");
            string fimReserva = reserva.DataFim.ToString("dd/MM/yyyy HH:mm");
            string tipoReserva = reserva.TipoReservaVeiculo != null ? reserva.TipoReservaVeiculo.Descricao : "";
            string responsavel = reserva.Responsavel != null ? reserva.Responsavel.NomeRazaoSocial : "";
            string descricao = reserva.Descricao;
            string quilometragem = reserva.Quilometragem.ToString("N2");
            string consumo = reserva.Consumo.ToString("N2");
            string destinatario = reserva != null && reserva.Veiculo != null && reserva.Veiculo.Gestor != null ? reserva.Veiculo.Gestor.EmailCorporativo : "";

            if (reserva.Excluir())
            {
                msg.CriarMensagem("Reserva excluída com sucesso", "Sucesso", MsgIcons.Sucesso);
                this.EnviarEmailExclusaoReservaParaOGestor(descricaoVeiculo, nomeGestor, inicioReserva, fimReserva, tipoReserva, responsavel, descricao, quilometragem, consumo, destinatario);
            }
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.Pesquisar();
    }

    private void EnviarEmailExclusaoReservaParaOGestor(string descricaoVeiculo, string nomeGestor, string inicioReserva, string fimReserva, string tipoReserva, string responsavel, string descricao, string quilometragem, string consumo, string destinatario)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Exclusão de Reserva de Veículo";
        mail.BodyHtml = true;

        string conteudoemail = @"<div style='font-family: Arial, Helvetica, sans-serif; font-size: 14pt; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Exclusão de Reserva de Veículo</div>
    <div style='margin-top:15px; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        A reserva para o Veículo " + descricaoVeiculo + (nomeGestor != "" ? ", gerido pelo usuário " + nomeGestor : "") + @", agendada para " + inicioReserva + @", foi excluída do Sistema Ambientalis Manager.<br /><br />  
        Dados da Reserva:      
        <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Início:</strong>
                    </div>
                    <div>
                        " + inicioReserva + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div>
                        <strong>Data de Fim:</strong>
                    </div>
                    <div>
                        " + fimReserva + @"
                    </div>
                    
                </td>                
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Veículo:</strong>
                    </div>
                    <div>
                        " + descricaoVeiculo + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Tipo de Reserva:</strong>
                    </div>
                    <div>
                        " + tipoReserva + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Responsável:</strong>
                    </div>
                    <div>
                        " + responsavel + @"
                    </div>                    
                </td>
            </tr>
            <tr>
                <td align='left' colspan='2'>
                    <div style='margin-top:10px;'>
                        <strong>Descrição:</strong>
                    </div>
                    <div>
                        " + descricao + @"
                    </div>                    
                </td>
            </tr>            
            <tr>
                <td align='left' style='width:50%;'>
                    <div style='margin-top:10px;'>
                        <strong>Quilomêtragem (KM):</strong>
                    </div>
                    <div>
                        " + quilometragem + @"
                    </div>                    
                </td>
                <td align='left' style='width:50%;'>
                    <div style='margin-top:10px;'>
                        <strong>Consumo (Lts):</strong>
                    </div>
                    <div>
                        " + consumo + @"
                    </div>                    
                </td>                
            </tr>                         
        </table>              
    </div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Exclusão de Reserva de Veículo", conteudoemail);

        if (destinatario != null && destinatario != "")
            mail.AdicionarDestinatario(destinatario);

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Exclusão de reserva de veículo", this.FuncionarioLogado, null, 587, false);
    }

    public string BindEditar(object o)
    {
        Reserva n = (Reserva)o;
        return "CadastroDeReserva.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id);
    }

    public bool BindingVisivelEditReserva(object o)
    {
        Reserva n = (Reserva)o;

        if (n.Responsavel != null && n.Responsavel.Id == WebUtil.FuncionarioLogado.Id)
            return true;

        if (n.Veiculo != null && n.Veiculo.Gestor != null && n.Veiculo.Gestor.Id == WebUtil.FuncionarioLogado.Id)
            return true;

        return false;
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

    protected void gdvReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvReservas.PageIndex = e.NewPageIndex;
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

    protected void gdvReservas_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvReservas.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void gdvReservas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirReserva(e);
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

    protected void btnGridExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a esta reserva serão perdidos. Deseja realmente excluir ?");
    }

    protected void ddlPaginacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gdvReservas.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    #endregion
}