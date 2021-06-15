using System.Web;
using System.Text;
using System.Web.UI;


namespace Utilitarios
{
    /// <summary>
    /// A JavaScript Alert
    /// </summary>
    public static class Alert
    {

        /// <summary>
        /// Mostra uma mensagem de alerta
        /// </summary>
        /// <param name="message">A mensagem mostrada pelo alerta</param>
        public static void Show(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                string cleanMessage = message.Replace("'", "\'");
                string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

                Page page = HttpContext.Current.CurrentHandler as Page;
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script);
                }
            }
        }
    }
}