using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;
using Modelo;
using System.Data.SqlTypes;
using System.Web.Script.Serialization;
using SisWebControls;
using System.Collections;


public partial class Site_Index : PageBase
{
    private Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                imgLogoIndex.ImageUrl = ConfiguracaoUtil.GetLinkLogomarca;
                this.CarregarAniversariantes();
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

    private void CarregarAniversariantes()
    {
        IList retorno = new Contato().GetContatosNoMesDeNascimento(DateTime.Now.Month);
        IList<Aniversariante> anis = new List<Aniversariante>();
        foreach (Object[] ret in retorno)
            anis.Add(new Aniversariante(ret));

        rptAniversariantesDoMes.DataSource = anis;
        rptAniversariantesDoMes.DataBind();
    }

    #region _________________ Metodos __________________


    #endregion

    #region __________________ Bindings __________________

    public string BindEmpresaCliente(Object o)
    {
        return "";
    }

    public string BindDescricao(Object o)
    {
        OrdemServico os = (OrdemServico)o;
        return os.Descricao;
    }

    public string BindTipoOS(Object o)
    {
        return "";
    }

    Cliente cli = null;
    public string BindingUrlCliente(object contato)
    {
        this.cli = ((Aniversariante)contato).Cliente;
        return "../Cliente/CadastroCliente.aspx" + Utilitarios.Criptografia.Seguranca.MontarParametros("id=" + cli.Id);
    }

    public string BindingNomeCliente(object contato)
    {
        return this.cli.NomeRazaoSocial;
    }

    #endregion

    [Serializable]
    public class Aniversariante
    {
        private String nome;
        private DateTime dataNascimento;
        private String funcao;
        private Cliente cliente;
        private String email;
        private int mesNascimento;

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }
        public String Funcao
        {
            get { return funcao; }
            set { funcao = value; }
        }
        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        public int MesNascimento
        {
            get { return mesNascimento; }
            set { mesNascimento = value; }
        }

        public Aniversariante(Object[] dados)
        {
            this.nome = dados[0].ToString();
            this.dataNascimento = dados[1].ToString().ToDateTime();
            this.funcao = dados[2].ToString();
            this.cliente = (dados[3] != null ? new Cliente(dados[3].ToString().ToInt32()).ConsultarPorId() : new Cliente() { NomeRazaoSocial = "N/I" });
            this.email = dados[4].ToString();
            this.mesNascimento = dados[5].ToString().ToInt32();
        }
    }
}