using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Filtros
{
    /// <summary>
    /// COnstrutor do Filtro para definir se os objetos consultados devem ser mantidos no Cache
    /// </summary>
    public class FiltroCache : Filtro
    {
        /// <summary>
        /// Construtor do Filtro
        /// </summary>
        /// <param name="manterObjetoNoCache">Valor que indica se os objetos consulta devem ser mantidos em cache</param>
        public FiltroCache(bool manterObjetoNoCache)
        {
            this.Valor1 = manterObjetoNoCache;
        }

        /// <summary>
        /// Adicionao Filtro Cache ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetCacheable((bool)this.Valor1);
        }
    }
}
