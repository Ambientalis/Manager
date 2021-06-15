using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Utilitarios;

public partial class MBOX : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Hide();
        if (!Page.IsPostBack)
        {
        }
    }

    public void Hide()
    {
        pnUp.Style.Add("display", "none");
        pnUpFundo.Style.Add("display", "none");
    }

    public void Show(string Mensagem, string Caption)
    {
        this.Show(Mensagem, Caption, MsgIcons.Sucesso, null);
    }

    public void Show(Msg msg)
    {
        if (msg.Mensagem != null || msg.Caption != null || msg.Botoes != null)
        {
            this.Show(msg.Mensagem, msg.Caption, msg.Ico, msg.Botoes);
        }
        msg = new Msg();
    }

    public void Show(string Mensagem, string Caption, MsgIcons m)
    {
        this.Show(Mensagem, Caption, m, null);
    }

    public void Show(string Mensagem, string Caption, MsgIcons m, params MsgBotoes[] botoes)
    {
        pnUp.Style.Add("display", "block");
        pnUpFundo.Style.Add("display", "block");
        this.InitializeButtons(botoes);
        this.InitializeIcon(m);
        lblCaption.Text = Caption;
        lblTexto.Text = Mensagem;
    }

    private void InitializeIcon(MsgIcons m)
    {
        switch (m)
        {
            case MsgIcons.Nenhum:
                {
                    iIcon.Visible = false;
                    break;
                }
            case MsgIcons.Alerta:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/alerta.png";
                    break;
                }
            case MsgIcons.Sucesso:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/sucesso.png";
                    break;
                }
            case MsgIcons.Exclamacao:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/exclamacao.png";
                    break;
                }
            case MsgIcons.Informacao:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/informacao.png";
                    break;
                }
            case MsgIcons.Erro:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/erro.png";
                    break;
                }
            case MsgIcons.Interrogacao:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/interrogacao.png";
                    break;
                }
            case MsgIcons.AcessoNegado:
                {
                    iIcon.Visible = true;
                    iIcon.ImageUrl = "~/MBOX/icons/acessoNegado.png";
                    break;
                }

        }
    }

    private void InitializeButtons(params MsgBotoes[] botoes)
    {
        btnOK.Visible = false;
        btnSalvar.Visible = false;
        btnExcluir.Visible = false;
        btnAlterar.Visible = false;
        btnAtualizar.Visible = false;
        btnCancelar.Visible = false;

        if (botoes != null)
        {
            foreach (MsgBotoes botao in botoes)
            {
                switch (botao)
                {
                    case MsgBotoes.Ok: { btnOK.Visible = true; } break;
                    case MsgBotoes.Salvar: { btnSalvar.Visible = true; } break;
                    case MsgBotoes.Excluir: { btnExcluir.Visible = true; } break;
                    case MsgBotoes.Alterar: { btnAlterar.Visible = true; } break;
                    case MsgBotoes.Atualizar: { btnAtualizar.Visible = true; } break;
                    case MsgBotoes.Cancelar: { btnCancelar.Visible = true; } break;
                    default:
                        break;
                }
            }
        }
    }

    #region ________________________ Eventos do Botoes ________________________

    public event EventHandler BtnOKClicked;
    public event EventHandler BtnSalvarClicked;
    public event EventHandler BtnExcluirClicked;
    public event EventHandler BtnAlterarClicked;
    public event EventHandler BtnAtualizarClicked;
    public event EventHandler BtnCancelarClicked;

    protected void BtnOK_Click(object sender, EventArgs e)
    {
        this.Hide();
        if (BtnOKClicked != null)
            BtnOKClicked(this, EventArgs.Empty);
    }

    protected void BtnSalvar_Click(object sender, EventArgs e)
    {
        this.Hide();
        if (BtnSalvarClicked != null)
            BtnSalvarClicked(this, EventArgs.Empty);
    }

    protected void BtnExcluir_Click(object sender, EventArgs e)
    {
        this.Hide();
        if (BtnExcluirClicked != null)
            BtnExcluirClicked(this, EventArgs.Empty);
    }

    protected void BtnAlterar_Click(object sender, EventArgs e)
    {
        this.Hide();
        if (BtnAlterarClicked != null)
            BtnAlterarClicked(this, EventArgs.Empty);
    }

    protected void BtnAtualizar_Click(object sender, EventArgs e)
    {
        this.Hide();
        if (BtnAtualizarClicked != null)
            BtnAtualizarClicked(this, EventArgs.Empty);
    }

    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        this.Hide();
        if (BtnCancelarClicked != null)
            BtnCancelarClicked(this, EventArgs.Empty);
    }

    #endregion

    public static void AdicionarMBOX(Button lbtn, string menssagem, string caption)
    {
        lbtn.Attributes.Add("onclick", "javascript:Show('" + menssagem + "', '" + caption + "');");
        lbtn.UseSubmitBehavior = false;
    }

    public static void AdicionarMBOX(Button lbtn, string menssagem, string caption, MsgIcons icon)
    {
        lbtn.Attributes.Add("onclick", "javascript:Show('" + menssagem + "', '" + caption + "', '" + icon.ToString() + "');");
        lbtn.UseSubmitBehavior = false;
    }

    public static void AdicionarMBOX(Button lbtn, string menssagem, string caption, MsgIcons icon, params MsgBotoes[] botoes)
    {
        string saida = "";
        foreach (MsgBotoes item in botoes)
        {
            saida += item.ToString() + ";";
        }
        if (saida.Length > 0)
            saida.Substring(0, saida.Length - 1);
        lbtn.Attributes.Add("onclick", "javascript:Show('" + menssagem + "', '" + caption + "', '" + icon.ToString() + "','" + saida + "');");
        lbtn.UseSubmitBehavior = false;
    }

}



