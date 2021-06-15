using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

public partial class Relatorios_ControlHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    IList<FiltroAux> filtrosEsquerda = new List<FiltroAux>();
    IList<FiltroAux> filtrosDireita = new List<FiltroAux>();
    IList<FiltroAux> filtrosCentro = new List<FiltroAux>();

    public void InsertFiltroEsquerda(string atributo, string valor)
    {
        filtrosEsquerda.Add(new FiltroAux(atributo, valor));
    }

    public void InsertFiltroDireita(string atributo, string valor)
    {
        filtrosDireita.Add(new FiltroAux(atributo, valor));
    }

    public void InsertFiltroCentro(string atributo, string valor)
    {
        filtrosCentro.Add(new FiltroAux(atributo, valor));
    }

    public void Atualizar()
    {
        rptDireita.DataSource = filtrosDireita;
        rptEsquerda.DataSource = filtrosEsquerda;
        rptCentro.DataSource = filtrosCentro;

        rptCentro.DataBind();
        rptDireita.DataBind();
        rptEsquerda.DataBind();

        this.Visible = true;
    }

    public void ConfigurarCabecalho(string nomeRelatorio)
    {
        lblHora.Text = DateTime.Now.ToString();
        lblNomeRelatorio.Text = nomeRelatorio;


        lblUsuario.Text = WebUtil.FuncionarioLogado.Login;
        lblFazenda.Text = "Sistema Ambientalis Manager - Relatório de " + nomeRelatorio;
        this.Page.Title = "Relatório - Sistema Ambientalis Manager: - Usuário: " + WebUtil.FuncionarioLogado.Login;
        //lblImg.ImageUrl = WebUtil.GetURLImagemLogoRelatorio;
    }

    [Serializable]
    private class FiltroAux
    {

        private string atributo = "";
        private string valor = "";

        public FiltroAux(string atributo, string valor)
        {
            this.atributo = atributo;
            this.valor = valor;
        }

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public string Atributo
        {
            get { return atributo; }
            set { atributo = value; }
        }

    }
}