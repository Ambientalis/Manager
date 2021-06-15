using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para selecionar objetos cujo atributo informado seja nulo
    /// </summary>
    public class FiltroIsNull : Filtro, IFiltroOu
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">o atributo que será verificado se é nulo</param>
        public FiltroIsNull(String atributo)
        {
            this.Atributo = atributo;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            c.Add(Restrictions.IsNull(this.Atributo));
        }

        //Expressão para o FiltroOu
        public ICriterion GetExpression()
        {
            return Restrictions.IsNull(this.Atributo);
        }

        #region IFiltroOu Members

        #endregion
    }
}
