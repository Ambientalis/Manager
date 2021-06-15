using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro de definição de quantidade máxima de resultados de uma consulta
    /// </summary>
    public class FiltroMaxResults : Filtro
    {
        /// <summary>
        /// Cria o filtro de Quantidade Máxima de Resultados
        /// </summary>
        /// <param name="quantidadeResultadosMax">A quantidade máxima de resultados</param>
        public FiltroMaxResults(int quantidadeResultadosMax)
        {
            this.Valor1 = quantidadeResultadosMax;
        }

        /// <summary>
        /// Adiciona o filtro de quantidade máxima de resultados no Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetMaxResults(Convert.ToInt32("0" + this.Valor1));
        }
    }
}
