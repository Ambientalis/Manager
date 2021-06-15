using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using SisWebControls;
using Utilitarios;

public partial class Cliente_Clientes : PageBase
{
    private Msg msg = new Msg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                this.CarregarCampos();
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

    private void CarregarCampos()
    {
        this.CarregarOrigens();
        if (Request["frn"] != null && Request["frn"].Trim().Equals("true"))
        {
            hfTipoCarregado.Value = "2";
            lblTituloTela.Text = "Pesquisa de fornecedores";
            btnImportarClientes.Visible = false;
        }
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

    #region _______________ Métodos _______________

    private void Pesquisar()
    {
        IList<Cliente> clientes = Cliente.Filtrar(
            tbxCodigoPesquisa.Text,
            tbxNomeRazaoApelidoPesquisa.Text,
            tbxCpfCnpjPesquisa.Text,
            ddlStatusPesquisa.SelectedValue,
            hfTipoCarregado.Value.ToInt32(),
            ddlOrigem.SelectedValue.ToInt32());

        gdvClientes.DataSource = clientes != null ? clientes : new List<Cliente>();
        gdvClientes.DataBind();

        lblStatus.Text = clientes != null ? clientes.Count + (hfTipoCarregado.Value.ToInt32() == 1 ? " cliente(s)" : " fornecedor(es)") + " encontrado(s)" : "";
    }

    private void ExcluirCliente(GridViewDeleteEventArgs e)
    {
        Cliente cliente = Cliente.ConsultarPorId(gdvClientes.DataKeys[e.RowIndex].Values[0].ToString().ToInt32());
        if (cliente != null)
        {
            if (cliente.Pedidos != null && cliente.Pedidos.Count > 0)
            {
                msg.CriarMensagem("Não é possível excluir clientes que possuam pedidos cadastrados. Apenas desativá-los.", "Informação", MsgIcons.Informacao);
                return;
            }

            if (cliente.Excluir())
                msg.CriarMensagem("Cliente excluído com sucesso", "Sucesso", MsgIcons.Sucesso);
            else
            {
                msg.CriarMensagem("Ocorreu um erro durante a exclusão", "Erro", MsgIcons.Erro);
                return;
            }
        }

        Transacao.Instance.Recarregar();
        this.Pesquisar();
    }

    public string BindEditar(Object o)
    {
        Cliente n = (Cliente)o;
        return "CadastroCliente.aspx" +
            (hfTipoCarregado.Value.ToInt32() == 2 ? "?frn=true" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id).Replace("?", "&")
            : Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + n.Id));
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

    protected void ddlPaginacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gdvClientes.PageSize = ddlPaginacao.SelectedValue.ToInt32();
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

    protected void gdrClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvClientes.PageIndex = e.NewPageIndex;
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

    protected void gdrClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            this.ExcluirCliente(e);
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

    protected void btnImportarClientes_Click(object sender, EventArgs e)
    {
        lblImportacaoClientes_ModalPopupExtender.Show();
    }

