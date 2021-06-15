using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Interface Genérica para os Filtros "OU"
    /// <para>Filtros = FiltroEq, FiltroNaoIgual, FiltroPropriedadeNaoIgual, FiltroLike, FiltroMaior, FiltroMaiorOuIgual, FiltroMenor, FiltroMenorOuIgual, FiltroBetween, FiltroIsNull, FiltroNotNull</para>
    /// </summary>
    public interface IFiltroOu
    {
        ICriterion GetExpression();
    }
}
