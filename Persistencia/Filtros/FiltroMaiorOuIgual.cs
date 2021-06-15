using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para a comparação de um valor com valores maiores ou iguais
    /// </summary>
    public class FiltroMaiorOuIgual : Filtro, IFiltroOu
    {
        /// <summary>
        /// O Construtor do Filtro
        /// </summary>
        /// <param name="atributo">O atributo referência</param>
        /// <param name="valor">O valor de referência para valores maiores ou iguais</param>
        public FiltroMaiorOuIgual(String atributo, Object valor)
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
            c.Add(Expression.Ge(this.Atributo, this.Valor1));
        }

        public ICriterion GetExpression()
        {
            return Expression.Ge(this.Atributo, this.Valor1);
        }

        #region IFiltroOu Members

        #endregion
    }
}
