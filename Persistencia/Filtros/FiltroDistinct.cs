using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para distinçao de registros duplicados
    /// </summary>
    public class FiltroDistinct : Filtro
    {
        /// <summary>
        /// Construtor do filtro 
        /// </summary>
        public FiltroDistinct()
        {
        }
        /// <summary>
        /// Adiciona um filtro Distinct a um Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetResultTransformer(CriteriaSpecification.DistinctRootEntity);
        }
    }
}
