using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrdemServico_CadastroDeOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String url = "http://ambientalismanager.com.br/OrdemServico/SolicitacaoAdiamento.aspx";
        if (Request["parametros"] != null && !string.IsNullOrEmpty(Request["parametros"].ToString()))
            url += "?parametros=" + Request["parametros"].ToString();
        Response.Redirect(url);
    }
}