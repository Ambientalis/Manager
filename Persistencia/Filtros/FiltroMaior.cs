using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para a comparação de um valor com valores maiores que tal
    /// </summary>
    public class FiltroMaior : Filtro, IFiltroOu
    {
        /// <summary>
        /// O Construtor do Filtro
        /// </summary>
        /// <param name="atributo">O atributo referência</param>
        /// <param name="valor">O valor referência para valores maiores</param>
        public FiltroMaior(String atributo, Object valor)
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
            c.Add(Expression.Gt(this.Atributo, this.Valor1));
        }

        public ICriterion GetExpression()
        {
            return Expression.Gt(this.Atributo, this.Valor1);
        }

        #region IFiltroOu Members

        #endregion
    }
}
