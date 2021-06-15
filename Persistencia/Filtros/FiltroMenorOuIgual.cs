using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro de comparação de um valor com valores menores ou iguais
    /// </summary>
    public class FiltroMenorOuIgual : Filtro, IFiltroOu
    {
        /// <summary>
        /// O Construtor do Filtro
        /// </summary>
        /// <param name="atributo">O atributo referência</param>
        /// <param name="valor">O valor de referência para valores menores ou iguais</param>
        public FiltroMenorOuIgual(String atributo, Object valor)
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
            c.Add(Expression.Le(this.Atributo, this.Valor1));
        }

        public ICriterion GetExpression()
        {
            return Expression.Le(this.Atributo, this.Valor1);
        }
    }
}
