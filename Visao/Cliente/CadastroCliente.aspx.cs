using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Persistencia.Fabrica;
using SisWebControls;
using Utilitarios;

public partial class Cliente_CadastroCliente : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
                this.NovoCliente();

                string idCliente = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                if (idCliente.ToInt32() > 0)
                    this.CarregarCliente(idCliente.ToInt32());
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
        if (Request["frn"] != null && Request["frn"].Trim().Equals("true"))
        {
            hfTipoCarregado.Value = "2";
            lblTituloTela.Text = "Cadastro de fornecedores";
        }

        ckRecebeLoginSenhaPessoaFisica.Visible = ckRecebeNotificacaoPessoaFisica.Visible =
            ckRecebeLoginSenhaPessoaJuridica.Visible = ckRecebeNotificacaoPessoaJuridica.Visible =
                ckContato1RecebeLoginSenha.Visible = ckContato1RecebeNotificacoes.Visible =
                ckContato2RecebeLoginSenha.Visible = ckContato2RecebeNotificacoes.Visible =
                ckContato3RecebeLoginSenha.Visible = ckContato3RecebeNotificacoes.Visible =
                pnlUsuarioParaAcompanhamento.Visible = (Request["frn"] == null);

        this.CarregarOrigens();
        this.CarregarUnidades();
        this.CarregarEstados(ddlEstadoEndereco);
        ddlCidadeEndereco.Items.Clear();
        ddlCidadeEndereco.Items.Insert(0, new ListItem("-- Selecione primeiro o estado --", "0"));
    }

    private void CarregarOrigens()
    {
        ddlOrigem.Items.Clear();
        ddlOrigem.Items.Add(new ListItem("-- Todos --", "0"));
        IList<Origem> origens = Origem.ConsultarTodosOrdemAlfabetica();
        if (origens != null)
            foreach (Origem or in origens)
                ddlOrigem.Items.Add(new ListItem(or.Nome, or.Id.ToString()));
        ddlOrigem.SelectedIndex = 0;
    }

    private void CarregarUnidades()
    {
        Unidade uni = Unidade.ConsultarPorId(Session["idEmp"].ToString().ToInt32());
        txtUnidade.Text = (uni != null ? uni.Descricao : "Não especificada");
    }

    private void CarregarEstados(DropDownList dropEstado)
    {
        dropEstado.DataValueField = "Id";
        dropEstado.DataTextField = "Nome";
        dropEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        dropEstado.DataBind();

        dropEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void NovoCliente()
    {
        tbxCodigoCliente.Text = "Gerado automaticamente";
        tbxLogin.Text = tbxSenha.Text = "Gerado automaticamente...";
        chkAtivo.Checked = ckRecebeLoginSenhaPessoaFisica.Checked = ckRecebeLoginSenhaPessoaJuridica.Checked = ckRecebeNotificacaoPessoaJuridica.Checked = ckRecebeNotificacaoPessoaFisica.Checked =
        ckContato1RecebeNotificacoes.Checked = ckContato1RecebeLoginSenha.Checked = ckContato2RecebeNotificacoes.Checked = ckContato2RecebeLoginSenha.Checked = ckContato3RecebeNotificacoes.Checked =
        ckContato3RecebeLoginSenha.Checked = true;

        tbxNomePessoaFisica.Text = tbxApelidoPessoaFisica.Text = tbxCPFPessoaFisica.Text = tbxRgPessoaFisica.Text = tbxOrgaoEmissorPessoaFisica.Text = tbxNacionalidadePessoaFisica.Text = tbxDataNascimentoPessoaFisica.Text =
            tbxEstadoCivilPessoaFisica.Text = tbxInscricaoEstadualPessoaFisica.Text = tbxInscricaoMunicipalPessoaFisica.Text = tbxTelefone1PessoaFisica.Text = tbxTelefone2PessoaFisica.Text = tbxEmailPessoaFisica.Text =
            txtObservacoes.Text = tbxRazaoSocialPessoaJuridica.Text = tbxNomeFantasiaPessoaJuridica.Text = tbxCNPJPessoaJuridica.Text = tbxInscricaoEstadualPessoaJuridica.Text = tbxInscricaoMunicipalPessoaJuridica.Text =
            tbxTelefone1PessoaJuridica.Text = tbxTelefone2PessoaJuridica.Text = tbxEmailPessoaJuridica.Text = tbxLogradouroEndereco.Text =
            tbxNumeroEndereco.Text = tbxBairroEndereco.Text = tbxComplementoEndereco.Text = tbxReferenciaEndereco.Text = tbxCepEndereco.Text = tbxNomeContato1.Text = tbxFuncaoContato1.Text = tbxPrimeiroTelefoneContato1.Text =
            tbxSegundoTelefoneContato1.Text = tbxEmailContato1.Text = tbxNomeContato2.Text = tbxFuncaoContato2.Text = tbxPrimeiroTelefoneContato2.Text = tbxSegundoTelefoneContato2.Text = tbxEmailContato2.Text =
            tbxNomeContato3.Text = tbxFuncaoContato3.Text = tbxPrimeiroTelefoneContato3.Text = tbxSegundoTelefoneContato3.Text = tbxEmailContato3.Text = hfId.Value = hfSenhaEdicao.Value = "";

        this.CarregarCampos();

        this.RecarregarImagemCliente();

        usuario_alteracao.Visible = false;
        usuario_novo.Visible = true;

        btnEnviarLoginSenha.Visible = false;

    }

    private void CarregarCidades()
    {
        ddlCidadeEndereco.Items.Clear();
        Estado es = Estado.ConsultarPorId(ddlEstadoEndereco.SelectedValue.ToInt32());
        ddlCidadeEndereco.DataTextField = "Nome";
        ddlCidadeEndereco.DataValueField = "Id";
        ddlCidadeEndereco.DataSource = es != null ? es.Cidades : new List<Cidade>();
        ddlCidadeEndereco.DataBind();
        ddlCidadeEndereco.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void ExcluirCliente()
    {
        Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());
        if (cliente != null)
        {
            if (cliente.Pedidos != null && cliente.Pedidos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir pois há pedidos cadastrados para esta pessoa. Caso queira retirar das listagens, basta desativar.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (cliente.Excluir())
                msg.CriarMensagem("Exclusão realizada com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.NovoCliente();
    }

    private void CarregarCliente(int id)
    {
        Cliente cliente = Cliente.ConsultarPorId(id);

        if (cliente != null)
        {
            Session["idCliente"] = hfId.Value = cliente.Id.ToString();
            chkAtivo.Checked = cliente.Ativo;
            tbxCodigoCliente.Text = cliente.Codigo;
            tbxCodigoCliente.Enabled = cliente.Codigo.Contains("CM");
            hfTipoCarregado.Value = (cliente.EhFornecedor ? "2" : "1");
            if (cliente.Origem != null)
                ddlOrigem.SelectedValue = cliente.Origem.Id.ToString();
            rbtnTipoPessoa.SelectedValue = cliente.Tipo.ToString();
            if (cliente.Tipo == Cliente.FISICA)
            {
                tbxNomePessoaFisica.Text = cliente.NomeRazaoSocial;
                tbxApelidoPessoaFisica.Text = cliente.ApelidoNomeFantasia;
                tbxCPFPessoaFisica.Text = cliente.GetCPFCNPJComMascara;
                tbxRgPessoaFisica.Text = cliente.Rg;
                tbxOrgaoEmissorPessoaFisica.Text = cliente.EmissorRg;
                tbxNacionalidadePessoaFisica.Text = cliente.Nacionalidade;
                tbxDataNascimentoPessoaFisica.Text = cliente.DataNascimento != SqlDateTime.MinValue.Value ? cliente.DataNascimento.EmptyToMinValueSQL() : "";
                tbxEstadoCivilPessoaFisica.Text = cliente.EstadoCivil;
                ddlSexoPessoaFisica.SelectedValue = cliente.Sexo.ToString();
                tbxInscricaoEstadualPessoaFisica.Text = cliente.InscricaoEstadual;
                chkIsentoPessoaFisica.Checked = cliente.IsentoICMS;
                tbxInscricaoMunicipalPessoaFisica.Text = cliente.InscricaoMunicipal;

                tbxTelefone1PessoaFisica.Text = cliente.Telefone1;
                tbxTelefone2PessoaFisica.Text = cliente.Telefone2;
                tbxEmailPessoaFisica.Text = cliente.Email;
                ckRecebeNotificacaoPessoaFisica.Checked = cliente.EmailRecebeNotificacoes;
                ckRecebeLoginSenhaPessoaFisica.Checked = cliente.EmailRecebeLoginSenha;
            }
            else
            {
                tbxRazaoSocialPessoaJuridica.Text = cliente.NomeRazaoSocial;
                tbxNomeFantasiaPessoaJuridica.Text = cliente.ApelidoNomeFantasia;
                tbxCNPJPessoaJuridica.Text = cliente.GetCPFCNPJComMascara;

                tbxInscricaoEstadualPessoaJuridica.Text = cliente.InscricaoEstadual;
                chkIsentoPessoaJuridica.Checked = cliente.IsentoICMS;
                tbxInscricaoMunicipalPessoaJuridica.Text = cliente.InscricaoMunicipal;

                tbxTelefone1PessoaJuridica.Text = cliente.Telefone1;
                tbxTelefone2PessoaJuridica.Text = cliente.Telefone2;
                tbxEmailPessoaJuridica.Text = cliente.Email;
                ckRecebeNotificacaoPessoaJuridica.Checked = cliente.EmailRecebeNotificacoes;
                ckRecebeLoginSenhaPessoaJuridica.Checked = cliente.EmailRecebeLoginSenha;
            }

            txtObservacoes.Text = cliente.Observacoes;
            this.CarregarClassificacao(cliente.GetUltimaClassificacao);
            //tbxDescricaoEndereco.Text = cliente.GetDescricaoEndereco;
            tbxLogradouroEndereco.Text = cliente.GetLogradouroEndereco;
            tbxNumeroEndereco.Text = cliente.GetNumeroEndereco;
            tbxBairroEndereco.Text = cliente.GetBairroEndereco;
            tbxComplementoEndereco.Text = cliente.GetComplementoEndereco;
            tbxReferenciaEndereco.Text = cliente.GetReferenciaEndereco;
            tbxCepEndereco.Text = cliente.GetCepEndereco;

            ddlEstadoEndereco.SelectedValue = cliente.GetEstadoEndereco != null ? cliente.GetEstadoEndereco.Id.ToString() : "0";
            this.CarregarCidades();
            ddlCidadeEndereco.SelectedValue = cliente.GetCidadeEndereco != null ? cliente.GetCidadeEndereco.Id.ToString() : "0";

            Contato contato1 = cliente.Contatos != null && cliente.Contatos.Count > 0 ? cliente.Contatos[0] : null;

            if (contato1 != null)
            {
                tbxNomeContato1.Text = contato1.Nome;
                tbxFuncaoContato1.Text = contato1.Funcao;
                tbxPrimeiroTelefoneContato1.Text = contato1.Telefone1;
                tbxSegundoTelefoneContato1.Text = contato1.Telefone2;
                tbxEmailContato1.Text = contato1.Email;
                txtNascimentoContato1.Text = contato1.DataNascimento.EmptyToMinValueSQL();
                ckContato1RecebeNotificacoes.Checked = contato1.RecebeNotificacoes;
                ckContato1RecebeLoginSenha.Checked = contato1.EmailRecebeLoginSenha;
            }

            Contato contato2 = cliente.Contatos != null && cliente.Contatos.Count > 0 ? cliente.Contatos[1] : null;

            if (contato2 != null)
            {
                tbxNomeContato2.Text = contato2.Nome;
                tbxFuncaoContato2.Text = contato2.Funcao;
                tbxPrimeiroTelefoneContato2.Text = contato2.Telefone1;
                tbxSegundoTelefoneContato2.Text = contato2.Telefone2;
                tbxEmailContato2.Text = contato2.Email;
                txtNascimentoContato2.Text = contato2.DataNascimento.EmptyToMinValueSQL();
                ckContato2RecebeNotificacoes.Checked = contato2.RecebeNotificacoes;
                ckContato2RecebeLoginSenha.Checked = contato2.EmailRecebeLoginSenha;
            }

            Contato contato3 = cliente.Contatos != null && cliente.Contatos.Count > 0 ? cliente.Contatos[2] : null;

            if (contato3 != null)
            {
                tbxNomeContato3.Text = contato3.Nome;
                tbxFuncaoContato3.Text = contato3.Funcao;
                tbxPrimeiroTelefoneContato3.Text = contato3.Telefone1;
                tbxSegundoTelefoneContato3.Text = contato3.Telefone2;
                tbxEmailContato3.Text = contato3.Email;
                txtNascimentoContato3.Text = contato3.DataNascimento.EmptyToMinValueSQL();
                ckContato3RecebeNotificacoes.Checked = contato3.RecebeNotificacoes;
                ckContato3RecebeLoginSenha.Checked = contato3.EmailRecebeLoginSenha;
            }

            usuario_alteracao.Visible = true;
            usuario_novo.Visible = false;

            tbxLoginEdicao.Text = cliente.Login != null && cliente.Login != "" ? cliente.Login : "";
            hfSenhaEdicao.Value = cliente.Senha != null && cliente.Senha != "" ? Utilitarios.Criptografia.Criptografia.Decrypt(cliente.Senha, true) : "";

            this.RecarregarImagemCliente();

            btnEnviarLoginSenha.Visible = cliente.EhCliente && cliente.EmailRecebeLoginSenha;
        }
    }

    private void CarregarClassificacao(ClassificacaoCliente classificacao)
    {
        hfRating.Value = "0";
        txtDataClassificacao.Text = string.Empty;

        if (classificacao != null)
        {
            hfRating.Value = classificacao.Classificacao.ToString();
            txtDataClassificacao.Text = classificacao.Data.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }

    private void SalvarClassificacao(Cliente cliente)
    {
        ClassificacaoCliente classificacao = new ClassificacaoCliente();
        classificacao.Cliente = cliente;
        classificacao.Data = DateTime.Now;
        classificacao.Classificacao = hfRating.Value.ToInt32();
        cliente.Classificacoes.Add(classificacao.Salvar());
        this.CarregarClassificacao(classificacao);
    }

    private void RecarregarImagemCliente()
    {
        Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());

        if (rbtnTipoPessoa.SelectedIndex == 0)
            imgPessoaFisica.ImageUrl = cliente != null && cliente.Imagem != null ? cliente.Imagem.UrlImagem : "~/Utilitarios/Imagens/FotosIndisponiveis/FotoIndisponivelAlbum.png";
        else
            imgPessoaJuridica.ImageUrl = cliente != null && cliente.Imagem != null ? cliente.Imagem.UrlImagem : "~/Utilitarios/Imagens/FotosIndisponiveis/FotoIndisponivelAlbum.png";
    }

    private void ExcluirImagemCliente()
    {
        Cliente c = Cliente.ConsultarPorId(hfId.Value.ToInt32());
        c.Imagem = null;
        c = c.Salvar();
        this.RecarregarImagemCliente();
    }

    private void SalvarCliente()
    {
        if (tbxCodigoCliente.Enabled && Cliente.ExisteClienteDiferenteDesseIdComEsteCodigo(tbxCodigoCliente.Text, hfId.Value.ToInt32()))
        {
            msg.CriarMensagem("Código já cadastrado. Informe outro código para continuar.", "Informação", MsgIcons.Informacao);
            return;
        }
        //Primeiro contato
        if (tbxEmailContato1.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailContato1.Text))
        {
            msg.CriarMensagem("Informe um e-mail válido para o primeiro contato.", "Informação", MsgIcons.Informacao);
            return;
        }
        //Segundo contato
        if (tbxEmailContato2.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailContato2.Text))
        {
            msg.CriarMensagem("Informe um e-mail válido para o segundo contato.", "Informação", MsgIcons.Informacao);
            return;
        }
        //Terceiro contato
        if (tbxEmailContato3.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailContato3.Text))
        {
            msg.CriarMensagem("Informe um e-mail válido para o terceiro contato.", "Informação", MsgIcons.Informacao);
            return;
        }

        Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());

        if (cliente == null)
            cliente = new Cliente();

        cliente.Ativo = chkAtivo.Checked;
        cliente.EhFornecedor = hfTipoCarregado.Value.ToInt32() == 2;
        cliente.EhCliente = !cliente.EhFornecedor;

        if (hfId.Value.ToInt32() > 0)
            cliente.Codigo = tbxCodigoCliente.Text;
        else
            cliente.Codigo = Cliente.GerarNumeroCliente();
        cliente.Origem = Origem.ConsultarPorId(ddlOrigem.SelectedValue.ToInt32());
        rbtnTipoPessoa.SelectedValue = cliente.Tipo.ToString();

        if (rbtnTipoPessoa.SelectedValue == "F")
        {
            if (!tbxNomePessoaFisica.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Informe o nome.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (string.IsNullOrEmpty(tbxCPFPessoaFisica.Text) || !Utilitarios.Validadores.ValidaCPF(tbxCPFPessoaFisica.Text))
            {
                msg.CriarMensagem("Informe um CPF válido.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (string.IsNullOrEmpty(tbxTelefone1PessoaFisica.Text.Trim()))
            {
                msg.CriarMensagem("Informe um telefone.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!Utilitarios.Validadores.ValidaEmail(tbxEmailPessoaFisica.Text))
            {
                msg.CriarMensagem("Informe um e-mail válido.", "Informação", MsgIcons.Informacao);
                return;
            }

            cliente.Tipo = Cliente.FISICA;
            cliente.NomeRazaoSocial = tbxNomePessoaFisica.Text;
            cliente.ApelidoNomeFantasia = tbxApelidoPessoaFisica.Text;
            cliente.CpfCnpj = tbxCPFPessoaFisica.Text.Replace(".", "").Replace("-", "");
            cliente.Rg = tbxRgPessoaFisica.Text;
            cliente.EmissorRg = tbxOrgaoEmissorPessoaFisica.Text;
            cliente.Nacionalidade = tbxNacionalidadePessoaFisica.Text;
            cliente.DataNascimento = tbxDataNascimentoPessoaFisica.Text.ToDateTime();
            cliente.EstadoCivil = tbxEstadoCivilPessoaFisica.Text;
            cliente.Sexo = Convert.ToChar(ddlSexoPessoaFisica.SelectedValue);
            cliente.InscricaoEstadual = tbxInscricaoEstadualPessoaFisica.Text;
            cliente.IsentoICMS = chkIsentoPessoaFisica.Checked;
            cliente.InscricaoMunicipal = tbxInscricaoMunicipalPessoaFisica.Text;
            cliente.Telefone1 = tbxTelefone1PessoaFisica.Text;
            cliente.Telefone2 = tbxTelefone2PessoaFisica.Text;
            cliente.Email = tbxEmailPessoaFisica.Text;
            cliente.EmailRecebeNotificacoes = ckRecebeNotificacaoPessoaFisica.Visible && ckRecebeNotificacaoPessoaFisica.Checked;
            cliente.EmailRecebeLoginSenha = ckRecebeLoginSenhaPessoaFisica.Visible && ckRecebeLoginSenhaPessoaFisica.Checked;
        }
        else
        {
            if (!tbxRazaoSocialPessoaJuridica.Text.IsNotNullOrEmpty())
            {
                msg.CriarMensagem("Informe a razão social .", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!tbxCNPJPessoaJuridica.Text.IsNotNullOrEmpty() || !Utilitarios.Validadores.ValidaCNPJ(tbxCNPJPessoaJuridica.Text))
            {
                msg.CriarMensagem("Informe um CNPJ válido.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (string.IsNullOrEmpty(tbxTelefone1PessoaJuridica.Text.Trim()))
            {
                msg.CriarMensagem("Informe um telefone.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (!Utilitarios.Validadores.ValidaEmail(tbxEmailPessoaJuridica.Text))
            {
                msg.CriarMensagem("Informe um e-mail válido.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (string.IsNullOrEmpty(tbxNomeContato1.Text.Trim()) || string.IsNullOrEmpty(tbxFuncaoContato1.Text.Trim()))
            {
                msg.CriarMensagem("É necessário informar um contato com uma função para pessoa jurídica.", "Informação", MsgIcons.Informacao);
                return;
            }

            cliente.Tipo = Cliente.JURIDICA;
            cliente.NomeRazaoSocial = tbxRazaoSocialPessoaJuridica.Text;
            cliente.ApelidoNomeFantasia = tbxNomeFantasiaPessoaJuridica.Text;
            cliente.CpfCnpj = tbxCNPJPessoaJuridica.Text.Replace(".", "").Replace("-", "").Replace("/", "");

            cliente.InscricaoEstadual = tbxInscricaoEstadualPessoaJuridica.Text;
            cliente.IsentoICMS = chkIsentoPessoaJuridica.Checked;
            cliente.InscricaoMunicipal = tbxInscricaoMunicipalPessoaJuridica.Text;
            cliente.Telefone1 = tbxTelefone1PessoaJuridica.Text;
            cliente.Telefone2 = tbxTelefone2PessoaJuridica.Text;
            cliente.Email = tbxEmailPessoaJuridica.Text;
            cliente.EmailRecebeNotificacoes = ckRecebeNotificacaoPessoaJuridica.Visible && ckRecebeNotificacaoPessoaJuridica.Checked;
            cliente.EmailRecebeLoginSenha = ckRecebeLoginSenhaPessoaJuridica.Visible && ckRecebeLoginSenhaPessoaJuridica.Checked;
        }

        cliente.Observacoes = txtObservacoes.Text;
        cliente = cliente.Salvar();
        //Endereços
        if (cliente.Enderecos == null)
            cliente.Enderecos = new List<Endereco>();

        Endereco endereco = cliente.Enderecos != null && cliente.Enderecos.Count > 0 ? cliente.Enderecos[0] : new Endereco();
        endereco.Pessoa = cliente;
        //endereco.Descricao = tbxDescricaoEndereco.Text;
        endereco.Logradouro = tbxLogradouroEndereco.Text;
        endereco.Numero = tbxNumeroEndereco.Text;
        endereco.Bairro = tbxBairroEndereco.Text;
        endereco.Complemento = tbxComplementoEndereco.Text;
        endereco.Referencia = tbxReferenciaEndereco.Text;
        endereco.Cep = tbxCepEndereco.Text;
        endereco.Cidade = Cidade.ConsultarPorId(ddlCidadeEndereco.SelectedValue.ToInt32());

        endereco = endereco.Salvar();

        if (cliente.Enderecos != null && cliente.Enderecos.Count > 0)
            cliente.Enderecos[0] = endereco;
        else
            cliente.Enderecos.Add(endereco);

        //Contatos
        if (cliente.Contatos == null)
            cliente.Contatos = new List<Contato>();

        Contato contato1 = cliente.Contatos != null && cliente.Contatos.Count > 0 && cliente.Contatos[0] != null && cliente.Contatos[0].Id > 0 ? cliente.Contatos[0] : new Contato();
        contato1.Cliente = cliente;
        contato1.Nome = tbxNomeContato1.Text;
        contato1.Funcao = tbxFuncaoContato1.Text;
        contato1.Telefone1 = tbxPrimeiroTelefoneContato1.Text;
        contato1.Telefone2 = tbxSegundoTelefoneContato1.Text;
        contato1.Email = tbxEmailContato1.Text;
        contato1.DataNascimento = txtNascimentoContato1.Text.ToDateTime();
        contato1.RecebeNotificacoes = ckContato1RecebeNotificacoes.Visible && ckContato1RecebeNotificacoes.Checked;
        contato1.EmailRecebeLoginSenha = ckContato1RecebeLoginSenha.Visible && ckContato1RecebeLoginSenha.Checked;

        contato1 = contato1.Salvar();

        if (cliente.Contatos != null && cliente.Contatos.Count > 0)
            cliente.Contatos[0] = contato1;
        else
            cliente.Contatos.Add(contato1);

        Contato contato2 = cliente.Contatos != null && cliente.Contatos.Count > 1 && cliente.Contatos[1] != null && cliente.Contatos[1].Id > 0 ? cliente.Contatos[1] : new Contato();
        contato2.Cliente = cliente;
        contato2.Nome = tbxNomeContato2.Text;
        contato2.Funcao = tbxFuncaoContato2.Text;
        contato2.Telefone1 = tbxPrimeiroTelefoneContato2.Text;
        contato2.Telefone2 = tbxSegundoTelefoneContato2.Text;
        contato2.Email = tbxEmailContato2.Text;
        contato2.DataNascimento = txtNascimentoContato2.Text.ToDateTime();
        contato2.RecebeNotificacoes = ckContato2RecebeNotificacoes.Visible && ckContato2RecebeNotificacoes.Checked;
        contato2.EmailRecebeLoginSenha = ckContato2RecebeLoginSenha.Visible && ckContato2RecebeLoginSenha.Checked;

        contato2 = contato2.Salvar();

        if (cliente.Contatos != null && cliente.Contatos.Count > 1)
            cliente.Contatos[1] = contato2;
        else
            cliente.Contatos.Add(contato2);

        Contato contato3 = cliente.Contatos != null && cliente.Contatos.Count > 2 && cliente.Contatos[2] != null && cliente.Contatos[2].Id > 0 ? cliente.Contatos[2] : new Contato();
        contato3.Cliente = cliente;
        contato3.Nome = tbxNomeContato3.Text;
        contato3.Funcao = tbxFuncaoContato3.Text;
        contato3.Telefone1 = tbxPrimeiroTelefoneContato3.Text;
        contato3.Telefone2 = tbxSegundoTelefoneContato3.Text;
        contato3.Email = tbxEmailContato3.Text;
        contato3.DataNascimento = txtNascimentoContato3.Text.ToDateTime();
        contato3.RecebeNotificacoes = ckContato3RecebeNotificacoes.Visible && ckContato3RecebeNotificacoes.Checked;
        contato3.EmailRecebeLoginSenha = ckContato3RecebeLoginSenha.Visible && ckContato3RecebeLoginSenha.Checked;

        contato3 = contato3.Salvar();

        if (cliente.Contatos != null && cliente.Contatos.Count > 2)
            cliente.Contatos[2] = contato3;
        else
            cliente.Contatos.Add(contato3);

        string login = "";
        string senha = "";

        //Usuario do CLiente
        if (pnlUsuarioParaAcompanhamento.Visible)
            if (hfId.Value.ToInt32() <= 0)
            {
                string nomeCliente = rbtnTipoPessoa.SelectedValue == "F" ? tbxNomePessoaFisica.Text.RemoverCaracteresEspeciais() : tbxRazaoSocialPessoaJuridica.Text.RemoverCaracteresEspeciais();
                nomeCliente = nomeCliente.Trim().Replace("  ", " ").Replace(" ", "");
                login = nomeCliente.Length >= 8 ? nomeCliente.Substring(0, 8) : nomeCliente;

                bool existeUsuario = true;
                int numeroAcrescentar = 0;
                string loginAux = login;
                string loginIncrementado = loginAux;

                while (existeUsuario)
                {
                    existeUsuario = Cliente.JaExisteClienteComOLogin(loginIncrementado);
                    if (existeUsuario)
                    {
                        numeroAcrescentar = numeroAcrescentar + 1;
                        loginIncrementado = loginAux + (numeroAcrescentar < 10 ? "0" + numeroAcrescentar.ToString() : numeroAcrescentar.ToString());
                    }
                    else
                        login = loginIncrementado;

                }

                senha = this.GerarSenha();

                if (login.IsNotNullOrEmpty() && senha.IsNotNullOrEmpty())
                {
                    if (!this.ValidarCliente(login, senha, senha))
                        return;
                    cliente.Login = login;
                    cliente.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(senha, true);

                    tbxLoginEdicao.Text = login;
                    hfSenhaEdicao.Value = senha;
                }
            }
            else
            {
                if (tbxLoginEdicao.Text.IsNotNullOrEmpty())
                {
                    if (Cliente.JaExisteClienteComOLoginDiferenteDesseId(tbxLoginEdicao.Text, hfId.Value.ToInt32()))
                    {
                        msg.CriarMensagem("Já existe outro usuário cadastrado com este login. Informe outro login para prosseguir", "Informação", MsgIcons.Informacao);
                        return;
                    }

                    cliente.Login = tbxLoginEdicao.Text;
                }

            }

        usuario_alteracao.Visible = true;
        usuario_novo.Visible = false;

        string mensagem = "";

        if (hfId.Value.ToInt32() <= 0 && cliente.EhCliente && login.IsNotNullOrEmpty() && senha.IsNotNullOrEmpty())
            mensagem = this.EnviarLoginESenha(cliente);

        cliente = cliente.Salvar();

        if (cliente.GetUltimaClassificacao == null && hfRating.Value.ToInt32() > 0)
            this.SalvarClassificacao(cliente);

        tbxCodigoCliente.Text = cliente.Codigo;
        Session["idCliente"] = hfId.Value = cliente.Id.ToString();
        btnEnviarLoginSenha.Visible = cliente.EhCliente && cliente.EmailRecebeLoginSenha;

        if (cliente.EhFornecedor)
            msg.CriarMensagem("Fornecedor salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
        else
        {
            if (mensagem != "")
                msg.CriarMensagem("Cliente salvo com sucesso! Os dados de acesso do usuário do cliente foram enviados para os e-mails abaixo:<br />" + mensagem, "Sucesso", MsgIcons.Sucesso);
            else
                msg.CriarMensagem("Cliente salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private string GerarSenha()
    {
        string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string senha = "";
        int quantDigitos = 8;
        Random random = new Random();
        for (int i = 0; i < quantDigitos; i++)
        {
            if (random.Next(2) == 1)
                senha += random.Next(0, 10).ToString();
            else
                senha += caracteres[random.Next(0, caracteres.Length)];
        }
        return senha;
    }

    private bool ValidarCliente(string login, string senha, string senhaComfirm)
    {
        if (login == "" || senha == "")
        {
            msg.CriarMensagem("Login/Senha não podem ser nulos", "Erro", MsgIcons.Erro);
            return false;
        }
        IList<Cliente> userAux = Cliente.FiltrarPorLoginEq(login);
        if ((userAux.Count > 0 && userAux != null) && !userAux.Contains(Cliente.ConsultarPorId(hfId.Value.ToInt32())))
        {
            msg.CriarMensagem("Já existe outro usuário com o mesmo login! <br />Informe outro login.", "Erro", MsgIcons.Erro);
            return false;
        }

        if (senha != senhaComfirm)
        {
            msg.CriarMensagem("Confirmação de senha não confere com a senha digitada.", "Erro", MsgIcons.Erro);
            return false;
        }
        return true;
    }

    private void AlterarSenha()
    {
        if (tbxNovaSenha.Text == "")
        {
            msg.CriarMensagem("Senha não pode ser nula", "Erro", MsgIcons.Erro);
            btnAlterarSenha_ModalPopupExtender.Show();
            return;
        }
        if (tbxNovaSenha.Text != tbxNovaSenhaConfirm.Text)
        {
            msg.CriarMensagem("Confirmação de senha não confere com a senha digitada.", "Erro", MsgIcons.Erro);
            btnAlterarSenha_ModalPopupExtender.Show();
            return;
        }
        Cliente func = Cliente.ConsultarPorId(hfId.Value.ToInt32());
        if (func != null)
        {
            func.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxNovaSenha.Text, true);
            func.Salvar();
            msg.CriarMensagem("Senha Alterada com sucesso", "Sucesso", MsgIcons.Sucesso);
            hfSenhaEdicao.Value = tbxNovaSenha.Text;
            btnAlterarSenha_ModalPopupExtender.Hide();
        }

    }

    private void EditarImagemClienteJuridico()
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("Salve primeiro para poder adicionar uma imagem.", "Informação", MsgIcons.Informacao);
            return;
        }

        lkbEditarImagemJuridica_ModalPopupExtender.Show();
    }

    private string EnviarLoginESenha(Cliente cliente)
    {
        if (cliente == null)
        {
            msg.CriarMensagem("O cliente não foi carregado corretamente!", "Erro", MsgIcons.Informacao);
            return null;
        }

        if (string.IsNullOrEmpty(cliente.Login) || string.IsNullOrEmpty(cliente.Senha))
        {
            msg.CriarMensagem("O cliente não possui dados de acesso válidos para envio!", "Erro", MsgIcons.Informacao);
            return null;
        }
        string emailsRetorno = "";

        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Dados de Acesso";
        mail.BodyHtml = true;

        string login = cliente.Login;
        string senha = Utilitarios.Criptografia.Criptografia.Decrypt(cliente.Senha, true);

        if (login == "" || senha == "")
        {
            msg.CriarMensagem("Este cliente não possui login e/ou senha cadastrados. Informe um login e uma senha para poder enviá-los para o usuário", "Informação", MsgIcons.Informacao);
            return "";
        }

        string conteudoemail = @"
        <div style='height: 20px; font-family: Arial, Helvetica, sans-serif; font-size: 14px; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Você foi cadastrado para poder acessar o sistema Ambientalis Manager e acompanhar o andamento de seus pedidos junto à Ambientalis</div>
    <table style='width: 100%; margin-top: 10px; height: auto; font-family: Arial, Helvetica, sans-serif; font-size: 14px;'>
        <tr>
            <td align='left' style='font-weight: bold; width: 100%;' colspan='2'>                
                Para acessar o sistema utilize os dados de acesso abaixo:</td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Login:
            </td>
            <td align='left' width='50%'>
                " + login + @"
            </td>
        </tr>
        <tr>
            <td align='right' width='50%' style='font-weight: bold;'>
                Senha:
            </td>
            <td align='left' width='50%'>
                " + senha + @"
            </td>
        </tr>        
    </table>
    <div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:15px;'>
            <a href='http://ambientalismanager.com.br/Account/LoginCliente.aspx'>Clique aqui</a> para acessar o sistema agora e acompanhar o andamento de seus pedidos!</div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Dados de Acesso", conteudoemail.ToString());

        if (rbtnTipoPessoa.SelectedValue == "F")
        {
            if (tbxEmailPessoaFisica.Text.IsNotNullOrEmpty() && ckRecebeLoginSenhaPessoaFisica.Checked)
            {
                mail.EmailsDestino.Add(tbxEmailPessoaFisica.Text);
                emailsRetorno += tbxEmailPessoaFisica.Text + "<br />";
            }
        }
        else
        {
            if (tbxEmailPessoaJuridica.Text.IsNotNullOrEmpty() && ckRecebeLoginSenhaPessoaJuridica.Checked)
            {
                mail.EmailsDestino.Add(tbxEmailPessoaJuridica.Text);
                emailsRetorno += tbxEmailPessoaJuridica.Text + "<br />";
            }
        }

        if (tbxEmailContato1.Text.IsNotNullOrEmpty() && ckContato1RecebeLoginSenha.Checked)
        {
            mail.EmailsDestino.Add(tbxEmailContato1.Text);
            emailsRetorno += tbxEmailContato1.Text + "<br />";
        }

        if (tbxEmailContato2.Text.IsNotNullOrEmpty() && ckContato2RecebeLoginSenha.Checked)
        {
            mail.EmailsDestino.Add(tbxEmailContato2.Text);
            emailsRetorno += tbxEmailContato2.Text + "<br />";
        }

        if (tbxEmailContato3.Text.IsNotNullOrEmpty() && ckContato3RecebeLoginSenha.Checked)
        {
            mail.EmailsDestino.Add(tbxEmailContato3.Text);
            emailsRetorno += tbxEmailContato3.Text + "<br />";
        }

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Envio de login e senha", this.FuncionarioLogado, null, 587, false);

        if (emailsRetorno == "")
            msg.CriarMensagem("Nenhum dos e-mails do cliente está marcado para receber o login e senha do usuário", "Informação", MsgIcons.Informacao);

        return emailsRetorno;

    }

    private string RetornarEstadoCivil(string estadoCivil)
    {
        switch (estadoCivil)
        {
            case "S":
                return "Solteiro";

            case "C":
                return "Casado";

            default:
                return "";
        }
    }

    #endregion

    #region _______________ Eventos _______________

    protected void UpLoad2_UpLoadComplete(object sender, EventArgs e)
    {
        try
        {
            this.RecarregarImagemCliente();
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

    protected void ddlEstadoEndereco_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarCidades();
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

    protected void btnNovoCadastro_Click(object sender, EventArgs e)
    {
        try
        {
            //Fornecedores
            if (hfTipoCarregado.Value.ToInt32() == 2)
                Response.Redirect("CadastroCliente.aspx?frn=true");
            else
                Response.Redirect("CadastroCliente.aspx");
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

    protected void lkbExcluirImagem_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() > 0)
                this.ExcluirImagemCliente();
            else
                msg.CriarMensagem("Não há imagem para ser excluida.", "Alerta", MsgIcons.Alerta);
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarCliente();
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

    protected void btnSalvarSenha_Click(object sender, EventArgs e)
    {
        try
        {
            this.AlterarSenha();
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

    protected void btnAlterarSenha_Click(object sender, EventArgs e)
    {
        btnAlterarSenha_ModalPopupExtender.Show();
    }

    protected void btnEditarImagemClienteJuridico_Click(object sender, EventArgs e)
    {
        try
        {
            this.EditarImagemClienteJuridico();
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

    protected void btnEditarImagemFisica_Click(object sender, EventArgs e)
    {
        try
        {
            this.EditarImagemClienteJuridico();
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

    protected void btnEnviarLoginSenha_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente cliente = new Cliente(hfId.Value.ToInt32()).ConsultarPorId();
            if (cliente == null)
            {
                msg.CriarMensagem("Cliente não carregado corretamente", "Informação", MsgIcons.Informacao);
                return;
            }

            cliente.Login = tbxLoginEdicao.Text.Trim();
            cliente = cliente.Salvar();

            if (rbtnTipoPessoa.SelectedValue == "F")
            {
                if (ckRecebeLoginSenhaPessoaFisica.Checked)
                {
                    if (tbxEmailPessoaFisica.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailPessoaFisica.Text))
                    {
                        msg.CriarMensagem("Informe um e-mail válido para o cliente.", "Informação", MsgIcons.Informacao);
                        return;
                    }
                }
            }
            else
            {
                if (ckRecebeLoginSenhaPessoaJuridica.Checked)
                {
                    if (tbxEmailPessoaJuridica.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailPessoaJuridica.Text))
                    {
                        msg.CriarMensagem("Informe um e-mail válido para o cliente.", "Informação", MsgIcons.Informacao);
                        return;
                    }
                }
            }

            if (ckContato1RecebeLoginSenha.Checked)
            {
                if (tbxEmailContato1.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailContato1.Text))
                {
                    msg.CriarMensagem("Informe um e-mail válido para o primeiro contato do cliente.", "Informação", MsgIcons.Informacao);
                    return;
                }
            }

            if (ckContato2RecebeLoginSenha.Checked)
            {
                if (tbxEmailContato2.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailContato2.Text))
                {
                    msg.CriarMensagem("Informe um e-mail válido para o segundo contato do cliente.", "Informação", MsgIcons.Informacao);
                    return;
                }
            }

            if (ckContato3RecebeLoginSenha.Checked)
            {
                if (tbxEmailContato3.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailContato3.Text))
                {
                    msg.CriarMensagem("Informe um e-mail válido para o terceiro contato do cliente.", "Informação", MsgIcons.Informacao);
                    return;
                }
            }

            string mensagem = this.EnviarLoginESenha(cliente);
            if (mensagem.IsNotNullOrEmpty())
                msg.CriarMensagem("Os dados de acesso do usuário do cliente foram enviados para os e-mails abaixo:<br />" + mensagem, "Sucesso", MsgIcons.Sucesso);
        }
        catch (Exception ex)
        {
            Email mail = new Email();
            mail.Assunto = "ERRO de email login senha (" + DateTime.Now + ")";
            mail.BodyHtml = true;
            mail.Mensagem = "ERRO: " + ex.Message + (ex.InnerException != null ? " - " + ex.InnerException.Message : "");
            mail.EmailsDestino.Add("contato@c2ti.com.br");
            mail.EnviarAutenticado("Erro de email login senha", this.FuncionarioLogado, null, 587, false);
        }
        finally
        {
            C2MessageBox.Show(msg);
        }
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());
            if (cliente == null)
            {
                msg.CriarMensagem("Somente após salvar esta funcionalidade pode ser acessada.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirCliente();
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

    protected void btnReclassificarCliente_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfRating.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Indique uma classificação!", "Informação", MsgIcons.Informacao);
                return;
            }

            Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());
            if (cliente == null)
            {
                msg.CriarMensagem("Salve primeiro para efetivar a classificação!", "Informação", MsgIcons.Informacao);
                return;
            }
            if (cliente.Classificacoes == null)
                cliente.Classificacoes = new List<ClassificacaoCliente>();
            this.SalvarClassificacao(cliente);
            msg.CriarMensagem("Classificação realizada com sucesso", "Sucesso", MsgIcons.Sucesso);
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

    protected void btnVerHistoricoClassificacoes_Click(object sender, EventArgs e)
    {
        try
        {
            Cliente cliente = Cliente.ConsultarPorId(hfId.Value.ToInt32());
            if (cliente == null)
            {
                msg.CriarMensagem("Nenhum pessoa carregada", "Informação", MsgIcons.Informacao);
                return;
            }

            if (cliente.Classificacoes == null || cliente.Classificacoes.Count < 1)
            {
                msg.CriarMensagem("Nenhuma classificação foi realizada ainda!", "Informação", MsgIcons.Informacao);
                return;
            }

            grvHistoricoClassificacoes.DataSource = cliente.Classificacoes.OrderByDescending(classi => classi.Data);
            grvHistoricoClassificacoes.DataBind();
            this.PopupHistoricoClassificacoes_ModalPopupExtender.Show();
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

    #region ______________Pre-Render_______________

    protected void lkbExcluirImagemJuridica_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir a imagem?");
    }

    protected void lkbExcluirImagem_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir a imagem?");
    }

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados serão perdidos. Deseja realmente excluir?");
    }

    #endregion

    #region ______________ Triggers _______________

    protected void UpLoad2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upFormularioCliente);
    }

    protected void Label1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upFormularioCliente);
    }

    protected void btnSalvarSenha_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioCliente);
    }

    protected void btnVerHistoricoClassificacoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(sender as Control, "Click", upHistoricoClassificacoes);
    }

    #endregion

}