    protected void btnCarregarArquivo_Click(object sender, EventArgs e)
    {
        string extensao = fulArquivoImportarClientes.FileName.Substring(fulArquivoImportarClientes.FileName.LastIndexOf('.'));

        if (extensao == ".txt")
        {
            IList<string> clientes = new List<string>();

            string nome = "";
            string path = System.Configuration.ConfigurationManager.AppSettings["pathAplicacao"].ToString() + "/Repositorio/" +
         HttpContext.Current.Session["idConfig"].ToString() + "/Clientes/" + "Importacoes/";
            do
            {
                nome = Guid.NewGuid().ToString().Substring(0, 10) + extensao;
            } while (System.IO.File.Exists(path + "/" + nome));

            //Criar diretorio
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();

            string vCamArq = path + "/" + nome;

            fulArquivoImportarClientes.SaveAs(vCamArq);

            StreamReader Leitura = new StreamReader(vCamArq, System.Text.Encoding.GetEncoding("iso-8859-1"));
            //variavel para receber as linhas
            string strLinha;
            //loop de leitura, linha por linha
            while (Leitura.Peek() != -1)
            {
                //lendo a linha atual
                strLinha = Leitura.ReadLine().ToHtml().ToHtmlToString();
                //verificando se a linha esta vazia
                if (strLinha.Trim().Length > 0)
                    clientes.Add(strLinha);
            }

            Leitura.Close();


            if (clientes != null && clientes.Count > 0)
            {
                foreach (string item in clientes)
                {
                    string[] dadosCliente = item.Split(';');

                    if (dadosCliente != null && dadosCliente.Length > 0)
                    {
                        string codigo = dadosCliente[2].Trim().RemoverCaracteresEspeciais().Replace("  ", " ").Replace(" ", "");

                        if (!Cliente.ExisteClienteComEsteCodigo(codigo))
                        {
                            Cliente novoCliente = new Cliente();
                            novoCliente.Ativo = dadosCliente[0].Trim().RemoverCaracteresEspeciais().Replace("  ", " ").Replace(" ", "") == "Ativo";
                            novoCliente.Codigo = codigo;
                            novoCliente.NomeRazaoSocial = dadosCliente[3].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                            novoCliente.ApelidoNomeFantasia = dadosCliente[4].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                            novoCliente.CpfCnpj = dadosCliente[5].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");

                            if (dadosCliente[1].Trim().RemoverCaracteresEspeciais().Replace("  ", " ").Replace(" ", "") == "Jurídica")
                            {
                                novoCliente.Tipo = Pessoa.JURIDICA;
                                novoCliente.InscricaoEstadual = dadosCliente[6].Trim().Replace("\"", "");
                                novoCliente.Telefone1 = dadosCliente[8].Trim().RemoverCaracteresEspeciais().Replace("  ", " ") + " " + dadosCliente[9].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                novoCliente.Email = dadosCliente[10].Trim().Replace("\"", "");

                                Endereco endereco = new Endereco();

                                endereco.Logradouro = dadosCliente[11].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Numero = dadosCliente[12].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Bairro = dadosCliente[13].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Complemento = dadosCliente[14].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Cep = dadosCliente[15].Trim().Replace("\"", "");

                                string cidadeAux = dadosCliente[16].Trim().Replace("\"", "");

                                Cidade cidade = Cidade.ConsultarPorNome(cidadeAux);

                                endereco.Cidade = cidade != null ? cidade : null;

                                endereco = endereco.Salvar();

                                novoCliente.Enderecos = new List<Endereco>();
                                novoCliente.Enderecos.Add(endereco);
                            }
                            else if (dadosCliente[1].Trim().RemoverCaracteresEspeciais().Replace("  ", " ").Replace(" ", "") == "Física")
                            {
                                novoCliente.Tipo = Pessoa.FISICA;

                                string data = dadosCliente[9].Trim().Replace("\"", "");

                                if (data != "00/00/00")
                                    novoCliente.DataNascimento = data.ToDateTime();

                                novoCliente.EstadoCivil = this.RetornarEstadoCivil(dadosCliente[10].Trim().RemoverCaracteresEspeciais().Replace("  ", " "));
                                novoCliente.Sexo = Convert.ToChar(dadosCliente[11].Trim().RemoverCaracteresEspeciais().Replace("  ", " "));
                                novoCliente.Telefone1 = dadosCliente[12].Trim().RemoverCaracteresEspeciais().Replace("  ", " ") + " " + dadosCliente[13].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                novoCliente.Email = dadosCliente[14].Trim().Replace("\"", "");

                                Endereco endereco = new Endereco();

                                endereco.Logradouro = dadosCliente[15].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Numero = dadosCliente[16].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Bairro = dadosCliente[17].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Complemento = dadosCliente[18].Trim().RemoverCaracteresEspeciais().Replace("  ", " ");
                                endereco.Cep = dadosCliente[19].Trim().Replace("\"", "");

                                string cidadeAux = dadosCliente[20].Trim().Replace("\"", "");

                                Cidade cidade = Cidade.ConsultarPorNome(cidadeAux);

                                endereco.Cidade = cidade != null ? cidade : null;

                                endereco = endereco.Salvar();

                                novoCliente.Enderecos = new List<Endereco>();
                                novoCliente.Enderecos.Add(endereco);
                            }


                            novoCliente.EmailRecebeNotificacoes = novoCliente.EmailRecebeLoginSenha = novoCliente.Email != null && novoCliente.Email != "";


                            //usuario do cliente
                            string nomeCliente = novoCliente.NomeRazaoSocial.RemoverCaracteresEspeciais().Trim().Replace("  ", " ").Replace(" ", "");

                            string login = nomeCliente.Length >= 8 ? nomeCliente.Substring(0, 8) : nomeCliente;

                            bool existeUsuario = true;

                            int numeroAcrescentar = 0;

                            while (existeUsuario)
                            {
                                existeUsuario = Cliente.JaExisteClienteComOLogin(login);

                                if (existeUsuario)
                                {
                                    numeroAcrescentar = numeroAcrescentar + 1;
                                    login = login + (numeroAcrescentar < 10 ? "0" + numeroAcrescentar.ToString() : numeroAcrescentar.ToString());
                                }

                            }

                            string senha = this.GerarSenha();

                            novoCliente.Login = login;
                            novoCliente.Senha = Utilitarios.Criptografia.Criptografia.Encrypt(senha, true);

                            novoCliente = novoCliente.Salvar();

                            if (novoCliente.Email != null && novoCliente.Email != "" && Utilitarios.Validadores.ValidaEmail(novoCliente.Email))
                                this.EnviarLoginESenha(login, senha, novoCliente.Email);
                        }

                    }

                }
                msg.CriarMensagem("Clientes importados com sucesso.", "Sucesso", MsgIcons.Sucesso);
                this.Pesquisar();
            }
            else
                msg.CriarMensagem("Não há clientes para sem importados no arquivo selecionado. Ou todos os clientes do arquivo já foram importados", "Informação", MsgIcons.Informacao);

        }
        else
            msg.CriarMensagem("Arquivo selecionado não é uma listagem de clientes válida.", "Informação", MsgIcons.Informacao);
    }

