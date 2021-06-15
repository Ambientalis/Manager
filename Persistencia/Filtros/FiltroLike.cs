using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro de comparação de parte de valor de atributo
    /// </summary>
    public class FiltroLike : Filtro, IFiltroOu
    {
        /// <summary>
        /// Cria o filtro de comparação de parte de valor
        /// </summary>
        /// <param name="atributo">O atributo a ser comparado</param>
        /// <param name="valor">Parte do valor que este atributo deve conter</param>
        public FiltroLike(String atributo, Object valor)
        {
            this.Atributo = atributo;
            this.Valor1 = valor;
        }

        /// <summary>
        /// Adiciona o filtro de parte de valor no Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            c.Add(Expression.Like(this.Atributo, "%" + this.Valor1.ToString() + "%"));
        }

        public ICriterion GetExpression()
        {
            return Expression.Like(this.Atributo, "%" + this.Valor1.ToString() + "%");
        }

        #region IFiltroOu Members

        #endregion
    }
}
