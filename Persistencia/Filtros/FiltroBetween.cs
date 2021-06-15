using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para comparação de atributos entre dois valores
    /// </summary>
    public class FiltroBetween : Filtro, IFiltroOu
    {
        /// <summary>
        /// Construtor do filtro Between
        /// </summary>
        /// <param name="atributo">O atributo a ser comparado</param>
        /// <param name="valorMin">O valor mínimo (inicial) do atributo</param>
        /// <param name="valorMax">O valor máximo (final) do atributo</param>
        public FiltroBetween(String atributo, Object valorMin, Object valorMax)
        {
            this.Atributo = atributo;
            this.Valor1 = valorMin;
            this.Valor2 = valorMax;

        }
        /// <summary>
        /// Adiciona um filtro Between a um Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.Add(Expression.Between(this.Atributo, this.Valor1, this.Valor2));
        }

        public ICriterion GetExpression()
        {
            return Expression.Between(this.Atributo, this.Valor1, this.Valor2);
        }
    }
}
