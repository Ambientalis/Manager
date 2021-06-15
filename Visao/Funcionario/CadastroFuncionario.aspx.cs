using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Persistencia.Fabrica;
using SisWebControls;
using Utilitarios;

public partial class Funcionario_CadastroFuncionario : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CarregarCampos();
                this.NovoFuncionario();

                string idFuncionario = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("id", this.Request));
                if (idFuncionario.ToInt32() > 0)
                    this.CarregarFuncionario(idFuncionario.ToInt32());

                string msgEmailSucesso = (Utilitarios.Criptografia.Seguranca.RecuperarParametro("msgEm", this.Request));

                if (msgEmailSucesso != null && msgEmailSucesso == "Suc")
                    msg.CriarMensagem("E-mail enviado com sucesso!", "Sucesso", MsgIcons.Sucesso);

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
        this.CarregarUnidades();
        this.CarregarEstados(ddlEstadoEndereco);
        ddlCidadeEndereco.Items.Clear();
        ddlCidadeEndereco.Items.Insert(0, new ListItem("-- Selecione primeiro o estado --", "0"));
        this.CarregarFuncoesFuncionario(null);
    }

    private void CarregarUnidades()
    {
        ddlUnidadeFuncao.DataValueField = "Id";
        ddlUnidadeFuncao.DataTextField = "Descricao";
        ddlUnidadeFuncao.DataSource = Unidade.ConsultarTodos();
        ddlUnidadeFuncao.DataBind();

        ddlUnidadeFuncao.Items.Insert(0, new ListItem("-- Selecione --", "0"));

        this.CarregarDepartamentos();
    }

    private void CarregarEstados(DropDownList dropEstado)
    {
        dropEstado.DataValueField = "Id";
        dropEstado.DataTextField = "Nome";
        dropEstado.DataSource = Estado.ConsultarTodosOrdemAlfabetica();
        dropEstado.DataBind();

        dropEstado.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregarFuncoesFuncionario(Funcionario funcionario)
    {
        cblFuncoes.Items.Clear();

        IList<Funcao> funcoes = Funcao.ConsultarTodosOrdemAlfabetica();

        if (funcoes != null && funcoes.Count > 0)
        {
            foreach (Funcao funcao in funcoes)
            {
                ListItem itemFuncao = new ListItem(funcao.GetDescricao, funcao.Id.ToString());

                if (funcionario != null && funcionario.Funcoes != null && funcionario.Funcoes.Count > 0 && funcionario.Funcoes.Contains(funcao))
                    itemFuncao.Selected = true;

                cblFuncoes.Items.Add(itemFuncao);
            }
        }
    }

    private void ExcluirFuncionario()
    {
        Funcionario funcionario = Funcionario.ConsultarPorId(hfId.Value.ToInt32());
        if (funcionario != null)
        {
            if (funcionario.OrdensServico != null && funcionario.OrdensServico.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que sejam responsáveis por alguma Ordem Serviço. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }
            if (funcionario.Visitas != null && funcionario.Visitas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que possuam Visitas. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcionario.Atividades != null && funcionario.Atividades.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que possuam Atividades. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcionario.Reservas != null && funcionario.Reservas.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir funcionários que possuam Reservas de Veículo. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcionario.Excluir())
                msg.CriarMensagem("Funcionário excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.NovoFuncionario();
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

    private void NovoFuncionario()
    {
        chkAtivo.Checked = true;

        tbxCodigoFuncionario.Text = "Gerado Automaticamente";

        tbxNome.Text = tbxApelido.Text = tbxCPF.Text = tbxRg.Text = tbxOrgaoEmissor.Text = tbxNacionalidade.Text = tbxDataNascimento.Text = tbxEstadoCivil.Text = tbxObservacoes.Text = tbxDescricaoEndereco.Text =
            tbxLogradouroEndereco.Text = tbxNumeroEndereco.Text = tbxBairroEndereco.Text = tbxComplementoEndereco.Text = tbxReferenciaEndereco.Text = tbxCepEndereco.Text = tbxCelularCorporativo.Text =
            tbxCelularPessoal.Text = tbxTelefoneResidencial.Text = tbxNomeContatoEmergencia.Text = tbxTelefoneContatoEmergencia.Text = tbxLoginEdicao.Text = tbxEmailCorporativo.Text = tbxEmailPessoal.Text = hfId.Value = "";

        ddlSexo.SelectedValue = "M";

        this.RecarregarImagemFuncionario();

        this.CarregarCampos();
        this.CarregarFuncoesFuncionario(null);

        usuario_alteracao.Visible = false;
        usuario_novo.Visible = true;

        btnEnviarLoginSenha.Visible = false;
    }

    private void ExcluirImagemFuncionario()
    {
        Funcionario funcionario = Funcionario.ConsultarPorId(hfId.Value.ToInt32());
        funcionario.Imagem = null;
        funcionario = funcionario.Salvar();
        this.RecarregarImagemFuncionario();
    }

    private void RecarregarImagemFuncionario()
    {
        Funcionario funcionario = Funcionario.ConsultarPorId(hfId.Value.ToInt32());
        imgPessoaFisica.ImageUrl = funcionario != null && funcionario.Imagem != null ? funcionario.Imagem.UrlImagem : "~/Utilitarios/Imagens/FotosIndisponiveis/FotoIndisponivelAlbum.png";
    }

    private void EditarImagemFuncionario()
    {
        if (hfId.Value.ToInt32() <= 0)
        {
            msg.CriarMensagem("Salve primeiro o funcionário para poder adicionar uma imagem ao mesmo.", "Informação", MsgIcons.Informacao);
            return;
        }

        lkbEditarImagemJuridica_ModalPopupExtender.Show();
    }

    private void CarregarDepartamentos()
    {
        ddlDepartamentoFuncao.Items.Clear();

        IList<Departamento> departamentos = (ddlUnidadeFuncao.SelectedIndex > 0 ? Departamento.ConsultarDepartamentosDaUnidade(ddlUnidadeFuncao.SelectedValue.ToInt32()) : new List<Departamento>());

        ddlDepartamentoFuncao.DataValueField = "Id";
        ddlDepartamentoFuncao.DataTextField = "Nome";
        ddlDepartamentoFuncao.DataSource = departamentos != null ? departamentos : new List<Departamento>();
        ddlDepartamentoFuncao.DataBind();

        ddlDepartamentoFuncao.Items.Insert(0, new ListItem("-- Selecione --", "0"));

        this.CarregarSetores();
    }

    private void CarregarCargos(DropDownList dropCargo)
    {
        dropCargo.Items.Clear();

        IList<Cargo> cargos = Cargo.ConsultarTodosOrdemAlfabetica();

        dropCargo.DataValueField = "Id";
        dropCargo.DataTextField = "Nome";
        dropCargo.DataSource = cargos != null ? cargos : new List<Cargo>();
        dropCargo.DataBind();

        dropCargo.Items.Insert(0, new ListItem("-- Selecione --", "0"));
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

        Funcionario func = Funcionario.ConsultarPorId(hfId.Value.ToInt32());
        if (func != null)
        {
            func.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(tbxNovaSenha.Text, true);
            func.Salvar();
            msg.CriarMensagem("Senha Alterada com sucesso", "Sucesso", MsgIcons.Sucesso);
            hfSenhaEdicao.Value = tbxNovaSenha.Text;
            btnAlterarSenha_ModalPopupExtender.Hide();
        }

    }

    private void CarregarFuncionario(int id)
    {
        Funcionario funcionario = Funcionario.ConsultarPorId(id);

        if (funcionario != null)
        {
            Session["idFuncionario"] = hfId.Value = funcionario.Id.ToString();

            chkAtivo.Checked = funcionario.Ativo;
            tbxCodigoFuncionario.Text = funcionario.Codigo;
            tbxNome.Text = funcionario.NomeRazaoSocial;

            this.CarregarFuncoesFuncionario(funcionario);

            tbxApelido.Text = funcionario.ApelidoNomeFantasia;
            tbxCPF.Text = funcionario.CpfCnpj;
            tbxRg.Text = funcionario.Rg;
            ckbVendedor.Checked = funcionario.Vendedor;
            tbxOrgaoEmissor.Text = funcionario.EmissorRg;
            tbxNacionalidade.Text = funcionario.Nacionalidade;
            tbxDataNascimento.Text = funcionario.DataNascimento != SqlDateTime.MinValue.Value ? funcionario.DataNascimento.ToShortDateString() : "";
            tbxEstadoCivil.Text = funcionario.EstadoCivil;
            ddlSexo.SelectedValue = funcionario.Sexo.ToString();
            tbxObservacoes.Text = funcionario.Observacoes;

            tbxDescricaoEndereco.Text = funcionario.GetDescricaoEndereco;
            tbxLogradouroEndereco.Text = funcionario.GetLogradouroEndereco;
            tbxNumeroEndereco.Text = funcionario.GetNumeroEndereco;
            tbxBairroEndereco.Text = funcionario.GetBairroEndereco;
            tbxComplementoEndereco.Text = funcionario.GetComplementoEndereco;
            tbxReferenciaEndereco.Text = funcionario.GetReferenciaEndereco;
            tbxCepEndereco.Text = funcionario.GetCepEndereco;

            ddlEstadoEndereco.SelectedValue = funcionario.GetEstadoEndereco != null ? funcionario.GetEstadoEndereco.Id.ToString() : "0";
            this.CarregarCidades();
            ddlCidadeEndereco.SelectedValue = funcionario.GetCidadeEndereco != null ? funcionario.GetCidadeEndereco.Id.ToString() : "0";

            //Telefones
            tbxCelularCorporativo.Text = funcionario.CelularCorporativo;
            tbxCelularPessoal.Text = funcionario.CelularPessoal;
            tbxTelefoneResidencial.Text = funcionario.TelefoneResidencial;
            tbxNomeContatoEmergencia.Text = funcionario.NomeContatoEmergencia;
            tbxTelefoneContatoEmergencia.Text = funcionario.TelefoneContatoEmergencia;

            //E-mails
            tbxEmailCorporativo.Text = funcionario.EmailCorporativo;
            tbxEmailPessoal.Text = funcionario.EmailPessoal;

            tbxLoginEdicao.Text = funcionario.Login != null && funcionario.Login != "" ? funcionario.Login : "";
            hfSenhaEdicao.Value = funcionario.Senha != null && funcionario.Senha != "" ? Utilitarios.Criptografia.Criptografia.Decrypt(funcionario.Senha, true) : "";

            btnEnviarLoginSenha.Enabled = btnEnviarLoginSenha.Visible = true;

            this.RecarregarImagemFuncionario();

            usuario_alteracao.Visible = true;
            usuario_novo.Visible = false;
        }
    }

    private void SalvarFuncionario()
    {
        new FabricaDAONHibernateBase().GetDAOBase().ExecutarComandoSql("set identity_insert funcionarios on");
        Funcionario funcionario = Funcionario.ConsultarPorId(hfId.Value.ToInt32());

        if (funcionario == null)
            funcionario = new Funcionario();

        funcionario.Ativo = chkAtivo.Checked;

        if (hfId.Value.ToInt32() > 0)
            funcionario.Codigo = tbxCodigoFuncionario.Text;
        else
            funcionario.Codigo = Funcionario.GerarNumeroCodigo();

        if (!tbxNome.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Informe o nome do funcionário.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (!tbxCPF.Text.IsNotNullOrEmpty())
        {
            msg.CriarMensagem("Informe o CPF do funcionário.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (!Utilitarios.Validadores.ValidaCPF(tbxCPF.Text))
        {
            msg.CriarMensagem("Informe um CPF válido para o funcionário.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (tbxEmailCorporativo.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailCorporativo.Text))
        {
            msg.CriarMensagem("Informe um E-mail Corporativo válido para o funcionário.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (tbxEmailPessoal.Text.IsNotNullOrEmpty() && !Utilitarios.Validadores.ValidaEmail(tbxEmailPessoal.Text))
        {
            msg.CriarMensagem("Informe um E-mail Pessoal válido para o funcionário.", "Informação", MsgIcons.Informacao);
            return;
        }

        if (hfId.Value.ToInt32() > 0)
        {
            if (Funcionario.JaExisteFuncionarioComOLoginDiferenteDesseId(tbxLoginEdicao.Text, hfId.Value.ToInt32()))
            {
                msg.CriarMensagem("Já existe outro funcionário cadastrado com este login. Informe outro login para prosseguir", "Informação", MsgIcons.Informacao);
                return;
            }
        }


        funcionario.Vendedor = ckbVendedor.Checked;
        funcionario.Tipo = Funcionario.FISICA;
        funcionario.NomeRazaoSocial = tbxNome.Text;

        funcionario.ApelidoNomeFantasia = tbxApelido.Text;
        funcionario.CpfCnpj = tbxCPF.Text.Replace(".", "").Replace("-", "");
        funcionario.Rg = tbxRg.Text;
        funcionario.EmissorRg = tbxOrgaoEmissor.Text;
        funcionario.Nacionalidade = tbxNacionalidade.Text;
        funcionario.DataNascimento = tbxDataNascimento.Text.ToDateTime();
        funcionario.EstadoCivil = tbxEstadoCivil.Text;
        funcionario.Sexo = Convert.ToChar(ddlSexo.SelectedValue);

        funcionario.Observacoes = tbxObservacoes.Text;

        //Endereços
        if (funcionario.Enderecos == null)
            funcionario.Enderecos = new List<Endereco>();

        Endereco endereco = funcionario.Enderecos != null && funcionario.Enderecos.Count > 0 ? funcionario.Enderecos[0] : new Endereco();

        endereco.Descricao = tbxDescricaoEndereco.Text;
        endereco.Logradouro = tbxLogradouroEndereco.Text;
        endereco.Numero = tbxNumeroEndereco.Text;
        endereco.Bairro = tbxBairroEndereco.Text;
        endereco.Complemento = tbxComplementoEndereco.Text;
        endereco.Referencia = tbxReferenciaEndereco.Text;
        endereco.Cep = tbxCepEndereco.Text;
        endereco.Cidade = Cidade.ConsultarPorId(ddlCidadeEndereco.SelectedValue.ToInt32());

        endereco = endereco.Salvar();

        if (funcionario.Enderecos != null && funcionario.Enderecos.Count > 0)
            funcionario.Enderecos[0] = endereco;
        else
            funcionario.Enderecos.Add(endereco);

        //Telefones
        funcionario.CelularCorporativo = tbxCelularCorporativo.Text;
        funcionario.CelularPessoal = tbxCelularPessoal.Text;
        funcionario.TelefoneResidencial = tbxTelefoneResidencial.Text;
        funcionario.NomeContatoEmergencia = tbxNomeContatoEmergencia.Text;
        funcionario.TelefoneContatoEmergencia = tbxTelefoneContatoEmergencia.Text;

        //E-mails
        funcionario.EmailCorporativo = tbxEmailCorporativo.Text;
        funcionario.EmailPessoal = tbxEmailPessoal.Text;

        string login = "";
        string senha = "";

        //Usuario do funcionario
        if (funcionario.Id == 0)
        {
            string nomeFuncionario = tbxNome.Text.RemoverCaracteresEspeciais();
            nomeFuncionario = nomeFuncionario.Trim().Replace("  ", " ").Replace(" ", "");
            login = nomeFuncionario.Length >= 8 ? nomeFuncionario.Substring(0, 8) : nomeFuncionario;
            bool existeUsuario = true;
            int numeroAcrescentar = 0;
            string loginAux = login;
            string loginIncrementado = loginAux;

            while (existeUsuario)
            {
                existeUsuario = Funcionario.JaExisteFuncionarioComOLogin(loginIncrementado);

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
                if (!this.ValidarFuncionario(login, senha, senha))
                    return;
                funcionario.Login = login;
                funcionario.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(senha, true);

                tbxLoginEdicao.Text = login;
                hfSenhaEdicao.Value = senha;
            }
        }
        else
        {
            if (tbxLoginEdicao.Text.IsNotNullOrEmpty())
                funcionario.Login = tbxLoginEdicao.Text;
        }

        usuario_alteracao.Visible = true;
        usuario_novo.Visible = false;

        funcionario = funcionario.Salvar();

        if (funcionario.Funcoes == null)
            funcionario.Funcoes = new List<Funcao>();
        List<Funcao> funcoesSelecionadas = new List<Funcao>();
        //Salvando as funções do funcionário
        if (cblFuncoes.Items != null && cblFuncoes.Items.Count > 0)
            foreach (ListItem item in cblFuncoes.Items)
                if (item.Selected)
                {
                    Funcao funcao = Funcao.ConsultarPorId(item.Value.ToInt32());
                    if (funcao != null && !funcionario.Funcoes.Contains(funcao))
                    {
                        Permissao novaPermissao = new Permissao();
                        novaPermissao.Emp = funcao.Setor.Departamento.Emp;
                        novaPermissao.Funcao = funcao;
                        novaPermissao.Funcionario = funcionario;
                        novaPermissao.Salvar();
                        funcionario.Funcoes.Add(funcao);
                    }
                    funcoesSelecionadas.Add(funcao);
                }
        //Excluindo as funções do funcionário que estavam selecionadas, mas foram deletadas
        IList<Funcao> funcoesExcluir = funcionario.Funcoes.Where(funcAux => !funcoesSelecionadas.Contains(funcAux)).ToList();
        for (int index = funcoesExcluir.Count - 1; index >= 0; index--)
        {
            if (funcionario.Funcoes.Contains(funcoesExcluir[index]))
                funcionario.Funcoes.Remove(funcoesExcluir[index]);
        }
        //Excluindo as permissões também
        IList<Permissao> permissoesExcluir = (funcionario.Permissoes != null ? funcionario.Permissoes.Where(permi => !funcionario.Funcoes.Contains(permi.Funcao)).ToList() : new List<Permissao>());
        foreach (Permissao permi in permissoesExcluir)
        {
            funcionario.Permissoes.Remove(permi);
            permi.Excluir();
        }

        Session["idFuncionario"] = hfId.Value = funcionario.Id.ToString();

        tbxCodigoFuncionario.Text = funcionario.Codigo;

        btnEnviarLoginSenha.Visible = true;

        msg.CriarMensagem("Funcionário salvo com sucesso!", "Sucesso", MsgIcons.Sucesso);

        if (login.IsNotNullOrEmpty() && senha.IsNotNullOrEmpty())
        {
            this.EnviarLoginESenha(funcionario);
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

    private bool ValidarFuncionario(string login, string senha, string senhaComfirm)
    {
        if (login == "" || senha == "")
        {
            msg.CriarMensagem("Login/Senha não podem ser nulos", "Erro", MsgIcons.Erro);
            return false;
        }
        IList<Funcionario> userAux = Funcionario.FiltrarPorLoginEq(login);
        if ((userAux.Count > 0 && userAux != null) && !userAux.Contains(Funcionario.ConsultarPorId(hfId.Value.ToInt32())))
        {
            msg.CriarMensagem("Já existe outro funcionário com o mesmo login! <br />Informe outro login.", "Erro", MsgIcons.Erro);
            return false;
        }

        if (senha != senhaComfirm)
        {
            msg.CriarMensagem("Confirmação de senha não confere com a senha digitada.", "Erro", MsgIcons.Erro);
            return false;
        }
        return true;
    }

    private void NovoCargo()
    {
        tbxNomeCargo.Text = hfIdCargo.Value = "";
    }

    private void NovoDepartamento()
    {
        tbxNomeDepartamento.Text = hfIdDepartamento.Value = "";
    }

    private void CarregarGridFuncoes()
    {
        IList<Funcao> funcoes = Funcao.ConsultarTodosAtivosInativosOrdemAlfabetica();
        gdvFuncoes.DataSource = funcoes != null ? funcoes : new List<Funcao>();
        gdvFuncoes.DataBind();
    }

    private void NovaFuncao()
    {
        campos_cadastro_funcoes.Visible = true;
        hfIdFuncao.Value = "";
        this.CarregarCargos(ddlCargoFuncao);
        this.CarregarUnidades();
    }

    private void CarregarSetores()
    {
        ddlSetorFuncao.Items.Clear();

        Departamento departamento = Departamento.ConsultarPorId(ddlDepartamentoFuncao.SelectedValue.ToInt32());

        ddlSetorFuncao.DataValueField = "Id";
        ddlSetorFuncao.DataTextField = "Nome";
        ddlSetorFuncao.DataSource = (departamento != null && departamento.Setores != null ? departamento.Setores : new List<Setor>());
        ddlSetorFuncao.DataBind();

        ddlSetorFuncao.Items.Insert(0, new ListItem("-- Selecione --", "0"));
    }

    private void CarregaFuncao(int id)
    {
        Funcao funcao = Funcao.ConsultarPorId(id);

        if (funcao != null)
        {
            this.NovaFuncao();

            campos_cadastro_funcoes.Visible = true;

            hfIdFuncao.Value = funcao.Id.ToString();
            ddlCargoFuncao.SelectedValue = funcao.Cargo.Id.ToString();
            ddlUnidadeFuncao.SelectedValue = funcao.Setor.Departamento.Emp.ToString();
            this.CarregarDepartamentos();
            ddlDepartamentoFuncao.SelectedValue = funcao.Setor.Departamento.Id.ToString();
            this.CarregarSetores();
            ddlSetorFuncao.SelectedValue = funcao.Setor.Id.ToString();
        }
    }

    private void ExcluirFuncao()
    {
        Funcao funcao = Funcao.ConsultarPorId(hfIdFuncao.Value.ToInt32());
        if (funcao != null)
        {
            if (funcao.Funcionarios != null && funcao.Funcionarios.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir Funções que possuam funcionários associados. Apenas desativá-las.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (funcao.Excluir())
                msg.CriarMensagem("Função exluída com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void AtivarDesativarFuncao(GridViewDeleteEventArgs e)
    {
        Funcao funcao = Funcao.ConsultarPorId(gdvFuncoes.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());

        if (funcao != null)
        {
            if (funcao.Ativo)
                funcao.Ativo = false;
            else
                funcao.Ativo = true;

            funcao.Funcionarios = null;

            funcao = funcao.Salvar();

            if (funcao.Ativo)
                msg.CriarMensagem("Função ativada com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
                msg.CriarMensagem("Função desativada com sucesso", "Sucesso", MsgIcons.Sucesso);
        }
    }

    private void SalvarFuncao()
    {
        Funcao funcao = Funcao.ConsultarPorId(hfIdFuncao.Value.ToInt32());

        if (funcao == null)
            funcao = new Funcao();

        funcao.Ativo = true;
        funcao.Cargo = Cargo.ConsultarPorId(ddlCargoFuncao.SelectedValue.ToInt32());
        funcao.Setor = Setor.ConsultarPorId(ddlSetorFuncao.SelectedValue.ToInt32());

        funcao = funcao.Salvar();

        msg.CriarMensagem("Função salva com sucesso.", "Sucesso", MsgIcons.Sucesso);
    }

    private void NovoSetor()
    {
        campos_cadastro_setores.Visible = true;
        hfIdSetor.Value = "";
        this.CarregarDepartamentos();
        tbxNomeSetor.Text = "";
    }

    private void CarregarGridSetores()
    {
        IList<Setor> setores = Setor.ConsultarTodosOrdemAlfabetica();
        gdvSetores.DataSource = setores != null ? setores : new List<Setor>();
        gdvSetores.DataBind();
    }

    private void ExcluirSetor(GridViewDeleteEventArgs e)
    {
        Setor setor = Setor.ConsultarPorId(gdvSetores.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (setor != null)
        {
            if (setor.OrdensServico != null && setor.OrdensServico.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir setores que estejam associados à alguma Ordem Serviço. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (setor.Funcoes != null && setor.Funcoes.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir setores que estejam associados à alguma Função. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (setor.Excluir())
                msg.CriarMensagem("Setor excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void CarregaSetor(int id)
    {
        Setor setor = Setor.ConsultarPorId(id);

        if (setor != null)
        {
            campos_cadastro_setores.Visible = true;
            hfIdSetor.Value = setor.Id.ToString();
            tbxNomeSetor.Text = setor.Nome;
            this.CarregarDepartamentos();
            ddlDepartamentoSetor.SelectedValue = setor.Departamento != null ? setor.Departamento.Id.ToString() : "0";
        }
    }

    private void SalvarSetor()
    {
        Setor setor = Setor.ConsultarPorId(hfIdSetor.Value.ToInt32());

        if (setor == null)
            setor = new Setor();

        setor.Nome = tbxNomeSetor.Text;
        setor.Ativo = true;
        setor.Departamento = Departamento.ConsultarPorId(ddlDepartamentoSetor.SelectedValue.ToInt32());

        setor = setor.Salvar();

        msg.CriarMensagem("Setor salvo com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    public string BindToolTipFuncao(object o)
    {
        Funcao f = (Funcao)o;
        return f.Ativo ? "Desativar" : "Ativar";
    }

    public bool BindUsuarioPodeAtivarDesativarFuncao(Object o)
    {
        return true;
    }

    private void ExcluirCargo()
    {
        Cargo cargo = Cargo.ConsultarPorId(hfIdCargo.Value.ToInt32());
        if (cargo != null)
        {
            if (cargo.Funcoes != null && cargo.Funcoes.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir Cargos que possuam funções associadas. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (cargo.Excluir())
                msg.CriarMensagem("Cargo excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void ExcluirDepartamento()
    {
        Departamento departamento = Departamento.ConsultarPorId(hfIdDepartamento.Value.ToInt32());
        if (departamento != null)
        {
            if (departamento.Setores != null && departamento.Setores.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir Departamentos que possuam setores associados. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (departamento.Excluir())
                msg.CriarMensagem("Departamento excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }
    }

    private void EnviarLoginESenha(Funcionario funcionario)
    {
        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Dados de Acesso";
        mail.BodyHtml = true;

        string login = tbxLoginEdicao.Text;
        string senha = hfSenhaEdicao.Value;

        if (login == "" || senha == "")
        {
            msg.CriarMensagem("Este funcionário não possui login e/ou senha cadastrados. Informe um login e uma senha para poder enviá-los para o usuário", "Informação", MsgIcons.Informacao);
            return;
        }

        if (!Utilitarios.Validadores.ValidaEmail(tbxEmailCorporativo.Text))
        {
            msg.CriarMensagem("Informe um e-mail válido para receber o Login e a Senha do funcionário.", "Informação", MsgIcons.Informacao);
            return;
        }

        string conteudoemail = @"
        <div style='height: 20px; font-family: Arial, Helvetica, sans-serif; font-size: 14px; margin-top: 20px; margin-right: 20px; border-bottom: 1px solid silver; font-weight: 700;' align='left'>
        Você foi cadastrado como funcionário para poder acessar o sistema Ambientalis Manager.</div>
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
<div style='text-align:center; font-size:14px; font-family: Arial, Helvetica, sans-serif; margin-top:10px;'>
            <a href='http://ambientalismanager.com.br/Account/Login.aspx'>Clique aqui</a> para acessar o sistema Ambientalis Manager agora.</div>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Manager - Dados de Acesso", conteudoemail.ToString());

        mail.EmailsDestino.Add(tbxEmailCorporativo.Text);


        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Envio de login e senha", this.FuncionarioLogado, null, 587, false);


        usuario_alteracao.Visible = true;
        usuario_novo.Visible = false;

        msg.CriarMensagem("E-mail enviado com sucesso", "Sucesso", MsgIcons.Sucesso);
    }

    #endregion

    #region _______________ Eventos ________________

    protected void gdvFuncoes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvFuncoes.PageIndex = e.NewPageIndex;
            this.CarregarGridFuncoes();
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
            Response.Redirect("CadastroFuncionario.aspx");
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
                this.ExcluirImagemFuncionario();
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

    protected void UpLoad2_UpLoadComplete(object sender, EventArgs e)
    {
        try
        {
            this.RecarregarImagemFuncionario();
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
            this.EditarImagemFuncionario();
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarFuncionario();
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

    protected void btnNovoDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoDepartamento();
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

    protected void btnDalvarDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            Departamento departamento = Departamento.ConsultarPorId(hfIdDepartamento.Value.ToInt32());

            if (departamento == null)
                departamento = new Departamento();

            departamento.Nome = tbxNomeDepartamento.Text;
            departamento.Ativo = true;

            departamento = departamento.Salvar();

            msg.CriarMensagem("Departamento salvo com sucesso.", "Sucesso", MsgIcons.Sucesso);

            lblCadastroDepartamento_ModalPopupExtender.Hide();

            if (hfIdDepartamento.Value.ToInt32() > 0)
            {
                this.CarregarGridFuncoes();
                this.CarregarFuncoesFuncionario(Funcionario.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnNovoCargo_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoCargo();
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

    protected void btnSalvarCargo_Click(object sender, EventArgs e)
    {
        try
        {
            Cargo cargo = Cargo.ConsultarPorId(hfIdCargo.Value.ToInt32());

            if (cargo == null)
                cargo = new Cargo();

            cargo.Nome = tbxNomeCargo.Text;
            cargo.Ativo = true;

            cargo = cargo.Salvar();

            msg.CriarMensagem("Cargo salvo com sucesso.", "Sucesso", MsgIcons.Sucesso);

            this.CarregarCargos(ddlCargoFuncao);

            if (hfIdCargo.Value.ToInt32() > 0)
            {
                this.CarregarGridFuncoes();
                this.CarregarFuncoesFuncionario(Funcionario.ConsultarPorId(hfId.Value.ToInt32()));
            }

            lblCadastroCargo_ModalPopupExtender.Hide();

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

    protected void btnEditarCargo_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoCargo();

            Cargo cargo = Cargo.ConsultarPorId(ddlCargoFuncao.SelectedValue.ToInt32());

            if (cargo != null && cargo.Id > 0)
            {
                tbxNomeCargo.Text = cargo.Nome;
                hfIdCargo.Value = cargo.Id.ToString();
            }

            lblCadastroCargo_ModalPopupExtender.Show();
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

    protected void btnEditarDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoDepartamento();

            Departamento departamento = Departamento.ConsultarPorId(ddlDepartamentoFuncao.SelectedValue.ToInt32());

            if (departamento != null && departamento.Id > 0)
            {
                tbxNomeDepartamento.Text = departamento.Nome;
                hfIdDepartamento.Value = departamento.Id.ToString();
            }

            lblCadastroDepartamento_ModalPopupExtender.Show();
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

    protected void btnEditarFuncoes_Click(object sender, EventArgs e)
    {
        try
        {

            hfIdFuncao.Value = "";
            this.CarregarGridFuncoes();
            this.NovaFuncao();
            campos_cadastro_funcoes.Visible = false;

            lblCadastroFuncoes_ModalPopupExtender.Show();
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

    protected void btnNovaFuncao_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovaFuncao();
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

    protected void ddlUnidadeFuncao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarDepartamentos();
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

    protected void ddlDepartamentoFuncao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.CarregarSetores();
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

    protected void btnGridEditarFuncao_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaFuncao(((Button)sender).CommandArgument.ToInt32());
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

    protected void gdvFuncoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.AtivarDesativarFuncao(e);
            Transacao.Instance.Recarregar();
            this.CarregarGridFuncoes();
            this.CarregarFuncoesFuncionario(Funcionario.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnExcluirFuncao_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdFuncao.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Não há nenhuma função carregada para poder ser excluída.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirFuncao();

            Transacao.Instance.Recarregar();
            this.NovaFuncao();
            campos_cadastro_funcoes.Visible = false;
            this.CarregarGridFuncoes();
            this.CarregarFuncoesFuncionario(Funcionario.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnSalvarFuncao_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarFuncao();

            Transacao.Instance.Recarregar();
            campos_cadastro_funcoes.Visible = false;
            this.CarregarGridFuncoes();
            this.CarregarFuncoesFuncionario(Funcionario.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void gdvSetores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvSetores.PageIndex = e.NewPageIndex;
            this.CarregarGridSetores();
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

    protected void gdvSetores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirSetor(e);
            Transacao.Instance.Recarregar();
            this.CarregarGridSetores();
            this.CarregarSetores();
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

    protected void btnEditarSetor_Click(object sender, EventArgs e)
    {
        try
        {

            hfIdSetor.Value = "";
            this.CarregarGridSetores();
            this.NovoSetor();
            campos_cadastro_setores.Visible = false;

            lblCadastroSetores_ModalPopupExtender.Show();
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

    protected void btnNovoSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoSetor();
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

    protected void btnGridEditarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.CarregaSetor(((Button)sender).CommandArgument.ToInt32());
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

    protected void btnSalvarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.SalvarSetor();

            Transacao.Instance.Recarregar();
            campos_cadastro_setores.Visible = false;
            this.CarregarGridSetores();
            this.CarregarSetores();

            if (hfIdSetor.Value.ToInt32() > 0)
            {
                this.CarregarGridFuncoes();
                this.CarregarFuncoesFuncionario(Funcionario.ConsultarPorId(hfId.Value.ToInt32()));
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

    protected void btnExcluirCargo_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdCargo.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Não há nenhum cargo carregado para poder ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirCargo();

            Transacao.Instance.Recarregar();
            this.CarregarCargos(ddlCargoFuncao);

            lblCadastroCargo_ModalPopupExtender.Hide();

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

    protected void btnExcluirDepartamento_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIdDepartamento.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Não há nenhum departamento carregado para poder ser excluído.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirDepartamento();

            Transacao.Instance.Recarregar();
            this.CarregarDepartamentos();

            lblCadastroDepartamento_ModalPopupExtender.Hide();

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

    protected void btnEditarDepartamentoCadSetor_Click(object sender, EventArgs e)
    {
        try
        {
            this.NovoDepartamento();

            Departamento departamento = Departamento.ConsultarPorId(ddlDepartamentoSetor.SelectedValue.ToInt32());

            if (departamento != null && departamento.Id > 0)
            {
                tbxNomeDepartamento.Text = departamento.Nome;
                hfIdDepartamento.Value = departamento.Id.ToString();
            }

            lblCadastroDepartamento_ModalPopupExtender.Show();
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

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Value.ToInt32() <= 0)
            {
                msg.CriarMensagem("Salve primeiro o funcionário para poder excluí-lo.", "Informação", MsgIcons.Informacao);
                return;
            }

            this.ExcluirFuncionario();
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
            Funcionario funcionario = Funcionario.ConsultarPorId(hfId.Value.ToInt32());

            this.EnviarLoginESenha(funcionario);

            Transacao.Instance.Recarregar();

            //  Response.Redirect("CadastroFuncionario.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + funcionario.Id + "&msgEm=Suc"));
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

    protected void btnEnviarEmailSenhaFuncionario_Click(object sender, EventArgs e)
    {
        try
        {

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

    #region _______________Pre-Render_______________

    protected void btnExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este funcionário serão perdidos. Deseja realmente excluir este funcionário ?");
    }

    protected void lkbExcluirImagem_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Deseja realmente excluir a imagem ?");
    }

    protected void gdvFuncoes_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvFuncoes.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnExcluirFuncao_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Ao excluir uma função, os funcionários que a exercem deixarão de exercê-la. Confirma a exclusão da função ?");
    }

    protected void btnAtivarDesativarFuncao_PreRender(object sender, EventArgs e)
    {
        if (((Button)sender).ToolTip == "Desativar")
            WebUtil.AdicionarConfirmacao((Button)sender, "Ao desativar uma função, os funcionários que a exercem deixarão de exercê-la.");
    }

    protected void gdvSetores_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvSetores.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluirSetor_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este setor serão perdidos. Deseja realmente excluir este setor ?");
    }

    protected void btnEnviarLoginSenha_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Uma nova senha será gerada para o funcionário e enviada para o e-mail que for informado.");
    }

    #endregion

    #region _______________ Triggers _______________

    protected void btnCadastraFuncionario_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnGridEditar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void UpLoad2_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "UpLoadComplete", upFormularioFuncionario);
    }

    protected void Label1_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica(UpLoad2, "UpLoadComplete", upFormularioFuncionario);
    }

    protected void btnSalvarSenha_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnDalvarDepartamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovoSetor);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaFuncoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnSalvarCargo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaFuncoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnExcluirCargo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);
    }

    protected void btnEditarCargo_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastroCargo);
    }

    protected void btnEditarDepartamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastroDepartamento);
    }

    protected void btnEditarFuncoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaFuncoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);
    }

    protected void btnSalvarFuncao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaFuncoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnExcluirFuncao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaFuncoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnGridEditarFuncao_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);
    }

    protected void gdvFuncoes_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upFormularioFuncionario);
    }

    protected void btnSalvarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaSetores);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);

        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaFuncoes);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    protected void btnEditarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovoSetor);
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upPesquisaSetores);
    }

    protected void gdvSetores_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "RowDeleting", upCamposNovaFuncao);
    }

    protected void btnGridEditarSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovoSetor);
    }

    protected void btnExcluirDepartamento_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCamposNovaFuncao);
    }

    protected void btnEditarDepartamentoCadSetor_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upCadastroDepartamento);
    }

    protected void btnSalvar_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upEnviarLoginSenhaFuncionario);
    }

    protected void btnEnviarEmailSenhaFuncionario_Init(object sender, EventArgs e)
    {
        WebUtil.InserirTriggerDinamica((Control)sender, "Click", upFormularioFuncionario);
    }

    #endregion

}