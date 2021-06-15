using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Filtros
{
    /// <summary>
    /// Filtro para retorno de uma faixa em específico de objetos
    /// </summary>
    public class FiltroFaixaResultado : Filtro
    {
        /// <summary>
        /// Construtor do Filtro
        /// </summary>
        /// <param name="inicio">O início numérico da faixa a ser retornada; Valores negativos representam inicio não especificado</param>
        /// <param name="fim">O fim numérico da faixa a ser retornada; Valores negativos representam fim não especificado</param>
        /// <!--Esse Filtro não guarda os objetos no cache-->
        public FiltroFaixaResultado(int inicio, int fim)
        {
            this.Valor1 = inicio > -1 ? inicio : 0;
            this.Valor2 = (fim - inicio) > 0 ? fim - inicio : int.MaxValue;
        }

        /// <summary>
        /// Adiciona o Filtro Faixa Resultado ao criteria
        /// </summary>
        /// <param name="c">O criteria</param>
        public override void adicionarFiltro(ref NHibernate.ICriteria c)
        {
            c.SetFirstResult(Convert.ToInt32(this.Valor1));
            c.SetMaxResults(Convert.ToInt32(this.Valor2));
        }
    }
}
