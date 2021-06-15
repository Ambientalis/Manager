                                                                                                                                  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Visitas_CadastroDeVisita : PageBase
{
    Msg msg = new Msg();

    private Funcionario FuncionarioLogado
    {
        get
        {
            return (Funcionario)Session["usuario_logado"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();

                string idVisita = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));

                string soVisualizacao = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("sovisu", this.Request));                

                if (idVisita.ToInt32() > 0)
                {

                    hfId.Value = idVisita;
                    this.CarregarVisita(idVisita.ToInt32());

                    if (soVisualizacao == "sim")
                        this.DesabilitarBotoesVisita();
                }
                else
                {
                    this.DesabilitarBotoesVisita();
                    msg.CriarMensagem("Visita não foi carregada corretamente. Por favor, volte á Pesquisa de Visitas e tente novamente.", "Erro", MsgIcons.Erro);                    
                    return;
                }
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
        this.CarregarTiposVisita();
        this.CarregarVisitantes();
        this.CarregarVeiculosReserva();
    }

    private void CarregarTiposVisita()
    {
        ddlTipoVisita.Items.Clear();

        ddlTipoVisita.DataValueField = "Id";
        ddlTipoVisita.DataTextField = "Nome";

        IList<TipoVisita> tiposVisitas = TipoVisita.ConsultarTodosOrdemAlfabetica();

        ddlTipoVisita.DataSource = tiposVisitas != null ? tiposVisitas : new List<TipoVisita>();
        ddlTipoVisita.DataBind();

        ddlTipoVisita.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVisitantes()
    {
        ddlVisitante.Items.Clear();

        ddlVisitante.DataValueField = "Id";
        ddlVisitante.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlVisitante.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlVisitante.DataBind();

        ddlVisitante.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVeiculosReserva()
    {
        ddlVeículoReserva.Items.Clear();

        IList<Veiculo> veiculos = Veiculo.ConsultarTodosOrdemAlfabetica();

        if (veiculos != null && veiculos.Count > 0)
        {
            foreach (Veiculo veiculo in veiculos)
            {
                ddlVeículoReserva.Items.Add(new ListItem(veiculo.Descricao + " - " + veiculo.Placa, veiculo.Id.ToString()));
            }
        }

        ddlVeículoReserva.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarVisita(int id)
    {
        Visita visita = Visita.ConsultarPorId(id);

        if (visita != null) 
        {
            chkAtivo.Checked = visita.Ativo;
            tbxDataInicioVisita.Text = visita.DataInicio.ToString("dd/MM/yyyy HH:mm");
            tbxDataFimVisita.Text = visita.DataFim.ToString("dd/MM/yyyy HH:mm");

            OrdemServico ordem = visita.OrdemServico;

            tbxPedidoVisita.Text = ordem != null && ordem.Pedido != null ? ordem.Pedido.Codigo + " - " + ordem.Pedido.Data.ToShortDateString() + " - " + ordem.Pedido.GetDescricaoTipo + " - " + ordem.Pedido.GetNomeCliente : "";

            tbxOSVisita.Text = ordem != null ? ordem.Codigo + " - " + ordem.Data.ToShortDateString() + " - " + ordem.GetDescricaoDepartamento + " - " + ordem.GetDescricaoSetor : "";

            ddlTipoVisita.SelectedValue = visita.TipoVisita != null ? visita.TipoVisita.Id.ToString() : "0";
            tbxDescricaoVisita.Text = visita.Descricao;

            ddlVisitante.SelectedValue = visita.Visitante != null ? visita.Visitante.Id.ToString() : "0";

            //Carregando os detalhamentos
            if (visita.Detalhamentos != null && visita.Detalhamentos.Count > 0)
            {
                detalhamento_edicao_visita.Visible = false;
                detalhamento_visualizacao_visita.Visible = true;

                Detalhamento ultimoDetalhamentoDaVisita = visita.GetUltimoDelhamento;

                tbxVisualizarDetalhamentoVisita.Text = ultimoDetalhamentoDaVisita != null ? ultimoDetalhamentoDaVisita.Conteudo : "";
            }
            else
            {
                detalhamento_edicao_visita.Visible = true;
                detalhamento_visualizacao_visita.Visible = false;
            }

            if (visita.Visitante != null && visita.Visitante.Id != this.FuncionarioLogado.Id)
                this.DesabilitarBotoesVisita();


            ddlVeículoReserva.SelectedValue = visita.Reserva != null && visita.Reserva.Veiculo != null ? visita.Reserva.Veiculo.Id.ToString() : "0";
            tbxDataInicioReserva.Text = visita.Reserva != null ? visita.Reserva.DataInicio.ToString("dd/MM/yyyy HH:mm") : "";
            tbxDataFimReserva.Text = visita.Reserva != null ? visita.Reserva.DataFim.ToString("dd/MM/yyyy HH:mm") : "";

            this.CarregarArvoreArquivosVisita(visita);
        }
    }

    private void DesabilitarBotoesVisita()
    {
        btnEditarDetalhamentosVisita.Enabled = btnEditarDetalhamentosVisita.Visible = btnNovoArquivoVisita.Enabled = btnNovoArquivoVisita.Visible = btnExcluirArquivoVisita.Enabled = btnExcluirArquivoVisita.Visible =
            btnRenomearArquivoVisita.Enabled = btnRenomearArquivoVisita.Visible = btnSalvarVisita.Enabled = btnSalvarVisita.Visible = detalhamento_edicao_visita.Visible = false;
        detalhamento_visualizacao_visita.Visible = true;
    }

    private void CarregarArvoreArquivosVisita(Visita visita)
    {
        TreeNode noSelecionado = trvAnexosVisita.SelectedNode;
        trvAnexosVisita.Nodes.Clear();

        if (visita != null && visita.Id > 0)
        {
            //Adicionando o pedido da visita
            Pedido pedido = visita.GetPedido;

            if (pedido != null)
            {
                TreeNode noPedido = new TreeNode("Pedido " + pedido.Codigo, pedido.Id.ToString());
                noPedido.ImageUrl = "../imagens/icone_pasta.png";

                if (pedido.Arquivos != null && pedido.Arquivos.Count > 0)
                {
                    foreach (Arquivo arquivoPedido in pedido.Arquivos)
                    {
                        TreeNode noArquivoPedido = new TreeNode()
                        {
                            Text = arquivoPedido.Nome,
                            Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                            ImageUrl = "../imagens/icone_tarefa.png"
                        };

                        noPedido.ChildNodes.Add(noArquivoPedido);
                    }
                }

                //Adicionando a OS da visita
                OrdemServico ordem = visita.GetOS;

                if (ordem != null)
                {
                    TreeNode noOS = new TreeNode("OS " + ordem.Codigo, ordem.Id.ToString());
                    noOS.ImageUrl = "../imagens/icone_pasta.png";

                    if (ordem.Arquivos != null && ordem.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivoPedido in ordem.Arquivos)
                        {
                            TreeNode noArquivoPedido = new TreeNode()
                            {
                                Text = arquivoPedido.Nome,
                                Value = "ARQPEDNE_" + arquivoPedido.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noOS.ChildNodes.Add(noArquivoPedido);
                        }
                    }



                    TreeNode noPai = new TreeNode("Visita " + visita.DataInicio.ToShortDateString(), visita.Id.ToString());
                    noPai.ImageUrl = "../imagens/icone_pasta.png";

                    //adicionando arquivos da OS na arvore
                    if (visita.Arquivos != null && visita.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivoPedido in visita.Arquivos)
                        {
                            TreeNode noArquivoPedido = new TreeNode()
                            {
                                Text = arquivoPedido.Nome,
                                Value = "ARQPED_" + arquivoPedido.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noPai.ChildNodes.Add(noArquivoPedido);
                        }
                    }

                    noOS.ChildNodes.Add(noPai);
                    noPedido.ChildNodes.Add(noOS);
                }


                trvAnexosVisita.Nodes.Add(noPedido);

            }
        }

        trvAnexosVisita.ExpandAll();
    }

    private void CarregarHistoricoDetalhamentosVisita()
    {
        Visita vsita = Visita.ConsultarPorId(hfId.Value.ToInt32());

        IList<Detalhamento> detalhamentos = vsita != null && vsita.Detalhamentos != null ? vsita.Detalhamentos.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Esta visita não possui detalhamentos salvos para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void ExcluirArquivosVisita()
    {
        if (trvAnexosVisita.SelectedValue.Contains("ARQPED_"))
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(trvAnexosVisita.SelectedNode.Value.Split('_')[1].ToInt32());

            arquivo.Excluir();

            msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    public string BindConteudoDetalhamento(object o)
    {
        Detalhamento detalhamento = (Detalhamento)o;
        return detalhamento.Conteudo;
    }

    private void SalvarVisita()
    {
        Visita visita = Visita.ConsultarPorId(hfId.Value.ToInt32());

        if (visita == null)
        {
            msg.CriarMensagem("Visita não foi carregada corretamente. Por favor, volte á Pesquisa de Visitas e tente novamente.", "Erro", MsgIcons.Erro);
            return;
        }

        //se  detalhamento esta em modo de edição, salvar um novo detalhamento para a OS
        if (detalhamento_edicao_visita.Visible == true)
        {
            if (visita.Detalhamentos == null)
                visita.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            detalhamento.Conteudo = editDetalhamentoVisita.Content;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            detalhamento = detalhamento.Salvar();

            visita.Detalhamentos.Add(detalhamento);

            tbxVisualizarDetalhamentoVisita.Text = detalhamento.Conteudo;

            detalhamento_edicao_visita.Visible = false;
            detalhamento_visualizacao_visita.Visible = true;
        }

        visita = visita.Salvar();
        hfId.Value = visita.Id.ToString();

        msg.CriarMensagem("Visita salva com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    #endregion

    #region _______________ Trigers _______________

    protected void btnVisualizarDetalhamentosVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnRenomearArquivoVisita_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void Label1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upFormularioVisita);
    }

    protected void UpLoad2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upFormularioVisita);
    }

    protected void btnSalvarRenomearArquivo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioVisita);
    }

    #endregion

    #region _______________ Eventos _______________

    protected void btnVisualizarDetalhamentosVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder visualizar o historico de detalhamentos.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarHistoricoDetalhamentosVisita();
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

    protected void btnEditarDetalhamentosVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder alterar seu detalhamento.", "Informação", MsgIcons.Informacao);
                return;
            }

            Visita visita = Visita.ConsultarPorId(hfId.Value.ToInt32());

            Detalhamento ultimoDetalhamento = visita.GetUltimoDelhamento;

            editDetalhamentoVisita.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

            detalhamento_edicao_visita.Visible = true;
            detalhamento_visualizacao_visita.Visible = false;
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

    protected void btnNovoArquivoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder inserir arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            Session["id_visita_arquivos"] = hfId.Value;

            lblUploadArquivos_ModalPopupExtender.Show();
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

    protected void btnExcluirArquivoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder excluir arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvAnexosVisita.SelectedValue.IsNotNullOrEmpty() || !trvAnexosVisita.SelectedValue.Contains("ARQPED_") || trvAnexosVisita.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da visita para ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirArquivosVisita();
            Transacao.Instance.Recarregar();            
            this.CarregarArvoreArquivosVisita(Visita.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnRenomearArquivoVisita_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a visita para poder renomear arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvAnexosVisita.SelectedValue.IsNotNullOrEmpty() || !trvAnexosVisita.SelectedValue.Contains("ARQPED_") || trvAnexosVisita.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da visita para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            hfIdArquivoRenomear.Value = trvAnexosVisita.SelectedNode.Value.Split('_')[1];
            tbxNovoNomeArquivo.Text = "";

            lblRenomearArquivos_ModalPopupExtender.Show();
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

    protected void btnSalvarVisita_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarVisita();
            Transacao.Instance.Recarregar();
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

    protected void UpLoad2_UpLoadComplete(object sender, EventArgs e)
    {
        try
        {
            Transacao.Instance.Recarregar();

            this.CarregarArvoreArquivosVisita(Visita.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnSalvarRenomearArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(hfIdArquivoRenomear.Value.ToInt32());

            arquivo.Nome = tbxNovoNomeArquivo.Text;

            arquivo = arquivo.Salvar();

            lblRenomearArquivos_ModalPopupExtender.Hide();

            Transacao.Instance.Recarregar();

            this.CarregarArvoreArquivosVisita(Visita.ConsultarPorId(hfId.Value.ToInt32()));            

            msg.CriarMensagem("Arquivo renomeado com sucesso", "Sucesso", MsgIcons.Sucesso);
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

    protected void trvAnexosVisita_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!trvAnexosVisita.SelectedValue.IsNotNullOrEmpty() || !trvAnexosVisita.SelectedValue.Contains("ARQPED"))
                visualizar_arquivos_visita.Visible = false;
            else
            {
                Arquivo arquivo = Arquivo.ConsultarPorId(trvAnexosVisita.SelectedNode.Value.Split('_')[1].ToInt32());

                visualizar_arquivos_visita.Visible = true;

                hplVisualizarArquivosVisita.NavigateUrl = arquivo.UrlImagem;
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

    #endregion    
}