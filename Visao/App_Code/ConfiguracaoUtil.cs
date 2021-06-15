using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilitarios;

/// <summary>
/// Summary description for ConfiguracaoUtil
/// </summary>
public class ConfiguracaoUtil
{
    private static Modelo.Configuracoes config;
    private static Modelo.Configuracoes ConfigInstance
    {
        get
        {
            if (ConfiguracaoUtil.config == null)
                ConfiguracaoUtil.config = Modelo.Configuracoes.GetConfiguracoesSistema();
            return ConfiguracaoUtil.config;
        }
    }

    public static void RefreshConfig()
    {
        ConfiguracaoUtil.config = Modelo.Configuracoes.GetConfiguracoesSistema();
    }
    public static string GetCorPadrao
    {
        get
        {
            return ConfiguracaoUtil.ConfigInstance.CorPadrao;
        }
    }
    public static string GetLinkLogomarca
    {
        get
        {
            return ConfiguracaoUtil.ConfigInstance.LinkLogomarca;
        }
    }
    public static string GetFavIcon
    {
        get
        {
            return ConfiguracaoUtil.ConfigInstance.FavIcon;
        }
    }
    public static string GetContatoEmpresa
    {
        get
        {
            return ConfiguracaoUtil.ConfigInstance.ContatoEmpresa;
        }
    }
}