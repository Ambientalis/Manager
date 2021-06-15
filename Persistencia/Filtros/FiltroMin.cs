using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para retornar o ojeto que tem a menor propriedade passada
    /// </summary>
    public class FiltroMin : Filtro
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">o atributo a ser comparado</param>
        public FiltroMin(String atributo)
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
            c.AddOrder(Order.Asc(this.Atributo));
        }
    }
}
