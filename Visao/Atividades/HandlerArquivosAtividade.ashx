<%@ WebHandler Language="C#" Class="HandlerArquivosAtividade" %>

using System;
using System.Web;
using Persistencia.Services;

public class HandlerArquivosAtividade : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        try
        {
            context.Session["idConfig"] = System.Configuration.ConfigurationManager.AppSettings["idConfig"];
            Utilitarios.Transacao.Instance.Abrir();
            if (context.Request.Files != null && context.Request.Files.Count > 0)
            {
                this.SalvarArquivosAtividade(context.Request.Files[0]);
                return;
            }
        }
        catch (Exception) { }
        finally
        {
            Utilitarios.Transacao.Instance.Fechar();
        }
    }

    private void SalvarArquivosAtividade(HttpPostedFile arquivoPostado)
    {
        string extensao = arquivoPostado.FileName.Substring(arquivoPostado.FileName.LastIndexOf('.'));
        string nome = "";
        string subPath = HttpContext.Current.Session["idConfig"].ToString() + "/Atividades/" + HttpContext.Current.Session["id_atividade_arquivos"].ToString() + "/Imagens/";

        string path = PathApplication.pathApplication + "/Repositorio/" + subPath;

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

        Modelo.Atividade atividade = Modelo.Atividade.ConsultarPorId(HttpContext.Current.Session["id_atividade_arquivos"].ToString().ToInt32());


        if (atividade != null)
        {
            if (atividade.Arquivos == null)
                atividade.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

            atividade.Arquivos.Add(arq);
            atividade.Salvar();
        }


        arq = arq.Salvar();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}