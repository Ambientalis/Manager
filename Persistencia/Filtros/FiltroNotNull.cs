using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para selecionar objetos cujo atributo informado não seja nulo
    /// </summary>
    public class FiltroIsNotNull : Filtro, IFiltroOu
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">o atributo que não pode ser nulo</param>
        public FiltroIsNotNull(String atributo)
        {
            this.Atributo = atributo;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            c.Add(Restrictions.IsNotNull(this.Atributo));
        }

        public ICriterion GetExpression()
        {
            return Restrictions.IsNotNull(this.Atributo);
        }

    }
}
