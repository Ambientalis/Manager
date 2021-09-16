using System.IO;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
using Persistencia.Services;

namespace Persistencia.Fabrica
{
    internal class CriarBanco
    {
        public static void CriarBancoComNHibernate(ref NHibernate.Cfg.Configuration _objConf, int idConfig)
        {
            //CRIAR BANCO AUTOMATICAMENTE

            string x = "C:\\source\\Manager\\Visao";

            StreamReader str = new StreamReader(x + "/App_Data/ConfiguracoesBanco/" + idConfig.ToString() + ".xml");
            string xml = str.ReadToEnd();
            if (xml.Contains("Source=ambientalis;") || xml.Contains("Server=51.161.1.128;"))
                new SchemaExport(_objConf).SetOutputFile(PathApplication.pathApplication + "/create_schema.sql").Execute(true, false, false);
        }
    }
}
