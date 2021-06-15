using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para criação de um subcriteria
    /// </summary>
    public class FiltroCreateCriteria : Filtro
    {
        /// <summary>
        /// Construtor para criar o filtro
        /// </summary>
        /// <param name="atributo">o nome da associacao</param>
        public FiltroCreateCriteria(String atributo)
        {
            this.Atributo = atributo;
        }

        public FiltroCreateCriteria(String atributo, String alias)
        {
            this.Atributo = atributo;
            this.Valor1 = alias;
        }

        /// <summary>
        /// Adiciona o filtro ao Criteria
        /// </summary>
        /// <param name="c">O Criteria</param>
        public override void adicionarFiltro(ref ICriteria c)
        {
            if (this.Valor1 == null)
            {
                c = c.CreateCriteria(this.Atributo);
            }
            else
            {
                c = c.CreateCriteria(this.Atributo, this.Valor1.ToString());
            }
        }
    }
}
