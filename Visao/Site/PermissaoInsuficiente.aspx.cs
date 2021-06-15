using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios.Criptografia;

public partial class Site_PermissaoInsuficiente : System.Web.UI.Page
{
    private string codigoEmpresa = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnTelaTelaLogin.PostBackUrl = "~/Account/Login.aspx?ic=" + (Session["idConfig"] != null ? Session["idConfig"].ToString().ToInt32() : 0);
    }
}