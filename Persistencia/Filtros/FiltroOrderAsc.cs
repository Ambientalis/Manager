using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Cria um fitro de ordenação ascendente para um atributo
    /// </summary>
    public class FiltroOrderAsc : Filtro
    {
        /// <summary>
        /// Cria o filtro de ordenação ascendente
        /// </summary>
        /// <param name="atributo">o Nome atributo a ser ordenado</param>
        public FiltroOrderAsc(String atributo)
        {
            this.Atributo = atributo;
        }

        /// <summary>
        /// Adiciona o filtro de ordenação ascendente ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.AddOrder(Order.Asc(this.Atributo));
        }
    }
}
