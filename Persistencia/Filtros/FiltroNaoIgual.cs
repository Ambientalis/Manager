using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para comparação de não igualdade
    /// </summary>
    public class FiltroNaoIgual : Filtro, IFiltroOu
    {
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="atributo">o atributo a ser comparado</param>
        /// <param name="valor">O valor de comparação</param>
        public FiltroNaoIgual(String atributo, Object valor)
        {
            this.Atributo = atributo;
            this.Valor1 = valor;
        }

        /// <summary>
        /// Adiciona o Filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.Add(!Expression.Eq(this.Atributo, this.Valor1));
        }

        public ICriterion GetExpression()
        {
            return !Expression.Eq(this.Atributo, this.Valor1);
        }

    }
}
