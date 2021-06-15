using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro de ordenação descendente de um atributo
    /// </summary>
    public class FiltroOrderDesc : Filtro
    {
        /// <summary>
        /// Cria um filtro de ordenação descendente
        /// </summary>
        /// <param name="atributo">o Nome do atributo que será ordenado descendentemente</param>
        public FiltroOrderDesc(String atributo)
        {
            this.Atributo = atributo;
        }

        /// <summary>
        /// Adiciona o filtro de ordenaçAO descendente a um Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.AddOrder(Order.Desc(this.Atributo));
        }
    }
}
