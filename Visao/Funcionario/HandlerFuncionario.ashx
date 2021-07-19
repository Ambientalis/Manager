<%@ WebHandler Language="C#" Class="HandlerFuncionario" %>

using System;
using System.Web;
using Persistencia.Services;


public class HandlerFuncionario : IHttpHandler, System.Web.SessionState.IRequiresSessionState  
{    
    public void ProcessRequest (HttpContext context) 
    {
        try
        {
            context.Session["idConfig"] = System.Configuration.ConfigurationManager.AppSettings["idConfig"];
            Utilitarios.Transacao.Instance.Abrir();
            if (context.Request.Files != null && context.Request.Files.Count > 0)
            {
                this.SalvarArquivoFuncionario(context.Request.Files[0]);
                return;
            }
        }
        catch (Exception) { }
        finally
        {
            Utilitarios.Transacao.Instance.Fechar();
        }
    }

    private void SalvarArquivoFuncionario(HttpPostedFile arquivoPostado)
    {
        string extensao = arquivoPostado.FileName.Substring(arquivoPostado.FileName.LastIndexOf('.'));
        string nome = "";
        string subPath = HttpContext.Current.Session["idConfig"].ToString() + "/Funcionarios/" + HttpContext.Current.Session["idFuncionario"].ToString() + "/Imagens/";
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

        Modelo.Imagem arq = new Modelo.Imagem();

        arq.Caminho = Utilitarios.WebUtil.GetPathRepositorio + subPath + nome;
        arq.Nome = arquivoPostado.FileName;
        arq.Host = HttpContext.Current.Request.Url.Authority;
        arq = arq.Salvar();

        Modelo.Funcionario p = Modelo.Funcionario.ConsultarPorId(HttpContext.Current.Session["idFuncionario"].ToString().ToInt32());
        p.Imagem = arq;
        p.Salvar();

        arq = arq.Salvar();
    }
 
    public bool IsReusable 
    {
        get {
            return false;
        }
    }

}