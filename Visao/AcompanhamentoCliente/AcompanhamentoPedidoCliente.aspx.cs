using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;
using Utilitarios.Criptografia;

public partial class AcompanhamentoCliente_AcompanhamentoPedidoCliente : System.Web.UI.Page
{
    private Msg msg = new Msg();

    private Cliente GetClienteLogado
    {
        get
        {
            return Session["usuario_cliente_logado"] == null ? null : (Cliente)Session["usuario_cliente_logado"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
        if (!Request.Browser.Crawler && !IsPostBack)
            try
            {                
                this.SetIdConfig();

                if (Session["idConfig"] == null || string.IsNullOrEmpty(Session["idConfig"].ToString()))
                    Response.Redirect("../Account/LoginCliente.aspx" + Seguranca.MontarParametros("codigoEmpresa=" + hfIdEmpresa.Value.ToString()) + "&page=" + this.Request.Url.AbsoluteUri);
                else
                    hfIdEmpresa.Value = Session["idConfig"].ToString();

                Transacao.Instance.Abrir();

                //Usuário Funcionário
                if (this.GetClienteLogado == null)
                    Response.Redirect("../Account/LoginCliente.aspx" + Seguranca.MontarParametros("codigoEmpresa=" + hfIdEmpresa.Value.ToString()) + "&page=" + this.Request.Url.AbsoluteUri);
                else
                {
                    lblDadosUsuarioLogado.Text = "Usuário: " + this.GetClienteLogado.Login + " - Cliente: " + this.GetClienteLogado.NomeRazaoSocial;
                    this.CarregarPedidosDoCliente();                    
                }

            }
            catch (Exception ex)
            {
                msg.CriarMensagem(ex);
            }
            finally
            {
                Transacao.Instance.Fechar(ref msg);
                C2MessageBox.Show(msg);
            }

        
    }

    
    #region ______________ Métodos _________________

    private void SetIdConfig()
    {
        Session["idConfig"] = ConfigurationManager.AppSettings["idConfig"];
    }

    private void CarregarPedidosDoCliente()
    {
        IList<Pedido> pedidos = Pedido.ConsultarPedidosDoClienteParaAcompanhamento(this.GetClienteLogado.ConsultarPorId().Id, ckbVisualizarPedidosOSAbertas.Checked);

        rptPedidos.DataSource = pedidos != null && pedidos.Count > 0 ? pedidos : new List<Pedido>();
        rptPedidos.DataBind();

        lblNenhumPedidoParaEsteCliente.Visible = pedidos == null || pedidos.Count == 0;
    }

    public IList<Atividade> BindAtividades(object o) 
    {
        OrdemServico ordem = (OrdemServico)o;
        return ordem != null && ordem.Atividades != null && ordem.Atividades.Count > 0 ? ordem.Atividades.OrderByDescending(x => x.Data).ToList() : new List<Atividade>();
    }

    public IList<Visita> BindVisitas(object o)
    {
        OrdemServico ordem = (OrdemServico)o;
        return ordem != null && ordem.Visitas != null && ordem.Visitas.Count > 0 ? ordem.Visitas.OrderByDescending(x => x.DataInicio).ToList() : new List<Visita>();
    }

    public IList<OrdemServico> BindOrdensServico(object o)
    {
        Pedido pedido = (Pedido)o;

        IList<OrdemServico> ordens = pedido != null && pedido.OrdensServico != null && pedido.OrdensServico.Count > 0 ? pedido.OrdensServico : new List<OrdemServico>();

        if (ckbVisualizarPedidosOSAbertas.Checked && ordens != null && ordens.Count > 0)
            ordens = ordens.Where(x => x.Ativo && x.DataEncerramento == SqlDate.MinValue).ToList();

        return ordens != null && ordens.Count > 0 ? ordens : new List<OrdemServico>();
    }

    public string BindDescricaoProjeto(object o)
    {
        Pedido pedido = (Pedido)o;
        return pedido != null ? "Pedido: " + pedido.Codigo + ", Data: " + pedido.Data.ToShortDateString() + ", Descrição: " + pedido.GetDescricaoTipo : "";
    }

    #endregion

    #region ______________ Eventos _________________

    protected void ckbVisualizarPedidosOSAbertas_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Transacao.Instance.Abrir();
            this.CarregarPedidosDoCliente();
        }
        catch (Exception ex)
        {
            msg.CriarMensagem(ex);
        }
        finally
        {
            Transacao.Instance.Fechar(ref msg);
            C2MessageBox.Show(msg);
        }
    }

    protected void gdvOrdensServico_PreRender(object sender, EventArgs e)
    {
        GridView gdOrdens = (GridView)(sender);

        GridViewRow pager = gdOrdens.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }    

    protected void gdvOrdensServico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex > -1)
        //{
        //    GridView gdvAtv = ((GridView)e.Row.FindControl("gdvAtividades"));

        //    if (gdvAtv.HeaderRow != null)
        //        gdvAtv.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    if (gdvAtv.FooterRow != null)
        //        gdvAtv.FooterRow.TableSection = TableRowSection.TableFooter;


        //    GridView gdvVisit = ((GridView)e.Row.FindControl("gdvVisitas"));

        //    if (gdvVisit.HeaderRow != null)
        //        gdvVisit.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    if (gdvVisit.FooterRow != null)
        //        gdvVisit.FooterRow.TableSection = TableRowSection.TableFooter;
        //}
    }

    #endregion


}