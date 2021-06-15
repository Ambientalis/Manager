<%@ WebHandler Language="C#" Class="HandlerArquivosPedidos" %>

using System;
using System.Web;

public class HandlerArquivosPedidos : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Session["idConfig"] = System.Configuration.ConfigurationManager.AppSettings["idConfig"];
            Utilitarios.Transacao.Instance.Abrir();
            if (context.Request.Files != null && context.Request.Files.Count > 0)
            {
                this.SalvarArquivosPedidos(context.Request.Files[0]);
                return;
            }
        }
        catch (Exception) { }
        finally
        {
            Utilitarios.Transacao.Instance.Fechar();
        }
    }

    private void SalvarArquivosPedidos(HttpPostedFile arquivoPostado)
    {
        string extensao = arquivoPostado.FileName.Substring(arquivoPostado.FileName.LastIndexOf('.'));
        string nome = "";
        string subPath = HttpContext.Current.Session["idConfig"].ToString() + (HttpContext.Current.Session["objeto_upload_pedido"].ToString() == "os" ? "/Ordens/" + HttpContext.Current.Session["id_os_arquivos"].ToString() : "/Pedidos/" + HttpContext.Current.Session["id_pedido_arquivos"].ToString()) + "/Imagens/";
        string path = System.Configuration.ConfigurationManager.AppSettings["pathAplicacao"].ToString() + "/Repositorio/" + subPath;

        do
        {
            nome = Guid.NewGuid().ToString().Substring(0, 10) + extensao;
        } while (System.IO.File.Exists(path + "/" + nome));

        //Criar diretorio
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
        if (!dir.Exists)
            dir.Create();

        arquivoPostado.SaveAs(path + "/" + nome);

        Modelo.Arquivo arq = new Modelo.Arquivo();

        arq.Caminho = Utilitarios.WebUtil.GetPathRepositorio + subPath + nome;
        arq.Nome = arquivoPostado.FileName;
        arq.Host = HttpContext.Current.Request.Url.Authority;
        arq = arq.Salvar();

        if (HttpContext.Current.Session["objeto_upload_pedido"].ToString() == "os")
        {
            Modelo.OrdemServico ordem = Modelo.OrdemServico.ConsultarPorId(HttpContext.Current.Session["id_os_arquivos"].ToString().ToInt32());

            if (ordem != null)
            {
                if (ordem.Arquivos == null)
                    ordem.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

                ordem.Arquivos.Add(arq);
                ordem.Salvar();
            }

        }
        else
        {
            Modelo.Pedido pedido = Modelo.Pedido.ConsultarPorId(HttpContext.Current.Session["id_pedido_arquivos"].ToString().ToInt32());

            if (pedido != null)
            {
                if (pedido.Arquivos == null)
                    pedido.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

                pedido.Arquivos.Add(arq);
                if (pedido.DataAceiteContrato.CompareTo(Modelo.SqlDate.MinValue) <= 0)
                    pedido.DataAceiteContrato = Modelo.SqlDate.MinValue;
                if (pedido.DataEnvioContrato.CompareTo(Modelo.SqlDate.MinValue) <= 0)
                    pedido.DataEnvioContrato = Modelo.SqlDate.MinValue;
                pedido.Salvar();
            }
        }

        arq = arq.Salvar();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}