using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para trazer ávidamente uma associação
    /// </summary>
    public class FiltroFetchJoin : Filtro
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">a associação que será trazida ávidamente</param>
        public FiltroFetchJoin(String atributo)
        {
            this.Atributo = atributo;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            c.SetFetchMode(this.Atributo, FetchMode.Join);
        }
    }
}
