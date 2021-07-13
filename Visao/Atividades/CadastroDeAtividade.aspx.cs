using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Atividades_CadastroDeAtividade : PageBase
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

                string idAtividade = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));

                string soVisualizacao = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("sovisu", this.Request));

                if (idAtividade.ToInt32() > 0)
                {

                    hfId.Value = idAtividade;
                    this.CarregarAtividade(idAtividade.ToInt32());

                    if (soVisualizacao == "sim")
                        this.DesabilitarBotoesAtividade();
                }
                else
                {
                    this.DesabilitarBotoesAtividade();
                    msg.CriarMensagem("Atividade não foi carregada corretamente. Por favor, volte á Pesquisa de Atividades e tente novamente.", "Erro", MsgIcons.Erro);
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
        this.CarregarTiposAtividade();
        this.CarregarExecutores();
    }

    private void CarregarTiposAtividade()
    {
        ddlTipoAtividade.Items.Clear();

        ddlTipoAtividade.DataValueField = "Id";
        ddlTipoAtividade.DataTextField = "Nome";

        IList<TipoAtividade> tiposAtividades = TipoAtividade.ConsultarTodosOrdemAlfabetica();

        ddlTipoAtividade.DataSource = tiposAtividades != null ? tiposAtividades : new List<TipoAtividade>();
        ddlTipoAtividade.DataBind();

        ddlTipoAtividade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarExecutores()
    {
        ddlExecutorAtividade.Items.Clear();

        ddlExecutorAtividade.DataValueField = "Id";
        ddlExecutorAtividade.DataTextField = "NomeRazaoSocial";

        IList<Funcionario> funcionarios = Funcionario.ConsultarTodosOrdemAlfabetica();

        ddlExecutorAtividade.DataSource = funcionarios != null ? funcionarios : new List<Funcionario>();
        ddlExecutorAtividade.DataBind();

        ddlExecutorAtividade.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void DesabilitarBotoesAtividade()
    {
        btnEditarDetalhamentosAtividade.Enabled = btnEditarDetalhamentosAtividade.Visible = btnNovoArquivoAtividade.Enabled = btnNovoArquivoAtividade.Visible = btnExcluirArquivoAtividade.Enabled =
            btnExcluirArquivoAtividade.Visible = btnRenomearArquivoAtividade.Enabled = btnRenomearArquivoAtividade.Visible = edicao_detalhamento_atividade.Visible = btnSalvarAtividade.Enabled = btnSalvarAtividade.Visible = false;
        visualizacao_detalhamento_atividade.Visible = true;
    }

    private void CarregarAtividade(int id)
    {
        Atividade atividade = Atividade.ConsultarPorId(id);

        if (atividade != null) 
        {
            hfId.Value = atividade.Id.ToString();
            chkAtivo.Checked = atividade.Ativo;
            tbxDataAtividade.Text = atividade.Data.ToShortDateString();

            OrdemServico ordem = atividade.OrdemServico;

            tbxOSAtividade.Text = ordem != null ? ordem.Codigo + " - " + ordem.Data.ToShortDateString() + " - " + ordem.GetDescricaoDepartamento + " - " + ordem.GetDescricaoSetor : "";

            tbxPedidoAtividade.Text = ordem != null && ordem.Pedido != null ? ordem.Pedido.Codigo + " - " + ordem.Pedido.Data.ToShortDateString() + " - " + ordem.Pedido.GetDescricaoTipo + " - " + ordem.Pedido.GetNomeCliente : "";

            ddlTipoAtividade.SelectedValue = atividade.TipoAtividade != null ? atividade.TipoAtividade.Id.ToString() : "0";

            tbxDescricaoAtividade.Text = atividade.Descricao;

            ddlExecutorAtividade.SelectedValue = atividade.Executor != null ? atividade.Executor.Id.ToString() : "0";

            if (atividade.Executor != null && atividade.Executor.Id != this.FuncionarioLogado.Id)
                this.DesabilitarBotoesAtividade();

            //Carregando os detalhamentos
            if (atividade.Detalhamentos != null && atividade.Detalhamentos.Count > 0)
            {
                edicao_detalhamento_atividade.Visible = false;
                visualizacao_detalhamento_atividade.Visible = true;

                Detalhamento ultimoDetalhamentoDaVisita = atividade.GetUltimoDelhamento;

                tbxVisualizarDetalhamentoAtividade.Text = ultimoDetalhamentoDaVisita != null ? ultimoDetalhamentoDaVisita.Conteudo : "";
            }
            else
            {
                edicao_detalhamento_atividade.Visible = true;
                visualizacao_detalhamento_atividade.Visible = false;
            }

            this.CarregarArvoreArquivosAtividade(atividade);
        }
    }

    private void CarregarArvoreArquivosAtividade(Atividade atividade)
    {
        TreeNode noSelecionado = trvArquivosAtividade.SelectedNode;
        trvArquivosAtividade.Nodes.Clear();

        if (atividade != null && atividade.Id > 0)
        {
            //Adicionando o pedido da visita
            Pedido pedido = atividade.GetPedido;

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
                OrdemServico ordem = atividade.GetOS;

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

                    TreeNode noPai = new TreeNode("Atividade " + atividade.Data.ToShortDateString(), atividade.Id.ToString());
                    noPai.ImageUrl = "../imagens/icone_pasta.png";

                    //adicionando arquivos da Atividade na arvore
                    if (atividade.Arquivos != null && atividade.Arquivos.Count > 0)
                    {
                        foreach (Arquivo arquivo in atividade.Arquivos)
                        {
                            TreeNode noArquivo = new TreeNode()
                            {
                                Text = arquivo.Nome,
                                Value = "ARQPED_" + arquivo.Id.ToString(),
                                ImageUrl = "../imagens/icone_tarefa.png"
                            };

                            noPai.ChildNodes.Add(noArquivo);
                        }
                    }

                    noOS.ChildNodes.Add(noPai);
                    noPedido.ChildNodes.Add(noOS);
                }

                trvArquivosAtividade.Nodes.Add(noPedido);
            }
        }

        trvArquivosAtividade.ExpandAll();
    }

    private void CarregarHistoricoDetalhamentosAtividade()
    {
        Atividade atividade = Atividade.ConsultarPorId(hfId.Value.ToInt32());

        IList<Detalhamento> detalhamentos = atividade != null && atividade.Detalhamentos != null ? atividade.Detalhamentos.OrderByDescending(x => x.DataSalvamento).ToList() : new List<Detalhamento>();

        if (detalhamentos == null || detalhamentos.Count == 0)
        {
            msg.CriarMensagem("Esta atividade não possui detalhamentos salvos para poder visualizar", "Informação", MsgIcons.Informacao);
            return;
        }

        rptHistoricoDetalhamentos.DataSource = detalhamentos;
        rptHistoricoDetalhamentos.DataBind();

        lblHistoricoDetalhamentos_ModalPopupExtender.Show();
    }

    private void ExcluirArquivosAtividade()
    {
        if (trvArquivosAtividade.SelectedValue.Contains("ARQPED_"))
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosAtividade.SelectedNode.Value.Split('_')[1].ToInt32());

            arquivo.Excluir();

            msg.CriarMensagem("Arquivo excluído com sucesso.", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void SalvarAtividade()
    {
        Atividade atividade = Atividade.ConsultarPorId(hfId.Value.ToInt32());

        if (atividade == null)
        {
            msg.CriarMensagem("Atividade não foi carregada corretamente. Por favor, volte á Pesquisa de Atividades e tente novamente.", "Erro", MsgIcons.Erro);
            return;
        }

        //se  detalhamento esta em modo de edição, salvar um novo detalhamento para a OS
        if (edicao_detalhamento_atividade.Visible == true)
        {
            if (atividade.Detalhamentos == null)
                atividade.Detalhamentos = new List<Detalhamento>();

            Detalhamento detalhamento = new Detalhamento();
            detalhamento.DataSalvamento = DateTime.Now;
            detalhamento.Conteudo = editDetalhamentoAtividade.Content;
            detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;

            detalhamento = detalhamento.Salvar();

            atividade.Detalhamentos.Add(detalhamento);

            tbxVisualizarDetalhamentoAtividade.Text = detalhamento.Conteudo;

            edicao_detalhamento_atividade.Visible = false;
            visualizacao_detalhamento_atividade.Visible = true;
        }

        atividade = atividade.Salvar();
        hfId.Value = atividade.Id.ToString();

        msg.CriarMensagem("Atividade salva com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    public string BindConteudoDetalhamento(object o)
    {
        Detalhamento detalhamento = (Detalhamento)o;
        return detalhamento.Conteudo;
    }

    #endregion

    #region _______________ Trigers _______________

    protected void btnVisualizarDetalhamentosAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upHistoricosDetalhamentos);
    }

    protected void btnRenomearArquivoAtividade_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upRenomearArquivo);
    }

    protected void btnSalvarRenomearArquivo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioAtividade);
    }

    protected void Label1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upFormularioAtividade);
    }

    protected void UpLoad2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upFormularioAtividade);
    }

    #endregion

    #region _______________ Eventos _______________

    protected void btnVisualizarDetalhamentosAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder visualizar o historico de detalhamentos.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.CarregarHistoricoDetalhamentosAtividade();
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

    protected void btnEditarDetalhamentosAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder alterar seu detalhamento.", "Informação", MsgIcons.Informacao);
                return;
            }

            Atividade atividade = Atividade.ConsultarPorId(hfId.Value.ToInt32());

            Detalhamento ultimoDetalhamento = atividade.GetUltimoDelhamento;

            editDetalhamentoAtividade.Content = ultimoDetalhamento != null ? ultimoDetalhamento.Conteudo : "";

            edicao_detalhamento_atividade.Visible = true;
            visualizacao_detalhamento_atividade.Visible = false;
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

    protected void btnNovoArquivoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder inserir arquivos na mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            Session["id_atividade_arquivos"] = hfId.Value;

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

    protected void btnExcluirArquivoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder excluir arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosAtividade.SelectedValue.IsNotNullOrEmpty() || !trvArquivosAtividade.SelectedValue.Contains("ARQPED_") || trvArquivosAtividade.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da atividade para ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirArquivosAtividade();
            Transacao.Instance.Recarregar();
            this.CarregarArvoreArquivosAtividade(Atividade.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnRenomearArquivoAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro a atividade para poder renomear arquivos da mesma.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!trvArquivosAtividade.SelectedValue.IsNotNullOrEmpty() || !trvArquivosAtividade.SelectedValue.Contains("ARQPED_") || trvArquivosAtividade.SelectedValue.Contains("ARQPEDNE_"))
            {
                msg.CriarMensagem("Selecione um arquivo da atividade para poder renomeá-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            hfIdArquivoRenomear.Value = trvArquivosAtividade.SelectedNode.Value.Split('_')[1];
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

    protected void btnSalvarAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarAtividade();
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

    protected void btnSalvarRenomearArquivo_Click(object sender, EventArgs e)
    {
        try
        {
            Arquivo arquivo = Arquivo.ConsultarPorId(hfIdArquivoRenomear.Value.ToInt32());

            arquivo.Nome = tbxNovoNomeArquivo.Text;

            arquivo = arquivo.Salvar();

            lblRenomearArquivos_ModalPopupExtender.Hide();

            Transacao.Instance.Recarregar();

            this.CarregarArvoreArquivosAtividade(Atividade.ConsultarPorId(hfId.Value.ToInt32()));

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

    protected void UpLoad2_UpLoadComplete(object sender, EventArgs e)
    {
        try
        {
            Transacao.Instance.Recarregar();

            this.CarregarArvoreArquivosAtividade(Atividade.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void trvArquivosAtividade_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (!trvArquivosAtividade.SelectedValue.IsNotNullOrEmpty() || !trvArquivosAtividade.SelectedValue.Contains("ARQPED"))
                visualizar_arquivos.Visible = false;
            else
            {
                Arquivo arquivo = Arquivo.ConsultarPorId(trvArquivosAtividade.SelectedNode.Value.Split('_')[1].ToInt32());

                visualizar_arquivos.Visible = true;

                hplVisualizarArquivo.NavigateUrl = arquivo.UrlImagem;
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