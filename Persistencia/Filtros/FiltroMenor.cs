using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para a comparação de um valor com valores menores
    /// </summary>
    public class FiltroMenor : Filtro, IFiltroOu
    {
        /// <summary>
        /// O Construtor do Filtro
        /// </summary>
        /// <param name="atributo">O Atributo referência</param>
        /// <param name="valor">O valor de referência para valores menores</param>
        public FiltroMenor(String atributo, Object valor)
        {
            this.Atributo = atributo;
            this.Valor1 = valor;
        }

        /// <summary>
        /// Adiciona o Filtro
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.Add(Expression.Lt(this.Atributo, this.Valor1));
        }

        public ICriterion GetExpression()
        {
            return Expression.Lt(this.Atributo, this.Valor1);
        }

        #region IFiltroOu Members

        #endregion
    }
}
