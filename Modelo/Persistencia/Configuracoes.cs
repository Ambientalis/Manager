using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class Configuracoes : ObjetoBase
    {
        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual Configuracoes Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<Configuracoes>(this);
        }

        public static Configuracoes GetConfiguracoesSistema()
        {
            Configuracoes config = new Configuracoes();
            config.AdicionarFiltro(Filtros.MaxResults(1));
            config = new FabricaDAONHibernateBase().GetDAOBase().ConsultarUnicoComFiltro(config);
            if (config == null)
                return new Configuracoes().Salvar();
            return config;
        }
    }
}
