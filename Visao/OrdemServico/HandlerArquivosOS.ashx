<%@ WebHandler Language="C#" Class="HandlerArquivosOS" %>

using System;
using System.Web;

public class HandlerArquivosOS : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    private Modelo.Funcionario FuncionarioLogado
    {
        get
        {
            return (Modelo.Funcionario)HttpContext.Current.Session["usuario_logado"];
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Session["idConfig"] = System.Configuration.ConfigurationManager.AppSettings["idConfig"];
            Utilitarios.Transacao.Instance.Abrir();
            if (context.Request.Files != null && context.Request.Files.Count > 0)
            {
                this.SalvarArquivosOS(context.Request.Files[0]);
                return;
            }
        }
        catch (Exception) { }
        finally
        {
            Utilitarios.Transacao.Instance.Fechar();
        }
    }

    private void SalvarArquivosOS(HttpPostedFile arquivoPostado)
    {
        string extensao = arquivoPostado.FileName.Substring(arquivoPostado.FileName.LastIndexOf('.'));
        string nome = "";
        string subPath = "";

        if (HttpContext.Current.Session["objeto_upload_OS"].ToString() == "visita")
            subPath = HttpContext.Current.Session["idConfig"].ToString() + "/Visitas/" + HttpContext.Current.Session["id_visita_arquivos"].ToString() + "/Imagens/";

        if (HttpContext.Current.Session["objeto_upload_OS"].ToString() == "atividade")
            subPath = HttpContext.Current.Session["idConfig"].ToString() + "/Atividades/" + HttpContext.Current.Session["id_atividade_arquivos"].ToString() + "/Imagens/";

        if (HttpContext.Current.Session["objeto_upload_OS"].ToString() == "os")
            subPath = HttpContext.Current.Session["idConfig"].ToString() + "/Ordens/" + HttpContext.Current.Session["id_os_arquivos"].ToString() + "/Imagens/";

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

        if (HttpContext.Current.Session["objeto_upload_OS"].ToString() == "visita")
        {
            Modelo.Visita visita = Modelo.Visita.ConsultarPorId(HttpContext.Current.Session["id_visita_arquivos"].ToString().ToInt32());

            if (visita != null)
            {
                if (visita.Arquivos == null)
                    visita.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

                visita.Arquivos.Add(arq);
                visita.Salvar();
            }
        }


        if (HttpContext.Current.Session["objeto_upload_OS"].ToString() == "atividade")
        {
            Modelo.Atividade atividade = Modelo.Atividade.ConsultarPorId(HttpContext.Current.Session["id_atividade_arquivos"].ToString().ToInt32());

            if (atividade != null)
            {
                if (atividade.Arquivos == null)
                    atividade.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

                atividade.Arquivos.Add(arq);
                atividade.Salvar();
            }
        }

        if (HttpContext.Current.Session["objeto_upload_OS"].ToString() == "os")
        {
            Modelo.OrdemServico os = Modelo.OrdemServico.ConsultarPorId(HttpContext.Current.Session["id_os_arquivos"].ToString().ToInt32());

            if (os != null)
            {
                if (os.Arquivos == null)
                    os.Arquivos = new System.Collections.Generic.List<Modelo.Arquivo>();

                os.Arquivos.Add(arq);
                os.Salvar();
                this.IncluirRegistroInclusaoArquivoNoDetalhamentoDaOS(os, arq);
            }
        }

        arq = arq.Salvar();
    }

    private void IncluirRegistroInclusaoArquivoNoDetalhamentoDaOS(Modelo.OrdemServico ordem, Modelo.Arquivo arquivo)
    {
        if (ordem.Detalhamentos == null)
            ordem.Detalhamentos = new System.Collections.Generic.List<Modelo.Detalhamento>();

        Modelo.Detalhamento detalhamento = new Modelo.Detalhamento();
        detalhamento.DataSalvamento = DateTime.Now;
        Modelo.Detalhamento utimoDetalhamento = ordem.GetUltimoDelhamento;
        detalhamento.Conteudo = utimoDetalhamento != null ? utimoDetalhamento.Conteudo : "";
        detalhamento.Conteudo += @"<br /><br />Arquivo " + arquivo.Nome + " Incluído em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". Usuário: " + this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento.Usuario = this.FuncionarioLogado.NomeRazaoSocial;
        detalhamento = detalhamento.Salvar();
        ordem.Detalhamentos.Add(detalhamento);
        ordem = ordem.Salvar();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}