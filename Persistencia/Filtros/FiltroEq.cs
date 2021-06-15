using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para comparação de igualdade de valor de atributo
    /// </summary>
    public class FiltroEq : Filtro, IFiltroOu
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">O atributo a ser comparado</param>
        /// <param name="valor">O valor que o atributo deve ter</param>
        public FiltroEq(String atributo, Object valor)
        {
            this.Atributo = atributo;
            this.Valor1 = valor;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            c.Add(Expression.Eq(this.Atributo, this.Valor1));
        }

        //Expressão para o FiltroOu
        public ICriterion GetExpression()
        {
            return Expression.Eq(this.Atributo, this.Valor1);
        }

        #region IFiltroOu Members

        #endregion
    }
}
