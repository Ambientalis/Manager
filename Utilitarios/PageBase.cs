using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using System.IO.Compression;
using Utilitarios;
using Modelo;

namespace Utilitarios
{
    [Serializable]
    public class PageBase : Page
    {
        protected Funcionario FuncionarioLogado
        {
            get
            {
                return (Funcionario)Session["usuario_logado"];
            }
        }

        public PageBase()
        {
        }

        protected override void OnPreInit(EventArgs e)
        {
            //if (Page.Request.ServerVariables["http_user_agent"].ToLower().Contains("safari"))
            //    Page.ClientTarget = "uplevel";
            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (ConfigurationManager.AppSettings["TituloAplicacao"] != null)
            {
                this.Title = ConfigurationManager.AppSettings["TituloAplicacao"].ToString();
            }
        }

        #region __________ Page Compress _____________

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new StringWriter()))
        //    {
        //        if (htmlwriter.GetType() == typeof(Image) || htmlwriter.GetType() == typeof(HtmlImage) || htmlwriter.GetType() == typeof(ImageButton))
        //            htmlwriter.AddAttribute("alt", ".");
        //        base.Render(htmlwriter);
        //        string html = htmlwriter.InnerWriter.ToString().Trim();

        //        if (true)
        //        {
        //            bool isAsync = !html.StartsWith("<");

        //            if (!isAsync)
        //            {
        //                StringBuilder sb = PageBase._TrimHtml(html);
        //                writer.Write(sb.ToString());
        //            }
        //            else
        //            {
        //                writer.Write(html);
        //            }
        //        }
        //        else
        //        {
        //            writer.Write(html);
        //        }
        //    }
        //    writer.Close();
        //    writer.Dispose();
        //}

        //private static StringBuilder _TrimHtml(string source)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    source = source.Trim();
        //    using (StringReader sr = new StringReader(source))
        //    {
        //        string data = string.Empty;
        //        while (data != null)
        //        {
        //            data = sr.ReadLine();
        //            if (data != null)
        //            {
        //                data = data.TrimStart(' ', '\t');
        //                if (data != string.Empty) sb.AppendLine(data);
        //            }
        //        }
        //    }

        //    return sb;
        //}


        #endregion

        public static string GetIdConfig
        {
            get
            {
                return WebUtil.GetIdConfig;
            }
        }

        public string IdConfig
        {
            get
            {
                return WebUtil.GetIdConfig;
            }
        }

        public static string GetPathAplicacao
        {
            get
            {
                return WebUtil.GetPathAplicacao;
            }
        }

        public string PathAplicacao
        {
            get
            {
                return WebUtil.GetPathAplicacao;
            }
        }

        public T GetMBOX<T>() where T : Control
        {
            return (T)this.Page.Master.FindControl("MBOX1");
        }

    }
}