    private string EnviarLoginESenha(string login, string senha, string email)
    {
        string emailsRetorno = "";

        Email mail = new Email();
        mail.Assunto = "Ambientalis Manager - Dados de Acesso";
        mail.BodyHtml = true;

        if (login == "" || senha == "")
            return "";

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
    </table>";

        mail.Mensagem = mail.GetTemplateBasico("Ambientalis Project - Dados de Acesso", conteudoemail.ToString());

        mail.EmailsDestino.Add(email);

        if (mail.EmailsDestino != null && mail.EmailsDestino.Count > 0)
            mail.EnviarAutenticado("Envio de login e senha", this.FuncionarioLogado, null, 587, false);

        return emailsRetorno;

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

    private string ToHtml(string s)
    {
        s = s.Replace("ó", "&oacute;");
        s = s.Replace("ò", "&ograve;");
        s = s.Replace("ô", "&ocirc;");
        s = s.Replace("õ", "&otilde;");
        s = s.Replace("ö", "&ouml;");
        s = s.Replace("á", "&aacute;");
        s = s.Replace("à", "&agrave;");
        s = s.Replace("â", "&acirc;");
        s = s.Replace("ã", "&atilde;");
        s = s.Replace("ä", "&auml;");
        s = s.Replace("é", "&eacute;");
        s = s.Replace("è", "&egrave;");
        s = s.Replace("ê", "&ecirc;");
        s = s.Replace("ú", "&uacute;");
        s = s.Replace("ù", "&ugrave;");
        s = s.Replace("û", "&ucirc;");
        s = s.Replace("ü", "&uuml;");
        s = s.Replace("í", "&iacute;");
        s = s.Replace("ì", "&igrave;");
        s = s.Replace("ç", "&ccedil;");
        s = s.Replace("Ó", "&Oacute;");
        s = s.Replace("Ò", "&Ograve;");
        s = s.Replace("Ô", "&Ocirc;");
        s = s.Replace("Õ", "&Otilde;");
        s = s.Replace("Ö", "&Ouml;");
        s = s.Replace("Á", "&Aacute;");
        s = s.Replace("À", "&Agrave;");
        s = s.Replace("Â", "&Acirc;");
        s = s.Replace("Ã", "&Atilde;");
        s = s.Replace("Ä", "&Auml;");
        s = s.Replace("É", "&Eacute;");
        s = s.Replace("È", "&Egrave;");
        s = s.Replace("Ê", "&Ecirc;");
        s = s.Replace("Ú", "&Uacute;");
        s = s.Replace("Ù", "&Ugrave;");
        s = s.Replace("Û", "&Ucirc;");
        s = s.Replace("Ü", "&Uuml;");
        s = s.Replace("Í", "&Iacute;");
        s = s.Replace("Ì", "&Igrave;");
        s = s.Replace("Ç", "&Ccedil;");
        s = s.Replace("º", "&ordm;");
        s = s.Replace("ª", "&ordf;");
        return s;
    }

    private string ToHtmlToString(string s)
    {
        s = s.Replace("&oacute;", "ó");
        s = s.Replace("&ograve;", "ò");
        s = s.Replace("&ocirc;", "ô");
        s = s.Replace("&otilde;", "õ");
        s = s.Replace("&ouml;", "ö");
        s = s.Replace("&aacute;", "á");
        s = s.Replace("&agrave;", "à");
        s = s.Replace("&acirc;", "â");
        s = s.Replace("&atilde;", "ã");
        s = s.Replace("&auml;", "ä");
        s = s.Replace("&eacute;", "é");
        s = s.Replace("&egrave;", "è");
        s = s.Replace("&ecirc;", "ê");
        s = s.Replace("&uacute;", "ú");
        s = s.Replace("&ugrave;", "ù");
        s = s.Replace("&ucirc;", "û");
        s = s.Replace("&uuml;", "ü");
        s = s.Replace("&iacute;", "í");
        s = s.Replace("&igrave;", "ì");
        s = s.Replace("&ccedil;", "ç");
        s = s.Replace("&Oacute;", "Ó");
        s = s.Replace("&Ograve;", "Ò");
        s = s.Replace("&Ocirc;", "Ô");
        s = s.Replace("&Otilde;", "Õ");
        s = s.Replace("&Ouml;", "Ö");
        s = s.Replace("&Aacute;", "Á");
        s = s.Replace("&Agrave;", "À");
        s = s.Replace("&Acirc;", "Â");
        s = s.Replace("&Atilde;", "Ã");
        s = s.Replace("&Auml;", "Ä");
        s = s.Replace("&Eacute;", "É");
        s = s.Replace("&Egrave;", "È");
        s = s.Replace("&Ecirc;", "Ê");
        s = s.Replace("&Uacute;", "Ú");
        s = s.Replace("&Ugrave;", "Ù");
        s = s.Replace("&Ucirc;", "Û");
        s = s.Replace("&Uuml;", "Ü");
        s = s.Replace("&Iacute;", "Í");
        s = s.Replace("&Igrave;", "Ì");
        s = s.Replace("&Ccedil;", "Ç");
        s = s.Replace("&ordm;", "º");
        s = s.Replace("&ordf;", "ª");
        //s = s.Replace("<o:p />", "");
        //s = s.Replace("<o:p>", "");
        //s = s.Replace("</o:p>", "");
        //s = s.Replace(">>", ">"); 
        return s;
    }

    #endregion

    #region ______________Pre-Render_______________

    protected void gdvClientes_PreRender(object sender, EventArgs e)
    {
        GridViewRow pager = this.gdvClientes.BottomPagerRow;
        if (pager != null)
        {
            pager.Visible = true;
        }
    }

    protected void btnGridExcluir_PreRender(object sender, EventArgs e)
    {
        WebUtil.AdicionarConfirmacao((Button)sender, "Todos os dados referentes a este cliente serão perdidos. Deseja realmente excluir este cliente ?");
    }

    #endregion

}