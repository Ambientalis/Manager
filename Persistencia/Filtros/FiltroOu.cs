using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    public class FiltroOu : Filtro
    {
        IList<IFiltroOu> filtros = new List<IFiltroOu>();

        /// <summary>
        /// Construtor do Filtro Ou
        /// </summary>
        /// <param name="filtros">Filtros = FiltroEq, FiltroNaoIgual, FiltroPropriedadeNaoIgual, FiltroLike, FiltroMaior, FiltroMaiorOuIgual, FiltroMenor, FiltroMenorOuIgual, FiltroBetween, FiltroIsNull, FiltroNotNull</param>
        public FiltroOu(params Filtro[] filtros)
        {
            this.filtros = filtros.Cast<IFiltroOu>().ToList<IFiltroOu>();
        }

        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            if (this.filtros != null)
                c.Add(this.MontarOrs(this.filtros));
        }

        private ICriterion MontarOrs(IList<IFiltroOu> fitrosOu)
        {
            if (fitrosOu.Count > 1)
            {
                IFiltroOu primeiroFiltro = fitrosOu[0];
                fitrosOu.RemoveAt(0);
                return Restrictions.Or(primeiroFiltro.GetExpression(), this.MontarOrs(fitrosOu));
            }
            return fitrosOu[0].GetExpression();
        }
    }
}
