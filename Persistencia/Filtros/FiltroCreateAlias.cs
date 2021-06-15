using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Cria um Alias para uma associação
    /// </summary>
    public class FiltroCreateAlias : Filtro
    {
        /// <summary>
        /// Construtor para criar o Alias
        /// </summary>
        /// <param name="associacao">o Nome da associação</param>
        /// <param name="alias">O Nome do alias</param>
        public FiltroCreateAlias(String associacao, String alias)
        {
            this.Valor1 = associacao;
            this.Valor2 = alias;
        }

        /// <summary>
        /// Cria um Alias que representa uma associação em um Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.CreateAlias(this.Valor1.ToString(), this.Valor2.ToString());
        }
    }
}
