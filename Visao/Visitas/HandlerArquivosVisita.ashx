<%@ WebHandler Language="C#" Class="HandlerArquivosVisita" %>

using System;
using System.Web;

public class HandlerArquivosVisita : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Session["idConfig"] = System.Configuration.ConfigurationManager.AppSettings["idConfig"];
            Utilitarios.Transacao.Instance.Abrir();
            if (context.Request.Files != null && context.Request.Files.Count > 0)
            {
                this.SalvarArquivosVisita(context.Request.Files[0]);
                return;
            }
        }
        catch (Exception) { }
        finally
        {
            Utilitarios.Transacao.Instance.Fechar();
        }
    }

    private void SalvarArquivosVisita(HttpPostedFile arquivoPostado)
    {
        string extensao = arquivoPostado.FileName.Substring(arquivoPostado.FileName.LastIndexOf('.'));
        string nome = "";
        string subPath = HttpContext.Current.Session["idConfig"].ToString() + "/Visitas/" + HttpContext.Current.Session["id_visita_arquivos"].ToString() + "/Imagens/";

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

        Modelo.Visita visita = Modelo.Visita.ConsultarPorId(HttpContext.Current.Session["id_visita_arquivos"].ToString().ToInt32());


        if (visita != null)
        {
            if (visita.Arquivos == null)
                visita.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

            visita.Arquivos.Add(arq);
            visita.Salvar();
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