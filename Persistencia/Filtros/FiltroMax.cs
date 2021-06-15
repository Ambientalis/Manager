using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para retornar o ojeto que tem a maior propriedade passada
    /// </summary>
    public class FiltroMax : Filtro
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">o atributo a ser comparado</param>
        public FiltroMax(String atributo)
        {
            this.Atributo = atributo;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetMaxResults(1);
            c.AddOrder(Order.Desc(this.Atributo));
        }
    }
}
