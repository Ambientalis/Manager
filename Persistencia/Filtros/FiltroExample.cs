using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro de Exemplo, onde um objeto de exemplo é utilizado par consulta em BD
    /// </summary>
    public class FiltroExample : Filtro
    {
        /// <summary>
        /// Construtor para criar o filtro de exemplo
        /// </summary>
        /// <param name="objetoExemplo">O objeto de exemplo</param>
        public FiltroExample(Object objetoExemplo)
        {
            this.Valor1 = objetoExemplo;
        }

        /// <summary>
        /// Adiciona o filtro de exemplo no Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.Add(Example.Create(this.Valor1).ExcludeNulls().ExcludeZeroes().EnableLike());
        }
    }
}
