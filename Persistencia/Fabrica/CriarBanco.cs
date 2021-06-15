﻿using System.IO;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;

namespace Persistencia.Fabrica
{
    internal class CriarBanco
    {
        public static void CriarBancoComNHibernate(ref NHibernate.Cfg.Configuration _objConf, int idConfig)
        {
            //CRIAR BANCO AUTOMATICAMENTE
            StreamReader str = new StreamReader(ConfigurationManager.AppSettings["pathAplicacao"].ToString() + "/App_Data/ConfiguracoesBanco/" + idConfig.ToString() + ".xml");
            string xml = str.ReadToEnd();
            if (xml.Contains("Source=aragom;") || xml.Contains("Server=aragom;"))
                new SchemaExport(_objConf).SetOutputFile(ConfigurationManager.AppSettings["pathAplicacao"].ToString() + "/create_schema.sql").Execute(true, false, false);
        }
    }
}
