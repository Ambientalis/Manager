using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisWebControls;
using Utilitarios;

public partial class OrdemServico_AtualizarOS : PageBase
{
    private Msg msg = new Msg();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAtualizarOS_Click(object sender, EventArgs e)
    {
        try
        {
            this.AtualizarOSsInformadas();
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

    private void AtualizarOSsInformadas()
    {
        String[] codigos = tbxCodigoOS.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string codigo in codigos)
            if (codigo.IsNotNullOrEmpty())
            {
                try
                {
                    OSUtil.AlterarOsVencida(codigo.Trim());
                }
                catch (Exception ex)
                {
                    msg.CriarMensagem(ex);
                    C2MessageBox.Show(msg);
                }
            }
        msg.CriarMensagem("Processo finalizado!", "Informação", MsgIcons.Informacao);
    }
}